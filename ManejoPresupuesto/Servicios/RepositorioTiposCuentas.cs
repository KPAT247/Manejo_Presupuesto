using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{

    public interface IRepositorioTiposCuentas
    {
        Task Actualizar(TipoCuenta tipoCuenta);
        Task Borrar(int id);
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string nombre, int usuarioId);
        Task<IEnumerable<TipoCuenta>> obtener(int usuarioId);
        Task<TipoCuenta> ObtenerPorId(int Id, int usuarioId);
        Task Ordenar(IEnumerable<TipoCuenta> tipoCuentasOrdenados);
    }

    public class RepositorioTiposCuentas: IRepositorioTiposCuentas
    {
        private readonly string connectionString;
        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>
                                                 ("TiposCuentas_Insertar",
                                                 new {usuarioId = tipoCuenta.UsuarioId,
                                                 nombre = tipoCuenta.Nombre}, 
                                                 commandType: System.Data.CommandType.StoredProcedure);
            tipoCuenta.Id =  id;
        }

        public async Task<bool> Existe(string nombre, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                @"SELECT 1 
                FROM TiposCuentas 
                WHERE Nombre = @Nombre AND UsuarioId = @UsuarioId;", 
                new {nombre, usuarioId});
            return existe == 1;
        }

        public async Task<IEnumerable<TipoCuenta>> obtener(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<TipoCuenta>(@"SELECT Id, Nombre, Orden
                                                            from TiposCuentas
                                                            WHERE UsuarioId = @UsuarioId
                                                            ORDER BY Orden", new {usuarioId});
        }

        public async Task Actualizar(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE TiposCuentas 
                                            SET Nombre = @Nombre 
                                            WHERE Id = @Id", tipoCuenta);
        }

        public async Task<TipoCuenta> ObtenerPorId(int Id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<TipoCuenta>(@"
                                                                            SELECT Id, Nombre, Orden
                                                                             FROM TiposCuentas 
                                                                            WHERE Id = @Id AND UsuarioId = @UsuarioId", new {Id, usuarioId});
        }

        public async Task Borrar(int id) 
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE TiposCuentas WHERE Id = @Id", new { id });
        }


        /// <summary>
        /// Actualiza el número de posición/orden de múltiples tipos de cuentas en la base de datos.
        /// </summary>
        /// <param name="tipoCuentasOrdenados">Colección de objetos TipoCuenta con sus respectivos Id y nuevo Orden.</param>
        public async Task Ordenar(IEnumerable<TipoCuenta> tipoCuentasOrdenados)
        {
            // Sentencia SQL parametrizada para actualizar la columna Orden según el Id
            var query = "UPDATE TiposCuentas SET Orden = @Orden WHERE Id = @Id;";
            // Abre la conexión a SQL Server
            using var connection = new SqlConnection(connectionString);
            // Dapper empaqueta la lista e ejecuta un UPDATE masivo mapeando @Orden y @Id
            await connection.ExecuteAsync(query, tipoCuentasOrdenados);
        }
    }
}

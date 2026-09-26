# 💰 Manejo de Presupuesto - ASP.NET Core 9 MVC

Una aplicación web moderna y robusta para la gestión y control de finanzas personales, desarrollada con **ASP.NET Core 9**, **Dapper** y **SQL Server**[cite: 1]. El proyecto implementa arquitectura en capas, principios **SOLID**, inyección de dependencias y componentes interactivos de interfaz de usuario[cite: 1].

---

## 🚀 Características Principales

* **Gestión de Tipos de Cuentas (CRUD Completo):** Creación, edición, eliminación y listado de tipos de cuentas financieras.
* **Reordenamiento Dinámico (Drag & Drop):** Interfaz interactiva mediante jQuery UI que permite reordenar la jerarquía de cuentas con actualización asíncrona (AJAX/Fetch) en la base de datos.
* **Módulo de Cuentas Financieras:** Registro de cuentas asociadas a tipos de cuentas con control de balance inicial y descripciones[cite: 1, 3].
* **Validaciones Personalizadas:** Atributos de validación a nivel de servidor (ej. `PrimeraLetraMayusculaAttribute`) y validación Unobtrusive en el cliente para una experiencia fluida.
* **Seguridad y Control de Acceso:** Aislamiento de datos por usuario (`UsuarioId`) y protección en peticiones masivas mediante validaciones de pertenencia de recursos.

---

## 🛠️ Tecnologías y Herramientas Utilizadas

### **Backend**
* **Framework:** .NET 9 / ASP.NET Core MVC[cite: 1]
* **ORM / Data Access:** [Dapper](https://github.com/DapperLib/Dapper) (Micro-ORM ligero y de alto rendimiento)
* **Base de Datos:** Microsoft SQL Server (Transact-SQL & Stored Procedures)
* **Inyección de Dependencias:** IoC Container nativo de .NET[cite: 1]

### **Frontend & UI**
* **Motor de Vistas:** Razor Pages / CSHTML[cite: 1]
* **Estilos:** Bootstrap 5 & CSS Isolation
* **Interactividad:** JavaScript (ES6+), jQuery & jQuery UI (Sortable)
* **Validación en Cliente:** jQuery Validation Unobtrusive

---

## 🏗️ Arquitectura y Patrones de Diseño

El proyecto sigue una estructura limpia basada en patrones de diseño empresariales:

* **Pattern Repository:** Encapsulamiento del acceso a datos mediante interfaces (`IRepositorioTiposCuentas`, `IRepositorioCuentas`) para desacoplar la lógica de negocio de la persistencia[cite: 1].
* **Dependency Injection (DI):** Registro de servicios con ciclos de vida adecuados (`Scoped` / `Transient`) en `Program.cs`[cite: 1].
* **Pattern PRG (Post-Redirect-Get):** Prevención de reenvíos duplicados de formularios en operaciones de modificación y borrado.
* **ViewModels:** Desacoplamiento de entidades de base de datos respecto a los datos requeridos por la vista (ej. `CuentaCreacionViewModel`)[cite: 2].

---

## 📁 Estructura del Proyecto

```text
ManejoPresupuesto/
├── Controllers/              # Controladores MVC (TiposCuentasController, CuentasController, etc.)
├── Models/                   # Entidades de dominio y ViewModels
├── Servicios/                # Repositorios (Dapper) e Interfaces de acceso a datos
├── Validaciones/             # Atributos de validación personalizados
├── Views/                    # Vistas Razor estructuradas por módulo
│   ├── Cuentas/
│   ├── TiposCuentas/
│   └── Shared/               # Layouts, estilos e inyecciones de scripts globales
├── wwwroot/                  # Archivos estáticos (CSS, JS, imágenes)
├── Program.cs                # Configuración de Middleware e Inyección de Dependencias
└── appsettings.json          # Cadena de conexión y configuración del entorno

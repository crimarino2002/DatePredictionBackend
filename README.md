# SalesDatePrediction

Descripción
Microservicio backend en .NET 8 para predicción de órdenes y gestión de ordenes, conectado a base de datos SQL Server mediante procedimientos almacenados. Incluye documentación Postman y frontend Angular. Ver documentacion y casos de uso en postman.
https://documenter.getpostman.com/view/44292827/2sB2j6AB81

* Tecnologías utilizadas
  * .NET 8
  * SQL Server (lógica implementada mediante procedimientos almacenados)
  * Entity Framework Core
  * Swashbuckle (Swagger)

* Estructura del proyecto
  * Controllers: expone los endpoints REST
    * GET /Customers/GetAll
    * GET /Orders/GetOrdersBy/{customerId}
    * POST /Orders/Create
    * GET /Product/GetAll
    * GET /Employees/GetAll
    * GET /Shippers/GetAll

* Services: lógica de escritura y lectura del negocio que consume SPs
  * ReadService
  * WriteService

* Entites: definición de DTO´s
  * Customer
  * Employee
  * Order
  * Product
  * Shipper

* Models: modelos genericos de conexión
  * DtoBase
  * Order
  * ApiResponse
  * PagedResponse
  + ResponseMetadata

* DbContext: configuración de conexión a la base de datos

* Requisitos previos
  * .NET SDK 8 instalado
  * SQL Server en ejecución con base de datos StoreSample
  * Node.js y Angular CLI (para el frontend)

* Configuración y ejecución local Backend
  * git clone https://github.com/crimarino2002/DatePredictionBackend.git
  * Configura la cadena de conexión en appsettings.json: ConnectionStrings.SqlServer
  * Ejecuta el backend:
    * dotnet run
    * dotnet run --project ./DatePredictionBackend/DatePredictionBackend.csproj
  * Abre Swagger:
    * http://localhost:5027/swagg

* Frontend (Angular)
  * git clone https://github.com/crimarino2002/DatePredictionFrontend.git
  * Entra a la carpeta del frontend:
  * cd DatePredictionFrontend
  * ng serve
    * http://localhost:4200

* D3
  * https://codepen.io/crimarino2002/pen/xbbjQNQ

Notas
* Todos los procesos de predicción y consulta avanzada están encapsulados en procedimientos almacenados en SQL Server.
* La API está lista para ser conectada a microservicios adicionales si es necesario escalar el sistema.
* La colección tiene adjuntas las variables modificables para la utlización de los servicios.
* Los SPs puedes descargarlos en este repositorio. https://github.com/crimarino2002/SPs

  
Autor
* Nombre: Cristhian Mariño
* LinkedIn: https://www.linkedin.com/in/cristhian-almg/

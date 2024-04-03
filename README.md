# Proyecto Tarjeta Crédito Backend

#### ¡Bienvenido al proyecto de Tarjeta de Crédito Backend!

El siguiente proyecto es una pequeña demo de un sistema backend que simula la gestión de la información de una tarjeta de crédito, así como de compras y pagos.

A continuación se detalla más sobre el proyecto:

## Tecnologías utilizadas en el proyecto:

- ASP.CORE
- Entity Framework (ORM)
- AutoMapper
- Swagger

## Prerrequisitos

- Deberás contar con la versión de .NET 6 instalada
- Deberás tener instalado Visual Studio IDE para poder ejecutar el proyecto
- Deberás tener instalado SQL Server o Docker

## Pasos para ejecutar la aplicación

Para poder ejecutar el proyecto con éxito deberá seguir los siguientes pasos:

1. Si tienes SQL Server instalado de forma local, te puedes saltar este paso.
    - Abre la terminal de su gusto (cmd, powershell, etc.) y sitúese en el directorio del proyecto\
    ```PS C:\Users\TuUsuario> cd <directorio-del proyecto>```
    - Ejecuta el siguiente comando\ 
    ```PS C:\Users\TuUsuario> docker-compose up -d```
    - Deberías tener una salida como la siguiente:\
    ```[+] Running 2/2```\
    ``` ✔ Network tarjetacreditobackend_default    Created     0.0s```\
    ``` ✔ Container sql-server                     Started     0.3s```

2. Ingresa a la base de datos, ya sea a través de la consola o de tu cliente preferido (como puede ser SQL Server Management Studio Management Studio), si usaste docker para crear el contendor de SQL Server las credenciales son las siguientes:
    - Usuario: sa
    - Contraseña: 1234

    **NOTA**: Puedes cambiar esta información en el archivo **docker-compose.yml**

3. Ahora ejecuta los scripts que se encuentran en el directorio Scripts\SCRIPTS_CREATE_AND_INSERT, en el siguiente orden
     - 0.CREATE_DATABASE.sql
     - 1.DDL_SCRIPS.sql
     - 2.DML_SCRIPS.sql

4. Ahora ejecuta los scripts que se encuentran en el directorio Scripts\SCRIPTS_PROCEDURE, en el siguiente orden
     - 1.MontoTotalDeLasCompras.sql
     - 2.ObtenerHistorialDeLaTarjeta.sql

5. Desde Visual Studio IDE abre la solución de la solución del proyecto o abre el archivo TarjetaCreditoBackend.sln con Visual Studio IDE

6. Desglosa la solución en el explorador de archivos en Visual Studio IDE

7. De clic derecho sobre el proyecto **Aplicación** y selecciona la opción **Establecer como proyecto de Inicio**


8. Siga los siguientes pasos para configurar correctamente la condición a hacia la base de datos:
    - Desglose el proyecto **Aplicación** y abra el archivo **appsettings.json**
    - Sitúese en la propiedad con el nombre **DefaultConnection**
    - En el primer campo llamado **Server**, coloque el nombre que tiene tu equipo (PC, Máquina, Ordenador, Laptop, etc.)

9. ¡Una vez hecho esto ya está listo para iniciar la aplicación!

## Arquitectura de la aplicación

La estructura del proyecto se basa en la arquitectura hexagonal, no obstante se han hecho cambios para simplificar el desarrollo del proyecto, a continuación de explica la estructura del proyecto:

* &#x1f4c1; `TarjetaCreditoBackend/` Directorio raiz donde se encuentra solución de la demo
    * &#x1f4c1; `Aplicacion/` Aplicación ASP.NET en este directorio/proyecto se encuentra todo los relaciones con la configuración del proyecto de la API por el cúal se expone los endpoints para ser consumidos
        * &#x1f4c1; `Controllers/` En este directorio se encuentran los controladores que exponen los endpoints de la API
    * &#x1f4c1; `Dominio/` En este directorio/proyecto encontramos todas aquellas clases POCO (clases unicamente con propiedades) como son los Dtos y entidades utilizados en la solución
        * &#x1f4c1; `Constants/` En este directorio se encuentra los valores constantes y estaticos utilizado en toda la aplicación 
        * &#x1f4c1; `Dtos/` En este directorio se encuentra todos los dtos utilizados en toda la aplicación
        * &#x1f4c1; `Entities/` En este directorio se encuentra las clases de entidades de la base de datos 
        * &#x1f4c1; `Enums/` En este directorio se encuentra todos los enums utilizados en toda la aplicación
        * &#x1f4c1; `IRepository/` En este directorio se encuentra las interfaces de los repositorios de toda la aplicación
    * &#x1f4c1; `Infraestructura/` En este directorio/proyecto encontramos la lógica de toda la aplicación, esto quiere decir que las consultas a la base de datos, mappers y si hubiera la necesidad de consumir un servicio Rest o Soap se encontraría en este directorio
        * &#x1f4c1; `EntityTypeConfiguration/` En este directorio se encuentra la configuración de las entidades de la base de datos 
        * &#x1f4c1; `MapperActions/` En este directorio se encuentra los MappersAction que son usados para agregar lógica extra necesaria para los Mappers
        * &#x1f4c1; `Mappers/` En este directorio se encuentra los mappers utilizados en la aplicación utilizando AutoMapper
        * &#x1f4c1; `Repository/` En este directorio se encuentra los repositorios utitilizados en toda aplicación y contienen la lógica base de negocios
    * &#x1f4c1; `Scripts/` En este directorio se encuentra los archivos de scripts para SQL Server, en un proyecto de la vida real este directorio estaría en su propio repositorio git para esta demo se ha dejado junto al proyecto para simplificar las cosas
        * &#x1f4c1; `SCRIPTS_CREATE_AND_INSERT/` En este carpeta se encuentra los archivos para crear la base datos, las tablas y los inserts necesarios para probar la aplicación, los archivos están enumerados en orden de ejecución
        * &#x1f4c1; `SCRIPTS_PROCEDURE/` En este carpeta se encuentra los archivos para crear la Stored Procedures necesarios para probar la aplicación
        * &#x1f4c1; `ROLLBACK_SCRIPTS_CREATE_AND_INSERT/` En este carpeta se encuentra los archivos para dar rollback a los scripts ejecutados en la carpeta SCRIPTS_CREATE_AND_INSERT/
        * &#x1f4c1; `ROLLBACK_SCRIPTS_PROCEDURE/` En este carpeta se encuentra los archivos para dar rollback a los scripts ejecutados en la carpeta SCRIPTS_PROCEDURE/



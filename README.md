# NotiPetWebApi

## Sobre el Proyecto 🐾

NotiPet es un proyecto cuyo propósito es el de conectar a los dueños de mascotas con las veterinarias con los cuales contratan servicios para sus mascotas permitiéndoles a estos estar al tanto de los servicios que le están siendo realizados a sus mascotas para de esta forma estar al tanto de éstos en todo momento.

API Web disponible en [https://notipet-api.herokuapp.com/](https://notipet-api.herokuapp.com/).

## Tecnologías Usadas

- C# 10 / .NET 6
- ASP.NET Core 6
- Entity Framework Core 6

### Tooling

- xUnit
- Docker
- Heroku
- Github Actions

## Corriendo el Proyecto

### Prerrequisitos

Para correr el proyecto debes tener instalado:

- .NET 6.0.X + SDK
- Docker Desktop

### Instalación

1. Clonar el repo:

```
git clone https://github.com/NotiPets/NotiPetWebApi.git
```

2. Construir imagen de Docker:

```
docker build .
```

3. Crear contenedor a partir de la imagen de Docker y correrlo:

```
docker run --name notipetweb <id de la imagen obtenido en el paso anterior>
```

### Comandos Disponibles

En el directorio del proyecto se pueden correr los siguientes comandos:

#### `docker start <nombre del contenedor>`

Inicia el contenedor si no está corriendo. 
Sí creaste el contenedor a través del comando indicado en los pasos de instalación, el nombre del contenedor será `notipetweb`. De lo contrario, deberás buscar su nombre en Docker Desktop.

#### `dotnet test`

Ejecuta todas las pruebas dentro del proyecto de xUnit.

#### `dotnet-format --check`

Aplica el formato recomendado de .NET. Para poder ejecutarlo debes haber instalado la herramienta de dotnet-format globalmente con el comando `dotnet tool install -g dotnet-format`.

#### `dotnet-format --check --verbosity diagnostic`

Realiza un reporte para verificar que el código cumpla con el formato recomendado de .NET. Para poder ejecutarlo debes haber instalado la herramienta `dotnet-format`dotnet-format globalmente con el comando `dotnet tool install -g dotnet-format`.

## Estrategía de Branches

1. Crear un feature branch a partir del branch main (un branch debe tener un solo objetivo). Usar la siguiente convención:
   `feature/{nombre del feature}`
2. Hacer push de tus cambios directamente al feature branch, **NUNCA DIRECTAMENTE A `main`**.
3. Crear un Pull Request a `main`.
4. Esperar a que el Pipeline de Github Actions se ejecute exitosamente y a que otra persona más apruebe tus cambios antes de realizar un merge a la rama `main`.
5. Eliminar el feature branch una vez se haya realizado merge exitosamente.

## Licencia

Ver `LICENSE`

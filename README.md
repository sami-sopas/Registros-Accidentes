
# Registro de Accidentes

Aplicacion realizada en C# de tipo Windows Forms usando .NET para la materia de Base de Datos. 
  

## Características

- Operaciones CRUD para distintas tablas
- Vistas para usuarios y administradores
- Conexion a BD
- Descargar reportes en PDF y Excel
- Graficas para la visualizacion de los datos
- Registro / Login
- Interfaz moderna  

## Requistos para instalarlo

- .NET 4.5 o más
- PgAdmin / Postgres
- Visual Studio (el morado) y sus dependencias con C#


## ¿Como instalarlo?

- Clona el repositorio

```bash
  git clone https://github.com/sami-sopas/Registros-Accidentes.git
```

- Abre la carpeta del directorio del proyecto

```bash
  cd ProyectoGriselda2.0
```

- Abre el proyecto en VS usando la solucion del proyecto

```bash
  ProyectoGriselda2.0.sln
```

- Sigue los pasos del video para instalar [Bunifu](https://www.youtube.com/watch?v=1QZHT9by2xo) 


- Agrega el controlador para conectarse a [Postgres](https://www.youtube.com/watch?v=vqtsrF0DWec) 

- Crea una nueva base de datos en Postgres llamada **accidentes**

- Importa la base de datos usando el archivo de **[accidentes.sql](accidentes.sql)**, esto contiene las tablas, las relaciones y todo lo necesario

Y seria todo ! Tecnicamente es todo lo necesario para utilizar el programa, ya puedes correrlo utilizando el boton de **Run**  

## A tomar en cuenta

- Puede ser que el proyecto no se vea bien en tu pantalla debido a que  Visual Studio nunca adapto el zoom de mi pantalla al programa, entonces mientras yo lo veo bien, algunos pueden notar la aplicacion un poco "desalineada"

- Las credenciales por defecto asignadas a la conexion a postgres son: 
  - Server = localhost
  - User Id = postgres
  - Password = usuario
  - Database = accidentes
  En caso de que no funcionen estas para ti, tendras que modificarlas manualmente en el codigo

- El rendimiento puede verse afectado por la libreria de Bunifu y las consultas, ya que suelen ser algo complejas

- La mayoria de las entradas NO estan validadas, asi que ten cuidado al llenar los formularios

- Hay una columna llamada tipo_usuario en la tabla de usuarios, para poder acceder al panel de administrador, el tipo_usuario deberia ser igual a "admin"  


## Screenshots

* Login / Registro
  
![image](https://github.com/sami-sopas/Registros-Accidentes/assets/99571985/c8620291-ad01-416f-8e20-e52d7a1b4a3f)

![image](https://github.com/sami-sopas/Registros-Accidentes/assets/99571985/5149d919-73e9-480a-ab99-8a54bea4ad05)

* Ver Accidentes
  
![image](https://github.com/sami-sopas/Registros-Accidentes/assets/99571985/2d05085e-e1f6-4697-98bf-bee911319093)


* Mapa
  
![image](https://github.com/sami-sopas/Registros-Accidentes/assets/99571985/d70b71e3-485d-44f6-b1d7-adc024c47b57)


* Descargar reportes
  
![image](https://github.com/sami-sopas/Registros-Accidentes/assets/99571985/c44eab80-47ac-4955-af7b-9d0fd9d97977)

* Panel de Administrador
  
![image](https://github.com/sami-sopas/Registros-Accidentes/assets/99571985/9bf74174-2359-4992-9e0b-b5aa4434c466)

* Registrando Accidente
  
![image](https://github.com/sami-sopas/Registros-Accidentes/assets/99571985/1a8c423b-ff4d-4d91-a091-430863f448a0)

* Reporte

![image](https://github.com/sami-sopas/Registros-Accidentes/assets/99571985/64bbd03a-244a-4e98-afbe-6966f9bb9dbd)
  

Y hay más capturas que podria tomar, pero estas son algunas de las más importantes
  

## Licencia

[MIT](https://choosealicense.com/licenses/mit/)


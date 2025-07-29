# 📚 Gestor de Libros con EF Core y SQLite

Este es un proyecto de consola en C# que permite gestionar una colección de libros usando **Entity Framework Core** y **SQLite**. El objetivo principal es practicar el uso de EF Core para realizar operaciones CRUD (Crear, Leer, Actualizar y Eliminar), con una base de datos local.

---

## 🚀 Funcionalidades actuales

✔️ Agregar libros  
✔️ Listar todos los libros  
✔️ Buscar un libro por ID  
✔️ Editar un libro existente  
✔️ Eliminar un libro  
✔️ Validaciones y manejo de errores básicos  
✔️ Método reutilizable para buscar por ID  
✔️ Uso de `try/catch` para manejo seguro de excepciones  

---

## 💾 Estructura del proyecto

- `Libro.cs`: Modelo de datos con propiedades como Título, Autor, Género, Año, etc.  
- `AppDbContext.cs`: Contexto de EF Core con configuración para usar SQLite.  
- `GestorLibros.cs`: Lógica de negocio (CRUD y menús).  
- `Program.cs`: Punto de entrada con menú interactivo.  

---

## 🛠️ Tecnologías usadas

- C#  
- .NET 6 / 7  
- Entity Framework Core  
- SQLite  

---

## ▶️ Cómo ejecutar

1. Clona el repositorio:
   ```bash
   git clone https://github.com/tu-usuario/EFCoreSQLiteBooks.git
   ```

2. Restaura los paquetes y compila:
   ```bash
   dotnet restore
   dotnet build
   ```

3. Ejecuta el proyecto:
   ```bash
   dotnet run
   ```

> Al ejecutar por primera vez, la base de datos se creará automáticamente si no existe (`db.Database.Migrate();` en `Program.cs`).

---

## 📌 Próximas mejoras

- [ ] Búsqueda avanzada por texto (título, autor o género) usando LINQ  
- [ ] Exportar e importar libros en formato JSON  
- [ ] Validaciones más robustas (por ejemplo, título mínimo de 3 caracteres)  
- [ ] Mostrar estadísticas simples (libros por género, año más común, etc.)  
- [ ] Interfaz gráfica con WinForms o MAUI (opcional)  

---

## 🧠 Aprendizajes clave

- Modelado de datos con clases C#  
- Uso de EF Core para mapear objetos a base de datos  
- Migraciones con `dotnet ef`  
- Consultas con LINQ  
- Validación de entrada en consola  
- Principios de separación de responsabilidades  

---

## 📂 Base de datos

El archivo `GestorLibros.db` se genera automáticamente en la carpeta del proyecto al ejecutar las migraciones. Contiene los datos persistentes en formato SQLite.

---

## 👨‍💻 Autor

**[immanuel448](https://github.com/immanuel448)**  
Proyecto educativo para reforzar habilidades en EF Core.

---

## 📜 Licencia

MIT License. Puedes usar y modificar este proyecto libremente.

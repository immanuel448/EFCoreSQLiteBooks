using Microsoft.EntityFrameworkCore;

namespace EFCoreSQLiteBooks
{
    // Modelo que representa la tabla Libros en la base de datos
    public class Libro
    {
        public int Id { get; set; } // Clave primaria autoincrementable
        public string Titulo { get; set; } // Título del libro
        public string Autor { get; set; } // Autor del libro
        public int AnoPublicacion { get; set; } // Año de publicación
        public string Genero { get; set; } // Género literario
    }

    // Contexto de EF Core para manejar la base de datos
    public class AppDbContext : DbContext
    {
        // Representa la tabla Libros en la base de datos
        public DbSet<Libro> Libros { get; set; }

        // Configura la conexión a la base de datos SQLite
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Especifica el archivo donde se guardará la base de datos SQLite
            optionsBuilder.UseSqlite("Data Source=libros2.db");
        }
    }

    internal class Program
    {
        static void Main()
        {
            // Crear instancia del contexto para trabajar con la base de datos
            using var db = new AppDbContext();

            // Aplica migraciones pendientes para actualizar la estructura de la base
            db.Database.Migrate();

            // Crear un nuevo libro con datos iniciales
            var libro = new Libro
            {
                Titulo = "1984",
                Autor = "George Orwell",
                AnoPublicacion = 1949,
                Genero = "Distopía"
            };

            // Agregar el nuevo libro al contexto (a la tabla Libros)
            db.Libros.Add(libro);

            // Guardar los cambios en la base de datos (ejecutar INSERT)
            db.SaveChanges();

            // Obtener todos los libros almacenados en la base
            var libros = db.Libros.ToList();

            // Mostrar en consola los libros encontrados
            Console.WriteLine("Libros en la base de datos:");
            foreach (var l in libros)
            {
                Console.WriteLine($"{l.Id}: {l.Titulo} - {l.Autor} ({l.AnoPublicacion}) [{l.Genero}]");
            }

            // Buscar el primer libro para actualizarlo
            var libroActualizar = db.Libros.FirstOrDefault();
            if (libroActualizar != null)
            {
                // Cambiar el género del libro
                libroActualizar.Genero = "Ciencia Ficción";

                // Guardar la actualización en la base (ejecutar UPDATE)
                db.SaveChanges();
            }

            // Buscar el primer libro para eliminarlo
            var libroEliminar = db.Libros.FirstOrDefault();
            if (libroEliminar != null)
            {
                // Remover el libro del contexto (marcar para eliminar)
                db.Libros.Remove(libroEliminar);

                // Guardar los cambios en la base (ejecutar DELETE)
                db.SaveChanges();
            }

            // Esperar a que el usuario presione una tecla para cerrar la consola
            Console.ReadKey();
        }
    }
}

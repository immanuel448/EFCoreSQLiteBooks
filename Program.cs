using Microsoft.EntityFrameworkCore;

namespace EFCoreSQLiteBooks
{
    // Modelo que representa la tabla Libros en la base de datos
    public class Libro
    {
        public int Id { get; set; } // Clave primaria autoincrementable
        public string Titulo { get; set; } // Título del libro
        public string Autor { get; set; } // Autor del libro
        public int AnhoPublicacion { get; set; } // Año de publicación
        public string Genero { get; set; } // Género literario
    }

    // Contexto de EF Core para manejar la base de datos
    public class AppDbContext : DbContext
    {
        // Representa la tabla "Libros" en la base de datos.
        public DbSet<Libro> Libros { get; set; }

        // Configura la conexión a la base de datos SQLite
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Obtener la ruta base donde está el proyecto (sube 3 niveles desde bin\Debug\net9.0)
            string proyectoRaiz = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;

            // Carpeta "Data" dentro de la raíz del proyecto
            string carpeta = Path.Combine(proyectoRaiz, "Data");

            // Crear carpeta "Data" si no existe
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            // Ruta completa al archivo libros2.db dentro de Data en la raíz del proyecto
            string rutaBaseDatos = Path.Combine(carpeta, "libros2.db");

            // Configurar EF Core para usar SQLite en la ruta especificada
            optionsBuilder.UseSqlite($"Data Source={rutaBaseDatos}");
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
            //para saber donde esta el archivo, eeeeeeeee
            Console.WriteLine("📁 Ruta real de la base de datos:");
            Console.WriteLine(Path.GetFullPath("libros2.db"));

            // Crear un nuevo libro con datos iniciales     CREAR ------------------------------------
            var libro = new Libro
            {
                Titulo = "1984",
                Autor = "George Orwell",
                AnhoPublicacion = 1949,
                Genero = "Distopía"
            };

            //Crea una lista de la clase libro
            List<Libro> misLibros = new List<Libro>
            {
                new Libro{
                    Titulo = "Cien años de soledad",
                    Autor = "Gabriel García Márquez",
                    AnhoPublicacion = 1967,
                    Genero = "Realismo mágico"
                },
                new Libro{
                    Titulo = "Orgullo y prejuicio",
                    Autor = "Jane Austen",
                    AnhoPublicacion = 1813,
                    Genero = "Romance"
                },
                new Libro{
                    Titulo = "El gran Gatsby",
                    Autor = "F. Scott Fitzgerald",
                    AnhoPublicacion = 1925,
                    Genero = "Ficción"
                },
                new Libro{
                    Titulo = "El Principito",
                    Autor = "Antoine de Saint-Exupéry",
                    AnhoPublicacion = 1943,
                    Genero = "Fábula"
                }
            };

            // Agregar los nuevos libros al contexto (a la tabla Libros)
            if (!db.Libros.Any()) // Solo si no hay ningún libro
            {
                db.Libros.Add(libro);
                db.Libros.AddRange(misLibros);
                // Guardar los cambios en la base de datos (ejecutar INSERT)
                db.SaveChanges();
            }
        
            // Obtener todos los libros almacenados en la base LEER ------------------------------------
            var libros = db.Libros.ToList();

            // Mostrar en consola los libros encontrados
            Console.WriteLine("Libros en la base de datos:");
            foreach (var l in libros)
            {
                Console.WriteLine($"{l.Id}: {l.Titulo} - {l.Autor} ({l.AnhoPublicacion}) [{l.Genero}]");
            }

            // Buscar el primer libro para actualizarlo, ACTUALIZAR ------------------------------------
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

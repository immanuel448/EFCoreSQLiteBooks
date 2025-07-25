﻿using Microsoft.EntityFrameworkCore;
using System.Numerics;

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
            // Obtener la ruta base donde está el proyecto (sube 3 niveles desde bin\Debug\net9.0); la carpeta termina quedando donde está el .sln
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

            var gestor = new GestorLibros(db);

            MostrarMenu(gestor);

            // Esperar a que el usuario presione una tecla para cerrar la consola
            Console.ReadKey();
        }

        static void MostrarMenu(GestorLibros gestor)
        {
            while (true)
            {
                Console.WriteLine("\n--- MENÙ DE LIBROS ---");
                Console.WriteLine("1. Agregar libro");
                Console.WriteLine("2. Ver todos los libros");
                Console.WriteLine("3. Buscar libro por ID");
                Console.WriteLine("4. Editar libro");
                Console.WriteLine("5. Eliminar libro");
                Console.WriteLine("6. Salir");
                Console.Write("Selecciona una opción: ");

                string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1": gestor.AgregarLibro(); break;
                    case "2": gestor.MostrarLibros(); break;
                    case "3": gestor.BuscarLibroPorId(); break;
                    case "4": gestor.EditarLibro(); break;
                    case "5": gestor.EliminarLibro(); break;
                    case "6": return;
                    default: Console.WriteLine("Opción no válida."); break;
                }
            }
        }
    }
}

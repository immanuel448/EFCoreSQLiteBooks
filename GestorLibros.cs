using System;
using System.Linq;

namespace EFCoreSQLiteBooks
{
    internal class GestorLibros
    {
        private readonly AppDbContext _db;

        // Constructor que recibe el contexto
        public GestorLibros(AppDbContext db)
        {
            _db = db;
        }

        public void AgregarLibro()
        {
            Console.WriteLine("\n--- Agregar nuevo libro ---");

            Console.Write("Título: ");
            string titulo = Console.ReadLine();

            Console.Write("Autor: ");
            string autor = Console.ReadLine();

            Console.Write("Año de publicación: ");
            int anho;
            while (!int.TryParse(Console.ReadLine(), out anho))
            {
                Console.Write("Por favor, introduce un año válido: ");
            }

            Console.Write("Género: ");
            string genero = Console.ReadLine();

            var nuevoLibro = new Libro
            {
                Titulo = titulo,
                Autor = autor,
                AnhoPublicacion = anho,
                Genero = genero
            };

            _db.Libros.Add(nuevoLibro);
            _db.SaveChanges();

            Console.WriteLine("✅ Libro agregado correctamente.");
        }

        public void MostrarLibros()
        {
            // Aquí puedes implementar la lectura de todos los libros
        }

        public void BuscarLibroPorId()
        {
            // Aquí puedes implementar la búsqueda por ID
        }

        public void EditarLibro()
        {
            // Aquí puedes implementar la edición
        }

        public void EliminarLibro()
        {
            // Aquí puedes implementar la eliminación
        }
    }
}

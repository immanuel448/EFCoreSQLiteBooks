using System;
using System.Linq;

namespace EFCoreSQLiteBooks
{
    internal class GestorLibros
    {
        private readonly AppDbContext _db;

        public GestorLibros(AppDbContext db)
        {
            _db = db;
        }

        public void AgregarLibro()
        {
            Console.WriteLine("\n--- AGREGAR NUEVO LIBRO ---");

            string titulo = SolicitarCampoNoVacio("Título");
            string autor = SolicitarCampoNoVacio("Autor");
            string genero = SolicitarCampoNoVacio("Género");

            Console.Write("Año de publicación: ");
            int anho;
            int anhoActual = DateTime.Now.Year;
            while (!int.TryParse(Console.ReadLine(), out anho) || anho < 0 || anho > anhoActual)
            {
                Console.Write("Por favor, introduce un año válido: ");
            }

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

        private static string SolicitarCampoNoVacio(string campo)
        {
            string valor;
            do
            {
                Console.Write($"{campo}: ");
                valor = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(valor))
                    Console.WriteLine($"⚠️ El {campo.ToLower()} no puede estar vacío.");
            } while (string.IsNullOrWhiteSpace(valor));

            return valor;
        }

        public void MostrarLibros()
        {
            var libros = _db.Libros.ToList();

            Console.WriteLine("\n--- LIBROS EN LA BASE DE DATOS ---");
            foreach (var l in libros)
            {
                Console.WriteLine($"{l.Id}: {l.Titulo} - {l.Autor} ({l.AnhoPublicacion}) [{l.Genero}]");
            }
        }

        // Método reutilizable que busca un libro por ID y devuelve una tupla (bool encontrado, Libro libro)
        private (bool, Libro?) BuscarLibroPorId(string accion)
        {
            Console.Write($"Ingrese el ID del libro para {accion}: ");
            bool valido = int.TryParse(Console.ReadLine(), out int id) && id > 0;

            if (!valido)
            {
                Console.WriteLine("❌ Error: debe ingresar un número entero mayor que cero como ID.");
                return (false, null);
            }

            var libro = _db.Libros.FirstOrDefault(l => l.Id == id);
            if (libro == null)
            {
                Console.WriteLine($"❌ No se encontró ningún libro con el ID {id}.");
                return (false, null);
            }

            return (true, libro);
        }

        public void BuscarLibroPorId()
        {
            var (encontrado, libro) = BuscarLibroPorId("buscar");
            if (!encontrado || libro == null) return;

            Console.WriteLine($"📖 Libro encontrado:");
            Console.WriteLine($"{libro.Titulo} - {libro.Autor} ({libro.AnhoPublicacion}) [{libro.Genero}]");
        }

        public void EditarLibro()
        {
            var (encontrado, libro) = BuscarLibroPorId("editar");
            if (!encontrado || libro == null) return;

            Console.WriteLine($"📘 Libro elegido: \"{libro.Titulo}\" de {libro.Autor} ({libro.AnhoPublicacion}) - Género: {libro.Genero}");

            Console.Write("¿Está seguro que desea modificar este libro? (S/N): ");
            string confirmar = Console.ReadLine()?.Trim().ToUpper();
            if (confirmar != "S")
            {
                Console.WriteLine("✋ Modificación cancelada.");
                return;
            }

            libro.Titulo = SolicitarDatoActualizado("Título", libro.Titulo);
            libro.Autor = SolicitarDatoActualizado("Autor", libro.Autor);
            libro.Genero = SolicitarDatoActualizado("Género", libro.Genero);

            string nuevoAnho = SolicitarDatoActualizado("Año de publicación", libro.AnhoPublicacion.ToString());
            if (int.TryParse(nuevoAnho, out int anho) && anho > 0 && anho <= DateTime.Now.Year)
            {
                libro.AnhoPublicacion = anho;
            }

            _db.SaveChanges();
            Console.WriteLine($"✅ El libro con ID {libro.Id} ha sido modificado exitosamente.");
        }

        private static string SolicitarDatoActualizado(string campo, string valorActual = "")
        {
            if (!string.IsNullOrEmpty(valorActual))
                Console.WriteLine($"{campo} actual: {valorActual}");

            Console.Write($"Nuevo {campo}{(string.IsNullOrEmpty(valorActual) ? "" : " (deje vacío para conservar el dato actual)")}: ");
            string entrada = Console.ReadLine();
            return string.IsNullOrWhiteSpace(entrada) ? valorActual : entrada;
        }

        public void EliminarLibro()
        {
            var (encontrado, libro) = BuscarLibroPorId("eliminar");
            if (!encontrado || libro == null) return;

            Console.WriteLine($"📕 Libro encontrado: {libro.Titulo} - {libro.Autor} ({libro.AnhoPublicacion})");

            Console.Write("¿Está seguro que desea eliminar este libro? (S/N): ");
            string confirmar = Console.ReadLine()?.Trim().ToUpper();
            if (confirmar != "S")
            {
                Console.WriteLine("✋ Eliminación cancelada.");
                return;
            }

            _db.Libros.Remove(libro);
            _db.SaveChanges();

            Console.WriteLine($"🗑️ Libro con ID {libro.Id} eliminado correctamente.");
        }
    }
}

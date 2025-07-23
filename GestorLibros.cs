using System;
using System.Linq;

namespace EFCoreSQLiteBooks
{
    internal class GestorLibros
    {
        private readonly AppDbContext _db;

        // Constructor que recibe el contextxo
        public GestorLibros(AppDbContext db)
        {
            _db = db;
        }

        public void AgregarLibro()
        {
            //agregar un libro desde consola
            Console.WriteLine("\n--- AGREGAR NUEVO LIBRO ---");

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
            // Obtener todos los libros almacenados en la base LEER ------------------------------------
            var libros = _db.Libros.ToList();

            // Mostrar en consola los libros encontrados
            Console.WriteLine("Libros en la base de datos:");
            foreach (var l in libros)
            {
                Console.WriteLine($"{l.Id}: {l.Titulo} - {l.Autor} ({l.AnhoPublicacion}) [{l.Genero}]");
            }
        }

        public void BuscarLibroPorId()
        {
            // Mostrar mensaje para pedir al usuario que ingrese el ID del libro
            Console.WriteLine("Ingrese el ID del libro para buscar:");

            // Leer la entrada del usuario y tratar de convertirla a entero.
            // 'validarID' será true si la conversión es exitosa y el número es mayor que cero.
            bool validarID = int.TryParse(Console.ReadLine(), out int IDsolicitado) && IDsolicitado > 0;

            // Si el ID es válido (entero y mayor que cero), procedemos a buscar el libro
            if (validarID)
            {
                // Buscar el primer libro en la base de datos que tenga el ID igual al solicitado
                // Si no lo encuentra, 'libroElejido' será null
                var libroElejido = _db.Libros.FirstOrDefault(l => l.Id == IDsolicitado);

                // Comprobar si se encontró un libro con ese ID
                if (libroElejido != null)
                {
                    // Mostrar información del libro encontrado
                    Console.WriteLine($"El libro correspondiente al ID: {IDsolicitado} es:");
                    Console.WriteLine($"{libroElejido.Titulo} - {libroElejido.Autor} ({libroElejido.AnhoPublicacion}) [{libroElejido.Genero}]");
                }
                else
                {
                    // Mensaje cuando no existe un libro con el ID ingresado
                    Console.WriteLine($"El libro con el id: {IDsolicitado}, no se ha encontrado");
                }
            }
            else
            {
                // Mensaje de error si el ID no es un número entero válido mayor que cero
                Console.WriteLine("Error!!, Debe elegir un valor numérico entero mayor a cero como ID");
            }
        }

        public void EditarLibro()
        {
            Console.WriteLine("Ingrese el ID del libro para ser editado:");
            bool validarID = int.TryParse(Console.ReadLine(), out int IDsolicitado) && IDsolicitado > 0;

            if (!validarID)
            {
                Console.WriteLine("Error: debe ingresar un número entero mayor a cero como ID.");
                return;
            }

            // Buscar el libro por ID en la base de datos
            var libroElegido = _db.Libros.FirstOrDefault(l => l.Id == IDsolicitado);
            if (libroElegido == null)
            {
                Console.WriteLine($"No se encontró ningún libro con el ID {IDsolicitado}.");
                return;
            }

            // Mostrar información para confirmar edición
            Console.WriteLine($"Libro elejido: \"{libroElegido.Titulo}\" de {libroElegido.Autor} ({libroElegido.AnhoPublicacion}) - Género: {libroElegido.Genero}");
            Console.Write("¿Está seguro que desea modificar este libro? (S/N): ");
            string confirmar = Console.ReadLine()?.Trim().ToUpper();
            if (confirmar != "S")
            {
                Console.WriteLine("Modificación cancelada.");
                return;
            }


            // Nuevos datos para el libro
            Console.Write("Nuevo título (deje vacío para conservar el dato actual): ");
            string nuevoDato = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nuevoDato))
            {
                libroElegido.Titulo = nuevoDato;
            }
            Console.Write("Nuevo autor: (deje vació para concervar el dato actual): ");
            nuevoDato = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nuevoDato))
            {
                libroElegido.Autor = nuevoDato;
            }
            Console.Write("Nuevo año de publicación: (deje vació para concervar el dato actual): ");
            nuevoDato = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nuevoDato))
            {
                bool resultado = int.TryParse(nuevoDato, out int datoEntero);
                libroElegido.AnhoPublicacion = resultado ? datoEntero : libroElegido.AnhoPublicacion; // Si no es un número, se conserva el dato actual
            }
            Console.Write("Nuevo género: (deje vació para concervar el dato actual): ");
            nuevoDato = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nuevoDato))
            {
                libroElegido.Genero = nuevoDato;
            }

            // Guardar cambios en la base de datos (se ejecuta DELETE)
            _db.SaveChanges();

            Console.WriteLine($"El libro con ID {IDsolicitado} ha sido modificado exitosamente.");




        }

        public void EliminarLibro()
        {
            Console.WriteLine("Ingrese el ID del libro para ser eliminado:");
            bool validarID = int.TryParse(Console.ReadLine(), out int IDsolicitado) && IDsolicitado > 0;

            if (validarID)
            {
                // Buscar el libro por ID en la base de datos
                var libroElegido = _db.Libros.FirstOrDefault(l => l.Id == IDsolicitado);

                if (libroElegido != null)
                {
                    // Mostrar información para confirmar eliminación
                    Console.WriteLine($"Libro encontrado: {libroElegido.Titulo} - {libroElegido.Autor} ({libroElegido.AnhoPublicacion})");
                    Console.Write("¿Está seguro que desea eliminar este libro? (S/N): ");
                    string confirmar = Console.ReadLine()?.Trim().ToUpper();

                    if (confirmar == "S")
                    {
                        // Marcar el libro para eliminación en el contexto
                        _db.Libros.Remove(libroElegido);

                        // Guardar cambios en la base de datos (se ejecuta DELETE)
                        _db.SaveChanges();

                        Console.WriteLine($"El libro con ID {IDsolicitado} ha sido eliminado exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("Eliminación cancelada.");
                    }
                }
                else
                {
                    //la obntención del libro en base al id, rtesultò en null, por lo tanto no se encontró el libro
                    Console.WriteLine($"No se encontró ningún libro con el ID {IDsolicitado}.");
                }
            }
            else
            {
                Console.WriteLine("Error: debe ingresar un número entero mayor a cero como ID.");
            }
        }
    }
}

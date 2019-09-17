using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Agenda
{
    class Program
    {
        static int Nregistros = 1;
        public struct Agenda
        {
            public string NombreTemp;
            public string ApellidoTemp;
            public string CedulaTemp;
            public short EdadTemp;
            public string SexoTemp;
            public string TelefonoTemp;
            public string EmailTemp;

        }
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            string Ruta = "agenda.bin";
            char opcion;
            bool Bandera = true;
            while (Bandera)
            {
                Console.Clear();
                Console.Write("\tMENÚ DE AGENDA\n" +
                    "\n1) Agregar nuevo Registro" +
                    "\n2) Buscar Registro" +
                    "\n3) Eliminar Registro" +
                    "\n4) Mostrar Registro" +
                    "\n5) Eliminar Todos los Registros" +
                    "\n6) Actualizar, Modificar Registros" +
                    "\n7) Salir" +
                    "\nSeleccione una opción: ");
                opcion = Console.ReadKey().KeyChar;
                Console.Clear();
                if (opcion == '1')
                    AgregarRegistro(Ruta);
                else if (opcion == '2')
                    BuscarRegistro(Ruta);
                else if (opcion == '3')
                    EliminarRegistro(Ruta, TotalRegistros(Ruta));
                else if (opcion == '4')
                    MostrarRegistro(Ruta);
                else if (opcion == '5')
                    EliminarTodoReg(Ruta, TotalRegistros(Ruta));
                else if (opcion == '6')
                    ActualizarRegistro(Ruta, TotalRegistros(Ruta));
                else if (opcion == '7')
                {
                    Console.WriteLine("Adiós, presione enter: ");
                    Bandera = false;
                }
                else
                {
                    Console.WriteLine("Ingrese valores válidos");
                    System.Threading.Thread.Sleep(2000);
                }

                Console.ReadKey();
            }
        }
     
        static void AgregarRegistro(string ruta)
        {
            FileStream FlujoBinario = new FileStream(ruta, FileMode.Append, FileAccess.Write);
            BinaryWriter escribe = new BinaryWriter(FlujoBinario, Encoding.ASCII);
            string Nombres, Apellidos, Cedula, Email, Sexo, Telefono;
            short Edad;
            Console.WriteLine("\tINGRESE DE DATOS\n");
            Console.Write("Ingrese nombres: ");
            Nombres = Console.ReadLine();
            Console.Write("Ingrese apellidos: ");
            Apellidos = Console.ReadLine();
            Console.Write("Ingrese número de cédula: ");
            Cedula = Console.ReadLine();
            Console.Write("Ingrese la edad: ");
            Edad = Convert.ToInt16(Console.ReadLine());
            Console.Write("Ingrese sexo: ");
            Sexo = Console.ReadLine();
            Console.Write("Ingrese número telefónico: ");
            Telefono = Console.ReadLine();
            Console.Write("\n INGRESE EL EMAIL:");
            Email = Console.ReadLine();
            escribe.Write(Nregistros);
            escribe.Write(Nombres);
            escribe.Write(Apellidos);
            escribe.Write(Cedula);
            escribe.Write(Edad);
            escribe.Write(Sexo);
            escribe.Write(Telefono);
            escribe.Write(Email);
            escribe.Close();
            FlujoBinario.Close();
            Nregistros++;
            System.Threading.Thread.Sleep(2500);
        }

        static void BuscarRegistro(string ruta)
        {
            byte aux = 0;
            FileStream FlujoBinario = new FileStream(ruta, FileMode.Open, FileAccess.Read);
            BinaryReader lee = new BinaryReader(FlujoBinario, Encoding.ASCII);
            Console.WriteLine("\nBUSCAR RESGISTROS\n");
            Console.WriteLine("Ingrese el numero de cedula que desea buscar: ");
            string cedula = Console.ReadLine();
            while (lee.PeekChar() != -1)
            {
                if (lee.ReadString() == cedula)
                {
                    Console.WriteLine("Nombre: {0}", lee.ReadString());
                    Console.WriteLine("Apellido: {0}", lee.ReadString());
                    Console.WriteLine("Edad: {0}", lee.ReadInt16());
                    Console.WriteLine("Sexo: {0}", lee.ReadString());
                    Console.WriteLine("Telefono: {0}", lee.ReadString());
                    Console.WriteLine("Email: {0}", lee.ReadString());
                    aux = 1;
                    break;
                }
                else
                {
                    lee.ReadString();
                    lee.ReadString();
                    lee.ReadInt16();
                    lee.ReadString();
                    lee.ReadString();
                    lee.ReadString();
                }
            }
            if (aux == 0)
            {
                Console.WriteLine("Registro no encontrado");
            }
            lee.Close();
            FlujoBinario.Close();
            System.Threading.Thread.Sleep(2500);
        }

        static void EliminarRegistro(string ruta, int N)
        {
            Agenda[] Vector = new Agenda[N];
            FileStream FlujoBinario = new FileStream(ruta, FileMode.Open, FileAccess.Read);
            BinaryReader lee = new BinaryReader(FlujoBinario, Encoding.ASCII);
            int i = 0;
            while (lee.PeekChar() != -1)
            {
                Vector[i].NombreTemp = lee.ReadString();
                Vector[i].ApellidoTemp = lee.ReadString();
                Vector[i].CedulaTemp = lee.ReadString();
                Vector[i].EdadTemp = lee.ReadInt16();
                Vector[i].SexoTemp = lee.ReadString();
                Vector[i].TelefonoTemp = lee.ReadString();
                Vector[i].EmailTemp = lee.ReadString();
                i++;
            }
            lee.Close();
            FlujoBinario.Close();
            Console.Clear();
            FlujoBinario = new FileStream(ruta, FileMode.Open, FileAccess.Read);
            BinaryReader leer = new BinaryReader(FlujoBinario, Encoding.ASCII);
            Console.Clear();
            Console.WriteLine("\n\tRegistros");
            while (leer.PeekChar() != -1)
            {
                Console.WriteLine("Nombre: {0}", leer.ReadString());
                Console.WriteLine("Apellido: {0}", leer.ReadString());
                Console.WriteLine("Cedula: {0}", leer.ReadString());
                Console.WriteLine("Edad: {0}", leer.ReadInt16());
                Console.WriteLine("Sexo: {0}", leer.ReadString());
                Console.WriteLine("Telefono: {0}", leer.ReadString());
                Console.WriteLine("Email: {0}", leer.ReadString());
            }
            leer.Close();
            FlujoBinario.Close();
            Console.WriteLine("\n Eliminar Registro");
            Console.Write("Ingrese el numero de cedula del registro que desea eliminar: ");
            string cedula = Console.ReadLine();
            int BReg = 0;
            FlujoBinario = new FileStream(ruta, FileMode.Create, FileAccess.Write);
            BinaryWriter escribe = new BinaryWriter(FlujoBinario, Encoding.ASCII);
            for (i = 0; i < N; i++)
            {
                if (Vector[i].CedulaTemp != cedula)
                {
                    escribe.Write(Vector[i].NombreTemp);
                    escribe.Write(Vector[i].ApellidoTemp);
                    escribe.Write(Vector[i].CedulaTemp);
                    escribe.Write(Vector[i].EdadTemp);
                    escribe.Write(Vector[i].SexoTemp);
                    escribe.Write(Vector[i].TelefonoTemp);
                    escribe.Write(Vector[i].EmailTemp);
                }
                else
                {
                    BReg = 1;
                }
            }
            if (BReg == 0)
            {
                Console.WriteLine("\nRegistro no Encontrado");
            }
            escribe.Close();
            FlujoBinario.Close();
            Console.WriteLine("Datos Eliminados correctamente");
            System.Threading.Thread.Sleep(2500);
            FlujoBinario = new FileStream(ruta, FileMode.Open, FileAccess.Read);
            BinaryReader ler = new BinaryReader(FlujoBinario, Encoding.ASCII);
            Console.WriteLine("\n Datos Registros Actuales \n");
            while (ler.PeekChar() != -1)
            {
                Console.WriteLine("Nombre: {0}", lee.ReadString());
                Console.WriteLine("Apellido: {0}", lee.ReadString());
                Console.WriteLine("Cedula: {0}", lee.ReadString());
                Console.WriteLine("Edad: {0}", lee.ReadInt16());
                Console.WriteLine("Sexo: {0}", lee.ReadString());
                Console.WriteLine("Telefono: {0}", lee.ReadString());
                Console.WriteLine("Email: {0}", lee.ReadString());
            }
            ler.Close();
            FlujoBinario.Close();
            System.Threading.Thread.Sleep(2500);
        }

        static void MostrarRegistro(string ruta)
        {
            FileStream FlujoBinario = new FileStream(ruta, FileMode.Open, FileAccess.Read);
            BinaryReader lee = new BinaryReader(FlujoBinario, Encoding.ASCII);
            Console.Clear();
            Console.WriteLine("\tLISTA DE REGISTROS \n");
            while (lee.PeekChar() != -1)
            {
                Console.WriteLine("Numero ID: {0}", lee.ReadBytes(2));
                Console.WriteLine("Nombre: {0}", lee.ReadString());
                Console.WriteLine("Apellido: {0}", lee.ReadString());
                Console.WriteLine("Cedula: {0}", lee.ReadString());
                Console.WriteLine("Edad: {0}", lee.ReadInt16());
                Console.WriteLine("Sexo: {0}", lee.ReadString());
                Console.WriteLine("Telefono: {0}", lee.ReadString());
                Console.WriteLine("Email: {0}", lee.ReadString());
            }
            lee.Close();
            FlujoBinario.Close();
            System.Threading.Thread.Sleep(2500);
        }
        static void EliminarTodoReg(string ruta, int N)
        {
            Agenda[] Vector = new Agenda[N];
            int i = 0;
            char op;
            Console.Write("Está seguro que desea eliminar todos los registros S/N");
            op = Console.ReadKey().KeyChar;
            if (op == 'S' || op == 's')
            {
                FileStream FlujoBinario = new FileStream(ruta, FileMode.Create, FileAccess.Write);
                BinaryWriter escribe = new BinaryWriter(FlujoBinario, Encoding.ASCII);
                escribe.Write(Vector[i].NombreTemp);
                escribe.Write(Vector[i].ApellidoTemp);
                escribe.Write(Vector[i].CedulaTemp);
                escribe.Write(Vector[i].EdadTemp);
                escribe.Write(Vector[i].SexoTemp);
                escribe.Write(Vector[i].TelefonoTemp);
                escribe.Write(Vector[i].EmailTemp);
                Console.WriteLine("\nDatos eliminados");
                escribe.Close();
                FlujoBinario.Close();
            }
            else
            {
                Console.WriteLine("\nDatos NO eliminados");
            }
            System.Threading.Thread.Sleep(2500);

        }

        static void ActualizarRegistro(string ruta, int N)
        {
            Agenda[] Vector = new Agenda[N];
            FileStream FlujoBinario = new FileStream(ruta, FileMode.Open, FileAccess.Read);
            BinaryReader lee = new BinaryReader(FlujoBinario, Encoding.ASCII);
            int i = 0;
            while (lee.PeekChar() != -1)
            {
                Vector[i].NombreTemp = lee.ReadString();
                Vector[i].ApellidoTemp = lee.ReadString();
                Vector[i].CedulaTemp = lee.ReadString();
                Vector[i].EdadTemp = lee.ReadInt16();
                Vector[i].SexoTemp = lee.ReadString();
                Vector[i].TelefonoTemp = lee.ReadString();
                Vector[i].EmailTemp = lee.ReadString();
                i++;
            }

            lee.Close();
            FlujoBinario.Close();
            Console.Clear();
            Console.WriteLine("\tMODIFICAR REGISTRO");
            Console.Write("\nIngrese número de cedula para modificar los datos: ");
            string cedula = Console.ReadLine();
            for (i = 0; i < N; i++)
            {
                if (Vector[i].CedulaTemp == cedula)
                {
                    Console.WriteLine("DATOS ANTERIORES");
                    Console.WriteLine("Nombre: {0}", Vector[i].NombreTemp);
                    Console.WriteLine("Apellido: {0}", Vector[i].ApellidoTemp);
                    Console.WriteLine("Edad: {0}", Vector[i].EdadTemp);
                    Console.WriteLine("Sexo: {0}", Vector[i].SexoTemp);
                    Console.WriteLine("Telefono: {0}", Vector[i].TelefonoTemp);
                    Console.WriteLine("Email: {0}", Vector[i].EmailTemp);


                    Console.WriteLine("NUEVOS DATOS");
                    Console.WriteLine("Nombre: ");
                    Vector[i].NombreTemp = Console.ReadLine();
                    Console.WriteLine("Apellido: ");
                    Vector[i].ApellidoTemp = Console.ReadLine();
                    Console.WriteLine("Edad: ");
                    Vector[i].EdadTemp = Convert.ToInt16(Console.ReadLine());
                    Console.WriteLine("Sexo: ");
                    Vector[i].SexoTemp = Console.ReadLine();
                    Console.WriteLine("Telefono: ");
                    Vector[i].TelefonoTemp = Console.ReadLine();
                    Console.WriteLine("Email: ");
                    Vector[i].EmailTemp = Console.ReadLine();
                    break;
                }
            }
            if (i == N)
            {
                Console.WriteLine("Registro no encontrado");
            }
            else
            {
                FlujoBinario = new FileStream(ruta, FileMode.Create, FileAccess.Write);
                BinaryWriter escribe = new BinaryWriter(FlujoBinario, Encoding.ASCII);
                for (i = 0; i < N; i++)
                {
                    escribe.Write(Vector[i].NombreTemp);
                    escribe.Write(Vector[i].ApellidoTemp);
                    escribe.Write(Vector[i].CedulaTemp);
                    escribe.Write(Vector[i].EdadTemp);
                    escribe.Write(Vector[i].SexoTemp);
                    escribe.Write(Vector[i].TelefonoTemp);
                    escribe.Write(Vector[i].EmailTemp);
                }
                lee.Close();
                FlujoBinario.Close();
            }
            System.Threading.Thread.Sleep(2500);
        }

        static int TotalRegistros(string ruta)
        {
            int Cont = 0;
            FileStream FlujoBinario = new FileStream(ruta, FileMode.Open, FileAccess.Read);
            BinaryReader lee = new BinaryReader(FlujoBinario, Encoding.ASCII);
            while (lee.PeekChar() != -1)
            {
                lee.ReadString();
                lee.ReadString();
                lee.ReadString();
                lee.ReadInt16();
                lee.ReadString();
                lee.ReadInt32();
                lee.ReadString();
                Cont++;
            }
            lee.Close();
            FlujoBinario.Close();
            return Cont;
        }
    }
}

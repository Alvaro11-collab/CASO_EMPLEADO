using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASO_EMPLEADO
{
    internal class Program
    {
        static int maxemp = 50;
        static string[] nombres_empl = new string[maxemp];
        static string[] correo_emp = new string[maxemp];
        static int conteo_empleados = 0;
        static string rutaArchivo = "empleados_datos.txt";
        static public void Titulo()
        {
            Console.WriteLine("============================================");
            Console.WriteLine("           SISTEMA DE EMPLEADOS             ");
            Console.WriteLine("============================================");
        }
        static public void Regsitrar_empleado()
        {
            Console.WriteLine("===========REGISTRO DE EMPLEADO=============");
            if (conteo_empleados >= maxemp)
            {
                Console.WriteLine(" Capacidad maxima alcanzada ");
                return;
            }
            Console.WriteLine("Ingrese nombres y apellidos: ");
            string nombres = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(nombres))
            {
                Console.WriteLine("Error: El nombre no puede estar vacio....!!");
                return;
            }
            string nombres_Mayus = nombres.ToUpper();
            string[] partes = nombres_Mayus.Split(' ');
            string primer_nombre;
            if (partes.Length > 0)
                primer_nombre = partes[0];
            else
                primer_nombre = "Usuario";
            string correo = $"{primer_nombre.ToLower()}@upn.edu.pe";
            nombres_empl[conteo_empleados] = nombres_Mayus;
            correo_emp[conteo_empleados] = correo;
            conteo_empleados++;
        }
        static public void buscar_emp()
        {
            Console.WriteLine("=============BUSCAR EMPLEADO==========");
            Console.WriteLine("======================================");
            if (conteo_empleados == 0)
            {
                Console.WriteLine("No hay empleados resgistrados");
                return;
            }
            Console.WriteLine("Ingrese el nombre de empleado a buscar: ");
            string buscar_nombre = Console.ReadLine()?.Trim().ToUpper();
            if (string.IsNullOrEmpty(buscar_nombre))
                return;
            bool encontrado = false;
            for (int i = 0; i < conteo_empleados; i++)
            {
                if (nombres_empl[i].Contains(buscar_nombre))
                {
                    Console.WriteLine("Nombre: " + nombres_empl[i] + "Correo: " + correo_emp[i]);
                    encontrado = true;
                }
            }
            if (!encontrado)
            {
                Console.WriteLine("Empleado no encontrado");
            }
        }
        static public void ordenar_alfabeticamente()
        {
            if (conteo_empleados <= 1)
            {
                Console.WriteLine("No hay suficuientes empleados para ordenar");
                return;
            }
            for (int i = 0; i < conteo_empleados - 1; i++)
            {
                for (int j = 0; j < conteo_empleados - 1 - i; j++)
                {
                    if (string.Compare(nombres_empl[j], nombres_empl[j + 1]) > 0)
                    {
                        string temp_nombre = nombres_empl[j];
                        nombres_empl[j] = nombres_empl[j + 1];
                        nombres_empl[j + 1] = temp_nombre;

                        string temp_correo = correo_emp[j];
                        correo_emp[j] = correo_emp[j + 1];
                        correo_emp[j + 1] = temp_correo;
                    }

                }
            }
        }
        static public void mostrar()
        {
            Console.WriteLine("======LISTADO DE EMPLEADOS======");
            Console.WriteLine("================================");
            if (conteo_empleados == 0)
            {
                Console.WriteLine("No hay empleados registrados");
                return;
            }
            Console.WriteLine("Nombres y Apellidos\t\tCorreo corporativo");
            for (int i = 0; i < conteo_empleados; i++)
            {
                Console.WriteLine(nombres_empl[i] + "\t\t" + correo_emp[i]);
            }
        }
        static public void guardar_datos_archivo()
        {
            Console.WriteLine("==========GUARDAR DATOS EN ARCHIVO==========");
            Console.WriteLine("********************************************");
            if (conteo_empleados==0)
            {
                Console.WriteLine("No hay empleados resgistrados");
                return;
            }
            using (StreamWriter escribir = new StreamWriter(rutaArchivo))
            {
                for (int i = 0; i <conteo_empleados;i++)
                {
                    escribir.WriteLine(nombres_empl[i]+ ","+correo_emp[i]);
                }
            }
            Console.WriteLine($"{conteo_empleados} Empleados guardados");
        }
        static public void Lee_datos()
        {
            Console.WriteLine("=======LEER DATOS DEL ARCHIVO=========");
            Console.WriteLine("======================================");
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("Archivo no existente");
                return;
            }
            using(StreamReader lector=new StreamReader(rutaArchivo))
            {
                string linea;
                while((linea=lector.ReadLine()) !=null)
                {
                    string[] partes = linea.Split(',');
                    if (partes.Length == 2)
                    {
                        Console.WriteLine($"Nombres: {partes[0]} Correo: {partes[1]}");
                    }
                }
            }

        }
        static public void Cargar_datos()
        {
            Console.WriteLine("=============CARGAR DATOS DESDE ARCHIVO=========");
            Console.WriteLine("================================================");
            if (!File.Exists (rutaArchivo))
            {
                Console.WriteLine("Archivo no existe");
                return;
            }
            string[] linea= File.ReadAllLines(rutaArchivo);
            conteo_empleados = 0;
            for (int i = 0; i < linea.Length && conteo_empleados < maxemp; i++)
            {
                string[] partes = linea[i].Split(',');
                if (partes.Length == 2)
                {
                    nombres_empl[conteo_empleados]=partes[0];
                    correo_emp[conteo_empleados] = partes[1];
                    conteo_empleados++;
                }

            }

        }
        static void Main(string[] args)
        {
            
            int opc = 0;
            while (opc != 8)
            {
                Console.Clear();
                Titulo();
                Console.WriteLine("[1]Registrar empleado");
                Console.WriteLine("[2]Buscar empleado");
                Console.WriteLine("[3]Ordenar alfabeticamente");
                Console.WriteLine("[4]Mostrar empleados");
                Console.WriteLine("[5]Guardar datos en archivo");
                Console.WriteLine("[6]Leer datos del archivo");
                Console.WriteLine("[7]Cargar datos desde el archivo");
                Console.WriteLine("[8]Salir");
                Console.WriteLine("Ingrese una opcion: ");
                if (!int.TryParse(Console.ReadLine(), out opc))
                {
                    Console.WriteLine("Error: Ingrese una opcion numerica");
                    continue;
                }
                switch (opc)
                {
                    case 1:
                        Regsitrar_empleado(); break;
                    case 2:
                        buscar_emp(); break;
                    case 3:
                        ordenar_alfabeticamente(); break;
                    case 4:
                        mostrar(); break;
                    case 5:
                        guardar_datos_archivo(); break;
                    case 6:
                        Lee_datos(); break;
                    case 7:
                        Cargar_datos(); break;
                    case 8:
                        Console.WriteLine("Gracias por usar el sistema"); break;
                    default: Console.WriteLine("Opcion no valida"); break;
                }
                Console.ReadKey();
            }
        }
    
    }

}

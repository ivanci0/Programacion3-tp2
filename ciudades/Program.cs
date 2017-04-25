using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ciudades
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("bienvenido, ingrese el nombre de la ciudad y luego enter");
            string ciudad = Console.ReadLine();

            WebRequest req = WebRequest.Create($"https://maps.googleapis.com/maps/api/geocode/json?address={ciudad.ToLower()}");

            WebResponse respuesta = req.GetResponse();

            Stream stream = respuesta.GetResponseStream();

            StreamReader sr = new StreamReader(stream);

            JObject data = JObject.Parse(sr.ReadToEnd());

            // si el status de la busqueda da "OK" continua, sino imprime un mensaje
            if (data["status"].ToString() == "OK")
            {
                // verifica si el resultado tiene mas de un elemento y modifica el mensaje final (es mayor a 2 porque uno de los resultados es si salio bien la busqueda)
                string mensajeFinal = data["results"].Count() > 2 ? "Los paises son: " : "El pais es: ";
                Console.WriteLine(mensajeFinal);
                
                // itera siempre porque el resultado siempre es un array con 2 elementos minimo
                foreach (var dato in data["results"])
                {
                    // devuelve una lista de "address_components" que contengan en "types" la palabra country
                    List<JToken> paises = dato["address_components"].Where(x => x["types"].Children().Contains("country")).ToList();
                    
                    // imprime el nombre del pais 
                    Console.WriteLine(paises[0]["long_name"]);
                }
            }
            else
            {
                Console.WriteLine("Escribi bien!!!");
            }

            Console.ReadLine();
        }
    }
}

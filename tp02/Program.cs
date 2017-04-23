using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace tp02
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("bienvenido, ingrese el nombre de la ciudad y luego enter");
            string titulo = Console.ReadLine();

            WebRequest req = WebRequest.Create($"https://maps.googleapis.com/maps/api/geocode/json?address={titulo.ToLower()}");

            WebResponse respuesta = req.GetResponse();

            Stream stream = respuesta.GetResponseStream();

            StreamReader sr = new StreamReader(stream);

            JObject data = JObject.Parse(sr.ReadToEnd());

            string response = (string)data["Year"];

            Console.WriteLine($"El año es: {response}");

            Console.ReadLine();
        }
    }
}

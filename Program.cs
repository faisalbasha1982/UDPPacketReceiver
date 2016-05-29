using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication4
{

    public class Value
    {
        public string Name { get; set; }
        public string result { get; set; }
    }

    public class Zone
    {
        public string DisplayName { get; set; }
        public List<Value> Values { get; set; }
        public string ZoneName { get; set; }
    }

    public class RootObject
    {
        public string __type { get; set; }
        public string Timestamp { get; set; }
        public List<Zone> Zones { get; set; }
    }


    class Program
    {

        static void Main(string[] args)
        {

            Console.Write("Starting to send packets....");

            int PORT = 25232;

            byte[] data = new byte[2048];
            string input, stringData;
            // create a writer and open the file
            TextWriter tw = new StreamWriter("D:\\newfile.txt");
            //TextWriter tw2 = new StreamWriter("D:\\fofile.txt");

            UdpClient server = new UdpClient(PORT); 

            //IPAddress broadcast = IPAddress.Parse(IPAddress.Any);
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, PORT);

            data = server.Receive(ref sender);

            Console.WriteLine("Message received from {0}:", sender.ToString());
            stringData = Encoding.ASCII.GetString(data, 0, data.Length);
            Console.WriteLine(stringData);

            // write a line of text to the file
            tw.WriteLine(stringData);

            JObject o = JObject.Parse(stringData);

            Console.WriteLine("Name: " + o["__type"]);
            Console.WriteLine("TimeStamp: " + o["Timestamp"]);
            Console.WriteLine("Zones: " + o["Zones"]);

            //JObject o = JObject.Parse(o["Zones"]);
            //Console.WriteLine(o.ToString());

            while (true)
            {
                //input = Console.ReadLine();
                //if (input == "exit")
                    //break;

                data = server.Receive(ref sender);
                stringData = Encoding.ASCII.GetString(data, 0, data.Length);
                Console.WriteLine(stringData);
                tw.WriteLine(stringData);

            }

            Console.WriteLine("Stopping client");
            // close the stream
            tw.Close();
            //tw2.Close();

            server.Close();
        }
    }
}

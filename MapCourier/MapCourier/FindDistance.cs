using System;
using System.IO;
using System.Net;

namespace MapCourier
{
    class FindDistance
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var url = "https://catalog.api.2gis.com/carrouting/6.0.0/global?key=rurbbn3446";
            var x1 = "82.93057";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";

            httpRequest.ContentType = "application/json";

            var data = @"{
   ""points"": [
       {
           ""type"": ""pedo"",
           ""x"": " + x1 + @",
           ""y"": 54.943207
       },
       {
           ""type"": ""pedo"",
           ""x"": 82.945039,
           ""y"": 55.033879
       }
   ]
}";

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                using (FileStream fstream = new FileStream("C:/Users/hvore/OneDrive/Desktop/check.txt", FileMode.OpenOrCreate))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(result);
                    // асинхронная запись массива байтов в файл
                    await fstream.WriteAsync(array, 0, array.Length);
                    Console.WriteLine("Текст записан в файл");
                }
            }

            Console.WriteLine(httpResponse.StatusCode);



        }
    }
}

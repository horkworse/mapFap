using System;
using System.IO;
using System.Net;

namespace MapCourier
{
    public class Mark
    {
        public readonly string X;
        public readonly string Y;
        public Mark NearStorage;
        public int NearStorageDist;
        public readonly char Status = 'n'; // 'b' - busy, 'f' - free, 'n' - not needed
        public Mark(string x, string y)
        {
            X = x;
            Y = y;
        }
    }
    class MarksNDistance
    {
        public readonly Mark Mark1;
        public readonly Mark Mark2;
        public readonly int Distance;
        public MarksNDistance(Mark mark1, Mark mark2)//При объявлении конструкор сам считает дистанцию по точкам. По моему, это будет удобнее чем создавать отдельный метод.
        {
            Mark1 = mark1;
            Mark2 = mark2;
            Distance = Convert.ToInt32(FindDistance.GetDistance(mark1.X, mark1.Y, mark2.X, mark2.Y));
        }
    }
    //Думаю классы понятны и просты, если что-то не так, сами меняйте я не хочу 
    class FindDistance
    {
        public static string GetDistance(string x1, string y1, string x2, string y2)//Возвращает дистанцию по координатам 2-х точек.
        {
            var url = "https://catalog.api.2gis.com/carrouting/6.0.0/global?key=rurbbn3446";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            string result;
            httpRequest.ContentType = "application/json";

            var data = @"{
   ""points"": [
       {
           ""type"": ""pedo"",
           ""x"": " + x1 + @",
           ""y"": " + y1 + @"
       },
       {
           ""type"": ""pedo"",
           ""x"": " + x2 + @",
           ""y"": " + y2 + @"
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
                result = streamReader.ReadToEnd();
            }

            var parce = @"""total_distance"":";
            result = result.Remove(0, result.IndexOf(parce) + parce.Length);
            result = result.Remove(result.IndexOf(','));
            return result;
        }
    }
}

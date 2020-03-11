using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using OnlineArticleCreator.Models;

namespace OnlineArticleCreator.Utils
{
    public static class ArticleReader
    {
        public static string ReadContentsFromDisk(string filePath)
        {
            var articleOnDisk = ReadArticleFromDisk(filePath);
            return articleOnDisk.Contents;
        }

        public static Article ReadArticleFromDisk(string filePath)
        {
            string json = GetJSONText(filePath);
            Article articleRead = JsonConvert.DeserializeObject<Article>(json);

            return articleRead;
        }

        private static string GetJSONText(string filePath)
        {
            string json = "";

            using (FileStream fileStream = File.OpenRead(filePath))
            using (StreamReader reader = new StreamReader(fileStream))
            {
                json = reader.ReadToEnd();
            }

            return json;
        }
    }
}

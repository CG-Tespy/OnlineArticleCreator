using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineArticleCreator.Models;
using Newtonsoft.Json;
using System.IO;
using CGT.AspNetMvc;

namespace OnlineArticleCreator.Utils
{
    /// <summary>
    /// Handles writing Article objects to files on disk.
    /// </summary>
    public class ArticleWriter
    {
        /// <summary>
        /// Writes an article to disk based on the passed writeArgs, and returns the
        /// written data. Throws an ArgumentException if any of the args are invalid.
        /// </summary>
        /// <returns>Data written to disk.</returns>
        public static string WriteToDisk(ArticleWriteArgs writeArgs)
        {
            ValidateArgs(writeArgs);
            CreateDirectoryIfNeededFor(writeArgs);
            SetUpTheFileFor(writeArgs);
            string writtenData = WriteDataToFileFor(writeArgs);

            return writtenData;
        }

        static void ValidateArgs(ArticleWriteArgs writeArgs)
        {
            if (InvalidPath(writeArgs.FolderPath))
                ThrowInvalidPathError(writeArgs.FolderPath);
            if (InvalidArticle(writeArgs.Article))
                ThrowInvalidArticleError(writeArgs.Article);
        }

        static bool InvalidPath(string path)
        {
            return string.IsNullOrEmpty(path);
        }

        static void ThrowInvalidPathError(string path)
        {
            string messageFormat = "Path {0} is not a valid path to write to.";
            string errorMessage = string.Format(messageFormat, path);
            throw new ArgumentException(errorMessage);
        }

        static bool InvalidArticle(Article article)
        {
            return article == null || !ValidationUtils.ModelIsValid(article);
        }

        static void ThrowInvalidArticleError(Article article)
        {
            string errorMessage = "";
            if (article == null)
                errorMessage = "Cannot write null Article to disk.";
            else if (!ValidationUtils.ModelIsValid(article))
                errorMessage = "Article invalid. Cannot write to disk. Article: " + article;

            throw new ArgumentException(errorMessage);
        }

        static void CreateDirectoryIfNeededFor(ArticleWriteArgs writeArgs)
        {
            if (!Directory.Exists(writeArgs.FolderPath))
                Directory.CreateDirectory(writeArgs.FolderPath);
        }

        /// <summary>
        /// Makes sure the file for the write args exists. If it already does, its contents
        /// are erased.
        /// </summary>
        static void SetUpTheFileFor(ArticleWriteArgs writeArgs)
        {
            using (FileStream fileStream = File.OpenWrite(writeArgs.FullFilePath)) { }
        }

        /// <summary>
        /// Writes the article's contents to the appropriate file, and returns what was written.
        /// </summary>
        /// <param name="writeArgs"></param>
        /// <returns></returns>
        static string WriteDataToFileFor(ArticleWriteArgs writeArgs)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Formatting = Formatting.Indented;

            string jsonData = JsonConvert.SerializeObject(writeArgs.Article, Formatting.Indented);
            using (StreamWriter streamWriter = new StreamWriter(writeArgs.FullFilePath))
            using (JsonWriter writer = new JsonTextWriter(streamWriter))
            {
                serializer.Serialize(writer, writeArgs.Article);
            }

            return jsonData;
        }
    }

}

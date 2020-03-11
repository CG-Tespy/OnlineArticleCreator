using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CGT.File;
using OnlineArticleCreator.Models;

namespace OnlineArticleCreator.Utils
{
    /// <summary>
    /// FileWriteArgs for Article objects.
    /// </summary>
    public class ArticleWriteArgs : FileWriteArgs
    {
        public Article Article { get; set; }

        public ArticleWriteArgs(Article toWrite = null,
            string fileName = "", string folderPath = "", string fileExtension = "txt") :
            base(fileName, folderPath, fileExtension)
        {
            this.Article = toWrite;
        }
    }
}

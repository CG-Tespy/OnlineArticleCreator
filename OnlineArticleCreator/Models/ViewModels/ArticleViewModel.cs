using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArticleCreator.Models.ViewModels
{
    public class ArticleViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Contents { get; set; }

        public Article CreateModel()
        {
            return new Article
            {
                Name = Name,
                Contents = Contents
            };
        }
    }
}

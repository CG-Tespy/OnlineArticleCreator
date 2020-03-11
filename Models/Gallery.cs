using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArticleCreator.Models
{
    public class Gallery
    {
        [Key]
        public int AccountID { get; set; }

        public Account Account { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}

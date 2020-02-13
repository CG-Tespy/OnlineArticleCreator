using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArticleCreator.Models
{
    public class Article
    {
        public int ID { get; set; }

        [NotMapped]
        public string Name { get; set; }

        [NotMapped]
        public string Contents { get; set; }

        public string Author
        {
            get
            {
                if (Account != null)
                {
                    return Account.UserName;
                }
                else
                    return "";
            }
        }
        public string FilePath { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime LastEdited { get; set; }

        // To link to other entities
        public int AccountID { get; set; }

        [NotMapped]
        public Account Account { get; set; }

        [NotMapped]
        public Gallery Gallery
        {
            get 
            {
                if (Account == null)
                    return null;
                return Account.Gallery;
            }
        }
    }
}

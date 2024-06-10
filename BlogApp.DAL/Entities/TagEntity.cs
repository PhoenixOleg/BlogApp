using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Models
{
    public class TagEntity
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;

        //Связь со статьями (многие-ко-многим)
        public List<ArticleEntity> Articles { get; set; } = [];
    }
}

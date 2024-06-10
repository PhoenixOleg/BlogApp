using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Interfaces
{
    public interface IArticle
    {
        public Guid Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        public DateTime PostDate { get; set; }
        public DateTime ModifyDate { get; set; } 

    }
}

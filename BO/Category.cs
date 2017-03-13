using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Category
    {
        public int Id { get; set; }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }


        public Category()
        {
        }

        public Category(string title)
        {
            Title = title;
        }


        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Category))
            {
                return Id == ((Category)obj).Id
                       && Title == ((Category)obj).Title;
            }
            else
            {
                return false;
            }
        }
    }
}

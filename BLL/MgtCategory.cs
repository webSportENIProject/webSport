using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MgtCategory
    {
        public List<Category> GetCategory()
        {
            // TODO : Aller chercher ces informations en BDD via la DAL
            var result = new List<Category>
            {
                new Category { Id = 1, Title = "Course à pied" },
                new Category { Id = 2, Title = "Gym" },
                new Category { Id = 3, Title = "Fitness" },
                new Category { Id = 4, Title = "Cyclisme" }
            };

            return result;
        }

        // CREATE

        // UPDATE

        // DELETE
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class Mail
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Email { get; set; }

        public string Titre { get; set; }

        public string Message { get; set; }
        
    }
}

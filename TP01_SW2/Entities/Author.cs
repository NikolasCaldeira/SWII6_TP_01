using System;
using System.Collections.Generic;
using System.Text;

namespace TP01_SW2.Negocio
{
    internal class Author
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public char Gender { get; set; }

        public Author() { }

        public Author(string name, string email, char gender)
        {
            Name = name;
            Email = email;
            Gender = gender;
        }
    }
}

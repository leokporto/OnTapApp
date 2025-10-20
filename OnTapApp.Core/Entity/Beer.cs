using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTapApp.Core.Entity
{
    public class Beer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Style { get; set; }

        public string Brewery { get; set; }

        public string FullName { get; set; }

        public float ABV { get; set; }

        public int MinIBU { get; set; }

        public int MaxIBU { get; set; }

    }
}

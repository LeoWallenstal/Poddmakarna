using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Category : BaseEntity
    {
        public string Text { get; set; }

        //Tom konstruktor för MongoDb
        public Category() :base() { }

        public override string ToString()
        {
            return Text;
        }
    }
}

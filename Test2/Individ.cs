using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Test2
{
    public class Individ
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }

        public double[] coefs;

        public Individ()
        {
        }

        public double Fitness_func()
        {
            double fitness = coefs[0] * x + coefs[1] * y + coefs[2] * z;
            return fitness;
        }
    }
}

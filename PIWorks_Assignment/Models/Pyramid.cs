using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIWorks_Assignment.Models
{
    public class Pyramid
    {
        public int depthOfValue;
        public int value;
        public bool isPrimeNumber;

        public Pyramid(int depth, int val, bool isPrime)
        {
            depthOfValue = depth;
            value = val;
            isPrimeNumber = isPrime;
        }
    }
}

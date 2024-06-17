using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPart
{
    class GetAllAlgs
    {
        public static IAlg[] Get2PartAlgs() => new IAlg[] {
            new Alg1(),
            new Alg2(),
            new Alg3(),
            new Alg4(),
            new Alg5(),
            new Alg6(),
            new Alg7(),
            new Alg8(),
            new Alg9(),
            new Alg10() };

        public static IAlg[] GetkPartAlgs() => new IAlg[] {
            new KPartBruteForce(),
            new KPartHeuristic() };
    }
}

using System;
using System.Collections.Generic;

namespace GeneticAlgorithm
{
    public class Population<TType,TSpecimen> where TType:ICloneable where TSpecimen:Specimen<TType>
    {


        public void Evolve()
        {
            throw new NotImplementedException();
        }

    }
}

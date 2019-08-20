using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace Core.Specflow
{
    [Binding]
    public class GenericNumberTransformer
    {
        [StepArgumentTransformation(@"(no|one)")]
        public int GetStringNumbers(string number)
        {
           if(number.Equals("no"))
            {
                return 0;
            }
            else if (number.Equals("one"))
            {
                return 1;
            } else
            {
                return 100;
            }
        }
    }
}

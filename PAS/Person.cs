using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS
{
    public class Person
    {
        public string SurName { get; private set; }
        public string GivenName { get; private set; }
        public double Height { get; private set; }
        public string Gender { get; private set; }
        public string EyeColor { get; private set; }

        public Person(string surName, string givenName, double height, string gender, string eyeColor)
        {
            SurName = surName;
            GivenName = givenName;
            Height = height;
            Gender = gender;
            EyeColor = eyeColor;
        }
    }
}

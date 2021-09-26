using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Framework;

namespace Marketplace.Domain
{
   public class ClassifiedAdText : Value<ClassifiedAdText>
   {
       public static ClassifiedAdText FromString(string title) => new ClassifiedAdText(title);

       private readonly string _value;
       public ClassifiedAdText(string value)
       {
           _value = value;
       }
   }
}

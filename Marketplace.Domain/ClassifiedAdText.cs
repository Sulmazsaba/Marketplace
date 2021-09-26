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

       public string Value { get; }
       internal ClassifiedAdText(string text) => Value = text;

       public static implicit operator string(ClassifiedAdText text) => text.Value;
   }
}

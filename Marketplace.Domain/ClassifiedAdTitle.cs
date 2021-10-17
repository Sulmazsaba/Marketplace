using Marketplace.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain
{
    public class ClassifiedAdTitle : Value<ClassifiedAdTitle>
    {
        public static ClassifiedAdTitle FromString(string title)
        {
            CheckValidity(title);
            return new ClassifiedAdTitle(title);
        }

        public string Value { get; }
        internal ClassifiedAdTitle(string title) =>
            Value = title;


        public static implicit operator string(ClassifiedAdTitle title) => title.Value;

        private static void CheckValidity(string value)
        {
            if (value.Length > 100)
                throw new ArgumentOutOfRangeException(nameof(value), "Title cannot be longer than 100 Characters");

        }

        public ClassifiedAdTitle() { }
    }
}

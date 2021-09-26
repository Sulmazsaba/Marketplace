using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Marketplace.Framework;

namespace Marketplace.Framework
{
    public abstract class Value<T> : IEquatable<T> where T : Value<T>
    {
        private List<PropertyInfo> Properties { get; set; }

        protected Value()
        {
            Properties = new List<PropertyInfo>();

        }

        public bool Equals(T other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            foreach (var property in Properties)
            {
                var oneValue = property.GetValue(this, null);
                var otherValue = property.GetValue(other, null);

                if (null == oneValue || null == otherValue) return false;
                if (false == oneValue.Equals(otherValue)) return false;
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Value<T>)obj);
        }

        public override int GetHashCode()
        {
            var hashCode = 36;
            foreach (var property in Properties)
            {
                var propertyValue = property.GetValue(this, null);
                if (null == propertyValue)
                    continue;

                hashCode = hashCode ^ propertyValue.GetHashCode();
            }

            return hashCode;
        }

        public override String ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var property in Properties)
            {
                var propertyValue = property.GetValue(this, null);
                if (null == propertyValue)
                    continue;

                stringBuilder.Append(propertyValue.ToString());
            }

            return stringBuilder.ToString();
        }




        public static bool operator ==(Value<T> left, Value<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Value<T> left, Value<T> right)
        {
            return !Equals(left, right);
        }
    }
}
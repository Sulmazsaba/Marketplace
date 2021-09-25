namespace Marketplace.Framework
{
    public class Value<T>
    {
        public bool Equals(T other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Amount.Equals(other.Amount);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((T)obj);
        }

        public override int GetHashCode() => Amount.GetHashCode();

        public static bool operator ==(T left, T right) =>
            Equals(left, right);

        public static bool operator !=(T left, T right) =>
           !Equals(left, right);
    }
}
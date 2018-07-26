namespace TK.CustomMap
{
    public struct Distance
    {
        private const double _metersPerMile = 1609.344;
        private const double _metersPerKilometer = 1000.0;

        public Distance(double meters)
        {
            Meters = meters;
        }

        public double Meters { get; }

        public double Miles => Meters / _metersPerMile;

        public double Kilometers => Meters / _metersPerKilometer;

        public static Distance FromMiles(double miles)
        {
            if (miles < 0)
            {
                miles = 0;
            }
            return new Distance(miles * _metersPerMile);
        }

        public static Distance FromMeters(double meters)
        {
            if (meters < 0)
            {
                meters = 0;
            }
            return new Distance(meters);
        }

        public static Distance FromKilometers(double kilometers)
        {
            if (kilometers < 0)
            {
                kilometers = 0;
            }
            return new Distance(kilometers * _metersPerKilometer);
        }

        public bool Equals(Distance other)
        {
            return Meters.Equals(other.Meters);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            return obj is Distance distance && Equals(distance);
        }

        public override int GetHashCode()
        {
            return Meters.GetHashCode();
        }

        public static bool operator ==(Distance left, Distance right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Distance left, Distance right)
        {
            return !left.Equals(right);
        }
    }
}

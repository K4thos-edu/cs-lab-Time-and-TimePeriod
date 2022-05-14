using System;

namespace TimeLibrary
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        // Properties
        public readonly byte Hours { get; }
        public readonly byte Minutes { get; }
        public readonly byte Seconds { get; }

        // Constructs
        public Time(byte hours = 0, byte minutes = 0, byte seconds = 0) : this()
        {
            Hours = rangeValidate(hours, 0, 23);
            Minutes = rangeValidate(minutes, 0, 59);
            Seconds = rangeValidate(seconds, 0, 59);
        }
        public byte rangeValidate(byte value, byte min, byte max)
        {
            if (value < min || value > max)
            {
                throw new ArgumentOutOfRangeException($"Time argument {value} outside {min}:{max} range");
            }
            return value;
        }

        public Time(string str) : this()
        {
            var arr = str.Split(':');
            if (arr.Length != 3)
            {
                throw new ArgumentException($"Time argument {str} not using h:mm:ss format");
            }

            for (int i = 0; i < arr.Length; i++)
            {
                byte value;
                try
                {
                    value = byte.Parse(arr[i]);
                }
                catch (FormatException)
                {
                    throw new ArgumentException($"Time argument {str} not using h:mm:ss format");
                }

                switch (i)
                {
                    case 0:
                        Hours = rangeValidate(value, 0, 23);
                        break;
                    case 1:
                        Minutes = rangeValidate(value, 0, 59);
                        break;
                    default:
                        Seconds = rangeValidate(value, 0, 59);
                        break;
                }
            }
        }

        public Time(long timeLength) : this()
        {
            Hours = (byte)(timeLength / 3600);
            Minutes = (byte)((timeLength / 60) % 60);
            Seconds = (byte)(timeLength % 60);
        }

        // ToString overloading (hh:mm:ss)
        public override string ToString()
        {
            return $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";
        }

        // IEquatable<Time>
        public override int GetHashCode()
        {
            return (Hours, Minutes, Seconds).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Time)
            {
                return Equals((Time)obj);
            }
            return false;
        }
        public bool Equals(Time obj)
        {
            return (Hours == obj.Hours && Minutes == obj.Minutes && Seconds == obj.Seconds);
        }

        // IComparable<Time>, operator overloading
        public static bool operator ==(Time obj1, Time obj2) => obj1.Equals(obj2);
        public static bool operator !=(Time obj1, Time obj2) => !obj1.Equals(obj2);

        public int CompareTo(Time obj)
        {
            var output = Hours.CompareTo(obj.Hours);
            if (output != 0)
            {
                return output;
            }
            output = Minutes.CompareTo(obj.Minutes);
            if (output != 0)
            {
                return output;
            }
            output = Seconds.CompareTo(obj.Seconds);
            return output;
        }
        public static bool operator >(Time obj1, Time obj2) => obj1.CompareTo(obj2) > 0;
        public static bool operator <(Time obj1, Time obj2) => obj1.CompareTo(obj2) < 0;
        public static bool operator >=(Time obj1, Time obj2) => obj1.CompareTo(obj2) >= 0;
        public static bool operator <=(Time obj1, Time obj2) => obj1.CompareTo(obj2) <= 0;
    }
}

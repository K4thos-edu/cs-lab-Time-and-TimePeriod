using System;

namespace TimeLibrary
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        // Properties
        public readonly byte Hours { get; }
        public readonly byte Minutes { get; }
        public readonly byte Seconds { get; }
        public readonly short Milliseconds { get; }

        // Constructs
        public Time(byte hours = 0, byte minutes = 0, byte seconds = 0, short milliseconds = 0) : this()
        {
            Hours = (byte)rangeValidate(hours, 0, 23);
            Minutes = (byte)rangeValidate(minutes, 0, 59);
            Seconds = (byte)rangeValidate(seconds, 0, 59);
            Milliseconds = rangeValidate(milliseconds, 0, 999);
        }
        public short rangeValidate(short value, short min, short max)
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
            if (arr.Length < 3 || arr.Length > 4)
            {
                throw new ArgumentException($"Time argument {str} not using h:mm:ss or h:mm:ss:fff format");
            }

            for (int i = 0; i < arr.Length; i++)
            {
                short value;
                try
                {
                    value = short.Parse(arr[i]);
                }
                catch (FormatException)
                {
                    throw new ArgumentException($"Time argument {str} not using h:mm:ss or h:mm:ss:fff format");
                }

                switch (i)
                {
                    case 0:
                        Hours = (byte)rangeValidate(value, 0, 23);
                        break;
                    case 1:
                        Minutes = (byte)rangeValidate(value, 0, 59);
                        break;
                    case 2:
                        Seconds = (byte)rangeValidate(value, 0, 59);
                        break;
                    default:
                        Milliseconds = rangeValidate(value, 0, 999);
                        break;
                }
            }
        }

        public Time(long timeLength) : this()
        {
            Hours = (byte)((timeLength / 3600000) % 24);
            Minutes = (byte)(((timeLength / 60000)) % 60);
            Seconds = (byte)((timeLength / 1000) % 60);
            Milliseconds = (short)(timeLength % 1000);
        }

        // ToString overloading (hh:mm:ss)
        public override string ToString()
        {
            return $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}"; // {Milliseconds:D3}
        }

        // IEquatable<Time>
        public override int GetHashCode()
        {
            return (Hours, Minutes, Seconds, Milliseconds).GetHashCode();
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
            return (Hours == obj.Hours && Minutes == obj.Minutes && Seconds == obj.Seconds && Milliseconds == obj.Milliseconds);
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
            if (output != 0)
            {
                return output;
            }
            output = Milliseconds.CompareTo(obj.Milliseconds);
            return output;
        }
        public static bool operator >(Time obj1, Time obj2) => obj1.CompareTo(obj2) > 0;
        public static bool operator <(Time obj1, Time obj2) => obj1.CompareTo(obj2) < 0;
        public static bool operator >=(Time obj1, Time obj2) => obj1.CompareTo(obj2) >= 0;
        public static bool operator <=(Time obj1, Time obj2) => obj1.CompareTo(obj2) <= 0;

        // Arithmetic calculations
        public static Time operator +(Time t, TimePeriod tp) => t.Plus(tp);
        public Time Plus(TimePeriod tp) => new Time(3600000 * Hours + 60000 * Minutes + 1000 * Seconds + Milliseconds + tp.TimeLength);
        public static Time Plus(Time t, TimePeriod tp) => new Time(3600000 * t.Hours + 60000 * t.Minutes + 1000 * t.Seconds + t.Milliseconds + tp.TimeLength);

        public static Time operator -(Time t, TimePeriod tp) => t.Minus(tp);
        public Time Minus(TimePeriod tp) => new Time(3600000 * Hours + 60000 * Minutes + 1000 * Seconds + Milliseconds - tp.TimeLength);
        public static Time Minus(Time t, TimePeriod tp) => new Time(3600000 * t.Hours + 60000 * t.Minutes + 1000 * t.Seconds + t.Milliseconds - tp.TimeLength);
    }
}

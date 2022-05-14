using System;

namespace TimeLibrary
{
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        // Properties
        public readonly long TimeLength { get; }

        // Constructs
        public TimePeriod(byte hours = 0, byte minutes = 0, byte seconds = 0) : this()
        {
            TimeLength = 3600 * rangeValidate(hours, 0, byte.MaxValue) + 60 * rangeValidate(minutes, 0, 59) + rangeValidate(seconds, 0, 59);
        }
        public byte rangeValidate(byte value, byte min, byte max)
        {
            if (value < min || value > max)
            {
                throw new ArgumentOutOfRangeException($"TimePeriod argument {value} outside {min}:{max} range");
            }
            return value;
        }

        public TimePeriod(Time obj1, Time obj2) : this()
        {
            TimeLength = getTimeLength(obj1) - getTimeLength(obj2);
        }
        public long getTimeLength(Time obj)
        {
            return 3600 * obj.Hours + 60 * obj.Minutes + obj.Seconds;
        }

        public TimePeriod(string str) : this()
        {
            var arr = str.Split(':');
            if (arr.Length != 3)
            {
                throw new ArgumentException($"TimePeriod argument {str} not using h:mm:ss format");
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
                    throw new ArgumentException($"TimePeriod argument {str} not using h:mm:ss format");
                }

                switch (i)
                {
                    case 0:
                        TimeLength += 3600 * rangeValidate(value, 0, byte.MaxValue);
                        break;
                    case 1:
                        TimeLength += 60 * rangeValidate(value, 0, 59);
                        break;
                    default:
                        TimeLength += rangeValidate(value, 0, 59);
                        break;
                }
            }
        }

        public TimePeriod(long timeLength = 0) : this()
        {
            var hours = (timeLength / 3600);
            var minutes = (timeLength / 60) % 60;
            var seconds = timeLength % 60;
            TimeLength = 3600 * rangeValidate((byte)hours, 0, byte.MaxValue) + 60 * rangeValidate((byte)minutes, 0, 59) + rangeValidate((byte)seconds, 0, 59);
        }

        // ToString overloading (hh:mm:ss)
        public override string ToString()
        {
            var hours = (TimeLength / 3600);
            var minutes = (TimeLength / 60) % 60;
            var seconds = TimeLength % 60;
            return $"{hours}:{minutes:D2}:{seconds:D2}";
        }

        // IEquatable<Time>
        public override int GetHashCode()
        {
            return (TimeLength).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is TimePeriod)
            {
                return Equals((TimePeriod)obj);
            }
            return false;
        }
        public bool Equals(TimePeriod obj)
        {
            return (TimeLength == obj.TimeLength);
        }

        // IComparable<Time>, operator overloading
        public static bool operator ==(TimePeriod obj1, TimePeriod obj2) => obj1.Equals(obj2);
        public static bool operator !=(TimePeriod obj1, TimePeriod obj2) => !obj1.Equals(obj2);

        public int CompareTo(TimePeriod obj)
        {
            return TimeLength.CompareTo(obj.TimeLength);
        }
        public static bool operator >(TimePeriod obj1, TimePeriod obj2) => obj1.CompareTo(obj2) > 0;
        public static bool operator <(TimePeriod obj1, TimePeriod obj2) => obj1.CompareTo(obj2) < 0;
        public static bool operator >=(TimePeriod obj1, TimePeriod obj2) => obj1.CompareTo(obj2) >= 0;
        public static bool operator <=(TimePeriod obj1, TimePeriod obj2) => obj1.CompareTo(obj2) <= 0;

        // Arithmetic calculations
        public static TimePeriod operator +(TimePeriod tp1, TimePeriod tp2) => tp1.Plus(tp2);
        public TimePeriod Plus(TimePeriod tp2) => new TimePeriod(TimeLength + tp2.TimeLength);
        public static TimePeriod Plus(TimePeriod tp1, TimePeriod tp2) => new TimePeriod(tp1.TimeLength + tp2.TimeLength);

        public static TimePeriod operator -(TimePeriod tp1, TimePeriod tp2) => tp1.Minus(tp2);
        public TimePeriod Minus(TimePeriod tp2) => new TimePeriod(TimeLength - tp2.TimeLength);
        public static TimePeriod Minus(TimePeriod tp1, TimePeriod tp2) => new TimePeriod(tp1.TimeLength - tp2.TimeLength);
    }
}

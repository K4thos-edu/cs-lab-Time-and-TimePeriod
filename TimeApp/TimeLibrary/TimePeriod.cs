using System;

namespace TimeLibrary
{
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        // Properties
        public readonly long TimeLength { get; }

        // Constructs

        /// <summary>
        /// TimePeriod construct out of individual time units
        /// </summary>
        /// <param name="hours">Hours time unit (0-255)</param>
        /// <param name="minutes">Minutes time unit (0-59)</param>
        /// <param name="seconds">Seconds time unit (0-59)</param>
        /// <param name="milliseconds">Milliseconds time unit (0-999)</param>
        public TimePeriod(byte hours = 0, byte minutes = 0, byte seconds = 0, short milliseconds = 0) : this()
        {
            TimeLength = 3600000 * rangeValidate(hours, 0, byte.MaxValue) + 60000 * rangeValidate(minutes, 0, 59) + 1000 * rangeValidate(seconds, 0, 59) + rangeValidate(milliseconds, 0, 999);
        }
        public long rangeValidate(long value, long min, long max)
        {
            if (value < min || value > max)
            {
                throw new ArgumentOutOfRangeException($"TimePeriod argument {value} outside {min}:{max} range");
            }
            return value;
        }

        /// <summary>
        /// TimePeriod construct out of difference between 2 TimePeriods
        /// </summary>
        /// <param name="obj1">TimePeriod 1</param>
        /// <param name="obj2">TimePeriod 2</param>
        public TimePeriod(Time obj1, Time obj2) : this()
        {
            TimeLength = getTimeLength(obj1) - getTimeLength(obj2);
        }
        public long getTimeLength(Time obj)
        {
            return 3600000 * obj.Hours + 60000 * obj.Minutes + 1000 * obj.Seconds + obj.Milliseconds;
        }

        /// <summary>
        /// TimePeriod construct out of time string
        /// </summary>
        /// <param name="str">String in h:mm:ss or h:mm:ss:fff format</param>
        /// <exception cref="ArgumentException"></exception>
        public TimePeriod(string str) : this()
        {
            var arr = str.Split(':');
            if (arr.Length < 3 || arr.Length > 4)
            {
                throw new ArgumentException($"TimePeriod argument {str} not using h:mm:ss or h:mm:ss:fff format");
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
                    throw new ArgumentException($"TimePeriod argument {str} not using h:mm:ss or h:mm:ss:fff format");
                }

                switch (i)
                {
                    case 0:
                        TimeLength += 3600000 * rangeValidate(value, 0, byte.MaxValue);
                        break;
                    case 1:
                        TimeLength += 60000 * rangeValidate(value, 0, 59);
                        break;
                    case 2:
                        TimeLength += 1000 * rangeValidate(value, 0, 59);
                        break;
                    default:
                        TimeLength += rangeValidate(value, 0, 999);
                        break;
                }
            }
        }

        /// <summary>
        /// Time construct out of time length
        /// </summary>
        /// <param name="timeLength">Length of time in milliseconds</param>
        public TimePeriod(long timeLength = 0) : this()
        {
            var hours = (timeLength / 3600000);
            var minutes = (timeLength / 60000) % 60;
            var seconds = (timeLength / 1000) % 60;
            var milliseconds = (timeLength % 1000);

            TimeLength = 3600000 * rangeValidate(hours, 0, byte.MaxValue) + 60000 * rangeValidate(minutes, 0, 59) + 1000 * rangeValidate(seconds, 0, 59) + rangeValidate(milliseconds, 0, 999);
        }

        // ToString overloading (hh:mm:ss:fff)
        public override string ToString()
        {
            var hours = (TimeLength / 3600000);
            var minutes = (TimeLength / 60000) % 60;
            var seconds = (TimeLength / 1000) % 60;
            var milliseconds = (TimeLength % 1000);
            return $"{hours}:{minutes:D2}:{seconds:D2}:{milliseconds:D3}";
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

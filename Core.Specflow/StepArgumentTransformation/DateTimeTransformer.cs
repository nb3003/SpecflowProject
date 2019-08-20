using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace Core.Specflow.StepArgumentTransformation
{
    [Binding]
    public class DateTimeTransformer
    {
        private const string DurationPattern = @"((?:\s?[+-]?\d+[w|s|m|h|d|M|y])*)";
        private const string DateStringPattern = @"(now|today|this month|this week|this year)";
        private const string OffsetStringPattern = @"([+-]?\d+):(\d+)";
        private DateTimeOffset? _now = null;
        private DateTimeOffset CurrentDateTimeOffset => (_now ?? DateTimeOffset.Now).ToUniversalTime();
        public void SetCurrentTime(DateTimeOffset now)
        {
            _now = now;
        }

        [StepArgumentTransformation(@"(?:" + DurationPattern + " from )*" + DateStringPattern + " with offset " + OffsetStringPattern + "")]
        public DateTimeOffset DateTransformerWithOffset(string durationString, string dateString, int offsetHr, int offsetMin)
        {
            TimeSpan timeSpanOffset = new TimeSpan(offsetHr, offsetMin, 0);
            return DateTransformerWithAllArgs(CurrentDateTimeOffset.ToOffset(timeSpanOffset), durationString, dateString);
        }

        private DateTimeOffset DateTransformerWithAllArgs(DateTimeOffset date, string durationString, string dateString)
        {
            DateTimeOffset newDate;
            // creates date with specific offset value
            switch (dateString)
            {
                case "today":
                    newDate = new DateTimeOffset(date.Date, date.Offset);
                    break;
                case "this month":
                    newDate = new DateTimeOffset(date.Year, date.Month, 1, 0, 0, 0, date.Offset);
                    break;
                case "this week":
                    DateTime weekDate = date.Subtract(TimeSpan.FromDays((int)date.DayOfWeek - 1)).Date;
                    newDate = new DateTimeOffset(weekDate, date.Offset);
                    break;
                case "this year":
                    newDate = new DateTimeOffset(date.Year, 1, 1, 0, 0, 0, date.Offset);
                    break;
                case "now":
                    newDate = date;
                    break;

                default: throw new ArgumentOutOfRangeException($"Invalid Date string Specified : {dateString}");
            }

            //set the date variations
            foreach (string value in durationString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                Match match = Regex.Match(value, "([+-]?\\d+)([w|s|m|h|d|M|y])");
                int duration = Convert.ToInt16(match.Groups[1].Value);
                char placeholder = Convert.ToChar(match.Groups[2].Value);

                switch (placeholder)
                {
                    case 's': newDate = newDate.AddSeconds(duration); break;
                    case 'm': newDate = newDate.AddMinutes(duration); break;
                    case 'h': newDate = newDate.AddHours(duration); break;
                    case 'd': newDate = newDate.AddDays(duration); break;
                    case 'M': newDate = newDate.AddMonths(duration); break;
                    case 'w': newDate = newDate.AddDays(duration * 7); break;
                    case 'y': newDate = newDate.AddYears(duration); break;

                    default: throw new ArgumentOutOfRangeException($"Invalid placeholder Specified : {placeholder}");
                }
            }

            return newDate;

        }
    }
}

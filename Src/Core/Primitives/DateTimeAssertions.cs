﻿using System;
using System.Diagnostics;
using FluentAssertions.Execution;

namespace FluentAssertions.Primitives
{
    /// <summary>
    /// Contains a number of methods to assert that a <see cref="DateTime"/> is in the expected state.
    /// </summary>
    /// <remarks>
    /// You can use the <see cref="FluentDateTimeExtensions"/> for a more fluent way of specifying a <see cref="DateTime"/>.
    /// </remarks>
    [DebuggerNonUserCode]
    public class DateTimeAssertions
    {
        public DateTimeAssertions(DateTime? value)
        {
            Subject = value;
        }

        /// <summary>
        /// Gets the object which value is being asserted.
        /// </summary>
        public DateTime? Subject { get; private set; }

        /// <summary>
        /// Asserts that the current <see cref="DateTime"/> is exactly equal to the <paramref name="expected"/> value.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<DateTimeAssertions> Be(DateTime expected, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(Subject.HasValue && (Subject.Value == expected))
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:date and time} to be {0}{reason}, but found {1}.",
                    expected, Subject.HasValue ? Subject.Value : default(DateTime?));

            return new AndConstraint<DateTimeAssertions>(this);
        }

        /// <summary>
        /// Asserts that the current <see cref="DateTime"/> or <see cref="DateTime"/> is not equal to the <paramref name="unexpected"/> value.
        /// </summary>
        /// <param name="unexpected">The unexpected value</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<DateTimeAssertions> NotBe(DateTime unexpected, string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(!Subject.HasValue || (Subject.Value != unexpected))
                .BecauseOf(because, becauseArgs)
                .FailWith("Did not expect {context:date and time} to be {0}{reason}.", unexpected);

            return new AndConstraint<DateTimeAssertions>(this);
        }

        /// <summary>
        /// Asserts that the current <see cref="DateTime"/>  is within the specified number of milliseconds (default = 20 ms)
        /// from the specified <paramref name="nearbyTime"/> value.
        /// </summary>
        /// <remarks>
        /// Use this assertion when, for example the database truncates datetimes to nearest 20ms. If you want to assert to the exact datetime,
        /// use <see cref="Be(DateTime, string, object[])"/>.
        /// </remarks>
        /// <param name="nearbyTime">
        /// The expected time to compare the actual value with.
        /// </param>
        /// <param name="precision">
        /// The maximum amount of milliseconds which the two values may differ.
        /// </param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<DateTimeAssertions> BeCloseTo(DateTime nearbyTime, int precision = 20, string because = "",
            params object[] becauseArgs)
        {
            long distanceToMinInMs = (long) (nearbyTime - DateTime.MinValue).TotalMilliseconds;
            DateTime minimumValue = nearbyTime.AddMilliseconds(-Math.Min(precision, distanceToMinInMs));

            long distanceToMaxInMs = (long) (DateTime.MaxValue - nearbyTime).TotalMilliseconds;
            DateTime maximumValue = nearbyTime.AddMilliseconds(Math.Min(precision, distanceToMaxInMs));

            Execute.Assertion
                .ForCondition(Subject.HasValue && (Subject.Value >= minimumValue) && (Subject.Value <= maximumValue))
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:date and time} to be within {0} ms from {1}{reason}, but found {2}.",
                    precision,
                    nearbyTime, Subject.HasValue ? Subject.Value : default(DateTime?));

            return new AndConstraint<DateTimeAssertions>(this);
        }

        /// <summary>
        /// Asserts that the current <see cref="DateTime"/>  is before the specified value.
        /// </summary>
        /// <param name="expected">The <see cref="DateTime"/>  that the current value is expected to be before.</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<DateTimeAssertions> BeBefore(DateTime expected, string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(Subject.HasValue && Subject.Value.CompareTo(expected) < 0)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected a {context:date and time} before {0}{reason}, but found {1}.", expected,
                    Subject.HasValue ? Subject.Value : default(DateTime?));

            return new AndConstraint<DateTimeAssertions>(this);
        }

        /// <summary>
        /// Asserts that the current <see cref="DateTime"/>  is either on, or before the specified value.
        /// </summary>
        /// <param name="expected">The <see cref="DateTime"/>  that the current value is expected to be on or before.</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<DateTimeAssertions> BeOnOrBefore(DateTime expected, string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(Subject.HasValue && Subject.Value.CompareTo(expected) <= 0)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected a {context:date and time} on or before {0}{reason}, but found {1}.", expected,
                    Subject.HasValue ? Subject.Value : default(DateTime?));

            return new AndConstraint<DateTimeAssertions>(this);
        }

        /// <summary>
        /// Asserts that the current <see cref="DateTime"/>  is after the specified value.
        /// </summary>
        /// <param name="expected">The <see cref="DateTime"/>  that the current value is expected to be after.</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<DateTimeAssertions> BeAfter(DateTime expected, string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(Subject.HasValue && Subject.Value.CompareTo(expected) > 0)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected a {context:date and time} after {0}{reason}, but found {1}.", expected,
                    Subject.HasValue ? Subject.Value : default(DateTime?));

            return new AndConstraint<DateTimeAssertions>(this);
        }

        /// <summary>
        /// Asserts that the current <see cref="DateTime"/>  is either on, or after the specified value.
        /// </summary>
        /// <param name="expected">The <see cref="DateTime"/>  that the current value is expected to be on or after.</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<DateTimeAssertions> BeOnOrAfter(DateTime expected, string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .ForCondition(Subject.HasValue && Subject.Value.CompareTo(expected) >= 0)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected a {context:date and time} on or after {0}{reason}, but found {1}.", expected,
                    Subject.HasValue ? Subject.Value : default(DateTime?));

            return new AndConstraint<DateTimeAssertions>(this);
        }

        /// <summary>
        /// Asserts that the current <see cref="DateTime"/>  has the <paramref name="expected"/> year.
        /// </summary>
        /// <param name="expected">The expected year of the current value.</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<DateTimeAssertions> HaveYear(int expected, string because = "", params object[] becauseArgs)
        {
            bool success = Execute.Assertion
                .ForCondition(Subject.HasValue)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected year {0}{reason}, but found a <null> DateTime.", expected);

            if (success)
            {
                Execute.Assertion
                    .ForCondition(Subject.Value.Year == expected)
                    .BecauseOf(because, becauseArgs)
                    .FailWith("Expected year {0}{reason}, but found {1}.", expected,
                        Subject.Value.Year);
            }

            return new AndConstraint<DateTimeAssertions>(this);
        }

        /// <summary>
        /// Asserts that the current <see cref="DateTime"/>  has the <paramref name="expected"/> month.
        /// </summary>
        /// <param name="expected">The expected month of the current value.</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<DateTimeAssertions> HaveMonth(int expected, string because = "", params object[] becauseArgs)
        {
            bool success = Execute.Assertion
                .ForCondition(Subject.HasValue)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected month {0}{reason}, but found a <null> DateTime.", expected);

            if (success)
            {
                Execute.Assertion
                    .ForCondition(Subject.Value.Month == expected)
                    .BecauseOf(because, becauseArgs)
                    .FailWith("Expected month {0}{reason}, but found {1}.", expected, Subject.Value.Month);
            }
            return new AndConstraint<DateTimeAssertions>(this);
        }

        /// <summary>
        /// Asserts that the current <see cref="DateTime"/>  has the <paramref name="expected"/> day.
        /// </summary>
        /// <param name="expected">The expected day of the current value.</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<DateTimeAssertions> HaveDay(int expected, string because = "", params object[] becauseArgs)
        {
            bool success = Execute.Assertion
                .ForCondition(Subject.HasValue)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected day {0}{reason}, but found a <null> DateTime.", expected);

            if (success)
            {
                Execute.Assertion
                    .ForCondition(Subject.Value.Day == expected)
                    .BecauseOf(because, becauseArgs)
                    .FailWith("Expected day {0}{reason}, but found {1}.", expected, Subject.Value.Day);
            }

            return new AndConstraint<DateTimeAssertions>(this);
        }

        /// <summary>
        /// Asserts that the current <see cref="DateTime"/>  has the <paramref name="expected"/> hour.
        /// </summary>
        /// <param name="expected">The expected hour of the current value.</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<DateTimeAssertions> HaveHour(int expected, string because = "", params object[] becauseArgs)
        {
            bool success = Execute.Assertion
                .ForCondition(Subject.HasValue)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected hour {0}{reason}, but found a <null> DateTime.", expected);

            if (success)
            {
                Execute.Assertion
                    .ForCondition(Subject.Value.Hour == expected)
                    .BecauseOf(because, becauseArgs)
                    .FailWith("Expected hour {0}{reason}, but found {1}.", expected, Subject.Value.Hour);
            }

            return new AndConstraint<DateTimeAssertions>(this);
        }

        /// <summary>
        /// Asserts that the current <see cref="DateTime"/>  has the <paramref name="expected"/> minute.
        /// </summary>
        /// <param name="expected">The expected minutes of the current value.</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<DateTimeAssertions> HaveMinute(int expected, string because = "",
            params object[] becauseArgs)
        {
            bool success = Execute.Assertion
                .ForCondition(Subject.HasValue)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected minute {0}{reason}, but found a <null> DateTime.", expected);

            if (success)
            {
                Execute.Assertion
                    .ForCondition(Subject.Value.Minute == expected)
                    .BecauseOf(because, becauseArgs)
                    .FailWith("Expected minute {0}{reason}, but found {1}.", expected, Subject.Value.Minute);
            }

            return new AndConstraint<DateTimeAssertions>(this);
        }

        /// <summary>
        /// Asserts that the current <see cref="DateTime"/>  has the <paramref name="expected"/> second.
        /// </summary>
        /// <param name="expected">The expected seconds of the current value.</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion 
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<DateTimeAssertions> HaveSecond(int expected, string because = "",
            params object[] becauseArgs)
        {
            bool success = Execute.Assertion
                .ForCondition(Subject.HasValue)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected second {0}{reason}, but found a <null> DateTime.", expected);

            if (success)
            {
                Execute.Assertion
                    .ForCondition(Subject.Value.Second == expected)
                    .BecauseOf(because, becauseArgs)
                    .FailWith("Expected second {0}{reason}, but found {1}.", expected, Subject.Value.Second);
            }

            return new AndConstraint<DateTimeAssertions>(this);
        }

        /// <summary>
        /// Returns a <see cref="DateTimeRangeAssertions"/> object that can be used to assert that the current <see cref="DateTime"/> 
        /// exceeds the specified <paramref name="timeSpan"/> compared to another <see cref="DateTime"/> .
        /// </summary>
        /// <param name="timeSpan">
        /// The amount of time that the current <see cref="DateTime"/>  should exceed compared to another <see cref="DateTime"/> .
        /// </param>
        public DateTimeRangeAssertions BeMoreThan(TimeSpan timeSpan)
        {
            return new DateTimeRangeAssertions(this, Subject, TimeSpanCondition.MoreThan, timeSpan);
        }

        /// <summary>
        /// Returns a <see cref="DateTimeRangeAssertions"/> object that can be used to assert that the current <see cref="DateTime"/> 
        /// is equal to or exceeds the specified <paramref name="timeSpan"/> compared to another <see cref="DateTime"/> .
        /// </summary>
        /// <param name="timeSpan">
        /// The amount of time that the current <see cref="DateTime"/>  should be equal or exceed compared to
        /// another <see cref="DateTime"/>.
        /// </param>
        public DateTimeRangeAssertions BeAtLeast(TimeSpan timeSpan)
        {
            return new DateTimeRangeAssertions(this, Subject, TimeSpanCondition.AtLeast, timeSpan);
        }

        /// <summary>
        /// Returns a <see cref="DateTimeRangeAssertions"/> object that can be used to assert that the current <see cref="DateTime"/> 
        /// differs exactly the specified <paramref name="timeSpan"/> compared to another <see cref="DateTime"/> .
        /// </summary>
        /// <param name="timeSpan">
        /// The amount of time that the current <see cref="DateTime"/>  should differ exactly compared to another <see cref="DateTime"/> .
        /// </param>
        public DateTimeRangeAssertions BeExactly(TimeSpan timeSpan)
        {
            return new DateTimeRangeAssertions(this, Subject, TimeSpanCondition.Exactly, timeSpan);
        }

        /// <summary>
        /// Returns a <see cref="DateTimeRangeAssertions"/> object that can be used to assert that the current <see cref="DateTime"/> 
        /// is within the specified <paramref name="timeSpan"/> compared to another <see cref="DateTime"/> .
        /// </summary>
        /// <param name="timeSpan">
        /// The amount of time that the current <see cref="DateTime"/>  should be within another <see cref="DateTime"/> .
        /// </param>
        public DateTimeRangeAssertions BeWithin(TimeSpan timeSpan)
        {
            return new DateTimeRangeAssertions(this, Subject, TimeSpanCondition.Within, timeSpan);
        }

        /// <summary>
        /// Returns a <see cref="DateTimeRangeAssertions"/> object that can be used to assert that the current <see cref="DateTime"/>   
        /// differs at maximum the specified <paramref name="timeSpan"/> compared to another <see cref="DateTime"/> .
        /// </summary>
        /// <param name="timeSpan">
        /// The maximum amount of time that the current <see cref="DateTime"/>  should differ compared to another <see cref="DateTime"/> .
        /// </param>
        public DateTimeRangeAssertions BeLessThan(TimeSpan timeSpan)
        {
            return new DateTimeRangeAssertions(this, Subject, TimeSpanCondition.LessThan, timeSpan);
        }

        /// <summary>
        /// Asserts that the current <see cref="DateTime"/> has the <paramref name="expected"/> date.
        /// </summary>
        /// <param name="expected">The expected date portion of the current value.</param>
        /// <param name="because">
        /// A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        /// is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="becauseArgs">
        /// Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<DateTimeAssertions> BeSameDateAs(DateTime expected, string because = "",
            params object[] becauseArgs)
        {
            var expectedDate = expected.Date;

            Execute.Assertion
                .ForCondition(Subject.HasValue)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected a {context:date and time} with date {0}{reason}, but found a <null> DateTime.", expectedDate)
                .Then
                .ForCondition(Subject.Value.Date == expectedDate)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected a {context:date and time} with date {0}{reason}, but found {1}.", expectedDate, Subject.Value);

            return new AndConstraint<DateTimeAssertions>(this);
        }
    }
}
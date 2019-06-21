using System;
using System.Diagnostics;

namespace PhantomLib.Utilities
{
    /// <summary>
    /// Used to time methods.
    /// 
    /// Return values are not affected by timing methods -- the return values are passed through timing execution
    /// 
    /// Similarly, this works with async methods as well by simply executing the method the same way it's defined.
    /// </summary>
    /// <code>
    /// //Calling your method normally:
    /// SomeObject myReturnValue = MyMethod(bool some, int parameters);
    /// 
    /// //Calling your method with a timing method
    /// SomeObject myReturnValue = AnalyticsTimer.WithMethod(() => MyMethod(true, 42)).Time();
    /// 
    /// //In both cases, myReturnValue is the same. This works for any data type
    /// </code>
    public static class AnalyticsTimer
    {
        public delegate void OnMethodTimedEvent(long timeEllapsed, string methodName);

        /// <summary>
        /// Needed to enable the timer. Designed so that it can be turned on/off statically based on
        /// App configuration (such as debug parameters). Ideally should be set inside of Main/ AppDelegate
        /// </summary>
        public static bool ENABLED;

        /// <summary>
        /// Used to customize what happens when a method is timed. Default behavior is to simply
        /// log the method and the time it took to complete.
        /// 
        /// Custom implementations should be definted inside of Main/ AppDelegate
        /// </summary>
        public static OnMethodTimedEvent DEFAULT_TIMER_HANDLER = (timeEllapsed, methodName) =>
        {
            string name = string.IsNullOrEmpty(methodName) ? "<unspecified>" : methodName;
            Console.WriteLine($"Method {name} took {timeEllapsed} milliseconds.");
        };

        public static TimerBuilderWithReturn<T> WithMethod<T>(Func<T> method)
        {
            return new TimerBuilderWithReturn<T>(method);
        }

        public static TimerBuilderVoid WithMethod(Action method)
        {
            return new TimerBuilderVoid(method);
        }

        public class TimerBuilderWithReturn<T>
        {
            protected Func<T> Method;
            protected string MethodName = "";
            protected OnMethodTimedEvent TimerHandler = DEFAULT_TIMER_HANDLER;
            protected bool IgnoreEnabled;

            public TimerBuilderWithReturn(Func<T> method)
            {
                Method = method;
            }

            public TimerBuilderWithReturn<T> WithMethodName(string name)
            {
                MethodName = name;
                return this;
            }

            public TimerBuilderWithReturn<T> WithTimerHandler(OnMethodTimedEvent timedEvent)
            {
                TimerHandler = timedEvent;
                return this;
            }

            public TimerBuilderWithReturn<T> ForceTimer()
            {
                IgnoreEnabled = true;
                return this;
            }

            public T Time()
            {
                if (ENABLED || IgnoreEnabled)
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    T value = Method();
                    stopwatch.Stop();

                    TimerHandler(stopwatch.ElapsedMilliseconds, MethodName);

                    return value;
                }

                return Method.Invoke();
            }
        }

        public class TimerBuilderVoid
        {
            protected Action Method;
            protected string MethodName = "";
            protected OnMethodTimedEvent TimerHandler = DEFAULT_TIMER_HANDLER;
            protected bool IgnoreEnabled;

            public TimerBuilderVoid(Action method)
            {
                Method = method;
            }

            public TimerBuilderVoid WithMethodName(string name)
            {
                MethodName = name;
                return this;
            }

            public TimerBuilderVoid WithTimerHandler(OnMethodTimedEvent timedEvent)
            {
                TimerHandler = timedEvent;
                return this;
            }

            public TimerBuilderVoid ForceTimer()
            {
                IgnoreEnabled = true;
                return this;
            }

            public void Time()
            {
                if (ENABLED || IgnoreEnabled)
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    Method();
                    stopwatch.Stop();
                    TimerHandler(stopwatch.ElapsedMilliseconds, MethodName);
                }

                Method.Invoke();
            }
        }
    }
}

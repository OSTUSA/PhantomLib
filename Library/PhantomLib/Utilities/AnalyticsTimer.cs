using System;
using System.Diagnostics;
using System.Threading.Tasks;

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
            Debug.WriteLine($"Method {name} took {timeEllapsed} milliseconds.");
        };

        public static TimerBuilderWithReturn<T> WithMethod<T>(Func<T> method)
        {
            return new TimerBuilderWithReturn<T>(method);
        }
        
        public static TimerBuilderAsyncWithReturn<T> WithAsyncMethod<T>(Func<Task<T>> method)
        {
            return new TimerBuilderAsyncWithReturn<T>(method);
        }
        
        public static TimerBuilderAsyncVoid WithAsyncMethod(Func<Task> method)
        {
            return new TimerBuilderAsyncVoid(method);
        }

        public static TimerBuilderVoid WithMethod(Action method)
        {
            return new TimerBuilderVoid(method);
        }

        public abstract class TimerBuilder<T, IMPL>
        {
            protected T Method;
            protected string MethodName = "";
            protected OnMethodTimedEvent TimerHandler = DEFAULT_TIMER_HANDLER;
            protected bool IgnoreEnabled;

            public abstract IMPL This { get; }

            public IMPL WithMethodName(string name)
            {
                MethodName = name;
                return This;
            }

            public IMPL WithTimerHandler(OnMethodTimedEvent timedEvent)
            {
                TimerHandler = timedEvent;
                return This;
            }

            public IMPL ForceTimer()
            {
                IgnoreEnabled = true;
                return This;
            }
        }

        public class TimerBuilderWithReturn<T> : TimerBuilder<Func<T>, TimerBuilderWithReturn<T>>
        {
            public TimerBuilderWithReturn(Func<T> method)
            {
                Method = method;
            }

            public override TimerBuilderWithReturn<T> This
            {
                get { return this; }
            }

            public T Time()
            {
                if (!ENABLED && !IgnoreEnabled) return Method();

                Stopwatch stopwatch = Stopwatch.StartNew();
                T value = Method();
                stopwatch.Stop();

                TimerHandler(stopwatch.ElapsedMilliseconds, MethodName);

                return value;
            }
        }

        public class TimerBuilderAsyncVoid : TimerBuilder<Func<Task>, TimerBuilderAsyncVoid>
        {

            public TimerBuilderAsyncVoid(Func<Task> method)
            {
                Method = method;
            }

            public override TimerBuilderAsyncVoid This
            {
                get { return this; }
            }

            public async Task Time()
            {
                if (!ENABLED && !IgnoreEnabled)
                {
                    await Method();
                    return;
                }

                Stopwatch stopwatch = Stopwatch.StartNew();
                await Method();
                stopwatch.Stop();

                TimerHandler(stopwatch.ElapsedMilliseconds, MethodName);
            }
        }

        public class TimerBuilderAsyncWithReturn<T> : TimerBuilder<Func<Task<T>>, TimerBuilderAsyncWithReturn<T>>
        {

            public TimerBuilderAsyncWithReturn(Func<Task<T>> method)
            {
                Method = method;
            }

            public override TimerBuilderAsyncWithReturn<T> This
            {
                get { return this; }
            }

            public async Task<T> Time()
            {
                if (!ENABLED && !IgnoreEnabled) return await Method();

                Stopwatch stopwatch = Stopwatch.StartNew();
                T result = await Method();
                stopwatch.Stop();

                TimerHandler(stopwatch.ElapsedMilliseconds, MethodName);

                return result;
            }
        }

        public class TimerBuilderVoid : TimerBuilder<Action, TimerBuilderVoid>
        {
            public TimerBuilderVoid(Action method)
            {
                Method = method;
            }

            public override TimerBuilderVoid This
            {
                get { return this; }
            }

            public void Time()
            {
                if (!ENABLED && !IgnoreEnabled)
                {
                    Method();
                    return;
                }

                Stopwatch stopwatch = Stopwatch.StartNew();
                Method();
                stopwatch.Stop();
                TimerHandler(stopwatch.ElapsedMilliseconds, MethodName);
            }
        }
    }
}

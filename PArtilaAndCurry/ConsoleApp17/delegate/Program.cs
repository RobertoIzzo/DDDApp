using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace @delegate
{
    class Program
    {
        static void Main(string[] args)
        {
            // public delegate TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
            Func<int, int, int> func = Sum;

            var result = func(2, 4);

            //Anonymous functions
            Func<int, int, int> func1 = delegate (int a, int b)
            {
               return a + b;
            };

            var result1 = func(2, 4);

            // Lambda Expression
            Func<int, int, int> func2 = (a, b) => a + b;
            var result2 = func2(2, 4);
            
            // func 
            Func<int, int, int> func0 = (a, b) => func(a,b);
            var resultx = func0(2, 4);


            var res = Operation(Sum, 1, 2);

            var res1 = Opt(Sum, 1, 2);

            var res2 = Opt(SumString, "a", "b");

            var res3 = Opt((x,y) => x+y, "a", "b");

            var curry = Apply((x, y) => x + y, "a", "b");
            var res4 = curry();

            Func<int, int, int, string> functionsenzaparametri = SampleFunction;

            Func<int, int, string> partialconparametro1 = ApplyPartial(functionsenzaparametri, 1);
            string resx = partialconparametro1(2,3);//

            Func<int, string> partialconparametro1e2 = ApplyPartial(partialconparametro1, 2);
            string resy = partialconparametro1e2(3);//

            Func<string> partialconparametro123 = ApplyPartial(partialconparametro1e2, 3);
            string resz = partialconparametro123();//
        }

        static string SampleFunction(int a, int b, int c)
        {
            return string.Format("a={0}; b={1}; c={2}", a, b, c);
        }

        public static int Sum(int a, int b)
        {
            return a + b;
        }

        public static string SumString(string a, string b)
        {
            return a + b;
        }

        //generic func
        public static TResult Operation1<T1, T2, TResult>(T1 arg1, T2 arg2)
        {
            return default(TResult);
        }

        //take delegate
        public static int Operation(Func<int, int, int> fn, int a, int b )
        {
            return fn(a, b);
        }

        //take generic delegate
        public static TResult Opt<T1, T2, TResult>(Func<T1, T2, TResult> func, T1 arg1, T2 arg2)
        {
            return func(arg1, arg2);
        }

        //return delegate generic
        public static Func<TResult> Apply<T1, T2, TResult>(Func<T1, T2, TResult> func, T1 arg1, T2 arg2)
        {
            return () => func(arg1, arg2);
        }

        //partial function
        static Func<T2, T3, TResult> ApplyPartial<T1, T2, T3, TResult>
            (Func<T1, T2, T3, TResult> function, T1 arg1)
        {
            return (b, c) => function(arg1, b, c);
        }

        static Func<T3, TResult> ApplyPartial<T2, T3, TResult>
            (Func<T2, T3, TResult> function, T2 arg2)
        {
            return c => function(arg2,c);
        }

        static Func<TResult> ApplyPartial<T3, TResult>
            (Func<T3, TResult> function, T3 arg3)
        {
            return () => function(arg3);
        }
    }
}

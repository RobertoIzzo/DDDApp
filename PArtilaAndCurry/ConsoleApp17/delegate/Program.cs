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

            // Normal call 
            string resulta = SampleFunction(1, 2, 3);
            
            // Call via currying 
            Func<int, Func<int, Func<int, string>>> f1 = Curry<int,int,int,string>(SampleFunction);
            Func<int, Func<int, string>> f2 = f1(1);
            Func<int, string> f3 = f2(2);
            string resultb = f3(3);

            //Or to do make all the calls together… 
            var curried = Curry<int, int, int, string>(SampleFunction);
            string result3 = curried(1)(2)(3);
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
        //converts a function with N parameters into a function 
        //with N-1 parameters by applying one argument, 
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

        //Currying
        //Whereas partial function application 
        //converts a function with N parameters into a function 
        //with N-1 parameters by applying one argument, 
        //currying effectively decomposes the function into functions taking 
        //a single parameter.We don’t pass any arguments into the Curry method itself:
        //Curry(f) returns a function f1 such that…
        //f1(a) returns a function f2 such that…
        //f2(b) returns a function f3 such that…
        //f3(c) invokes f(a, b, c)
        static Func<T1, Func<T2, Func<T3, TResult>>> Curry<T1, T2, T3, TResult>
            (Func<T1, T2, T3, TResult> function)
        {
            return a => b => c => function(a, b, c);
        }

        static Func<T1, Func<T2, Func<T3, TResult>>> Curry1<T1, T2, T3, TResult>
            (Func<T1, T2, T3, TResult> function)
        {
            return delegate(T1 a)
            {
                return delegate(T2 b)
                {
                    return delegate(T3 c)
                    {
                        return function(a, b, c);
                    };
                };
            };
        }

        static Func<T1, Func<T2, Func<T3, TResult>>> Curry2<T1, T2, T3, TResult>
            (Func<T1, T2, T3, TResult> function)
        {
            Func<T2, Func<T3, TResult>> Func(T1 a)
            {
                Func<T3, TResult> Func1(T2 b)
                {
                    TResult Func2(T3 c) => function(a, b, c);
                    return Func2;
                }
                return Func1;
            }
            return Func;
        }
    }
}

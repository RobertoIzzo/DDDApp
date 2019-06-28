using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp17
{


    //    function curry(fn)
    //    {
    //    var arity = fn.length;

    //        if (arguments.length - 1 < arity)
    //    {
    //        return curry.bind(null, ...arguments);
    //    }
    //    else
    //    {
    //    return fn.apply(null, Array.prototype.slice.call(arguments, 1));
    //}
    //}
    //    function sum(x, y, z)
    //    {
    //    return x + y + z;
    //}

    //var curriedSum = curry(sum)
    //var add20 = curriedSum(15, 5)
    //var res = add20(6) //26
    class Program
    {
        static void Main(string[] args)
        {
            var curriedSum = CurryTest.Apply<int,int,int>(Sum, 1, 2);
            var res = curriedSum();//return 3
        }
        public static int Sum(int x, int y)
        {
            return x + y;
        }
    }


    class CurryTest
    {
        public static Func<TResult> Apply<TResult, TArg1, TArg2>(Func<TArg1, TArg2, TResult> func,
            TArg1 arg1, TArg2 arg2)
        {
            return () => func(arg1, arg2);
        }
    }


}

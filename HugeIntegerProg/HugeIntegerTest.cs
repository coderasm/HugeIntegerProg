using System;

namespace HugeIntegerProg
{
  class HugeIntegerTest
  {
    public static void MethodTests()
    {
      string[,] pairs = { { "100", "7" }, { "2019", "2310" }, { "8384724", "2395" } };
      for (int i = 0; i < pairs.GetLength(0); i++)
      {
        var hugeIntOne = HugeInteger.Input(pairs[i, 0]);
        var hugeIntTwo = HugeInteger.Input(pairs[i, 1]);
        var sum = hugeIntOne.sum(hugeIntTwo);
        var diff = hugeIntOne.diff(hugeIntTwo);
        var prod = hugeIntOne.prod(hugeIntTwo);
        var div = hugeIntOne.div(hugeIntTwo);
        var mod = hugeIntOne.mod(hugeIntTwo);
        Console.WriteLine(hugeIntOne.toString() + " + " + hugeIntTwo.toString() + " = " + sum.toString());
        Console.WriteLine(hugeIntOne.toString() + " - " + hugeIntTwo.toString() + " = " + diff.toString());
        Console.WriteLine(hugeIntOne.toString() + " * " + hugeIntTwo.toString() + " = " + prod.toString());
        Console.WriteLine(hugeIntOne.toString() + " / " + hugeIntTwo.toString() + " = " + div.toString());
        Console.WriteLine(hugeIntOne.toString() + " % " + hugeIntTwo.toString() + " = " + mod.toString());
      }
    }

    public static void FactorialTest()
    {
      var FACTORIAL = 25;
      var accumulator = HugeInteger.Input("1");
      for (int i = 2; i <= FACTORIAL; i++)
      {
        var multiplier = HugeInteger.Input(i.ToString());
        accumulator = accumulator.prod(multiplier);
      }
      Console.WriteLine(FACTORIAL + "! = " + accumulator.toString());
    }
  }
}

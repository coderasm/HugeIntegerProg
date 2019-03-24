using System;

namespace HugeIntegerProg
{
  class HugeIntegerTest
  {
    public static void Test()
    {
      var hugeIntOne = HugeInteger.Input("56");
      var hugeIntTwo = HugeInteger.Input("48");
      var prod = hugeIntOne.prod(hugeIntTwo);
      Console.WriteLine(hugeIntOne.toString() + " * " + hugeIntTwo.toString() + " = " + prod.toString());
      Console.ReadKey();
    }
  }
}

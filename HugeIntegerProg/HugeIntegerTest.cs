using System;

namespace HugeIntegerProg
{
  class HugeIntegerTest
  {
    public static void Test()
    {
      var hugeIntOne = HugeInteger.Input("67285");
      var hugeIntTwo = HugeInteger.Input("-66374");
      var sum = hugeIntOne.sum(hugeIntTwo);
      Console.WriteLine(hugeIntOne.toString() + " + " + hugeIntTwo.toString() + " = " + sum.toString());
      Console.ReadKey();
    }
  }
}

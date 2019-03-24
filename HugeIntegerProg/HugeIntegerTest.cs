using System;

namespace HugeIntegerProg
{
  class HugeIntegerTest
  {
    public static void Test()
    {
      var hugeIntOne = HugeInteger.Input("-23");
      var hugeIntTwo = HugeInteger.Input("3");
      var div = hugeIntOne.div(hugeIntTwo);
      Console.WriteLine(hugeIntOne.toString() + " / " + hugeIntTwo.toString() + " = " + div.toString());
      Console.ReadKey();
    }
  }
}

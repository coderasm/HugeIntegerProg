using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugeIntegerProg
{
  class HugeInteger
  {
    private static int DIGITS_SIZE = 30;
    private int[] digits = new int[DIGITS_SIZE];
    private Sign sign = Sign.None;

    public HugeInteger(int[] digits, Sign sign)
    {
      this.digits = digits;
      this.sign = sign;
    }

    public HugeInteger sum(HugeInteger hugeInt)
    {
      bool summing = true;
      if (sign != hugeInt.sign && sign != Sign.None && hugeInt.sign != Sign.None)
        summing = false;

      var lengthOne = 0;
      for (int i = 0; i < digits.Length; i++)
      {
        if(digits[i] != 0)
        {
          lengthOne = digits.Length - i;
          break;
        }
      }

      var lengthTwo = 0;
      for (int i = 0; i < hugeInt.digits.Length; i++)
      {
        if (hugeInt.digits[i] != 0)
        {
          lengthTwo = hugeInt.digits.Length - i;
          break;
        }
      }

      var longest = lengthOne > lengthTwo ? this : hugeInt;
      var shortest = lengthOne < lengthTwo ? this : hugeInt;

      var largest = longest;
      var smallest = shortest;

      if(lengthOne == lengthTwo)
      {

      }
    }
  }

  enum Sign
  {
    Negative,
    None,
    Positive
  }
}

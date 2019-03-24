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
    private Sign sign = Sign.Positive;

    private HugeInteger(int[] digits, Sign sign)
    {
      this.digits = digits;
      this.sign = sign;
    }

    public static HugeInteger Input(string number)
    {
      int[] digits = new int[DIGITS_SIZE];
      var sign = number.StartsWith("-") ? Sign.Negative : Sign.Positive;
      var offset = sign == Sign.Negative ? 1 : 0;
      for (int i = 0; i < number.Length - offset; i++)
      {
        digits[i] = int.Parse(number[number.Length - i - 1].ToString());
      }
      return new HugeInteger(digits, sign);
    }

    public HugeInteger sum(HugeInteger hugeInt)
    {
      var carry = 0;
      bool summing = true;
      if (sign != hugeInt.sign)
        summing = false;
      var newDigits = new int[30];
      var newSign = Sign.Positive;
      if (summing)
      {
        for (int i = 0; i < DIGITS_SIZE; i++)
        {
          newDigits[i] = (digits[i] + hugeInt.digits[i] + carry) % 10;
          carry = (digits[i] + hugeInt.digits[i] + carry) / 10;
        }
        newSign = sign;
      }
      else
      {
        if (hugeInt.sign == Sign.Negative)
          hugeInt.sign = Sign.Positive;
        return diff(hugeInt);
      }
      return new HugeInteger(newDigits, newSign);
    }

    public HugeInteger diff(HugeInteger hugeInt)
    {
      var carry = 0;
      if (sign == Sign.Negative && hugeInt.sign == Sign.Positive)
      {
        hugeInt.sign = Sign.Negative;
        return sum(hugeInt);
      }
      if (sign == Sign.Positive && hugeInt.sign == Sign.Negative)
      {
        hugeInt.sign = Sign.Positive;
        return sum(hugeInt);
      }

      var lengthOne = length();
      var lengthTwo = hugeInt.length();

      var longest = lengthOne > lengthTwo ? this : hugeInt;
      var shortest = lengthOne < lengthTwo ? this : hugeInt;

      var largest = longest;
      var smallest = shortest;
      if (largest == smallest)
      {
        largest = larger(hugeInt);
        smallest = largest == this ? hugeInt : this;
      }

      var newDigits = new int[30];
      var newSign = Sign.Positive;
      for (int i = DIGITS_SIZE - 1; i >= 0; i--)
      {
        var larger = largest.digits[i];
        if (i > 0 && (smallest.digits[i - 1] > largest.digits[i - 1]))
          larger = largest.digits[i] - 1;
        newDigits[i] = ((larger + carry) - smallest.digits[i]) % 10;
        if (i > 0 && (smallest.digits[i - 1] > largest.digits[i - 1]))
          carry = 10;
        else
          carry = 0;
      }
      newSign = largest.sign;
      return new HugeInteger(newDigits, newSign);
    }

    private HugeInteger larger(HugeInteger hugeInt)
    {
      var larger = this;
      for (int i = DIGITS_SIZE - 1; i >= 0; i--)
      {
        if (digits[i] > hugeInt.digits[i])
        {
          larger = this;
          break;
        }
        else if (digits[i] < hugeInt.digits[i])
        {
          larger = hugeInt;
          break;
        }
      }
      return larger;
    }

    private int length()
    {
      var length = DIGITS_SIZE;
      for (int i = digits.Length - 1; i >= 0; i--)
      {
        if (digits[i] != 0)
        {
          length = i + 1;
          break;
        }
      }
      return length;
    }

    public string toString()
    {
      var number = "";
      var length = this.length();
      for (int i = length - 1; i >= 0; i--)
      {
        number += digits[i];
      }
      var signToPrint = sign == Sign.Positive ? "" : "-";
      return signToPrint + number;
    }
  }

  enum Sign
  {
    Negative,
    Positive
  }
}

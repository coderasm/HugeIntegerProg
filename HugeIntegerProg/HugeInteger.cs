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

    private HugeInteger()
    {

    }

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
        //Don't overwrite the operand
        var bigger = largest.digits[i];
        if (i > 0 && (smallest.digits[i - 1] > largest.digits[i - 1]))
          bigger = largest.digits[i] - 1;
        newDigits[i] = ((bigger + carry) - smallest.digits[i]) % 10;
        if (i > 0 && (smallest.digits[i - 1] > largest.digits[i - 1]))
          carry = 10;
        else
          carry = 0;
      }
      newSign = largest.sign;
      return new HugeInteger(newDigits, newSign);
    }

    public HugeInteger prod(HugeInteger hugeInt)
    {
      var carry = 0;
      var accumulator = new HugeInteger(new int[DIGITS_SIZE], Sign.Positive);
      for (int i = 0; i < 15; i++)
      {
        var product = new int[DIGITS_SIZE];
        var coefficientOne = digits[i];
        for (int j = 0; j < 16; j++)
        {
          var coefficientTwo = hugeInt.digits[j];
          product[i + j] = (coefficientOne * coefficientTwo + carry) % 10;
          carry = coefficientOne * coefficientTwo + carry > 10 ? (coefficientOne * coefficientTwo + carry) / 10 : 0;
        }
        accumulator = accumulator.sum(new HugeInteger(product, Sign.Positive));
      }
      var newSign = sign != hugeInt.sign ? Sign.Negative : Sign.Positive;
      return new HugeInteger(accumulator.digits, newSign);
    }

    public HugeInteger div(HugeInteger hugeInt)
    {
      if (hugeInt.length() > length())
        return new HugeInteger();
      else
      {
        var newSign = sign != hugeInt.sign ? Sign.Negative : Sign.Positive;
        var divisor = hugeInt.clone();
        divisor.sign = Sign.Positive;
        var dividend = clone();
        dividend.sign = Sign.Positive;
        var quotient = new HugeInteger();
        while (dividend.larger(divisor) != divisor)
        {
          quotient = quotient.sum(Input("1"));
          dividend = dividend.diff(divisor);
        }
        quotient.sign = newSign;
        return quotient;
      }
    }

    public HugeInteger clone()
    {
      var zero = new HugeInteger();
      return sum(zero);
    }

    private HugeInteger larger(HugeInteger hugeInt)
    {
      if (length() > hugeInt.length())
        return this;
      if (hugeInt.length() > length())
        return hugeInt;
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

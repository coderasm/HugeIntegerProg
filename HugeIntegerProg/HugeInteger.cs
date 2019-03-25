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
        var clone = hugeInt.clone();
        clone.sign = Sign.Positive;
        return diff(clone);
      }
      return new HugeInteger(newDigits, newSign);
    }

    public HugeInteger diff(HugeInteger hugeInt)
    {
      var carry = 0;
      if (sign == Sign.Negative && hugeInt.sign == Sign.Positive)
      {
        var clone = hugeInt.clone();
        clone.sign = Sign.Negative;
        return sum(clone);
      }
      if (sign == Sign.Positive && hugeInt.sign == Sign.Negative)
      {
        var clone = hugeInt.clone();
        clone.sign = Sign.Positive;
        return sum(clone);
      }

      var largest = larger(hugeInt);
      var smallest = largest == this ? hugeInt : this;
      largest = preBorrow(largest, smallest);

      var newDigits = new int[30];
      var newSign = largest.sign;
      for (int i = DIGITS_SIZE - 1; i >= 0; i--)
      {
        newDigits[i] = (largest.digits[i] - smallest.digits[i]) % 10;
      }
      return new HugeInteger(newDigits, newSign);
    }

    private HugeInteger preBorrow(HugeInteger larger, HugeInteger smaller)
    {
      var largerClone = larger.clone();
      for (int i = DIGITS_SIZE - 1; i >= 0; i--)
      {
        if (larger.digits[i] < smaller.digits[i])
        {
          //find index to borrow from
          var greaterThanIndex = 0;
          for (int p = i + 1; p < DIGITS_SIZE; p++)
          {
            if (largerClone.digits[p] > smaller.digits[p])
            {
              greaterThanIndex = p;
              break;
            }
          }
          //borrow
          largerClone.digits[greaterThanIndex]--;
          //distribute the borrow
          for (int j = greaterThanIndex - 1; j > i; j--)
          {
            largerClone.digits[j] += 9;
          }
          largerClone.digits[i] += 10;
        }
      }
      return largerClone;
    }

    public HugeInteger prod(HugeInteger hugeInt)
    {
      var carry = 0;
      var accumulator = new HugeInteger(new int[DIGITS_SIZE], Sign.Positive);
      var thisLength = length();
      thisLength = thisLength == DIGITS_SIZE ? thisLength : thisLength + 1;
      var hugeIntLength = hugeInt.length();
      hugeIntLength = hugeIntLength == DIGITS_SIZE ? hugeIntLength : hugeIntLength + 1;
      for (int i = 0; i < thisLength; i++)
      {
        var product = new int[DIGITS_SIZE];
        var coefficientOne = digits[i];
        for (int j = 0; j < hugeIntLength; j++)
        {
          var coefficientTwo = hugeInt.digits[j];
          product[i + j] = (coefficientOne * coefficientTwo + carry) % 10;
          carry = coefficientOne * coefficientTwo + carry;
          carry = carry >= 10 ? carry / 10 : 0;
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

    public HugeInteger mod(HugeInteger hugeInt)
    {
      var newSign = sign != hugeInt.sign ? Sign.Negative : Sign.Positive;
      if (hugeInt.length() > length())
      {
        var result = clone();
        result.sign = newSign;
        return result;
      }
      else
      {
        var divisor = hugeInt.clone();
        divisor.sign = Sign.Positive;
        var dividend = clone();
        dividend.sign = Sign.Positive;
        while (dividend.larger(divisor) != divisor)
        {
          dividend = dividend.diff(divisor);
        }
        return dividend;
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
      var length = 1;
      for (int i = DIGITS_SIZE - 1; i >= 0; i--)
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

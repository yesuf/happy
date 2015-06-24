using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Calculator
{
    class Calculate
    {
        ulong firstNumber { get; set; }
        ulong secondNumber { get; set; }
        public static ulong result = 0;
        string equationOperator { get; set; }

        public Calculate(ulong firstNumber, ulong secondNumber, string equationOperator)
        {

            this.firstNumber = firstNumber;
            this.secondNumber = secondNumber;
            this.equationOperator = equationOperator;

            switch (this.equationOperator)
            {
                case "+":
                    result = this.firstNumber + this.secondNumber;
                    break;
                case "-":
                    if (result < this.secondNumber)
                    {
                        result = this.secondNumber - this.firstNumber;
                        break;
                    }
                    result = this.firstNumber - this.secondNumber;
                    break;
                case "/":
                    if (this.secondNumber == 0)
                    {
                        Console.WriteLine("\nCannot Divide by Zero!");
                        break;
                    }
                    result = this.firstNumber / this.secondNumber;
                    break;
                case "*":
                    result = this.firstNumber * this.secondNumber;
                    break;
                case "%":
                    result = this.firstNumber % this.secondNumber;
                    break;
            }

            Console.WriteLine("\n{0}\t{1}\t{2}\t= {3}", this.firstNumber, this.equationOperator, this.secondNumber, result);
        }


    }

    class ContinueEval
    {
        public static void continueEvaluate()
        {
            string nextInput = "", equationOperator = "", secondNumber = "";
            ulong prevResult;
            while (nextInput != "done")
            {
                Console.Write("\n{0}", Calculate.result);
                prevResult = Calculate.result;
                nextInput = Console.ReadLine();
                if (nextInput == "done")
                    break;
                ushort n = 1;
                for (n = 0; n < nextInput.Length; n++)
                {
                    if (!(Regex.IsMatch(nextInput.Substring(0, n), @"^\d+$")))
                    {
                        equationOperator = nextInput[n].ToString();
                        break;
                    }
                }
                secondNumber = nextInput.Substring((n + 1), (nextInput.Length - 1));

                switch (equationOperator)
                {
                    case "+":
                        Calculate.result += ulong.Parse(secondNumber);
                        break;
                    case "-":
                        if (Calculate.result < ulong.Parse(secondNumber))
                        {
                            Calculate.result = ulong.Parse(secondNumber) - Calculate.result;
                            break;
                        }
                        Calculate.result -= ulong.Parse(secondNumber);
                        break;
                    case "/":
                        if (ulong.Parse(secondNumber) == 0)
                        {
                            Console.WriteLine("\nCannot Divide by Zero!");
                            break;
                        }
                        Calculate.result /= ulong.Parse(secondNumber);
                        break;
                    case "*":
                        Calculate.result *= ulong.Parse(secondNumber);
                        break;
                    case "%":
                        Calculate.result %= ulong.Parse(secondNumber);
                        break;
                }
                Console.WriteLine("\n{0}\t{1}\t{2}\t= {3}", prevResult, equationOperator, secondNumber, Calculate.result);

            };
        }
    }

    class ESlicer
    {
        public static void sliceEquation(string inputValue)
        {
            if (inputValue == "done")
                Environment.Exit(0);
            string firstNumber = "", secondNumber = "", equationOperator = "";
            ushort n = 1, end = 0;

            while (Regex.IsMatch(inputValue.Substring(0, n), @"^\d+$"))
            {
                firstNumber = inputValue.Substring(0, n);
                if (!(Regex.IsMatch(inputValue[n].ToString(), @"^\d+$")))
                {
                    equationOperator = inputValue[n].ToString();
                    break;
                }
                if (n > inputValue.Length)
                    break;
                n++;
            }
            end = (ushort)(inputValue.Length - (firstNumber.Length + 1));
            secondNumber = inputValue.Substring((n + 1), end);
            Calculate p = new Calculate(ulong.Parse(firstNumber), ulong.Parse(secondNumber), equationOperator);
        }
    }


    class Program
    {
        static void Main()
        {
            string inputValue;
            Console.Write("Enter string of Equation: ");
            inputValue = Console.ReadLine();


            ESlicer.sliceEquation(inputValue);
            ContinueEval.continueEvaluate();
            Console.Write("Thank you for using!");
            Console.Read();
        }

    }
}
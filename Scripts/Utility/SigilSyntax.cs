namespace BrokenSigilCollection.Utility;

using System;
using System.Globalization;
using System.Numerics;
using Godot;

/// <summary>
/// Provides utility methods for parsing and processing numeric and syntax operations.
/// </summary>
public static class SigilSyntax
{

    private const string SYNTAX_CHARACTERS = "+-*/=#@";

    /// <summary>
    /// Parses a string with a numeric suffix into a Variant.
    /// </summary>
    public static Variant ParseNumber(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input cannot be null or empty.", nameof(input));

        // Handle multi-char suffixes like "ul", "us"
        string suffix = GetSuffix(input, out string numberPart);

        return suffix switch
        {
            "f" => float.Parse(numberPart, CultureInfo.InvariantCulture),
            "d" => double.Parse(numberPart, CultureInfo.InvariantCulture),
            "sb" => sbyte.Parse(numberPart, CultureInfo.InvariantCulture),
            "s" => short.Parse(numberPart, CultureInfo.InvariantCulture),
            "i" => int.Parse(numberPart, CultureInfo.InvariantCulture),
            "l" => long.Parse(numberPart, CultureInfo.InvariantCulture),

            "b" => byte.Parse(numberPart, CultureInfo.InvariantCulture),   // same as b
            "us" => ushort.Parse(numberPart, CultureInfo.InvariantCulture),
            "ui" => uint.Parse(numberPart, CultureInfo.InvariantCulture),
            "ul" => ulong.Parse(numberPart, CultureInfo.InvariantCulture),

            _ => throw new FormatException($"Unknown numeric suffix '{suffix}'")
        };
    }

    /// <summary>
    /// Processes two numbers and applies an operation based on the syntax.
    /// </summary>
    public static Variant ProcessNumbers(Variant num1, string opNum2)
    {
        if (string.IsNullOrWhiteSpace(opNum2))
            throw new ArgumentException("Input cannot be null or empty.", nameof(opNum2));

        var (op2, val2) = ParseOperation(opNum2);

        // Handle multi-char suffixes like "ul", "us"
        string suffix = GetSuffix(val2, out string num2);

        return suffix switch
        {
            "f" => Operate<float>(num1.As<float>(), float.Parse(num2, CultureInfo.InvariantCulture), op2),
            "d" => Operate<double>(num1.As<double>(), double.Parse(num2, CultureInfo.InvariantCulture), op2),
            "sb" => Operate<sbyte>(num1.As<sbyte>(), sbyte.Parse(num2, CultureInfo.InvariantCulture), op2),
            "s" => Operate<short>(num1.As<short>(), short.Parse(num2, CultureInfo.InvariantCulture), op2),
            "i" => Operate<int>(num1.As<int>(), int.Parse(num2, CultureInfo.InvariantCulture), op2),
            "l" => Operate<long>(num1.As<long>(), long.Parse(num2, CultureInfo.InvariantCulture), op2),

            "b" => Operate<byte>(num1.As<byte>(), byte.Parse(num2, CultureInfo.InvariantCulture), op2),
            "us" => Operate<ushort>(num1.As<ushort>(), ushort.Parse(num2, CultureInfo.InvariantCulture), op2),
            "ui" => Operate<uint>(num1.As<uint>(), uint.Parse(num2, CultureInfo.InvariantCulture), op2),
            "ul" => Operate<ulong>(num1.As<ulong>(), ulong.Parse(num2, CultureInfo.InvariantCulture), op2),

            _ => throw new FormatException($"Unknown numeric suffix '{suffix}'")
        };
    }

    /// <summary>
    /// Extracts the suffix from a numeric string and returns the number part.
    /// </summary>
    public static string GetSuffix(string input, out string numberPart)
    {
        // Walk backward until first non-letter
        int i = input.Length - 1;
        while (i >= 0 && char.IsLetter(input[i]))
            i--;

        numberPart = input[..(i + 1)];
        return input[(i + 1)..].ToLowerInvariant();
    }

    /// <summary>
    /// Parses an operation from a string, returning the operator and value.
    /// </summary>
    public static (char op, string value) ParseOperation(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input cannot be null or empty.", nameof(input));

        if (char.IsDigit(input[0]))
            return ('=', input);

        return (input[0], input[1..]);
    }

    /// <summary>
    /// Applies an arithmetic operation to two numbers.
    /// </summary>
    public static T Operate<[MustBeVariant] T>(T num1, T num2, char op) where T : INumber<T>
    {
        return op switch
        {
            '+' => num1 + num2,
            '-' => num1 - num2,
            '*' => num1 * num2,
            '/' => num1 / num2,
            '=' => num2, // replace → ignore d1, return d2
            _ => throw new InvalidOperationException($"Unknown operation '{op}'")
        };
    }

    /// <summary>
    /// Checks if the input character is a syntax character.
    /// </summary>
    public static bool IsSyntaxChar(char input)
    {
        foreach (char character in SYNTAX_CHARACTERS)
        {
            if (input == character)
                return true;
        }
        return false;
    }
    
}

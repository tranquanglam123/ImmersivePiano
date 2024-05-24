using UnityEngine;

public static class StringExtensions
{
    public static bool ContainSharp(this string s)
    {
        return (s.Contains("#"));
    }
    public static string ToBold(this string text)
    {
        return $"<b>{text}</b>";
    }
    public static string ToUnderline(this string text)
    {
        return $"<u>{text}</u>";
    }
    public static string ToItalic(this string text)
    {
        return $"<i>{text}</i>";
    }
}

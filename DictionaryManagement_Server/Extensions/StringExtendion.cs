using System.Text;

public static class StringExtension
{
    public static string HideSlash(this string value)
    {        
        return value.Replace("\\", "__123455432__");
    }

    public static string UnhideSlash(this string value)
    {
        return value.Replace("__123455432__", "\\");
    }
}
using System.Globalization;
using CommunityToolkit.Maui.Converters;

namespace ToDoList.App.Converters;

public class ToDoItemToColorConverter : BaseConverterOneWay<bool, Color>
{
    public override Color ConvertFrom(bool value, CultureInfo? culture)
    {
        if (value)
        {
            return Colors.Green;
        }

        return Colors.Red;
    }

    public override Color DefaultConvertReturnValue { get; set; }
}
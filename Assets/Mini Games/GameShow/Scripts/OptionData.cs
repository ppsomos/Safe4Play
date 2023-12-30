using System;

public class OptionData
{
	internal string optionText;
	internal bool value;
	internal int id;
	internal string imageUrl;

	public OptionData(string text, string value2)
	{
		optionText = text;
		value = value2.StartsWith("1", StringComparison.Ordinal) ? true : false;
	}

}
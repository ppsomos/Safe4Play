using System;

public class OptionDataBM
{
	internal string optionText;
	internal bool value;

	public OptionDataBM(string text, string value2)
	{
		optionText = text;
		value = value2.StartsWith("1", StringComparison.Ordinal) ? true : false;
	}

}
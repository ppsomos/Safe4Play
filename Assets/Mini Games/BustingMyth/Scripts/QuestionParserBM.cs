using System;
using UnityEngine;

public class QuestionParserBM
{
    public QuestionBM ConvertString(string s)
    {
        Debug.Log(s);
        QuestionBM q = new QuestionBM();

        // Get question string
        int beginIndex = s.IndexOf("questionString", StringComparison.Ordinal);
        if (beginIndex >= 0)
        {
            beginIndex += 17;
            int endIndex = s.IndexOf('\"', beginIndex);
            q.questionString = s.Substring(beginIndex, endIndex - beginIndex);
        }

        // Get explanation
        int explanationBeginIndex = s.IndexOf("explanation", StringComparison.Ordinal);
        if (explanationBeginIndex >= 0)
        {
            explanationBeginIndex += 14;
            int explanationEndIndex = s.IndexOf('\"', explanationBeginIndex);
            q.explanation = s.Substring(explanationBeginIndex, explanationEndIndex - explanationBeginIndex);
        }

        // Get externalContent
        int linkBeginIndex = s.IndexOf("externalContent", StringComparison.Ordinal);
        if (linkBeginIndex >= 0)
        {
            linkBeginIndex += 18;
            int linkEndIndex = s.IndexOf('\"', linkBeginIndex);
            q.externalContent = s.Substring(linkBeginIndex, linkEndIndex - linkBeginIndex);
        }

        // Get options
        while (s.Contains("option"))
        {
            s = s.Substring(FindNextOption(s) + 11);
            string text = FindNextText(s);
            string value = FindNextValue(s);

            OptionDataBM od = new OptionDataBM(text, value);
            q.options.Add(od);
        }

        return q;
    }

    private int FindNextOption(string s)
    {
        return s.IndexOf("option", StringComparison.Ordinal);
    }

    private string FindNextText(string s)
    {
        int beginIndex = s.IndexOf("text", StringComparison.Ordinal) + 7;
        int endIndex = s.IndexOf('\"', beginIndex);
        return s.Substring(beginIndex, endIndex - beginIndex);
    }

    private string FindNextValue(string s)
    {
        int beginIndex = s.IndexOf("value", StringComparison.Ordinal) + 8;
        int endIndex = s.IndexOf('\"', beginIndex);
        return s.Substring(beginIndex, endIndex - beginIndex);
    }


    // public QuestionBM ConvertString(string s)
    // {
    //     QuestionBM q = new QuestionBM();
    //
    //     // Get question string
    //     int beginIndex = s.IndexOf("questionString", StringComparison.Ordinal) + 17;
    //     int endIndex = s.IndexOf('\"', beginIndex);
    //
    //     q.questionString = (string)s.Substring(beginIndex, endIndex - beginIndex);
    //
    //     int explanationBeginIndex = s.IndexOf("explanation", StringComparison.Ordinal) + 14;
    //     int explanationEndIndex = s.IndexOf('\"', explanationBeginIndex);
    //     q.explanation = (string)s.Substring(explanationBeginIndex, explanationEndIndex - explanationBeginIndex);
    //
    //     int linkBeginIndex = s.IndexOf("externalContent", StringComparison.Ordinal) + 18;
    //     int linkEndIndex = s.IndexOf('\"', linkBeginIndex);
    //     q.externalContent = (string)s.Substring(linkBeginIndex, linkEndIndex - linkBeginIndex);
    //
    //     // Get options
    //     s = s.Substring(FindNextOption(s) + 11);
    //
    //     do
    //     {
    //         s = s.Substring(FindNextOption(s) + 10);
    //         string Text = FindNextText(s);
    //         string Value = FindNextValue(s);
    //
    //         OptionDataBM od = new OptionDataBM(Text, Value);
    //         q.options.Add(od);
    //     } while (s.Length > 0 && s.Contains("option"));
    //
    //     return q;
    // }

    // Find value 1 or 0 for an option
    // private static string FindNextValue(string s)
    // {
    //     int x = s.IndexOf("value\":", StringComparison.Ordinal);
    //     return s.Substring(x + 7, 1);
    //
    // }
    //
    // // Find option text
    // private static string FindNextText(string s)
    // {
    //     int x = s.IndexOf("text\": ", StringComparison.Ordinal);
    //     string temp = s.Substring(x + 6);
    //     int y = temp.IndexOf("\",", StringComparison.Ordinal);
    //     return s.Substring(x + 9, y - 3);
    // }
    //
    // private static int FindNextOption(string s)
    // {
    //
    //     int option = s.IndexOf("option", StringComparison.Ordinal);
    //     return option;
    // }
}
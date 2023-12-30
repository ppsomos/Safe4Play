using System;
using UnityEngine;

public class QuestionParser
{
    public Question ConvertString(string s)
    {
        Question q = new Question();
 
        // Get question string
        int beginIndex = s.IndexOf("questionString", StringComparison.Ordinal) + 17;
        int endIndex = s.IndexOf('\"', beginIndex);
        q.questionString = (string)s.Substring(beginIndex, endIndex - beginIndex);

        GetFeedback(q, s);

        // Get options
        
        s = s.Substring(FindNextOption(s) + 11);

        do
        {
            s = s.Substring(FindNextOption(s) + 10);
            string Text = FindNextText(s);
            string Value = FindNextValue(s);

            OptionData od = new OptionData(Text, Value);
            q.options.Add(od);
        } while (s.Length > 0 && s.Contains("\"option"));

        return q;
    }

    private void GetFeedback(Question q, string s)
    {
        
        int x1 = FindNextFeedback(s) + 11;
        string temp1 = s.Substring(x1);
        int y1 = temp1.IndexOf("}", StringComparison.Ordinal);
        s = s.Substring(x1, y1);

        Debug.Log("START" + s);

        do
        {
            int x = FindNextFeedback(s) + 12;
            string temp = s.Substring(x);
            int y = temp.IndexOf("\"", StringComparison.Ordinal);
            string f = s.Substring(x, y);
            q.feedback.Add(f);
            
            s = s.Substring(FindNextFeedback(s)+12);
        } while (s.Length > 0 && s.Contains("feedback"));
    }

    // Find value 1 or 0 for an option
    private static string FindNextValue(string s)
    {
        int x = s.IndexOf("value\":", StringComparison.Ordinal);
        return s.Substring(x + 7, 1);

    }

    // Find option text
    private static string FindNextText(string s)
    {
        int x = s.IndexOf("text\": ", StringComparison.Ordinal);
        string temp = s.Substring(x + 9);
        int y = temp.IndexOf("\",", StringComparison.Ordinal);
        return s.Substring(x + 9, x + y + 1);
    }

    private static int FindNextOption(string s)
    {
        
        int option = s.IndexOf("option", StringComparison.Ordinal);
        return option;
    }

    private static int FindNextFeedback(string s)
    {
        int feedback = s.IndexOf("feedback", StringComparison.Ordinal);
        return feedback;
    }
}

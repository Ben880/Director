
using System.Text;
using TMPro;
using UnityEngine;

public class D_Debug
{
    private static bool useTextMesh = false;
    private static int listSize = 20;
    private static TextMeshProUGUI text;
    private static D_CircularList<string> strings = new D_CircularList<string>(listSize);
    private static StringBuilder sb = new StringBuilder();
    private static D_DirectorObjects director;

    public D_Debug()
    {
    }

    public D_Debug(D_DirectorObjects d)
    {
        director = d;
    }

    public void Log(string message)
    {
        strings.add(message);
        if (director.isDebug())
        {
            if (!useTextMesh)
            {
                Debug.Log(message);
            }
            else
            {
                sb.Clear();
                foreach (var str in strings.getRange(listSize*-1, 0))
                {
                    sb.Append(str);
                    sb.Append("\n");
                }
                text.text = sb.ToString();
            }
        }
    }
    
    public void setConsole(TextMeshProUGUI t)
    {
        text = t;
        useTextMesh = true;
    }
}

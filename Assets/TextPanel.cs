using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextPanel : MonoBehaviour {

    public Text codeText;
    public static TextPanel cur;
    public static string startingText;

    void Awake () {
        cur = this;
    }

    public static void GenerateText (params Codeblock[] blocks) {
        cur.codeText.text = startingText + "\n";
        foreach (Codeblock block in blocks) {
            cur.codeText.text += block.data.code + "\n";
        }
    }

}

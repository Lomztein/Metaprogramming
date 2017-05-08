using UnityEngine;
using System.Collections;

[CreateAssetMenu (fileName = "New Data", menuName = "Metaprogramming/Create Data", order = 0)]
public class CodeblockData : ScriptableObject {

    [TextArea]
    public string code;
    public string codeDesc;
    public bool correct;

}

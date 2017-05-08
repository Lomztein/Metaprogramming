using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class SequenceStep {

    public string stepComment;
    public string stepName;
    public bool resetAfter;

    public CodeblockData[] codeBlocks;

}

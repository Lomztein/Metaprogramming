using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SequenceStep : MonoBehaviour {

    public string levelName; // This should only have a value for the first one.
    public string startingCode; // Ditto

    public string stepComment;
    public string stepName;
    public bool resetAfter;

    public CodeblockData[] codeBlocks;
    public SequenceStep nextStep;
    public List<GameObject> currentBlocks;

    void Start () {
        // Shuffle blovks
        List<CodeblockData> remaining = codeBlocks.ToList ();
        for (int i = 0; i < codeBlocks.Length; i++) {
            int index = Random.Range (0, remaining.Count);
            codeBlocks[i] = remaining[index];
            remaining.RemoveAt (index);
        }

        CreateBlocks ();
        AllPanels.SetSequenceText (stepName);
    }

    public void CreateBlocks () {
        foreach (CodeblockData block in codeBlocks) {
            GameObject newBlock = Codeblock.Create (block, AllPanels.panels.codeblockContainer);
            currentBlocks.Add (newBlock);
        }
    }

    private void DestroyBlocks () {
        foreach (GameObject obj in currentBlocks) {
            Destroy (obj);
        }
    }

    public void OnAddedToSequence () {
        bool anyCorrect = currentBlocks.Where (x => x.GetComponent<Codeblock>().data.correct).Count () != 0;
        if (anyCorrect)
            return;

        DestroyBlocks ();
        if (nextStep) {
            GameObject newStep = (GameObject)Instantiate (nextStep.gameObject);
            SequenceStep step = newStep.GetComponent<SequenceStep> ();
            AllPanels.panels.currentStep = step;

            if (stepComment.Length > 0) {
                Codeblock.CreateComment (stepComment, AllPanels.panels.codeSeqenceContainer);
            }
        } else {
            Debug.Log ("Ran out of steps lol");
        }

        Destroy (gameObject);
    }
}

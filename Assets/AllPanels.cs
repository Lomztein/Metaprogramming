using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AllPanels : MonoBehaviour {

    public static AllPanels panels;

    public RectTransform codeblockContainer;
    public RectTransform codeSeqenceContainer;

    public Text headerTitleText;
    public Text headerSequenceText;

    public SequenceStep[] startingSteps;
    public SequenceStep currentStep;

    public static int errorCount = -1;
    public const int MAX_ERRS = 3;
    public Text errorCountText;

    public int yellowThreshold = 1;
    public int redTreshold = 2;

    void Awake () {
        panels = this;
    }

    void Start () {
        StartSequence (0);
        OnShitHappened ();
    }

    public void StartSequence (int sequenceID) {
        GameObject step = (GameObject)Instantiate (startingSteps[sequenceID].gameObject);
        currentStep = step.GetComponent<SequenceStep> ();
        headerTitleText.text = currentStep.levelName;
        if (currentStep.startingCode.Length > 0)
            TextPanel.startingText = currentStep.startingCode;
    }

    public static void SetSequenceText (string text) {
        panels.headerSequenceText.text = text;
    }

    public static void OnShitHappened () {
        errorCount++;
        panels.errorCountText.text = errorCount + " / " + MAX_ERRS + " mistakes.";
        if (errorCount >= panels.yellowThreshold)
            panels.errorCountText.color = Color.yellow;
        if (errorCount >= panels.redTreshold)
            panels.errorCountText.color = Color.red;
        if (errorCount >= MAX_ERRS) {
            panels.errorCountText.text = "YOU ARE A FAILURE.";
        }
    }
}

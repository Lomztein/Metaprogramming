using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class AllPanels : MonoBehaviour {

    public static AllPanels panels;

    public RectTransform codeblockContainer;
    public RectTransform codeSeqenceContainer;

    public Text headerTitleText;
    public Text headerSequenceText;

	public Level level;
    public SequenceStep currentStep;

    public static int errorCount = -1;
    public const int MAX_ERRS = 3;
    public Text errorCountText;

    public int yellowThreshold = 1;
    public int redTreshold = 2;

    void Awake () {
		Level.dataPath = Application.dataPath + "/Levels/";
		Debug.Log (Level.dataPath);
        panels = this;
    }

    void Start () {
		Directory.CreateDirectory (Level.dataPath);
		StartLevel (LevelSelector.selectedLevel);
		TextPanel.GenerateText ();
        OnShitHappened ();
    }

	public void StartLevel (string filePath) {
		GameObject levelGo = CodeIO.LoadLevel (filePath);
		level = levelGo.GetComponent<Level>();

		currentStep = level.data.sequenceSteps[0];
		headerTitleText.text = level.data.levelName;
		if (level.data.startingCode.Length > 0)
			TextPanel.startingText = level.data.startingCode;

		level.LoadStep (currentStep);
    }

    public static void SetSequenceText (string text) {
        panels.headerSequenceText.text = text;
    }

	public static void OnWin () {
		Codeblock.CreateComment ("Congratulations, you've succesfully completed this level! Returning to level select in a few seconds..", AllPanels.panels.codeSeqenceContainer);
		errorCount = -1;
		panels.Invoke ("ReturnToLevelSelect", 5f);
	}

	void ReturnToLevelSelect () {
		SceneManager.LoadScene (0);
	}

    public static void OnShitHappened () {
        errorCount++;
        panels.errorCountText.text = errorCount + " / " + MAX_ERRS + " mistakes.";
        if (errorCount >= panels.yellowThreshold)
            panels.errorCountText.color = Color.yellow;
        if (errorCount >= panels.redTreshold)
            panels.errorCountText.color = Color.red;
        if (errorCount >= MAX_ERRS) {
            panels.errorCountText.text = "Too many mistakes.. returning..";
			panels.Invoke ("ReturnToLevelSelect", 5f);
        }
    }
}

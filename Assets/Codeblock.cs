using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Codeblock : MonoBehaviour {

    public CodeblockData data;

    public Text codeText;
    public Image background;
    public static float flashTime = 0.5f;

    private static GameObject _blockPrefab;
    public static GameObject blockPrefab {
        get {
            if (!_blockPrefab)
                blockPrefab = Resources.Load<GameObject> ("BlockPrefab");
            return _blockPrefab;
        }

        set {
            _blockPrefab = value;
        }
    }

    void Start () {
        codeText.text = data.codeDesc;
    }

    public static GameObject Create (CodeblockData data, Transform parentContainer) {
        GameObject newBlock = (GameObject)Instantiate (blockPrefab, parentContainer);
        newBlock.GetComponent<Codeblock> ().data = data;
        return newBlock;
    }

    public static GameObject CreateComment (string text, Transform parentContainer) {
        GameObject newBlock = (GameObject)Instantiate (blockPrefab, parentContainer);
		CodeblockData data = new CodeblockData ();

        data.code = "\n// " + text + "\n";
        data.codeDesc = "Comment.";

        newBlock.GetComponent<Codeblock> ().data = data;
        return newBlock;
    }

    public void OnClick () {
        if (data.correct) {
            transform.SetParent (AllPanels.panels.codeSeqenceContainer);
            Codeblock[] blocks = AllPanels.panels.codeSeqenceContainer.GetComponentsInChildren<Codeblock> ();
			AllPanels.panels.level.currentBlocks.Remove (this.gameObject);
            TextPanel.GenerateText (blocks);
            Destroy (GetComponent<Button> ());
            AllPanels.panels.level.OnAddedToSequence ();
        } else {
            StartCoroutine (FlashColor (Color.red));
            GetComponent<Button> ().interactable = false;
            AllPanels.OnShitHappened ();
        }
    }

    private IEnumerator FlashColor (Color color) {
        background.color = color;
        int iterations = Mathf.RoundToInt (flashTime / Time.fixedDeltaTime);
        float speed = Time.fixedDeltaTime / flashTime;

        while (iterations >= 0) {
            background.color = new Color (background.color.r + speed, background.color.g + speed, background.color.b + speed);
            iterations--;
            yield return new WaitForFixedUpdate ();
        }

    }
}

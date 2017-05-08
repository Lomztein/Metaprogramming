using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Level : MonoBehaviour {

	[System.Serializable]
	public class LevelData {
		
		public string levelName;
		[TextArea]
		public string startingCode;
		public List<SequenceStep> sequenceSteps;

	}

	public int currentIndex;
	public List<GameObject> currentBlocks;

	public LevelData data;

	public static string dataPath;

	public void LoadStep (SequenceStep step) {
		// Shuffle blovks
		List<CodeblockData> remaining = step.codeBlocks.ToList ();
		for (int i = 0; i < step.codeBlocks.Length; i++) {
			int index = Random.Range (0, remaining.Count);
			step.codeBlocks[i] = remaining[index];
			remaining.RemoveAt (index);
		}

		CreateBlocks (step);
		AllPanels.SetSequenceText (step.stepName);
	}

	public void CreateBlocks (SequenceStep step) {
		currentBlocks = new List<GameObject>();
		foreach (CodeblockData block in step.codeBlocks) {
			GameObject newBlock = Codeblock.Create (block, AllPanels.panels.codeblockContainer);
			currentBlocks.Add (newBlock);
		}
	}

	private void DestroyBlocks () {
		foreach (GameObject obj in currentBlocks) {
			GameObject.Destroy (obj);
		}
	}

	public void OnAddedToSequence () {
		bool anyCorrect = currentBlocks.Where (x => x.GetComponent<Codeblock>().data.correct).Count () != 0;
		if (anyCorrect)
			return;

		DestroyBlocks ();
		if (data.sequenceSteps.Count > currentIndex + 1) {
			AllPanels.panels.currentStep = data.sequenceSteps[currentIndex + 1];

			if (data.sequenceSteps[currentIndex].stepComment.Length > 0) {
				Codeblock.CreateComment (data.sequenceSteps[currentIndex].stepComment, AllPanels.panels.codeSeqenceContainer);
			}
			currentIndex++;
			LoadStep (data.sequenceSteps[currentIndex]);
		} else {
			AllPanels.OnWin ();
		}
	}
}

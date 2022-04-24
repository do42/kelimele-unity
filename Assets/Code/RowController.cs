using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowController : MonoBehaviour
{
	[SerializeField] private List<CellController> cells;
	public int CellAmount => cells.Count;
	[SerializeField] ContentController contentController;

	public void UpdateText(string msg)
	{
		var arrayChar = msg.ToCharArray();
		for (int i = 0; i < cells.Count; i++)
		{
			var cell = cells[i];

			bool isExist = i < arrayChar.Length;
			var content = isExist ? arrayChar[i] : ' ';
			cell.UpdateText(content);
		}
        if (!contentController.seriMod)
        {
			for (int l = 1; l <= 6; l++)
			{
				if (gameObject.tag == "Satir" + l && msg != "")
				{
					PlayerPrefs.SetString("Satir" + l, msg);


				}
			}
		}
		
	}

	public void UpdateState(List<State> states)
	{

		StartCoroutine(CellUpdateAnimation(states));
		
	}

	IEnumerator CellUpdateAnimation(List<State> states)
    {
		for (int i = 0; i < states.Count; i++)
		{
			var cell = cells[i];
			var state = states[i];
			cell.UpdateState(state);
			yield return new WaitForSeconds(0.5f);
		}
		
	}
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordManager : MonoBehaviour
{
	[SerializeField] private string origin;
	[SerializeField] private Words wordsScript;
	[SerializeField] KeyboardManager keyboardManager;
	public bool seriMod;


	private void Start(){


        if (seriMod)
        {
			origin = wordsScript.selectedWord;
        }
        else
        {
			origin = PlayerPrefs.GetString("TodaysWord");
        }
		
		print(origin);
	}


	

    



    public List<State> GetStates(string msg)
	{
		

		var result = new List<State>();

		var list = origin.ToCharArray().ToList();
		var listCurrent = msg.ToCharArray().ToList();
		var temporaryStr = msg;
		var correctStr = "";
		for (var i = 0; i < listCurrent.Count; i++)
		{
			bool repeated = false;
			var currentChar = listCurrent[i];
			var contains = list.Contains(currentChar);
			if (contains)
			{
				
				//var index = list.FindIndex(x => x == currentChar);
				var index = list[i];
				print(index+" "+listCurrent[i]);
				
				var isCorrect = index == listCurrent[i];
				


				for(int a = 0; a < 5; a++)
                {

					for (int j = 0; j < correctStr.Length; j++)
					{
						if (temporaryStr[i] == correctStr[j]) 
                        {
							repeated = true;
                        }

					}
					
					
                }


				//---
				if (isCorrect)
                {
					result.Add(State.Correct);
					keyboardManager.ButonuYesilYap(listCurrent[i].ToString());
					correctStr += listCurrent[i];
                }
                else if(!repeated)
                {

					
					

					result.Add(State.Contain);
					keyboardManager.ButonuSariYap(listCurrent[i].ToString());
                }
                else
                {
					result.Add(State.Fail);
					
				}
				//result.Add(isCorrect ? State.Correct : State.Contain);
				//--
			}
			else
			{
				result.Add(State.Fail);
				keyboardManager.ButonuGriYap(listCurrent[i].ToString());
			}

			

		}

		


		return result;
	}
}
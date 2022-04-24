using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using UnityEngine.UI;
using System.Collections;

public class ContentController : MonoBehaviour
{
	[SerializeField] private TMP_InputField inputField;
	[SerializeField] private Button temp;
	[SerializeField] private List<RowController> rows;
	[SerializeField] private WordManager wordManager;

	[SerializeField] Animator[] rowAnim;
	[SerializeField] GameManager gm;
	[SerializeField] Words words;
	public bool seriMod;

	public int deneme;

	private int _index;

	private void Start()
	{
		inputField.onValueChanged.AddListener(OnUpdateContent);
		inputField.onSubmit.AddListener(OnSubmit);
		deneme = 0;

		if (!seriMod && PlayerPrefs.GetInt("HasFinished") == 0)
		{
			StartCoroutine(BirazBekleVeGuncelle());
		}
	}

	IEnumerator BirazBekleVeGuncelle()
    {
		yield return new WaitForSecondsRealtime(0.25f);
		for (int l = 1; l <= 6; l++)
		{
			if (PlayerPrefs.GetString("Satir" + l).Length == 5)
			{
				print(PlayerPrefs.GetString("Satir" + l));
				inputField.text = (PlayerPrefs.GetString("Satir" + l));
				OnSubmit(inputField.text);

			}
		}
	}
	private void OnUpdateContent(string msg)
	{
		print(rows.Count+ " "+ _index );
		var row = rows[_index];
		row.UpdateText(msg);
	}

	private bool UpdateState()
	{
		var states = wordManager.GetStates(inputField.text);
		rows[_index].UpdateState(states);

		foreach (var state in states)
		{
			print(state);
			if (state != State.Correct)
				return false;
		}

		return true;
	}

	public void OnSubmit(string msg)
	{
		
		bool sent = false;
		for(int i = 0; i < words.wholewordsarray.Length; i++)
        {
			if(msg == words.wholewordsarray[i])
            {
				sent = true;
				temp.Select();

				inputField.Select();
				
				if (!IsEnough())
				{
					
					gm.HEADSUPBIATCH("Yetersiz harf");
					return;
				}

				var isWin = UpdateState();
				if (isWin)
				{
				
					
					rowAnim[deneme].SetBool("REVEAL", true);
					StartCoroutine(WinRoutine());
					
					gm.timeManager.SetNextPlay();
					return;
				}
				_index++;
				rowAnim[deneme].SetBool("REVEAL", true);
				var isLost = _index == rows.Count;
				if (isLost)
				{
					
					
					gm.timeManager.SetNextPlay();
					StartCoroutine(LoseRoutine());
					return;
				}
				
				deneme++;
				/*
				if (!seriMod && wordManager.seriMod == false)
				{
					Debug.Log("Triggered");
					for (int l = 1; l <= 6; l++)
					{
						Debug.Log("Triggere1d");
						if (gameObject.tag == "Satir" + l && wordManager.seriMod == false)
						{
							PlayerPrefs.SetString("Satir" + l, msg);

							Debug.Log("Triggered2:" + PlayerPrefs.GetString("Satir" + l, msg));
						}
					}
				}*/
				inputField.text = "";
            }
            else
            {
				if(i == words.wholewordsarray.Length - 1 && !sent)
                {
					gm.HEADSUPBIATCH("Geçersiz kelime");
				}
				

			}
        }

		
		
	}

	IEnumerator WinRoutine(){
        if (seriMod)
        {
			yield return new WaitForSeconds(2f);
			gm.HEADSUPBIATCH("Kazandınız!");
			Debug.Log("Kazandın...");
			PlayerPrefs.SetInt("TotalGames", PlayerPrefs.GetInt("TotalGames") + 1);
			PlayerPrefs.SetInt("CurrentStreak", PlayerPrefs.GetInt("CurrentStreak") + 1);

			if (PlayerPrefs.GetInt("CurrentStreak") >= PlayerPrefs.GetInt("BestStreak"))
			{
				PlayerPrefs.SetInt("BestStreak", PlayerPrefs.GetInt("CurrentStreak"));
			}

			PlayerPrefs.SetInt("ROW" + (deneme), PlayerPrefs.GetInt("ROW" + deneme) + 1);
			inputField.enabled = false;
			PlayerPrefs.SetInt("Won", PlayerPrefs.GetInt("Won") + 1);
			yield return new WaitForSeconds(2f);
			gm.GameOver_Won();
        }
        else
        {
			yield return new WaitForSeconds(2f);
			gm.HEADSUPBIATCH("Kazandınız!");
			Debug.Log("Kazandın...");
			PlayerPrefs.SetInt("TotalGamesT", PlayerPrefs.GetInt("TotalGamesT") + 1);
			PlayerPrefs.SetInt("CurrentStreakT", PlayerPrefs.GetInt("CurrentStreakT") + 1);

			if (PlayerPrefs.GetInt("CurrentStreakT") >= PlayerPrefs.GetInt("BestStreakT"))
			{
				PlayerPrefs.SetInt("BestStreakT", PlayerPrefs.GetInt("CurrentStreakT"));
			}
			PlayerPrefs.SetInt("HasFinished", 1);
			PlayerPrefs.SetInt("HasLoaded", 0);
			PlayerPrefs.SetInt("HasWon", 1);
			PlayerPrefs.SetInt("DenemeSayisi", deneme + 1);
			PlayerPrefs.SetInt("ROWT" + (deneme), PlayerPrefs.GetInt("ROWT" + deneme) + 1);
			inputField.enabled = false;
			PlayerPrefs.SetInt("WonT", PlayerPrefs.GetInt("WonT") + 1);
			yield return new WaitForSeconds(2f);
			gm.TODAYGameWON();
		}
			
	}

	IEnumerator LoseRoutine()
    {
        if (seriMod)
        {
			yield return new WaitForSeconds(2f);
			gm.HEADSUPBIATCH("Kaybettiniz");
			Debug.Log("Kaybettin!");
			PlayerPrefs.SetInt("CurrentStreak", 0);
			PlayerPrefs.SetInt("TotalGames", PlayerPrefs.GetInt("TotalGames") + 1);
			inputField.enabled = false;
			PlayerPrefs.SetInt("Lost", PlayerPrefs.GetInt("Lost") + 1);

			yield return new WaitForSeconds(2f);
			gm.GameOver_Lost();
        }
        else
        {
			yield return new WaitForSeconds(2f);
			gm.HEADSUPBIATCH("Kaybettiniz");
			Debug.Log("Kaybettin!");
			PlayerPrefs.SetInt("CurrentStreakT", 0);
			PlayerPrefs.SetInt("TotalGamesT", PlayerPrefs.GetInt("TotalGamesT") + 1);
			inputField.enabled = false;
			PlayerPrefs.SetInt("LostT", PlayerPrefs.GetInt("LostT") + 1);
			PlayerPrefs.SetInt("HasWon", 0);
			PlayerPrefs.SetInt("HasFinished", 1);
			PlayerPrefs.SetInt("HasLoaded", 0);
			yield return new WaitForSeconds(2f);
			gm.TODAYGameLOSSS();
		}
		
	}

	private bool IsEnough()
	{
		print(_index);
		return inputField.text.Length == rows[_index].CellAmount;
		
	}



}
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellController : MonoBehaviour
{
    [SerializeField] private Color colorCorrect;
    [SerializeField] private Color colorExist;
    [SerializeField] private Color colorFail;
    [SerializeField] private Color colorNone;
    Vector3 bas = new Vector3(1, 1, 1);
    Vector3 son = new Vector3(1.25f, 1.25f, 1.25f);
    Vector3 sonNokta = new Vector3(1.25f, 1.25f, 1.25f);
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private ColorManager colorManager;
    GameObject nokta;
    RectTransform cellImg;
    WordManager wordManager;


    

 
   

    private void Awake()
    {
        cellImg = text.gameObject.GetComponentInParent<RectTransform>();
        wordManager = GameObject.FindGameObjectWithTag("god").GetComponent<WordManager>();
        nokta = gameObject.transform.Find("Image").gameObject;
        

        colorCorrect = PlayerPrefsExtra.GetColor("CorrectColor");
        colorExist = PlayerPrefsExtra.GetColor("ContainsColor");


       

    }
    public void UpdateText(char msg)
    {
        string a = text.text;
        text.SetText(msg.ToString());
        if (a != text.text)
        {
            if (text.text == "i")
            {
                nokta.SetActive(true);
            }
            else
            {
                nokta.SetActive(false);
            }

            StartCoroutine(CellAnim());
        }



    }

    IEnumerator CellAnim()
    {

        float startTime = Time.unscaledTime;

        while (Time.unscaledTime < startTime + 0.15f)
        {
            if (text.text == "i")
            {
                nokta.transform.localScale = Vector3.Lerp(bas, sonNokta, Mathf.Lerp(0, 1, (Time.unscaledTime - startTime) / 0.15f));
            }

            cellImg.localScale = Vector3.Lerp(bas, son, Mathf.Lerp(0, 1, (Time.unscaledTime - startTime) / 0.15f));
            yield return null;
        }

        cellImg.localScale = son;
        StartCoroutine(CellAnim2());
    }
    IEnumerator CellAnim2()
    {
        float startTime = Time.unscaledTime;

        while (Time.unscaledTime < startTime + 0.15f)
        {
            if (text.text == "i")
            {
                nokta.transform.localScale = Vector3.Lerp(sonNokta, bas, Mathf.Lerp(0, 1, (Time.unscaledTime - startTime) / 0.15f));
            }

            cellImg.localScale = Vector3.Lerp(son, bas, Mathf.Lerp(0, 1, (Time.unscaledTime - startTime) / 0.15f));
            yield return null;
        }
        if (text.text == "i")
        {
            nokta.transform.localScale = bas;
        }
        cellImg.localScale = bas;

    }

    public void UpdateState(State state)
    {
        background.color = GetColor(state);

        if (!wordManager.seriMod)
        {
            if (state == State.Correct)
            {

                PlayerPrefs.SetString(gameObject.transform.parent.tag + gameObject.name, "yesil");
                return;
            }
            if (state == State.Contain)
            {
                PlayerPrefs.SetString(gameObject.transform.parent.tag + gameObject.name, "sari");
                return;
            }
            if (state == State.Fail)
            {
                PlayerPrefs.SetString(gameObject.transform.parent.tag + gameObject.name, "gri");
                return;
            }


        }

    }

    private Color GetColor(State state)
    {
        return state switch
        {
            State.Correct => colorCorrect,
            State.Fail => colorFail,
            State.None => colorNone,
            State.Contain => colorExist,

        };
    }




}

public enum State
{
    None,
    Contain,
    Correct,
    Fail
}
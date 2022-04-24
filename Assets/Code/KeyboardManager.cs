using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardManager : MonoBehaviour
{

    [SerializeField] TMP_InputField _InputField;
    [SerializeField] ContentController contentController;
    [SerializeField] Button[] keys;
    string harfler = "ertyuýopðüasdfghjklþizcvbnmöç";
    [SerializeField] Color32 sari, yesil, gri, basmaRengi;
    private Color32 primaryC, secondaryC;
    private void Awake()
    {
        primaryC = new Color(PlayerPrefsExtra.GetColor("CorrectColor").r, PlayerPrefsExtra.GetColor("CorrectColor").g, PlayerPrefsExtra.GetColor("CorrectColor").b, 0.5f);
        secondaryC = new Color( PlayerPrefsExtra.GetColor("ContainsColor").r, PlayerPrefsExtra.GetColor("ContainsColor").g, PlayerPrefsExtra.GetColor("ContainsColor").b,0.5f);

        DeclareButtons();
    }
    private void DeclareButtons()
    {

        keys[0] = gameObject.transform.Find("Button").GetComponent<Button>();
        for(int i = 1; i < 29; i++)
        {
            
            keys[i] = gameObject.transform.Find("Button (" + i + ")").gameObject.GetComponent<Button>();
        }
    }



    public void ButonuSariYap(string harf)
    {
        StartCoroutine(SariAnim(harf));
    }

    IEnumerator SariAnim(string harf)
    {
        yield return new WaitForSeconds(2.5f);
        for (int i = 0; i < 29; i++)
        {
            if (harf == harfler[i].ToString())
            {
                Image keyImg = keys[i].gameObject.GetComponent<Image>();
                if (keyImg.color != primaryC)
                {
                    keyImg.color = secondaryC;
                }

            }
        }
    }

    public void ButonuYesilYap(string harf)
    {
        StartCoroutine(YesilAnim(harf));
    }

    public void ButonuGriYap(string harf)
    {

        StartCoroutine(GriAnim(harf));
    }

    IEnumerator GriAnim(string harf)
    {
        yield return new WaitForSeconds(2.5f);
        for (int i = 0; i < 29; i++)
        {
            if (harf == harfler[i].ToString())
            {

                keys[i].gameObject.GetComponent<Image>().color = gri;
            }
        }
    }

    IEnumerator YesilAnim(string harf)
    {
        yield return new WaitForSeconds(2.5f);
        for (int i = 0; i < 29; i++)
        {
            if (harf == harfler[i].ToString())
            {
                keys[i].gameObject.GetComponent<Image>().color = primaryC;
            }
        }
    }


    public void ButonuBasýlýYap(string harf)
    {

        for (int i = 0; i < 29; i++)
        {
            if (harf == harfler[i].ToString())
            {
                Color32 oncekiColor =  keys[i].gameObject.GetComponent<Image>().color;
                print(oncekiColor);
                if(oncekiColor.a == 0)
                {
                    print("kkkk");
                    keys[i].gameObject.GetComponent<Image>().color = basmaRengi;
                }
               
            }
        }
    }

    public void ButonuNormalYap(string harf)
    {

        for (int i = 0; i < 29; i++)
        {
            if (harf == harfler[i].ToString())
            {

                keys[i].gameObject.GetComponent<Image>().color = new Color32(255,255,255,0);
            }
        }
    }
    IEnumerator BasmaRutini(string harf)
    {
        
        yield return new WaitForSeconds(0.1f);
        ButonuNormalYap(harf);
    }

    public void ButtonDown(string harf)
    {
        if(_InputField.text.Length < 5)
        {
            
            

                
            _InputField.text += harf;
        }



        for (int i = 0; i < 29; i++)
        {
            if (harf == harfler[i].ToString())
            {
                Color32 oncekiColor = keys[i].gameObject.GetComponent<Image>().color;
               
                if (!oncekiColor.Equals(primaryC) && !oncekiColor.Equals(secondaryC) && !oncekiColor.Equals(gri))
                {
                    
                    keys[i].gameObject.GetComponent<Image>().color = basmaRengi;
                    StartCoroutine(BasmaRutini(harf));
                }

            }
        }
        

    }

    public void RemoveLetter()
    {
        
        string value = _InputField.text;
        if(value.Length > 0)
        {
            _InputField.text = value.Substring(0, value.Length - 1);
        }
        
    }

    public void Enter()
    {
        contentController.OnSubmit(_InputField.text);
    }
}

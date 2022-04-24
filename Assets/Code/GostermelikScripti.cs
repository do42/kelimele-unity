using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GostermelikScripti : MonoBehaviour
{

    [SerializeField] ColorManager colorManager;
    Color32 sari = new Color32(226,206,48,255), yesil = new Color32(39,171,30,255), siyah = new Color32(91,91,91,255); 
    [SerializeField] int satir, cell;
    void Start()
    {
                cell = cell - 1;

               
        


        if (PlayerPrefs.GetString("Satir" + (satir + 1) + "Cell (" + cell + ")") == "sari")
                {
                    gameObject.GetComponent<Image>().color = PlayerPrefsExtra.GetColor("ContainsColor");
                }
                else if (PlayerPrefs.GetString("Satir" + (satir + 1) + "Cell (" + cell + ")") == "yesil")
                {
                    gameObject.GetComponent<Image>().color = PlayerPrefsExtra.GetColor("CorrectColor");
                }
                else if (PlayerPrefs.GetString("Satir" + (satir + 1) + "Cell (" + cell + ")") == "gri" || PlayerPrefs.GetString("Satir" + (satir + 1) + "Cell (" + cell + ")") == "" || !PlayerPrefs.HasKey("Satir" + (satir + 1) + "Cell (" + cell + ")"))
                {
                    gameObject.GetComponent<Image>().color = siyah;
                }

          


    }


    
}

    


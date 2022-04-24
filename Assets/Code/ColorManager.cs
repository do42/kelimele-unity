using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public Color32 defaultYesil, defaultSari, defaultGri, turuncu, mavi, mor, kirmizi, kahverengi, goodYesil, goodSari;
    [SerializeField] GameObject colorCerceve1, colorCerceve2, colorCerceve3, colorCerceve4;



    private void Start()
    {
        if(PlayerPrefsExtra.GetColor("CorrectColor") == defaultYesil)
        {
            colorCerceve1.SetActive(true);
            colorCerceve2.SetActive(false);
            colorCerceve3.SetActive(false);
            colorCerceve4.SetActive(false);
        }else if (PlayerPrefsExtra.GetColor("CorrectColor") == goodYesil)
        {
            colorCerceve1.SetActive(false);
            colorCerceve2.SetActive(true);
            colorCerceve3.SetActive(false);
            colorCerceve4.SetActive(false);

        } else if (PlayerPrefsExtra.GetColor("CorrectColor") == kirmizi)
        {
            colorCerceve1.SetActive(false);
            colorCerceve2.SetActive(false);
            colorCerceve3.SetActive(true);
            colorCerceve4.SetActive(false);
        }
        else
        {
            colorCerceve1.SetActive(false);
            colorCerceve2.SetActive(false);
            colorCerceve3.SetActive(false);
            colorCerceve4.SetActive(true);
        }
    }



    public void SetColorCouple(int sayi) // defaultYesil, mavi, goodYesil, kirmizi
    {
        Color32 renk;
        if(sayi == 1)
        {
            renk = defaultYesil;
            colorCerceve1.SetActive(true);
            colorCerceve2.SetActive(false);
            colorCerceve3.SetActive(false);
            colorCerceve4.SetActive(false);
        }
        else if(sayi == 2)
        {
            renk = goodYesil;
            colorCerceve1.SetActive(false);
            colorCerceve2.SetActive(true);
            colorCerceve3.SetActive(false);
            colorCerceve4.SetActive(false);
        }
        else if(sayi == 3)
        {
            renk = kirmizi;
            colorCerceve1.SetActive(false);
            colorCerceve2.SetActive(false);
            colorCerceve3.SetActive(true);
            colorCerceve4.SetActive(false);
        }
        else
        {
            colorCerceve1.SetActive(false);
            colorCerceve2.SetActive(false);
            colorCerceve3.SetActive(false);
            colorCerceve4.SetActive(true);
            renk = mavi;
        }


        Color32 containsRenk;
        if (sayi == 1)
        {
            containsRenk = defaultSari;
        }
        else if (sayi == 2)
        {
            containsRenk = goodSari;
        }
        else if (sayi == 3)
        {
            containsRenk = turuncu;
        }
        else
        {
            containsRenk = mor;
        }
     

        PlayerPrefsExtra.SetColor("ContainsColor", containsRenk);

        PlayerPrefsExtra.SetColor("CorrectColor", renk);
        
      
    }
    


    
}

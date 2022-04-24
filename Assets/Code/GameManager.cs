using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject seriPanel, anaMenuCanvas, oyunMenu1Canvas, oyunMenu2Canvas, statsPanel, colorsPanel;
    [SerializeField] private Animator seriAnim, anaMenuAnim, infoAnim, statsAnim, colorsPanelAnim, rateboxAnim;
    public TimeManager timeManager;
    [SerializeField] private TMP_Text headsUpText, countdownOrAffirmative;
    [SerializeField] TMP_Text dateText;
    [SerializeField] GameObject keyboard, endGameLayout;
    [SerializeField] WordManager wordManager;
    [SerializeField] Button GunlukStatsButonu, SeriStatsButonu, startButton;
    [SerializeField] GameObject GameOverMenu, infoPanel, rateBoxPanel;
    [SerializeField] ColorManager colorManager;
    [SerializeField] TMP_Text startBtnTexti, kelimeaviNOtext, altidakactext,kelimeneyditext;
	[SerializeField] Image wonOrLossImage;
    
    [SerializeField] TMP_Text kelimeNeydi, oynanOyunText, kazanmaYuzdesiText, suankiSeriText, enIyiSeriTxt, no0txt, no1txt, no2txt, no3txt, no4txt, no5txt, NTXT0, NTXT1, NTXT2, NTXT3, NTXT4, NTXT5, GAMECOUNT, WINRATE, STREAKCURRENT, STREAKMAX;
    [SerializeField] Sprite win, loss;
    [SerializeField] ContentController contentController;
    [SerializeField] Slider no0,no1,no2,no3,no4,no5,  N0,N1,N2,N3,N4,N5;
  
    [SerializeField] TMP_Text  promudegilmiText;
  
    
    float timer = 1f;
    bool preview;
    [SerializeField] Words words;
    [SerializeField] TMP_Text situationTextONbuton;
    private void Awake() {


        




        if(!PlayerPrefs.HasKey("Won")){

            

            PlayerPrefs.SetInt("Won", 0);
            
           
        }

        if(!PlayerPrefs.HasKey("Premium")){
            PlayerPrefs.SetInt("Premium", 0);
        }

        if(!PlayerPrefs.HasKey("Gold")){
            PlayerPrefs.SetInt("Gold", 0);
        }

        if(!PlayerPrefs.HasKey("CurrentStreak")){
            PlayerPrefs.SetInt("CurrentStreak", 0);
        }

        if(!PlayerPrefs.HasKey("BestStreak")){
            PlayerPrefs.SetInt("BestStreak", 0);
        }

        if(!PlayerPrefs.HasKey("TotalGames")){
            PlayerPrefs.SetInt("TotalGames", 0);
        }





        if(!PlayerPrefs.HasKey("Energy")){
            PlayerPrefs.SetInt("Energy", 1);
        }


        if (!PlayerPrefs.HasKey("Rate"))
        {
            PlayerPrefs.SetInt("Rate", 0);
        }

        if(!PlayerPrefs.HasKey("CanPlay")){
            PlayerPrefs.SetInt("CanPlay", 1);
        }


        if(PlayerPrefs.HasKey("HasLoaded") == false)
        {
            PlayerPrefs.SetInt("HasLoaded", 0);
        }
        if (PlayerPrefs.HasKey("HasFinished") == false)
        {
            PlayerPrefs.SetInt("HasFinished", 0);
        }

        for(int i = 1; i <= 6; i++)
        {
            if (!PlayerPrefs.HasKey("Satir"+i))
            {
                PlayerPrefs.SetString("Satir"+i, "");
            }
        }

        

        if (PlayerPrefsExtra.GetColor("CorrectColor") == new Color32(0,0,0,0))
        {
            PlayerPrefsExtra.SetColor("CorrectColor", new Color32(39, 171, 30, 255));
        }

        if (PlayerPrefsExtra.GetColor("ContainsColor") == new Color32(0, 0, 0, 0))
        {
            PlayerPrefsExtra.SetColor("ContainsColor", new Color32(226, 206, 48, 255));
        }

    }

    

    private void Start() {
       


        if(PlayerPrefs.GetInt("HasWon")==1 && PlayerPrefs.GetInt("HasFinished") == 1)
        {
            situationTextONbuton.text = "Kazandın";
        }else if(PlayerPrefs.GetInt("HasWon") == 0 && PlayerPrefs.GetInt("HasFinished") == 1)
        {
            situationTextONbuton.text = "Kaybettin";
        }else if (PlayerPrefs.GetInt("HasFinished") == 0)
        {
            situationTextONbuton.text = "Devam ediyor";
        }
        
        if(PlayerPrefs.GetInt("Premium") == 0){
        
            if (PlayerPrefs.GetInt("HasFinished") == 1){
                
                    
                    startBtnTexti.text = "Bugünün kelimesinde ne yaptığını gör";
            }else{
                    
                    startBtnTexti.text = "Bugünün kelimesini hemen oyna";
            }
            promudegilmiText.text = "Artık Açık Kaynak!";
        }
        else
        {
            promudegilmiText.text = "Artık Açık Kaynak!";
        }

        startButton.interactable = false;
     






        if(PlayerPrefs.GetInt("Rate") % 3 == 0 && PlayerPrefs.GetInt("Rate") < 99 && PlayerPrefs.GetInt("Rate") > 0)
        {
            ShowRateBox();
        }

    }

    
    void ShowRateBox()
    {
        rateBoxPanel.SetActive(true);
    }
    
    
    IEnumerator CloseRateRoutine()
    {
        rateboxAnim.SetBool("Out", true);
        yield return new WaitForSeconds(0.35f);
        rateBoxPanel.SetActive(false);
    }

    void CloseRateBox()
    {
        StartCoroutine(CloseRateRoutine());
    }


     IEnumerator ClosePanelRoutine_Seri(){
        seriAnim.SetBool("OutC", true);
        yield return new WaitForSeconds(0.35f);
        seriPanel.SetActive(false);
    }
    public void CloseSeriPanel(){
        StartCoroutine(ClosePanelRoutine_Seri());
    }

    public void OpenSeriPanel(){
        seriPanel.SetActive(true);
        seriAnim.SetBool("OutC", false);
    }

    public void CloseColorsPanel()
    {
        StartCoroutine(CloseColorsPanelRoutine());
    }
    IEnumerator CloseColorsPanelRoutine()
    {
        colorsPanelAnim.SetBool("Out", true);
        yield return new WaitForSeconds(0.35f);
        colorsPanel.SetActive(false);
    }

    public void OpenColorsPanel()
    {
        colorsPanel.SetActive(true);
        colorsPanelAnim.SetBool("Out", false);
    }
    

    public void StartTheGame(){


        if(PlayerPrefs.GetInt("HasFinished") == 0)
        {
            wordManager.seriMod = false;
            words.seriMod = false;
            contentController.seriMod = false;


            for(int i = 1;i<= 6; i++)
            {
                for(int k = 0; k <= 4; k++)
                {
                    
                    PlayerPrefs.SetString("Satir" + i + "Cell (" + k + ")", "");
                }
            }

            words.ArrayHazirlama();
            PlayerPrefs.SetString("TodaysWord", words.AllWordsArray[words.SelectNewWorldForNewDay(timeManager.DayOfToday(), timeManager.MonthOfToday())]);
            StartCoroutine(OyunaBaslaRutini());
        }
        else
        {
            print(PlayerPrefs.GetInt("DenemeSayisi"));
            wordManager.seriMod = false;
            words.seriMod = false;
            contentController.seriMod = false;
            GameOverMenu.SetActive(false);
            keyboard.SetActive(false);
            endGameLayout.SetActive(true);
            
            if (PlayerPrefs.GetInt("HasWon") == 0)
            {
                altidakactext.text = "X/6";
            }
            else
            {
                altidakactext.text = PlayerPrefs.GetInt("DenemeSayisi") + "/6";
            }
            
            kelimeaviNOtext.text = "#kelimele " + words.SelectNewWorldForNewDay(timeManager.DayOfToday(), timeManager.MonthOfToday());
            kelimeneyditext.text = PlayerPrefs.GetString("TodaysWord");
            StartCoroutine(OyunaBaslaRutini());
        }
           
        


    }

    IEnumerator OyunaBaslaRutini(){

        




            
            oyunMenu1Canvas.SetActive(true);
            oyunMenu2Canvas.SetActive(true);
            anaMenuAnim.SetBool("AnaMenuOut", true);
            yield return new WaitForSeconds(0.4f);
            anaMenuCanvas.SetActive(false);

            
            
           
        
       
        

    }
    
    IEnumerator OyunaBaslaRoutinePassMain()
    {
        contentController.seriMod = true;
        words.seriMod = true;
        wordManager.seriMod = true;
        oyunMenu1Canvas.SetActive(true);
        oyunMenu2Canvas.SetActive(true);
        anaMenuAnim.SetBool("AnaMenuOut", true);
        yield return new WaitForSeconds(0.4f);
        anaMenuCanvas.SetActive(false);
    }

    IEnumerator OyunaBaslaRoutinePassSeriMod()
    {
        contentController.seriMod = true;
        wordManager.seriMod = true;
        words.seriMod = true;
        seriAnim.SetBool("OutC", true);
        yield return new WaitForSeconds(0.55f);
        seriPanel.SetActive(false);
        StartCoroutine(OyunaBaslaRoutinePassMain());
    }

    public void SeriMod()
    {
       
        if (PlayerPrefs.GetInt("Premium") == 0)
        {
            OpenSeriPanel();
        }
        else
        {

            StartCoroutine(OyunaBaslaRoutinePassMain());
        }

    }
   

   

    public void HemenSeriyeGonder()
    {
        StartCoroutine(OyunaBaslaRoutinePassSeriMod());
    }






    void Update()
    {
        if (timeManager.timeUpdated)
        {
            dateText.text = timeManager.dateTime;
        }
        else
        {
            dateText.text = "İnternet bekleniyor, lütfen bekleyiniz";
        }
        

        if (timeManager.timeUpdated && !startButton.IsInteractable())
        {
            startButton.interactable = true;
        }

        if (PlayerPrefs.GetInt("HasFinished") == 1)
        {
            countdownOrAffirmative.text = timeManager.timeLeft;
        }
        else
        {
            countdownOrAffirmative.text = "Günlük kelime hazır";
        }



        if (Input.GetKeyDown(KeyCode.Escape) && anaMenuCanvas.activeInHierarchy && !infoPanel.activeInHierarchy && !seriPanel.activeInHierarchy && !statsPanel.activeInHierarchy)
        {

            // Quit the application
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && infoPanel.activeInHierarchy)
        {

            LeaveInfoBtn();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && seriPanel.activeInHierarchy)
        {
            CloseSeriPanel();
           
        }
        if (Input.GetKeyDown(KeyCode.Escape) && statsPanel.activeInHierarchy)
        {

            StatsKapa();
        }

       
        if (Input.GetKeyDown(KeyCode.Escape) && !anaMenuCanvas.activeInHierarchy && oyunMenu1Canvas.activeInHierarchy && oyunMenu2Canvas.activeInHierarchy)
        {
            GoBackToMainMenu();

        }

        if (Input.GetKeyDown(KeyCode.Escape) && colorsPanel.activeInHierarchy)
        {
            CloseColorsPanel();

        }

        if (Input.GetKeyDown(KeyCode.Escape) && rateBoxPanel.activeInHierarchy)
        {
            RateBox_MaybeLater();

        }

    }



    public void InfoBTN()
    {
        infoPanel.SetActive(true);
        
    }

    public void LeaveInfoBtn()
    {
        StartCoroutine(InfoCloseRt());
    }


    IEnumerator InfoCloseRt()
    {
        infoAnim.SetBool("SlideOut", true);
        yield return new WaitForSeconds(0.35f);
        infoPanel.SetActive(false);
    }

    public void GameOver_Lost(){
        GameOverMenu.SetActive(true);
        StatslariGuncelleOyunSonu(true);
        wonOrLossImage.sprite = loss;
        kelimeNeydi.text = "Cevap: "+ words.selectedWord;
        PlayerPrefs.SetInt("Rate", PlayerPrefs.GetInt("Rate") + 1);
    }

    public void GameOver_Won(){
        GameOverMenu.SetActive(true);
        StatslariGuncelleOyunSonu(true);
        wonOrLossImage.sprite = win;
        kelimeNeydi.text = "Cevap: "+ words.selectedWord;
        PlayerPrefs.SetInt("Rate", PlayerPrefs.GetInt("Rate") + 1);
    }


    public void TODAYGameWON()
    {
        GameOverMenu.SetActive(true);
        StatslariGuncelleOyunSonu(false);
        wonOrLossImage.sprite = win;
        kelimeNeydi.text = "Cevap: " + PlayerPrefs.GetString("TodaysWord");
        PlayerPrefs.SetInt("Rate", PlayerPrefs.GetInt("Rate") + 1);

    }

    public void TODAYGameLOSSS()
    {
        GameOverMenu.SetActive(true);
        StatslariGuncelleOyunSonu(false);
        wonOrLossImage.sprite = loss;
        kelimeNeydi.text = "Cevap: " + PlayerPrefs.GetString("TodaysWord");
        PlayerPrefs.SetInt("Rate", PlayerPrefs.GetInt("Rate") + 1);
    }


    public void Stats_GunlukBTN() // bunlara efekt ekle
    {
        GAMECOUNT.text = PlayerPrefs.GetInt("TotalGamesT").ToString();
        if (PlayerPrefs.GetInt("TotalGamesT") > 0)
        {
            WINRATE.text = (PlayerPrefs.GetInt("WonT") * 100 / PlayerPrefs.GetInt("TotalGamesT")).ToString();
        }
        else
        {
            WINRATE.text = "0";
        }

        STREAKCURRENT.text = PlayerPrefs.GetInt("CurrentStreakT").ToString();
        STREAKMAX.text = PlayerPrefs.GetInt("BestStreakT").ToString();

        N0.maxValue = PlayerPrefs.GetInt("WonT");
        N1.maxValue = PlayerPrefs.GetInt("WonT");
        N2.maxValue = PlayerPrefs.GetInt("WonT");
        N3.maxValue = PlayerPrefs.GetInt("WonT");
        N4.maxValue = PlayerPrefs.GetInt("WonT");
        N5.maxValue = PlayerPrefs.GetInt("WonT");


        NTXT0.text = PlayerPrefs.GetInt("ROWT0").ToString();
        NTXT1.text = PlayerPrefs.GetInt("ROWT1").ToString();
        NTXT2.text = PlayerPrefs.GetInt("ROWT2").ToString();
        NTXT3.text = PlayerPrefs.GetInt("ROWT3").ToString();
        NTXT4.text = PlayerPrefs.GetInt("ROWT4").ToString();
        NTXT5.text = PlayerPrefs.GetInt("ROWT5").ToString();

        N0.value = PlayerPrefs.GetInt("ROWT0");
        N1.value = PlayerPrefs.GetInt("ROWT1");
        N2.value = PlayerPrefs.GetInt("ROWT2");
        N3.value = PlayerPrefs.GetInt("ROWT3");
        N4.value = PlayerPrefs.GetInt("ROWT4");
        N5.value = PlayerPrefs.GetInt("ROWT5");
        GunlukStatsButonu.interactable = false;
        SeriStatsButonu.interactable = true;
    }

    public void Stats_SeriBtn() // bunlara efekt ekle
    {
        GAMECOUNT.text = PlayerPrefs.GetInt("TotalGames").ToString();
        if (PlayerPrefs.GetInt("TotalGames") > 0)
        {
            WINRATE.text = (PlayerPrefs.GetInt("Won") * 100 / PlayerPrefs.GetInt("TotalGames")).ToString();
        }
        else
        {
            WINRATE.text = "0";
        }

        STREAKCURRENT.text = PlayerPrefs.GetInt("CurrentStreak").ToString();
        STREAKMAX.text = PlayerPrefs.GetInt("BestStreak").ToString();

        N0.maxValue = PlayerPrefs.GetInt("Won");
        N1.maxValue = PlayerPrefs.GetInt("Won");
        N2.maxValue = PlayerPrefs.GetInt("Won");
        N3.maxValue = PlayerPrefs.GetInt("Won");
        N4.maxValue = PlayerPrefs.GetInt("Won");
        N5.maxValue = PlayerPrefs.GetInt("Won");


        NTXT0.text = PlayerPrefs.GetInt("ROW0").ToString();
        NTXT1.text = PlayerPrefs.GetInt("ROW1").ToString();
        NTXT2.text = PlayerPrefs.GetInt("ROW2").ToString();
        NTXT3.text = PlayerPrefs.GetInt("ROW3").ToString();
        NTXT4.text = PlayerPrefs.GetInt("ROW4").ToString();
        NTXT5.text = PlayerPrefs.GetInt("ROW5").ToString();

        N0.value = PlayerPrefs.GetInt("ROW0");
        N1.value = PlayerPrefs.GetInt("ROW1");
        N2.value = PlayerPrefs.GetInt("ROW2");
        N3.value = PlayerPrefs.GetInt("ROW3");
        N4.value = PlayerPrefs.GetInt("ROW4");
        N5.value = PlayerPrefs.GetInt("ROW5");

        GunlukStatsButonu.interactable = true;
        SeriStatsButonu.interactable = false;
    }


    private void StatslariGuncelleOyunSonu(bool seriMod){
        if (seriMod)
        {
            oynanOyunText.text = PlayerPrefs.GetInt("TotalGames").ToString();
            kazanmaYuzdesiText.text = (PlayerPrefs.GetInt("Won") * 100 / PlayerPrefs.GetInt("TotalGames")).ToString();
            suankiSeriText.text = PlayerPrefs.GetInt("CurrentStreak").ToString();
            enIyiSeriTxt.text = PlayerPrefs.GetInt("BestStreak").ToString();

            no0.maxValue = PlayerPrefs.GetInt("Won");
            no1.maxValue = PlayerPrefs.GetInt("Won");
            no2.maxValue = PlayerPrefs.GetInt("Won");
            no3.maxValue = PlayerPrefs.GetInt("Won");
            no4.maxValue = PlayerPrefs.GetInt("Won");
            no5.maxValue = PlayerPrefs.GetInt("Won");


            no0txt.text = PlayerPrefs.GetInt("ROW0").ToString();
            no1txt.text = PlayerPrefs.GetInt("ROW1").ToString();
            no2txt.text = PlayerPrefs.GetInt("ROW2").ToString();
            no3txt.text = PlayerPrefs.GetInt("ROW3").ToString();
            no4txt.text = PlayerPrefs.GetInt("ROW4").ToString();
            no5txt.text = PlayerPrefs.GetInt("ROW5").ToString();

            no0.value = PlayerPrefs.GetInt("ROW0");
            no1.value = PlayerPrefs.GetInt("ROW1");
            no2.value = PlayerPrefs.GetInt("ROW2");
            no3.value = PlayerPrefs.GetInt("ROW3");
            no4.value = PlayerPrefs.GetInt("ROW4");
            no5.value = PlayerPrefs.GetInt("ROW5");
        }
        else
        {
            oynanOyunText.text = PlayerPrefs.GetInt("TotalGamesT").ToString();
            kazanmaYuzdesiText.text = (PlayerPrefs.GetInt("WonT") * 100 / PlayerPrefs.GetInt("TotalGamesT")).ToString();
            suankiSeriText.text = PlayerPrefs.GetInt("CurrentStreakT").ToString();
            enIyiSeriTxt.text = PlayerPrefs.GetInt("BestStreakT").ToString();

            no0.maxValue = PlayerPrefs.GetInt("WonT");
            no1.maxValue = PlayerPrefs.GetInt("WonT");
            no2.maxValue = PlayerPrefs.GetInt("WonT");
            no3.maxValue = PlayerPrefs.GetInt("WonT");
            no4.maxValue = PlayerPrefs.GetInt("WonT");
            no5.maxValue = PlayerPrefs.GetInt("WonT");


            no0txt.text = PlayerPrefs.GetInt("ROWT0").ToString();
            no1txt.text = PlayerPrefs.GetInt("ROWT1").ToString();
            no2txt.text = PlayerPrefs.GetInt("ROWT2").ToString();
            no3txt.text = PlayerPrefs.GetInt("ROWT3").ToString();
            no4txt.text = PlayerPrefs.GetInt("ROWT4").ToString();
            no5txt.text = PlayerPrefs.GetInt("ROWT5").ToString();

            no0.value = PlayerPrefs.GetInt("ROWT0");
            no1.value = PlayerPrefs.GetInt("ROWT1");
            no2.value = PlayerPrefs.GetInt("ROWT2");
            no3.value = PlayerPrefs.GetInt("ROWT3");
            no4.value = PlayerPrefs.GetInt("ROWT4");
            no5.value = PlayerPrefs.GetInt("ROWT5");
        }
        


        
    }


    public void StatsAcBTN()
    {
        statsPanel.SetActive(true);
        InfoPanelVerileriGuncelle();
        
    }

    public void StatsKapa()
    {
        StartCoroutine(StatsKapat());
    }

    IEnumerator StatsKapat()
    {
        statsAnim.SetBool("StatsOut", true);
        yield return new WaitForSeconds(0.35f);
        statsPanel.SetActive(false);
    }



    public void InfoPanelVerileriGuncelle()
    {
        GAMECOUNT.text = PlayerPrefs.GetInt("TotalGamesT").ToString();
        if (PlayerPrefs.GetInt("TotalGamesT") > 0)
        {
            WINRATE.text = (PlayerPrefs.GetInt("WonT") * 100 / PlayerPrefs.GetInt("TotalGamesT")).ToString();
        }
        else
        {
            WINRATE.text = "0";
        }

        STREAKCURRENT.text = PlayerPrefs.GetInt("CurrentStreakT").ToString();
        STREAKMAX.text = PlayerPrefs.GetInt("BestStreakT").ToString();

        N0.maxValue = PlayerPrefs.GetInt("WonT");
        N1.maxValue = PlayerPrefs.GetInt("WonT");
        N2.maxValue = PlayerPrefs.GetInt("WonT");
        N3.maxValue = PlayerPrefs.GetInt("WonT");
        N4.maxValue = PlayerPrefs.GetInt("WonT");
        N5.maxValue = PlayerPrefs.GetInt("WonT");


        NTXT0.text = PlayerPrefs.GetInt("ROWT0").ToString();
        NTXT1.text = PlayerPrefs.GetInt("ROWT1").ToString();
        NTXT2.text = PlayerPrefs.GetInt("ROWT2").ToString();
        NTXT3.text = PlayerPrefs.GetInt("ROWT3").ToString();
        NTXT4.text = PlayerPrefs.GetInt("ROWT4").ToString();
        NTXT5.text = PlayerPrefs.GetInt("ROWT5").ToString();

        N0.value = PlayerPrefs.GetInt("ROWT0");
        N1.value = PlayerPrefs.GetInt("ROWT1");
        N2.value = PlayerPrefs.GetInt("ROWT2");
        N3.value = PlayerPrefs.GetInt("ROWT3");
        N4.value = PlayerPrefs.GetInt("ROWT4");
        N5.value = PlayerPrefs.GetInt("ROWT5");
        GunlukStatsButonu.interactable = false;
        SeriStatsButonu.interactable = true;
    }

    public void DevamEtBTN(){
        if (words.seriMod)
        {
            SceneManager.LoadScene(0);
        }
        else
        {

            

            GameOverMenu.SetActive(false);
            keyboard.SetActive(false);
            endGameLayout.SetActive(true);
            if (PlayerPrefs.GetInt("HasWon") == 0)
            {
                altidakactext.text = "X/6";
            }
            else
            {
                altidakactext.text = PlayerPrefs.GetInt("DenemeSayisi") + "/6";
            }
            
            kelimeaviNOtext.text = "#kelimele " + words.SelectNewWorldForNewDay(timeManager.DayOfToday(), timeManager.MonthOfToday());
            kelimeneyditext.text = PlayerPrefs.GetString("TodaysWord");


        }  
        
    }


    public void BuyPremiumBTN(){
        PlayerPrefs.SetInt("Premium", 1);
        SceneManager.LoadScene(0);
    }



    public void HEADSUPBIATCH(string _Txt)
    {
        StartCoroutine(Appear(_Txt));
    }


    public void GO_TO_GITHUB_BTN()
    {
        Application.OpenURL("https://github.com/do42/kelimele-unity");
    }


    IEnumerator Disappear()
    {
        
        float startTime = Time.unscaledTime;
        while (Time.unscaledTime < startTime + 1f)
        {
            headsUpText.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, (Time.unscaledTime - startTime) / 1f));
            yield return null;
        }

        headsUpText.color = new Color(1, 1, 1, 0);
        headsUpText.gameObject.SetActive(false);
    }

    IEnumerator Appear(string _text)
    {
        headsUpText.gameObject.SetActive(true);
        headsUpText.text = _text;
        float startTime = Time.unscaledTime;
        while (Time.unscaledTime < startTime + 0.5f)
        {
            headsUpText.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, (Time.unscaledTime - startTime) / 0.5f));
            yield return null;
        }

        headsUpText.color = new Color(1, 1, 1, 1);
        StartCoroutine(Disappear());
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Share()
    {
      
        string morEmoji = "🟪";
        string turuncuEmoji = "🟧";
        string maviEmoji = "🟦";
        string kirmiziEmoji = "🟥";
        string sariEmoji = "🟨";
        string yesilEmoji = "🟩";
        string siyahEmoji = "⬛️";
        string myRun = "";
        string kactaKac;

        if (PlayerPrefs.GetInt("HasWon") == 0)
        {
            kactaKac = "X/6";
        }
        else
        {
            kactaKac = PlayerPrefs.GetInt("DenemeSayisi") + "/6";
        }


        string SecilenPrimryEmoji;
        string SecilenSecondaryEmoji;


        
        if(PlayerPrefsExtra.GetColor("CorrectColor") == colorManager.defaultYesil || PlayerPrefsExtra.GetColor("CorrectColor") == colorManager.goodYesil)
        {
            SecilenPrimryEmoji = yesilEmoji;
            SecilenSecondaryEmoji = sariEmoji;
        }else if(PlayerPrefsExtra.GetColor("CorrectColor") == colorManager.mavi)
        {
            SecilenPrimryEmoji = maviEmoji;
            SecilenSecondaryEmoji = morEmoji;
        }
        else
        {
            SecilenPrimryEmoji = kirmiziEmoji;
            SecilenSecondaryEmoji = turuncuEmoji;
        }


        for (int i = 0; i < 6; i++)
        {
            
            for(int k = 0; k < 5;k++) {
                if(PlayerPrefs.GetString("Satir"+(i+1)+"Cell ("+k+")") == "sari")
                {
                    myRun += SecilenSecondaryEmoji;
                }
                else if (PlayerPrefs.GetString("Satir" + (i + 1) + "Cell (" + k + ")") == "yesil")
                {
                    myRun += SecilenPrimryEmoji;
                }
                else if(PlayerPrefs.GetString("Satir" + (i + 1) + "Cell (" + k + ")") == "gri")
                {
                    myRun += siyahEmoji;
                }

            }
            myRun += "\n";
        }

        new NativeShare().SetText("#kelimele " + words.SelectNewWorldForNewDay(timeManager.DayOfToday(), timeManager.MonthOfToday()) + "\n" + kactaKac + "\n" + myRun).Share();
        
    }



    public void RateBox_NoThanks()
    {
        PlayerPrefs.SetInt("Rate", 101);
        CloseRateBox();
    }

    public void RateBox_MaybeLater()
    {
        PlayerPrefs.SetInt("Rate", 0);
        CloseRateBox();
    }

    public void RateBox_OKRATE()
    {
        PlayerPrefs.SetInt("Rate", 101);
        CloseRateBox();
        Application.OpenURL("market://details?id=com.zinedstudios.kelimele");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TimeManager : MonoBehaviour
{
    
    public String timeLeft;
    public DateTime currentTime;
    public DateTime lastPlayedTime;
    private WordTimeApi wordTimeApi;
    public string dateTime;
    DateTime ParsedTime;
    public bool timeUpdated = false;


   

    private void Start()
    {
        wordTimeApi = GameObject.Find("WorldTimeObject").GetComponent<WordTimeApi>();
        timeUpdated = false;
    }

    public void SetNextPlay(){
        DateTime nextPuzzle;
        DateTime nextPuzzleDecoy;
   


        nextPuzzleDecoy = ParsedTime.AddDays(1);


        
        
        nextPuzzle = new DateTime(nextPuzzleDecoy.Year, nextPuzzleDecoy.Month, nextPuzzleDecoy.Day, 0, 0, 0);

        

        PlayerPrefs.SetString("NextTime", nextPuzzle.ToBinary().ToString()); 
       
    }

    

    public void CalculateNextPlayableTime(){
        var nextTime = Convert.ToInt64(PlayerPrefs.GetString("NextTime"));
        var _nextTime = DateTime.FromBinary(nextTime);
        var currentTime = ParsedTime;


        var difference = _nextTime.Subtract(currentTime);

        var rawTime = (float) difference.TotalSeconds;

        if(PlayerPrefs.GetInt("HasFinished") == 1 && rawTime <= 0)
        {
            PlayerPrefs.SetInt("HasLoaded", 1);
            PlayerPrefs.SetInt("HasWon", 0);
            PlayerPrefs.SetInt("DenemeSayisi", 0);
            
            PlayerPrefs.SetInt("HasFinished", 0);

            for (int l = 1; l <= 6; l++)
            {
                
                
                PlayerPrefs.SetString("Satir" + l, "");


                
            }


            timeLeft = "Günlük kelime hazır";
            return;
        }

        TimeSpan timer = TimeSpan.FromSeconds(rawTime);
        
        timeLeft = $"Sıradaki günlük kelime: {timer:hh\\:mm\\:ss}";
    }


    void Update(){

       

        if (wordTimeApi.timeUpdated)
        {
            dateTime = wordTimeApi.dateTime;

            DateTime.TryParse(dateTime, out ParsedTime);
           
            timeUpdated = true;

            if (PlayerPrefs.GetInt("HasFinished") == 1)
            {
                CalculateNextPlayableTime();
            }

        }
        else
        {
            timeUpdated = false;
           
        }
        
       

        
        




        
    }


    public int DayOfToday()
    {
        return ParsedTime.Day;
    }

    public int MonthOfToday()
    {
        return ParsedTime.Month;
    }




    
    



}

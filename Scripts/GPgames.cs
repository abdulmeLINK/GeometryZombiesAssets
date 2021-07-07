using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GPgames : MonoBehaviour
{

    void Start()
    {
        PlayGamesClientConfiguration.Builder builder = new PlayGamesClientConfiguration.Builder();
        PlayGamesPlatform.InitializeInstance(builder.Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate((bool success, string err) =>
        {
            if (success)
            {
                Debug.Log("Giris Basarili");
            }
            else {

                Debug.Log("Giris Basarisiz");
                Debug.Log("HATA :" + err);
            }
        });
    }


    void Update()
    {

    }

    public void LoginSeq()
    {

        PlayGamesClientConfiguration.Builder builder = new PlayGamesClientConfiguration.Builder();
        PlayGamesPlatform.InitializeInstance(builder.Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate((bool success, string err) =>
        {
            if (success)
            {
                Debug.Log("Giris Basarili");
            }
            else
            {

                Debug.Log("Giris Basarisiz");
                Debug.Log("HATA : " + err);
            }
        });

    }

    public void RepScore(string id,int Score) {

        Social.ReportScore(Score, id, (bool success) => {
            if (success)
            {
                Debug.Log("Skor Yukleme Basarili");
            }
            else
            {

                Debug.Log("Skor Yukleme Basarisiz");

            }
        });

    }

    public void RepAchievement(string id) {

        Social.ReportProgress(id, 100.0f, (bool success) => {
            if (success)
            {
                Debug.Log("Basarim Yukleme Basarili");
            }
            else
            {

                Debug.Log("Basarim Yukleme Basarisiz");

            }
        });

    }

    public void ShowLeaderBoard() {
        
        Social.ShowLeaderboardUI();
    }
    public void ShowAchievement()
    {
        Social.ShowAchievementsUI();
    }
}

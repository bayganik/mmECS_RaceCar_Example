using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

public class GameEndsSystem : MonoBehaviour 
{
    void OnEnable()
    {
        EventManager.StartListening("EndOfGameEvent", Handle);
    }
    void OnDisable()
    {
        EventManager.StopListening("EndOfGameEvent", Handle);
    }
    void Handle(string value)
    {
        //
        // Make the panel visible again
        //
        GameObject go = GameObject.Find("GameOverCanvas");
        go.GetComponent<CanvasGroup>().alpha = 1f;

        Text endtxt = GameObject.Find("GameOverTxt").GetComponent<Text>();
        endtxt.text = "GameOver, " + value + " wins :-)";
        if (endtxt == null)
            return;
        Application.Quit();
        List<GameObject> goList = ObjectTracker.Find<UIEndComponent>();
        //
        // Game object is a panel that has text for end of game
        //
        //foreach (GameObject mmGO in goList)
        //{
        //    //
        //    // Get the component so we can have the data we need
        //    //
        //    //Text endTxt = mmGO.GetComponent<Text>();
        //    //endTxt.text = "GameOver, " + value + " wins :-)";
        //    //mmGO.gameObject.SetActive(true);
        //    Application.Quit();
        //}
    }

}
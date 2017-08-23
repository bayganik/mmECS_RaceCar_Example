using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerSystem : MonoBehaviour 
{
    void OnEnable()
    {


    }
    void OnDisable()
    {
        //EventManager.StopListening("Spawn", SomeOtherFunction);
    }
    private void Start()
    {
        //
        // Take off the display that says GameOver
        //
        GameObject go = GameObject.Find("GameOverCanvas");
        go.GetComponent<CanvasGroup>().alpha = 0f;
        //go.gameObject.SetActive(false);
        //
        // Trigger event to create opponents
        //
        EventManager.TriggerEvent("InitializeOpponentEvent", "GameInit");
    }


}
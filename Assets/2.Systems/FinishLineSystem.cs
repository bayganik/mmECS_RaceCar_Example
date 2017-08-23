using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FinishLineSystem : MonoBehaviour 
{
    void OnEnable()
    {
        EventManager.StartListening("FinishLineEvent", Handle);
    }
    void OnDisable()
    {
        EventManager.StopListening("FinishLineEvent", Handle);
    }
    void Handle(string value)
    {
        List<GameObject> goList = ObjectTracker.Find<EndOfGameComponent>();
        foreach (GameObject mmGO in goList)
        {
            //
            // We have an object with EndOfGameComponent
            // then it is done, stop the game
            //
            EventManager.TriggerEvent("EndOfGameEvent", "GameEnds");
            return;
        }
        //
        // Not end of game yet.  Find the car object and see if it crossed the line
        //
        GameObject car = GameObject.Find(value);

        goList = ObjectTracker.Find<FinishLineComponent>();
        foreach (GameObject mmFinishLine in goList)
        {
            if (car.transform.position.y > mmFinishLine.transform.position.y)
            {
                //
                // Someone has crossed the finish line, add component 
                // with winner's name
                //
                var eofg = mmFinishLine.AddComponent<EndOfGameComponent>();
                eofg.WinnerName = car.name;
                //
                // This will stop the game
                //
                EventManager.TriggerEvent("EndOfGameEvent", car.name);
            }
        }
    }

}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpponentMoveSystem : MonoBehaviour 
{
    bool gameOver = false;

    void OnEnable()
    {
        EventManager.StartListening("EndOfGameEvent", HandleGameOver);
    }
    void OnDisable()
    {
        EventManager.StopListening("EndOfGameEvent", HandleGameOver);
    }
    void HandleGameOver(string val)
    {
        //
        // if EndOfGameEvent has happened, then set the flag, so car does not move
        //
        gameOver = true;
    }
    void Update()
    {
        //
        // get all the objects that have Player and Input component
        //
        List<GameObject> goComps = ObjectTracker.Find<OpponentsComponent>();
        foreach (GameObject goInput in goComps)
        {
            if (gameOver)
                return;

            SpeedComponent speed = goInput.GetComponent<SpeedComponent>();
            if (speed == null)
                continue;

            //input.xAxis = player.speed * Input.GetAxis(input.HorizontalInput);
            //input.shoot = Input.GetButton(input.ShootInput);
            //
            // move the player vertically
            //
            Vector3 now = goInput.transform.position;
            goInput.transform.position = new Vector3(now.x, now.y + speed.speed, now.z);
            //
            // find out if player has crossed the finish line
            //
            EventManager.TriggerEvent("FinishLineEvent", goInput.name);
        }
    }

}
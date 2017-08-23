using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMoveSystem : MonoBehaviour 
{
    bool gameOver = false;
    void OnEnable()
    {
        EventManager.StartListening("EndOfGameEvent", HandleGameOver);
        EventManager.StartListening("PlayerMoveEvent", Handle);
    }
    void OnDisable()
    {
        EventManager.StopListening("EndOfGameEvent", HandleGameOver);
        EventManager.StopListening("PlayerMoveEvent", Handle);
    }
    void Handle(string val)
    { }
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
        List<GameObject> goComps = ObjectTracker.Find<PlayerComponent, InputComponent>();
        foreach (GameObject goInput in goComps)
        {
            if (gameOver)
                return;

            InputComponent input = goInput.GetComponent<InputComponent>();
            PlayerComponent player = goInput.GetComponent<PlayerComponent>();
            if ((player == null) || (input == null))
                continue;

            input.yAxis = player.speed * Input.GetAxis(input.VerticalInput);

            //input.xAxis = player.speed * Input.GetAxis(input.HorizontalInput);
            //input.shoot = Input.GetButton(input.ShootInput);
            //
            // move the player vertically
            //
            Vector3 now = goInput.transform.position;
            goInput.transform.position = new Vector3(now.x, now.y + input.yAxis, now.z);
            //
            // find out if player has crossed the finish line
            //
            EventManager.TriggerEvent("FinishLineEvent", goInput.name);
        }
    }

}
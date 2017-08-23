using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitializeOpponentSystem : MonoBehaviour 
{
    //
    // Opponents become childrend of game object "OpponentContainer"
    // OpponentContainer
    //      +-- Opps_0
    //      +-- Opps_1
    //      ...
    //      +-- Opps_5
    //
    void OnEnable()
    {
        EventManager.StartListening("InitializeOpponentEvent", Handle);
    }
    void OnDisable()
    {
        EventManager.StopListening("InitializeOpponentEvent", Handle);
    }
    void Handle(string value)
    {
        //
        // Find the OpponentsContainer object.  It has an "OpponentValues" component
        //
        List<GameObject> goComps = ObjectTracker.Find<OpponentValues>();

        foreach (GameObject opContainer in goComps)
        {
            //
            // Get the component so we can have the data we need
            //
            OpponentValues ocComp = opContainer.GetComponent<OpponentValues>();
            //
            // Player's car is the origin of the spawn point for all others
            // The opponents use the Player's car as offset to place themselves
            //
            var spawnX = ocComp.spawnOrigin.position.x + Mathf.Abs(ocComp.spawnOrigin.position.x / 2.0f);
            var spawnY = ocComp.spawnOrigin.position.y;
            //
            //  Setup of opponent cars as children
            //
            for (int cnt = 0; cnt < ocComp.maxCars; cnt++)
            {
                //
                // create object from prefabs in Assets/Resources folder
                //      Give it a name starting with "Opps_" plust a number
                //      Give it a sprite renderer so it can be displayed
                //      Give it a position offset on x-axis from the player car
                //
                GameObject oppsObj = GameObject.Instantiate(Resources.Load("Opponent", typeof(GameObject))) as GameObject;
                oppsObj.name = "Opps_" + cnt.ToString();
                //
                // This objects' parent is the "OpponentContainer"
                //
                oppsObj.transform.parent = opContainer.transform;
                //
                // Get the sprite renderer component & change the location
                //
                SpriteRenderer oppsRender = oppsObj.GetComponent<Renderer>() as SpriteRenderer;
                //
                // Add spawn X to width of the car multiply by count
                //
                var x = (spawnX) + ((oppsRender.bounds.extents.x * 3.0f) * cnt);
                var y = spawnY;
                oppsRender.transform.localPosition = new Vector2(x, y);
                //
                // Add OpponentCarComponent & SpeedComponent to this GameObject
                //
                oppsObj.AddComponent<OpponentsComponent>();
                var sc = oppsObj.AddComponent<SpeedComponent>();
                //
                // Get the SpeedComponent
                //
                //var sc = oppsObj.GetComponent<SpeedComponent>();
                sc.speed = (Random.value * 0.02f);
                //
                // Tell ObjectTracker about this GameObject
                //
                ObjectTracker.Register(sc.GetType(), oppsObj);

            }
        }
    }

}
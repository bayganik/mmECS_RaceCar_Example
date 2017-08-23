using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentValues : MonoBehaviour
{

    //
    // OpponentContainer is a gameobject that will have children
    //    which are the orange opponent cars
    //    Will attach Assets/Resources/Opponent to this 
    //
    public Transform spawnOrigin;       //player car location - used as offset 
    public int maxCars = 6;             //number of opponents to spawn
    private void OnEnable()
    {
        //
        // Register component with ObjectTracker
        // When attached to a GameObject (entity)
        //
        ObjectTracker.Register(this.GetType(), this.gameObject);
    }
}

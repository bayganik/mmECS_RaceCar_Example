using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class InputComponent : MonoBehaviour
{
    //
    // This allows a GameObject to be under control of input keys
    //
    public float xAxis;
    public float yAxis;
    public bool shoot;

    public string HorizontalInput = "Horizontal";
    public string VerticalInput = "Vertical";
    public string ShootInput = "Fire1";

    private void OnEnable()
    {
        //
        // Register component with ObjectTracker
        // When attached to a GameObject (entity)
        //
        ObjectTracker.Register(this.GetType(), this.gameObject);
    }
}
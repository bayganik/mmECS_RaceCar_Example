using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputSystem : MonoBehaviour 
{
    bool isVerticalUsed = false;
    bool isHorizontalUsed = false;

    void OnEnable()
    {
        //EventManager.StartListening("Spawn", SomeOtherFunction);
    }
    void OnDisable()
    {
        //EventManager.StopListening("Spawn", SomeOtherFunction);
    }
    void SomeOtherFunction(string value)
    { 
          Debug.Log("function was called!");
    }
    private void Update()
    {
        List<GameObject> goComps = ObjectTracker.Find<InputComponent>();
        foreach (GameObject opInput in goComps)
        {
            //
            // Get the component so we can have the data we need
            //
            InputComponent ocComp = opInput.GetComponent<InputComponent>();

            if (Input.GetAxisRaw(ocComp.VerticalInput) != 0)
            {
                if (!isVerticalUsed)
                {
                    EventManager.TriggerEvent("PlayerMoveEvent", "");
                    isVerticalUsed = true;
                }
            }
            else
            {
                isVerticalUsed = false;
            }
            if (Input.GetAxisRaw(ocComp.HorizontalInput) != 0)
            {
                if (!isHorizontalUsed)
                {
                    EventManager.TriggerEvent("PlayerMoveEvent", "");
                    isHorizontalUsed = true;
                }
            }
            else
            {
                isHorizontalUsed = false;
            }
        }
    }

}
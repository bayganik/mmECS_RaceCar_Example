﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class EndOfGameComponent : MonoBehaviour
{
    public string WinnerName;

	private void OnEnable()
    {
        //
        // Register component with ObjectTracker
        // When attached to a GameObject (entity)
        //
        ObjectTracker.Register(this.GetType(), this.gameObject);
    }
}
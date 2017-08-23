﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class OpponentsComponent : MonoBehaviour
{

    private void OnEnable()
    {
        //
        // Register component with ObjectTracker
        // When attached to a GameObject (entity)
        //
        ObjectTracker.Register(this.GetType(), this.gameObject);
    }
}
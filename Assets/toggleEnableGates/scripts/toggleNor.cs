﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(enable))]

public class toggleNor : MonoBehaviour
{

    public enable enable1;
    public enable enable2;

    private void Update()
    {

        if (!(enable1.isOn || enable2.isOn))
        {

            gameObject.GetComponent<enable>().isOn = true;

        }

    }

}

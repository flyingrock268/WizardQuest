using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(enable))]

public class toggleOr : MonoBehaviour
{

    public enable enable1;
    public enable enable2;

    public void Update()
    {

        if (enable1.isOn || enable2.isOn)
        {

            gameObject.GetComponent<enable>().isOn = true;

        }

    }
}

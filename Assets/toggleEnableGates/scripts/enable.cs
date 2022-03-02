using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enable : MonoBehaviour
{

    public bool isOn = false;
    bool switched = false;
    public GameObject obj;

    public void Update() {

        if (obj != null && !switched)
        {

            if (isOn && !obj.activeSelf)
            {

                obj.SetActive(true);
                switched = true;

            }

            else if(isOn && obj.activeSelf)
            {

                obj.SetActive(false);
                switched = true;

            }

        }

    }

}

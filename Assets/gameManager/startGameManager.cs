using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startGameManager : MonoBehaviour
{
    public startScreen screen;

    void start() {

        if (screen != null) {

            Instantiate(screen);
        
        }
    
    }
}

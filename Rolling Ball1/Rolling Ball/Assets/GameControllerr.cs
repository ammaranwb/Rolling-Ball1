using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerr : MonoBehaviour
{

    public GameController GameOverScreen;
    int maxPlatform = 0;

    public void GameOver()
    {
        GameOverScreen.Setup(maxPlatform);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameController
{
    void checkPlayerHP();
    IEnumerator Gameover();
    void StartGame();
    void BlinkWhenLowHP();
}

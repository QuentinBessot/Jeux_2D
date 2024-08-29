using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayerEffect : MonoBehaviour
{
    public void AddSpeed(int speedGiven, float speedDuration) 
    {
        Move_Players.instance.moveSpeed += speedGiven;
        StartCoroutine(RemoveSpeed(speedGiven, speedDuration));
    }

    public IEnumerator RemoveSpeed(int speedGiven, float speedDuration) 
    {
        yield return new WaitForSeconds(speedDuration);
        Move_Players.instance.moveSpeed -= speedGiven;  
    }
    public void AddSJump(int jumpGiven, float junpDuration)
    {
        Move_Players.instance.jumpForce += jumpGiven;
        StartCoroutine(RemoveJump(jumpGiven, junpDuration));
    }

    public IEnumerator RemoveJump(int jumpGiven, float junpDuration)
    {
        yield return new WaitForSeconds(junpDuration);
        Move_Players.instance.jumpForce -= jumpGiven;
    }

}

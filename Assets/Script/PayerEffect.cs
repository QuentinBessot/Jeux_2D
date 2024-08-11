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


}

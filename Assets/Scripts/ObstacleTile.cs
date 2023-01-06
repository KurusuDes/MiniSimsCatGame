using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTile : MonoBehaviour
{
    void Start()
    {
        Invoke("Obstacle", 0.2f);
    }
    public void Obstacle()
    {
        GameManager.Instance.DisableAtPosition(transform.position);
    }
}

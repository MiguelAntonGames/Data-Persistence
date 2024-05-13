using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private MainManager _manager;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        _manager.GameOver();
    }
}

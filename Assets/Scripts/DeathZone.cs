using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {
    private void OnCollisionEnter(Collision other) {
        Destroy(other.gameObject);
        GameplayManager.Instance.GameOver();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public static ActiveWeapon Instance;

    [SerializeField] private Sword sword;

    private void Awake() {
        Instance = this;
    }

    public Sword GetActiveWeapon() {
        return sword;
    }
}

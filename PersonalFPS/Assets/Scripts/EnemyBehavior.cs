using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float health;
    Color alive, death;
    void Start()
    {

    }

    void Update()
    {

    }

    public void GetDamage(float damage)
    {
        print("Enemy Received " + damage + " Damaged");
        health -= damage;
    }
}

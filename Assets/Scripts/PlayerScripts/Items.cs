using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public int parametr;
    public PlayerMove playerMove;

    void Start()
    {

    }

    
    void Update()
    {
        
    }

    void GetAbility()
    {
        if (parametr == 1)
            playerMove.GetDash();
        if (parametr == 2)
            playerMove.GetDoubleJumpe();
        if (parametr == 3)
            playerMove.AddHealth();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetAbility();
            Destroy(gameObject, 3f);
        }
    }
}

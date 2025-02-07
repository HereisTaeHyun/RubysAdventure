using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController rubyController = other.GetComponent<RubyController>();
        if(rubyController != null)
        {
            if(rubyController.health < rubyController.maxHealth)
            {
                rubyController.ChangeHealth(1);
                Destroy(gameObject); 
            }
        }
    }
}

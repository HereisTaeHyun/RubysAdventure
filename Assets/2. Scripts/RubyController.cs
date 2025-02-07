using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    Rigidbody2D rb2D;
    public float moveSpeed = 50.0f;
    public int maxHealth = 5;
    public int health{get{return currentHealth;}}
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        // currentHealth = maxHealth;
        currentHealth = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 position = rb2D.position;
        position.x += moveSpeed * horizontal * Time.deltaTime;
        position.y += moveSpeed * vertical * Time.deltaTime;
        rb2D.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth); // currentHealth + amount = 제한 값으로 이 값이 0 ~ maxHealth에 있게됨
        Debug.Log($"{currentHealth} / {maxHealth}");
    }
}

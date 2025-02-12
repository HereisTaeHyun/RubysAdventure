using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    Rigidbody2D rb2D;
    public const float THROW_POWER = 300.0f; 
    public float moveSpeed = 3.0f;
    public int maxHealth = 5;
    public int health{get{return currentHealth;}}
    public GameObject projectilePrefeb;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    
    private int currentHealth;
    private Vector2 position;
    private Vector2 lookDir = new Vector2(1, 0);
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        position = rb2D.position;
        // currentHealth = maxHealth;
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // move에 x와 y 모두 저장
        Vector2 move = new Vector2(horizontal, vertical);
        // move.x와 move.y가 0이 아닌지 확인하는 것으로 Approximately는 근사치면 true 반환
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDir.Set(move.x, move.y);
            lookDir.Normalize(); // 방향 벡터 저장
        }

        // lookDir은 정규화한 방향 벡터
        // move에는 방향 + 크기, move.magnitude로 크기 전달
        anim.SetFloat("Look X", lookDir.x);
        anim.SetFloat("Look Y", lookDir.y);
        anim.SetFloat("Speed", move.magnitude);

        position += move * moveSpeed * Time.deltaTime;
        rb2D.MovePosition(position);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            isInvincible = false;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }
    }

    public void ChangeHealth(int amount)
    {
        if(amount < 0)
        {
            anim.SetTrigger("Hit");
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth); // currentHealth + amount = 제한 값으로 이 값이 0 ~ maxHealth에 있게됨
        Debug.Log($"{currentHealth} / {maxHealth}");
    }

    private void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefeb, rb2D.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDir, THROW_POWER);
        anim.SetTrigger("Launch");
    }
}

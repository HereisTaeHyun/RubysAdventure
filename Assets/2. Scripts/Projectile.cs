using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public const float DISTANCE_DESTROY = 100.0F;
    Rigidbody2D rb2D;
    // Start는 생성 다음 프레임에 초기화
    // 즉, RubyController서 Instantiate 후 Launch가 먼저 시행 되기에 Null 참조 에러 발생
    // Awake는 Instantiate 시점에 즉시 시행되기에 Awake 필요
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > DISTANCE_DESTROY)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rb2D.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent<EnemyController>(out var enemyController))
        {
            enemyController.fix();
        }
        Destroy(gameObject);
    }
}

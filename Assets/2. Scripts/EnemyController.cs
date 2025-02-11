using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // public 변수
    public float changeTime = 2.0f;
    public float moveSpeed = 2.0f;
    public bool vertical;

    // private 변수
    private float moveTimer;
    private int moveDir = 1;
    private Vector2 pos;
    Rigidbody2D rb;
    Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveTimer = changeTime;
        pos = rb.position;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Timer가 0 미만이 될 경우 moveDir 역전
        moveTimer -= Time.deltaTime;
        if(moveTimer < 0)
        {
            moveDir = -moveDir;
            moveTimer = changeTime;
        }

        // vertical == true면 상하 아니면 좌우
        if(vertical)
        {
            pos.y = pos.y + (moveSpeed * moveDir * Time.deltaTime);
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", moveDir);
        }
        else
        {
            pos.x = pos.x + (moveSpeed * moveDir * Time.deltaTime);
            anim.SetFloat("MoveX", moveDir);
            anim.SetFloat("MoveY", 0);
        }
        rb.MovePosition(pos);
    }
}

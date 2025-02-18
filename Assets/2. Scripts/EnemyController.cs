using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int needFix = 3;
    // public 변수
    public float changeTime = 2.0f;
    public float moveSpeed = 2.0f;
    public bool vertical;
    public ParticleSystem smokeEffect;
    public ParticleSystem crashEffect;
    public AudioSource audioSource;
    public AudioClip fixedClip;

    // private 변수
    private float moveTimer;
    private int moveDir = 1;
    private bool broken;
    private Vector2 pos;
    private int fixedCount;
    private RubyController rubyController;
    Rigidbody2D rb;
    Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveTimer = changeTime;
        pos = rb.position;
        anim = GetComponent<Animator>();
        broken = true;
        fixedCount = 0;
        audioSource = GetComponent<AudioSource>();
        rubyController = GameObject.FindGameObjectWithTag("Player").GetComponent<RubyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(broken != true)
        {
            return;
        }
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

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void fix()
    {
        if(fixedCount >= needFix)
        {
            broken = false;
            rb.simulated = false;
            anim.SetTrigger("Fixed");
            rubyController.PlaySound(fixedClip);
            smokeEffect.Stop();
            audioSource.clip = null;
        }
        else
        {
            fixedCount++;
        }
    }
}

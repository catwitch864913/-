using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 移動 : MonoBehaviour
{
    public static 移動 move;
    public 假房間逃出口 逃出口腳本;
    public int 最大血量;
    public static int 當前血量=100;
    public float runSpeed;
    public float jumpSpeed;
    public float time = 0.5f;
    //紀錄上一個傳送門的位置
    public Vector2 teleportSource;

    [HideInInspector] public Rigidbody2D myRigidbody;
    private Animator myAnim;
    private Animator DeathInterface;
    private BoxCollider2D myFeet;
    private bool isGround;
    private bool 平台;
    public GameObject 噴血特效;

    public GameObject 死亡介面;

    private void Awake()
    {
        move = this;
        if (SaveManger.instance.繼續遊戲 == true)
        {
            this.最大血量 = SaveManger.instance.存檔資料.PlayerHp;
            當前血量 = 最大血量;
            this.transform.position = SaveManger.instance.存檔資料.PlayerPos;
            切換箭矢.當前魔毒箭矢數量 = SaveManger.instance.存檔資料.PoisonArrow;
            切換箭矢.當前仙草箭矢數量 = SaveManger.instance.存檔資料.GrassJellyArrow;
            仙草數量.當前仙草數量 = SaveManger.instance.存檔資料.GrassJelly;
            //魔毒數量.當前魔毒數量 = SaveManger.instance.存檔資料.Devilpoison;
            SaveManger.instance.繼續遊戲 = false;
        }
        else
        {
            //最大血量 = 100;
            //當前血量 = 最大血量;
            切換箭矢.當前魔毒箭矢數量 = 0;
            切換箭矢.當前魔毒箭矢數量 = 0;
        }
    }
    void Start()
    {
        DeathInterface = 死亡介面.GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        if (逃出口腳本 != null)
        {
            逃出口腳本 = GameObject.Find("門 (8)").GetComponent<假房間逃出口>();
        }
    }
    void Update()
    {
        Flip();
        Run();
        Jump();
        CheckGrounded();
        SwitchAnimation();
        die();
        穿越平台();
    }


    void CheckGrounded()                                                                    //檢測是否為地面
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
                   myFeet.IsTouchingLayers(LayerMask.GetMask("穿越平台"));
        平台 = myFeet.IsTouchingLayers(LayerMask.GetMask("穿越平台"));
    }

    void Flip()
    {
        bool plyerHasHAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;        //移動翻轉
        if (plyerHasHAxisSpeed)
        {
            if (myRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");                                        //移動
        Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
        bool plyerHasHAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("Run", plyerHasHAxisSpeed);                                          //Run動畫參數
    }

    void Jump()                                                                             //跳
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                myAnim.SetBool("Jump", true);
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                myRigidbody.velocity = Vector2.up * jumpVel;
                音效.跳起();
            }
        }
    }
    public void 關閉移動速度()
    {
        myRigidbody.velocity = new Vector2(0, 0);
        myAnim.SetBool("Idle", true); myAnim.SetBool("Fall", false);
        myAnim.SetBool("Jump", false); myAnim.SetBool("Run", false);
    }

    void SwitchAnimation()
    {
        myAnim.SetBool("Idle", false);
        if (myAnim.GetBool("Jump"))
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("Fall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("Fall", false);
            myAnim.SetBool("Idle", true);
        }
    }
    public void DamegePlayer(int damage)
    {
        當前血量 -= damage;
        Instantiate(噴血特效, transform.position, Quaternion.identity);
    }
    public void 地刺(int 傷害)
    {
        當前血量 -= 傷害;
        Instantiate(噴血特效, transform.position, Quaternion.identity);
    }
    void 呼叫死亡介面()
    {
        死亡介面.SetActive(true);
        DeathInterface.SetBool("死亡", true);
        Invoke("切換死亡介面動畫", 2f);
    }
    void 切換死亡介面動畫()
    {
        Time.timeScale = 0f;
    }
    void die()
    {
        if (當前血量 <= 0)
        {
            myRigidbody.velocity = new Vector2(0, 0);
            GameObject.Find("弓").GetComponent<射箭腳本>().enabled = false;//關閉物件中aa腳本
            myAnim.SetBool("Die", true);
            GameObject.Find("主角").GetComponent<移動>().enabled = false;
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("死亡"))
        {
            Invoke("呼叫死亡介面", 1f);
            Destroy(this.gameObject, 1f);
            當前血量 = 0;
        }

    }
    void 穿越平台()
    {
        if (isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
        float moveY = Input.GetAxis("Vertical");
        if (平台 && moveY < -0.1f)
        {
            gameObject.layer = LayerMask.NameToLayer("穿越平台");
            Invoke("RestorPlayerLayer", time);
        }
    }
    void RestorPlayerLayer()
    {
        if (!isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
    public void ReturnBlood()
    {
        if (當前血量 < 100)
        {
            當前血量 += 5;
        }
        else if (當前血量 > 100)
        {
            當前血量 = 100;
        }
    }
    public void TeleportBack()
    {
        // 如果有儲存的傳送門位置，就將玩家傳送回去
        if (teleportSource != null)
        {
            transform.position = teleportSource;
            //目前下方的腳本無用
            //逃出口腳本.重製當前怪物數量();
        }
    }

    /*void 傳送到20()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //myRigidbody.position += Vector2.up * 50;
            myRigidbody.position = new Vector2(myRigidbody.position.x, 20);
        }
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NejikoController : MonoBehaviour
{

    //1.プレイヤーのキー入力を受け取る
    //2.キー入力の方向に移動する
    //3.移動に合わせてアニメーションを再生する


    CharacterController controller;

    Vector3 moveDirection = Vector3.zero;

    public float speed = 0f;

    Animator animator;
    //ジャンプの高さを決める変数
    public float jumpPower = 5f;
    //重力の強さを決める変数
    public float gravityPower = 0;

    //ラインの数の最大値
    int MaxLine = 2;

    //ラインの数の最小値
    int MixLine = -2;

    //ライン間の距離
    float LineWidth = 1.0f;

    //移動先のライン
    int targetline = 0;

    //敵キャラクターと当たった時に停止する時間
    float StunTime = 0.5f;

    //キャラクタが止まってから動き出すまでの復帰時間
    public float recoverTime=0.0f;

    //プレイヤーのHP
    public int playerHitPoint = 3;

    //キャラクタースタン中が判断するクラス
    bool Isstun()
    {
        return recoverTime  > 0.0f;
    }


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            Debug.Log("地面についてるよ");
            //ねじ子がジャンプを行う処理
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpPower;
                animator.SetBool("jump", moveDirection.z > 0f);
            }
        }

        //スタン中の場合は移動量を0に固定する
        if(Isstun() == true)
        {
            moveDirection.x = 0f;
            moveDirection.z = 0f;
            recoverTime -= Time.deltaTime;
        }

        //敵に触れてスタン中なら前進しない
        if(Isstun()　== false)
        {
            //1フレーム事に全身する距離の更新
            float movePowerZ = moveDirection.z + (speed * Time.deltaTime);
            //更新した距離と現在地の差分距離の計算
            moveDirection.z = Mathf.Clamp(movePowerZ, 0f, speed);
        }


        //X方向は目標ポジションまでの差分で速度をだす
        float ratioX = (targetline * LineWidth - transform.position.x) / LineWidth;
        moveDirection.x = ratioX * speed;

        //右レーン切り替え
        if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
        {
            if (controller.isGrounded && targetline < MaxLine)
            {
                targetline = targetline + 1;
            }
        }

        if (Input.GetKeyDown("left") || Input.GetKeyDown("a"))
        {
            if (controller.isGrounded && targetline > MixLine)
            {
                targetline = targetline - 1;
            }
        }

        //左のレーン切り替え
        /*
                if (Input.GetAxis("Vertical") > 0.0f)
                {
                    moveDirection.z = Input.GetAxis("Vertical") * speed;
                }
                else
                {
                    moveDirection.z = 0.0f;
                }
        */

        //Horaizontal(左右移動)ねじこを回転させる
        //transform.Rotate(0, Input.GetAxis("Horizontal") * 3f, 0);

        //キャラクターが重力で落下する処理
        moveDirection.y = moveDirection.y - gravityPower * Time.deltaTime;

        //移動量をtransformに変換する
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        //controllerに移動量を渡す
        controller.Move(globalDirection * Time.deltaTime);
        //ねじこのアニメーションを最新する
        animator.SetBool("run", moveDirection.z > 0f);
    }
    //敵キャラクターに当たった場合の処理
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Robo")
        {
            Debug.Log("敵にぶつかった！");
            recoverTime = StunTime;
            playerHitPoint--;
            animator.SetTrigger("damege");
            Destroy(hit.gameObject);
        }
    }
}

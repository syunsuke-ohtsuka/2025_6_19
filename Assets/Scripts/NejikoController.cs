using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NejikoController : MonoBehaviour
{

    //1.�v���C���[�̃L�[���͂��󂯎��
    //2.�L�[���͂̕����Ɉړ�����
    //3.�ړ��ɍ��킹�ăA�j���[�V�������Đ�����


    CharacterController controller;

    Vector3 moveDirection = Vector3.zero;

    public float speed = 0f;

    Animator animator;
    //�W�����v�̍��������߂�ϐ�
    public float jumpPower = 5f;
    //�d�͂̋��������߂�ϐ�
    public float gravityPower = 0;

    //���C���̐��̍ő�l
    int MaxLine = 2;

    //���C���̐��̍ŏ��l
    int MixLine = -2;

    //���C���Ԃ̋���
    float LineWidth = 1.0f;

    //�ړ���̃��C��
    int targetline = 0;

    //�G�L�����N�^�[�Ɠ����������ɒ�~���鎞��
    float StunTime = 0.5f;

    //�L�����N�^���~�܂��Ă��瓮���o���܂ł̕��A����
    public float recoverTime=0.0f;

    //�v���C���[��HP
    public int playerHitPoint = 3;

    //�L�����N�^�[�X�^���������f����N���X
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
            Debug.Log("�n�ʂɂ��Ă��");
            //�˂��q���W�����v���s������
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpPower;
                animator.SetBool("jump", moveDirection.z > 0f);
            }
        }

        //�X�^�����̏ꍇ�͈ړ��ʂ�0�ɌŒ肷��
        if(Isstun() == true)
        {
            moveDirection.x = 0f;
            moveDirection.z = 0f;
            recoverTime -= Time.deltaTime;
        }

        //�G�ɐG��ăX�^�����Ȃ�O�i���Ȃ�
        if(Isstun()�@== false)
        {
            //1�t���[�����ɑS�g���鋗���̍X�V
            float movePowerZ = moveDirection.z + (speed * Time.deltaTime);
            //�X�V���������ƌ��ݒn�̍��������̌v�Z
            moveDirection.z = Mathf.Clamp(movePowerZ, 0f, speed);
        }


        //X�����͖ڕW�|�W�V�����܂ł̍����ő��x������
        float ratioX = (targetline * LineWidth - transform.position.x) / LineWidth;
        moveDirection.x = ratioX * speed;

        //�E���[���؂�ւ�
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

        //���̃��[���؂�ւ�
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

        //Horaizontal(���E�ړ�)�˂�������]������
        //transform.Rotate(0, Input.GetAxis("Horizontal") * 3f, 0);

        //�L�����N�^�[���d�͂ŗ������鏈��
        moveDirection.y = moveDirection.y - gravityPower * Time.deltaTime;

        //�ړ��ʂ�transform�ɕϊ�����
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        //controller�Ɉړ��ʂ�n��
        controller.Move(globalDirection * Time.deltaTime);
        //�˂����̃A�j���[�V�������ŐV����
        animator.SetBool("run", moveDirection.z > 0f);
    }
    //�G�L�����N�^�[�ɓ��������ꍇ�̏���
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Robo")
        {
            Debug.Log("�G�ɂԂ������I");
            recoverTime = StunTime;
            playerHitPoint--;
            animator.SetTrigger("damege");
            Destroy(hit.gameObject);
        }
    }
}

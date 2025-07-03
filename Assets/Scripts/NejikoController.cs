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

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") > 0.0f)
        {
            moveDirection.z = Input.GetAxis("Vertical") * speed;
        }
        else
        {
            moveDirection.z = 0.0f;
        }

        //Horaizontal(���E�ړ�)�˂�������]������
        transform.Rotate(0, Input.GetAxis("Horizontal") * 3f, 0);

        //jump�L�[�̓��͂�������΁A�˂�������]������
        if(Input.GetButton("Jump"))
        {
            moveDirection.y = 0.1f;
            animator.SetTrigger("jump");
        }

        //�ړ��ʂ�transform�ɕϊ�����
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        //controller�Ɉړ��ʂ�n��
        controller.Move(globalDirection);
        //�˂����̃A�j���[�V�������ŐV����
        animator.SetBool("run", moveDirection.z > 0f);
    }
}

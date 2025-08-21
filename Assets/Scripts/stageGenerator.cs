using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageGenerator : MonoBehaviour
{
    //�^�[�Q�b�g�L�����N�^�[�̕ێ��p�ϐ�
    public Transform character;
    //�X�e�[�W��prfab��z��ŊǗ�����ϐ�
    public GameObject[] stageChip;
    //Scene�Ɏ��̉��������X�e�[�W��prefab���Ǘ�����z��
    public List<GameObject> genrateStageList = new List<GameObject>();

    public int preInstance = 5;

    public int currentChipIndex = 5;

    private int characterPositionIndex =0;

    void Start()
    {
        
    }

    private int GetPreInstance()
    {
        return preInstance;
    }

    // Update is called once per frame
    void Update()
    {
        //�L�����N�^�[�̌��݈ʒu���猻�݂̃X�e�[�W�`�b�v�̃C���f�b�N�X���v�Z
        int caracterPositionIndex = (int)(character.position.z / 30f);
        Debug.Log(characterPositionIndex);

        //��������---------------------------------------------------------------
        //�L�����N�^�[���i�񂾂�X�e�[�W�`�b�v��ǉ��Ő�������
        for(int i = preInstance + caracterPositionIndex; i >= preInstance; i++)
        {
            //�ŏ��ɍ�����X�e�[�W���{�����̒ʉ݂����X�e�[�W�������X�e�[�W�𐶐�����
            if(genrateStageList.Count > preInstance + caracterPositionIndex)
            {
                return;
            }
            //�����𐶐�����
            int randomValue = Random.Range(0, stageChip.Length);
            Debug.Log(randomValue);

            //�����ō��X�e�[�W�������_���ɕύX����
            GameObject stageObject = Instantiate(stageChip[randomValue]);


            stageObject.transform.position = new Vector3(0, 0, currentChipIndex * 30f);
            //���������X�e�[�W�`�b�v���Ǘ����X�g�ɒǉ�
            genrateStageList.Add(stageObject);
            currentChipIndex++;
        }
        //�����܂�---------------------------------------------------------------
    }
}

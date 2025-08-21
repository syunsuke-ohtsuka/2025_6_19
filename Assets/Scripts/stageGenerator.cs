using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageGenerator : MonoBehaviour
{
    //ターゲットキャラクターの保持用変数
    public Transform character;
    //ステージのprfabを配列で管理する変数
    public GameObject[] stageChip;
    //Sceneに実体化させたステージのprefabを管理する配列
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
        //キャラクターの現在位置から現在のステージチップのインデックスを計算
        int caracterPositionIndex = (int)(character.position.z / 30f);
        Debug.Log(characterPositionIndex);

        //ここから---------------------------------------------------------------
        //キャラクターが進んだらステージチップを追加で生成する
        for(int i = preInstance + caracterPositionIndex; i >= preInstance; i++)
        {
            //最初に作ったステージ数＋自分の通貨したステージ数だけステージを生成する
            if(genrateStageList.Count > preInstance + caracterPositionIndex)
            {
                return;
            }
            //乱数を生成する
            int randomValue = Random.Range(0, stageChip.Length);
            Debug.Log(randomValue);

            //ここで作るステージをランダムに変更する
            GameObject stageObject = Instantiate(stageChip[randomValue]);


            stageObject.transform.position = new Vector3(0, 0, currentChipIndex * 30f);
            //生成したステージチップを管理リストに追加
            genrateStageList.Add(stageObject);
            currentChipIndex++;
        }
        //ここまで---------------------------------------------------------------
    }
}

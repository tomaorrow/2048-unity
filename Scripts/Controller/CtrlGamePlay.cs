using System;
using UnityEngine;
using UnityEngine.UI;

//主控制器
public class CtrlGamePlay : MonoBehaviour 
{    
    EntityGenerator entityGenerator = new EntityGenerator();
    CtrlMerge ctrlMerge = new CtrlMerge();
    Judgement judgement = new Judgement();
    CtrlScore ctrlScore = new CtrlScore();

    //方块合并状态
    bool isMerging;
    
    void Start ()
    {
        //初始化地图
        Map.Instance.Init();
        //生成两个方块实体
        entityGenerator.GenerateFirst();
        //初始化得分
        ctrlScore.Init();

        ctrlMerge.Finish += OnFinishMerge;

    }
	
	void Update ()
    {
		if (isMerging)
        {
            ctrlMerge.CheckFinish();
        }
	}

    //合并
    public void Merge(Direction dir)
    {
        ctrlMerge.Merge(dir);

        isMerging = true;
    }

    public void OnFinishMerge(object sender, EventArgs e)
    {
        if (ctrlMerge.HasMerged || ctrlMerge.HasMoved)
        {
            Finish();
        }

        isMerging = false;          //2019-3-12 将该句从Finish()中移到该行，解决获取用户输入后未发生移动但一直调用CheckFinish()的问题
    }
    
    //一轮行动结束
    public void Finish()
    {
        entityGenerator.Generate();

        //更新得分
        ctrlScore.UpdateScore();

        //判输
        if (judgement.JudgeLose())
        {
            //游戏结束
            GameObject.Find("txt_goal").GetComponent<Text>().text = "游戏结束！";

        }
    }
}

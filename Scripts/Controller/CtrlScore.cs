using UnityEngine;
using UnityEngine.UI;

public class CtrlScore
{
    public static int Score { get; set; }
    public static int TopScore { get; set; }

    private GameObject obj_score, obj_topScore;
    private Text txt_score, txt_topScore;

    //初始化计分板
    public void Init()
    {
        //当前得分
        Score = 0;
        obj_score = GameObject.Find("txt_score");
        txt_score = obj_score.GetComponent<Text>();
        txt_score.text = Score.ToString();
        //历史最高分
        TopScore = PlayerPrefs.GetInt("topScore");
        obj_topScore = GameObject.Find("txt_topScore");
        txt_topScore = obj_topScore.GetComponent<Text>();
        txt_topScore.text = TopScore.ToString();
    }

    public void UpdateScore()
    {
        //当前得分
        txt_score.text = Score.ToString();
        //历史最高分
        if (Score > TopScore)
        {
            TopScore = Score;
            txt_topScore.text = TopScore.ToString();
            PlayerPrefs.SetInt("topScore", TopScore);
        }
    }
}

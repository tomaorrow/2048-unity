using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//游戏结束窗口
public class GameOverPanel : MonoBehaviour {

    //返回键
    public Button Btn_Back;

	void Start ()
    {
        Btn_Back.onClick.AddListener(OnClick);
	}
	
    //点击返回键关闭窗口
    void OnClick()
    {
        GameObject gameOverPanel = GameObject.Find("gameOverPanel");
        gameOverPanel.GetComponent<CanvasGroup>().alpha = 0;
        gameOverPanel.GetComponent<CanvasGroup>().interactable = false;
        gameOverPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    
    //显示窗口
    public static void Show()
    {
        GameObject gameOverPanel = GameObject.Find("gameOverPanel");
        gameOverPanel.GetComponent<CanvasGroup>().alpha = 1;
        gameOverPanel.GetComponent<CanvasGroup>().interactable = true;
        gameOverPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}

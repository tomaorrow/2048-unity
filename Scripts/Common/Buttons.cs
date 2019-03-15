using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//重新开始
public class Buttons : MonoBehaviour
{
    public Button Btn_Restart;
    public Button Btn_Share;
    public Button Btn_OK;

    void Start()
    {
        Btn_Restart.onClick.AddListener(Restart);
        Btn_Share.onClick.AddListener(Share);
        Btn_OK.onClick.AddListener(OK);
    }

    //重新开始
    void Restart()
    {
        SceneManager.LoadScene("game");
    }

    //分享按钮
    void Share()
    {
        GameObject gameOverPanel = GameObject.Find("emmm");
        gameOverPanel.GetComponent<CanvasGroup>().alpha = 1;
        gameOverPanel.GetComponent<CanvasGroup>().interactable = true;
        gameOverPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    //“好的”按钮
    void OK()
    {
        GameObject gameOverPanel = GameObject.Find("emmm");
        gameOverPanel.GetComponent<CanvasGroup>().alpha = 0;
        gameOverPanel.GetComponent<CanvasGroup>().interactable = false;
        gameOverPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
	

}

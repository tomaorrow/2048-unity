using UnityEngine;

//场景入口
public class SceneEntryGamePlay : MonoBehaviour
{	
    //载入“获取用户输入”和“主控制器”脚本
	void Start ()
    {
        gameObject.AddComponent<UserInput>();
        gameObject.AddComponent<CtrlGamePlay>();

        Debug.unityLogger.logEnabled = false;
	}
}

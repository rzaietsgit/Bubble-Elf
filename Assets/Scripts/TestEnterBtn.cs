using UnityEngine;

public class TestEnterBtn : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void EnterBtn()
	{
		Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.GameScene);
	}
}

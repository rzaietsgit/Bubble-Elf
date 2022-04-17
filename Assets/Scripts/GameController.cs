using UnityEngine;

public class GameController : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void ChangeToMap()
	{
		Singleton<SceneManager>.Instance.ChangeScene(EnumSceneType.MapScene);
	}
}

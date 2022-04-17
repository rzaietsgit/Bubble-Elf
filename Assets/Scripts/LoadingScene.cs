using System.Collections;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{
	public GameObject LogoObj;

	private void Start()
	{
		InitAndroid.action.GAEvent("Loadingshow:0");
		StartLoading();
		
		StartCoroutine(Time10());
	}

	private IEnumerator Time10()
	{
		yield return new WaitForSeconds(10f);
		InitAndroid.action.GAEvent("Loadingshow:10");
		yield return new WaitForSeconds(10f);
		InitAndroid.action.GAEvent("Loadingshow:20");
		yield return new WaitForSeconds(10f);
		InitAndroid.action.GAEvent("Loadingshow:30");
	}

	private void StartLoading()
	{
		UI.Instance.ClearUI();
		if ((bool)LoadingSceneUI.action)
		{
			LoadingSceneUI.action.HideLoadingSceneUI();
		}
		Singleton<TestScript>.Instance.Save();
		Singleton<DataManager>.Instance.isUpdateWinData = false;
		EnumSceneType changeSceneType = Singleton<DataManager>.Instance.ChangeSceneType;
		string sceneName = Singleton<SceneManager>.Instance.GetSceneName(changeSceneType);
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
	}

	private IEnumerator IEStartLoading()
	{
		if ((bool)LoadingSceneUI.action)
		{
			LoadingSceneUI.action.HideLoadingSceneUI();
		}
		Singleton<TestScript>.Instance.Save();
		Singleton<DataManager>.Instance.bBuyLB = false;
		EnumSceneType ChangeSceneType = Singleton<DataManager>.Instance.ChangeSceneType;
		string sceneName = Singleton<SceneManager>.Instance.GetSceneName(ChangeSceneType);
		int displayProgress = 0;
		AsyncOperation op = Application.LoadLevelAsync(sceneName);
		op.allowSceneActivation = false;
		int toProgress;
		while (op.progress < 0.9f)
		{
			toProgress = (int)op.progress * 100;
			while (displayProgress < toProgress)
			{
				displayProgress++;
				yield return new WaitForEndOfFrame();
			}
		}
		toProgress = 100;
		while (displayProgress < toProgress)
		{
			displayProgress++;
			yield return new WaitForEndOfFrame();
		}
		op.allowSceneActivation = true;
	}
}

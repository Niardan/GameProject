using Assets.Scripts.Basic;
using Assets.Scripts.Mining.Controllers;
using Assets.Scripts.Mining.Views;
using Assets.Scripts.ViewDescription;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets
{
	public class GameManager
	{
		private GameObject _menu;

		private MiningSceneViewsDescription _miningSceneViewsDescription;
		private string _key;


		public GameManager(GameObject menu, MiningSceneViewsDescription miningSceneViewsDescription)
		{
			_menu = menu;
			_miningSceneViewsDescription = miningSceneViewsDescription;
		}

		public void LoadScene(string sceneName)
		{

		}

		public void LoadMiningScene(string key)
		{
			_menu.SetActive(false);
			_key = key;
			SceneManager.sceneLoaded += OnSceneLoaded;
			SceneManager.LoadScene("Mining", LoadSceneMode.Additive);
		}

		public void UnloadScene(string sceneName)
		{
			SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
			SceneManager.UnloadSceneAsync("Mining");

		}

		private void SceneManager_sceneUnloaded(Scene arg0)
		{
			_menu.SetActive(true);
		}

		private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
			GameObject sceneGo = GameObject.Find("MiningView");
			ISceneView sceneView = sceneGo.GetComponent<GameMiningView>();
			var gameMiningController = new GameMiningController((GameMiningView)sceneView, _miningSceneViewsDescription[_key], this);
			_menu.SetActive(false);
		}
	}
}
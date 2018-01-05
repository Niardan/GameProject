using Assets;
using Assets.Scripts.ViewDescription;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
	[SerializeField] private Button _color4;
	[SerializeField] private GameObject _canvas;
	[SerializeField] private MiningSceneViewsDescription _miningSceneViewsDescription;
	[SerializeField] private TMP_Dropdown _dropdovn;
	private string _key;
	private GameManager _gameManager;
	void Start()
	{
		_gameManager = new GameManager(_canvas, _miningSceneViewsDescription);
		_color4.onClick.AddListener(OnStartClick);

	}


	private void OnStartClick()
	{
		_gameManager.LoadMiningScene(_dropdovn.options[_dropdovn.value].text);
	}






	// Update is called once per frame
	void Update()
	{

	}
}

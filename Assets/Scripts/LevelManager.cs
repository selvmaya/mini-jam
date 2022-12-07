using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Tools.Helpers;
using Tools.Types;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manager that lives in each scene and lets us switch.
/// </summary>
public class LevelManager : Singleton<LevelManager>
{
	[SerializeField, Required, AssetsOnly] private SceneReference menuScene;
	[SerializeField, Required, AssetsOnly] private SceneReference levelScene;

	[SerializeField, Min(0.001f)] private float transitionDelay = 0.1f;
	[SerializeField, Required, SceneObjectsOnly] private Image transitionImage;

	private bool _waking;
	private bool _transitioning;
	private float _transitionStartTime;
	private SceneReference _transitionTargetScene;

	private void Start()
	{
		_waking = true;
		_transitioning = true;
	}

	private void Update()
	{
		if (!_transitioning) return;

		float rawT = _transitionStartTime.TimeSince() / transitionDelay;
		if (_waking) rawT = 1 - rawT;
		transitionImage.color = Color.Lerp(Color.clear, Color.black, rawT);
		if (_transitionStartTime.TimeSince() >= transitionDelay)
		{
			_transitioning = false;

			if (!_waking)
			{
				Debug.Log($"Loading scene: '{_transitionTargetScene.ScenePath}'");
				SceneManager.LoadScene(_transitionTargetScene.ScenePath);
				_transitionTargetScene = null;
			}
		}
	}

	[UsedImplicitly] // UI
	public void GoToMenu()
	{
		TransitionToScene(menuScene);
	}
	[UsedImplicitly] // UI
	public void GoToLevel()
	{
		TransitionToScene(levelScene);
	}

	private void TransitionToScene(SceneReference scene)
	{
		if (_transitioning) return;

		_waking = false;
		_transitioning = true;
		_transitionStartTime = Time.time;
		_transitionTargetScene = scene;
	}
}
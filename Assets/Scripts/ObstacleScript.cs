using Tools.Helpers;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
	[SerializeField] private float despawnDelay;
	[SerializeField] private AnimationCurve despawnCurve = new AnimationCurve();

	private float _spawnTime;

	private void Start()
	{
		_spawnTime = Time.time;
	}

	private void Update()
	{

	}

	private void FixedUpdate()
	{
		if (_spawnTime.TimeSince() >= despawnDelay)
		{
			Destroy(gameObject);
		}
	}
}
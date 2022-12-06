using Tools.Helpers;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
	[SerializeField, Min(0.001f)] private float despawnDelay;

	private float _spawnTime;
	private Vector3 _spawnScale;

	private void Start()
	{
		_spawnTime = Time.time;
		_spawnScale = transform.localScale;
	}

	private void Update()
	{
		float rawT = _spawnTime.TimeSince() / despawnDelay;
		transform.localScale = Vector3.Slerp(_spawnScale, Vector3.zero, rawT);
	}

	private void FixedUpdate()
	{
		if (_spawnTime.TimeSince() >= despawnDelay)
		{
			Destroy(gameObject);
		}
	}
}
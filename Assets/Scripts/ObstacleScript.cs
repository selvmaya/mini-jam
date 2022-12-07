using Tools.Helpers;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
	[SerializeField, Min(0.001f)] private float despawnStartDelay = 0.3f;
	[SerializeField, Min(0.001f)] private float despawnDuration = 0.2f;

	private float _spawnTime;
	private Vector3 _spawnScale;

	private void Start()
	{
		_spawnTime = Time.time;
		_spawnScale = transform.localScale;
	}

	private float DespawnStartTime => _spawnTime + despawnStartDelay;

	private void Update()
	{
		if (_spawnTime.TimeSince() >= despawnStartDelay)
		{
			float rawT = DespawnStartTime.TimeSince() / despawnDuration;
			transform.localScale = Vector3.Slerp(_spawnScale, Vector3.zero, rawT);
		}
	}

	private void FixedUpdate()
	{
		if (DespawnStartTime.TimeSince() >= despawnDuration)
		{
			Destroy(gameObject);
		}
	}
}
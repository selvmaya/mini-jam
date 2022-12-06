using Tools.Types;
using UnityEngine;

namespace Spawners
{
	public class ObstacleSpawnerScript : Singleton<ObstacleSpawnerScript>
	{
		private Camera _cam;
		private Camera Cam => _cam != null ? _cam : _cam = Camera.main;

		[SerializeField] private GameObject obstacle;

		public void TrySpawnAtPos(Vector2 aimPos)
		{
			Instantiate(obstacle, Cam.ScreenToWorldPoint(aimPos), Quaternion.identity);
		}
	}
}
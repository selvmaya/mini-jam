using Sirenix.OdinInspector;
using Tools.Types;
using UnityEngine;

namespace Spawners
{
	public class ObstacleSpawnerScript : Singleton<ObstacleSpawnerScript>
	{
		private Camera _cam;
		private Camera Cam => _cam != null ? _cam : _cam = Camera.main;

		[SerializeField, Required] private GameObject obstacle;

		public void TrySpawnAtPos(Vector2 aimPos)
		{
			Vector3 pos = Cam.ScreenToWorldPoint(aimPos);
			Instantiate(obstacle, pos, Quaternion.identity);
		}
	}
}
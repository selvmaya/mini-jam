using System;
using Sirenix.OdinInspector;
using Tools.Helpers;
using Tools.Types;
using UnityEngine;

namespace Spawners
{
	public class ObstacleSpawnerScript : Singleton<ObstacleSpawnerScript>
	{
		[SerializeField] private float spawnCooldown = 0.1f;
		[SerializeField, Required] private GameObject obstacle;


		private Camera _cam;
		private Camera Cam => _cam != null ? _cam : _cam = Camera.main;

		public bool CanSpawnBlocks { private get; set; }

		private void Start()
		{
			CanSpawnBlocks = true;
		}

		public void TrySpawnAtPos(Vector2 aimPos)
		{
			if (CanSpawnBlocks)
			{
				_blockSpawnBuffer = true;
				_lastAimPos = aimPos;
			}
		}

		private bool _blockSpawnBuffer;
		private Vector2 _lastAimPos;

		private void FixedUpdate()
		{
			if (_blockSpawnBuffer && _lastSpawnTime.TimeSince() >= spawnCooldown)
			{
				SpawnBlock();
			}
		}

		private float _lastSpawnTime;

		private void SpawnBlock()
		{
			_blockSpawnBuffer = false;
			_lastSpawnTime = Time.time;
			Vector2 pos = Cam.ScreenToWorldPoint(_lastAimPos);
			Instantiate(obstacle, pos, Quaternion.identity);
		}
	}
}
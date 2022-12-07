using System;
using System.Collections.Generic;
using System.Linq;
using Tools.Helpers;
using Tools.Types;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
	public class ArrowSpawnerScript : Singleton<ArrowSpawnerScript>
	{
		[SerializeField] private float spawnDelay = 1f;
		[SerializeField] private List<ArrowContainer> arrows = new List<ArrowContainer>();

		private float _lastSpawnTime;
		public bool CanSpawnArrows { private get; set; } // also set by heart when it dies

		private void Start()
		{
			CanSpawnArrows = true;
		}

		private void FixedUpdate()
		{
			if (CanSpawnArrows && _lastSpawnTime.TimeSince() >= spawnDelay)
			{
				SpawnArrow();
			}
		}

		private void SpawnArrow()
		{
			ArrowContainer arrow = GetRandomArrow();
			Vector2 pos = GetRandomPosition();
			GameObject arrowObject = Instantiate(arrow.prefab, pos, Quaternion.identity);
			Rigidbody2D arrowRb = arrowObject.GetComponent<Rigidbody2D>();
			arrowRb.velocity = -pos.normalized * arrow.moveSpeed;
			_lastSpawnTime = Time.time;
		}

		private ArrowContainer GetRandomArrow()
		{
			float total = 0;
			List<(float min, ArrowContainer arrow)> arrowsData = new List<(float min, ArrowContainer arrow)>();
			foreach (ArrowContainer arrow in arrows)
			{
				arrowsData.Add((total, arrow));
				total += arrow.relativeChance;
			}
			float randomNum = Random.Range(0, total);
			foreach ((float min, ArrowContainer arrow) data in arrowsData)
			{
				if (randomNum >= data.min)
				{
					return data.arrow;
				}
			}
			Debug.LogWarning("GetRandomArrow() error?");
			return arrows.First();
		}

		private Camera _cam;
		private Camera Cam => _cam != null ? _cam : _cam = Camera.main;
		private Vector2 GetRandomPosition()
		{
			float height = Cam.orthographicSize;
			Vector2 corner = new Vector2(Cam.aspect * height, height);
			Vector2 randomPos = corner.magnitude * Random.insideUnitCircle.normalized;
			return randomPos;
		}

		private static int RandSignedInt => Random.value > 0.5f ? 1 : -1;
		private static bool RandBool => Random.value > 0.5f;
	}
}
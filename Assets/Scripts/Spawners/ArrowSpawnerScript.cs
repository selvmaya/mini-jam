using System.Collections.Generic;
using System.Linq;
using Tools.Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
	public class ArrowSpawnerScript : MonoBehaviour
	{
		[SerializeField] private float spawnDelay = 1f;
		[SerializeField] private List<ArrowContainer> arrows = new List<ArrowContainer>();

		private float _lastSpawnTime;

		private void FixedUpdate()
		{
			if (_lastSpawnTime.TimeSince() >= spawnDelay)
			{
				SpawnArrow();
			}
		}

		private void SpawnArrow()
		{
			ArrowContainer arrow = GetRandomArrow();
			(Vector2 pos, Vector2 dir) = GetRandomVector();
			GameObject arrowObject = Instantiate(arrow.prefab, pos, Quaternion.identity);
			Rigidbody2D arrowRb = arrowObject.GetComponent<Rigidbody2D>();
			arrowRb.velocity = dir * arrow.moveSpeed;
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
		private (Vector2 pos, Vector2 dir) GetRandomVector()
		{
			float height = Cam.orthographicSize;
			Vector2 size = new Vector2(height * Cam.aspect, height);
			Vector2 cornerOffset = size / 2;
			Vector2Int swapper = new Vector2Int(RandSignedInt, RandSignedInt);
			Vector2 pos = cornerOffset * swapper;
			Vector2 dir = -pos.normalized;
			return (pos, dir);
		}

		private static int RandSignedInt => RandBool ? 1 : -1;
		private static bool RandBool => Random.value > 0;
	}
}
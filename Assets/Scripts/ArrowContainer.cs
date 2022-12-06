using Sirenix.OdinInspector;
using UnityEngine;

public class ArrowContainer : ScriptableObject
{
	[field: SerializeField, AssetsOnly] public GameObject prefab;
	[field: SerializeField, AssetsOnly] public float moveSpeed;
	[field: SerializeField, AssetsOnly] public float relativeChance;
}
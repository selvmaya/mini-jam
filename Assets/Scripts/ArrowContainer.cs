using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New ArrowContainer", menuName = "ArrowContainer")]
public class ArrowContainer : ScriptableObject
{
	[field: SerializeField, Required, AssetsOnly] public GameObject prefab;

	[field: ValidateInput("@moveSpeed > 0f", "Arrow does not move.", InfoMessageType.Warning)]
	[field: SerializeField, Min(0)] public float moveSpeed = 1f;

	[field: ValidateInput("@relativeChance > 0f", "Arrow cannot spawn.")]
	[field: SerializeField, Min(0)] public float relativeChance = 1f;
}
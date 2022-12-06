using UnityEngine;

/// <summary>
/// Heart can be damaged by error. Defensive objective for the player.
/// </summary>
public class HeartScript : MonoBehaviour
{
	private AudioSource _audio;
	private AudioSource Audio => _audio != null ? _audio : _audio = GetComponent<AudioSource>();

	[field: SerializeField] public int Health { get; private set; } = 10;

	public void Damage(int amount)
	{
		Health -= amount;
		if (Health <= 0)
		{

		}
	}
}
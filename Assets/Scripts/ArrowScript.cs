using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Damaging projectile that spawns to hit the defensive objective (heart).
/// </summary>
[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(AudioSource))]
public class ArrowScript : MonoBehaviour
{
	[SerializeField, Required] private AudioClip sfx;
	[SerializeField, ReadOnly] private int damageAmount = 1;

	private AudioSource _audio;
	private AudioSource Audio => _audio != null ? _audio : _audio = GetComponent<AudioSource>();

	private Rigidbody2D _rb;
	private Rigidbody2D Rb => _rb != null ? _rb : _rb = GetComponent<Rigidbody2D>();

	private void Awake()
	{
		Audio.clip = sfx;
	}

	private void Update()
	{
		Vector2 travelDirection = Rb.velocity.normalized;
		transform.rotation = Quaternion.LookRotation(travelDirection);
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		GameObject other = col.gameObject;
		// if other == wall: destroy
		// if other == heaart: damaage
		ArrowScript arrow = other.GetComponent<ArrowScript>();
		if (arrow != null)
		{
			return; // ignore other arrows
		}

		HeartScript heart = other.GetComponent<HeartScript>();
		if (heart != null)
		{
			heart.Damage(damageAmount);
		}

		Audio.Play();
		Rb.velocity = Vector2.zero;
		StartCoroutine(DestroyAfterSfx());
	}

	private IEnumerator DestroyAfterSfx()
	{
		yield return new WaitUntil(() => !Audio.isPlaying);
		Destroy(gameObject);
	}
}
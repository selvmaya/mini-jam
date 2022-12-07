using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Damaging projectile that spawns to hit the defensive objective (heart).
/// </summary>
[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
public class ArrowScript : MonoBehaviour
{
	[SerializeField] private int damageAmount = 1;
	[SerializeField, Required] private AudioClip sfx;

	private AudioSource _audio;
	private AudioSource Audio => _audio != null ? _audio : _audio = GetComponent<AudioSource>();

	private Rigidbody2D _rb;
	private Rigidbody2D Rb => _rb != null ? _rb : _rb = GetComponent<Rigidbody2D>();

	private SpriteRenderer _sprite;
	private SpriteRenderer Sprite => _sprite != null ? _sprite : _sprite = GetComponent<SpriteRenderer>();

	private void Awake()
	{
		Audio.clip = sfx;
	}

	private void Start()
	{
		Vector2 travelDirection = Rb.velocity.normalized;
		transform.rotation = Quaternion.FromToRotation(Vector2.up, travelDirection);
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
		Sprite.enabled = false;
		yield return new WaitUntil(() => !Audio.isPlaying);
		Destroy(gameObject);
	}
}
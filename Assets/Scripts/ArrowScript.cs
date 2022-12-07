using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
	private SpriteRenderer Sprite => _sprite != null ? _sprite : _sprite = GetComponentInChildren<SpriteRenderer>();
	private Light2D _light;
	private Light2D Light => _light != null ? _light : _light = GetComponentInChildren<Light2D>();

	private BoxCollider2D _box;
	private BoxCollider2D Box => _box != null ? _box : _box = GetComponent<BoxCollider2D>();

	private void Start()
	{
		Audio.clip = sfx;
		Vector2 travelDirection = Rb.velocity.normalized;
		transform.rotation = Quaternion.FromToRotation(Vector2.up, travelDirection);
		_hasAlreadyCollided = false;
	}

	private bool _hasAlreadyCollided;
	private void FixedUpdate()
	{
		if (_hasAlreadyCollided) return;

		Collider2D col = Physics2D.OverlapBox(transform.position, Box.size, transform.rotation.eulerAngles.z, LayerMask.GetMask("Default"));
		if (col == null) return;

		_hasAlreadyCollided = true;
		GameObject other = col.gameObject;
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
		Rb.simulated = false;
		Box.enabled = false;
		Sprite.enabled = false;
		Light.enabled = false;
		StartCoroutine(DestroyAfterSfx());
	}

	private IEnumerator DestroyAfterSfx()
	{
		yield return new WaitUntil(() => !Audio.isPlaying);
		Destroy(gameObject);
	}
}
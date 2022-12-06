using Spawners;
using Tools.Types;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
	public class InputManager : Singleton<InputManager>
	{
		private ObstacleSpawnerScript _spawner;
		private ObstacleSpawnerScript Spawner => _spawner != null ? _spawner : _spawner = ObstacleSpawnerScript.Instance;

		private InputActions _input;

		protected override void Awake()
		{
			base.Awake();
			_input = new InputActions();
			_input.Gameplay.Aim.performed += AimInput;
			_input.Gameplay.Click.performed += ClickInput;
			_input.Enable();
		}
		private void OnDisable()
		{
			_input.Disable();
			_input = null;
		}

		private Vector2 _aimPos;
		private void AimInput(InputAction.CallbackContext ctx)
		{
			if (ctx.performed) _aimPos = ctx.ReadValue<Vector2>();
		}
		private void ClickInput(InputAction.CallbackContext ctx)
		{
			Spawner.TrySpawnAtPos(_aimPos);
		}
	}
}
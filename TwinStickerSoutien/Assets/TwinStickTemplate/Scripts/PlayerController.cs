using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private Rigidbody _rigidbody = null;

	[SerializeField]
	private float _moveSpeed = 1f;

	[SerializeField]
	private float _maxSpeed = 10f;

	[SerializeField]
	private float _groundFriction = 1f;

	private Camera _camera = null;
	private Vector3 _inputMovement = Vector3.zero;

	private void Awake()
	{
		_camera = LevelReferences.Instance.Camera;
	}

	private void Update()
	{
		_inputMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
	}

	private void FixedUpdate()
	{
		Vector3 velocity = _rigidbody.velocity;

		bool isInputActivated = _inputMovement != Vector3.zero;

		if (isInputActivated == true)
		{
			velocity = _inputMovement * _moveSpeed;
		}
		else
		{
			if (_rigidbody.velocity != Vector3.zero)
			{
				velocity = Vector3.Lerp(velocity, Vector3.zero, _groundFriction);
			}
		}

		velocity = Vector3.ClampMagnitude(velocity, _maxSpeed);

		_rigidbody.velocity = velocity;

		Ray worldMousePosition = _camera.ScreenPointToRay(Input.mousePosition);

		bool result = Physics.Raycast(worldMousePosition, out RaycastHit hit);

		if (result == true)
		{
			Vector3 hitPoint = hit.point;
			hitPoint.y = transform.position.y;
			Quaternion lookRotation = Quaternion.LookRotation(hitPoint - transform.position);
			transform.rotation = lookRotation;
		}
	}
}

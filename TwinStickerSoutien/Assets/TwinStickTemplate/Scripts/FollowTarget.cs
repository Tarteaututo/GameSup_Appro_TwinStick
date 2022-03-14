using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    private Transform _target = null;

    [SerializeField]
	private float _followSpeed = 1f;

	[SerializeField]
	private float _distanceToRest = 1f;

	private void LateUpdate()
	{
		var targetPosition = _target.position;
		targetPosition.y = transform.position.y;

		if (Vector3.Distance(transform.position, targetPosition) > _distanceToRest)
		{
			transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _followSpeed);
		}
	}
}

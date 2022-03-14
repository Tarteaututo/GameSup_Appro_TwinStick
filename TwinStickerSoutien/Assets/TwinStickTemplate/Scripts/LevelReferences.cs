using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelReferences : MBSingleton<LevelReferences>
{
	[SerializeField]
	private Camera _camera = null;

	public Camera Camera => _camera;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class FixingLeanTouch : MonoBehaviour
{
    [SerializeField] private LeanTouch leanTouch;
    [SerializeField] private LayerMask theIntendedLayer;

    public void FixTheLeanTouch()
    {
        leanTouch.Clear();
        leanTouch.enabled = false;
    }

	void OnEnable()
	{
		LeanTouch.OnFingerTap += HandleFingerTap;
	}

	void OnDisable()
	{
		LeanTouch.OnFingerTap -= HandleFingerTap;
	}

	void HandleFingerTap(LeanFinger finger)
	{
		if (finger.IsOverGui)
		{
			Debug.Log("You just tapped the screen on top of the GUI!");
		}
	}
}

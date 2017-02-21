// -------------------------------------------
// Control Freak 2
// Copyright (C) 2013-2016 Dan's Game Tools
// http://DansGameTools.com
// -------------------------------------------

//! \cond

using UnityEngine;
using UnityEngine.EventSystems;

namespace ControlFreak2.UI
{
public class IgnoreGraphicRaycasts : MonoBehaviour, ICanvasRaycastFilter  
	{
	bool ICanvasRaycastFilter.IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
		return false;
		}

	}
}

//! \endcond


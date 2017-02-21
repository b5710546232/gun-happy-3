// -------------------------------------------
// Control Freak 2
// Copyright (C) 2013-2016 Dan's Game Tools
// http://DansGameTools.com
// -------------------------------------------

//! \cond

using UnityEngine;

using System.Collections.Generic;


namespace ControlFreak2.Internal
{

public interface ISpriteAnimator
	{
	void AddUsedSprites			(ISpriteOptimizer optimizer);
	void OnSpriteOptimization	(ISpriteOptimizer optimizer);
	MonoBehaviour GetComponent	();
	}

// ----------------------
public interface ISpriteOptimizer
	{
	Sprite	GetOptimizedSprite	(Sprite oldSprite);
	void	AddSprite			(Sprite sprite);
	}

}

//! \endcond

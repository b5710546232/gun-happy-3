// -------------------------------------------
// Control Freak 2
// Copyright (C) 2013-2016 Dan's Game Tools
// http://DansGameTools.com
// -------------------------------------------

#if UNITY_4_6 || UNITY_4_7 || UNITY_4_8 || UNITY_4_9 
	#define UNITY_PRE_5_0
#endif

#if UNITY_PRE_5_0 || UNITY_5_0 
	#define UNITY_PRE_5_1
#endif

#if UNITY_PRE_5_1 || UNITY_5_1 
	#define UNITY_PRE_5_2
#endif

#if UNITY_PRE_5_2 || UNITY_5_2 
	#define UNITY_PRE_5_3
#endif

#if UNITY_PRE_5_3 || UNITY_5_3 
	#define UNITY_PRE_5_4
#endif

#if UNITY_PRE_5_4 || UNITY_5_4 
	#define UNITY_PRE_5_5
#endif

#if UNITY_PRE_5_5 || UNITY_5_5 
	#define UNITY_PRE_5_6
#endif

#if UNITY_PRE_5_6 || UNITY_5_6 
	#define UNITY_PRE_5_7
#endif


#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.IO;

namespace ControlFreak2Editor
{

public class ConfigManager : EditorWindow
	{
	public const string 
		CF1_SYMBOL		= "CONTROL_FREAK_INSTALLED",
		CF2_SYMBOL		= "CF2_INSTALLED",
		//NGUI_SYMBOL		= "CF_NGUI",
		//DAIKON_SYMBOL	= "CF_DAIKON",
		CF_FORCE_MOBILE_MODE	= "CF_FORCE_MOBILE_MODE",
		CF_DONT_FORCE_MOBILE_MODE_IN_EDITOR = "CF_DONT_FORCE_MOBILE_MODE_IN_EDITOR";

		
		
		

	// ---------------
	static public bool AreControlFreakSymbolsDefined()
		{
		return (IsSymbolDefinedForAll(CF2_SYMBOL) == 1);
		}


	// --------------------
	static public bool IsBuildTargetGroupSupported(BuildTargetGroup btg)
		{
		if (((int)btg < 0) || (btg == BuildTargetGroup.Unknown))
			return false;		
	
		string btgName = btg.ToString().ToLower();
		if (btgName.Length == 0)
			return false;

//#if !UNITY_PRE_5_0
//		if (btg == BuildTargetGroup.iPhone)			return false;
//#else

#if !UNITY_PRE_5_4
		if (btgName == "wp8")				return false;
		if (btgName == "blackberry")		return false;
		if (btgName == "webplayer")		return false;
#endif

#if !UNITY_PRE_5_5
		if (btgName == "ps3")			return false;
		if (btgName == "xbox360")		return false;
#endif



//BuildTargetGroup.Metro (dep)
//BuildTargetGroup.WebPlayer		
//BuildTargetGroup.WP8
//BuildTargetGroup.iPhone

		return true;
		}


	// ------------------
	static public void AddDefaultControlFreakSymbols()
		{
		AddSymbolToAll(CF2_SYMBOL);

		AddSymbol(ConfigManager.CF_FORCE_MOBILE_MODE, BuildTargetGroup.Android);
		AddSymbol(ConfigManager.CF_FORCE_MOBILE_MODE, BuildTargetGroup.PSM);
		AddSymbol(ConfigManager.CF_FORCE_MOBILE_MODE, BuildTargetGroup.Tizen);

#if UNITY_PRE_5_0
		AddSymbol(ConfigManager.CF_FORCE_MOBILE_MODE, BuildTargetGroup.iPhone);
#else
		AddSymbol(ConfigManager.CF_FORCE_MOBILE_MODE, BuildTargetGroup.iOS);
		AddSymbol(ConfigManager.CF_FORCE_MOBILE_MODE, BuildTargetGroup.WSA);
#endif

#if UNITY_PRE_5_4
		AddSymbol(ConfigManager.CF_FORCE_MOBILE_MODE, BuildTargetGroup.WP8);
		AddSymbol(ConfigManager.CF_FORCE_MOBILE_MODE, BuildTargetGroup.BlackBerry);
#endif
		}


	// -------------------------
	static public void RemoveAllControlFreakSymbols()
		{
		RemoveSymbolFromAll(CF2_SYMBOL);
		//RemoveSymbolFromAll(CF_DEV_SYMBOL);
		RemoveSymbolFromAll(CF_FORCE_MOBILE_MODE);
		RemoveSymbolFromAll(CF_DONT_FORCE_MOBILE_MODE_IN_EDITOR);
		

		// TODO : remove all!
		}
		
		
	// -----------------
	static public void SetSymbolForAll(string symbol, bool state)
		{
		if (state)
			AddSymbolToAll(symbol);
		else
			RemoveSymbolFromAll(symbol);
		}

		
	// -----------------
	static public int IsSymbolDefinedForAll(string symbol)
		{
		int combinedState = -1;
		foreach (BuildTargetGroup targetGroup in System.Enum.GetValues(typeof(BuildTargetGroup)))
			{
			if (!IsBuildTargetGroupSupported(targetGroup)) // == BuildTargetGroup.Unknown)
				continue;

			int state = IsSymbolDefined(symbol, (BuildTargetGroup)targetGroup) ? 1 : 0;

			if ((state != combinedState) && (combinedState != -1))
				return -1;

			combinedState = state;	
			}

		return ((combinedState != -1) ? combinedState : 0);
		}



	// -----------------
	static public void AddSymbolToAll(string symbol)
		{
		foreach (BuildTargetGroup targetGroup in System.Enum.GetValues(typeof(BuildTargetGroup)))
			{
			if (!IsBuildTargetGroupSupported(targetGroup)) // == BuildTargetGroup.Unknown)
				continue;
	

			AddSymbol(symbol, (BuildTargetGroup)targetGroup);
			}
		}
		
	// -------------
	static public void RemoveSymbolFromAll(string symbol)
		{
		foreach (BuildTargetGroup targetGroup in System.Enum.GetValues(typeof(BuildTargetGroup)))
			{
			if (!IsBuildTargetGroupSupported(targetGroup)) //targetGroup == BuildTargetGroup.Unknown)
				continue;

			RemoveSymbol(symbol, (BuildTargetGroup)targetGroup);
			}
		}

	// ------------------
	static private string[] GetSymbols(BuildTargetGroup tgt)
		{
		string symbolsStr = PlayerSettings.GetScriptingDefineSymbolsForGroup(tgt);
		if (symbolsStr == "")
			return null;

		string[] symbols = symbolsStr.Split(new char[]{',', ';'}, System.StringSplitOptions.RemoveEmptyEntries);
			
		for (int i = 0; i < symbols.Length; ++i)
			symbols[i] = symbols[i].Trim();

		return symbols;
		}


	// -------------
	static public bool IsSymbolDefined(string symbolStr, UnityEditor.BuildTargetGroup tgt)
		{
		if (!IsBuildTargetGroupSupported(tgt))
			return false;

		string[] symbols = GetSymbols(tgt);
		if (symbols == null) 
			return false;

		foreach (string sym in symbols)
			{
			if (sym.Equals(symbolStr))
				return true;
			}

		return false;
		}
		

	// ----------
	static public void RemoveSymbol(string symbol, UnityEditor.BuildTargetGroup tgt)
		{	
		string symbolsStr = "";

		string[] symbols = GetSymbols(tgt);
		if (symbols == null) 
			return;

		foreach (string sym in symbols)
			{
			if ((sym.Length == 0) || (sym == symbol))	
				continue;

			symbolsStr += ((symbolsStr.Length > 0) ? (";" + sym) : sym);			
			}
	
		PlayerSettings.SetScriptingDefineSymbolsForGroup(tgt, symbolsStr);		
		}
		


	// ------------
	static public void AddSymbol(string symbol, UnityEditor.BuildTargetGroup tgt)
		{
////#if !UNITY_PRE_5_4
//		if (((int)tgt == 15) || ((int)tgt == 16))		// Skip WP8 and Blackberry
//			return;
//#endif

		if (!IsBuildTargetGroupSupported(tgt))
			return;

		if (IsSymbolDefined(symbol, tgt))
			return;

		string symbolsStr = PlayerSettings.GetScriptingDefineSymbolsForGroup(tgt).Trim();

		if (symbolsStr.Length == 0)
			symbolsStr = symbol;
		else
			symbolsStr += (";" + symbol);
	
		PlayerSettings.SetScriptingDefineSymbolsForGroup(tgt, symbolsStr);
		}

	}
}

#endif

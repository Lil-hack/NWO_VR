using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCamera : MonoBehaviour {
	public UnityStandardAssets.ImageEffects.ColorCorrectionCurves colorCur; 
	// Use this for initialization
	public void Black()
	{
		colorCur.saturation = 0f;
		
	}
}

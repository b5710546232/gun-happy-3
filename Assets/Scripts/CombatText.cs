using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatText : MonoBehaviour {

	private float speed;
	private Vector3 direction;
	private float fadeTime;

	public AnimationClip criticalAnim;
	private bool isCritical;

	
	
	// Update is called once per frame
	void Update () {
		if(!isCritical){
			float translation = speed*Time.deltaTime;
			transform.Translate(direction*translation);
		}
		
	}

	public void Init(float speed,Vector3 direction,float fadeTime,bool isCritical)
	{
		this.speed = speed;
		this.direction = direction;
		this.fadeTime = fadeTime;
		this.isCritical = isCritical;
		if(isCritical){
			GetComponent<Animator>().SetTrigger("isCritical");
			StartCoroutine("Critical");
		}
		else{
			StartCoroutine("FadeOut");
		}
		
	}
	private IEnumerator Critical(){
		yield return new WaitForSeconds(criticalAnim.length);
		isCritical = false;
		StartCoroutine("FadeOut");
	}

	private IEnumerator FadeOut(){

		float startAlpha = GetComponent<Text>().color.a;

		float rate = 1.0f/ fadeTime;
		float progress = 0.0f;

		while(progress< 1.0){
			Color tmpColor = GetComponent<Text>().color;

			GetComponent<Text>().color = new Color(tmpColor.r,tmpColor.g,tmpColor.b,Mathf.Lerp(startAlpha,0,progress));
			progress += rate*Time.deltaTime;

			yield return null;
		}
		Destroy(gameObject);
	}
}

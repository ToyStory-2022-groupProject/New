/// <summary>
/// 
/// Cymbal Sound
/// 
/// This is a simple script JUST for showcase purposes (test scene). It coordinates some sounds based on variable iterations and pre-made transitions (animController demo).
/// 
/// NOTE> I do not give support for this script. Feel free to tweak and use it as a base for your own sounds/transitions.
/// 
/// </summary>
/// 

using UnityEngine;
using System.Collections;

public class CymbalSound : MonoBehaviour {
	
	[SerializeField]AudioClip[] sounds;
	private AudioSource _audioSource;


	void Awake(){

		_audioSource = GetComponent <AudioSource> ();

	}

	// Use this for initialization
	void Start () {

	
		if (gameObject.name == "HauntedMonkey-Legacy") {

			StartCoroutine (_legacySound());
				
		} else {

			StartCoroutine (_mecanimSound());
				
		}

	}

	private IEnumerator _legacySound(){

		while (true) {

						
			if (GetComponent<Animation>().IsPlaying ("Cymbals") && !GetComponent<AudioSource>().isPlaying) {
			
						
				_audioSource.clip = sounds[0];
				GetComponent<AudioSource>().Play();
			
						
			}else if (GetComponent<Animation>().clip.name != "Cymbals" && GetComponent<AudioSource>().isPlaying){

			
				GetComponent<AudioSource>().Stop();
			
			}
			yield return null;
				
		}

	}

	private IEnumerator _mecanimSound(){


		Animator thisAnim = GetComponent<Animator> ();
		bool laughOnce = false;

		while (true) {


			if ( !thisAnim.GetCurrentAnimatorStateInfo(0).IsName ("Breath") && !GetComponent<AudioSource>().isPlaying){

				if (laughOnce){
					laughOnce =! laughOnce;
				}

					_audioSource.clip = sounds[0];
					GetComponent<AudioSource>().Play ();

			}else if(!GetComponent<AudioSource>().isPlaying && thisAnim.GetCurrentAnimatorStateInfo(0).IsName ("Breath")){

				if (!laughOnce){
					_audioSource.clip = sounds[ Random.Range (1, 3)];
					GetComponent<AudioSource>().Play ();
					laughOnce =! laughOnce;
				}
			}
			yield return null;
		}
	}
		

	
}

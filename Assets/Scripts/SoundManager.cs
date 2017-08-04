using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	[SerializeField]
	public static AudioSource musicSource;

	[SerializeField]
	public static AudioSource sfxSource;

	static Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

	// Use this for initialization
	void Start () {

		sfxSource = gameObject.GetComponents<AudioSource> ()[0];

		AudioClip[] clips = Resources.LoadAll<AudioClip> ("Audio") as AudioClip[];

		foreach (AudioClip clip in clips) {
			audioClips.Add (clip.name, clip);
		}
	}
		
	public static void PlaySfx(string name)
	{
		sfxSource.PlayOneShot (audioClips [name]);
	}
}

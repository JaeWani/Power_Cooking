using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip audioClip;
    public string key;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;

    public List<Sound> sounds = new List<Sound>();
    public Dictionary<string, Sound> soundDictionary = new Dictionary<string, Sound>();

    private void Awake()
    {
        if(soundManager == null) 
        {
            soundManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        foreach(var item in sounds)
        {
            string key = item.key;
            
            soundDictionary.Add(key,item);
        }
    }

    private void _PlaySound(string key)
    {
        var obj = new GameObject("Audio Source");
        var audioSource = obj.AddComponent<AudioSource>();

        StartCoroutine(play());
        IEnumerator play()
        {
            float time = soundDictionary[key].audioClip.length;
            audioSource.clip = soundDictionary[key].audioClip;
            audioSource.Play();
            yield return new WaitForSeconds(time);
            Destroy(obj);
        }
    }
    public static void PlaySound(string key) => soundManager._PlaySound(key);

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private List<AudioSource> allSounds;
    public float volume = 0.3f;

    public void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            allSounds = new List<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public AudioSource PlayAudio(AudioClip clip, string gameObjectName, bool isLoop = false, float volume = 0.5f)
    {
        GameObject nObject = new GameObject();
        nObject.name = gameObjectName;
        AudioSource audioSourceComponent = nObject.AddComponent<AudioSource>();
        audioSourceComponent.clip = clip;
        audioSourceComponent.loop = isLoop;
        audioSourceComponent.volume = volume;
        allSounds.Add(audioSourceComponent);
        audioSourceComponent.Play();
        StartCoroutine(WaitForAudio(audioSourceComponent));
        return audioSourceComponent;
    }

    private IEnumerator WaitForAudio(AudioSource source)
    {
        if (source.loop)
        {
            yield return null;
        }
        else
        {
            while (source.isPlaying)
            {
                yield return new WaitForSeconds(0.3f);
            }

            Destroy(source.gameObject);
        }
    }
}
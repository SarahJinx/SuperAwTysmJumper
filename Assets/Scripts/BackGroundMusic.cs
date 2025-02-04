using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    public AudioClip lvl1MainMusic;
    bool isLoop = true;
    public float volume = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayAudio(lvl1MainMusic, "LvL 1 Music", isLoop, volume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

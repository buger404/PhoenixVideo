using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundPlayer
{
    public static List<AudioClip> audios = new List<AudioClip>();
    static SoundPlayer(){
        object[] audio = Resources.LoadAll("sfx");
        foreach(AudioClip au in audio)
            audios.Add(au);
    }
    public static void Play(string tar){
        GameObject go = new GameObject("Audio: " + tar);
        go.transform.position = Vector3.zero;
        go.transform.parent = Camera.main.transform;
        int sndindex =  audios.FindIndex(m => m.name == tar);
        if(sndindex == -1) return;
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = audios[sndindex];
        source.volume = 0.9f + Random.Range(-0.2f,0.2f);
        source.Play();
        GameObject.Destroy(go, source.clip.length);
    }
}


public class AssetsLoader
{
    public static Dictionary<Sprite[],string> data = new Dictionary<Sprite[],string>();
    static AssetsLoader(){
        foreach(string name in Directory.GetFiles("Assets\\Resources"))
            Debug.Log(name);
    }

}
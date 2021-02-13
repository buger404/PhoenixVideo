using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class GroupAnimation : MonoBehaviour
{
    private int frame = 0;
    private Sprite[] aniA,aniB;
    private bool LastBox;
    public string path;
    private float dtime = 0;
    private SpriteRenderer spriteRenderer;
    public int playTime = 0;
    public bool IsPause = false;
    public bool PausePlay = false;
    public float GapTime = 1f;
    private void Awake() {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        aniA = Resources.LoadAll<Sprite>(path + "(a)");
        aniB = Resources.LoadAll<Sprite>(path + "(b)");
    }
    public async Task Reload(string Path,bool isPause = false){
        path = Path;
        aniA = null;
        if(isPause){
            aniA = Resources.LoadAll<Sprite>(path);
        }else{
            aniA = Resources.LoadAll<Sprite>(path + "(a)");
        }
        playTime = 0; IsPause = isPause; frame = 0; PausePlay = false;
        if(path.Contains("deskslam") || path.Contains("p2\\objection")) SoundPlayer.Play("deskslam");
        if(path.Contains("document")) SoundPlayer.Play("evidenceshoop");
        if(path.Contains("damage")) SoundPlayer.Play("whack");
        //if(path.Contains("surprised")) SoundPlayer.Play("whack");
        await Task.Run(() => {
            while(aniA.Length == 0) Thread.Sleep(50);
        });
        if(isPause) return;
        aniB = null;
        aniB = Resources.LoadAll<Sprite>(path + "(b)");
        await Task.Run(() => {
            while(aniB.Length == 0) Thread.Sleep(50);
        });
    }
    void FixedUpdate()
    {
        if(!IsPause){
            if(LastBox != Drama.Saying){
                LastBox = Drama.Saying;
                frame = 0;
            }
        }
        int Slength = aniA.Length;
        if(LastBox && !IsPause) Slength = aniB.Length;
        dtime += Time.deltaTime;
        float sTime = 0.15f;
        if(!LastBox && !IsPause && frame >= Slength - 1) sTime += GapTime;

        if(dtime >= sTime){
            dtime = 0;
            frame++;
            if(frame > Slength - 1) {
                playTime++;
                if(playTime > 10) playTime = 10;
                if(!IsPause) frame = 0; else return;
            }
            if(LastBox && !IsPause){
                spriteRenderer.sprite = aniB[frame];
            }else{
                spriteRenderer.sprite = aniA[frame];
            }
        }
    }

}

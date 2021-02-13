using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Drama : MonoBehaviour
{
    //律师 检察官 法官 吵闹 肃静
    public GameObject s1,s2,s3,s4,s5,shock;
    public GameObject objection1,objection2;
    public GameObject evident,witness;
    public GameObject p1zoom,p2zoom;
    public static bool Saying;
    public static bool IsPlay = false;
    private static GroupAnimation Lastga;
    public AudioSource bgm;
    public Image evidence,witn;
    public static bool IsMouseUp = false;
    private GameObject last;
    public Text title,content;
    public GameObject box;
    public async Task Delay(int time){
        await Task.Run(() => {
            Thread.Sleep(time);
        });
    }
    public void BGM(string name){
        bgm.clip = Resources.Load<AudioClip>("bgm\\" + name);
        bgm.Play();
        bgm.loop = true;
    }
    public void wit(string name){
        witn.sprite = Resources.Load<Sprite>("witness\\" + name);
        witn.SetNativeSize();
        if(last != null && last != witness) last.SetActive(false);
        witness.SetActive(true); last = witness;
    }
    public void evi(string name){
        evidence.sprite = Resources.Load<Sprite>("evidence\\" + name);
        evidence.SetNativeSize();
        if(last != null && last != evident) last.SetActive(false);
        evident.SetActive(true); last = evident;
    }
    public async Task say(string Title,string Content,string snd,bool SkipConfirm = false){
        title.text = Title;
        content.text = "";
        box.SetActive(true);
        Saying = true;
        for(int i = 0;i < Content.Length;i++){
            content.text += Content[i];
            SoundPlayer.Play(snd);
            await Delay(50);
        }
        Saying = false;
        IsMouseUp = false;
        if(!SkipConfirm){
            await Task.Run(() => {
                while(!IsMouseUp) Thread.Sleep(50);
            });
        }else{
            await Delay(700);
        }
        box.SetActive(false);
    }
    public async Task sayAdd(string Content,string snd,bool SkipConfirm = false){
        box.SetActive(true);
        Saying = true;
        for(int i = 0;i < Content.Length;i++){
            content.text += Content[i];
            SoundPlayer.Play(snd);
            await Delay(50);
        }
        Saying = false;
        IsMouseUp = false;
        if(!SkipConfirm){
            await Task.Run(() => {
                while(!IsMouseUp) Thread.Sleep(50);
            });
        }
        box.SetActive(false);
    }
    public async Task p1(string action,int loopcount = 0){
        GroupAnimation ga = s1.transform.Find("p1").GetComponent<GroupAnimation>();
        await ga.Reload("p1\\" + action,loopcount == 1);
        if(last != null && last != s1) last.SetActive(false);
        s1.SetActive(true); last = s1;
        await Task.Run(() => {
            while(ga.playTime < loopcount) Thread.Sleep(50);
        });
        if(loopcount == 1) await Delay(500);
    }
    public async Task p2(string action,int loopcount = 0){
        GroupAnimation ga = s2.transform.Find("p2").GetComponent<GroupAnimation>();
        await ga.Reload("p2\\" + action,loopcount == 1);
        if(last != null && last != s2) last.SetActive(false);
        s2.SetActive(true); last = s2;
        await Task.Run(() => {
            while(ga.playTime < loopcount) Thread.Sleep(50);
        });
        if(loopcount == 1) await Delay(500);
    }
    public async Task p3(string action,int loopcount = 0){
        GroupAnimation ga = s3.transform.Find("p3").GetComponent<GroupAnimation>();
        await ga.Reload("p3\\" + action,loopcount == 1);
        if(last != null && last != s3) last.SetActive(false);
        s3.SetActive(true); last = s3;
        await Task.Run(() => {
            while(ga.playTime < loopcount) Thread.Sleep(50);
        });
        if(loopcount == 1) await Delay(500);
    }
    public async Task noisy(){
        if(last != null) last.SetActive(false);
        GroupAnimation ga = s4.GetComponent<GroupAnimation>();
        SoundPlayer.Play("gallery");
        await ga.Reload(ga.path);
        s4.SetActive(true); last = s4;
        await Task.Run(() => {
            while(ga.playTime < 3) Thread.Sleep(50);
        });
        ga = s5.GetComponent<GroupAnimation>();
        SoundPlayer.Play("gavel");
        await ga.Reload(ga.path);
        s5.SetActive(true); s4.SetActive(false);
        await Task.Run(() => {
            while(ga.playTime < 1) Thread.Sleep(50);
        });
        s5.SetActive(false);
    }
    public async Task shocks(){
        shock.SetActive(true);
        SoundPlayer.Play("badum");
        await Task.Run(() => {
            Thread.Sleep(1000);
        });
        shock.SetActive(false);
    }
    public async Task shockdone(){
        SoundPlayer.Play("dramapound");
        await Task.Run(() => {
            Thread.Sleep(1000);
        });
    }
    public async Task Objection1(){
        objection1.SetActive(true);
        await Task.Run(() => {
            Thread.Sleep(1000);
        });
        objection1.SetActive(false);
    }
    public async Task Objection2(){
        objection2.SetActive(true);
        await Task.Run(() => {
            Thread.Sleep(1000);
        });
        objection2.SetActive(false);
    }

    private async Task Play() {
        await say("404","Click to start.","male");
        await noisy();
        BGM("start");
        await p3("normal");
        await say("法官","本庭就冰棍诈骗案开始审理。","male");
        await p1("normal");
        await say("律师","辩方准备完毕。","male");
        await p2("normal");
        await say("检察官","检方准备完毕。","male");
        await p3("normal");
        await say("法官","那么，请检察官说明案件。","male");
        await p2("document");
        await say("检察官","2月12日晚间，被告冰棍在某群发放了66亿元的红包...","male",true);
        await p3("surprised");
        SoundPlayer.Play("whack");
        await say("法官","6...66亿元！！！！","male");
        await p2("document");
        await say("检察官","是的，问题就在这里，66亿元是被告通过电信诈骗骗来的。","male");
        evi("inter");
        BGM("case");
        await say("检察官","这是案发当晚受骗群的照骗。","male");
        await say("检察官","被告长期通过拍摄自己的女装照片进行网恋诈骗...","male",true);
        bgm.Stop();
        await Objection1();
        await p1("deskslam",1);
        await p1("handsondesk");
        await say("律师","被告的女装照是从 github 拷贝下来的，并不是本人拍摄的。","male");
        SoundPlayer.Play("objection");
        await p1("superobjection",1);
        await p1("pointing");
        BGM("success");
        await say("律师","而且，66亿元也只是开玩笑的，检方的主张从根本上就是错误的。","male");
        await Objection2();
        await p2("objection",1);
        await p2("smirk",1);
        await p2("shrug",1);
        await p2("handondesk");
        await say("检察官","女装照片从哪里来的和诈骗没有关系，66亿元也真实存在，检方将证明这一点。","male");
        await p2("bow",1);
        await p3("headshake",1);
        await p3("normal");
        await say("法官","看来男孩子在外面也要注意保护自己了。","male");
        await Objection1();
        await p3("surprised");
        await shocks();
        await p2("strained");
        await shocks();
        await p1("confident");
        await shockdone();
        await say("律师","辩方也已经掌握了决定性的证据，就是","male",true);
        evi("msg");
        await sayAdd("...","male");
        p1zoom.SetActive(true);
        await sayAdd("这张照片！","male");
        p1zoom.SetActive(false);
        await p1("deskslam",1);
        await p1("superobjection",1);
        await p1("pointing");
        await say("律师","辩方指控阳光加冰为真凶！","male");
        await p2("damage",1);
        await p2("strained");
        await say("检察官","............","male");
        await say("检察官","这不是今天最重要的证人吗！？","male");
        await noisy();
        await p3("warning");
        await say("法官","肃静...！肃静...！","male");
        await say("法官","被告，这是怎么回事？","male");
        wit("冰棍");
        await say("冰棍","人家冤枉啦~群主才是坏淫~","female");
        await say("冰棍","呜呜呜呜呜呜...棍棍好伤心...","female");
        bgm.Stop();
        await p3("warning");
        await say("法官","看来有必要听一听阳光加冰的说法了，","male",true);
        await p3("normal");
        await sayAdd("请证人入庭吧。","male");
        wit("阳光加冰");
        await say("阳光加冰","草。","male");
        await p2("normal");
        await say("检察官","证人，请说明你的姓名和职业。","male");
        wit("阳光加冰");
        await say("阳光加冰","阳光加冰，受骗群的群主。","male");
        await p3("normal");
        await say("法官","那么，请作证吧。","male");
        wit("阳光加冰");
        BGM("ask1");
        await say("阳光加冰","谁搞的，真他妈是个人才。","male");
        await say("阳光加冰","我不记得我换了头像。","male");
        await say("阳光加冰","我说我携款潜逃只是开玩笑而已。","male");
        await Objection1();
        await p1("deskslam",1);
        await p1("handsondesk");
        await say("律师","为什么要开这种玩笑？","male");
        await p2("strained");
        await say("检察官","............","male");
        await p2("smirk",1);
        bgm.Stop();
        SoundPlayer.Play("realization");
        await p2("smirked");
        await say("检察官","看来，证人只是顺着群友的玩笑而已，辩方的主张根本不成立。","male");
        await p1("handsondesk");
        await say("律师","怎...怎么说？","male");
        await p2("document");
        await say("检察官","请看辩方证据前后的聊天记录。","male");
        await p3("surprised");
        SoundPlayer.Play("whack");
        await say("法官","这...这是...！","male");
        evi("msg2");
        await say("检察官","另外，还从别的消息记录中找到了证人的不在场证明。","male");
        p2zoom.SetActive(true);
        await say("检察官","检方主张被告有罪！","male");
        p2zoom.SetActive(false);
        await noisy();
        await p3("warning");
        await say("法官","肃静...！肃静...！","male");
        wit("阳光加冰");
        await say("阳光加冰","都说了不是我了。","male");
        SoundPlayer.Play("objection");
        await Delay(1000);
        await p2("strained");
        await shocks();
        await p3("surprised");
        await shocks();
        await p1("sweating");
        await shocks();
        wit("404");
        await shockdone();
        await say("404","冰棍是无罪的！我相信冰棍！","male");
        await say("404","冰棍只是喜欢女装的女孩纸而已！","male");
        wit("冰棍");
        SoundPlayer.Play("whack");
        await say("冰棍","人家...人家是男孩子啦！","female");
        await p2("normal");
        await say("检察官","看来证人也被被告骗得不浅啊。","male");
        wit("404");
        await say("404","请让我作出证词！","male");
        await p3("warning");
        await say("法官","...","male");
        await p3("normal");
        await say("法官","本案还有很多疑点，就听听证人的证词吧。","male");
        wit("404");
        await say("404","非常感谢！","male");
        await p2("normal");
        await say("检察官","那么证人，请说明你的姓名和职业。","male");
        wit("404");
        await say("404","404，受骗群的管理员。","male");
        await p3("normal");
        await say("法官","那么，请作证吧。","male");
        BGM("ask2");
        wit("404");
        await say("404","冰棍的女装真的很可爱啊...这么可爱的女孩子怎么会骗人...！","male");
        await say("404","而且...而且66亿元已经汇到了海外账户，可以查查看转账记录啊！","male");
        await say("404","冰棍这么可爱，我让她穿什么衣服她就穿什么拍照给我。","male");
        await say("404","冰棍一定喜欢我，我也喜欢冰棍！","male");
        await say("404","冰棍是我至高无上绝世无双无人能敌貌美如花英俊潇洒聪明能干女子力十足...","male");
        await say("404","威震四方骚气可爱卡哇伊琴棋书画样样精通技艺高超深谋远虑的女王大人！","male");
        await say("404","这样的人，怎么可能是犯...","male");
        bgm.Stop();
        await Objection1();
        await p1("deskslam",1);
        await p1("handsondesk");
        await say("律师",".......","male");
        await p1("confident");
        await say("律师","终于，让我抓住尾巴了。","male");
        BGM("hot");
        p1zoom.SetActive(true);
        await sayAdd("404是本案的真凶！！！","male");
        p1zoom.SetActive(false);
        await p1("document");
        await say("律师","刚才证人的证词提到“女孩子”，首先，被告并不是女孩子，这是矛盾的。","male");
        await Objection2();
        await p2("strained");
        await say("检察官","被告的性别与本案没有关系！","male");
        await Objection1();
        await p1("handsondesk");
        await say("律师","请听我说完，证人的证词还提到“我让她穿什么衣服她就穿什么拍照给我。”","male");
        await say("律师","证人，是女装照片对吧？","male");
        wit("404");
        await say("404","是...是又怎么样？","male");
        await p1("deskslam",1);
        await p1("handsondesk");
        await say("律师","被告是男性，这一点应该不需要证明了吧？","male");
        await say("律师","一个男生会拍女装照诱惑男生，有很大可能是受到了他人逼迫。","male");
        await p2("strained");
        await say("检察官","你是想说...证人逼迫被告拍摄女装照？","male");
        await p1("nodding",1);
        await p1("confident");
        await say("律师","并且，辩方能够证明诈骗66亿的主谋正是被告！","male");
        await p3("surprised");
        await say("法官","你说什么么么么么么么！！！！","male");
        await p1("confident");
        await say("律师","证人提到66亿“已经汇到海外账户”，但是并没有任何报告提到这个情况！","male");
        await p2("damage",1);
        await p2("strained");
        await say("检察官","噢噢噢噢噢噢噢噢噢噢噢噢噢噢噢噢哦哦哦！！！！！","male");
        await p2("damage",1);
        await p1("deskslam",1);
        await p1("handsondesk");
        await say("律师","现在，一切的真相都","male",true);
        p1zoom.SetActive(true);
        await sayAdd("揭开了！！！","male");
        p1zoom.SetActive(false);
        wit("404");
        await say("404",".........................","male");
        await say("404",".........................","male");
        SoundPlayer.Play("whack");
        await shocks();
        await say("404","噢噢噢噢噢噢噢噢噢噢噢噢噢噢噢噢哦哦哦！","male");
        BGM("truth");
        await p1("deskslam",1);
        await p1("handsondesk");
        await say("律师","证人误以为被告是女孩子，并且经常逼迫他拍摄女装照片用来诈骗，","male");
        await say("律师","最后在群里将66亿窃走！","male");
        await say("律师","群头像和消息记录也是证人为了栽赃给被告而做的！","male");
        await say("律师","辩方请求立刻下达判决！","male",true);
        bgm.Stop();
        await Objection2();
        await p2("objection",1);
        await p2("smirk",1);
        await p2("shrug",1);
        await p2("shrug",1);
        await p2("handondesk");
        await say("检察官","这些都只是辩方的猜测，辩方有证据吗？","male");
        SoundPlayer.Play("whack");
        await p1("sweating");
        await say("律师","这...这个...","male",true);
        await p2("shrug",1);
        await p2("handondesk");
        await say("检察官","果然被告是有罪的，检方请求下达判决。","male");
        await p2("bow",1);
        await Objection1();
        await p1("sweating");
        await say("律师","..........","male",true);
        await p1("thinking");
        await say("律师","辩...辩方有证据。","male",true);
        await Objection2();
        await p2("objection",1);
        await p2("handondesk");
        await say("检察官","辩方只是在虚张声势！不可能有那样的证据！","male");
        await p3("warning");
        await say("法官","辩方可以提供证据吗？","male");
        await p1("thinking");
        await say("律师","......","male",true);
        await p1("confident");
        SoundPlayer.Play("realization");
        BGM("real-truth");
        await say("律师","当然可以。","male",true);
        await say("律师","......决定性的证据。","male",true);
        await p1("document");
        await say("律师","请看辩方提交的证据。","male",true);
        evi("msg2");
        await say("律师","在消息记录里面，绿色的代表的是本人的消息。","male",true);
        await say("律师","也就是说，这是证人的截图，完全有伪造的可能！","male",true);
        await p2("damage",1);
        await p2("strained");
        await say("检察官","这...不可能...！","male");
        await p1("headshake",1);
        await p1("deskslam",1);
        await p1("handsondesk");
        await say("律师","这张群头像是由证人绘制的，只要检查它的pptx文件...","male",true);
        await p1("superobjection",1);
        await p1("pointing");
        await say("律师","就能知道替换头像是谁了！","male",true);
        await p2("damage",1);
        await p2("strained");
        await say("检察官",".............","male");
        wit("404");
        await say("404",".........................","male");
        await say("404",".........................","male");
        await say("404",".........................","male");
        await say("404","没错，就是我，然后呢？","male");
        await say("404","这个游戏是由我制作的，你们...","male");
        SoundPlayer.Play("supershock");
        bgm.Stop();
        witness.SetActive(false);
    }

    private async void Update() {
        if(!IsPlay){
            IsPlay = true;
            await Play();
        }
        if(Input.GetMouseButtonUp(0)) IsMouseUp = true;
    }
}

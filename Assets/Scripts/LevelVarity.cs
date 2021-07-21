using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelVarity
{
    public static string me = null;
    public static List<string> stteller = new List<string> {
        "我", "芙妮絲(妹妹)", "我", "我", "我", "我"
    };
    public static List<string> start = new List<string> {
        "什麼......  \n(點擊以繼續)", "怎麼了嗎,哥哥?", "快趴下,芙妮絲!!!!!!", "芙妮絲......我絕對會保護你的......", "嗯...... \n難道我已經死了嗎......", "什麼...這是!!! \n我變成史萊姆了!?"
    };
    public static List<List<int>> LevelWeather = new List<List<int>> {
        new List<int> {-1, 0, -1}
    };
    public static List<List<string>> LevelName = new List<List<string>> {
        new List<string> {"陌生的世界", "新手村", "初次的冒險"}
    };
    public static List<int> keyMax = new List<int> { 1, 3, };
    public static List<int> doorKey = new List<int> { 0, 2, 3};
    public static List<string> adteller = new List<string> { "傳送門", "", "","嚮導", };
    public static List<List<string>> chat = new List<List<string>> {
        new List<string>{},
        new List<string>{},
        new List<string>{},
        new List<string>{
        "ルーキーの村這裡的天氣可說是千變萬化,而且還是永晝呢!",
        "你問我是誰? \n我是不可思議的小魔女--瑪莉~",
        "據說這個世界的傳送門都異常兇猛!?",
        "勇者啊,繼續前進吧!"}
    };
    public static List<List<string>> advice = new List<List<string>> {
        new List<string> {
        "勇者啊,你是腦袋浸水還是沒腦袋? \n你沒看到這門上的鑰匙孔數嗎?你鑰匙不夠啊!!!",
        "勇者啊,你是眼睛脫窗還是沒眼睛? \n你沒看到佈告欄還有最新訊息嗎?凡事不要超之過急!!!"},
        new List<string> {""},new List<string> {""},
        new List<string> {
        "歡迎來到蘑菇村!!! \n你知道松茸跟蘑菇的差別嗎?他們從生物分類的\"科\"就有所不同呢! \n(松茸：マツタケ)",
        "一說到冒險,就會想到什麼?...... \n迷宮!!!傳說中這裡的迷宮是活的呢。"
        }
    };
    public static List<List<int>> littlech = new List<List<int>> {    //0:世界之聲 1:me 2:魔龍王
        new List<int> {0,1,0,1,0,0,1,2,1,2,1},
        new List<int> {0,0,1,0,1,0,0,0,0}
    };
    public static List<List<string>> teller = new List<List<string>> {
        new List<string> {"世界の声",me,"世界の声",me,"世界の声","世界の声",me,"魔龍王",me,"魔龍王",me},
        new List<string> {"世界の声","世界の声",me,"世界の声",me,"世界の声","世界の声","世界の声","世界の声"}
    };
    public static List<List<string>> story = new List<List<string>> {
        new List<string> {
        "勇者啊,運用你的智慧、力量以及運氣在我的地下城大鬧一翻吧!",
        "什麼地下城!? 芙妮絲呢??? 還有你是誰? \n把我的妹妹還來!!!",
        "想找回你的妹妹嗎? 哼,那你就好好掙扎到那最後一道門吧! \n至於我是誰...不是你需要知道的,你也沒資格知道。",
        "芙妮絲...你到底在哪裡......",
        "操作說明：\n 移動鍵：W-A-S-D \n 普攻：F （已解鎖）    大招：q (未解鎖) \n 開門：G   使用道具：Q (詳情可見暫停->說明)",
        "在右邊這種特殊(紫色)的牆壁上,能夠以\"撞擊牆壁的方式進一步地跳躍(按住方向鍵並在按一次 W )\"!",
        "???  \n這個聲音......",
        "史萊姆...\"傳說中的勇者\"呢?",
        "就是你...就是你把芙妮絲擄走的吧!!!",
        "哼,看來預言似乎出錯了啊。 \n 你就記住我的名字吧, 我就是這個地下城的守護者-- \"魔龍王\" 伊弗歐斯",
        "等等別走, 這裡到底是哪裡啊!!!"},
        new List<string> {
        "歡迎來到新手村第一層!在這裡你可以看到許多新的敵人、新的機關,也是個能讓你快速成長的好地方喔~",
        "在這裡請記住：「每當心情鬱悶的時候,用手托腮就好,手臂會因為幫上忙而開心的。」",
        "???   查理布朗?",
        "啊不是搞錯了,是：「心之所嚮,均歸虛空!」 \n 放開雙手去冒險吧!!!",
        "放開雙手要怎麼操作啊......",
        "小心草叢!",
        "謹慎行事是冒險者公會的第一守則!",
        "只有右邊這種紫色的特殊牆壁才能讓史萊姆二連跳喲,要記住!",
        "只有下面這種紫色的特殊牆壁才能讓史萊姆二連跳喲,要記住!"
        }
    };
    public static List<Vector2> spawnpoint = new List<Vector2> {
        new Vector2(5, 12), new Vector2(5, 60), new Vector2(54, 76)
    };
    public static List<List<Vector2>> promptpoint = new List<List<Vector2>> {
        new List<Vector2> {
            new Vector2(87, 6), new Vector2(7, 24), new Vector2(87, 42), new Vector2(8.5f, 60)
        }
    };

}

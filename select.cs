using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class select : MonoBehaviour
{
    

    //問題の進行度
    public int nowquestion = 0;
    //問題数
    int questionnum = 11;
    //テキスト
    public Text comment;
    public Text[] objectname = new Text[2];
    float[] textcolor = new float[4];
    public Text end;

    //今のコメント進行度の数
    int nowcoment = 0;
    //セリフ一覧
    List<string> startcomment = new List<string>() { "ここは幸せな夢の中", "目の前の鏡は真実を写す", "本当のあなたを見つめましょう", "さあ、私と共に", "あなたが捨てたのはどっち？", };
    List<string> judgecomment0 = new List<string>() { "あなたは", "を捨てました" };
    List<string> judgecomment1 = new List<string>() { "が目に入って、", "が好きなので、", "が気に入って、", "を助けるために、", "しか得られず、", "をかばって、", "でもよかったが、", "を守るために、", "を捨てる代わりに、", "が結果的に残りましたが、" };
    string[,] selectname = new string[13, 2] { { "焼肉", "寿司" }, { "たけのこ", "きのこ" }, { "犬", "猫" }, { "家族", "姫" }, { "平和", "健康" }, { "両腕", "両足" }, { "視力", "聴力" }, { "理性", "感情" }, { "過去", "未来" }, { "人間", "人間性" }, { "", "" }, { "夢", "現" }, { "", "" } };
    List<string> reason = new List<string>()
    {
        "“健康”がないあなたは" + "\r\n" + "現を無視することすらできません",
        "“両腕”がないあなたは" + "\r\n" + "現を壊すことすらできません",
        "“視力”がないあなたは" + "\r\n" + "現との境界すら分かりません",
        "“感情”がないあなたは" + "\r\n" + "現を拒否する動機すらありません",
        "“過去”がないあなたは" + "\r\n" + "現を拒否する根拠すらありません",
        "“人間性”がないあなたは" + "\r\n" + "現が現でないことへの執着すらありません"
    };
    List<string> end1comment = new List<string>()
    {
        "あなたは“現”を手に入れ、" + "\r\n" + "“夢”を捨てました",
        "現を捨てきることは" + "\r\n" + "残念ながらできませんでした"
    };
    List<string> end2comment = new List<string>()
    {
        "そして、あなたは“夢”を手に入れ、" + "\r\n" + "“現”を捨てました",
        "こうして、捨てた“現”は" + "\r\n" + "二度と目の前に現れることなく",
        "あなたはいつまでもいつまでも" + "\r\n" + "“夢”と共に幸せに暮らしました"
    };
    List<string> endlist = new List<string>(){ "END1 現を写す鏡", "END2 夢堕ち" };
    List<string> reasonans = new List<string>();


    //勝利条件
    int[] win = new int[6] { 0, 1, 1, 0, 1, 0 };
    int finalwin = 1;
    //進行度
    public int progress = 0;
    //動作一時停止
    public int move = 0;


    //選択肢オブジェクト
    public GameObject Selected;
    //選択肢オブジェクトの最初位置
    Vector3 Selectedposi = new Vector3(0, 19, 0);
    //選択肢オブジェクトの移動距離
    int Selectedmove = -20;
    //これまでの選択肢
    public int[] Decision;
    //その時の選択肢
    public int ans = 0;
    //選択オブジェクト一覧
    public GameObject[] selectobject = new GameObject[22];
    public GameObject mirrar;


    //矢印系オブジェクト
    public GameObject selectarrow;
    //矢印系位置
    Vector3[] selectarrowposi = new Vector3[3] { new Vector3(0, 100, 0), new Vector3(-5, -2, 0), new Vector3(5, -2, 0) };
    Vector3 selectarrowscale = new Vector3(0.9f, 0.9f, 1);
    //暗幕オブジェクト
    public GameObject blackbox;

    public AudioSource se;
    public AudioClip seselect;
    public AudioClip sedicision;

    // Start is called before the first frame update
    void Start()
    {
        comment.text = startcomment[nowcoment];
        objectname[0].text = ""; ;
        objectname[1].text = ""; ;
        end.text = "";

        nowcoment++;

        Selected.transform.position = Selectedposi;
        selectarrow.transform.position = selectarrowposi[0];
        selectarrow.transform.localScale = selectarrowscale;

        Decision = new int[questionnum + 1];
        
        SpriteRenderer black = blackbox.GetComponent<SpriteRenderer>();
        SpriteRenderer arrow = selectarrow.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move == 0)
        {
            switch (progress)
            {
                //最初の説明
                case 0:
                    firstexplain();
                    break;

                //人生は選択の連続
                case 1:
                    //左右選択をした時の動作
                    operate();
                    //決定キーを押した時の動作
                    StartCoroutine(selectdecision());
                    break;

                //審判の時だ
                case 2:
                    judge();
                    break;

                //END1
                case 3:
                    StartCoroutine(randamend1());
                    break;

                //END2
                case 4:
                    finaljudge();
                    break;

                //END2-1
                case 5:
                    StartCoroutine(endcomment21());            
                    break;

                //END2-2(夢堕ち)
                case 6:
                    StartCoroutine(endcomment22());
                    break;
            }
        }
    }

    //最初の説明(nowcoment = 0)
    private void firstexplain()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            comment.text = startcomment[nowcoment];
            nowcoment++;

            if (nowcoment == startcomment.Count)
            {
                progress++;
                //第一問を表示(次の問題に移行)
                nextquestion();
                //コメント進行度を初期化
                nowcoment = 0;
            }
        }
    }


    //選択キー
    private void operate()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ans = 1;
            selectarrow.transform.position = selectarrowposi[ans];
            se.PlayOneShot(seselect);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ans = 2;
            selectarrow.transform.position = selectarrowposi[ans];
            se.PlayOneShot(seselect);
        }
    }


    //(最大問題数-1)回選択する(nowquestion = 1)
    //決定キーを押した時の動作
    private IEnumerator selectdecision()
    {
        if (Input.GetKeyDown(KeyCode.Z) && ans != 0 && move == 0)
        {
            se.PlayOneShot(sedicision);
            //答えた問題の処理
            Decision[nowquestion - 1] = (ans + 1) % 2;
            yield return StartCoroutine(remove());


            //次の問題に移行
            nextquestion();

            //全ての問題が終わった時の処理
            if (nowquestion == questionnum)
            {              
                progress = 2;
                comment.text = judgecomment0[0] + selectname[nowcoment, (Decision[nowcoment] + 1) % 2] + judgecomment1[nowcoment] + "\r\n" + selectname[nowcoment, Decision[nowcoment]] + judgecomment0[1];
                nowcoment++;
            }
        }
    }


    //次の問題に移行するための処理
    private void nextquestion()
    {
        Selectedposi.y += Selectedmove;
        Selected.transform.position = Selectedposi;
        ans = 0;
        //オブジェクトの名前書き換え
        objectname[0].text = selectname[nowquestion, 0];
        objectname[1].text = selectname[nowquestion, 1];
        objectname[0].color = new Color(0, 0, 0, 90);
        objectname[1].color = new Color(0, 0, 0, 90);

        //矢印系オブジェクトの位置の初期化
        selectarrow.transform.position = selectarrowposi[ans];
        selectarrow.transform.localScale = selectarrowscale;
        selectarrow.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        nowquestion++;
    }


    //選択したものを消す
    private IEnumerator remove()
    {
        move = 1;
        Vector3 selectobposi;
        Vector3 selectscale = selectarrowscale;
        for (int i = 0; i < 90; i++)
        {
            selectobposi.x = -i - 1;
            selectobposi.y = -i - 1;
            selectobposi.z = -i - 1;
            selectobject[nowquestion * 2 + Decision[nowquestion - 1] - 2].transform.rotation = Quaternion.Euler(selectobposi);
            textcolor[0] = 0.999999999f - (i + 1) * 0.0111111111f;
            objectname[ans - 1].color = new Color(0, 0, 0, textcolor[0]);
            selectscale.x = 0.9f - (i + 1) * 0.01f;
            selectscale.y = 0.9f - (i + 1) * 0.01f;
            selectarrow.transform.localScale = selectscale;
            selectarrow.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, textcolor[0]);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1);
        move = 0;
    }




    //審判
    private void judge()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            comment.text = judgecomment0[0] + selectname[nowcoment, (Decision[nowcoment] + 1) % 2] + judgecomment1[nowcoment] + "\r\n" + selectname[nowcoment, Decision[nowcoment]] + judgecomment0[1];
            nowcoment++;
            
            //セリフを言い終わったら
            if (nowcoment > questionnum - 2)
            {
                if (Decision[4] == win[0] && Decision[5] == win[1] && Decision[6] == win[2] && Decision[7] == win[ 3] && Decision[8] == win[4] && Decision[9] == win[5])
                {
                    progress = 4;
                }
                else
                {
                    progress = 3;
                }
                nowcoment = 0;
            }
        }
    }


    //END1
    private IEnumerator randamend1()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (nowcoment == 0)
            {
                //最後のアドバイスを決める
                for (int i = 0; i < 6; i++)
                {
                    if (Decision[i + 4] != win[i])
                    {
                        reasonans.Add(reason[i]);
                    }
                }
                int num = Random.Range(0, reasonans.Count - 1);
                comment.text = reasonans[num];
                
            }
            else if(nowcoment == 1)
            {
                comment.text = end1comment[nowcoment];
            }
            else if(nowcoment == 2)
            {
                end.text = endlist[0];
                yield return StartCoroutine(Ending());
            }
            else
            {
                SceneManager.LoadScene("Title");
            }

            nowcoment++;
        }
    }

    //END2
    private void finaljudge()
    {
        if(nowcoment == -1)
        {
            operate();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (nowcoment == 0)
            {
                nextquestion();
                comment.text = "";
                nowcoment = -1;
            }
            //nowcoment = -1
            else if (ans != 0 && nowcoment == -1)
            {
                nowcoment = 1;
                nowquestion = 11;
                Decision[nowquestion - 1] = (ans + 1) % 2;

                //次の問題に移行
                nowquestion = 12;
                nextquestion();

                //現を捨てる(夢堕ち)
                if (Decision[10] == finalwin)
                {
                    comment.transform.rotation = Quaternion.Euler(0, 180, 0);
                    progress = 6;
                    comment.text = end2comment[0];
                    mirrar.transform.localScale = new Vector3(1, 1, 1);
                }
                //夢を捨てる
                else
                {
                    progress = 5;
                    comment.text = end1comment[0];
                }
            }
        }
    }
    //END2-1
    private IEnumerator endcomment21()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (nowcoment < 2)
            {
                comment.text = end1comment[nowcoment];
            }
            //END1表示
            else if(nowcoment == 2)
            {
                end.text = endlist[0];
                yield return StartCoroutine(Ending());
            }
            else
            {
                SceneManager.LoadScene("Title");
            }
            nowcoment++;
        }
    }
    //END2-2
    private IEnumerator endcomment22()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (nowcoment < 3)
            {
                comment.text = end2comment[nowcoment];
            }
            //END2表示
            else if(nowcoment == 3)
            {
                end.text = endlist[1];
                yield return StartCoroutine(Ending());
            }
            else
            {
                SceneManager.LoadScene("Title");
            }
            nowcoment++;
        }
    }
    //ENDingへの移動
    private IEnumerator Ending()
    {
        move = 1;
        for (int i = 0; i < 100; i++)
        {
            textcolor[0] = (i + 1) * 0.01f;
            blackbox.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, textcolor[0]);
            textcolor[0] = 0.1650943f - (i + 1) * 0.001650943f;
            textcolor[1] = 0.2606897f - (i + 1) * 0.002606897f;
            textcolor[2] = 1.0f - (i + 1) * 0.01f;
            textcolor[3] = 1.0f - (i + 1) * 0.01f;
            comment.color = new Color(textcolor[0], textcolor[1], textcolor[2], textcolor[3]);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1);
        move = 0;
    }
}
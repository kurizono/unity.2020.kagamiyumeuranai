using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class back : MonoBehaviour
{
    select selectcs;

    //オブジェクト
    public GameObject hiro;
    public GameObject[] hiropart = new GameObject[6];
    public GameObject[] family = new GameObject[2];
    public GameObject[] prinsess = new GameObject[2];
    public GameObject[] cat = new GameObject[2];
    public GameObject[] dog = new GameObject[2];
    public GameObject donbi;
    public GameObject devil;
    public GameObject whitebox;
    
    //場所
    Vector3 hiroposi = new Vector3(0, -0.05f, 0);
    Vector3 familyposi = new Vector3(-0.299f, -0.051f, 0);
    Vector3 prinsessposi = new Vector3(0.342f, -0.045f, 0);

    //その他もろもろ
    int time = 0;
    float[] need = new float[10];
    int eye = 0;
    int path = 0;
    int future = 0;
    int animal = 0;
    int human = 0;
    int arufa = 0;
    int[] ans = new int[15];

    [SerializeField] private AudioSource source;
    public AudioClip[] mainmusic = new AudioClip[2];

    // Start is called before the first frame update
    void Start()
    {
        selectcs = GetComponent<select>();
    }

    // Update is called once per frame
    void Update()
    {
        if(future == 0 && path == 0)
        {
            movedog();
            movecat();
            movehiro();
            movefamily();
            moveprinsess();
        }
        if(eye > 0)
        {
            eyemove();
        }
        if(path > 0)
        {
            colorchange();
        }
        
        StartCoroutine(backchange());

        time++;
    }

    //犬を左右に揺らす
    private void movedog()
    {
        need[0] = Mathf.Sin(time / 30) * 15;
        dog[1].transform.rotation = Quaternion.Euler(0, 0 ,5 + need[0]);
    }
    //猫を左右に揺らす
    private void movecat()
    {
        need[0] = Mathf.Sin(time / 30) * 15;
        cat[1].transform.rotation = Quaternion.Euler(0, 0, -5 - need[0]);
    }
    //勇者を上下に揺らす
    private void movehiro()
    {
        need[0] = Mathf.Abs(Mathf.Sin(time / 30)) * 0.05f;
        hiro.transform.localPosition = new Vector3(hiroposi.x, hiroposi.y + need[0], hiroposi.z);
    }
    //家族を上下に揺らす
    private void movefamily()
    {
        need[0] = Mathf.Abs(Mathf.Sin(time / 30)) * 0.05f;
        family[0].transform.localPosition = new Vector3(familyposi.x, familyposi.y + need[0], familyposi.z);
    }
    //姫を上下に揺らす
    private void moveprinsess()
    {
        need[0] = Mathf.Abs(Mathf.Sin(time / 30)) * 0.05f;
        prinsess[0].transform.localPosition = new Vector3(prinsessposi.x, prinsessposi.y + need[0], prinsessposi.z);
    }

    //視界が悪くなる
    private void eyemove()
    {
        need[0] = Mathf.Sin(eye / 30) * 0.25f + 0.5f;
        whitebox.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, need[0]);
        eye++;
    }

    //過去が曖昧になる
    private void colorchange()
    {

    }


    //条件諸々
    private IEnumerator backchange()
    {
           if (Input.GetKeyDown(KeyCode.Z))
            {
                if (selectcs.progress == 1)
                {
                switch (selectcs.nowquestion)
                {
                    //犬と猫
                    case 3:
                        if (ans[3] == 0)
                        {
                            ans[3] = 1;

                            if (selectcs.ans == 1)   //犬を黒くする
                            {

                                for (int i = 0; i < 100; i++)
                                {
                                    need[0] = 1 - (i + 1) * 0.01f;
                                    dog[0].GetComponent<SpriteRenderer>().color = new Color(need[0], need[0], need[0], 1);
                                    yield return new WaitForSeconds(0.01f);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < 100; i++)
                                {
                                    need[0] = 1 - (i + 1) * 0.01f;
                                    cat[0].GetComponent<SpriteRenderer>().color = new Color(need[0], need[0], need[0], 1);
                                    yield return new WaitForSeconds(0.01f);
                                }
                                animal = 1;
                            }
                        }
                        break;
                    //家族と姫
                    case 4:
                        if (ans[4] == 0)
                        {
                            ans[4] = 1;

                            if (selectcs.ans == 1)
                            {
                                for (int i = 0; i < 100; i++)
                                {
                                    need[0] = 1 - (i + 1) * 0.01f;
                                    need[1] = 0.9f - (i + 1) * 0.009f;
                                    need[2] = 0.8f - (i + 1) * 0.008f;
                                    family[0].GetComponent<SpriteRenderer>().color = new Color(need[0], need[0], 0, 1);
                                    family[1].GetComponent<SpriteRenderer>().color = new Color(need[0], need[1], need[2], 1);
                                    yield return new WaitForSeconds(0.01f);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < 100; i++)
                                {
                                    need[0] = 1 - (i + 1) * 0.01f;
                                    need[1] = 0.9f - (i + 1) * 0.009f;
                                    need[2] = 0.8f - (i + 1) * 0.008f;
                                    prinsess[0].GetComponent<SpriteRenderer>().color = new Color(need[0], need[0], need[0], 1);
                                    prinsess[1].GetComponent<SpriteRenderer>().color = new Color(need[0], need[0], need[0], 1);
                                    yield return new WaitForSeconds(0.01f);
                                }
                                human = 1;
                            }
                        }
                        break;
                    //平和と健康
                    case 5:
                        if (ans[5] == 0)
                        {
                            ans[5] = 1;
                            if (selectcs.ans == 1)
                            {
                                for (int i = 0; i < 90; i++)
                                {
                                    need[0] = (i + 1) * 2;
                                    family[1].transform.rotation = Quaternion.Euler(need[0], 180, 0);
                                    prinsess[1].transform.rotation = Quaternion.Euler(need[0], 0, 0);
                                    yield return new WaitForSeconds(0.01f);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < 100; i++)
                                {
                                    need[0] = 1 - (i + 1) * 0.01f;
                                    need[1] = 1 - (i + 1) * 0.005f;
                                    need[2] = 1 - (i + 1) * 0.005f;
                                    hiropart[4].GetComponent<SpriteRenderer>().color = new Color(need[0], need[1], need[2], 1);
                                    yield return new WaitForSeconds(0.01f);
                                }
                            }
                        }
                        break;
                            //両腕と両足
                        case 6:
                        if (ans[6] == 0)
                        {
                            ans[6] = 1;
                            if (selectcs.ans == 1)
                            {
                                for (int i = 0; i < 90; i++)
                                {
                                    need[0] = i + 1;
                                    hiropart[0].transform.rotation = Quaternion.Euler(0, need[0], 0);
                                    hiropart[1].transform.rotation = Quaternion.Euler(0, -need[0], 0);
                                    yield return new WaitForSeconds(0.01f);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < 90; i++)
                                {
                                    need[0] = i + 1;
                                    hiropart[2].transform.rotation = Quaternion.Euler(0, need[0], 0);
                                    hiropart[3].transform.rotation = Quaternion.Euler(0, -need[0], 0);
                                    yield return new WaitForSeconds(0.01f);
                                }
                            }
                        }
                                break;
                            //視力と聴力
                        case 7:
                        if (ans[7] == 0)
                        {
                            ans[7] = 1;
                            if (selectcs.ans == 1)
                            {
                                for (int i = 0; i < 100; i++)
                                {
                                    need[0] = (i + 1) * 0.005f;
                                    whitebox.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, need[0]);
                                    yield return new WaitForSeconds(0.01f);
                                }
                                eye = 1;
                            }
                            else
                            {
                                source.Stop();
                                source.clip = mainmusic[1];
                                source.Play();
                            }
                        }
                                break;
                            //理性と感情
                        case 8:
                        if (ans[8] == 0)
                        {
                            ans[8] = 1;
                            if (selectcs.ans == 1)
                            {
                                for (int i = 0; i < 90; i++)
                                {
                                    need[0] = (i + 1) * 2;
                                    family[0].transform.rotation = Quaternion.Euler(need[0], 180, 0);
                                    prinsess[0].transform.rotation = Quaternion.Euler(need[0], 0, 0);
                                    dog[0].transform.rotation = Quaternion.Euler(need[0], 0, 0);
                                    cat[0].transform.rotation = Quaternion.Euler(need[0], 0, 0);
                                    yield return new WaitForSeconds(0.01f);
                                }
                            }
                            else
                            {
                                path = 1;
                                for (int i = 0; i < 90; i++)
                                {

                                    need[0] = i + 1;
                                    need[1] = (i + 1) * 0.005f;
                                    familyposi.x -= 0.005f;
                                    prinsessposi.x += 0.005f;
                                    family[0].transform.rotation = Quaternion.Euler(0, 180, -need[0]);
                                    prinsess[0].transform.rotation = Quaternion.Euler(0, 0, -need[0]);
                                    
                                    family[0].transform.localPosition = new Vector3(familyposi.x - need[1], familyposi.y, familyposi.z);
                                    prinsess[0].transform.localPosition = new Vector3(prinsessposi.x + need[1], prinsessposi.y, prinsessposi.z);
                                    dog[0].transform.rotation = Quaternion.Euler(0, 0, need[0]);
                                    cat[0].transform.rotation = Quaternion.Euler(0, 0, -need[0]);
                                    yield return new WaitForSeconds(0.01f);
                                }
                                path = 0;
                            }
                        }
                                break;
                            //過去と未来
                            case 9:
                        if (ans[9] == 0)
                        {
                            ans[9] = 1;
                            if (selectcs.ans == 1)
                            {
                                for (int i = 0; i < 100; i++)
                                {

                                    need[0] = 1 - (i + 1) * 0.01f;
                                    if (human == 1)
                                    {
                                        family[0].GetComponent<SpriteRenderer>().color = new Color(1, need[0], need[0], 1); //赤
                                        family[1].GetComponent<SpriteRenderer>().color = new Color(need[0], 1, 1, 1);
                                    }
                                    else
                                    {
                                        prinsess[0].GetComponent<SpriteRenderer>().color = new Color(1, 1, need[0], 1);//黄
                                        prinsess[1].GetComponent<SpriteRenderer>().color = new Color(1, need[0], 1, 1);
                                    }
                                    if (animal == 1)
                                    {
                                        dog[0].GetComponent<SpriteRenderer>().color = new Color(need[0], 1, need[0], 1);//緑
                                    }
                                    else
                                    {
                                        cat[0].GetComponent<SpriteRenderer>().color = new Color(need[0], need[0], 1, 1);//蒼
                                    }
                                    yield return new WaitForSeconds(0.01f);

                                }
                            }
                            else
                            {
                                future = 1;
                            }
                        }
                                break;

                            //人間と人間性
                            case 10:
                        if (ans[10] == 0)
                        {
                            ans[10] = 1;
                            if (selectcs.ans == 1)
                            {
                                donbi.transform.localScale = new Vector3(0, 0, 0);
                                for (int i = 0; i < 90; i++)
                                {
                                    need[0] = i + 1;
                                    hiro.transform.rotation = Quaternion.Euler(-need[0], 0, 0);
                                    yield return new WaitForSeconds(0.01f);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < 90; i++)
                                {
                                    need[0] = i + 1;
                                    donbi.transform.rotation = Quaternion.Euler(90 - need[0], 0, 0);
                                    hiropart[4].transform.rotation = Quaternion.Euler(-need[0], 0, 0);
                                    yield return new WaitForSeconds(0.01f);
                                }
                            }
                        }
                                break;                            
                        }
                }

            }
        
        if (selectcs.progress == 6 && arufa == 0)
        {
            source.Stop();
            arufa++;
        }
    }
}

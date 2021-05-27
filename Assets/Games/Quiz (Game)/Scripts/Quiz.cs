using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Quiz : MonoBehaviour
{
    public LoadQuestions[] Load = new LoadQuestions[1];
    public SaveQuestions[] Save;
    public Text[] answersText;
    string[] answers_no_let = new string[5];
    public string[] letters;
    public GameObject[] answerPan = new GameObject[5];

    public int imNo;
    public int trueAnswer;

    public Text qText;
    public Text tText;

    bool answer;
    bool answer_choosed;

    public int record;
    int max_rec;

    float sec = 0;
    string sec_s;
    int min = 0;
    public Text full_time_t;
    bool start_time;

    float qTime = 45;
    string qTimeString;
    public Text qTimeText;

    List<object> qList;
    Questionlist crntQ;
    int randQ;

    public GameObject choose_btn;
    public GameObject btn_panel;

    float time = 5;
    public Text time_t;
    bool start = false;
    public GameObject text;

    int qNo = 1;
    public Text qNoT;

    float anim_time = 2;
    bool anim_start;
    bool anim_end;

    int MaxQuestions;

    void Start()
    {
        max_rec = PlayerPrefs.GetInt("QuizMaxRecord");
        if (PlayerPrefs.GetInt("QuizGameType") == 2)
        {
            MaxQuestions = 10;
            qNoT.text = "ВОПРОС № 1/" + MaxQuestions;
        }
        else
        {
            MaxQuestions = 6;
            qNoT.text = "ВОПРОС № 1/" + MaxQuestions;
        }
    }

    void Update()
    {
        if(start_time == true)
        {
            qTime = qTime - Time.deltaTime;
            qTimeString = Mathf.Round(qTime).ToString();
            if (qTimeString.Length == 1) qTimeText.text = "00:0" + qTimeString;
            else qTimeText.text = "00:" + qTimeString;
            if (qTime < 0)
            {
                qNo += 1;
                qNoT.text = "ВОПРОС №" + qNo.ToString() + "/" + MaxQuestions;
                questiongenerate();
                answer_choosed = false;
                qTime = 45;
            }


            sec = sec + Time.deltaTime;
            sec_s = Mathf.Round(sec).ToString();

            if (Mathf.Round(sec) == 60)
            {
                min = min + 1;
                sec = 0;
            }

            if (min.ToString().Length == 1 & sec_s.Length == 1)
            {
                full_time_t.text = "0" + min.ToString() + ":" + "0" + sec_s;
            }
            else if (min.ToString().Length == 2 & sec_s.Length == 1)
            {
                full_time_t.text = min.ToString() + ":" + "0" + sec_s;
            }
            else if (min.ToString().Length == 1 & sec_s.Length == 2)
            {
                full_time_t.text = "0" + min.ToString() + ":" + sec_s;
            }
            else
            {
                full_time_t.text = min.ToString() + ":" + sec_s;
            }
        }
        
        if (start == true)
        {
            text.SetActive(true);
            time = time - Time.deltaTime;
            time_t.text = Mathf.Round(time).ToString();
        }

        if (time < 1)
        {
           
            qList = new List<object>(Load[0].categories[0].questions);
            for (int i = 1; i < Load[0].categories.Length; i++)
            {
                qList.AddRange(Load[0].categories[i].questions);
            }
            questiongenerate();
            text.SetActive(false);
            time = 2;
            start = false;
            choose_btn.SetActive(true);
            btn_panel.SetActive(false);
            start_time = true;
        }

        if (anim_start == true)
        {
            anim_time = anim_time - Time.deltaTime;
        }

        if (anim_time < 0)
        {
            anim_start = false;
            anim_end = true;
            anim_time = 2;
        }
        

        if (anim_end == true)
        {
            qNo += 1;
            qNoT.text = "ВОПРОС №" + qNo.ToString() + "/" + MaxQuestions;
            if (answer == true) record = record + 1;
            qList.RemoveAt(randQ);
            answer_choosed = false;
            qTime = 45;
            GameObject.Find("Answer (" + imNo.ToString() + ")").GetComponent<Animator>().SetTrigger("Normal");
            GameObject.Find("Answer (" + trueAnswer.ToString() + ")").GetComponent<Animator>().SetTrigger("Normal");
            btn_panel.SetActive(false);
            anim_end = false;
            questiongenerate();
        }

        
    }

    public void Left()
    {

    }

    public void Right()
    {
        if (PlayerPrefs.GetInt("QuizGameType") == 1)
        {
            questiongenerate();
        }   
    }


    [ContextMenu("Load")]
    public void LoadField()
    {
        for (int i = 0; i < 5; i++)
        {
            Load[i] = JsonUtility.FromJson<LoadQuestions>(File.ReadAllText(Application.streamingAssetsPath + "/Quiz.json"));
        }
            
    }

    public void OnClickPlay()
    {
        start = true;
    }

    public void questiongenerate()
    {
        if (qNo < MaxQuestions)
        {
            randQ = Random.Range(0, qList.Count);
            crntQ = qList[randQ] as Questionlist;
            qText.text = crntQ.question;
            tText.text = Load[0].categories[crntQ.topicKey].category.ToUpper();

            List<string> answers = new List<string>(crntQ.answers);
            for (int i = 0; i < crntQ.answers.Length; i++)
            {
                int rand = Random.Range(0, answers.Count);
                answers_no_let[i] = answers[rand];
                answersText[i].text = letters[i] + answers_no_let[i];
                answers.RemoveAt(rand);
            }
            for (int i = 0; i < 5; i++)
            {
                answerPan[i].SetActive(false);
            }
            for (int i = 0; i < crntQ.answers.Length; i++)
            {
                answerPan[i].SetActive(true);
            }
        } 
        else
        {
            if (record > max_rec)
            {
                PlayerPrefs.SetInt("QuizMaxRecord", record);
            }
            PlayerPrefs.SetString("QuizTime", (full_time_t.text).ToString());
            PlayerPrefs.SetInt("QuizRecord", record);
            SceneManager.LoadScene(15);
        }    
    }

    public void answersBttns(int index)
    {
        answer_choosed = true;
        imNo = index;
        if (answers_no_let[index] == crntQ.answers[0]) answer = true;
        else answer = false;
    }

    public void ChooseButton()
    {
        if (answer_choosed == true)
        {
            if (answer == true)
                {
                    GameObject.Find("Answer (" + imNo.ToString() + ")").GetComponent<Animator>().SetTrigger("t");
                }
            else
            {
                GameObject.Find("Answer (" + imNo.ToString() + ")").GetComponent<Animator>().SetTrigger("f");
                if(PlayerPrefs.GetInt("QuizGameType") == 1)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (answers_no_let[i] == crntQ.answers[0])
                        {
                            GameObject.Find("Answer (" + i.ToString() + ")").GetComponent<Animator>().SetTrigger("t");
                            trueAnswer = i;
                        }
                    }
                }
            }
                
            btn_panel.SetActive(true);
            anim_start = true;
            
        }
    }
}
[System.Serializable]
public class Questionlist
{
    public string question;
    public string[] answers = new string[5];
    public int topicKey;
}

[System.Serializable]
public class categoryList
{
    public string category;
    public Questionlist[] questions;
}

[System.Serializable]
public class LoadQuestions
{
    public string Load;
    public categoryList[] categories;
}

[System.Serializable]
public class SaveQuestions
{
    public float answered;
    public string question;
    public string trueAnswer;
    public string[] answers = new string[5];
    public int topicKey;
    public int qNo;
    public float qTime;
}

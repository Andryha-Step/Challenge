using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Snake : MonoBehaviour {

	public KeyCode left = KeyCode.A;
	public KeyCode right = KeyCode.D;
	public KeyCode up = KeyCode.W;
	public KeyCode down = KeyCode.S;

	public int size = 32;
	public float shift = 1;
	public float timeoutMove = 0.5f;
	public float timeoutBonus = 5;
	public GameObject _tail;
	public GameObject _bonus;
	private float curTimeout;
	private Vector3[,] pos;
	private List<GameObject> tail;
	private Vector3 tail_pos;
	private Vector3 tail_last;
	public int t_Count;
	private float h, v;
	public int hor;
	public int ver;
	
	public int record = 0;
	public Text rec_t;
	public Text lifes_t;
	public float sec = 0;
	string sec_s; 
	public int min = 0;
	public Text time_t;

	public string max_rec_text;
	public int max_rec;


	public static int tailCount;
	
	public static bool lose;
	
	public static bool left_w;
	public static bool right_w;
	public static bool down_w;
	public static bool up_w;

	public int step;

	void Start () 
	{
		lose = false;
		left_w = false;
		right_w = false;
		down_w = false;
		up_w = false;
		tailCount = 1;
		t_Count = tailCount;
		tail = new List<GameObject>();
		tail.Add(this.gameObject);
		float posX = -shift*size/2-shift;
		float posY = Mathf.Abs(posX);
		float Xreset = posX;
		pos = new Vector3[size, size];
		for(int y = 0; y < size; y++)
		{
			posY -= shift;
			for(int x = 0; x < size; x++)
			{
				posX += shift;
				pos[x,y] = new Vector3(posX, posY, 0);
			}
			posX = Xreset;
		}

		tail[0].transform.position = pos[size/2, size/2];
		StartCoroutine (AddBonus());
		v = shift;

		max_rec_text = PlayerPrefs.GetString("SnakeMaxRecordText");
		max_rec = PlayerPrefs.GetInt("SnakeMaxRecord");
	}

	IEnumerator AddBonus()
	{
		GameObject clone = Instantiate(_bonus) as GameObject;
		clone.transform.position = pos[Random.Range(0, size), Random.Range(0, size)];
		yield return new WaitForSeconds (timeoutBonus);
		if(!lose) StartCoroutine (AddBonus());
	}

	void Move(int count)
	{
		step = 1;

		tail_last = tail[tail.Count-1].transform.position;

		tail_pos = tail[0].transform.position;


		if (h < 0)
		{
			hor = hor - 1;
		}

		if (h > 0)
		{
			hor = hor + 1;
		}

		if (v < 0)
		{
			ver = ver - 1;
		}

		if (v > 0)
		{
			ver = ver + 1;
		}

		if (hor == 0)
		{
			tail[0].transform.position = new Vector3(tail_pos.x - h * (15), tail_pos.y, 0);
			hor = 16;
		}
        else
        {
			if (hor == 16)
			{
				tail[0].transform.position = new Vector3(tail_pos.x - h * (15), tail_pos.y, 0);
				hor = 0;
			}
			else
			{
				if (ver == 0)
				{
					tail[0].transform.position = new Vector3(tail_pos.x, tail_pos.y - v * 22, 0);
					ver = 23;
				}
				else
				{
					if (ver == 23)
					{
						tail[0].transform.position = new Vector3(tail_pos.x, tail_pos.y - v * 22, 0);
						ver = 0;
					}
					else
					{
						tail[0].transform.position = new Vector3(tail_pos.x + h, tail_pos.y + v, 0);
					}
				}
			}
		}


		Vector3 tmp = Vector3.zero;

		for(int j = 1; j < count; j++)
		{
			tmp = tail[j].transform.position;
			tail[j].transform.position = tail_pos;
			tail_pos = tmp;
		}
		
		
	}

	void Update () 
	{
        

		sec = sec + Time.deltaTime;
		sec_s = Mathf.Round(sec).ToString();

		if (Mathf.Round(sec) == 60)
        {
			min = min + 1;
			sec = 0;
        }

		if (min.ToString().Length == 1 & sec_s.Length == 1) 
		{ 
			time_t.text = "0" + min.ToString() + ":" + "0" + sec_s;
		}
		else if(min.ToString().Length == 2 & sec_s.Length == 1)
		{
			time_t.text = min.ToString() + ":" + "0" + sec_s;
		}
		else if (min.ToString().Length == 1 & sec_s.Length == 2)
		{
			time_t.text = "0" +  min.ToString() + ":" + sec_s;
		}
		else
		{
			time_t.text = min.ToString() + ":" + sec_s;
		}

		record = (tailCount * 5) - 5;
		if (record.ToString().Length == 1)
        {
			rec_t.text = "000" + record.ToString();
		}
		else if (record.ToString().Length == 2)
		{
			rec_t.text = "00" + record.ToString();
		}
		else if (record.ToString().Length == 3)
		{
			rec_t.text = "0" + record.ToString();
		}
		else
		{
			rec_t.text = record.ToString();
		}

		curTimeout += Time.deltaTime;
		if (curTimeout > timeoutMove)
		{
			curTimeout = 0;
			Move(tailCount);
		}

		if (t_Count != tailCount)
		{
			GameObject clone = Instantiate(_tail) as GameObject;
			clone.name = "Tail_" + tail.Count;
			clone.transform.position = tail_last;
			tail.Add(clone);
		}
		t_Count = tailCount;

		if (Input.GetKeyDown(left))
		{
			Left();
		}
		else if(Input.GetKeyDown(right)) 
		{
			Right();
		}
		else if(Input.GetKeyDown(down)) 
		{
			Down();
		}
		else if(Input.GetKeyDown(up)) 
		{
			Up();
		}

		if (left_w == true)
		{
			Left();
			left_w = false;
		}
		else if (right_w == true)
		{
			Right();
			right_w = false;
		}
		else if (down_w == true)
		{
			Down();
			down_w = false;
		}
		else if (up_w == true)
		{
			Up();
			up_w = false;
		}

		if (lose)
		{
			enabled = false;
			lifes_t.text = "0";
			PlayerPrefs.SetString("SnakeTime", (time_t.text).ToString());
			PlayerPrefs.SetInt("SnakeRecord", record);
			if (max_rec < record)
			{
				PlayerPrefs.SetInt("SnakeMaxRecord", record);
				PlayerPrefs.SetString("SnakeMaxRecordText", (rec_t.text).ToString());
			}
			SceneManager.LoadScene(12);
		}


	}

	void OnCollisionEnter2D(Collision2D other) 
	{
		if(other.collider.tag == "Player")
		{
			lose = true;
		}
	}

	public void Left()
    {
		if (hor > 0 & hor < 16 & ver > 0 & ver < 23)
		{
			if (h <= 0 & right_w == false & step == 1)
			{
				h = -shift;
				v = 0;
				step = 0;
			}
			else
			{
				if (tailCount < 2)
				{
					h = -shift;
					v = 0;
				}
			}
		}
	}

	public void Right()
	{
		if (hor > 0 & hor < 16 & ver > 0 & ver < 23)
		{
			if (h >= 0 & left_w == false & step == 1)
			{
				h = shift;
				v = 0;
				step = 0;
			}
			else
			{
				if (tailCount < 2)
				{
					h = shift;
					v = 0;
				}
			}
		}
	}

	public void Down()
	{
		if (hor > 0 & hor < 16 & ver > 0 & ver < 23)
		{
			if (v <= 0 & up_w == false & step == 1)
			{
				v = -shift;
				h = 0;
				step = 0;
			}
			else
			{
				if (tailCount < 2)
				{
					v = -shift;
					h = 0;
				}
			}
		}
	}

	public void Up()
	{
		if (hor > 0 & hor < 16 & ver > 0 & ver < 23)
		{
			if (v >= 0 & down_w == false & step == 1)
			{
				v = shift;
				h = 0;
				step = 0;
			}
			else
			{
				if (tailCount < 2)
				{
					v = shift;
					h = 0;
				}
			}
		}
	}
}

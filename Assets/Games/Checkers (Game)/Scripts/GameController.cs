using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour {
    public Text turnText;
    public Text dataText;
    public Image resultPanel;
    public Image creditsPanel;
    public Button creditsButton;

    private enum Turn
    {
        enemyTurn,
        playerTurn
    }
    private Turn turn;
    private int turnsKingMoving;
    private bool inFinal = false;
    private int finalCounter = 0;
    private bool isGameOver = false;
    private Board board;
    private Bot bot;
    private Player player;
    private List<BoardConfiguration> historic;

    public GameObject BlockPanel;
    public GameObject StartBtn;
    float sec = 0;
    float min_time;
    string sec_s;
    int min = 0;
    public Text full_time_t;
    bool start_time;
    float time = 5;
    public Text time_t;
    bool start = false;
    public GameObject text;

    int white;
    int black;
    public Text WhiteMount;
    public Text BlackMount;
    public Text Record;
    public int record;
    public string record_t;

    public static bool Help = true;

    void Awake()
    {
        turn = Turn.enemyTurn;
        if (resultPanel != null)
            resultPanel.gameObject.SetActive(false);
        else
            Debug.LogError("Couldn't find the result panel object.");

        if (creditsPanel != null)
            creditsPanel.gameObject.SetActive(false);
        else
            Debug.LogError("Couldn't find the credits panel object.");
        
        historic = new List<BoardConfiguration>();
        Load();
        bot = new Bot(historic);
        player = new Player();
        turnsKingMoving = 0;
    }

    /*
     * Initialize variables.
     */
    void Start()
    {
        Help = false;
        board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board> ();
        StartCoroutine(LateStart(0.5f));
        min_time = PlayerPrefs.GetFloat("CheckersMinTime");

        if (creditsButton == null)
            Debug.LogError("Credits Button not Found.");
        else
        {
            creditsButton.onClick.AddListener(() => creditsPanel.gameObject.SetActive(true));
        }
        if (turnText == null)
            Debug.LogError("Turn Text not Found.");
        if (dataText == null)
            Debug.LogError("Data text not founded!");
        if (historic == null)
            dataText.text = "Dados: 0";
        else
            dataText.text = "Dados: " + historic.Count;
    }

    public void Help_bttn()
    {
        Help = true;
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.NextTurn();
    }

    void Update()
    {
        white = this.board.GetPlayerPieces().Count;
        black = this.board.GetEnemyPieces().Count;
        WhiteMount.text = "x" + white.ToString() + "/12";
        BlackMount.text = "x" + black.ToString() + "/12";

        record = (12 - black) * 10;
        if (record.ToString().Length == 1)
        {
            Record.text = "000" + record.ToString();
            record_t = "000" + record.ToString();
        }
        else if (record.ToString().Length == 2)
        {
            Record.text = "00" + record.ToString();
            record_t = "00" + record.ToString();
        }
        else if (record.ToString().Length == 3)
        {
            Record.text = "0" + record.ToString();
            record_t = "0" + record.ToString();
        }
        else
        {
            Record.text = record.ToString();
            record_t = record.ToString();
        }

        if (start)
        {
            
            text.SetActive(true);
            time = time - Time.deltaTime;
            time_t.text = Mathf.Round(time).ToString();
        }

        if (time < 1)
        {
            text.SetActive(false);
            time = 2;
            start = false;
            start_time = true;
            StartBtn.SetActive(false);
            BlockPanel.SetActive(false);
        }

        if (start_time)
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

        if (Input.GetKeyUp(KeyCode.S))
        {
            Save(new List<BoardConfiguration>());
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            Load();
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            Clear();
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            Test();
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            Debug.Log("historic size: " + historic.Count);
        }
    }

    public void StartButton()
    {
        if (start == false)
        start = true;
    }
    /// <summary>
    /// Random function to write some persitance tests in it.
    /// </summary>
    /// <remarks>
    /// Only used for Debug Purpose.
    /// </remarks>
    public void Test()
    {
        BoardConfiguration foo =
                new BoardConfiguration("b#b###################w######w####B###b#########W########w###w##");
        Movement movement = new Movement(new IntVector2(7, 1),
            new IntVector2(3, 5), new IntVector2(5, 3));
        foo.AddMovement(movement, 5f);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gameStorage.dat", FileMode.Open);
        historic = (List<BoardConfiguration>)bf.Deserialize(file);
        file.Close();

        bool result = false;
        bool result2 = false;
        foreach (BoardConfiguration bc in historic)
        {
            if (bc.Equals(foo))
            {
                result = true;
                Debug.Log("find: " + bc.ToString());
                
                Debug.Log("movement contains: " + 
                    bc.HasMovementConfiguration(foo.GetMovementsConfigurations()[0]));
            }
        }

        result2 = historic.Contains(foo);

        Debug.Log("result for: " + result + "\nresult contain: " + result2);
    }

    /// <summary>
    /// Increment and save the game historic in the 'gameStorage' file.
    /// </summary>
    public void Save(List<BoardConfiguration> list)
    {
        Debug.Log("Save Called.");
        // Create a File.
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameStorage.dat");

        // Add the new board configuration list in the historic.
        bool existsConf;
        foreach (BoardConfiguration config in list)
        {
            existsConf = false;
            foreach(BoardConfiguration historicConfig in historic)
            {
                // See if it's has a new Movement for that existing configuration.
                if (historicConfig.Equals(config))
                {
                    existsConf = true;
                    if (!historicConfig.HasMovementConfiguration(config.GetMovementsConfigurations()[0]))
                    {
                        historicConfig.AddMovement(config.GetMovementsConfigurations()[0].GetMove(),
                        config.GetMovementsConfigurations()[0].GetAdaptation());
                    } 
                }
            }
            if (!existsConf)
            {
                historic.Add(config);
            }
        }

        bf.Serialize(file, historic);
        file.Close();
    }

    /// <summary>
    /// Load the game historic in the 'gameStorage' file.
    /// </summary>
    public void Load()
    {
        Debug.Log("Load Called.");
        if (File.Exists(Application.persistentDataPath + "/gameStorage.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameStorage.dat", FileMode.Open);
            historic = (List<BoardConfiguration>) bf.Deserialize(file);
            file.Close();

            /*
            foreach(BoardConfiguration conf in historic)
            {
                Debug.Log(conf.ToString());
            }
            */
        }
    }

    /// <summary>
    /// Clear the game historic in the 'gameStorage' file.
    /// </summary>
    /// <remarks>
    /// Only used for Debug Purpose.
    /// </remarks>
    public void Clear()
    {
        Debug.Log("Clear Called.");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameStorage.dat");
        bf.Serialize(file, new List<BoardConfiguration>());
        file.Close();

    }

    /// <summary>
    /// Change the turn between the enemy and the player.
    /// Also update the UI text.
    /// </summary>
    public void NextTurn()
    {
        board.RefreshAllPieces();
        if (turn == Turn.playerTurn)
        {
            turn = Turn.enemyTurn;
            turnText.text = "VEZ DO ADIVERSÁRIO";
            StartCoroutine(BotPlay());

        }
        else
        {
            turn = Turn.playerTurn;
            turnText.text = "SUA VEZ";
            board.SomePieceCanCapture();
            player.Play();
        }
    }

    IEnumerator BotPlay()
    {
        yield return new WaitForSeconds(1.0f);
        bot.Play();
    }

    public void SendToPlayer(TileHandler tile)
    {
        player.SelectionHandler(tile);
    }

    public void NotifyPlayerEndOfMovement()
    {
        bool isSucessiveCapture = false;
        float finalValue = -100f;
        IsInFinals();
        if (turn == Turn.playerTurn) {
            // See if just move a king piece
            RefreshDrawCounters(player);

            player.NotifyEndOfMovement();
            isSucessiveCapture = player.GetIsSucessiveCapture();
            if (!isSucessiveCapture)
                board.RefreshAllPieces();
            // Verify if the player won the game.
            if (WinGame(bot, this.board.GetEnemyPieces()) && resultPanel != null)
            {
                finalValue = -30f;
                PlayerPrefs.SetString("CheckersResalt", "ПОБЕДА");
                if (min_time == 0 || min_time > sec)
                {
                    PlayerPrefs.SetFloat("CheckersMinTime", sec);
                    PlayerPrefs.SetString("CheckersMinTimeText", (full_time_t.text).ToString());
                }
                PlayerPrefs.SetString("CheckersRecord", "0120");
                PlayerPrefs.SetString("CheckersTime", (full_time_t.text).ToString());
                SceneManager.LoadScene(16);
            }
        }
        else
        {
            // See if just move a king piece
            RefreshDrawCounters(bot);

            bot.NotifyEndOfMovement();
            isSucessiveCapture = bot.GetIsSucessiveCapture();
            if (!isSucessiveCapture)
                board.RefreshAllPieces();
            // Verify if the bot won the game.
            if (WinGame(player, this.board.GetPlayerPieces()))
            {
                finalValue = 30f;
                PlayerPrefs.SetString("CheckersResalt", "ПОРАЖЕНИЕ");
                PlayerPrefs.SetString("CheckersTime", (full_time_t.text).ToString());
                SceneManager.LoadScene(16);
            }
        }

        if (inFinal)
            finalCounter += 1;

        if(turnsKingMoving >= 20 || finalCounter >= 10)
        {
            finalValue = 10f;
            PlayerPrefs.SetString("CheckersResalt", "ПОБЕДА");
            if (min_time == 0 || min_time > sec)
            {
                PlayerPrefs.SetFloat("CheckersMinTime", sec);
                PlayerPrefs.SetString("CheckersMinTimeText", (full_time_t.text).ToString());
            }
            PlayerPrefs.SetString("CheckersRecord", "0120");
            PlayerPrefs.SetString("CheckersTime", (full_time_t.text).ToString());
            SceneManager.LoadScene(16);
        }
        if (isGameOver)
        {
            if(finalValue > -100f)
                PlayerPrefs.SetString("CheckersResalt", "ПОРАЖЕНИЕ");
            PlayerPrefs.SetString("CheckersTime", (full_time_t.text).ToString());
            SceneManager.LoadScene(16);
            bot.SetLastMovement(finalValue);
            Save(bot.GetConfigList());
        }
        if(!isSucessiveCapture && !isGameOver)
            this.NextTurn();
    }

    /// <summary>
    /// Verify the winning condition given a player.
    /// </summary>
    /// <remarks>
    /// #### CONDITIONS ####
    /// 1- the player hasn't pieces.
    /// 2- the player can't move the pieces his has.
    /// </remarks>
    private bool WinGame(AbstractPlayer absEnemy, ArrayList enemiesPieces)
    {
        return enemiesPieces.Count == 0 ||
            (!absEnemy.SomePieceCanCapture(enemiesPieces) &&
            !absEnemy.SomePieceCanWalk(enemiesPieces));
    }

    /// <summary>
    /// Updates the turnKingMoving variable that is incremented
    /// when some player just moves a king.
    /// </summary>
    private void RefreshDrawCounters(AbstractPlayer absPlayer)
    {
        if (absPlayer.UsedKingPiece() && !absPlayer.GetIsCapturing())
        {
            turnsKingMoving += 1;
        }
        else
        {
            turnsKingMoving = 0;
        }
    }

    /// <summary>
    /// See the condition to start with the final countdown.
    /// If the game do not finish
    /// </summary>
    private void IsInFinals ()
    {
        if ( !inFinal &&
            this.board.GetPlayerPieces().Count <= 2 &&
            this.board.GetEnemyPieces().Count <= 2 &&
            this.board.NumberOfPlayerManPieces() + this.board.NumberOfEnemyManPieces() <= 1 )
        {
            Debug.Log("Estamos em finais");
            inFinal = true;
        }
    }

    /// <summary>
    /// Finish the game.
    /// Open the result panel with the text given as parameter.
    /// </summary>
    private void ShowResultPanel(string text, bool hasPlayerWin)
    {
        isGameOver = true;
        if (resultPanel.gameObject.activeSelf)
            return;
        resultPanel.GetComponent<PanelController>().PlaySound(hasPlayerWin);
        Text resultText = resultPanel.transform.GetChild(0).GetComponent<Text>();
        resultText.text = text;
    }

    /// <summary>
    /// Return true if is the player turn.
    /// </summary>
    public bool IsPlayerTurn()
    {
        if (turn == Turn.playerTurn)
            return true;
        return false;
    }

    public List<BoardConfiguration> getHistoric()
    {
        return historic;
    }
}

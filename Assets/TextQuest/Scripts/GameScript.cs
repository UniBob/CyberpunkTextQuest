using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameScript : MonoBehaviour
{
    //UI
    [SerializeField] private AudioSource source;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private Text titleTextBox;
    [SerializeField] private Button[] buttons;
    [SerializeField] private Text[] buttonsTextBox;
    //Pages
    [SerializeField] private PageData[] pages;
    [SerializeField] private int pageDataIteraror;
    //Other variables
    [SerializeField] private float textWritingSpeed;
    private float timer;
    private string currentWritingText = "";
    private int currentWritingTextIterator;
    private string skipButtonText = "Пропустить";


    //private int[] frasesCount;


    private void Start()
    {
        //frasesCount = new int[9];
        //for (int i = 0; i < pages.Length; i++)
        //{
        //    frasesCount[(int)pages[i].character]++;
        //}
        //Debug.Log(Character.Pepe.ToString() + " " + frasesCount[(int)Character.Pepe].ToString());
        //Debug.Log(Character.Meredit.ToString() + " " + frasesCount[(int)Character.Meredit].ToString());
        //Debug.Log(Character.Thoughts.ToString() + " " + frasesCount[(int)Character.Thoughts].ToString());
        //Debug.Log(Character.Vakako.ToString() + " " + frasesCount[(int)Character.Vakako].ToString());
        //Debug.Log(Character.Liana.ToString() + " " + frasesCount[(int)Character.Liana].ToString());
        //Debug.Log(Character.Smowsher.ToString() + " " + frasesCount[(int)Character.Smowsher].ToString());
        //Debug.Log(Character.Victor.ToString() + " " + frasesCount[(int)Character.Victor].ToString());
        //Debug.Log(Character.Courier.ToString() + " " + frasesCount[(int)Character.Courier].ToString());
        //Debug.Log(Character.Jonny.ToString() + " " + frasesCount[(int)Character.Jonny].ToString());
        LoadStartState();
        ChangeState(pageDataIteraror);
    }

    private void LoadStartState()
    {
        if (PlayerPrefs.HasKey("pageNumber"))
        {
            pageDataIteraror = PlayerPrefs.GetInt("pageNumber");
        }
        else
            pageDataIteraror = 0;
    }

    void SaveGame(int pageNumber)
    {
        PlayerPrefs.SetInt("pageNumber", pageNumber);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        if (currentWritingTextIterator < currentWritingText.Length)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                textBox.text = textBox.text + currentWritingText[currentWritingTextIterator++];
                timer += textWritingSpeed;
            }
        }
        else
        {
            for (int i = 0; i < pages[pageDataIteraror].buttonText.Length; i++)
            {
                buttons[i].gameObject.SetActive(true);
                buttonsTextBox[i].text = pages[pageDataIteraror].buttonText[i];
            }
        }
    }

    public void ButtonAction(int i)
    {
        if (currentWritingTextIterator < currentWritingText.Length)
        {
            StartSubtitle(pageDataIteraror);
            textBox.text += currentWritingText;
            currentWritingTextIterator = currentWritingText.Length;
        }
        else
        {
            ChangeState(pages[pageDataIteraror].nextPage[i]);
            pageDataIteraror = pages[pageDataIteraror].nextPage[i];
        }
    }

    private void SaveCheckPoint()
    {
        int i = pageDataIteraror;
        while (!pages[i].isCheckPoint)
        {
            i--;
        }
        SaveGame(i);

    }

    public void ResetSaves()
    {
        PlayerPrefs.SetInt("pageNumber", 0);
        PlayerPrefs.Save();
        pageDataIteraror = 0;
        ChangeState(pageDataIteraror);
    }

    private void ChangeState(int i)
    {
        source.Stop();
        switch (i)
        {
            //GAMOVER
            case 666666:
                SaveCheckPoint();
                SceneManager.LoadScene(1);
                break;

            //VA11-HALLa
            case 77100:
                pageDataIteraror++;
                SaveGame(pageDataIteraror);
                SceneManager.LoadScene(2);
                break;

            //PINBALL
            case 151221:
                Debug.Log("");
                break;

            default:
                source.clip = pages[i].voice;
                backgroundImage.sprite = pages[i].backgroundImage;
                currentWritingText = pages[i].textboxText;
                StartSubtitle(i);
                titleTextBox.text = pages[i].titleText;
                currentWritingTextIterator = 0;
                buttons[0].gameObject.SetActive(true);
                buttons[0].GetComponentInChildren<Text>().text = skipButtonText;
                buttons[1].gameObject.SetActive(false);
                source.Play();
                break;
        }
    }

    private void StartSubtitle(int i)
    {
        switch (pages[i].character)
        {
            case Character.Meredit:
                textBox.text = "<color=#EA524F>Мередит:</color> ";
                break;
            case Character.Pepe:
                textBox.text = "<color=#EA524F>Пепе:</color> ";
                break;
            case Character.Jonny:
                textBox.text = "<color=#EA524F>Джонни:</color> ";
                break;
            case Character.Thoughts:
                textBox.text = "";
                break;
            case Character.Vakako:
                textBox.text = "<color=#EA524F>Вакако:</color> ";
                break;
            case Character.Liana:
                textBox.text = "- ";
                break;
            case Character.Smowsher:
                textBox.text = "";
                break;
            case Character.Victor:
                textBox.text = "<color=#EA524F>Виктор:</color> ";
                break;
            case Character.Courier:
                textBox.text = "<color=#EA524F>Курьер:</color> ";
                break;
        }
    }
    public void QuitGame()
    {
        SaveGame(pageDataIteraror);
        Application.Quit();
    }
}

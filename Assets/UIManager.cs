using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private int distance;
    [SerializeField] private List<Image> distanceNos;
    [SerializeField] private List<Sprite> numberSprites;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject gameOverScreen;

    public List<GameObject> gameOverMenus;

    bool pauseState = false;
    bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    void updateDistanceCounter()
    {
        string distanceString = (Mathf.Clamp(distance, 0, 9999999)).ToString();

        for (int i = 0; i < distanceNos.Count; i++)
        {
            RectTransform digitTransform = distanceNos[i].rectTransform;
            digitTransform.anchoredPosition = new Vector2(digitTransform.anchoredPosition.x, -74 - Mathf.Sin(1.5f * Time.unscaledTime + i) * 2);

            if (i > distanceString.Length - 1)
            {
                distanceNos[i].color = Color.clear;
            }
            else
            {
                distanceNos[i].color = Color.white;
                distanceNos[i].sprite = numberSprites[int.Parse(distanceString[i].ToString())];
            }
        }
    }

    public void showGameOverScreen()
    {
        if (!gameOver)
        {
            gameOver = true;
            gameOverScreen.SetActive(true);

            pauseScreen.SetActive(false);
            gameUI.SetActive(false);
        }
    }

    public void setGameOverMenu(int id)
    {
        for (int i = 0; i < gameOverMenus.Count; i++)
        {
            gameOverMenus[i].SetActive(i == id);
        }
    }
    public void setPauseState(bool open)
    {
        pauseState = open;

        pauseScreen.SetActive(open);
        gameUI.SetActive(!open);
        
        if (open)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void playAgain()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void quitToTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    // Update is called once per frame
    void Update()
    {
        distance = (int)player.transform.position.x ;

        updateDistanceCounter();

        if (Input.GetKeyDown("escape") && !gameOver)
        {
            setPauseState(!pauseState);
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 1500f;
    public int health = 5;
    private int score = 0;
    public Text scoreText;
    public Text healthText;
    public Image winLoseImage;


    IEnumerator LoadScene(float seconds)
    {
        health = 5;
        score = 0;
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnWin()
    {
        Text winText = winLoseImage.GetComponentInChildren<Text>();
        winText.text = "You win!";
        winText.color = Color.black;
        winLoseImage.color = Color.green;
        winLoseImage.gameObject.SetActive(true);
        StartCoroutine(LoadScene(3));
    }

    void OnLose()
    {
        Text loseText = winLoseImage.GetComponentInChildren<Text>();
        loseText.text = "Game Over!";
        loseText.color = Color.white;
        winLoseImage.color = Color.red;
        winLoseImage.gameObject.SetActive(true);
        StartCoroutine(LoadScene(3));
    }
    void SetHealthText()
    {
        healthText.text = $"Health: {--health}";
    }

    void SetScoreText()
    {
        scoreText.text = $"Score: {++score}";
    }

    void Update()
    {
        if (health == 0)
            OnLose();
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene("menu");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("a"))
            rb.AddForce(-speed * Time.deltaTime, 0, 0);
        if (Input.GetKey("d"))
            rb.AddForce(speed * Time.deltaTime, 0, 0);
        if (Input.GetKey("w"))
            rb.AddForce(0, 0, speed * Time.deltaTime);
        if (Input.GetKey("s"))
            rb.AddForce(0, 0, -speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Pickup"))
        {
            SetScoreText();
            Destroy(other.gameObject);
        }

        if (other.GetComponent<Collider>().CompareTag("Trap"))
            SetHealthText();

        if (other.GetComponent<Collider>().CompareTag("Goal"))
            OnWin();
    }
}

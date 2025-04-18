using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI; // Thêm dòng này để sử dụng Text

public class GameManagerEditTests
{
    GameManager gameManager;
    GameObject gameManagerObject;
    GameObject playButton;
    GameObject gameOverUI;
    Text scoreText;
    GameObject scoreTextObject;

    [SetUp]
    public void Setup()
    {
        gameManagerObject = new GameObject("GameManager");
        gameManager = gameManagerObject.AddComponent<GameManager>();

        playButton = new GameObject("PlayButton");
        playButton.SetActive(false);

        gameOverUI = new GameObject("GameOverUI");
        gameOverUI.SetActive(false);

        scoreTextObject = new GameObject("ScoreText");
        scoreText = scoreTextObject.AddComponent<Text>();
        scoreText.text = "0";

        gameManager.playButton = playButton;
        gameManager.gameOver = gameOverUI;
        gameManager.scoreText = scoreText;
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(gameManagerObject);
        GameObject.DestroyImmediate(playButton);
        GameObject.DestroyImmediate(gameOverUI);
        GameObject.DestroyImmediate(scoreTextObject);
    }

    [Test]
    public void IncreaseScore_UpdatesScoreAndTextCorrectly()
    {
        Assert.AreEqual(0, gameManager.score, "Initial score should be 0.");
        Assert.AreEqual("0", scoreText.text, "Initial score text should be 0.");

        gameManager.IncreaseScore();

        Assert.AreEqual(1, gameManager.score, "Score should increase to 1.");
        Assert.AreEqual("1", scoreText.text, "Score text should update to '1'.");
    }
}

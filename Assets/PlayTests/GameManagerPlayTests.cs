using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class GameManagerPlayTests
{
    const string MainSceneName = "Flappy Bird";
    GameManager gameManager;
    Player player;

    [UnitySetUp]
    public IEnumerator UnitySetUp()
    {
        Debug.Log("--- Test UnitySetUp START: Loading scene (MainSceneName) ---");
        AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(MainSceneName, LoadSceneMode.Single);
        if (sceneLoadOperation == null)
        {
            Assert.Fail($"Failed to start loading scene ({MainSceneName}). Check scene name and Build Settings.");
            yield break;
        }

        while (!sceneLoadOperation.isDone)
        {
            yield return null;
        }

        gameManager = Object.FindObjectOfType<GameManager>();
        player = Object.FindObjectOfType<Player>();
        Assert.IsNotNull(gameManager, "GameManager reference is null after scene load.");
        Assert.IsNotNull(player, "Player reference is null after scene load.");
    }

    [UnityTest]
    public IEnumerator GameStartsPausedAndPlayerDisabled()
    {
        Assert.AreEqual(0f, Time.timeScale, "Game should start with Time.timeScale = 0.");
        Assert.IsFalse(player.enabled, "Player should be disabled initially.");
        Assert.IsTrue(gameManager.playButton.activeSelf, "Play button should be active initially.");
        yield return null; // Yield to allow frame update
    }

    [UnityTest]
    public IEnumerator PlayButtonStartsGame()
    {
        Assert.AreEqual(0f, Time.timeScale, "Precondition failed: TimeScale not 0.");
        Assert.IsFalse(player.enabled, "Precondition failed: Player not disabled.");

        gameManager.Play();
        yield return null; // Yield to allow frame update

        Assert.AreEqual(1f, Time.timeScale, "Time.timeScale should be 1 after Play.");
        Assert.IsTrue(player.enabled, "Player should be enabled after Play.");
        Assert.IsFalse(gameManager.playButton.activeSelf, "Play button should be inactive after Play.");
        Assert.IsFalse(gameManager.gameOver.activeSelf, "Game Over UI should be inactive after Play.");
        Assert.AreEqual(0, gameManager.score, "Score should be 0 after Play.");
        Assert.AreEqual("0", gameManager.scoreText.text, "Score text should be '0' after Play.");
    }

    [Test]
    public void GameManagerPlayTestsSimplePasses()
    {
        // Use the Assert class to test conditions
        Assert.Pass("Simple test passed.");
    }

    [UnityTest]
    public IEnumerator GameManagerPlayTestsWithEnumeratorPasses()
    {
        yield return null;
        Assert.Pass();
    }
}

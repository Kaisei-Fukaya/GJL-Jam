using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalStage : MonoBehaviour
{
    GameManager _gameManager;
    PostProcessingController _postProcessingController;
    bool _musicTriggered = false;
    [SerializeField] string[] _finalMessages;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _postProcessingController = PostProcessingController.Instance;
    }

    public void TriggerFinalStage()
    {
        _gameManager.finalStage = true;
    }

    public void BackToStart()
    {
        StartCoroutine(FadeOut());
    }

    public void DestroyEnemies()
    {
        var enemies = FindObjectsOfType<FlyingRobotAI>();
        foreach (var e in enemies)
        {
            Destroy(e);
        }
    }

    public void PlayMessage()
    {
        MessageToPlayer.Instance.DisplayMessage(_finalMessages, 2f, 4f);
    }

    public void ResetPostProcessingNoise()
    {
        PostProcessingController.Instance.SetNoiseIntensity(0f);
    }

    public void SetMusicToFinal()
    {
        if (!_musicTriggered)
        {
            MusicPlayer.Instance.PlayMusic(1);
            _musicTriggered = true;
        }
    }


    IEnumerator FadeOut()
    {
        float intensity = 0f;
        _postProcessingController.SetVignetteColour(Color.black);
        GameManager.Instance.EndGameFinal();
        while (true)
        {
            if (intensity < 1f)
            {
                _postProcessingController.SetVignetteIntensity(intensity);
                intensity += 0.2f * Time.deltaTime;
            }
            else
            {
                MenuManager.Instance.ChangeScene(2);
                break;
            }
            yield return null;
        }
    }
}

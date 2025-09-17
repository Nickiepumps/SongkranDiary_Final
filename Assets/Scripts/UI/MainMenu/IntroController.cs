using UnityEngine;
using UnityEngine.Playables;

public class IntroController : MonoBehaviour
{
    [Header("Intro Reference")]
    [SerializeField] SceneController sceneController;
    [SerializeField] PlayableDirector playableDirector;

    [Header("Outro Reference")]
    [SerializeField] private Animation outroAnim;
    public void SkipIntro()
    {
        playableDirector.Stop();
        sceneController.ChangeScene("Tutorial");
    }
}

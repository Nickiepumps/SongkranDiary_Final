using UnityEngine;
using UnityEngine.Playables;

public class IntroController : MonoBehaviour
{
    [SerializeField] SceneController sceneController;
    [SerializeField] PlayableDirector playableDirector;
    public void SkipIntro()
    {
        playableDirector.Stop();
        sceneController.ChangeScene("Tutorial");
    }
}

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LoadLevelOnClick : MonoBehaviour
{
    [SerializeField] private Image screenCover;
    [SerializeField] private FloatReference duration;
    [SerializeField] private int sceneIndex;

    public void LoadForest()
    {
        screenCover.DOFade(1, duration.Value).OnComplete(() => EventsManager.OnLoadLevel.Invoke(sceneIndex));
    }
}
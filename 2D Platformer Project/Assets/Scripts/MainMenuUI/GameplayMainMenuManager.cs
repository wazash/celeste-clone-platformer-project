using UnityEngine;

public class GameplayMainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuWindow;

    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        ShowGameplayMainMenu(player.InputHandler.PauseInput);

    }

    public void ShowGameplayMainMenu(bool value)
    {
        if (value)
        {
            player.InputHandler.PauseInput = true;
            if (player.InputHandler.playerInput.currentActionMap.name != "UI")
            {
                player.InputHandler.playerInput.SwitchCurrentActionMap("UI");
            }
            menuWindow.SetActive(true);
        }
        else
        {
            player.InputHandler.PauseInput = false;
            if(player.InputHandler.playerInput.currentActionMap.name != "Gameplay")
            {
                player.InputHandler.playerInput.SwitchCurrentActionMap("Gameplay");
            }
            menuWindow.SetActive(false);
        }
    }
}

using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Tooltip("The button to use for showing menu")]
    public ControllerButton button = ControllerButton.Menu;

    private void Awake()
    {
        gameObject.SetActive(false);
        ViveInput.AddPressDown(HandRole.LeftHand, button, ShowMenu);
        ViveInput.AddPressDown(HandRole.RightHand, button, ShowMenu);
    }

    private void OnDestroy()
    {
        ViveInput.RemovePressDown(HandRole.LeftHand, button, ShowMenu);
        ViveInput.RemovePressDown(HandRole.RightHand, button, ShowMenu);
    }

    public void ShowMenu()
    {
        GetComponent<TeleportToPlayer>().Teleport();
        gameObject.SetActive(true);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

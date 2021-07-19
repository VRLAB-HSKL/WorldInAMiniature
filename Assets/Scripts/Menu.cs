using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Anzeige eines Menüs durch Klicken des Menu-Buttons auf einem Controller (rechts oder links).
/// 
/// Das Menu erscheint vor dem Spieler. Es kann die Welt zurückgesetzt werden oder die 3D Miniatur
/// vor den Spieler teleportiert werden.
/// </summary>
[RequireComponent(typeof(TeleportThisToPlayer))]
public class Menu : MonoBehaviour
{
    /// <summary>
    /// Button welcher zum Öffnen des Menüs verwendet wird.
    /// </summary>
    [Tooltip("Welcher Button öffnet das Menü?")]
    public ControllerButton button = ControllerButton.Menu;

    /// <summary>
    /// Registriert Listener für den Button-Press und blendet das Menü initial aus.
    /// </summary>
    private void Awake()
    {
        gameObject.SetActive(false);
        ViveInput.AddPressDown(HandRole.LeftHand, button, ShowMenu);
        ViveInput.AddPressDown(HandRole.RightHand, button, ShowMenu);
    }

    /// <summary>
    /// Entfernt Listener für den Button-Press
    /// </summary>
    private void OnDestroy()
    {
        ViveInput.RemovePressDown(HandRole.LeftHand, button, ShowMenu);
        ViveInput.RemovePressDown(HandRole.RightHand, button, ShowMenu);
    }

    /// <summary>
    /// Teleportiert das Menu vor den Spieler und setzt es auf aktiv.
    /// </summary>
    public void ShowMenu()
    {
        GetComponent<TeleportThisToPlayer>().Teleport();
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Setzt die Welt zurück.
    /// </summary>
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

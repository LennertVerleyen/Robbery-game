using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class PanelManager: MonoBehaviour
{
    [SerializeField]
    private UIDocument doc;

    VisualElement root;

    private Button StartButton;
    private Button RestartButton;
    private Button QuitButton;
    private Button MainMenuButton;

    void Start()
    {
        root = doc.rootVisualElement;
        StartButton = root.Q<Button>("StartButton");
        QuitButton = root.Q<Button>("QuitButton");
        RestartButton = root.Q<Button>("RestartButton");
        MainMenuButton = root.Q<Button>("MainMenuButton");

        StartButton.clickable.clicked += StartButtonClicked;
        QuitButton.clickable.clicked += QuitButtonClicked;
        RestartButton.clickable.clicked += RestartButtonClicked;
        MainMenuButton.clickable.clicked += MainMenuButtonClicked;

        void StartButtonClicked()
        {
            SceneManager.LoadScene("GameScene");
        }

        void RestartButtonClicked()
        {
            SceneManager.LoadScene("GameScene");
        }

        void MainMenuButtonClicked()
        {
            SceneManager.LoadScene("MenuScene");
        }

        void QuitButtonClicked()
        {
            Application.Quit();
        }
    }
}

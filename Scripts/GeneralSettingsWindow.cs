using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProcGenMusic
{
    public class GeneralSettingsWindow : NetworkBehaviour
    {
        [SerializeField] public MusicGenerator mMusicGenerator;
        public GameObject generalSettingsWindow;            // Le panel de la fenêtre UI
        public FirstPersonCamera cameraController;          // Le script de la caméra
        public GameObject crosshair;                        // L'image de la croix (UI Image)
        public Button ExitButton;                                         
                                            
        public SettingsWindowController SettingsWindowController; //

        public bool IsWindowOpen = false;

        // Start is called before the first frame update
        void Awake()
        {
            // Initialise the state of the cursor and the window
            Cursor.lockState = IsWindowOpen ? CursorLockMode.None : CursorLockMode.Locked;
            generalSettingsWindow.SetActive(IsWindowOpen);
        }

        void Start()
        {
            ExitButton.onClick.AddListener(ExitPressed);
        }

        void Update()
        {
            // Attempt to open or close the settings window when ‘Escape’ is pressed
            if (Input.GetKeyDown(KeyCode.Escape) && SettingsWindowController.IsWindowOpen==false)
            {
                ToggleWindow();
            }

            
        }

        private void ExitPressed()
        {
            if(IsWindowOpen)
            {
                Application.Quit();
            }
        }

        // Toggle the display of the Escape window
        void ToggleWindow()
        {
            IsWindowOpen = !IsWindowOpen;
            generalSettingsWindow.SetActive(IsWindowOpen);

            // Blocking or unblocking camera movements
            cameraController.enabled = !IsWindowOpen;

            // Show or hide the mouse cursor
            Cursor.visible = IsWindowOpen;
            Cursor.lockState = IsWindowOpen ? CursorLockMode.None : CursorLockMode.Locked;

            // Show or hide the cross
            crosshair.SetActive(!IsWindowOpen);
        }


    }
}
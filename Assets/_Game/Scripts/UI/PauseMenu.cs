using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace _Game.Scripts.UI
{
    public class PauseMenu : MonoBehaviour
    {
        public static event Action OnPaused;
        public static event Action OnUnpaused;
        
        [SerializeField] private GameObject menuBackground;
        [SerializeField] private Page mainPage;

        private MenuController _menuController;
        private PlayerInputActions _playerInput;
        private bool _isPaused;

        public void SetMasterVolume(float sliderValue)
        {
            AudioManager.instance.SetMasterVolume(sliderValue);
        }

        public void SetMusicVolume(float sliderValue)
        {
            AudioManager.instance.SetMusicVolume(sliderValue);
        }

        public void SetSoundEffectsVolume(float sliderValue)
        {
            AudioManager.instance.SetSoundEffectsVolume(sliderValue);
        }
        
        public void ExitToMainMenu()
        {
            SceneManager.LoadScene("Main Menu");
        }

        public void TogglePause()
        {
            TogglePause(default);
        }

        private void TogglePause(InputAction.CallbackContext callbackContext)
        {
            _isPaused = !_isPaused;
            
            menuBackground.SetActive(_isPaused);
            mainPage.gameObject.SetActive(_isPaused);

            if (_isPaused)
            {
                _menuController.PushPage(mainPage);
                OnPaused?.Invoke();
            }
            else
            {
                _menuController.PopAllPages();
                OnUnpaused?.Invoke();
            }
        }

        private void Awake()
        {
            _menuController = GetComponent<MenuController>();
            _playerInput = new PlayerInputActions();
        }

        protected virtual void OnEnable()
        {
            _playerInput.Player.Pause.performed += TogglePause;

            _playerInput.Enable();
        }
        protected virtual void OnDisable()
        {
            _playerInput.Player.Pause.performed -= TogglePause;

            _playerInput.Disable();
        }
    }
}
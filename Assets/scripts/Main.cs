using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Maze
{
    public class Main : MonoBehaviour
    {
        private ListExecuteObject _interactiveObject; 
        private InputController _inputController;
        private Reference _reference;
        private ViewBonus _viewBonus;
        private ViewEndGame _viewEndGame;

        private CameraController _cameraController;

        private int _bonusCount;
        [SerializeField]private BadBonus badBonus;

        [SerializeField] private GameObject _player;
        [SerializeField] private Button _restartButton;
        void Awake()
        {
            Time.timeScale = 1f;
            _reference = new Reference();

            _inputController = new InputController(_player.GetComponent<Unit>());// обращаемся к базовому классу чз дочерний
            _cameraController = new CameraController(_player.transform, _reference.MainCamera.transform);
            _interactiveObject = new ListExecuteObject();// создание листа перечисляемых объектов


            _viewBonus = new ViewBonus(_reference.BonusLabel);
            _viewEndGame = new ViewEndGame(_reference.BonusLabel);

            _restartButton.onClick.AddListener(RestartGame);
            _restartButton.gameObject.SetActive(false);
            _interactiveObject.AddExecuteObject(_inputController);// добавляем _inputController в этот лист
            _interactiveObject.AddExecuteObject(_cameraController);
            
            foreach(var item in _interactiveObject)
            {
                if(item is GoodBonus goodBonus)
                {
                    goodBonus.AddPoints += AddBonus;
                }

                if( item is BadBonus badBonus)
                {
                    badBonus.OnCaugthPlayer += _viewEndGame.GameOver;
                    badBonus.OnCaugthPlayer += CaughtPlayer;
                }
            }
        }

        private void CaughtPlayer(string value, Color args)
        {
            _restartButton.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }

        private void AddBonus (int value)//метод обработчик
        {
            _bonusCount += value;
            _viewBonus.Display(_bonusCount);
        }

        private void RestartGame()// перезагрузка уровня
        {
            SceneManager.LoadScene(0);
        }

        void Update()
        {
            for( int i= 0; i< _interactiveObject.Length; i++)
            {
                
                if (_interactiveObject[i] == null)
                {
                    continue;
                }

                _interactiveObject[i].Update();
            }
        }
    }
}

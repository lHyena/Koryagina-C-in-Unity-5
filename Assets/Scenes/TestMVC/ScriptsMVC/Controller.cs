using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC
{
    public class Controller 
    {
        private Transform _playerT;
        private View _triggerT;
        private View _playerView;
        public Controller(View player, View trigger)
        {
            _playerView = player;
            _playerT = player._transform;
            _triggerT = trigger;

            _triggerT.OnLevelObjectContact += ControllerRecieveAction;
        }

        private void ControllerRecieveAction(Collider contactView, int Val, Transform valT)
        {
            Debug.Log("���������� �������: ��� ������� � ��������" + contactView.gameObject.name);
            GameObject.Destroy(contactView.gameObject);
        }

        public void MyUpdate()
        {

        }
    }
}


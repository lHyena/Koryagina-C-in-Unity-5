using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Maze
{
    public sealed class Player : Unit // sealed - �������, ��� ����� ���������;
    {
        private void Awake()
        {
            _transform = transform;

            if (GetComponent<Rigidbody>())
            {
                _rb = GetComponent<Rigidbody>();
            }

            isDead = false;
            Health = 100;
            
        }

        public override void Move(float x, float y, float z) // override ����������� ��� ���������� � ��������� 
        {
            if (_rb)
            {
                _rb.AddForce(new Vector3(x, y, z) * Speed);
            }
            else
            {
                Debug.Log("No Rigidbody");
            }

            
        }

    }
}

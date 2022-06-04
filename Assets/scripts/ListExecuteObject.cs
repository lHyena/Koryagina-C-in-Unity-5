using System.Collections;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Maze
{
    public sealed class ListExecuteObject : IEnumerator, IEnumerable // ��� ����������� ��������� ���������� ���������
                                                                     // IEnumerable - ���������� ������ �� �������������
                                                                     // IEnumerator - ���������� ���������� ��� �������� ��-���
    {
        private IExecute[] _interactiveObject;
        private int _index = -1;
        public object Current => _interactiveObject[_index];// Current - ��������� �������� ��-��
        public int Length => _interactiveObject.Length;
       
        public ListExecuteObject()
        {
            Bonus[] BonusObject = Object.FindObjectsOfType<Bonus>();
            for(int i= 0; i< BonusObject.Length; i++)
            {
                if(BonusObject[i] is IExecute intObj)
                {
                    AddExecuteObject(intObj);
                }
            }
        }

        public IExecute this[int curr]
        {
            get => _interactiveObject[curr];
            private set => _interactiveObject[curr] = value;
        }

        public void AddExecuteObject(IExecute execute)
        {
            if(_interactiveObject == null)// ��������� ������
            {
                _interactiveObject = new[] { execute };// ���� ������ Null(� ��� ������ ���) �� ������� ����� ������ � ��������� � ���� execute
                return;
            }

            Array.Resize(ref _interactiveObject, Length + 1);// ���� � ������� ���-�� ����
            _interactiveObject[Length - 1] = execute;// � ��������� � ��������� �������
        }

        public bool MoveNext() //��������� ��������� �� ���-��� ��-� �� �������� ������� � ������������������ � ��������� �� ����������� �� ���������
        {
            if(_index == Length - 1)
            {
                Reset();
                return false;// ���� ������������������ �����������, MoveNext ���������� false
            }
            _index++;
            return true; 
        }

        public void Reset()// ����������� � ������ � ���������
        {
            _index = -1;
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

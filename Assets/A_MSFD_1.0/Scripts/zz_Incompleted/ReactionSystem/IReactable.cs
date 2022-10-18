using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public interface IReactable
    {
        /// <summary>
        /// ��������� ��� ��������� �����, ��������� ����� ���� �������� �� ������� �������. 
        /// ���� ����� �������� ������������ ����� ���������, �� �� �������� ��� ����������� ��� ���� �������
        /// </summary>
        /// <param name="sender"></param>
        void Impact(ImpactData impactData);

        void AddListener(string impactType, Action<ImpactData> callback);
        void RemoveListener(string impactType, Action<ImpactData> callback);
    }
}
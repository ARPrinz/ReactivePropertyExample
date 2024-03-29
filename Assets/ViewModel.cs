using System;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ViewModel: MonoBehaviour
    {
        [SerializeField] private DataModel model;
        
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private Image icon;
        
        [SerializeField] private Color activeColor;
        [SerializeField] private Color inactiveColor;
        
        private void Awake()
        {
            model.ActiveFlag.BindToGOActivity(_gameObject);
            model.ActiveFlag.Select(b => b ? activeColor : inactiveColor).BindToTint(icon);
        }
    }
}
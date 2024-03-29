using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private DataModel model;

        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(model.SwitchFlag);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                model.SwitchFlag();
            }
        }
    }
}
using System;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace View
{
    public class PhoneNumberMenu : MonoBehaviour
    {
        [SerializeField] private GameObject inputFieldTMP;
        private TMP_InputField _inputField;

#if !UNITY_EDITOR &&UNITY_WEBGL 
    [DllImport("__Internal")]
    private static extern void GetPhoneNumber(string value);
    
#endif
        private void Start()
        {
            if (inputFieldTMP is null) throw new NullReferenceException("inputFieldTMP is null");
            _inputField = inputFieldTMP.GetComponent<TMP_InputField>();
            if (_inputField is null) throw new NullReferenceException("inputField  is null");
        }

        public void OnSubmit()
        {
            if (_inputField.text.Length == 12)
            {
                PlayerPrefs.SetInt("IsGameEnded", 1);
                var level = PlayerPrefs.GetInt("GameLevel");
                var discount = (level - 1) switch
                {
                    1 => 5,
                    2 => 10,
                    3 => 15,
                    _ => 0,
                };
                var res = _inputField.text + ";" + discount.ToString();
                Debug.Log(res);
                SceneManager.LoadSceneAsync(0);
#if !UNITY_EDITOR && UNITY_WEBGL
    GetPhoneNumber(res);
#endif
            }
        }
        

        public void OnChange()
        {
            if (_inputField.text.Length == 0 )
            {
                _inputField.text = "+";
            }

            if (_inputField.text[0] != '+')
            {
                _inputField.text = _inputField.text.Insert(0, "+");
            }

            var index = _inputField.text.LastIndexOf('+');
            if (index != 0)
            {
                _inputField.text=_inputField.text.Remove(index);
            }
        }
        
    }
}
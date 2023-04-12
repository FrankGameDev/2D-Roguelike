using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get { return _instance; } }
    private static UiManager _instance;


    //Player stats ref
    private PlayerParameters playerParameters;

    // - HEALTH
    public Slider healthSlider;
    private static Slider _healthSlider;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        playerParameters = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerParams;
    }


    // Start is called before the first frame update
    void Start()
    {
        _healthSlider = healthSlider;
        _healthSlider.maxValue = playerParameters.hp;
        _healthSlider.value = _healthSlider.maxValue;
    }


    public static void ChangeHpValue(float value) => _healthSlider.value -= value;

}

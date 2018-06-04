using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRain : MonoBehaviour
{
    public static ControllerRain Instance;
    
    private bool _isActive = true;

	private void Awake()
	{
        Instance = this;
        SetEmmition(false);
	}

	private void Update()
    {
		if(Quest.Instance != null)
        {
            SetEmmition(Quest.Instance.Status.Weather.Weather == QuestWeather.Type.Rain);
        }
	}
    
    private void SetEmmition(bool value)
    {
        if(_isActive != value)
        {
            _isActive = value;

            if (!value)
            {
                GetComponent<ParticleSystem>().Stop();
            }
            else
            {
                GetComponent<ParticleSystem>().Play();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ScreenCardsStatus : MonoBehaviour
{
    [SerializeField]
    private Text _day;

    [SerializeField]
    private Text _time;

    [SerializeField]
    private Text _weather;

	private void Update()
	{
        if (Quest.Instance != null)
        {
            _day.text = "ДЕНЬ " + (Quest.Instance.Status.Date.Day + 1).ToLeadingZerosString(2);
            _time.text = Quest.Instance.Status.Date.Hours.ToLeadingZerosString(2) + ":" + Quest.Instance.Status.Date.Minutes.ToLeadingZerosString(2);

            switch (Quest.Instance.Status.Weather.Weather)
            {
                case QuestWeather.Type.Normal:
                    _weather.text = "ОБЛАЧНО";
                    break;

                case QuestWeather.Type.Rain:
                    _weather.text = "ДОЖДЬ";
                    break;

                case QuestWeather.Type.Snow:
                    _weather.text = "СНЕГ";
                    break;
            }
            
            if(Quest.Instance.IsPause)
            {
                _time.gameObject.SetActive(Mathf.Sin(Time.realtimeSinceStartup * 10) > 0);
            }
            else
            {
                _time.gameObject.SetActive(true);
            }
        }
	}
}

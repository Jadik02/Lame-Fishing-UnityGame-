using UnityEngine;
using System.Collections;
using System;

public class WorldTime : MonoBehaviour
{
    public event EventHandler<TimeSpan> WorldTimeChanged;

    [SerializeField]
    private float _dayLength; //seconds

    private TimeSpan _currentTime;
    private float _minuteLength => _dayLength / WorldTimeConstants.MinutesInDay;
    private void Start()
    {
        _currentTime = TimeSpan.FromMinutes(600);
        StartCoroutine(AddMinute());
    }
    private IEnumerator AddMinute()
    {
        _currentTime += TimeSpan.FromMinutes(1);
        WorldTimeChanged?.Invoke(this, _currentTime);
        yield return new WaitForSeconds(_minuteLength);
        StartCoroutine(AddMinute());

    }
}

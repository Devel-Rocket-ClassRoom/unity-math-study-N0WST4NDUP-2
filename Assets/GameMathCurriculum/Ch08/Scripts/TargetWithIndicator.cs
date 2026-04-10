using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetWithIndicator : MonoBehaviour
{
    public GameObject Indicator;

    private Camera _cam;

    private void Awake()
    {
        var color = GetComponent<Renderer>().material.color;
        Indicator.GetComponent<Image>().color = color;
        Indicator.SetActive(false);
        _cam = Camera.main;
    }

    private void Update()
    {
        var screenPos = _cam.WorldToScreenPoint(transform.position);
        if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height)
        {
            Indicator.SetActive(true);
            Indicator.transform.position = new(
                Mathf.Clamp(screenPos.x, 0, Screen.width),
                Mathf.Clamp(screenPos.y, 0, Screen.height),
                0f);

            if (screenPos.z < 0)
            {
                Indicator.transform.position = new(
                    Screen.width - Indicator.transform.position.x,
                    Screen.height - Indicator.transform.position.y,
                    0f);
            }
        }
        else
        {
            Indicator.SetActive(true);
            Indicator.transform.position = new(
                Mathf.Clamp(screenPos.x, 0, Screen.width),
                Mathf.Clamp(screenPos.y, 0, Screen.height),
                0f);

            if (screenPos.z < 0)
            {
                Indicator.transform.position = new(
                    Screen.width - Indicator.transform.position.x,
                    Screen.height - Indicator.transform.position.y,
                    0f);
            }
        }
        Debug.Log($"{gameObject.name}'s Screen Pos: {screenPos}");
    }
}


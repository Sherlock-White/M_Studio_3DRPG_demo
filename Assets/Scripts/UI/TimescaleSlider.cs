using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimescaleSlider : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.RigisterSlider(GetComponent<Slider>());
    }
}

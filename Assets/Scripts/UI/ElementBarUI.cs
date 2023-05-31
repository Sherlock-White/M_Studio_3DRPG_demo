using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementBarUI : MonoBehaviour
{
    public GameObject weakElement;
    public GameObject strongElement;
    public GameObject exStrongElement;
    private Transform currentAmountBar;
    private Element element;

    private void OnEnable()
    {
        element.onAmountChanged += RefreshAmount;
        element.onDisappear += RemoveElementUI;
    }

    public void SetElementUIType(Element.ElementType type)
    {
        switch (type)
        {
            case Element.ElementType.WEAK:
                weakElement.SetActive(true);
                currentAmountBar = weakElement.transform.GetChild(1);
                strongElement.SetActive(false);
                exStrongElement.SetActive(false);
                break;
            case Element.ElementType.STRONG:
                weakElement.SetActive(false);
                strongElement.SetActive(true);
                currentAmountBar = strongElement.transform.GetChild(1);
                exStrongElement.SetActive(false);
                break;
            case Element.ElementType.EX_STRONG:
                weakElement.SetActive(false);
                strongElement.SetActive(false);
                exStrongElement.SetActive(true);
                currentAmountBar = exStrongElement.transform.GetChild(1);
                break;
        }
    }



    public void RefreshAmount(float amount, Element.ElementType type)
    {
        Image fillImage = currentAmountBar.GetComponent<Image>();
        if (fillImage == null) return;
        fillImage.fillAmount = amount / Element.GetInitialAmount(type);
    }

    public void RemoveElementUI(Element element)
    {
        // TODO: ÔªËØÏûÊ§ºóÒÆ³ýUI
        this.element.onDisappear -= RemoveElementUI;
        this.element.onAmountChanged -= RefreshAmount;
    }
}

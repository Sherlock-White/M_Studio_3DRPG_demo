using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementBarUI : MonoBehaviour
{
    public GameObject templatePrefab;
    private GameObject weakElement;
    private GameObject strongElement;
    private GameObject exStrongElement;
    private Transform pool;
    private Transform currentAmountBar;
    private Transform barPoint;
    private Element element;

    private void Awake()
    {
        weakElement = transform.Find("WeakElement").gameObject;
        strongElement = transform.Find("StrongElement").gameObject;
        exStrongElement = transform.Find("ExStrongElement").gameObject;
        pool = GameObject.Find("ElementPool").transform;
    }

    private void OnEnable()
    {
        element.onAmountChanged += RefreshAmount;
        element.onDisappear += ReturnToPool;
    }

    private void OnDisable()
    {
        element.onAmountChanged -= RefreshAmount;
        element.onDisappear -= ReturnToPool;
    }

    private void LateUpdate()
    {
        if (gameObject.activeSelf)
        {
            transform.position = barPoint.position;
            transform.forward = -Camera.main.transform.forward;
        }
    }

    private GameObject GetActiveElement(Const.ElementType type)
    {
        switch (type)
        {
            case Const.ElementType.WEAK:
                return weakElement;
            case Const.ElementType.STRONG:
                return strongElement;
            case Const.ElementType.EX_STRONG:
                return exStrongElement;
            default:
                Debug.LogError("invalid type input");
                return null;
        }
    }

    public void SetupElementUI(Element element, GameObject target)
    {
        this.element = element;
        weakElement.SetActive(false);
        strongElement.SetActive(false);
        exStrongElement.SetActive(false);
        GameObject activeElement = GetActiveElement(element.type);
        if (activeElement)
        {
            activeElement.SetActive(true);
            currentAmountBar = activeElement.transform.GetChild(1);
            Image fillImage = currentAmountBar.GetComponent<Image>();
            fillImage.color = element.GetElementColor();
        }
        barPoint = target.transform.Find("elementBarPoint");
        transform.position = barPoint.position;
    }

    public void RefreshAmount(Element element, float amount, Const.ElementType type)
    {
        if(this.element.key == element.key)
        {
            Image fillImage = currentAmountBar.GetComponent<Image>();
            if (fillImage == null) return;
            fillImage.fillAmount = amount / Element.GetInitialAmount(type);
        }
    }

    public void ReturnToPool(Element element)
    {
        if(this.element.key == element.key)
        {
            transform.SetParent(pool);
        }
    }
}

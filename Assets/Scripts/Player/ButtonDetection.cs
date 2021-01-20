using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDetection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	Vector3 originalScale;
	private void OnEnable()
	{
		originalScale = GetComponent<RectTransform>().localScale;
	}

	private void OnDisable()
	{
		GetComponent<RectTransform>().localScale = originalScale;
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		Player.instance.menus.setActiveButton(this.gameObject);
		Player.instance.menus.setMouseOnButton(true, this.gameObject);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Player.instance.menus.setMouseOnButton(false, null);
	}
}

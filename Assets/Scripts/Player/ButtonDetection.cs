using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDetection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public void OnPointerEnter(PointerEventData eventData)
	{
		Player.instance.menus.setActiveButton(this.gameObject);
		Player.instance.menus.setMouseOnButton(true, this.gameObject);
		Debug.Log("ON UI ELEMENT!");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("NOT ON UI ELEMENT!");
		Player.instance.menus.setMouseOnButton(false, null);
	}
}

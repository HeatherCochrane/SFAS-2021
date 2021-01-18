using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDetection : MonoBehaviour, IPointerEnterHandler
{
	public void OnPointerEnter(PointerEventData eventData)
	{
		Player.instance.menus.setActiveButton(this.gameObject);
	}
}

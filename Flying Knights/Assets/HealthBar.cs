using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

	public Slider slider;
	
	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;
	}

	public void SetHealth(int health)
	{
		slider.value = health;

	}

	public void Damage(int damageNumber)
    {
		float oldValue = slider.value;
		if (oldValue <= damageNumber)
        {
			slider.value = 0;
			//déclencher mort
        }
        else
        {
			slider.value = oldValue - damageNumber;
        }

    }

}

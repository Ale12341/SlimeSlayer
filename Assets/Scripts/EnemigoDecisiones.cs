using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemigoDecisiones : ScriptableObject
{
public abstract bool Decision(EnemigoControlador enemigoControlador);

}

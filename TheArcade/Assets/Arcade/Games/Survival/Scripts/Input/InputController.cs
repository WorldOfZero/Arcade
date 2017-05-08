using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : MonoBehaviour {
    public float Horizontal { get; protected set; }
    public float Vertical { get; protected set; }
}

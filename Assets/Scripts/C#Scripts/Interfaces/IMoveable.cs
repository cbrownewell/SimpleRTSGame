using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable<T>
{
    void MoveTo(T target);
}

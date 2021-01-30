using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRoomObject
{
    bool m_bActivate { get; set; }
    void ActivateObject(bool bActivate);
}
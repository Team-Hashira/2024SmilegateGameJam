using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgentComponent
{
    public void Initialize(Unit agent);
    public void AfterInit();
    public void Dispose();
}

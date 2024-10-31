using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnitComponent
{
    public void Initialize(Unit agent);
    public void AfterInit();
    public void Dispose();
}

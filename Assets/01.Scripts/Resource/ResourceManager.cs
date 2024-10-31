using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoSingleton<ResourceManager>
{
    public List<Resource> resourceList;

    /// <summary>
    /// 리소스 가져오는 함수
    /// </summary>
    public Resource GetResource(ResourceType type)
    {
        for (int i = 0; i < resourceList.Count; i++)
        {
            if(resourceList[i].type == type)
                return resourceList[i];
        }
        return null;
    }
    
    /// <summary>
    /// 리소스 개수 추가하는 함수
    /// </summary>
    public void AddResource(ResourceType type, int count = 1)
    {
        Resource resource = GetResource(type);

        if (resource == null)
        {
            resourceList.Add(new Resource(){type = type, count = count});
        }
        else
        {
            resource.count += count;
            resource.OnAmountChangedEvent?.Invoke(resource.count.ToString($"000000000"));
        }
    }
    
    
}

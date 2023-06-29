using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDeployerFactory : MonoBehaviour
{
    public static BaseBuffDeployer GetBuffDeployer(E_buff_Type buffDeployer)
    {
        string buffDeployerName = "BuffDeployer" + buffDeployer.ToString();
        Type type = Type.GetType(buffDeployerName);
        return Activator.CreateInstance(type) as BaseBuffDeployer;
    }
}

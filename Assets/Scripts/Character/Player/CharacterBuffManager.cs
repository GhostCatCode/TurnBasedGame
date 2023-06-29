using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBuffManager : MonoBehaviour
{
    private Character character;
    private List<BaseBuffDeployer> deployers = new List<BaseBuffDeployer>();

    private void Start()
    {
        character = GetComponent<Character>();
    }

    public void AddBuff(BuffData data)
    {
        BaseBuffDeployer buffDeployer = deployers.Find((x) => x.data.buffId == data.buffId);
        if (buffDeployer != null)
        {
            buffDeployer.MergeBuff(data);
        }
        else
        {
            buffDeployer = BuffDeployerFactory.GetBuffDeployer(data.type);
            buffDeployer.Init(data);
            deployers.Add(buffDeployer);
        }
    }

    public void RemoveBuff(int buffId)
    {
        BaseBuffDeployer buffDeployer = deployers.Find((x) => x.data.buffId == buffId);
        if (buffDeployer != null)
        {
            buffDeployer.RemoveBuff();
            deployers.Remove(buffDeployer);
        }
    }

    public void RemoveAllBuff()
    {
        for(int i = deployers.Count -1; i >= 0; i--)
        {
            BaseBuffDeployer buffDeployer = deployers[i];
            buffDeployer.RemoveBuff();
            deployers.Remove(buffDeployer);

        }
    }
}

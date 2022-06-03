using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CDR.MechSystem;

namespace CDR.ObjectPoolingSystem
{
    public enum Owner
	{
        Player1,
        Player2
	}

    public interface IPoolable //attach to scripts to pool
    {
        string ID { get; set; }
        Owner objectOwner { get; set; }

        void ResetObject();
        void Return();
    }

    public interface IPool
    {
        //IPoolable poolable { get; }
        //int poolSize { get; }
        //int activePoolables { get; }
        IActiveCharacter targetCharacter { get; }

        void Initialize();
        void Destroy();
        void ReturnAll();
        GameObject GetPoolable(string _id, Owner _PoolingOwner);
    }
}
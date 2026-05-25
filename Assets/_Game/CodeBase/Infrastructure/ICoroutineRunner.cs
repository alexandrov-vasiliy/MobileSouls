using System.Collections;
using UnityEngine;

namespace _Game.CodeBase.Infrastructure
{
    public interface ICoroutineRunner
    { 
        Coroutine StartCoroutine(IEnumerator routine);
    }
}
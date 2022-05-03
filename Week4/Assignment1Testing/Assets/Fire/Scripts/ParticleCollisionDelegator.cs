using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionDelegator : MonoBehaviour
{
    public bool ignoreChildSystems = true;
    [System.Serializable]
    public class Bind
    {
        public string psTag;
        public string methodName;
    }
    public List<Bind> bindings = new List<Bind>();
    private Dictionary<string,Bind> bindingsDictionary = new Dictionary<string,Bind>();
    private List<ParticleSystem> childSystems;
    // Start is called before the first frame update
    void Start()
    {
        childSystems = new List<ParticleSystem>(GetComponentsInChildren<ParticleSystem>());

        foreach (Bind bind in bindings){
            bindingsDictionary.Add(bind.psTag,bind);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        ParticleSystem ps = other.GetComponent<ParticleSystem>();
        if (ignoreChildSystems && childSystems.Contains(ps)) return;
        
        Bind bind = null;
        bindingsDictionary.TryGetValue(ps.tag,out bind);
        if (bind == null) return;

        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        ParticlePhysicsExtensions.GetCollisionEvents(ps,gameObject,collisionEvents);
        BroadcastMessage(bind.methodName,collisionEvents);
    }
}

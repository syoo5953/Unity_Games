using UnityEngine;

#pragma warning disable CS0618

public class ParticleController : Singleton<ParticleController>
{

    public GameObject[] effects;

    public Effect PlayParticles(string name, Transform effectPos) {
        //Effect effect = GameManager.Instance.objectPool.GetObject(name).GetComponent<Effect>();
        int idx = FindObj(name);
        Effect effect = Instantiate(effects[idx]).GetComponent<Effect>() ;
        effect.Init(effectPos);

        ParticleSystem[] particles = effect.particles;
        for(int i = 0; i < particles.Length; i++)
            particles[i].Play();

        return effect;
    }

    private int FindObj(string name)
    {
        for(int i = 0; i < effects.Length; i++)
        {
            if(effects[i].name.Equals(name))
            {
                return i;
            }
        }
        return 0;
    }
}
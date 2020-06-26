using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public AudioSource aSource;

    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    [System.Obsolete]
    void OnTriggerEnter2D(Collider2D coll)
    {
        Character character = coll.GetComponent<Character>();
        if (character)
        {
            aSource.Play();
            var HitBox = GameObject.Find("HitBox");
            var posBForLife = GameObject.Find("posBForLife");
            var PShb = HitBox.GetComponent<ParticleSystem>();
            character.EnhanceLife();
            var ps = GetComponent<ParticleSystem>();
            ps.loop = false;
            var sh = ps.shape;
            var main = ps.main;
            main.simulationSpace = ParticleSystemSimulationSpace.Custom;
            main.customSimulationSpace = posBForLife.transform;
            ps.startLifetime = 5;
            ps.startSpeed = 10;
            var VOL = ps.velocityOverLifetime;
            VOL.speedModifier = 20;
            sh.shapeType = ParticleSystemShapeType.Mesh;
        }
    }

    void Update() {   }
}

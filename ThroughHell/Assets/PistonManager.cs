using UnityEngine;
using System.Collections.Generic;

public class PistonManager : MonoBehaviour
{
    private static PistonManager instance;
    public static PistonManager Instance => instance;

    private List<PistonBlockData> pistons = new List<PistonBlockData>();

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        float delta = Time.deltaTime;
        foreach (var piston in pistons)
        {
            piston?.Update(delta);
        }
    }

    public void Register(PistonBlock block, float duration, bool startOpen)
    {
        if (pistons.Exists(p => p.block == block)) return;
        pistons.Add(new PistonBlockData(block, duration, startOpen));
    }

    //  Nested helper class
    private class PistonBlockData
    {
        public PistonBlock block;
        public float timer;
        public float duration;
        public bool isOpen;

        public PistonBlockData(PistonBlock block, float duration, bool startOpen)
        {
            this.block = block;
            this.duration = duration;
            this.isOpen = startOpen;
            this.timer = duration;
        }

        public void Update(float deltaTime)
        {
            timer -= deltaTime;
            if (timer <= 0f)
            {
                isOpen = !isOpen;
                timer = duration;
                block.ApplyState(isOpen);
            }
        }
    }
}
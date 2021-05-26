using System.Collections;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    public IEnumerator InstatiateParticleSystem(string player)
    {

        if (player == "red")
        {
            // instantiate 3 red fireballs at different spawnpoints with random velocities
            for (int i = 0; i < 3; i++)
            {
                if (!Player.destroyed)
                {
                    GameObject go = Instantiate(GameManager.instance.redFire, GameManager.instance.redSpawnPoints[i].position, GameManager.instance.redSpawnPoints[i].rotation);
                    go.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(-25f,-10f), 0, 0);
                    GameManager.instance.flameParticlesList.Add(go);
                    yield return new WaitForSeconds(0.2f);
                }
                
                
            }
            // destroy these fireballs after a few sconds if possible
            StartCoroutine(waitBeforeDestoryingFlameParticles());
        }
        else
        {
            // instantiate 3 blue fireballs at different spawnpoints with random velocities
            for (int i = 0; i < 3; i++)
            {
                if (!Player.destroyed)
                {
                    GameObject go = Instantiate(GameManager.instance.blueFire, GameManager.instance.blueSpawnPoints[i].position, GameManager.instance.blueSpawnPoints[i].rotation);
                    go.GetComponent<Rigidbody2D>().velocity = new Vector3(Random.Range(10f, 25f), 0, 0);
                    GameManager.instance.flameParticlesList.Add(go);
                    yield return new WaitForSeconds(0.2f);
                }
              

            }

            // destroy these fireballs after a few sconds if possible
            StartCoroutine(waitBeforeDestoryingFlameParticles());
        }

    }

    IEnumerator waitBeforeDestoryingFlameParticles()
    {
        yield return new WaitForSeconds(5f);
        GameManager.instance.DestroyParticleSystems();
    }




}

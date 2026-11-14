using UnityEngine;

namespace HedgeMaze {


    public class MaterialUpdater : MonoBehaviour {
        [SerializeField] private Material HedgeOne_Material;
        [SerializeField] private Material HedgeTwo_Material;

        public void SetMode(int mode) {
            if (mode == 0) //base
            {
                HedgeOne_Material.SetFloat("doRoses", (float)0.0);
                HedgeOne_Material.SetFloat("doSpooky", (float)0.0);
            }
            if (mode == 1) //Spooky
            {
                HedgeOne_Material.SetFloat("doRoses", (float)0.0);
                HedgeOne_Material.SetFloat("doSpooky", (float)1.0);
            }
            if (mode == 1) //Rosesa
            {
                HedgeOne_Material.SetFloat("doRoses", (float)1.0);
                HedgeOne_Material.SetFloat("doSpooky", (float)0.0);
            }

        }

    }
}

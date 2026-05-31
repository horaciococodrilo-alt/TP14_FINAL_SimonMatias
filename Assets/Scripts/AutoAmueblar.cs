using UnityEngine;
using UnityEditor;

public class AutoAmueblar : MonoBehaviour
{
    [MenuItem("Tools/Amueblar Aula")]
    static void Amueblar()
    {
        GameObject pupitreFBX = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Models/Pupitre.fbx");
        GameObject sillaFBX   = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Models/Silla.fbx");
        GameObject netbookFBX = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Models/Netbook.fbx");

        if (pupitreFBX == null || sillaFBX == null)
        {
            Debug.LogError("No encontre Pupitre.fbx o Silla.fbx en Assets/Models.");
            return;
        }

        GameObject viejo = GameObject.Find("Mobiliario");
        if (viejo != null) DestroyImmediate(viejo);
        GameObject contenedor = new GameObject("Mobiliario");

        float escala = 1.8f;

        float centroX = -8f;
        float centroZ = -6f;

        int filas = 4;
        int columnas = 4;
        float sepX = 2.5f;
        float sepZ = 3.0f;

        float rotPupitre   = 180f;
        float rotSilla     = 180f;
        float rotNetbook   = 0f;
        float offsetSillaZ = -0.9f;
        float alturaNetbook = 1.54f;

        float anchoTotal = (columnas - 1) * sepX;
        float largoTotal = (filas - 1) * sepZ;
        float startX = centroX - anchoTotal / 2f;
        float startZ = centroZ - largoTotal / 2f;

        int contador = 0;
        for (int f = 0; f < filas; f++)
        {
            for (int c = 0; c < columnas; c++)
            {
                float x = startX + c * sepX;
                float z = startZ + f * sepZ;

                // PUPITRE
                GameObject pup = (GameObject)PrefabUtility.InstantiatePrefab(pupitreFBX);
                pup.transform.position = new Vector3(x, 0f, z);
                pup.transform.rotation = Quaternion.Euler(0f, rotPupitre, 0f);
                pup.transform.localScale = Vector3.one * escala;
                pup.transform.SetParent(contenedor.transform);
                pup.name = "Pupitre_" + f + "_" + c;
                AgregarCollider(pup);

                // SILLA
                GameObject sil = (GameObject)PrefabUtility.InstantiatePrefab(sillaFBX);
                sil.transform.position = new Vector3(x, 0f, z + offsetSillaZ);
                sil.transform.rotation = Quaternion.Euler(0f, rotSilla, 0f);
                sil.transform.localScale = Vector3.one * escala;
                sil.transform.SetParent(contenedor.transform);
                sil.name = "Silla_" + f + "_" + c;
                AgregarCollider(sil);

                // NETBOOK (1 de cada 2) - recogible: tag + collider trigger
                if (netbookFBX != null && contador % 2 == 0)
                {
                    GameObject net = (GameObject)PrefabUtility.InstantiatePrefab(netbookFBX);
                    net.transform.position = new Vector3(x, alturaNetbook, z);
                    net.transform.rotation = Quaternion.Euler(0f, rotNetbook, 0f);
                    net.transform.localScale = Vector3.one * escala;
                    net.transform.SetParent(contenedor.transform);
                    net.name = "Netbook_" + f + "_" + c;
                    PrepararRecogible(net);
                }
                contador++;
            }
        }

        Debug.Log("Aula amueblada. Netbooks listas como recogibles.");
    }

    static void AgregarCollider(GameObject obj)
    {
        foreach (MeshFilter mf in obj.GetComponentsInChildren<MeshFilter>())
        {
            MeshCollider mc = mf.gameObject.GetComponent<MeshCollider>();
            if (mc == null)
                mc = mf.gameObject.AddComponent<MeshCollider>();
            mc.sharedMesh = mf.sharedMesh;
        }
    }

    // Marca la netbook como recogible: tag Coleccionable + BoxCollider trigger
    static void PrepararRecogible(GameObject net)
    {
        net.tag = "Coleccionable";
        BoxCollider bc = net.GetComponent<BoxCollider>();
        if (bc == null)
            bc = net.AddComponent<BoxCollider>();
        bc.isTrigger = true;
        // Agrandar un poco el area para que sea facil de recolectar al pasar cerca
        bc.size = new Vector3(0.5f, 0.4f, 0.5f);
        bc.center = new Vector3(0f, 0.15f, 0f);
    }
}
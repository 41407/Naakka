using System.Collections.Generic;
using UnityEngine;

namespace Tests.Helpers
{
    public static class TestFactory
    {
        static List<GameObject> InstantiatedObjects { get; } = new List<GameObject>();

        public static GameObject ConstructGameObject(string name = "TestFactoryCreatedObject")
        {
            var go = new GameObject(name);
            InstantiatedObjects.Add(go);
            return go;
        }

        public static void TearDown()
        {
            foreach (var obj in InstantiatedObjects)
            {
                TestHelpers.Destroy(obj);
            }

            InstantiatedObjects.Clear();
        }

        public static GameObject With<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.name == "TestFactoryCreatedObject")
                gameObject.name = typeof(T).Name;
            gameObject.AddComponent<T>();
            return gameObject;
        }

        public static GameObject WithChild(this GameObject parent, GameObject child)
        {
            child.transform.SetParent(parent.transform);
            return parent;
        }

        public static GameObject At(this GameObject gameObject, Vector3 position)
        {
            gameObject.transform.position = position;
            return gameObject;
        }
    }
}

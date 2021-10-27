using Object = UnityEngine.Object;

namespace Tests.Helpers
{
    public static class TestHelpers
    {
        public static void Destroy(Object obj)
        {
            if (obj) Object.DestroyImmediate(obj);
        }
    }
}

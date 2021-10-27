using NSubstitute;
using UnityEngine;
using Zenject;

namespace Tests.Helpers
{
    public static class ZenjectExtensions
    {
        public static ConcreteIdArgConditionCopyNonLazyBinder BindSubstitute<T>(this DiContainer container) where T : class
        {
            return container.Bind<T>().FromInstance(Substitute.For<T>()).AsSingle();
        }

        public static ConcreteIdArgConditionCopyNonLazyBinder BindSubstitute<T>(this DiContainer container, string id) where T : class
        {
            return string.IsNullOrEmpty(id)
                ? container.BindSubstitute<T>()
                : container.Bind<T>().WithId(id).FromInstance(Substitute.For<T>());
        }

        public static ScopeConcreteIdArgConditionCopyNonLazyBinder BindSubstituteMultiple<T>(this DiContainer container) where T : class
        {
            return container.Bind<T>().FromMethodMultiple(ctx => new[] {Substitute.For<T>(), Substitute.For<T>(), Substitute.For<T>()});
        }

        public static ConcreteIdArgConditionCopyNonLazyBinder BindComponentUnderTest<T>(this DiContainer container) where T : Component
        {
            return container.BindComponentUnderTest<T>(TestFactory.ConstructGameObject(typeof(T).Name)).AsSingle();
        }

        public static ScopeConcreteIdArgConditionCopyNonLazyBinder BindComponentUnderTest<T>(this DiContainer container, GameObject targetObject) where T : Component
        {
            return container.Bind<T>().FromNewComponentOn(targetObject);
        }
    }
}

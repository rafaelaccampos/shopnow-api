using AutoBogus;

namespace ShopNow.Tests.Shared.Builders
{
    public class BaseBuilder<T> : AutoFaker<T> where T : class
    {
        public BaseBuilder()
            : base("pt_BR")
        {
            Actions = new List<Action<T>>();
        }

        protected IList<Action<T>> Actions { get; set; }

        public override T Generate(string ruleSets = null)
        {
            var obj = base.Generate(ruleSets);
            foreach (var action in Actions)
            {
                action(obj);
            }

            return obj;
        }

        public override void Populate(T instance, string ruleSets = null)
        {
            base.Populate(instance, ruleSets);

            foreach(var action in Actions)
            {
                action(instance);
            }
        }
    }
}
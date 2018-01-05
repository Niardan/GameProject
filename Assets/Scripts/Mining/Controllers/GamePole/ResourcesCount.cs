namespace Assets.Scripts.Mining.Controllers.GamePole
{
    public class ResourcesCount
    {
        private readonly TypeResources _typeResources;
        private readonly int _countResources;

        public ResourcesCount(TypeResources typeResources, int countResources)
        {
            _typeResources = typeResources;
            _countResources = countResources;
        }

        public int CountResources {
            get
            {
                return _countResources;
            }
        }

        public TypeResources TypeResources {
            get
            {
                return _typeResources;
            }
        }
    }
}
namespace Infrastructure.FirebaseControllers
{
    public class FirebaseContainer
    {
        public FirebaseContainer(
            FirebaseInitializer firebaseInitializer,
            FirebaseEvents firebaseEvents)
        {
            FirebaseInitializer = firebaseInitializer;
            FirebaseEvents = firebaseEvents;
        }
        
        public FirebaseInitializer FirebaseInitializer { get; private set; }
        public FirebaseEvents FirebaseEvents { get; private set; }
    }
}

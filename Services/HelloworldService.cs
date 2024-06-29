namespace ApiPeople.Services
{
    public class HelloworldService : IHelloWorldService
    {
        public string GetHelloWorld()
        {
            return "Hello World!";
        }
    }

    public interface IHelloWorldService
    {
        string GetHelloWorld();
    }
}

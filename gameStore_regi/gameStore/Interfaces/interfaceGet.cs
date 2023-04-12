using Microsoft.AspNetCore.Mvc;

namespace gameStore.Interfaces
{
    public interface interfaceGet
    {
        IActionResult Get(string uId);
    }
}
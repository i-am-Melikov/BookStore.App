using BookStore.Service.Services.Implementations;

MenuService menuService = new MenuService();
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("1. As Admin");
Console.WriteLine("2. As User");

string request =Console.ReadLine();
if(request == "1")
{
    bool result= await menuService.Login();
    while (!result)
    {
          if (!result)
          {
              result = await menuService.Login();
              Console.WriteLine("2.Return As User");
              request = Console.ReadLine();
                if (request == "2")
                {
                    result = true;
                }
          }
    }
}

if (menuService.IsAdmin)
{
    menuService.ShowMenuAdmin();
}
else
{
    menuService.ShowMenuUser();
}
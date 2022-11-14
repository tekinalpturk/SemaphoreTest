
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Text;
using System.Diagnostics;

Console.WriteLine("Program Started");

using HttpClient httpClient = new();

int[] arrayToCheck = Enumerable.Range(1, 1000).ToArray();



Console.WriteLine($"Total number of numbers to check = {arrayToCheck.Count()}");
Stopwatch sw = new Stopwatch();
sw.Start();

var semaphore = new SemaphoreSlim(initialCount: 1, maxCount: 1);
var tasks = arrayToCheck.Select(async item =>
{
    await SendApiRequest(item);
});
await Task.WhenAll(tasks);

sw.Stop();
Console.WriteLine($"Time elapsed = {sw.Elapsed}");
Console.WriteLine("Program Finished");
Console.ReadLine();


async Task SendApiRequest(int number)
{
    try
    {
        //Send HTTP request and get the response
        var response = await httpClient.GetAsync($"https://localhost:7009/api/Math/IsPrime?number={number}");
        var responseAsString = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{number} {(bool.Parse(responseAsString) ? "is Prime" : "is not Prime")}"); 
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{number} - {ex.Message}");
    }
}
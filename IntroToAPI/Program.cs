using IntroToAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntroToAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Reference: Highlight HttpClient, ctrl + ., select: using system httpclient.
            HttpClient httpClient = new HttpClient();
            var response = httpClient.GetAsync("http://swapi.dev/api/people/1/").Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Person personResponse = response.Content.ReadAsAsync<Person>().Result;
                Console.WriteLine(personResponse.name);

                foreach(string vehicleURL in personResponse.vehicles)
                {
                    HttpResponseMessage vehicleResponse = httpClient.GetAsync(vehicleURL).Result;
                    Console.WriteLine(vehicleResponse.Content.ReadAsStringAsync().Result);
                    Vehicle vehicle = vehicleResponse.Content.ReadAsAsync<Vehicle>().Result;
                    Console.WriteLine(vehicle.name);
                }
            }

            SWAPIService swapiService = new SWAPIService();

            // Cycles through first 10 Persons on the List: for (int i = 1; i <= 10; i++) { }

            Person personOne = swapiService.GetPersonAsync("http://swapi.dev/api/people/08/").Result; // A new person. Does the same operation as code above.

                if (personOne != null)
                {
                    Console.Clear();
                    Console.WriteLine($"The Character that has been Entered is: {personOne}.");
                    foreach (string vehicleUrl in personOne.vehicles)
                    {
                        var vehicle = swapiService.GetVehicleAsync(vehicleUrl).Result;
                        Console.WriteLine($"They drive a {vehicle.name}");
                    }
                    Console.ReadKey();
                }
            

            var genericResponse = swapiService.GetTAsyncGeneric<Vehicle>("http://swapi.dev/api/vehicles/4/").Result;
            Console.WriteLine(genericResponse.cargo_capacity);
            Console.WriteLine(genericResponse.name);

            SearchResult<Person> skywalkers = swapiService.GetPersonSearchAsync("skywalker").Result;
            foreach(Person person in skywalkers.results)
            {
                Console.WriteLine(person.name);
            }

        }
    }
}

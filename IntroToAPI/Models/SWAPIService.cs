﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntroToAPI.Models
{
    public class SWAPIService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<Person> GetPersonAsync(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if(response.IsSuccessStatusCode)
            {
                Person person = await response.Content.ReadAsAsync<Person>();
                return person;
            }
            return null;
        }

        public async Task<Vehicle> GetVehicleAsync(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Vehicle>(); // Does the same operation as the Method above.
            }
            return null;
        }

        public async Task<T> GetTAsyncGeneric<T>(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }
            return default; // Default needs to be used because of the generic type.
        }

        public async Task<SearchResult<Person>> GetPersonSearchAsync(string query)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://swapi.dev/api/people/?search=" + query);
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<SearchResult<Person>>();
            }
            return null;
        }
    }
}

﻿using LopezAutoSales.Shared.Models;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Threading.Tasks;

namespace LopezAutoSales.Client
{
    public class VINDecoder
    {
        public const string Path = "https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVin/{0}?format=json";
        private readonly HttpClient _client;
        private readonly IJSRuntime _js;

        private string DecodedVIN { get; set; }
        private string JsonData { get; set; }

        public VINDecoder(HttpClient client, IJSRuntime js)
        {
            _client = client;
            _js = js;
        }

        public async Task TryDecodeAsync(Car car)
        {
            if (DecodedVIN != car.VIN)
                JsonData = await _client.GetStringAsync(string.Format(Path, car.VIN));
            else
                await _js.InvokeVoidAsync("blazorAlert", $"{car.VIN} has already been decoded.");
            car.JsonData = JsonData;
            if (!TrySetVariables(car))
                await _js.InvokeVoidAsync("blazorAlert", $"{car.VIN} could not be decoded.");
            else if (DecodedVIN != car.VIN)
                await _js.InvokeVoidAsync("blazorAlert", $"{car.VIN} was successfully decoded.");
            DecodedVIN = car.VIN;
        }

        private bool TrySetVariables(Car car)
        {
            car.DeserializeJson();
            if (car.Data.Results.Find(x => x.VariableId == 143).Value != "0")
                return false;
            car.Year = int.Parse(car.Data.Results.Find(x => x.VariableId == 29).Value);
            car.Make = car.Data.Results.Find(x => x.VariableId == 26).Value;
            car.Model = car.Data.Results.Find(x => x.VariableId == 28).Value;
            return true;
        }
    }
}

﻿using Demo.API.Helpers;

namespace Demo.API.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<TOutput> ConvertResponse<TOutput>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return default;
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            return typeof(TOutput) == typeof(string) ?
                (TOutput)(object)responseContent :
                SerializationHelper.FromJson<TOutput>(responseContent);
        }
    }
}

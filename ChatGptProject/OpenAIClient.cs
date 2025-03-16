using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open_AI_Project
{
    public class OpenAIClient
    {
        private readonly string _apiKey;
        string _systemMessage;
        List<Message> _systemRole;
        public OpenAIClient(string apiKey) {
        this._apiKey = apiKey;
            _systemMessage = "You are a MS SQL DB admin.";
            _systemRole = new List<Message> { new Message {
            role="system",
            content=_systemMessage
            } };
        }

        /// <summary>
        /// This method uses simple javascript code demonstration from open AI sample js code using c# objects, no external libraries for open AI
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> TalkToChatGpt(string prompt)
        {
            var client = new RestClient("https://api.openai.com/v1/chat");
            var request = new RestRequest("completions", Method.Post);
            request.AddHeader("Authorization", $"Bearer {_apiKey}");
            request.AddHeader("Content-Type", "application/json");

            //now adding user prompt which will be asked at runtime.
            var requestBody = new ChatGptModel { 
                messages = new List<Message> { 
                new Message {
                    role = "user",
                    content = prompt 
                }
            }, 
                model = "gpt-4o" 
            };
            requestBody.messages.AddRange(_systemRole);

            request.AddParameter("application/json", JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                dynamic completion = JsonConvert.DeserializeObject(response.Content);
                var responseString = (string)completion.choices[0].message.content;
                return responseString;
            }
            else
            {
                throw new Exception("Unable to get response from open AI.");
            }
        }
    }
}

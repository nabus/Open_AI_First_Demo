using Open_AI_Project;

var aiService = new OpenAIClient("Your_OpenAI_API_Key");

Console.WriteLine("Go ahead and ask to chat gpt:");
string prompt = Console.ReadLine();//your input

string response = await aiService.TalkToChatGpt(prompt);
Console.WriteLine("Look what I found: ");//response from chat gpt
Console.WriteLine(response);
Console.ReadLine();
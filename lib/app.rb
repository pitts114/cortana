require 'dotenv/load'
require 'sinatra'
require 'openai'

get '/' do
  client = OpenAI::Client.new(access_token: ENV['OPENAI_ACCESS_TOKEN'])

  response = client.chat(
    parameters: {
        model: "gpt-3.5-turbo",
        messages: [{ role: "user", content: "Hello!"}]
    })
  response.dig("choices", 0, "message", "content")
end

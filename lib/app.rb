require 'dotenv/load'
require 'sinatra'
require 'openai'

get '/' do
  'Cortana'
end

post '/' do
  client = OpenAI::Client.new(access_token: ENV['OPENAI_ACCESS_TOKEN'])

  request.body.rewind
  request_payload = JSON.parse request.body.read
  prompt = request_payload["prompt"] || raise("Request provide a prompt")

  response = client.chat(
    parameters: {
        model: "gpt-3.5-turbo",
        messages: [{ role: "user", content: prompt}]
    })
  response.dig("choices", 0, "message", "content")
end

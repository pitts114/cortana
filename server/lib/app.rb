require 'dotenv/load'
require 'sinatra'
require 'openai'

get '/' do
  'Cortana'
end

# curl -X POST -H "Content-Type: application/octet-stream" --data-binary @audio.wav http://localhost:4567/
post '/' do
  # read wav from request body
  request.body.rewind
  wav = request.body.read

  # get text from wav
  response = Net::HTTP.post(URI("http://#{ENV['DEEPSPEECH_HOST']}:#{ENV['DEEPSPEECH_PORT']}/stt"), wav)
  prompt = response.body

  puts "prompt: #{prompt}"

  client = OpenAI::Client.new(access_token: ENV['OPENAI_ACCESS_TOKEN'])
  response = client.chat(
    parameters: {
        model: "gpt-3.5-turbo",
        messages: [{ role: "user", content: prompt}]
    })
  content = response.dig("choices", 0, "message", "content")

  puts "response: #{content}"

  response = Net::HTTP.post(URI("http://#{ENV['MIMIC3_HOST']}:#{ENV['MIMIC3_PORT']}/api/tts"), content)
  wav = response.body

  wav
end

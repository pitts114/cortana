# Cortana

## Useful commands

(Mac OS Only) Play audio from response:
`curl -X POST --data 'Hello world.' --output - localhost:59125/api/tts | play -t wav -`
# Cortana
![Cortana from Halo 2](cortana.png)


## Deepspeech
Deepspeech accepts wav files with 16kHz sample rate and 16 bit depth.


## Useful commands

Play audio from response:

Linux
`curl -X POST --data 'Hello world.' --output - localhost:59125/api/tts | aplay`

Mac OS

`curl -X POST --data 'Hello world.' --output - localhost:59125/api/tts | play -t wav -`

FROM romainsah/deepspeech-server:latest

RUN mkdir -p /opt/deepspeech
ADD https://github.com/mozilla/DeepSpeech/releases/download/v0.9.3/deepspeech-0.9.3-models.pbmm /opt/deepspeech/deepspeech-0.9.3-models.pbmm
ADD https://github.com/mozilla/DeepSpeech/releases/download/v0.9.3/deepspeech-0.9.3-models.scorer /opt/deepspeech/deepspeech-0.9.3-models.scorer
ADD deepspeech/config.json /opt/deepspeech/config.json

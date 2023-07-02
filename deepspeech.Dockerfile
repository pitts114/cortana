FROM romainsah/deepspeech-server:latest

ADD https://github.com/mozilla/DeepSpeech/releases/download/v0.9.3/deepspeech-0.9.3-models.pbmm /opt/deepspeech
ADD https://github.com/mozilla/DeepSpeech/releases/download/v0.9.3/deepspeech-0.9.3-models.scorer /opt/deepspeech

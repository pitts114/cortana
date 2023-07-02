FROM mycroftai/mimic3:0.2.4

USER root
RUN mkdir -p "/home/mimic3/.local/share/mycroft/mimic3"
RUN chmod a+rwx "/home/mimic3/.local/share/mycroft/mimic3"
USER mimic3

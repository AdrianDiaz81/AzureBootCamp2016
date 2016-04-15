FROM microsoft/aspnet:1.0.0-rc1-update1

#Adds location of global commands to path so that they can be run (because entry point doesn�t run bash profile to load dnvm)
ENV PATH $PATH:$DNX_USER_HOME/bin
#Required for dnx-watch to detect file changes on the mounted volume
ENV MONO_MANAGED_WATCHER 1
#Set global packages location to the /opt/packages location
ENV DNX_PACKAGES /opt/dnx/packages
#trace to get more output from DNX.
ENV DNX_TRACE 1

#Install DNX-Watch
RUN dnu commands install Microsoft.Dnx.Watcher --packages $DNX_USER_HOME/bin

COPY project.json /app/
WORKDIR /app
RUN ["dnu", "restore"]
COPY . /app




ENTRYPOINT ["/bin/bash", "-c", "dnx-watch web"]
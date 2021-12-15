FROM mcr.microsoft.com/mssql/server

ARG PROJECT_DIR=/tmp/pieshop
RUN mkdir -p $PROJECT_DIR
WORKDIR $PROJECT_DIR
COPY sql/DevDockerDbSetup.sql ./
COPY sql/wait-for-it.sh ./
COPY sql/entrypoint.sh ./
COPY sql/setup.sh ./

CMD ["/bin/bash", "entrypoint.sh"]
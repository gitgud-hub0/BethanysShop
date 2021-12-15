./wait-for-it.sh piedb:1433 --timeout=0 --strict --sleep 5s && \
/opt/mssql-tools/bin/sqlcmd -S localhost -i DevDockerDbSetup.sql -U sa -P "$SA_PASSWORD"
# Useful commands

This document serves as a second memory to help remember useful commands.

## PgSql Commands

### Backup database

`docker exec -e PGPASSWORD=<password> BeersDb_PgSql pg_dump -h localhost -U <username> -F c -b -v -f /var/lib/postgresql/data/beers_db.backup beers_db`

### Restore database

**Create Database**

`docker exec -e PGPASSWORD=<password> NomeDoContainer psql -U <username> -c "CREATE DATABASE beers_db;"`

**Restore data**

No powershell execute:

`docker exec -e PGPASSWORD=<password> NomeDoContainer pg_restore -U <username> -d beers_db -v /var/lib/postgresql/data/beers_db.backup;`

## Postgres
docker run --rm --name my-postgres -p 5432:5432 -e POSTGRES_PASSWORD=postgres postgres:12.2-alpine

psql -h localhost -U postgres --password

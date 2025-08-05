# Crypto.Compare


## Database

File: ./db/init.sql

## Swagger
Swagger: /swagger/index.html

## Local start in Docker

```
docker-compose -f docker-compose.tests.yml down
docker-compose -f docker-compose.tests.yml build api-public 
docker-compose -f docker-compose.tests.yml run --rm start_dependencies
docker-compose -f docker-compose.tests.yml up -d api-public 
```

docker-compose -f docker-compose.tests.yml down
docker-compose -f docker-compose.tests.yml build api-public sql 
docker-compose -f docker-compose.tests.yml run --rm start_dependencies
docker-compose -f docker-compose.tests.yml up -d api-public 

#!/bin/bash
cd /Users/mac/code2026/net-projects/ecommerce/backend/Ecommerce.Api
dotnet run &
PID=$!
sleep 5 # wait for server to start

echo "--- LOGIN ---"
RESPONSE=$(curl -s -X POST http://localhost:5032/api/Auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@gmail.com","password":"Admin@123"}')

echo "Result: $RESPONSE"

TOKEN=$(echo $RESPONSE | grep -o '"accessToken":"[^"]*' | cut -d'"' -f4)
echo "Token: $TOKEN"

echo "--- FETCH PRODUCT ---"
curl -s -v -X GET http://localhost:5032/api/Product \
  -H "Authorization: Bearer $TOKEN"

kill $PID

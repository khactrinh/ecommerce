#!/bin/bash

NAME=$1

echo "📦 Creating migration: $NAME"

dotnet ef migrations add "$NAME" \
  --project backend/Ecommerce.Infrastructure \
  --output-dir Persistence/Migrations

echo "🚀 Updating database..."

dotnet ef database update \
  --project backend/Ecommerce.Infrastructure

echo "✅ Done"
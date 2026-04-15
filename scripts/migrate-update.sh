#!/bin/bash

# 🔥 lấy root project (parent của scripts)
ROOT_DIR="$(cd "$(dirname "$0")/.." && pwd)"
PROJECT="$ROOT_DIR/backend/Ecommerce.Infrastructure"
STARTUP_PROJECT="$ROOT_DIR/backend/Ecommerce.Api"

echo "🚀 Updating database..."

dotnet ef database update \
  --project "$PROJECT" \
  --startup-project "$STARTUP_PROJECT" \
  --verbose

echo "✅ Done"
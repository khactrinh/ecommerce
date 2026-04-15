#!/bin/bash  

set -e  

# 🔥 lấy root project (parent của scripts)
ROOT_DIR="$(cd "$(dirname "$0")/.." && pwd)"

PROJECT="$ROOT_DIR/backend/Ecommerce.Infrastructure"
STARTUP_PROJECT="$ROOT_DIR/backend/Ecommerce.Api"
MIGRATION_DIR="$PROJECT/Persistence/Migrations"

GREEN='\033[0;32m'  
RED='\033[0;31m'  
NC='\033[0m'  

if [ -z "$1" ]; then  
  echo -e "${RED}❌ Migration name is required${NC}"  
  echo "👉 Usage: ./scripts/migrate-add.sh <MigrationName>"
  echo "👉 Example: ./scripts/migrate-add.sh AddProductTable"  
  exit 1  
fi  

NAME=$1  

echo -e "${GREEN}📦 Creating migration:${NC} $NAME"  

dotnet ef migrations add "$NAME" \
  --project "$PROJECT" \
  --startup-project "$STARTUP_PROJECT" \
  --output-dir "$MIGRATION_DIR" \
  --verbose

echo -e "${GREEN}✅ Migration created. EF will handle timestamp automatically.${NC}"
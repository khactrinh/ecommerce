#!/bin/bash

echo "🔥 Dropping database..."
dotnet ef database drop -f

echo "🚀 Updating database..."
dotnet ef database update

echo "✅ Done"
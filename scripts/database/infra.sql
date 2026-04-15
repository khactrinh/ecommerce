CREATE SCHEMA IF NOT EXISTS messaging;

CREATE TABLE messaging.processed_messages (
    id UUID PRIMARY KEY,
    processed_at TIMESTAMP NOT NULL DEFAULT now()
);

CREATE TABLE IF NOT EXISTS messaging.inbox_messages (
    id UUID PRIMARY KEY,
    type TEXT NOT NULL,
    content JSONB NOT NULL,
    occurred_on TIMESTAMP NOT NULL,
    processed_on TIMESTAMP NULL,
    retry_count INT DEFAULT 0,
    error TEXT NULL
);
-- =========================
-- Roles
-- =========================
INSERT INTO roles (id, name)
VALUES
    ('11111111-1111-1111-1111-111111111111', 'Admin'),
    ('22222222-2222-2222-2222-222222222222', 'Customer')
    ON CONFLICT (id) DO NOTHING;
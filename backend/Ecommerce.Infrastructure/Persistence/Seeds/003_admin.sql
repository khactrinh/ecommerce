-- =========================
-- Admin user
-- password: 123456
-- =========================

INSERT INTO users (id, email, password_hash)
VALUES (
           'aaaaaaaa-1111-1111-1111-111111111111',
           'admin@gmail.com',
           '$2a$11$w7Q9ZqKQ0xYQ9Q1x6G5lUe0FzYw0Zqk1W6vKqz6X1VQY7QF7s9u7G' -- bcrypt(123456)
       )
    ON CONFLICT (email) DO NOTHING;

-- assign role Admin
INSERT INTO user_roles (user_id, role_id)
VALUES (
           'aaaaaaaa-1111-1111-1111-111111111111',
           '11111111-1111-1111-1111-111111111111'
       )
    ON CONFLICT DO NOTHING;
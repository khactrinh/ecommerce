-- =========================
-- Categories
-- =========================

-- Parent
INSERT INTO categories (id, name, description)
VALUES
    ('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', 'Electronics', 'Devices and gadgets'),
    ('bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb', 'Fashion', 'Clothing and style'),
    ('cccccccc-cccc-cccc-cccc-cccccccccccc', 'Home & Living', 'Home products')
    ON CONFLICT (id) DO NOTHING;

-- Children Electronics
INSERT INTO categories (id, name, description, parent_id)
VALUES
    ('dddddddd-dddd-dddd-dddd-dddddddddddd', 'Laptop', 'All kinds of laptops', 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa'),
    ('eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee', 'Phone', 'Smartphones', 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa'),
    ('ffffffff-ffff-ffff-ffff-ffffffffffff', 'Accessories', 'Tech accessories', 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa')
    ON CONFLICT (id) DO NOTHING;

-- Children Fashion
INSERT INTO categories (id, name, description, parent_id)
VALUES
    ('99999999-9999-9999-9999-999999999999', 'Men', 'Men fashion', 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb'),
    ('88888888-8888-8888-8888-888888888888', 'Women', 'Women fashion', 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb')
    ON CONFLICT (id) DO NOTHING;
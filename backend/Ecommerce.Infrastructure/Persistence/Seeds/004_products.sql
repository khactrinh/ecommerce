-- =========================
-- Products
-- =========================

-- Fixed products
INSERT INTO products (id, name, price, stock, image_url, category_id, created_at)
VALUES
    (gen_random_uuid(), 'MacBook Pro M3', 2500, 10, 'https://picsum.photos/300?1', 'dddddddd-dddd-dddd-dddd-dddddddddddd', now()),
    (gen_random_uuid(), 'Dell XPS 13', 1800, 8, 'https://picsum.photos/300?2', 'dddddddd-dddd-dddd-dddd-dddddddddddd', now()),
    (gen_random_uuid(), 'iPhone 15 Pro', 1200, 15, 'https://picsum.photos/300?3', 'eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee', now()),
    (gen_random_uuid(), 'Samsung Galaxy S24', 1100, 20, 'https://picsum.photos/300?4', 'eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee', now()),
    (gen_random_uuid(), 'Men T-Shirt Basic', 25, 100, 'https://picsum.photos/300?5', '99999999-9999-9999-9999-999999999999', now());

-- Random products (30 items)
INSERT INTO products (id, name, price, stock, image_url, category_id, created_at)
SELECT
    gen_random_uuid(),
    'Laptop Random ' || i,
    (random() * 2500 + 500)::int,
    (random() * 50 + 1)::int,
    'https://picsum.photos/300?random=' || i,
    'dddddddd-dddd-dddd-dddd-dddddddddddd',
    now()
FROM generate_series(1, 30) AS s(i);
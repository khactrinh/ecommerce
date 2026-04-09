export async function getProducts(page = 1, pageSize = 12) {
  const res = await fetch(
    `http://localhost:5001/api/Product?page=${page}&pageSize=${pageSize}`,
    {
      cache: "no-store", // tránh cache khi dev
    },
  );

  if (!res.ok) {
    throw new Error("Failed to fetch products");
  }

  return res.json();
}

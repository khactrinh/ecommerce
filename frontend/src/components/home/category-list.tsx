export default function CategoryList() {
  const categories = ["Laptop", "Phone", "Tablet", "Accessories"];

  return (
    <div className="grid grid-cols-4 md:grid-cols-8 gap-3">
      {categories.map((c) => (
        <div
          key={c}
          className="bg-white p-3 rounded-lg text-center shadow-sm hover:shadow transition"
        >
          {c}
        </div>
      ))}
    </div>
  );
}

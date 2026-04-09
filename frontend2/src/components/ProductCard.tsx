type Props = {
  product: {
    name: string;
    price: number;
    image: string;
  };
};

export default function ProductCard({ product }: Props) {
  return (
    <div className="bg-white rounded-xl shadow-sm hover:shadow-md transition p-3">
      {/* Image */}
      <div className="relative overflow-hidden rounded-lg">
        <img
          src={product.image}
          className="h-48 w-full object-cover hover:scale-105 transition"
        />

        {/* Discount */}
        <span className="absolute top-2 left-2 bg-red-500 text-white text-xs px-2 py-1 rounded">
          -20%
        </span>
      </div>

      {/* Name */}
      <h3 className="mt-3 font-medium line-clamp-2">{product.name}</h3>

      {/* Rating */}
      <div className="text-yellow-500 text-sm mt-1">
        ⭐⭐⭐⭐☆ <span className="text-gray-500">(120)</span>
      </div>

      {/* Price */}
      <div className="mt-2">
        <span className="text-red-500 font-bold text-lg">
          {product.price.toLocaleString()}đ
        </span>
        <span className="text-gray-400 line-through text-sm ml-2">
          {(product.price * 1.2).toLocaleString()}đ
        </span>
      </div>

      {/* CTA */}
      <button className="mt-3 w-full bg-black text-white py-2 rounded-lg hover:bg-gray-800">
        Add to cart
      </button>
    </div>
  );
}

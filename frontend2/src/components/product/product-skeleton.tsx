export default function ProductSkeleton() {
  return (
    <div className="grid grid-cols-4 gap-4">
      {Array.from({ length: 8 }).map((_, i) => (
        <div key={i} className="h-40 bg-gray-200 animate-pulse rounded" />
      ))}
    </div>
  );
}

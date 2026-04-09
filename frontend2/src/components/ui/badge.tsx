export default function Badge({ children }: { children: React.ReactNode }) {
  return (
    <span className="text-xs bg-red-100 text-red-500 px-2 py-1 rounded">
      {children}
    </span>
  );
}

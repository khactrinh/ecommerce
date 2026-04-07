export default function Container({ children }: { children: React.ReactNode }) {
  return (
    <div className="max-w-7xl mx-auto p-6">
      <div className="bg-white p-6 rounded-xl shadow-sm">{children}</div>
    </div>
  );
}

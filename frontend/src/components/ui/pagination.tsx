type Props = {
  current: number;
  totalPages: number;
  onChange: (page: number) => void;
};

export default function Pagination({ current, totalPages, onChange }: Props) {
  if (totalPages <= 1) return null;

  return (
    <div className="flex gap-2">
      {Array.from({ length: totalPages }).map((_, i) => {
        const page = i + 1;

        return (
          <button
            key={page}
            onClick={() => onChange(page)}
            className={`px-3 py-1 border rounded ${
              page === current ? "bg-black text-white" : ""
            }`}
          >
            {page}
          </button>
        );
      })}
    </div>
  );
}

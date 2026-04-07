import clsx from "clsx";

type Props = React.ButtonHTMLAttributes<HTMLButtonElement> & {
  variant?: "primary" | "outline" | "ghost";
};

export default function Button({
  variant = "primary",
  className,
  ...props
}: Props) {
  return (
    <button
      className={clsx(
        "px-4 py-2 rounded-lg text-sm font-medium transition",
        {
          "bg-primary text-white hover:opacity-90": variant === "primary",
          "border hover:bg-gray-100": variant === "outline",
          "hover:bg-gray-100": variant === "ghost",
        },
        className,
      )}
      {...props}
    />
  );
}

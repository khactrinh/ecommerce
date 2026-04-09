import Header from "@/components/layout/header";
import Footer from "@/components/layout/footer";

export default function ShopLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <>
      <Header />
      <main className="bg-gray-50 min-h-screen">{children}</main>
      <Footer />
    </>
  );
}

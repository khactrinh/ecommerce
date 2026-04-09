import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  /* config options here */
  images: {
    domains: ["picsum.photos"], // 👈 thêm dòng này
  },
};

export default nextConfig;

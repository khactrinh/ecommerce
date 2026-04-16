"use client";

import React, { createContext, useContext, useEffect, useState } from "react";
import { api } from "@/lib/api";
import { LoginResponse } from "@/lib/types";

interface User {
  id: string;
  email: string;
  role: string;
}

interface AuthContextType {
  user: User | null;
  token: string | null;
  login: (email: string, password: string) => Promise<void>;
  logout: () => void;
  isLoading: boolean;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [user, setUser] = useState<User | null>(null);
  const [token, setToken] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const savedToken = localStorage.getItem("auth_token");
    if (savedToken) {
      setToken(savedToken);
      // In a real app, you might want to fetch user profile here
      // decode token to get user info for now
      try {
        const payload = JSON.parse(atob(savedToken.split(".")[1]));
        setUser({
          id: payload.sub,
          email: payload.email,
          role: payload[
            "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
          ],
        });
      } catch (e) {
        localStorage.removeItem("auth_token");
      }
    }
    setIsLoading(false);
  }, []);

  const login = async (email: string, password: string) => {
    const response = await api.post<LoginResponse>("/api/v1/auth/login", {
      email,
      password,
      ipAddress: "string",
    });

    const jwt = response.accessToken;
    localStorage.setItem("auth_token", jwt);
    localStorage.setItem("refresh_token", response.refreshToken);
    setToken(jwt);

    const decodeJwt = (token: string) => {
      try {
        return JSON.parse(atob(token.split(".")[1]));
      } catch {
        return null;
      }
    };

    const payload = decodeJwt(jwt);

    if (payload) {
      setUser({
        id: payload.sub,
        email: payload.email,
        role: payload[
          "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        ],
      });
    }
  };

  const logout = () => {
    localStorage.removeItem("auth_token");
    setUser(null);
    setToken(null);
  };

  return (
    <AuthContext.Provider value={{ user, token, login, logout, isLoading }}>
      {children}
    </AuthContext.Provider>
  );
}

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};

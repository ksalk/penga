'use client'

import { useAuth } from "../auth";

const API_BASE_URL = process.env.NEXT_PUBLIC_PENGA_API_BASE_URL;

export const useUsersApi = () => {
    const auth = useAuth();

    const headers = () => ({
      "Content-Type": "application/json",
      Authorization: `Bearer ${auth.token}`,
    });

    const saveUserInfo = async () => {
        try {
            const response = await fetch(`${API_BASE_URL}/user`, {
            method: 'POST',
            headers: headers()
          });
      
          if (!response.ok) {
            throw new Error('Failed to post data');
          }
      
          return await response.json();
        } catch (error) {
          console.error("Error posting data:", error);
          throw error;
        }
      };

    return { saveUserInfo };
}

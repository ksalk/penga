'use client'

import { useAuth } from "./auth";
import { useUsersApi } from "./api/userApi";
import { useAuth } from "./auth";

export default function Home() {
  const { authData, login, logout } = useAuth();
  const usersApi = useUsersApi();

  return (
    <div>
      {!authData && (
        <div>
          <button className="px-4 py-2 bg-blue-500 text-white font-semibold rounded-md" onClick={login}>Login</button>
        </div>
        )
      }
      {authData && (
        <div>
          <div>Welcome</div>
          <div>{ authData.username }</div>
          <div>
            <button className="px-4 py-2 bg-blue-500 text-white font-semibold rounded-md" onClick={logout}>Logout</button>
          </div>
        </div>
        )
      }
    </div>
  );
}

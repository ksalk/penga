'use client'

import { useAuth } from "./auth";
import { useUsersApi } from "./api/userApi";
import { useState, useEffect } from "react";

export default function Home() {
  const auth = useAuth();
  const usersApi = useUsersApi();
  const [account, setAccount] = useState();

  useEffect(() => {
    const fetchUserInfo = async () => {
      await auth.msalInstance.initialize();
      const myAccounts = auth.msalInstance.getAllAccounts();
      setAccount(myAccounts[0]);
    }
    
    fetchUserInfo();
  }, []);

  async function login() {
    await auth.msalInstance.loginPopup();

    const myAccounts = auth.msalInstance.getAllAccounts();
    auth.account = myAccounts[0];

    setAccount(myAccounts[0]);
    const response = await auth.msalInstance.acquireTokenSilent({
      account: auth.account,
      scopes: ['api://' + process.env.NEXT_PUBLIC_ENTRA_ID_CLIENT_ID + '/access_api']
    });

    auth.token = response.accessToken;
    console.log(auth.account);
    console.log(response);
    await usersApi.saveUserInfo();
  }

  async function logout() {
    await auth.msalInstance.logoutPopup({
      account: account,
      postLogoutRedirectUri: window.location.href,
      mainWindowRedirectUri: window.location.href
    });
    console.log("Logged out...");
  }

  return (
    <div>
      {!account && (
        <div>
          <button className="px-4 py-2 bg-blue-500 text-white font-semibold rounded-md" onClick={login}>Login</button>
        </div>
        )
      }
      {account && (
        <div>
          <div>Welcome</div>
          <div>{ account.name }</div>
          <div>
            <button className="px-4 py-2 bg-blue-500 text-white font-semibold rounded-md" onClick={logout}>Logout</button>
          </div>
        </div>
        )
      }
    </div>
  );
}

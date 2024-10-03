'use client'

import React, { useState, useEffect } from 'react';

export default function Home() {
  const [userInfo, setUserInfo] = useState();

  async function getUserInfo() {
    const response = await fetch('/.auth/me');
    const payload = await response.json();
    const { clientPrincipal } = payload;
    return clientPrincipal;
  }

  useEffect(() => {
    const fetchUserInfo = async () => {
      setUserInfo(await getUserInfo())
    };

    fetchUserInfo();
  }, []);

  return (
    <div>
      {!userInfo && (
        <div>
          <a href="/.auth/login/aad"><button className="px-4 py-2 bg-blue-500 text-white font-semibold rounded-md">Login</button></a>
        </div>
      )
      }
      {userInfo && (
        <div>
          <div>Welcome</div>
          <div>{userInfo && userInfo.userDetails}</div>
          <div>
            <a href="/.auth/logout"><button className="px-4 py-2 bg-blue-500 text-white font-semibold rounded-md">Logout</button></a>
          </div>
        </div>
      )
      }
    </div>

  );
}

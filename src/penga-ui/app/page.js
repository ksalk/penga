'use client'

import React, { useState } from 'react';

export default function Home() {
  const [ userInfo, setUserInfo ] = useState();

  async function getUserInfo() {
    const response = await fetch('/.auth/me');
    const payload = await response.json();
    const { clientPrincipal } = payload;
    return clientPrincipal;
  }
  
  (async () => {
    setUserInfo(await getUserInfo());
  })();

  return (
    <div>
    <a href="/.auth/login/aad"><button>Login</button></a>
        { userInfo && (
            <div>
              <div>Welcome</div>
              <div>{ userInfo && userInfo.userDetails}</div>
            </div>
          )
        }
    </div>
    
  );
}

'use client'

import React, { useState, useEffect } from 'react';

export default function Home() {
  const [ userInfo, setUserInfo ] = useState();

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
    <a href="/.auth/login/aad"><button>Login</button></a>
    <a href="/login"><button>Login 2</button></a>
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

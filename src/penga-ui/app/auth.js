'use client'

import { useState, useEffect } from "react";

const { PublicClientApplication } = require("@azure/msal-browser")

const LOCAL_STORAGE_ACCESS_TOKEN_KEY = "penga-access-token";

const config = {
    auth: {
        clientId: process.env.NEXT_PUBLIC_ENTRA_ID_CLIENT_ID,
        authority: "https://login.microsoftonline.com/" + process.env.NEXT_PUBLIC_ENTRA_ID_TENANT_ID
    },
    cache: {
        cacheLocation: "localStorage",
        storeAuthStateInCookie: false
    }
};

// For now I'm just storing access token in localStorage
// If I find better approach I'll change it in the future
export const useAuth2 = () => {
    const [authData, setAuthData] = useState(null);

    const msalInstance = new PublicClientApplication(config);
    msalInstance.initialize();

    useEffect(() => {
        if(!authData) {
            setAuthData(JSON.parse(localStorage.getItem(LOCAL_STORAGE_ACCESS_TOKEN_KEY)));
        }
    }, []);

    const login = async () => {
        await msalInstance.loginPopup();

        const myAccounts = msalInstance.getAllAccounts();
    
        const response = await msalInstance.acquireTokenSilent({
          account: myAccounts[0],
          scopes: ['api://' + process.env.NEXT_PUBLIC_ENTRA_ID_CLIENT_ID + '/access_api']
        });
    
        const loggedInAuthData = {
            accessToken: response.accessToken,
            toketExpiresAt: response.expiresOn,
            username: response.account.username
        };
        setAuthData(loggedInAuthData);
        localStorage.setItem(LOCAL_STORAGE_ACCESS_TOKEN_KEY, JSON.stringify(loggedInAuthData));
    };
  
    const logout = async () => {
        await msalInstance.logoutPopup();
        setAuthData(null);
        localStorage.removeItem(LOCAL_STORAGE_ACCESS_TOKEN_KEY);
    };
  
    return { authData, login, logout };
  };
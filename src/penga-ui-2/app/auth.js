const { PublicClientApplication } = require("@azure/msal-browser")

const config = {
    auth: {
        clientId: process.env.NEXT_PUBLIC_ENTRA_ID_CLIENT_ID,
        authority: "https://login.microsoftonline.com/" + process.env.NEXT_PUBLIC_ENTRA_ID_TENANT_ID
    }
};

const data = {
    account: null,
    msalInstance: new PublicClientApplication(config),
    token: ""
};

export function useAuth() {
    return data;
}
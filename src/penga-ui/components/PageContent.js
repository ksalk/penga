"use client"

import { useAuth } from "@/app/auth";

export function PageContent({ children }) {
    const { authData, login, logout } = useAuth();

    return <>
            { !authData && <div className="p-5">Please login to use Penga.</div> }
            { authData && <> { children } </> }
        </>
}
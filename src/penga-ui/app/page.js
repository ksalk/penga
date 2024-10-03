'use client'

export default function Home() {
  async function getUserInfo() {
    const response = await fetch('http://localhost:4280/.auth/me');
    const payload = await response.json();
    const { clientPrincipal } = payload;
    return clientPrincipal;
  }
  
  (async () => {
    console.log(await getUserInfo());
  })();

  return (
    <a href="/.auth/login/aad"><button>Login</button></a>
  );
}

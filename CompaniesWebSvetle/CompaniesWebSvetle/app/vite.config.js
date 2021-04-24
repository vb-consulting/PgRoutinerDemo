import { defineConfig } from 'vite'
import svelte from '@sveltejs/vite-plugin-svelte'

// https://vitejs.dev/config/

const getConfig = define => new Object({
    plugins: [svelte()],
    build: {
        outDir: '../wwwroot',
        emptyOutDir: true
    },
    server: {
        host: "localhost",
        port: 3001,
        strictPort: true,
        https: false,
        open: true
    },
    define: define
});

export default ({ command, mode }) => {
    if (command === "serve" && mode === "development") {
        return defineConfig(getConfig({_BASE_URL: JSON.stringify("https://localhost:5001/")}));
    } else {
        return defineConfig(getConfig());
    }
}
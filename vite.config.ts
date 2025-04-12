import { defineConfig, Plugin } from 'vite';
import solidPlugin from 'vite-plugin-solid';
import { dirname, resolve } from 'path';
import tailwindcss from '@tailwindcss/vite';
import { fileURLToPath } from 'url';

export default defineConfig({
  plugins: [tailwindcss(), solidPlugin()],
  server: {
    port: 3000,
    hmr: {
      port: 3000
    }
  },
  base: '/frontend',
  build: {
    target: 'esnext',
    lib: {
      name: "AccountingFrontend",
      entry: resolve(__dirname, './frontend/index.tsx'),
      formats: ['es', "umd"],
      fileName: "index"
    }
  },
  resolve: {
    alias: [
      {
        find: '~',
        replacement: resolve(__dirname, './src')
      },
      {
        find: '@frontend',
        replacement: resolve(__dirname, './frontend')
      },
      {
        find: '@web',
        replacement: resolve(__dirname, './Accounting.Web')
      }
    ]
  }
});
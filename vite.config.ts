import { defineConfig, Plugin } from 'vite';
import solidPlugin from 'vite-plugin-solid';
import { resolve } from 'path';
import tailwindcss from '@tailwindcss/vite';
import MagicString from "magic-string"

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
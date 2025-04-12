import { defineConfig, Plugin } from 'vite';
import solidPlugin from 'vite-plugin-solid';
import { dirname, resolve } from 'path';
import tailwindcss from '@tailwindcss/vite';
import { fileURLToPath } from 'url';

const __filename= fileURLToPath(import.meta.url);
const __dirname = dirname(__filename);

// console.log('__dirname is', __dirname);
// const entryPath = resolve(__dirname, './frontend/index.tsx');
// console.log('entryPath is', entryPath);

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
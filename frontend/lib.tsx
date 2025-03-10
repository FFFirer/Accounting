import { render } from "solid-js/web";
import App from "./App";

export const renderApp = (el: HTMLElement) => render(() => <App />, el)
import type { Component } from "solid-js";

import logo from "./logo.svg";
import styles from "./App.module.css";
import { Route, Router } from "@solidjs/router";
import { routes } from "./routes";

const App: Component = () => {
  return <Router>{routes}</Router>;
};

export default App;

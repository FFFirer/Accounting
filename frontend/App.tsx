import { Router } from "@solidjs/router";
import { Routes } from "./routes";
import { DialogContainer } from "./components/Dialog";

export default () => (
  <>
    <Router base="/app" children={Routes} />
  </>
);

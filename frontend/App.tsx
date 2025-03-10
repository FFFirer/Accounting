import { Router } from "@solidjs/router";
import { Routes } from "./routes";

export default () => <Router base="/app" children={Routes} />
import { Route, Router } from "@solidjs/router";
import { TaskList } from "./List";

export const Routes = () => <Router base={"/Tasks"}>
    <Route path={'/'} component={TaskList}></Route>
</Router>
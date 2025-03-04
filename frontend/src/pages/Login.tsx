import { RouteDefinition } from "@solidjs/router";
import { Component } from "solid-js";

export const Login : Component = () => {
    return <div>Login</div>
}

export const LoginRoute : RouteDefinition = {
    path: '/login',
    component: Login
}
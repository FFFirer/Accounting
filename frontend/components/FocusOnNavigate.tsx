import { useLocation } from "@solidjs/router"
import { onMount, ParentProps } from "solid-js";

export interface FocusOnNavigateProps {
    selector: string
}

export default (props: ParentProps<FocusOnNavigateProps>) => {
    const location = useLocation();

    onMount(() => {
        const focusElement = document.querySelector(props.selector) 

        if(focusElement){
            focusElement.focus();
        }
    })

    const unsubscribe = location.
}
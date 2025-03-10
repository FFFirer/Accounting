import { A, AnchorProps } from "@solidjs/router"

export default (props: AnchorProps) => {
    return <A {...props} data-enhance-nav="false" />
}
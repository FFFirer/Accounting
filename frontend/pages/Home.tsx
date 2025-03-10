import NavLink from "@frontend/components/NavLink"

export default () => {
    return <div>
        <a href="/">Home</a>

        <button class="btn btn-primary">Click!</button>
        <ul>
            <li>
                <NavLink href="/about" class="btn btn-link">About</NavLink>
            </li>
        </ul>
    </div>
}
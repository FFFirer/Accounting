import { useFormContext } from "solid-form-context";
import { JSX } from "solid-js";

export default (props: JSX.ButtonHTMLAttributes<HTMLButtonElement>) => {
  const form = useFormContext();

  const submit = () => form?.submit();

  return <button {...props} onClick={submit} />;
};

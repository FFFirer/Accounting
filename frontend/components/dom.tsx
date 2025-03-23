export const getStyle = (el?: Element & { style: any }, name?: string) => {
  if (!el || !name) return null;
  try {
    const computed = document.defaultView?.getComputedStyle(el, "");
    return el.style?.[name] ?? computed?.[name as any] ?? null;
  } catch (error) {
    el.style?.[name] ?? null;
  }
};

export const hasClass = (el?: Element, cls?: string) => {
  if (!el || !cls) return false;
  if (cls.indexOf(" ") !== -1)
    throw new Error("className should not contain space.");
  if (el.classList) {
    return el.classList.contains(cls);
  } else {
    return (" " + el.className + " ").indexOf(" " + cls + " ") > -1;
  }
};

export const addClass = (el?: Element, cls?: string) => {
  if (!el) return;
  var curClass = el.className;
  var classes = (cls || "").split(" ");

  for (var i = 0, j = classes.length; i < j; i++) {
    var clsName = classes[i];
    if (!clsName) continue;

    if (el.classList) {
      el.classList.add(clsName);
    } else if (!hasClass(el, clsName)) {
      curClass += " " + clsName;
    }
  }
  if (!el.classList) {
    el.setAttribute("class", curClass);
  }
};

const trim = (string?: string) => {
  return (string || "").replace(/^[\s\uFEFF]+|[\s\uFEFF]+$/g, "");
};

export const removeClass = (el?: Element, cls?: string) => {
  if (!el || !cls) return;
  var classes = cls.split(" ");
  var curClass = " " + el.className + " ";

  for (var i = 0, j = classes.length; i < j; i++) {
    var clsName = classes[i];
    if (!clsName) continue;

    if (el.classList) {
      el.classList.remove(clsName);
    } else if (hasClass(el, clsName)) {
      curClass = curClass.replace(" " + clsName + " ", " ");
    }
  }
  if (!el.classList) {
    el.setAttribute("class", trim(curClass));
  }
};

window.blazorInterop = {
  setCookie: function (name, value, days) {
    var expires = "";
    if (days) {
      var date = new Date();
      date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
      expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
  },
  readCookie: function (name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
      var c = ca[i];
      while (c.charAt(0) == ' ') c = c.substring(1, c.length);
      if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
  },
  autoBlur: () => {
    debugger
    const activeEl = document.activeElement;
    activeEl?.blur();
  }
};


const toggleTheme = (e) => {
  const checked = e.checked;
  const theme = e.value

  window.blazorInterop.setCookie("theme", checked ? theme : '');
}

const clearValue = (el) => {
  el.value = undefined;
}
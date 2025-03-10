const st = (e, t) => e === t, G = Symbol("solid-proxy"), Oe = typeof Proxy == "function", J = {
  equals: st
};
let ot = Be;
const I = 1, Y = 2, Re = {
  owned: null,
  cleanups: null,
  context: null,
  owner: null
};
var y = null;
let ne = null, it = null, p = null, E = null, j = null, Z = 0;
function $e(e, t) {
  const n = p, r = y, s = e.length === 0, o = t === void 0 ? r : t, l = s ? Re : {
    owned: null,
    cleanups: null,
    context: o ? o.context : null,
    owner: o
  }, i = s ? e : () => e(() => O(() => K(l)));
  y = l, p = null;
  try {
    return $(i, !0);
  } finally {
    p = n, y = r;
  }
}
function B(e, t) {
  t = t ? Object.assign({}, J, t) : J;
  const n = {
    value: e,
    observers: null,
    observerSlots: null,
    comparator: t.equals || void 0
  }, r = (s) => (typeof s == "function" && (s = s(n.value)), Ue(n, s));
  return [Ie.bind(n), r];
}
function R(e, t, n) {
  const r = De(e, t, !1, I);
  z(r);
}
function P(e, t, n) {
  n = n ? Object.assign({}, J, n) : J;
  const r = De(e, t, !0, 0);
  return r.observers = null, r.observerSlots = null, r.comparator = n.equals || void 0, z(r), Ie.bind(r);
}
function lt(e) {
  return $(e, !1);
}
function O(e) {
  if (p === null) return e();
  const t = p;
  p = null;
  try {
    return e();
  } finally {
    p = t;
  }
}
function he(e, t, n) {
  const r = Array.isArray(e);
  let s, o = n && n.defer;
  return (l) => {
    let i;
    if (r) {
      i = Array(e.length);
      for (let c = 0; c < e.length; c++) i[c] = e[c]();
    } else i = e();
    if (o)
      return o = !1, l;
    const a = O(() => t(i, s, l));
    return s = i, a;
  };
}
function Te(e) {
  return y === null || (y.cleanups === null ? y.cleanups = [e] : y.cleanups.push(e)), e;
}
function Ne() {
  return y;
}
function _e(e, t) {
  const n = y, r = p;
  y = e, p = null;
  try {
    return $(t, !0);
  } catch (s) {
    ge(s);
  } finally {
    y = n, p = r;
  }
}
function at(e) {
  const t = p, n = y;
  return Promise.resolve().then(() => {
    p = t, y = n;
    let r;
    return $(e, !1), p = y = null, r ? r.done : void 0;
  });
}
const [An, vn] = /* @__PURE__ */ B(!1);
function je(e, t) {
  const n = Symbol("context");
  return {
    id: n,
    Provider: ht(n),
    defaultValue: e
  };
}
function de(e) {
  let t;
  return y && y.context && (t = y.context[e.id]) !== void 0 ? t : e.defaultValue;
}
function ke(e) {
  const t = P(e), n = P(() => ie(t()));
  return n.toArray = () => {
    const r = n();
    return Array.isArray(r) ? r : r != null ? [r] : [];
  }, n;
}
function Ie() {
  if (this.sources && this.state)
    if (this.state === I) z(this);
    else {
      const e = E;
      E = null, $(() => Q(this), !1), E = e;
    }
  if (p) {
    const e = this.observers ? this.observers.length : 0;
    p.sources ? (p.sources.push(this), p.sourceSlots.push(e)) : (p.sources = [this], p.sourceSlots = [e]), this.observers ? (this.observers.push(p), this.observerSlots.push(p.sources.length - 1)) : (this.observers = [p], this.observerSlots = [p.sources.length - 1]);
  }
  return this.value;
}
function Ue(e, t, n) {
  let r = e.value;
  return (!e.comparator || !e.comparator(r, t)) && (e.value = t, e.observers && e.observers.length && $(() => {
    for (let s = 0; s < e.observers.length; s += 1) {
      const o = e.observers[s], l = ne && ne.running;
      l && ne.disposed.has(o), (l ? !o.tState : !o.state) && (o.pure ? E.push(o) : j.push(o), o.observers && Me(o)), l || (o.state = I);
    }
    if (E.length > 1e6)
      throw E = [], new Error();
  }, !1)), t;
}
function z(e) {
  if (!e.fn) return;
  K(e);
  const t = Z;
  ct(
    e,
    e.value,
    t
  );
}
function ct(e, t, n) {
  let r;
  const s = y, o = p;
  p = y = e;
  try {
    r = e.fn(t);
  } catch (l) {
    return e.pure && (e.state = I, e.owned && e.owned.forEach(K), e.owned = null), e.updatedAt = n + 1, ge(l);
  } finally {
    p = o, y = s;
  }
  (!e.updatedAt || e.updatedAt <= n) && (e.updatedAt != null && "observers" in e ? Ue(e, r) : e.value = r, e.updatedAt = n);
}
function De(e, t, n, r = I, s) {
  const o = {
    fn: e,
    state: r,
    updatedAt: null,
    owned: null,
    sources: null,
    sourceSlots: null,
    cleanups: null,
    value: t,
    owner: y,
    context: y ? y.context : null,
    pure: n
  };
  return y === null || y !== Re && (y.owned ? y.owned.push(o) : y.owned = [o]), o;
}
function Fe(e) {
  if (e.state === 0) return;
  if (e.state === Y) return Q(e);
  if (e.suspense && O(e.suspense.inFallback)) return e.suspense.effects.push(e);
  const t = [e];
  for (; (e = e.owner) && (!e.updatedAt || e.updatedAt < Z); )
    e.state && t.push(e);
  for (let n = t.length - 1; n >= 0; n--)
    if (e = t[n], e.state === I)
      z(e);
    else if (e.state === Y) {
      const r = E;
      E = null, $(() => Q(e, t[0]), !1), E = r;
    }
}
function $(e, t) {
  if (E) return e();
  let n = !1;
  t || (E = []), j ? n = !0 : j = [], Z++;
  try {
    const r = e();
    return ut(n), r;
  } catch (r) {
    n || (j = null), E = null, ge(r);
  }
}
function ut(e) {
  if (E && (Be(E), E = null), e) return;
  const t = j;
  j = null, t.length && $(() => ot(t), !1);
}
function Be(e) {
  for (let t = 0; t < e.length; t++) Fe(e[t]);
}
function Q(e, t) {
  e.state = 0;
  for (let n = 0; n < e.sources.length; n += 1) {
    const r = e.sources[n];
    if (r.sources) {
      const s = r.state;
      s === I ? r !== t && (!r.updatedAt || r.updatedAt < Z) && Fe(r) : s === Y && Q(r, t);
    }
  }
}
function Me(e) {
  for (let t = 0; t < e.observers.length; t += 1) {
    const n = e.observers[t];
    n.state || (n.state = Y, n.pure ? E.push(n) : j.push(n), n.observers && Me(n));
  }
}
function K(e) {
  let t;
  if (e.sources)
    for (; e.sources.length; ) {
      const n = e.sources.pop(), r = e.sourceSlots.pop(), s = n.observers;
      if (s && s.length) {
        const o = s.pop(), l = n.observerSlots.pop();
        r < s.length && (o.sourceSlots[l] = r, s[r] = o, n.observerSlots[r] = l);
      }
    }
  if (e.tOwned) {
    for (t = e.tOwned.length - 1; t >= 0; t--) K(e.tOwned[t]);
    delete e.tOwned;
  }
  if (e.owned) {
    for (t = e.owned.length - 1; t >= 0; t--) K(e.owned[t]);
    e.owned = null;
  }
  if (e.cleanups) {
    for (t = e.cleanups.length - 1; t >= 0; t--) e.cleanups[t]();
    e.cleanups = null;
  }
  e.state = 0;
}
function ft(e) {
  return e instanceof Error ? e : new Error(typeof e == "string" ? e : "Unknown error", {
    cause: e
  });
}
function ge(e, t = y) {
  throw ft(e);
}
function ie(e) {
  if (typeof e == "function" && !e.length) return ie(e());
  if (Array.isArray(e)) {
    const t = [];
    for (let n = 0; n < e.length; n++) {
      const r = ie(e[n]);
      Array.isArray(r) ? t.push.apply(t, r) : t.push(r);
    }
    return t;
  }
  return e;
}
function ht(e, t) {
  return function(r) {
    let s;
    return R(
      () => s = O(() => (y.context = {
        ...y.context,
        [e]: r.value
      }, ke(() => r.children))),
      void 0
    ), s;
  };
}
function x(e, t) {
  return O(() => e(t || {}));
}
function H() {
  return !0;
}
const le = {
  get(e, t, n) {
    return t === G ? n : e.get(t);
  },
  has(e, t) {
    return t === G ? !0 : e.has(t);
  },
  set: H,
  deleteProperty: H,
  getOwnPropertyDescriptor(e, t) {
    return {
      configurable: !0,
      enumerable: !0,
      get() {
        return e.get(t);
      },
      set: H,
      deleteProperty: H
    };
  },
  ownKeys(e) {
    return e.keys();
  }
};
function re(e) {
  return (e = typeof e == "function" ? e() : e) ? e : {};
}
function dt() {
  for (let e = 0, t = this.length; e < t; ++e) {
    const n = this[e]();
    if (n !== void 0) return n;
  }
}
function ae(...e) {
  let t = !1;
  for (let l = 0; l < e.length; l++) {
    const i = e[l];
    t = t || !!i && G in i, e[l] = typeof i == "function" ? (t = !0, P(i)) : i;
  }
  if (Oe && t)
    return new Proxy(
      {
        get(l) {
          for (let i = e.length - 1; i >= 0; i--) {
            const a = re(e[i])[l];
            if (a !== void 0) return a;
          }
        },
        has(l) {
          for (let i = e.length - 1; i >= 0; i--)
            if (l in re(e[i])) return !0;
          return !1;
        },
        keys() {
          const l = [];
          for (let i = 0; i < e.length; i++)
            l.push(...Object.keys(re(e[i])));
          return [...new Set(l)];
        }
      },
      le
    );
  const n = {}, r = /* @__PURE__ */ Object.create(null);
  for (let l = e.length - 1; l >= 0; l--) {
    const i = e[l];
    if (!i) continue;
    const a = Object.getOwnPropertyNames(i);
    for (let c = a.length - 1; c >= 0; c--) {
      const u = a[c];
      if (u === "__proto__" || u === "constructor") continue;
      const h = Object.getOwnPropertyDescriptor(i, u);
      if (!r[u])
        r[u] = h.get ? {
          enumerable: !0,
          configurable: !0,
          get: dt.bind(n[u] = [h.get.bind(i)])
        } : h.value !== void 0 ? h : void 0;
      else {
        const f = n[u];
        f && (h.get ? f.push(h.get.bind(i)) : h.value !== void 0 && f.push(() => h.value));
      }
    }
  }
  const s = {}, o = Object.keys(r);
  for (let l = o.length - 1; l >= 0; l--) {
    const i = o[l], a = r[i];
    a && a.get ? Object.defineProperty(s, i, a) : s[i] = a ? a.value : void 0;
  }
  return s;
}
function gt(e, ...t) {
  if (Oe && G in e) {
    const s = new Set(t.length > 1 ? t.flat() : t[0]), o = t.map((l) => new Proxy(
      {
        get(i) {
          return l.includes(i) ? e[i] : void 0;
        },
        has(i) {
          return l.includes(i) && i in e;
        },
        keys() {
          return l.filter((i) => i in e);
        }
      },
      le
    ));
    return o.push(
      new Proxy(
        {
          get(l) {
            return s.has(l) ? void 0 : e[l];
          },
          has(l) {
            return s.has(l) ? !1 : l in e;
          },
          keys() {
            return Object.keys(e).filter((l) => !s.has(l));
          }
        },
        le
      )
    ), o;
  }
  const n = {}, r = t.map(() => ({}));
  for (const s of Object.getOwnPropertyNames(e)) {
    const o = Object.getOwnPropertyDescriptor(e, s), l = !o.get && !o.set && o.enumerable && o.writable && o.configurable;
    let i = !1, a = 0;
    for (const c of t)
      c.includes(s) && (i = !0, l ? r[a][s] = o.value : Object.defineProperty(r[a], s, o)), ++a;
    i || (l ? n[s] = o.value : Object.defineProperty(n, s, o));
  }
  return [...r, n];
}
const mt = (e) => `Stale read from <${e}>.`;
function qe(e) {
  const t = e.keyed, n = P(() => e.when, void 0, void 0), r = t ? n : P(n, void 0, {
    equals: (s, o) => !s == !o
  });
  return P(
    () => {
      const s = r();
      if (s) {
        const o = e.children;
        return typeof o == "function" && o.length > 0 ? O(
          () => o(
            t ? s : () => {
              if (!O(r)) throw mt("Show");
              return n();
            }
          )
        ) : o;
      }
      return e.fallback;
    },
    void 0,
    void 0
  );
}
const yt = [
  "allowfullscreen",
  "async",
  "autofocus",
  "autoplay",
  "checked",
  "controls",
  "default",
  "disabled",
  "formnovalidate",
  "hidden",
  "indeterminate",
  "inert",
  "ismap",
  "loop",
  "multiple",
  "muted",
  "nomodule",
  "novalidate",
  "open",
  "playsinline",
  "readonly",
  "required",
  "reversed",
  "seamless",
  "selected"
], wt = /* @__PURE__ */ new Set([
  "className",
  "value",
  "readOnly",
  "formNoValidate",
  "isMap",
  "noModule",
  "playsInline",
  ...yt
]), pt = /* @__PURE__ */ new Set([
  "innerHTML",
  "textContent",
  "innerText",
  "children"
]), bt = /* @__PURE__ */ Object.assign(/* @__PURE__ */ Object.create(null), {
  className: "class",
  htmlFor: "for"
}), At = /* @__PURE__ */ Object.assign(/* @__PURE__ */ Object.create(null), {
  class: "className",
  formnovalidate: {
    $: "formNoValidate",
    BUTTON: 1,
    INPUT: 1
  },
  ismap: {
    $: "isMap",
    IMG: 1
  },
  nomodule: {
    $: "noModule",
    SCRIPT: 1
  },
  playsinline: {
    $: "playsInline",
    VIDEO: 1
  },
  readonly: {
    $: "readOnly",
    INPUT: 1,
    TEXTAREA: 1
  }
});
function vt(e, t) {
  const n = At[e];
  return typeof n == "object" ? n[t] ? n.$ : void 0 : n;
}
const Pt = /* @__PURE__ */ new Set([
  "beforeinput",
  "click",
  "dblclick",
  "contextmenu",
  "focusin",
  "focusout",
  "input",
  "keydown",
  "keyup",
  "mousedown",
  "mousemove",
  "mouseout",
  "mouseover",
  "mouseup",
  "pointerdown",
  "pointermove",
  "pointerout",
  "pointerover",
  "pointerup",
  "touchend",
  "touchmove",
  "touchstart"
]);
function St(e, t, n) {
  let r = n.length, s = t.length, o = r, l = 0, i = 0, a = t[s - 1].nextSibling, c = null;
  for (; l < s || i < o; ) {
    if (t[l] === n[i]) {
      l++, i++;
      continue;
    }
    for (; t[s - 1] === n[o - 1]; )
      s--, o--;
    if (s === l) {
      const u = o < r ? i ? n[i - 1].nextSibling : n[o - i] : a;
      for (; i < o; ) e.insertBefore(n[i++], u);
    } else if (o === i)
      for (; l < s; )
        (!c || !c.has(t[l])) && t[l].remove(), l++;
    else if (t[l] === n[o - 1] && n[i] === t[s - 1]) {
      const u = t[--s].nextSibling;
      e.insertBefore(n[i++], t[l++].nextSibling), e.insertBefore(n[--o], u), t[s] = n[o];
    } else {
      if (!c) {
        c = /* @__PURE__ */ new Map();
        let h = i;
        for (; h < o; ) c.set(n[h], h++);
      }
      const u = c.get(t[l]);
      if (u != null)
        if (i < u && u < o) {
          let h = l, f = 1, m;
          for (; ++h < s && h < o && !((m = c.get(t[h])) == null || m !== u + f); )
            f++;
          if (f > u - i) {
            const S = t[l];
            for (; i < u; ) e.insertBefore(n[i++], S);
          } else e.replaceChild(n[i++], t[l++]);
        } else l++;
      else t[l++].remove();
    }
  }
}
const Se = "_$DX_DELEGATE";
function Et(e, t, n, r = {}) {
  let s;
  return $e((o) => {
    s = o, t === document ? e() : ye(t, e(), t.firstChild ? null : void 0, n);
  }, r.owner), () => {
    s(), t.textContent = "";
  };
}
function me(e, t, n, r) {
  let s;
  const o = () => {
    const i = document.createElement("template");
    return i.innerHTML = e, i.content.firstChild;
  }, l = () => (s || (s = o())).cloneNode(!0);
  return l.cloneNode = l, l;
}
function Ke(e, t = window.document) {
  const n = t[Se] || (t[Se] = /* @__PURE__ */ new Set());
  for (let r = 0, s = e.length; r < s; r++) {
    const o = e[r];
    n.has(o) || (n.add(o), t.addEventListener(o, jt));
  }
}
function ce(e, t, n) {
  n == null ? e.removeAttribute(t) : e.setAttribute(t, n);
}
function Ct(e, t, n) {
  n ? e.setAttribute(t, "") : e.removeAttribute(t);
}
function xt(e, t) {
  t == null ? e.removeAttribute("class") : e.className = t;
}
function Lt(e, t, n, r) {
  if (r)
    Array.isArray(n) ? (e[`$$${t}`] = n[0], e[`$$${t}Data`] = n[1]) : e[`$$${t}`] = n;
  else if (Array.isArray(n)) {
    const s = n[0];
    e.addEventListener(t, n[0] = (o) => s.call(e, n[1], o));
  } else e.addEventListener(t, n, typeof n != "function" && n);
}
function Ot(e, t, n = {}) {
  const r = Object.keys(t || {}), s = Object.keys(n);
  let o, l;
  for (o = 0, l = s.length; o < l; o++) {
    const i = s[o];
    !i || i === "undefined" || t[i] || (Ee(e, i, !1), delete n[i]);
  }
  for (o = 0, l = r.length; o < l; o++) {
    const i = r[o], a = !!t[i];
    !i || i === "undefined" || n[i] === a || !a || (Ee(e, i, !0), n[i] = a);
  }
  return n;
}
function Rt(e, t, n) {
  if (!t) return n ? ce(e, "style") : t;
  const r = e.style;
  if (typeof t == "string") return r.cssText = t;
  typeof n == "string" && (r.cssText = n = void 0), n || (n = {}), t || (t = {});
  let s, o;
  for (o in n)
    t[o] == null && r.removeProperty(o), delete n[o];
  for (o in t)
    s = t[o], s !== n[o] && (r.setProperty(o, s), n[o] = s);
  return n;
}
function $t(e, t = {}, n, r) {
  const s = {};
  return R(
    () => s.children = V(e, t.children, s.children)
  ), R(() => typeof t.ref == "function" && Tt(t.ref, e)), R(() => Nt(e, t, n, !0, s, !0)), s;
}
function Tt(e, t, n) {
  return O(() => e(t, n));
}
function ye(e, t, n, r) {
  if (n !== void 0 && !r && (r = []), typeof t != "function") return V(e, t, r, n);
  R((s) => V(e, t(), s, n), r);
}
function Nt(e, t, n, r, s = {}, o = !1) {
  t || (t = {});
  for (const l in s)
    if (!(l in t)) {
      if (l === "children") continue;
      s[l] = Ce(e, l, null, s[l], n, o, t);
    }
  for (const l in t) {
    if (l === "children")
      continue;
    const i = t[l];
    s[l] = Ce(e, l, i, s[l], n, o, t);
  }
}
function _t(e) {
  return e.toLowerCase().replace(/-([a-z])/g, (t, n) => n.toUpperCase());
}
function Ee(e, t, n) {
  const r = t.trim().split(/\s+/);
  for (let s = 0, o = r.length; s < o; s++)
    e.classList.toggle(r[s], n);
}
function Ce(e, t, n, r, s, o, l) {
  let i, a, c, u, h;
  if (t === "style") return Rt(e, n, r);
  if (t === "classList") return Ot(e, n, r);
  if (n === r) return r;
  if (t === "ref")
    o || n(e);
  else if (t.slice(0, 3) === "on:") {
    const f = t.slice(3);
    r && e.removeEventListener(f, r, typeof r != "function" && r), n && e.addEventListener(f, n, typeof n != "function" && n);
  } else if (t.slice(0, 10) === "oncapture:") {
    const f = t.slice(10);
    r && e.removeEventListener(f, r, !0), n && e.addEventListener(f, n, !0);
  } else if (t.slice(0, 2) === "on") {
    const f = t.slice(2).toLowerCase(), m = Pt.has(f);
    if (!m && r) {
      const S = Array.isArray(r) ? r[0] : r;
      e.removeEventListener(f, S);
    }
    (m || n) && (Lt(e, f, n, m), m && Ke([f]));
  } else t.slice(0, 5) === "attr:" ? ce(e, t.slice(5), n) : t.slice(0, 5) === "bool:" ? Ct(e, t.slice(5), n) : (h = t.slice(0, 5) === "prop:") || (c = pt.has(t)) || (u = vt(t, e.tagName)) || (a = wt.has(t)) || (i = e.nodeName.includes("-") || "is" in l) ? (h && (t = t.slice(5), a = !0), t === "class" || t === "className" ? xt(e, n) : i && !a && !c ? e[_t(t)] = n : e[u || t] = n) : ce(e, bt[t] || t, n);
  return n;
}
function jt(e) {
  let t = e.target;
  const n = `$$${e.type}`, r = e.target, s = e.currentTarget, o = (a) => Object.defineProperty(e, "target", {
    configurable: !0,
    value: a
  }), l = () => {
    const a = t[n];
    if (a && !t.disabled) {
      const c = t[`${n}Data`];
      if (c !== void 0 ? a.call(t, c, e) : a.call(t, e), e.cancelBubble) return;
    }
    return t.host && typeof t.host != "string" && !t.host._$host && t.contains(e.target) && o(t.host), !0;
  }, i = () => {
    for (; l() && (t = t._$host || t.parentNode || t.host); ) ;
  };
  if (Object.defineProperty(e, "currentTarget", {
    configurable: !0,
    get() {
      return t || document;
    }
  }), e.composedPath) {
    const a = e.composedPath();
    o(a[0]);
    for (let c = 0; c < a.length - 2 && (t = a[c], !!l()); c++) {
      if (t._$host) {
        t = t._$host, i();
        break;
      }
      if (t.parentNode === s)
        break;
    }
  } else i();
  o(r);
}
function V(e, t, n, r, s) {
  for (; typeof n == "function"; ) n = n();
  if (t === n) return n;
  const o = typeof t, l = r !== void 0;
  if (e = l && n[0] && n[0].parentNode || e, o === "string" || o === "number") {
    if (o === "number" && (t = t.toString(), t === n))
      return n;
    if (l) {
      let i = n[0];
      i && i.nodeType === 3 ? i.data !== t && (i.data = t) : i = document.createTextNode(t), n = F(e, n, r, i);
    } else
      n !== "" && typeof n == "string" ? n = e.firstChild.data = t : n = e.textContent = t;
  } else if (t == null || o === "boolean")
    n = F(e, n, r);
  else {
    if (o === "function")
      return R(() => {
        let i = t();
        for (; typeof i == "function"; ) i = i();
        n = V(e, i, n, r);
      }), () => n;
    if (Array.isArray(t)) {
      const i = [], a = n && Array.isArray(n);
      if (ue(i, t, n, s))
        return R(() => n = V(e, i, n, r, !0)), () => n;
      if (i.length === 0) {
        if (n = F(e, n, r), l) return n;
      } else a ? n.length === 0 ? xe(e, i, r) : St(e, n, i) : (n && F(e), xe(e, i));
      n = i;
    } else if (t.nodeType) {
      if (Array.isArray(n)) {
        if (l) return n = F(e, n, r, t);
        F(e, n, null, t);
      } else n == null || n === "" || !e.firstChild ? e.appendChild(t) : e.replaceChild(t, e.firstChild);
      n = t;
    }
  }
  return n;
}
function ue(e, t, n, r) {
  let s = !1;
  for (let o = 0, l = t.length; o < l; o++) {
    let i = t[o], a = n && n[e.length], c;
    if (!(i == null || i === !0 || i === !1)) if ((c = typeof i) == "object" && i.nodeType)
      e.push(i);
    else if (Array.isArray(i))
      s = ue(e, i, a) || s;
    else if (c === "function")
      if (r) {
        for (; typeof i == "function"; ) i = i();
        s = ue(
          e,
          Array.isArray(i) ? i : [i],
          Array.isArray(a) ? a : [a]
        ) || s;
      } else
        e.push(i), s = !0;
    else {
      const u = String(i);
      a && a.nodeType === 3 && a.data === u ? e.push(a) : e.push(document.createTextNode(u));
    }
  }
  return s;
}
function xe(e, t, n = null) {
  for (let r = 0, s = t.length; r < s; r++) e.insertBefore(t[r], n);
}
function F(e, t, n, r) {
  if (n === void 0) return e.textContent = "";
  const s = r || document.createTextNode("");
  if (t.length) {
    let o = !1;
    for (let l = t.length - 1; l >= 0; l--) {
      const i = t[l];
      if (s !== i) {
        const a = i.parentNode === e;
        !o && !l ? a ? e.replaceChild(s, i) : e.insertBefore(s, n) : a && i.remove();
      } else o = !0;
    }
  } else e.insertBefore(s, n);
  return [s];
}
const kt = !1;
function Ve() {
  let e = /* @__PURE__ */ new Set();
  function t(s) {
    return e.add(s), () => e.delete(s);
  }
  let n = !1;
  function r(s, o) {
    if (n)
      return !(n = !1);
    const l = {
      to: s,
      options: o,
      defaultPrevented: !1,
      preventDefault: () => l.defaultPrevented = !0
    };
    for (const i of e)
      i.listener({
        ...l,
        from: i.location,
        retry: (a) => {
          a && (n = !0), i.navigate(s, { ...o, resolve: !1 });
        }
      });
    return !l.defaultPrevented;
  }
  return {
    subscribe: t,
    confirm: r
  };
}
let fe;
function we() {
  (!window.history.state || window.history.state._depth == null) && window.history.replaceState({ ...window.history.state, _depth: window.history.length - 1 }, ""), fe = window.history.state._depth;
}
we();
function It(e) {
  return {
    ...e,
    _depth: window.history.state && window.history.state._depth
  };
}
function Ut(e, t) {
  let n = !1;
  return () => {
    const r = fe;
    we();
    const s = r == null ? null : fe - r;
    if (n) {
      n = !1;
      return;
    }
    s && t(s) ? (n = !0, window.history.go(-s)) : e();
  };
}
const Dt = /^(?:[a-z0-9]+:)?\/\//i, Ft = /^\/+|(\/)\/+$/g, We = "http://sr";
function k(e, t = !1) {
  const n = e.replace(Ft, "$1");
  return n ? t || /^[?#]/.test(n) ? n : "/" + n : "";
}
function X(e, t, n) {
  if (Dt.test(t))
    return;
  const r = k(e), s = n && k(n);
  let o = "";
  return !s || t.startsWith("/") ? o = r : s.toLowerCase().indexOf(r.toLowerCase()) !== 0 ? o = r + s : o = s, (o || "/") + k(t, !o);
}
function Bt(e, t) {
  if (e == null)
    throw new Error(t);
  return e;
}
function Mt(e, t) {
  return k(e).replace(/\/*(\*.*)?$/g, "") + k(t);
}
function He(e) {
  const t = {};
  return e.searchParams.forEach((n, r) => {
    r in t ? Array.isArray(t[r]) ? t[r].push(n) : t[r] = [t[r], n] : t[r] = n;
  }), t;
}
function qt(e, t, n) {
  const [r, s] = e.split("/*", 2), o = r.split("/").filter(Boolean), l = o.length;
  return (i) => {
    const a = i.split("/").filter(Boolean), c = a.length - l;
    if (c < 0 || c > 0 && s === void 0 && !t)
      return null;
    const u = {
      path: l ? "" : "/",
      params: {}
    }, h = (f) => n === void 0 ? void 0 : n[f];
    for (let f = 0; f < l; f++) {
      const m = o[f], S = m[0] === ":", d = S ? a[f] : a[f].toLowerCase(), g = S ? m.slice(1) : m.toLowerCase();
      if (S && se(d, h(g)))
        u.params[g] = d;
      else if (S || !se(d, g))
        return null;
      u.path += `/${d}`;
    }
    if (s) {
      const f = c ? a.slice(-c).join("/") : "";
      if (se(f, h(s)))
        u.params[s] = f;
      else
        return null;
    }
    return u;
  };
}
function se(e, t) {
  const n = (r) => r === e;
  return t === void 0 ? !0 : typeof t == "string" ? n(t) : typeof t == "function" ? t(e) : Array.isArray(t) ? t.some(n) : t instanceof RegExp ? t.test(e) : !1;
}
function Kt(e) {
  const [t, n] = e.pattern.split("/*", 2), r = t.split("/").filter(Boolean);
  return r.reduce((s, o) => s + (o.startsWith(":") ? 2 : 3), r.length - (n === void 0 ? 0 : 1));
}
function Xe(e) {
  const t = /* @__PURE__ */ new Map(), n = Ne();
  return new Proxy({}, {
    get(r, s) {
      return t.has(s) || _e(n, () => t.set(s, P(() => e()[s]))), t.get(s)();
    },
    getOwnPropertyDescriptor() {
      return {
        enumerable: !0,
        configurable: !0
      };
    },
    ownKeys() {
      return Reflect.ownKeys(e());
    }
  });
}
function Ge(e) {
  let t = /(\/?\:[^\/]+)\?/.exec(e);
  if (!t)
    return [e];
  let n = e.slice(0, t.index), r = e.slice(t.index + t[0].length);
  const s = [n, n += t[1]];
  for (; t = /^(\/\:[^\/]+)\?/.exec(r); )
    s.push(n += t[1]), r = r.slice(t[0].length);
  return Ge(r).reduce((o, l) => [...o, ...s.map((i) => i + l)], []);
}
const Vt = 100, Je = je(), pe = je(), be = () => Bt(de(Je), "<A> and 'use' router primitives can be only used inside a Route."), Wt = () => de(pe) || be().base, Ht = (e) => {
  const t = Wt();
  return P(() => t.resolvePath(e()));
}, Xt = (e) => {
  const t = be();
  return P(() => {
    const n = e();
    return n !== void 0 ? t.renderPath(n) : n;
  });
}, Gt = () => be().location;
function Jt(e, t = "") {
  const { component: n, preload: r, load: s, children: o, info: l } = e, i = !o || Array.isArray(o) && !o.length, a = {
    key: e,
    component: n,
    preload: r || s,
    info: l
  };
  return Ye(e.path).reduce((c, u) => {
    for (const h of Ge(u)) {
      const f = Mt(t, h);
      let m = i ? f : f.split("/*", 1)[0];
      m = m.split("/").map((S) => S.startsWith(":") || S.startsWith("*") ? S : encodeURIComponent(S)).join("/"), c.push({
        ...a,
        originalPath: u,
        pattern: m,
        matcher: qt(m, !i, e.matchFilters)
      });
    }
    return c;
  }, []);
}
function Yt(e, t = 0) {
  return {
    routes: e,
    score: Kt(e[e.length - 1]) * 1e4 - t,
    matcher(n) {
      const r = [];
      for (let s = e.length - 1; s >= 0; s--) {
        const o = e[s], l = o.matcher(n);
        if (!l)
          return null;
        r.unshift({
          ...l,
          route: o
        });
      }
      return r;
    }
  };
}
function Ye(e) {
  return Array.isArray(e) ? e : [e];
}
function Qe(e, t = "", n = [], r = []) {
  const s = Ye(e);
  for (let o = 0, l = s.length; o < l; o++) {
    const i = s[o];
    if (i && typeof i == "object") {
      i.hasOwnProperty("path") || (i.path = "");
      const a = Jt(i, t);
      for (const c of a) {
        n.push(c);
        const u = Array.isArray(i.children) && i.children.length === 0;
        if (i.children && !u)
          Qe(i.children, c.pattern, n, r);
        else {
          const h = Yt([...n], r.length);
          r.push(h);
        }
        n.pop();
      }
    }
  }
  return n.length ? r : r.sort((o, l) => l.score - o.score);
}
function oe(e, t) {
  for (let n = 0, r = e.length; n < r; n++) {
    const s = e[n].matcher(t);
    if (s)
      return s;
  }
  return [];
}
function Qt(e, t, n) {
  const r = new URL(We), s = P((u) => {
    const h = e();
    try {
      return new URL(h, r);
    } catch {
      return console.error(`Invalid path ${h}`), u;
    }
  }, r, {
    equals: (u, h) => u.href === h.href
  }), o = P(() => s().pathname), l = P(() => s().search, !0), i = P(() => s().hash), a = () => "", c = he(l, () => He(s()));
  return {
    get pathname() {
      return o();
    },
    get search() {
      return l();
    },
    get hash() {
      return i();
    },
    get state() {
      return t();
    },
    get key() {
      return a();
    },
    query: n ? n(c) : Xe(c)
  };
}
let _;
function Zt() {
  return _;
}
function zt(e, t, n, r = {}) {
  const { signal: [s, o], utils: l = {} } = e, i = l.parsePath || ((w) => w), a = l.renderPath || ((w) => w), c = l.beforeLeave || Ve(), u = X("", r.base || "");
  if (u === void 0)
    throw new Error(`${u} is not a valid base path`);
  u && !s().value && o({ value: u, replace: !0, scroll: !1 });
  const [h, f] = B(!1);
  let m;
  const S = (w, b) => {
    b.value === d() && b.state === v() || (m === void 0 && f(!0), _ = w, m = b, at(() => {
      m === b && (g(m.value), A(m.state), N[1]((C) => C.filter((U) => U.pending)));
    }).finally(() => {
      m === b && lt(() => {
        _ = void 0, w === "navigate" && nt(m), f(!1), m = void 0;
      });
    }));
  }, [d, g] = B(s().value), [v, A] = B(s().state), T = Qt(d, v, l.queryWrapper), L = [], N = B([]), M = P(() => typeof r.transformUrl == "function" ? oe(t(), r.transformUrl(T.pathname)) : oe(t(), T.pathname)), Ae = () => {
    const w = M(), b = {};
    for (let C = 0; C < w.length; C++)
      Object.assign(b, w[C].params);
    return b;
  }, ze = l.paramsWrapper ? l.paramsWrapper(Ae, t) : Xe(Ae), ve = {
    pattern: u,
    path: () => u,
    outlet: () => null,
    resolvePath(w) {
      return X(u, w);
    }
  };
  return R(he(s, (w) => S("native", w), { defer: !0 })), {
    base: ve,
    location: T,
    params: ze,
    isRouting: h,
    renderPath: a,
    parsePath: i,
    navigatorFactory: tt,
    matches: M,
    beforeLeave: c,
    preloadRoute: rt,
    singleFlight: r.singleFlight === void 0 ? !0 : r.singleFlight,
    submissions: N
  };
  function et(w, b, C) {
    O(() => {
      if (typeof b == "number") {
        b && (l.go ? l.go(b) : console.warn("Router integration does not support relative routing"));
        return;
      }
      const U = !b || b[0] === "?", { replace: ee, resolve: D, scroll: te, state: q } = {
        replace: !1,
        resolve: !U,
        scroll: !0,
        ...C
      }, W = D ? w.resolvePath(b) : X(U && T.pathname || "", b);
      if (W === void 0)
        throw new Error(`Path '${b}' is not a routable path`);
      if (L.length >= Vt)
        throw new Error("Too many redirects");
      const Pe = d();
      (W !== Pe || q !== v()) && (kt || c.confirm(W, C) && (L.push({ value: Pe, replace: ee, scroll: te, state: v() }), S("navigate", {
        value: W,
        state: q
      })));
    });
  }
  function tt(w) {
    return w = w || de(pe) || ve, (b, C) => et(w, b, C);
  }
  function nt(w) {
    const b = L[0];
    b && (o({
      ...w,
      replace: b.replace,
      scroll: b.scroll
    }), L.length = 0);
  }
  function rt(w, b) {
    const C = oe(t(), w.pathname), U = _;
    _ = "preload";
    for (let ee in C) {
      const { route: D, params: te } = C[ee];
      D.component && D.component.preload && D.component.preload();
      const { preload: q } = D;
      b && q && _e(n(), () => q({
        params: te,
        location: {
          pathname: w.pathname,
          search: w.search,
          hash: w.hash,
          query: He(w),
          state: null,
          key: ""
        },
        intent: "preload"
      }));
    }
    _ = U;
  }
}
function en(e, t, n, r) {
  const { base: s, location: o, params: l } = e, { pattern: i, component: a, preload: c } = r().route, u = P(() => r().path);
  a && a.preload && a.preload();
  const h = c ? c({ params: l, location: o, intent: _ || "initial" }) : void 0;
  return {
    parent: t,
    pattern: i,
    path: u,
    outlet: () => a ? x(a, {
      params: l,
      location: o,
      data: h,
      get children() {
        return n();
      }
    }) : n(),
    resolvePath(m) {
      return X(s.path(), m, u());
    }
  };
}
const tn = (e) => (t) => {
  const {
    base: n
  } = t, r = ke(() => t.children), s = P(() => Qe(r(), t.base || ""));
  let o;
  const l = zt(e, s, () => o, {
    base: n,
    singleFlight: t.singleFlight,
    transformUrl: t.transformUrl
  });
  return e.create && e.create(l), x(Je.Provider, {
    value: l,
    get children() {
      return x(nn, {
        routerState: l,
        get root() {
          return t.root;
        },
        get preload() {
          return t.rootPreload || t.rootLoad;
        },
        get children() {
          return [P(() => (o = Ne()) && null), x(rn, {
            routerState: l,
            get branches() {
              return s();
            }
          })];
        }
      });
    }
  });
};
function nn(e) {
  const t = e.routerState.location, n = e.routerState.params, r = P(() => e.preload && O(() => {
    e.preload({
      params: n,
      location: t,
      intent: Zt() || "initial"
    });
  }));
  return x(qe, {
    get when() {
      return e.root;
    },
    keyed: !0,
    get fallback() {
      return e.children;
    },
    children: (s) => x(s, {
      params: n,
      location: t,
      get data() {
        return r();
      },
      get children() {
        return e.children;
      }
    })
  });
}
function rn(e) {
  const t = [];
  let n;
  const r = P(he(e.routerState.matches, (s, o, l) => {
    let i = o && s.length === o.length;
    const a = [];
    for (let c = 0, u = s.length; c < u; c++) {
      const h = o && o[c], f = s[c];
      l && h && f.route.key === h.route.key ? a[c] = l[c] : (i = !1, t[c] && t[c](), $e((m) => {
        t[c] = m, a[c] = en(e.routerState, a[c - 1] || e.routerState.base, Le(() => r()[c + 1]), () => e.routerState.matches()[c]);
      }));
    }
    return t.splice(s.length).forEach((c) => c()), l && i ? l : (n = a[0], a);
  }));
  return Le(() => r() && n)();
}
const Le = (e) => () => x(qe, {
  get when() {
    return e();
  },
  keyed: !0,
  children: (t) => x(pe.Provider, {
    value: t,
    get children() {
      return t.outlet();
    }
  })
});
function sn([e, t], n, r) {
  return [e, r ? (s) => t(r(s)) : t];
}
function on(e) {
  let t = !1;
  const n = (s) => typeof s == "string" ? { value: s } : s, r = sn(B(n(e.get()), {
    equals: (s, o) => s.value === o.value && s.state === o.state
  }), void 0, (s) => (!t && e.set(s), s));
  return e.init && Te(e.init((s = e.get()) => {
    t = !0, r[1](n(s)), t = !1;
  })), tn({
    signal: r,
    create: e.create,
    utils: e.utils
  });
}
function ln(e, t, n) {
  return e.addEventListener(t, n), () => e.removeEventListener(t, n);
}
function an(e, t) {
  const n = e && document.getElementById(e);
  n ? n.scrollIntoView() : t && window.scrollTo(0, 0);
}
const cn = /* @__PURE__ */ new Map();
function un(e = !0, t = !1, n = "/_server", r) {
  return (s) => {
    const o = s.base.path(), l = s.navigatorFactory(s.base);
    let i, a;
    function c(d) {
      return d.namespaceURI === "http://www.w3.org/2000/svg";
    }
    function u(d) {
      if (d.defaultPrevented || d.button !== 0 || d.metaKey || d.altKey || d.ctrlKey || d.shiftKey)
        return;
      const g = d.composedPath().find((M) => M instanceof Node && M.nodeName.toUpperCase() === "A");
      if (!g || t && !g.hasAttribute("link"))
        return;
      const v = c(g), A = v ? g.href.baseVal : g.href;
      if ((v ? g.target.baseVal : g.target) || !A && !g.hasAttribute("state"))
        return;
      const L = (g.getAttribute("rel") || "").split(/\s+/);
      if (g.hasAttribute("download") || L && L.includes("external"))
        return;
      const N = v ? new URL(A, document.baseURI) : new URL(A);
      if (!(N.origin !== window.location.origin || o && N.pathname && !N.pathname.toLowerCase().startsWith(o.toLowerCase())))
        return [g, N];
    }
    function h(d) {
      const g = u(d);
      if (!g)
        return;
      const [v, A] = g, T = s.parsePath(A.pathname + A.search + A.hash), L = v.getAttribute("state");
      d.preventDefault(), l(T, {
        resolve: !1,
        replace: v.hasAttribute("replace"),
        scroll: !v.hasAttribute("noscroll"),
        state: L ? JSON.parse(L) : void 0
      });
    }
    function f(d) {
      const g = u(d);
      if (!g)
        return;
      const [v, A] = g;
      r && (A.pathname = r(A.pathname)), s.preloadRoute(A, v.getAttribute("preload") !== "false");
    }
    function m(d) {
      clearTimeout(i);
      const g = u(d);
      if (!g)
        return a = null;
      const [v, A] = g;
      a !== v && (r && (A.pathname = r(A.pathname)), i = setTimeout(() => {
        s.preloadRoute(A, v.getAttribute("preload") !== "false"), a = v;
      }, 20));
    }
    function S(d) {
      if (d.defaultPrevented)
        return;
      let g = d.submitter && d.submitter.hasAttribute("formaction") ? d.submitter.getAttribute("formaction") : d.target.getAttribute("action");
      if (!g)
        return;
      if (!g.startsWith("https://action/")) {
        const A = new URL(g, We);
        if (g = s.parsePath(A.pathname + A.search), !g.startsWith(n))
          return;
      }
      if (d.target.method.toUpperCase() !== "POST")
        throw new Error("Only POST forms are supported for Actions");
      const v = cn.get(g);
      if (v) {
        d.preventDefault();
        const A = new FormData(d.target, d.submitter);
        v.call({ r: s, f: d.target }, d.target.enctype === "multipart/form-data" ? A : new URLSearchParams(A));
      }
    }
    Ke(["click", "submit"]), document.addEventListener("click", h), e && (document.addEventListener("mousemove", m, { passive: !0 }), document.addEventListener("focusin", f, { passive: !0 }), document.addEventListener("touchstart", f, { passive: !0 })), document.addEventListener("submit", S), Te(() => {
      document.removeEventListener("click", h), e && (document.removeEventListener("mousemove", m), document.removeEventListener("focusin", f), document.removeEventListener("touchstart", f)), document.removeEventListener("submit", S);
    });
  };
}
function fn(e) {
  const t = () => {
    const r = window.location.pathname.replace(/^\/+/, "/") + window.location.search, s = window.history.state && window.history.state._depth && Object.keys(window.history.state).length === 1 ? void 0 : window.history.state;
    return {
      value: r + window.location.hash,
      state: s
    };
  }, n = Ve();
  return on({
    get: t,
    set({ value: r, replace: s, scroll: o, state: l }) {
      s ? window.history.replaceState(It(l), "", r) : window.history.pushState(l, "", r), an(decodeURIComponent(window.location.hash.slice(1)), o), we();
    },
    init: (r) => ln(window, "popstate", Ut(r, (s) => {
      if (s && s < 0)
        return !n.confirm(s);
      {
        const o = t();
        return !n.confirm(o.value, { state: o.state });
      }
    })),
    create: un(e.preload, e.explicitLinks, e.actionBase, e.transformUrl),
    utils: {
      go: (r) => window.history.go(r),
      beforeLeave: n
    }
  })(e);
}
var hn = /* @__PURE__ */ me("<a>");
function dn(e) {
  e = ae({
    inactiveClass: "inactive",
    activeClass: "active"
  }, e);
  const [, t] = gt(e, ["href", "state", "class", "activeClass", "inactiveClass", "end"]), n = Ht(() => e.href), r = Xt(n), s = Gt(), o = P(() => {
    const l = n();
    if (l === void 0) return [!1, !1];
    const i = k(l.split(/[?#]/, 1)[0]).toLowerCase(), a = decodeURI(k(s.pathname).toLowerCase());
    return [e.end ? i === a : a.startsWith(i + "/") || a === i, i === a];
  });
  return (() => {
    var l = hn();
    return $t(l, ae(t, {
      get href() {
        return r() || e.href;
      },
      get state() {
        return JSON.stringify(e.state);
      },
      get classList() {
        return {
          ...e.class && {
            [e.class]: !0
          },
          [e.inactiveClass]: !o()[0],
          [e.activeClass]: o()[0],
          ...t.classList
        };
      },
      link: "",
      get "aria-current"() {
        return o()[1] ? "page" : void 0;
      }
    }), !1), l;
  })();
}
const Ze = (e) => x(dn, ae(e, {
  "data-enhance-nav": "false"
}));
var gn = /* @__PURE__ */ me('<div><a href=/>Home</a><button class="btn btn-primary">Click!</button><ul><li>');
const mn = () => (() => {
  var e = gn(), t = e.firstChild, n = t.nextSibling, r = n.nextSibling, s = r.firstChild;
  return ye(s, x(Ze, {
    href: "/about",
    class: "btn btn-link",
    children: "About"
  })), e;
})();
var yn = /* @__PURE__ */ me("<div>");
const wn = () => (() => {
  var e = yn();
  return ye(e, x(Ze, {
    href: "/",
    class: "btn btn-link",
    children: "Home"
  })), e;
})(), pn = [{
  path: "/",
  component: mn
}, {
  path: "/about",
  component: wn
}], bn = () => x(fn, {
  base: "/app",
  children: pn
}), Pn = (e) => Et(() => x(bn, {}), e);
export {
  Pn as renderApp
};

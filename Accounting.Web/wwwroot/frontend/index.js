const m = {
  context: void 0,
  registry: void 0,
  effects: void 0,
  done: !1,
  getContextId() {
    return _e(this.context.count);
  },
  getNextContextId() {
    return _e(this.context.count++);
  }
};
function _e(e) {
  const t = String(e), n = t.length - 1;
  return m.context.id + (n ? String.fromCharCode(96 + n) : "") + t;
}
function Q(e) {
  m.context = e;
}
const Fe = !1, pt = (e, t) => e === t, oe = Symbol("solid-proxy"), Me = typeof Proxy == "function", ie = {
  equals: pt
};
let Ve = Qe;
const D = 1, le = 2, He = {
  owned: null,
  cleanups: null,
  context: null,
  owner: null
}, me = {};
var y = null;
let pe = null, yt = null, w = null, O = null, U = null, ue = 0;
function We(e, t) {
  const n = w, r = y, s = e.length === 0, o = t === void 0 ? r : t, i = s ? He : {
    owned: null,
    cleanups: null,
    context: o ? o.context : null,
    owner: o
  }, l = s ? e : () => e(() => k(() => Z(i)));
  y = i, w = null;
  try {
    return I(l, !0);
  } finally {
    w = n, y = r;
  }
}
function _(e, t) {
  t = t ? Object.assign({}, ie, t) : ie;
  const n = {
    value: e,
    observers: null,
    observerSlots: null,
    comparator: t.equals || void 0
  }, r = (s) => (typeof s == "function" && (s = s(n.value)), Je(n, s));
  return [Ye.bind(n), r];
}
function wt(e, t, n) {
  const r = fe(e, t, !0, D);
  X(r);
}
function F(e, t, n) {
  const r = fe(e, t, !1, D);
  X(r);
}
function bt(e, t, n) {
  Ve = Lt;
  const r = fe(e, t, !1, D);
  r.user = !0, U ? U.push(r) : X(r);
}
function x(e, t, n) {
  n = n ? Object.assign({}, ie, n) : ie;
  const r = fe(e, t, !0, 0);
  return r.observers = null, r.observerSlots = null, r.comparator = n.equals || void 0, X(r), Ye.bind(r);
}
function vt(e) {
  return e && typeof e == "object" && "then" in e;
}
function At(e, t, n) {
  let r, s, o;
  r = !0, s = e, o = {};
  let i = null, l = me, c = null, a = !1, u = "initialValue" in o, f = typeof r == "function" && x(r);
  const d = /* @__PURE__ */ new Set(), [g, P] = (o.storage || _)(o.initialValue), [h, p] = _(void 0), [S, v] = _(void 0, {
    equals: !1
  }), [j, T] = _(u ? "ready" : "unresolved");
  m.context && (c = m.getNextContextId(), o.ssrLoadFrom === "initial" ? l = o.initialValue : m.load && m.has(c) && (l = m.load(c)));
  function $(L, C, E, B) {
    return i === L && (i = null, B !== void 0 && (u = !0), (L === l || C === l) && o.onHydrated && queueMicrotask(
      () => o.onHydrated(B, {
        value: C
      })
    ), l = me, M(C, E)), C;
  }
  function M(L, C) {
    I(() => {
      C === void 0 && P(() => L), T(C !== void 0 ? "errored" : u ? "ready" : "unresolved"), p(C);
      for (const E of d.keys()) E.decrement();
      d.clear();
    }, !1);
  }
  function W() {
    const L = xt, C = g(), E = h();
    if (E !== void 0 && !i) throw E;
    return w && w.user, C;
  }
  function Y(L = !0) {
    if (L !== !1 && a) return;
    a = !1;
    const C = f ? f() : r;
    if (C == null || C === !1) {
      $(i, k(g));
      return;
    }
    const E = l !== me ? l : k(
      () => s(C, {
        value: g(),
        refetching: L
      })
    );
    return vt(E) ? (i = E, "value" in E ? (E.status === "success" ? $(i, E.value, void 0, C) : $(i, void 0, ve(E.value), C), E) : (a = !0, queueMicrotask(() => a = !1), I(() => {
      T(u ? "refreshing" : "pending"), v();
    }, !1), E.then(
      (B) => $(E, B, void 0, C),
      (B) => $(E, void 0, ve(B), C)
    ))) : ($(i, E, void 0, C), E);
  }
  return Object.defineProperties(W, {
    state: {
      get: () => j()
    },
    error: {
      get: () => h()
    },
    loading: {
      get() {
        const L = j();
        return L === "pending" || L === "refreshing";
      }
    },
    latest: {
      get() {
        if (!u) return W();
        const L = h();
        if (L && !i) throw L;
        return g();
      }
    }
  }), f ? wt(() => Y(!1)) : Y(!1), [
    W,
    {
      refetch: Y,
      mutate: P
    }
  ];
}
function Pt(e) {
  return I(e, !1);
}
function k(e) {
  if (w === null) return e();
  const t = w;
  w = null;
  try {
    return e();
  } finally {
    w = t;
  }
}
function Le(e, t, n) {
  const r = Array.isArray(e);
  let s, o = n && n.defer;
  return (i) => {
    let l;
    if (r) {
      l = Array(e.length);
      for (let a = 0; a < e.length; a++) l[a] = e[a]();
    } else l = e();
    if (o)
      return o = !1, i;
    const c = k(() => t(l, s, i));
    return s = l, c;
  };
}
function qe(e) {
  return y === null || (y.cleanups === null ? y.cleanups = [e] : y.cleanups.push(e)), e;
}
function Ke() {
  return y;
}
function Ge(e, t) {
  const n = y, r = w;
  y = e, w = null;
  try {
    return I(t, !0);
  } catch (s) {
    Re(s);
  } finally {
    y = n, w = r;
  }
}
function St(e) {
  const t = w, n = y;
  return Promise.resolve().then(() => {
    w = t, y = n;
    let r;
    return I(e, !1), w = y = null, r ? r.done : void 0;
  });
}
const [Hn, Wn] = /* @__PURE__ */ _(!1);
function Xe(e, t) {
  const n = Symbol("context");
  return {
    id: n,
    Provider: Ot(n),
    defaultValue: e
  };
}
function Oe(e) {
  let t;
  return y && y.context && (t = y.context[e.id]) !== void 0 ? t : e.defaultValue;
}
function ze(e) {
  const t = x(e), n = x(() => Ae(t()));
  return n.toArray = () => {
    const r = n();
    return Array.isArray(r) ? r : r != null ? [r] : [];
  }, n;
}
let xt;
function Ye() {
  if (this.sources && this.state)
    if (this.state === D) X(this);
    else {
      const e = O;
      O = null, I(() => ae(this), !1), O = e;
    }
  if (w) {
    const e = this.observers ? this.observers.length : 0;
    w.sources ? (w.sources.push(this), w.sourceSlots.push(e)) : (w.sources = [this], w.sourceSlots = [e]), this.observers ? (this.observers.push(w), this.observerSlots.push(w.sources.length - 1)) : (this.observers = [w], this.observerSlots = [w.sources.length - 1]);
  }
  return this.value;
}
function Je(e, t, n) {
  let r = e.value;
  return (!e.comparator || !e.comparator(r, t)) && (e.value = t, e.observers && e.observers.length && I(() => {
    for (let s = 0; s < e.observers.length; s += 1) {
      const o = e.observers[s], i = pe && pe.running;
      i && pe.disposed.has(o), (i ? !o.tState : !o.state) && (o.pure ? O.push(o) : U.push(o), o.observers && Ze(o)), i || (o.state = D);
    }
    if (O.length > 1e6)
      throw O = [], new Error();
  }, !1)), t;
}
function X(e) {
  if (!e.fn) return;
  Z(e);
  const t = ue;
  Ct(
    e,
    e.value,
    t
  );
}
function Ct(e, t, n) {
  let r;
  const s = y, o = w;
  w = y = e;
  try {
    r = e.fn(t);
  } catch (i) {
    return e.pure && (e.state = D, e.owned && e.owned.forEach(Z), e.owned = null), e.updatedAt = n + 1, Re(i);
  } finally {
    w = o, y = s;
  }
  (!e.updatedAt || e.updatedAt <= n) && (e.updatedAt != null && "observers" in e ? Je(e, r) : e.value = r, e.updatedAt = n);
}
function fe(e, t, n, r = D, s) {
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
  return y === null || y !== He && (y.owned ? y.owned.push(o) : y.owned = [o]), o;
}
function ce(e) {
  if (e.state === 0) return;
  if (e.state === le) return ae(e);
  if (e.suspense && k(e.suspense.inFallback)) return e.suspense.effects.push(e);
  const t = [e];
  for (; (e = e.owner) && (!e.updatedAt || e.updatedAt < ue); )
    e.state && t.push(e);
  for (let n = t.length - 1; n >= 0; n--)
    if (e = t[n], e.state === D)
      X(e);
    else if (e.state === le) {
      const r = O;
      O = null, I(() => ae(e, t[0]), !1), O = r;
    }
}
function I(e, t) {
  if (O) return e();
  let n = !1;
  t || (O = []), U ? n = !0 : U = [], ue++;
  try {
    const r = e();
    return Et(n), r;
  } catch (r) {
    n || (U = null), O = null, Re(r);
  }
}
function Et(e) {
  if (O && (Qe(O), O = null), e) return;
  const t = U;
  U = null, t.length && I(() => Ve(t), !1);
}
function Qe(e) {
  for (let t = 0; t < e.length; t++) ce(e[t]);
}
function Lt(e) {
  let t, n = 0;
  for (t = 0; t < e.length; t++) {
    const r = e[t];
    r.user ? e[n++] = r : ce(r);
  }
  if (m.context) {
    if (m.count) {
      m.effects || (m.effects = []), m.effects.push(...e.slice(0, n));
      return;
    }
    Q();
  }
  for (m.effects && (m.done || !m.count) && (e = [...m.effects, ...e], n += m.effects.length, delete m.effects), t = 0; t < n; t++) ce(e[t]);
}
function ae(e, t) {
  e.state = 0;
  for (let n = 0; n < e.sources.length; n += 1) {
    const r = e.sources[n];
    if (r.sources) {
      const s = r.state;
      s === D ? r !== t && (!r.updatedAt || r.updatedAt < ue) && ce(r) : s === le && ae(r, t);
    }
  }
}
function Ze(e) {
  for (let t = 0; t < e.observers.length; t += 1) {
    const n = e.observers[t];
    n.state || (n.state = le, n.pure ? O.push(n) : U.push(n), n.observers && Ze(n));
  }
}
function Z(e) {
  let t;
  if (e.sources)
    for (; e.sources.length; ) {
      const n = e.sources.pop(), r = e.sourceSlots.pop(), s = n.observers;
      if (s && s.length) {
        const o = s.pop(), i = n.observerSlots.pop();
        r < s.length && (o.sourceSlots[i] = r, s[r] = o, n.observerSlots[r] = i);
      }
    }
  if (e.tOwned) {
    for (t = e.tOwned.length - 1; t >= 0; t--) Z(e.tOwned[t]);
    delete e.tOwned;
  }
  if (e.owned) {
    for (t = e.owned.length - 1; t >= 0; t--) Z(e.owned[t]);
    e.owned = null;
  }
  if (e.cleanups) {
    for (t = e.cleanups.length - 1; t >= 0; t--) e.cleanups[t]();
    e.cleanups = null;
  }
  e.state = 0;
}
function ve(e) {
  return e instanceof Error ? e : new Error(typeof e == "string" ? e : "Unknown error", {
    cause: e
  });
}
function Re(e, t = y) {
  throw ve(e);
}
function Ae(e) {
  if (typeof e == "function" && !e.length) return Ae(e());
  if (Array.isArray(e)) {
    const t = [];
    for (let n = 0; n < e.length; n++) {
      const r = Ae(e[n]);
      Array.isArray(r) ? t.push.apply(t, r) : t.push(r);
    }
    return t;
  }
  return e;
}
function Ot(e, t) {
  return function(r) {
    let s;
    return F(
      () => s = k(() => (y.context = {
        ...y.context,
        [e]: r.value
      }, ze(() => r.children))),
      void 0
    ), s;
  };
}
function R(e, t) {
  return k(() => e(t || {}));
}
function re() {
  return !0;
}
const Pe = {
  get(e, t, n) {
    return t === oe ? n : e.get(t);
  },
  has(e, t) {
    return t === oe ? !0 : e.has(t);
  },
  set: re,
  deleteProperty: re,
  getOwnPropertyDescriptor(e, t) {
    return {
      configurable: !0,
      enumerable: !0,
      get() {
        return e.get(t);
      },
      set: re,
      deleteProperty: re
    };
  },
  ownKeys(e) {
    return e.keys();
  }
};
function ye(e) {
  return (e = typeof e == "function" ? e() : e) ? e : {};
}
function Rt() {
  for (let e = 0, t = this.length; e < t; ++e) {
    const n = this[e]();
    if (n !== void 0) return n;
  }
}
function Se(...e) {
  let t = !1;
  for (let i = 0; i < e.length; i++) {
    const l = e[i];
    t = t || !!l && oe in l, e[i] = typeof l == "function" ? (t = !0, x(l)) : l;
  }
  if (Me && t)
    return new Proxy(
      {
        get(i) {
          for (let l = e.length - 1; l >= 0; l--) {
            const c = ye(e[l])[i];
            if (c !== void 0) return c;
          }
        },
        has(i) {
          for (let l = e.length - 1; l >= 0; l--)
            if (i in ye(e[l])) return !0;
          return !1;
        },
        keys() {
          const i = [];
          for (let l = 0; l < e.length; l++)
            i.push(...Object.keys(ye(e[l])));
          return [...new Set(i)];
        }
      },
      Pe
    );
  const n = {}, r = /* @__PURE__ */ Object.create(null);
  for (let i = e.length - 1; i >= 0; i--) {
    const l = e[i];
    if (!l) continue;
    const c = Object.getOwnPropertyNames(l);
    for (let a = c.length - 1; a >= 0; a--) {
      const u = c[a];
      if (u === "__proto__" || u === "constructor") continue;
      const f = Object.getOwnPropertyDescriptor(l, u);
      if (!r[u])
        r[u] = f.get ? {
          enumerable: !0,
          configurable: !0,
          get: Rt.bind(n[u] = [f.get.bind(l)])
        } : f.value !== void 0 ? f : void 0;
      else {
        const d = n[u];
        d && (f.get ? d.push(f.get.bind(l)) : f.value !== void 0 && d.push(() => f.value));
      }
    }
  }
  const s = {}, o = Object.keys(r);
  for (let i = o.length - 1; i >= 0; i--) {
    const l = o[i], c = r[l];
    c && c.get ? Object.defineProperty(s, l, c) : s[l] = c ? c.value : void 0;
  }
  return s;
}
function $t(e, ...t) {
  if (Me && oe in e) {
    const s = new Set(t.length > 1 ? t.flat() : t[0]), o = t.map((i) => new Proxy(
      {
        get(l) {
          return i.includes(l) ? e[l] : void 0;
        },
        has(l) {
          return i.includes(l) && l in e;
        },
        keys() {
          return i.filter((l) => l in e);
        }
      },
      Pe
    ));
    return o.push(
      new Proxy(
        {
          get(i) {
            return s.has(i) ? void 0 : e[i];
          },
          has(i) {
            return s.has(i) ? !1 : i in e;
          },
          keys() {
            return Object.keys(e).filter((i) => !s.has(i));
          }
        },
        Pe
      )
    ), o;
  }
  const n = {}, r = t.map(() => ({}));
  for (const s of Object.getOwnPropertyNames(e)) {
    const o = Object.getOwnPropertyDescriptor(e, s), i = !o.get && !o.set && o.enumerable && o.writable && o.configurable;
    let l = !1, c = 0;
    for (const a of t)
      a.includes(s) && (l = !0, i ? r[c][s] = o.value : Object.defineProperty(r[c], s, o)), ++c;
    l || (i ? n[s] = o.value : Object.defineProperty(n, s, o));
  }
  return [...r, n];
}
function Tt(e) {
  let t, n;
  const r = (s) => {
    const o = m.context;
    if (o) {
      const [l, c] = _();
      m.count || (m.count = 0), m.count++, (n || (n = e())).then((a) => {
        !m.done && Q(o), m.count--, c(() => a.default), Q();
      }), t = l;
    } else if (!t) {
      const [l] = At(() => (n || (n = e())).then((c) => c.default));
      t = l;
    }
    let i;
    return x(
      () => (i = t()) ? k(() => {
        if (!o || m.done) return i(s);
        const l = m.context;
        Q(o);
        const c = i(s);
        return Q(l), c;
      }) : ""
    );
  };
  return r.preload = () => n || ((n = e()).then((s) => t = () => s.default), n), r;
}
const Nt = (e) => `Stale read from <${e}>.`;
function et(e) {
  const t = e.keyed, n = x(() => e.when, void 0, void 0), r = t ? n : x(n, void 0, {
    equals: (s, o) => !s == !o
  });
  return x(
    () => {
      const s = r();
      if (s) {
        const o = e.children;
        return typeof o == "function" && o.length > 0 ? k(
          () => o(
            t ? s : () => {
              if (!k(r)) throw Nt("Show");
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
const kt = [
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
], _t = /* @__PURE__ */ new Set([
  "className",
  "value",
  "readOnly",
  "formNoValidate",
  "isMap",
  "noModule",
  "playsInline",
  ...kt
]), jt = /* @__PURE__ */ new Set([
  "innerHTML",
  "textContent",
  "innerText",
  "children"
]), It = /* @__PURE__ */ Object.assign(/* @__PURE__ */ Object.create(null), {
  className: "class",
  htmlFor: "for"
}), Ut = /* @__PURE__ */ Object.assign(/* @__PURE__ */ Object.create(null), {
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
function Dt(e, t) {
  const n = Ut[e];
  return typeof n == "object" ? n[t] ? n.$ : void 0 : n;
}
const Bt = /* @__PURE__ */ new Set([
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
function Ft(e, t, n) {
  let r = n.length, s = t.length, o = r, i = 0, l = 0, c = t[s - 1].nextSibling, a = null;
  for (; i < s || l < o; ) {
    if (t[i] === n[l]) {
      i++, l++;
      continue;
    }
    for (; t[s - 1] === n[o - 1]; )
      s--, o--;
    if (s === i) {
      const u = o < r ? l ? n[l - 1].nextSibling : n[o - l] : c;
      for (; l < o; ) e.insertBefore(n[l++], u);
    } else if (o === l)
      for (; i < s; )
        (!a || !a.has(t[i])) && t[i].remove(), i++;
    else if (t[i] === n[o - 1] && n[l] === t[s - 1]) {
      const u = t[--s].nextSibling;
      e.insertBefore(n[l++], t[i++].nextSibling), e.insertBefore(n[--o], u), t[s] = n[o];
    } else {
      if (!a) {
        a = /* @__PURE__ */ new Map();
        let f = l;
        for (; f < o; ) a.set(n[f], f++);
      }
      const u = a.get(t[i]);
      if (u != null)
        if (l < u && u < o) {
          let f = i, d = 1, g;
          for (; ++f < s && f < o && !((g = a.get(t[f])) == null || g !== u + d); )
            d++;
          if (d > u - l) {
            const P = t[i];
            for (; l < u; ) e.insertBefore(n[l++], P);
          } else e.replaceChild(n[l++], t[i++]);
        } else i++;
      else t[i++].remove();
    }
  }
}
const je = "_$DX_DELEGATE";
function tt(e, t, n, r = {}) {
  let s;
  return We((o) => {
    s = o, t === document ? e() : $e(t, e(), t.firstChild ? null : void 0, n);
  }, r.owner), () => {
    s(), t.textContent = "";
  };
}
function z(e, t, n, r) {
  let s;
  const o = () => {
    const l = document.createElement("template");
    return l.innerHTML = e, l.content.firstChild;
  }, i = () => (s || (s = o())).cloneNode(!0);
  return i.cloneNode = i, i;
}
function nt(e, t = window.document) {
  const n = t[je] || (t[je] = /* @__PURE__ */ new Set());
  for (let r = 0, s = e.length; r < s; r++) {
    const o = e[r];
    n.has(o) || (n.add(o), t.addEventListener(o, Yt));
  }
}
function xe(e, t, n) {
  te(e) || (n == null ? e.removeAttribute(t) : e.setAttribute(t, n));
}
function Mt(e, t, n) {
  te(e) || (n ? e.setAttribute(t, "") : e.removeAttribute(t));
}
function Vt(e, t) {
  te(e) || (t == null ? e.removeAttribute("class") : e.className = t);
}
function Ht(e, t, n, r) {
  if (r)
    Array.isArray(n) ? (e[`$$${t}`] = n[0], e[`$$${t}Data`] = n[1]) : e[`$$${t}`] = n;
  else if (Array.isArray(n)) {
    const s = n[0];
    e.addEventListener(t, n[0] = (o) => s.call(e, n[1], o));
  } else e.addEventListener(t, n, typeof n != "function" && n);
}
function Wt(e, t, n = {}) {
  const r = Object.keys(t || {}), s = Object.keys(n);
  let o, i;
  for (o = 0, i = s.length; o < i; o++) {
    const l = s[o];
    !l || l === "undefined" || t[l] || (Ie(e, l, !1), delete n[l]);
  }
  for (o = 0, i = r.length; o < i; o++) {
    const l = r[o], c = !!t[l];
    !l || l === "undefined" || n[l] === c || !c || (Ie(e, l, !0), n[l] = c);
  }
  return n;
}
function qt(e, t, n) {
  if (!t) return n ? xe(e, "style") : t;
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
function Kt(e, t = {}, n, r) {
  const s = {};
  return F(
    () => s.children = ee(e, t.children, s.children)
  ), F(() => typeof t.ref == "function" && Gt(t.ref, e)), F(() => Xt(e, t, n, !0, s, !0)), s;
}
function Gt(e, t, n) {
  return k(() => e(t, n));
}
function $e(e, t, n, r) {
  if (n !== void 0 && !r && (r = []), typeof t != "function") return ee(e, t, r, n);
  F((s) => ee(e, t(), s, n), r);
}
function Xt(e, t, n, r, s = {}, o = !1) {
  t || (t = {});
  for (const i in s)
    if (!(i in t)) {
      if (i === "children") continue;
      s[i] = Ue(e, i, null, s[i], n, o, t);
    }
  for (const i in t) {
    if (i === "children")
      continue;
    const l = t[i];
    s[i] = Ue(e, i, l, s[i], n, o, t);
  }
}
function te(e) {
  return !!m.context && !m.done && (!e || e.isConnected);
}
function zt(e) {
  return e.toLowerCase().replace(/-([a-z])/g, (t, n) => n.toUpperCase());
}
function Ie(e, t, n) {
  const r = t.trim().split(/\s+/);
  for (let s = 0, o = r.length; s < o; s++)
    e.classList.toggle(r[s], n);
}
function Ue(e, t, n, r, s, o, i) {
  let l, c, a, u, f;
  if (t === "style") return qt(e, n, r);
  if (t === "classList") return Wt(e, n, r);
  if (n === r) return r;
  if (t === "ref")
    o || n(e);
  else if (t.slice(0, 3) === "on:") {
    const d = t.slice(3);
    r && e.removeEventListener(d, r, typeof r != "function" && r), n && e.addEventListener(d, n, typeof n != "function" && n);
  } else if (t.slice(0, 10) === "oncapture:") {
    const d = t.slice(10);
    r && e.removeEventListener(d, r, !0), n && e.addEventListener(d, n, !0);
  } else if (t.slice(0, 2) === "on") {
    const d = t.slice(2).toLowerCase(), g = Bt.has(d);
    if (!g && r) {
      const P = Array.isArray(r) ? r[0] : r;
      e.removeEventListener(d, P);
    }
    (g || n) && (Ht(e, d, n, g), g && nt([d]));
  } else if (t.slice(0, 5) === "attr:")
    xe(e, t.slice(5), n);
  else if (t.slice(0, 5) === "bool:")
    Mt(e, t.slice(5), n);
  else if ((f = t.slice(0, 5) === "prop:") || (a = jt.has(t)) || (u = Dt(t, e.tagName)) || (c = _t.has(t)) || (l = e.nodeName.includes("-") || "is" in i)) {
    if (f)
      t = t.slice(5), c = !0;
    else if (te(e)) return n;
    t === "class" || t === "className" ? Vt(e, n) : l && !c && !a ? e[zt(t)] = n : e[u || t] = n;
  } else
    xe(e, It[t] || t, n);
  return n;
}
function Yt(e) {
  if (m.registry && m.events && m.events.find(([c, a]) => a === e))
    return;
  let t = e.target;
  const n = `$$${e.type}`, r = e.target, s = e.currentTarget, o = (c) => Object.defineProperty(e, "target", {
    configurable: !0,
    value: c
  }), i = () => {
    const c = t[n];
    if (c && !t.disabled) {
      const a = t[`${n}Data`];
      if (a !== void 0 ? c.call(t, a, e) : c.call(t, e), e.cancelBubble) return;
    }
    return t.host && typeof t.host != "string" && !t.host._$host && t.contains(e.target) && o(t.host), !0;
  }, l = () => {
    for (; i() && (t = t._$host || t.parentNode || t.host); ) ;
  };
  if (Object.defineProperty(e, "currentTarget", {
    configurable: !0,
    get() {
      return t || document;
    }
  }), m.registry && !m.done && (m.done = _$HY.done = !0), e.composedPath) {
    const c = e.composedPath();
    o(c[0]);
    for (let a = 0; a < c.length - 2 && (t = c[a], !!i()); a++) {
      if (t._$host) {
        t = t._$host, l();
        break;
      }
      if (t.parentNode === s)
        break;
    }
  } else l();
  o(r);
}
function ee(e, t, n, r, s) {
  const o = te(e);
  if (o) {
    !n && (n = [...e.childNodes]);
    let c = [];
    for (let a = 0; a < n.length; a++) {
      const u = n[a];
      u.nodeType === 8 && u.data.slice(0, 2) === "!$" ? u.remove() : c.push(u);
    }
    n = c;
  }
  for (; typeof n == "function"; ) n = n();
  if (t === n) return n;
  const i = typeof t, l = r !== void 0;
  if (e = l && n[0] && n[0].parentNode || e, i === "string" || i === "number") {
    if (o || i === "number" && (t = t.toString(), t === n))
      return n;
    if (l) {
      let c = n[0];
      c && c.nodeType === 3 ? c.data !== t && (c.data = t) : c = document.createTextNode(t), n = G(e, n, r, c);
    } else
      n !== "" && typeof n == "string" ? n = e.firstChild.data = t : n = e.textContent = t;
  } else if (t == null || i === "boolean") {
    if (o) return n;
    n = G(e, n, r);
  } else {
    if (i === "function")
      return F(() => {
        let c = t();
        for (; typeof c == "function"; ) c = c();
        n = ee(e, c, n, r);
      }), () => n;
    if (Array.isArray(t)) {
      const c = [], a = n && Array.isArray(n);
      if (Ce(c, t, n, s))
        return F(() => n = ee(e, c, n, r, !0)), () => n;
      if (o) {
        if (!c.length) return n;
        if (r === void 0) return n = [...e.childNodes];
        let u = c[0];
        if (u.parentNode !== e) return n;
        const f = [u];
        for (; (u = u.nextSibling) !== r; ) f.push(u);
        return n = f;
      }
      if (c.length === 0) {
        if (n = G(e, n, r), l) return n;
      } else a ? n.length === 0 ? De(e, c, r) : Ft(e, n, c) : (n && G(e), De(e, c));
      n = c;
    } else if (t.nodeType) {
      if (o && t.parentNode) return n = l ? [t] : t;
      if (Array.isArray(n)) {
        if (l) return n = G(e, n, r, t);
        G(e, n, null, t);
      } else n == null || n === "" || !e.firstChild ? e.appendChild(t) : e.replaceChild(t, e.firstChild);
      n = t;
    }
  }
  return n;
}
function Ce(e, t, n, r) {
  let s = !1;
  for (let o = 0, i = t.length; o < i; o++) {
    let l = t[o], c = n && n[e.length], a;
    if (!(l == null || l === !0 || l === !1)) if ((a = typeof l) == "object" && l.nodeType)
      e.push(l);
    else if (Array.isArray(l))
      s = Ce(e, l, c) || s;
    else if (a === "function")
      if (r) {
        for (; typeof l == "function"; ) l = l();
        s = Ce(
          e,
          Array.isArray(l) ? l : [l],
          Array.isArray(c) ? c : [c]
        ) || s;
      } else
        e.push(l), s = !0;
    else {
      const u = String(l);
      c && c.nodeType === 3 && c.data === u ? e.push(c) : e.push(document.createTextNode(u));
    }
  }
  return s;
}
function De(e, t, n = null) {
  for (let r = 0, s = t.length; r < s; r++) e.insertBefore(t[r], n);
}
function G(e, t, n, r) {
  if (n === void 0) return e.textContent = "";
  const s = r || document.createTextNode("");
  if (t.length) {
    let o = !1;
    for (let i = t.length - 1; i >= 0; i--) {
      const l = t[i];
      if (s !== l) {
        const c = l.parentNode === e;
        !o && !i ? c ? e.replaceChild(s, l) : e.insertBefore(s, n) : c && l.remove();
      } else o = !0;
    }
  } else e.insertBefore(s, n);
  return [s];
}
const Jt = !1;
function rt() {
  let e = /* @__PURE__ */ new Set();
  function t(s) {
    return e.add(s), () => e.delete(s);
  }
  let n = !1;
  function r(s, o) {
    if (n)
      return !(n = !1);
    const i = {
      to: s,
      options: o,
      defaultPrevented: !1,
      preventDefault: () => i.defaultPrevented = !0
    };
    for (const l of e)
      l.listener({
        ...i,
        from: l.location,
        retry: (c) => {
          c && (n = !0), l.navigate(s, { ...o, resolve: !1 });
        }
      });
    return !i.defaultPrevented;
  }
  return {
    subscribe: t,
    confirm: r
  };
}
let Ee;
function Te() {
  (!window.history.state || window.history.state._depth == null) && window.history.replaceState({ ...window.history.state, _depth: window.history.length - 1 }, ""), Ee = window.history.state._depth;
}
Te();
function Qt(e) {
  return {
    ...e,
    _depth: window.history.state && window.history.state._depth
  };
}
function Zt(e, t) {
  let n = !1;
  return () => {
    const r = Ee;
    Te();
    const s = r == null ? null : Ee - r;
    if (n) {
      n = !1;
      return;
    }
    s && t(s) ? (n = !0, window.history.go(-s)) : e();
  };
}
const en = /^(?:[a-z0-9]+:)?\/\//i, tn = /^\/+|(\/)\/+$/g, st = "http://sr";
function H(e, t = !1) {
  const n = e.replace(tn, "$1");
  return n ? t || /^[?#]/.test(n) ? n : "/" + n : "";
}
function se(e, t, n) {
  if (en.test(t))
    return;
  const r = H(e), s = n && H(n);
  let o = "";
  return !s || t.startsWith("/") ? o = r : s.toLowerCase().indexOf(r.toLowerCase()) !== 0 ? o = r + s : o = s, (o || "/") + H(t, !o);
}
function nn(e, t) {
  if (e == null)
    throw new Error(t);
  return e;
}
function rn(e, t) {
  return H(e).replace(/\/*(\*.*)?$/g, "") + H(t);
}
function ot(e) {
  const t = {};
  return e.searchParams.forEach((n, r) => {
    r in t ? Array.isArray(t[r]) ? t[r].push(n) : t[r] = [t[r], n] : t[r] = n;
  }), t;
}
function sn(e, t, n) {
  const [r, s] = e.split("/*", 2), o = r.split("/").filter(Boolean), i = o.length;
  return (l) => {
    const c = l.split("/").filter(Boolean), a = c.length - i;
    if (a < 0 || a > 0 && s === void 0 && !t)
      return null;
    const u = {
      path: i ? "" : "/",
      params: {}
    }, f = (d) => n === void 0 ? void 0 : n[d];
    for (let d = 0; d < i; d++) {
      const g = o[d], P = g[0] === ":", h = P ? c[d] : c[d].toLowerCase(), p = P ? g.slice(1) : g.toLowerCase();
      if (P && we(h, f(p)))
        u.params[p] = h;
      else if (P || !we(h, p))
        return null;
      u.path += `/${h}`;
    }
    if (s) {
      const d = a ? c.slice(-a).join("/") : "";
      if (we(d, f(s)))
        u.params[s] = d;
      else
        return null;
    }
    return u;
  };
}
function we(e, t) {
  const n = (r) => r === e;
  return t === void 0 ? !0 : typeof t == "string" ? n(t) : typeof t == "function" ? t(e) : Array.isArray(t) ? t.some(n) : t instanceof RegExp ? t.test(e) : !1;
}
function on(e) {
  const [t, n] = e.pattern.split("/*", 2), r = t.split("/").filter(Boolean);
  return r.reduce((s, o) => s + (o.startsWith(":") ? 2 : 3), r.length - (n === void 0 ? 0 : 1));
}
function it(e) {
  const t = /* @__PURE__ */ new Map(), n = Ke();
  return new Proxy({}, {
    get(r, s) {
      return t.has(s) || Ge(n, () => t.set(s, x(() => e()[s]))), t.get(s)();
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
function lt(e) {
  let t = /(\/?\:[^\/]+)\?/.exec(e);
  if (!t)
    return [e];
  let n = e.slice(0, t.index), r = e.slice(t.index + t[0].length);
  const s = [n, n += t[1]];
  for (; t = /^(\/\:[^\/]+)\?/.exec(r); )
    s.push(n += t[1]), r = r.slice(t[0].length);
  return lt(r).reduce((o, i) => [...o, ...s.map((l) => l + i)], []);
}
const ln = 100, ct = Xe(), Ne = Xe(), de = () => nn(Oe(ct), "<A> and 'use' router primitives can be only used inside a Route."), cn = () => Oe(Ne) || de().base, an = (e) => {
  const t = cn();
  return x(() => t.resolvePath(e()));
}, un = (e) => {
  const t = de();
  return x(() => {
    const n = e();
    return n !== void 0 ? t.renderPath(n) : n;
  });
}, at = () => de().location, fn = () => de().isRouting;
function dn(e, t = "") {
  const { component: n, preload: r, load: s, children: o, info: i } = e, l = !o || Array.isArray(o) && !o.length, c = {
    key: e,
    component: n,
    preload: r || s,
    info: i
  };
  return ut(e.path).reduce((a, u) => {
    for (const f of lt(u)) {
      const d = rn(t, f);
      let g = l ? d : d.split("/*", 1)[0];
      g = g.split("/").map((P) => P.startsWith(":") || P.startsWith("*") ? P : encodeURIComponent(P)).join("/"), a.push({
        ...c,
        originalPath: u,
        pattern: g,
        matcher: sn(g, !l, e.matchFilters)
      });
    }
    return a;
  }, []);
}
function hn(e, t = 0) {
  return {
    routes: e,
    score: on(e[e.length - 1]) * 1e4 - t,
    matcher(n) {
      const r = [];
      for (let s = e.length - 1; s >= 0; s--) {
        const o = e[s], i = o.matcher(n);
        if (!i)
          return null;
        r.unshift({
          ...i,
          route: o
        });
      }
      return r;
    }
  };
}
function ut(e) {
  return Array.isArray(e) ? e : [e];
}
function ft(e, t = "", n = [], r = []) {
  const s = ut(e);
  for (let o = 0, i = s.length; o < i; o++) {
    const l = s[o];
    if (l && typeof l == "object") {
      l.hasOwnProperty("path") || (l.path = "");
      const c = dn(l, t);
      for (const a of c) {
        n.push(a);
        const u = Array.isArray(l.children) && l.children.length === 0;
        if (l.children && !u)
          ft(l.children, a.pattern, n, r);
        else {
          const f = hn([...n], r.length);
          r.push(f);
        }
        n.pop();
      }
    }
  }
  return n.length ? r : r.sort((o, i) => i.score - o.score);
}
function be(e, t) {
  for (let n = 0, r = e.length; n < r; n++) {
    const s = e[n].matcher(t);
    if (s)
      return s;
  }
  return [];
}
function gn(e, t, n) {
  const r = new URL(st), s = x((u) => {
    const f = e();
    try {
      return new URL(f, r);
    } catch {
      return console.error(`Invalid path ${f}`), u;
    }
  }, r, {
    equals: (u, f) => u.href === f.href
  }), o = x(() => s().pathname), i = x(() => s().search, !0), l = x(() => s().hash), c = () => "", a = Le(i, () => ot(s()));
  return {
    get pathname() {
      return o();
    },
    get search() {
      return i();
    },
    get hash() {
      return l();
    },
    get state() {
      return t();
    },
    get key() {
      return c();
    },
    query: n ? n(a) : it(a)
  };
}
let V;
function mn() {
  return V;
}
function pn(e, t, n, r = {}) {
  const { signal: [s, o], utils: i = {} } = e, l = i.parsePath || ((b) => b), c = i.renderPath || ((b) => b), a = i.beforeLeave || rt(), u = se("", r.base || "");
  if (u === void 0)
    throw new Error(`${u} is not a valid base path`);
  u && !s().value && o({ value: u, replace: !0, scroll: !1 });
  const [f, d] = _(!1);
  let g;
  const P = (b, A) => {
    A.value === h() && A.state === S() || (g === void 0 && d(!0), V = b, g = A, St(() => {
      g === A && (p(g.value), v(g.state), $[1]((N) => N.filter((q) => q.pending)));
    }).finally(() => {
      g === A && Pt(() => {
        V = void 0, b === "navigate" && B(g), d(!1), g = void 0;
      });
    }));
  }, [h, p] = _(s().value), [S, v] = _(s().state), j = gn(h, S, i.queryWrapper), T = [], $ = _([]), M = x(() => typeof r.transformUrl == "function" ? be(t(), r.transformUrl(j.pathname)) : be(t(), j.pathname)), W = () => {
    const b = M(), A = {};
    for (let N = 0; N < b.length; N++)
      Object.assign(A, b[N].params);
    return A;
  }, Y = i.paramsWrapper ? i.paramsWrapper(W, t) : it(W), L = {
    pattern: u,
    path: () => u,
    outlet: () => null,
    resolvePath(b) {
      return se(u, b);
    }
  };
  return F(Le(s, (b) => P("native", b), { defer: !0 })), {
    base: L,
    location: j,
    params: Y,
    isRouting: f,
    renderPath: c,
    parsePath: l,
    navigatorFactory: E,
    matches: M,
    beforeLeave: a,
    preloadRoute: mt,
    singleFlight: r.singleFlight === void 0 ? !0 : r.singleFlight,
    submissions: $
  };
  function C(b, A, N) {
    k(() => {
      if (typeof A == "number") {
        A && (i.go ? i.go(A) : console.warn("Router integration does not support relative routing"));
        return;
      }
      const q = !A || A[0] === "?", { replace: he, resolve: K, scroll: ge, state: J } = {
        replace: !1,
        resolve: !q,
        scroll: !0,
        ...N
      }, ne = K ? b.resolvePath(A) : se(q && j.pathname || "", A);
      if (ne === void 0)
        throw new Error(`Path '${A}' is not a routable path`);
      if (T.length >= ln)
        throw new Error("Too many redirects");
      const ke = h();
      (ne !== ke || J !== S()) && (Jt || a.confirm(ne, N) && (T.push({ value: ke, replace: he, scroll: ge, state: S() }), P("navigate", {
        value: ne,
        state: J
      })));
    });
  }
  function E(b) {
    return b = b || Oe(Ne) || L, (A, N) => C(b, A, N);
  }
  function B(b) {
    const A = T[0];
    A && (o({
      ...b,
      replace: A.replace,
      scroll: A.scroll
    }), T.length = 0);
  }
  function mt(b, A) {
    const N = be(t(), b.pathname), q = V;
    V = "preload";
    for (let he in N) {
      const { route: K, params: ge } = N[he];
      K.component && K.component.preload && K.component.preload();
      const { preload: J } = K;
      A && J && Ge(n(), () => J({
        params: ge,
        location: {
          pathname: b.pathname,
          search: b.search,
          hash: b.hash,
          query: ot(b),
          state: null,
          key: ""
        },
        intent: "preload"
      }));
    }
    V = q;
  }
}
function yn(e, t, n, r) {
  const { base: s, location: o, params: i } = e, { pattern: l, component: c, preload: a } = r().route, u = x(() => r().path);
  c && c.preload && c.preload();
  const f = a ? a({ params: i, location: o, intent: V || "initial" }) : void 0;
  return {
    parent: t,
    pattern: l,
    path: u,
    outlet: () => c ? R(c, {
      params: i,
      location: o,
      data: f,
      get children() {
        return n();
      }
    }) : n(),
    resolvePath(g) {
      return se(s.path(), g, u());
    }
  };
}
const wn = (e) => (t) => {
  const {
    base: n
  } = t, r = ze(() => t.children), s = x(() => ft(r(), t.base || ""));
  let o;
  const i = pn(e, s, () => o, {
    base: n,
    singleFlight: t.singleFlight,
    transformUrl: t.transformUrl
  });
  return e.create && e.create(i), R(ct.Provider, {
    value: i,
    get children() {
      return R(bn, {
        routerState: i,
        get root() {
          return t.root;
        },
        get preload() {
          return t.rootPreload || t.rootLoad;
        },
        get children() {
          return [x(() => (o = Ke()) && null), R(vn, {
            routerState: i,
            get branches() {
              return s();
            }
          })];
        }
      });
    }
  });
};
function bn(e) {
  const t = e.routerState.location, n = e.routerState.params, r = x(() => e.preload && k(() => {
    e.preload({
      params: n,
      location: t,
      intent: mn() || "initial"
    });
  }));
  return R(et, {
    get when() {
      return e.root;
    },
    keyed: !0,
    get fallback() {
      return e.children;
    },
    children: (s) => R(s, {
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
function vn(e) {
  const t = [];
  let n;
  const r = x(Le(e.routerState.matches, (s, o, i) => {
    let l = o && s.length === o.length;
    const c = [];
    for (let a = 0, u = s.length; a < u; a++) {
      const f = o && o[a], d = s[a];
      i && f && d.route.key === f.route.key ? c[a] = i[a] : (l = !1, t[a] && t[a](), We((g) => {
        t[a] = g, c[a] = yn(e.routerState, c[a - 1] || e.routerState.base, Be(() => r()[a + 1]), () => e.routerState.matches()[a]);
      }));
    }
    return t.splice(s.length).forEach((a) => a()), i && l ? i : (n = c[0], c);
  }));
  return Be(() => r() && n)();
}
const Be = (e) => () => R(et, {
  get when() {
    return e();
  },
  keyed: !0,
  children: (t) => R(Ne.Provider, {
    value: t,
    get children() {
      return t.outlet();
    }
  })
});
function An([e, t], n, r) {
  return [e, r ? (s) => t(r(s)) : t];
}
function Pn(e) {
  let t = !1;
  const n = (s) => typeof s == "string" ? { value: s } : s, r = An(_(n(e.get()), {
    equals: (s, o) => s.value === o.value && s.state === o.state
  }), void 0, (s) => (!t && e.set(s), m.registry && !m.done && (m.done = !0), s));
  return e.init && qe(e.init((s = e.get()) => {
    t = !0, r[1](n(s)), t = !1;
  })), wn({
    signal: r,
    create: e.create,
    utils: e.utils
  });
}
function Sn(e, t, n) {
  return e.addEventListener(t, n), () => e.removeEventListener(t, n);
}
function xn(e, t) {
  const n = e && document.getElementById(e);
  n ? n.scrollIntoView() : t && window.scrollTo(0, 0);
}
const Cn = /* @__PURE__ */ new Map();
function En(e = !0, t = !1, n = "/_server", r) {
  return (s) => {
    const o = s.base.path(), i = s.navigatorFactory(s.base);
    let l, c;
    function a(h) {
      return h.namespaceURI === "http://www.w3.org/2000/svg";
    }
    function u(h) {
      if (h.defaultPrevented || h.button !== 0 || h.metaKey || h.altKey || h.ctrlKey || h.shiftKey)
        return;
      const p = h.composedPath().find((M) => M instanceof Node && M.nodeName.toUpperCase() === "A");
      if (!p || t && !p.hasAttribute("link"))
        return;
      const S = a(p), v = S ? p.href.baseVal : p.href;
      if ((S ? p.target.baseVal : p.target) || !v && !p.hasAttribute("state"))
        return;
      const T = (p.getAttribute("rel") || "").split(/\s+/);
      if (p.hasAttribute("download") || T && T.includes("external"))
        return;
      const $ = S ? new URL(v, document.baseURI) : new URL(v);
      if (!($.origin !== window.location.origin || o && $.pathname && !$.pathname.toLowerCase().startsWith(o.toLowerCase())))
        return [p, $];
    }
    function f(h) {
      const p = u(h);
      if (!p)
        return;
      const [S, v] = p, j = s.parsePath(v.pathname + v.search + v.hash), T = S.getAttribute("state");
      h.preventDefault(), i(j, {
        resolve: !1,
        replace: S.hasAttribute("replace"),
        scroll: !S.hasAttribute("noscroll"),
        state: T ? JSON.parse(T) : void 0
      });
    }
    function d(h) {
      const p = u(h);
      if (!p)
        return;
      const [S, v] = p;
      r && (v.pathname = r(v.pathname)), s.preloadRoute(v, S.getAttribute("preload") !== "false");
    }
    function g(h) {
      clearTimeout(l);
      const p = u(h);
      if (!p)
        return c = null;
      const [S, v] = p;
      c !== S && (r && (v.pathname = r(v.pathname)), l = setTimeout(() => {
        s.preloadRoute(v, S.getAttribute("preload") !== "false"), c = S;
      }, 20));
    }
    function P(h) {
      if (h.defaultPrevented)
        return;
      let p = h.submitter && h.submitter.hasAttribute("formaction") ? h.submitter.getAttribute("formaction") : h.target.getAttribute("action");
      if (!p)
        return;
      if (!p.startsWith("https://action/")) {
        const v = new URL(p, st);
        if (p = s.parsePath(v.pathname + v.search), !p.startsWith(n))
          return;
      }
      if (h.target.method.toUpperCase() !== "POST")
        throw new Error("Only POST forms are supported for Actions");
      const S = Cn.get(p);
      if (S) {
        h.preventDefault();
        const v = new FormData(h.target, h.submitter);
        S.call({ r: s, f: h.target }, h.target.enctype === "multipart/form-data" ? v : new URLSearchParams(v));
      }
    }
    nt(["click", "submit"]), document.addEventListener("click", f), e && (document.addEventListener("mousemove", g, { passive: !0 }), document.addEventListener("focusin", d, { passive: !0 }), document.addEventListener("touchstart", d, { passive: !0 })), document.addEventListener("submit", P), qe(() => {
      document.removeEventListener("click", f), e && (document.removeEventListener("mousemove", g), document.removeEventListener("focusin", d), document.removeEventListener("touchstart", d)), document.removeEventListener("submit", P);
    });
  };
}
function dt(e) {
  const t = () => {
    const r = window.location.pathname.replace(/^\/+/, "/") + window.location.search, s = window.history.state && window.history.state._depth && Object.keys(window.history.state).length === 1 ? void 0 : window.history.state;
    return {
      value: r + window.location.hash,
      state: s
    };
  }, n = rt();
  return Pn({
    get: t,
    set({ value: r, replace: s, scroll: o, state: i }) {
      s ? window.history.replaceState(Qt(i), "", r) : window.history.pushState(i, "", r), xn(decodeURIComponent(window.location.hash.slice(1)), o), Te();
    },
    init: (r) => Sn(window, "popstate", Zt(r, (s) => {
      if (s && s < 0)
        return !n.confirm(s);
      {
        const o = t();
        return !n.confirm(o.value, { state: o.state });
      }
    })),
    create: En(e.preload, e.explicitLinks, e.actionBase, e.transformUrl),
    utils: {
      go: (r) => window.history.go(r),
      beforeLeave: n
    }
  })(e);
}
var Ln = /* @__PURE__ */ z("<a>");
function On(e) {
  e = Se({
    inactiveClass: "inactive",
    activeClass: "active"
  }, e);
  const [, t] = $t(e, ["href", "state", "class", "activeClass", "inactiveClass", "end"]), n = an(() => e.href), r = un(n), s = at(), o = x(() => {
    const i = n();
    if (i === void 0) return [!1, !1];
    const l = H(i.split(/[?#]/, 1)[0]).toLowerCase(), c = decodeURI(H(s.pathname).toLowerCase());
    return [e.end ? l === c : c.startsWith(l + "/") || c === l, l === c];
  });
  return (() => {
    var i = Ln();
    return Kt(i, Se(t, {
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
    }), !1), i;
  })();
}
const ht = (e) => R(On, Se(e, {
  "data-enhance-nav": "false"
}));
var Rn = /* @__PURE__ */ z('<div><a href=/>Home</a><button class="btn btn-primary">Click!</button><ul><li>');
const $n = () => (() => {
  var e = Rn(), t = e.firstChild, n = t.nextSibling, r = n.nextSibling, s = r.firstChild;
  return $e(s, R(ht, {
    href: "/about",
    class: "btn btn-link",
    children: "About"
  })), e;
})();
var Tn = /* @__PURE__ */ z("<div>");
const Nn = () => (() => {
  var e = Tn();
  return $e(e, R(ht, {
    href: "/",
    class: "btn btn-link",
    children: "Home"
  })), e;
})();
var kn = /* @__PURE__ */ z("<h1>Hello world!");
const gt = () => kn(), _n = /* @__PURE__ */ Object.freeze(/* @__PURE__ */ Object.defineProperty({
  __proto__: null,
  default: gt
}, Symbol.toStringTag, { value: "Module" })), jn = [{
  path: "/",
  component: $n
}, {
  path: "/about",
  component: Nn
}, {
  path: "/Tasks",
  children: [{
    path: "/",
    component: gt
  }]
}], In = () => R(dt, {
  base: "/app",
  children: jn
}), Un = (e) => {
  const t = at();
  return fn(), bt(() => {
    document.querySelectorAll("a[data-enhance-nav=false][data-not-solid]").forEach((r) => {
      const s = r, o = s.dataset.activeClass || "active", i = new URL(s.href);
      t.pathname.startsWith(i.pathname) ? s.classList.add(o) : s.classList.remove(o);
    });
  }), e.children;
};
var Dn = /* @__PURE__ */ z("<h1>Demo1"), Bn = /* @__PURE__ */ z("<h1>Demo2");
const Fn = [{
  path: "/",
  component: Tt(() => Promise.resolve().then(() => _n))
}, {
  path: "/Demo1",
  component: () => Dn()
}, {
  path: "/Demo2",
  component: () => Bn()
}], Mn = "/app/Tasks", Vn = () => R(dt, {
  base: Mn,
  root: Un,
  children: Fn
}), qn = (e) => {
  console.debug("render app [Tasks]", e), tt(() => R(Vn, {}), e);
}, Kn = (e) => tt(() => R(In, {}), e);
export {
  Vn as AppTasks,
  Mn as AppTasksRoutePrefix,
  Fn as AppTasksRoutes,
  Kn as renderApp,
  qn as renderAppTasks
};

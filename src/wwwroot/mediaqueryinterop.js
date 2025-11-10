export class MediaQueryInterop {
    /** Map<string elementId, { listeners: Array<{ mql: MediaQueryList, handler: Function, query: string }> }> */
    listeners = new Map();

    /** Map<string elementId, MutationObserver> */
    observers = new Map();

    addMediaQueryListener(dotNetHelper, elementId, query) {
        const mql = window.matchMedia(query);

        const handler = (event) => {
            try {
                dotNetHelper.invokeMethodAsync('UpdateMatches', event.matches);
            } catch {
                // If circuit is gone, swallow; cleanup will happen via remove/destroy.
            }
        };

        // init bucket
        if (!this.listeners.has(elementId)) {
            this.listeners.set(elementId, { listeners: [] });
        } else {
            // prevent duplicate same-query listener
            const { listeners } = this.listeners.get(elementId);
            if (listeners.some(l => l.query === query)) {
                return; // already wired for this query
            }
        }

        mql.addEventListener?.('change', handler);
        // (Older Safari fallback)
        if (!mql.addEventListener && mql.addListener)
            mql.addListener(handler);

        this.listeners.get(elementId).listeners.push({ mql, handler, query });

        // push initial state
        try {
            dotNetHelper.invokeMethodAsync('UpdateMatches', mql.matches);
        } catch { /* ignore */ }
    }

    removeMediaQueryListener(elementId) {
        const entry = this.listeners.get(elementId);
        if (!entry) return;

        for (const { mql, handler } of entry.listeners) {
            mql.removeEventListener?.('change', handler);
            if (!mql.removeEventListener && mql.removeListener) mql.removeListener(handler);
        }

        this.listeners.delete(elementId);

        // also stop and delete an observer if present
        const obs = this.observers.get(elementId);
        if (obs) {
            try { obs.disconnect(); } catch { /* ignore */ }
            this.observers.delete(elementId);
        }
    }

    isMediaQueryMatched(query) {
        return window.matchMedia(query).matches;
    }

    createObserver(elementId) {
        if (this.observers.has(elementId)) return; // already observing

        const target = document.getElementById(elementId);
        if (!target || !target.parentNode) return; // nothing to observe yet

        const obs = new MutationObserver((mutations) => {
            // If the target node disappears anywhere in the observed subtree, clean up.
            const removed = mutations.some(m =>
                Array.from(m.removedNodes).includes(target) ||
                // also handle wholesale subtree replacements:
                (target.isConnected === false)
            );

            if (removed) {
                this.removeMediaQueryListener(elementId);
            }
        });

        try {
            obs.observe(target.parentNode, { childList: true, subtree: true });
            this.observers.set(elementId, obs);
        } catch {
            // Parent may vanish during navigation; just ignore.
        }
    }

    /** Explicit tear-down you can call from DisposeAsync on the .NET side */
    destroy(elementId) {
        this.removeMediaQueryListener(elementId);
    }
}

window.MediaQueryInterop = new MediaQueryInterop();

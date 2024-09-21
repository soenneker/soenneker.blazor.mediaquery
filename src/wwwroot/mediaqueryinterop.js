export class MediaQueryInterop {
    observer;
    listeners = new Map();

    addMediaQueryListener(dotNetHelper, elementId, query) {
        const mediaQueryList = window.matchMedia(query);

        const listener = (event) => {
            dotNetHelper.invokeMethod('UpdateMatches', event.matches);
        };

        // Check if listener is already added
        if (!this.listeners.has(elementId)) {
            this.listeners.set(elementId, { mediaQueryList, listeners: [] });
        } else {
            // Check for existing listener
            const existing = this.listeners.get(elementId).listeners;
            if (existing.some(l => l.query === query)) return; // Already exists
        }

        mediaQueryList.addEventListener('change', listener);
        this.listeners.get(elementId).listeners.push({ listener, query });

        dotNetHelper.invokeMethod('UpdateMatches', mediaQueryList.matches);
    }

    removeMediaQueryListener(elementId) {
        if (this.listeners.has(elementId)) {
            const { mediaQueryList, listeners } = this.listeners.get(elementId);

            listeners.forEach(({ listener, query }) => {
                mediaQueryList.removeEventListener('change', listener);
            });

            this.listeners.delete(elementId);
        }
    }

    isMediaQueryMatched(query) {
        return window.matchMedia(query).matches;
    }

    createObserver(elementId) {
        const target = document.getElementById(elementId);

        this.observer = new MutationObserver((mutations) => {
            const targetRemoved = mutations.some((mutation) => {
                return Array.from(mutation.removedNodes).includes(target);
            });

            if (targetRemoved) {
                this.removeMediaQueryListener(elementId);
                this.observer.disconnect();
                delete this.observer;
            }
        });

        this.observer.observe(target.parentNode, { childList: true });
    }
}

window.MediaQueryInterop = new MediaQueryInterop();
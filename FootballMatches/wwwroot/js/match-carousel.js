class MatchCarousel extends HTMLElement {
    constructor() {
        super();
        this.attachShadow({ mode: "open" });

        const template = document.createElement("template");
        template.innerHTML = `
            <div class="carousel">
                <div class="carousel-track">
                    <slot></slot>
                </div>
            </div>
        `;

        this.shadowRoot.append(template.content.cloneNode(true));

        this.track = this.shadowRoot.querySelector(".carousel-track");
        this.items = [];
        this.startX = 0;
        this.currentX = 0;
        this.isDragging = false;
        this.currentOffset = 0;
        this.maxOffset = 0;

        this.resizeObserver = new ResizeObserver(() => this.updateBoundaries());
    }

    connectedCallback() {
        const linkElem = document.createElement("link");
        linkElem.setAttribute("rel", "stylesheet");
        linkElem.setAttribute("href", "/css/carousel.css");

        this.shadowRoot.appendChild(linkElem);

        this.items = Array.from(this.querySelectorAll("match-card"));
        this.items.forEach(item => this.resizeObserver.observe(item));

        this.track.addEventListener("mousedown", this.startDrag.bind(this));
        this.track.addEventListener("mousemove", this.drag.bind(this));
        this.track.addEventListener("mouseup", this.endDrag.bind(this));
        this.track.addEventListener("mouseleave", this.endDrag.bind(this));
        this.track.addEventListener("touchstart", this.startDrag.bind(this));
        this.track.addEventListener("touchmove", this.drag.bind(this));
        this.track.addEventListener("touchend", this.endDrag.bind(this));

        window.addEventListener("resize", this.updateBoundaries.bind(this));

        this.updateBoundaries();
    }

    updateBoundaries() {
        this.maxOffset = null;
        this.currentOffset = 0;
        this.track.style.transform = `translateX(${this.currentOffset}%)`;
    }

    startDrag(event) {
        this.isDragging = true;
        this.startX = event.touches ? event.touches[0].clientX : event.clientX;
        this.track.style.transition = "none";
    }

    drag(event) {
        if (!this.isDragging) return;

        this.calculateMaxOffset();

        const currentX = event.touches ? event.touches[0].clientX : event.clientX;
        const delta = currentX - this.startX;

        const deltaPercent = (delta / this.track.offsetParent.clientWidth) * 100;
        let newOffset = this.currentOffset + deltaPercent;

        let reseted = false;
        if (newOffset > 0) {
            newOffset = 0;
            reseted = true;
        } else if (newOffset < this.maxOffset) {
            newOffset = this.maxOffset;
            reseted = true;
        }

        this.currentOffset = newOffset;
        this.track.style.transform = `translateX(${this.currentOffset}%)`;

        if (!reseted) this.startX = currentX;
    }

    calculateMaxOffset() {
        if (this.maxOffset != null) return;

        const containerWidth = this.track.offsetParent.clientWidth;
        const gap = parseFloat(window.getComputedStyle(this.track).gap) || 0;
        const totalItemsWidth =
            this.items.reduce((acc, item) => acc + item.clientWidth, 0) +
            (this.items.length - 1) * gap;

        if (totalItemsWidth < containerWidth) this.maxOffset = 0;
        else
            this.maxOffset =
                -((totalItemsWidth - containerWidth) / containerWidth) * 100;

        this.currentOffset = 0;
        this.track.style.transform = `translateX(${this.currentOffset}%)`;
    }

    endDrag(event) {
        if (!this.isDragging) return;
        this.isDragging = false;
        this.track.style.transition = "transform 0.3s ease"; // Smooth transition
    }

    disconnectedCallback() {
        this.items.forEach(item => this.resizeObserver.unobserve(item));
        this.resizeObserver.disconnect();
    }
}

customElements.define("match-carousel", MatchCarousel);

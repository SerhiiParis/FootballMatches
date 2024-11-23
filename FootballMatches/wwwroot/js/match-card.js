class MatchCard extends HTMLElement {
    constructor() {
        super();
        this.attachShadow({ mode: "open" });

        const template = document.createElement("template");
        template.innerHTML = `
            <div class="card">
                <div class="card-header">
                    <span class="match-time">
                        <span class="icon">▶</span> <span id="matchTime"></span>
                    </span>
                </div>
                <div class="card-content" id="matchTeams"></div>
                <div class="card-odds">
                    <div class="odds-box">
                        <span>1</span>
                        <div class="value" id="odds1"></div>
                    </div>
                    <div class="odds-box">
                        <span>X</span>
                        <div class="value" id="oddsX"></div>
                    </div>
                    <div class="odds-box">
                        <span>2</span>
                        <div class="value" id="odds2"></div>
                    </div>
                </div>
            </div>
        `;

        this.shadowRoot.append(template.content.cloneNode(true));
    }

    async connectedCallback() {
        const cssURL = "/css/match-card.css";
        const cssContent = await fetch(cssURL).then((res) => res.text());
        const style = document.createElement("style");
        style.textContent = cssContent;

        this.shadowRoot.appendChild(style);

        this.shadowRoot.getElementById("matchTime").textContent = this.getAttribute("match-time");
        this.shadowRoot.getElementById("matchTeams").textContent = this.getAttribute("match-teams");
        this.shadowRoot.getElementById("odds1").textContent = this.getAttribute("odds1");
        this.shadowRoot.getElementById("oddsX").textContent = this.getAttribute("oddsX");
        this.shadowRoot.getElementById("odds2").textContent = this.getAttribute("odds2");
    }
}

customElements.define("match-card", MatchCard);

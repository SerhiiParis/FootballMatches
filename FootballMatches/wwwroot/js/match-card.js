class MatchCard extends HTMLElement {
    constructor() {
        super();
        this.attachShadow({ mode: "open" });

        const template = document.createElement("template");
        template.innerHTML = `
            <div class="card">
                <div class="card-header">
                    <span class="match-time">
                        <span id="matchDateTime"></span>
                    </span>
                </div>
                <div class="card-content">
                    <div class="team-row">
                        <img class="team-crest" id="team1Crest" alt="Team 1 Crest">
                        <span id="team1"></span>
                    </div>
                    <div class="team-row">
                        <img class="team-crest" id="team2Crest" alt="Team 2 Crest">
                        <span id="team2"></span>
                    </div>
                </div>
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
        const cssURL = "/css/match.css";
        const cssContent = await fetch(cssURL).then((res) => res.text());
        const style = document.createElement("style");
        style.textContent = cssContent;

        this.shadowRoot.appendChild(style);

        const datetimeRaw = this.getAttribute("datetime");
        const datetime = new Date(datetimeRaw);

        const day = String(datetime.getDate()).padStart(2, "0");
        const month = String(datetime.getMonth() + 1).padStart(2, "0");
        const hours = String(datetime.getHours()).padStart(2, "0");
        const minutes = String(datetime.getMinutes()).padStart(2, "0");

        const formattedDateTime = `${day}.${month} ${hours}:${minutes}`;

        this.shadowRoot.getElementById("matchDateTime").textContent = formattedDateTime;
        this.shadowRoot.getElementById("team1").textContent = this.getAttribute("team1");
        this.shadowRoot.getElementById("team2").textContent = this.getAttribute("team2");
        this.shadowRoot.getElementById("team1Crest").src = this.getAttribute("team1-crest");
        this.shadowRoot.getElementById("team2Crest").src = this.getAttribute("team2-crest");
    }
}

customElements.define("match-card", MatchCard);

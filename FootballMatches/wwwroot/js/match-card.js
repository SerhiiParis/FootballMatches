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
                        <img id="team1CrestUrl" class="team-crest hidden" alt="Team 1 Crest" loading="lazy" />
                        <span id="team1"></span>
                    </div>
                    <div class="team-row">
                        <img id="team2CrestUrl" class="team-crest hidden" alt="Team 2 Crest" loading="lazy" />
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

        const style = document.createElement("style");
        style.textContent = `
            .card {
                display: flex;
                flex-direction: column;
                justify-content: space-between;
                background: linear-gradient(135deg, #FF6B6B, #C21010);
                border-radius: 12px;
                color: white;
                padding: 16px;
                height: 160px;
                width: 280px;
                box-shadow: 0 6px 12px rgba(0, 0, 0, 0.15);
                font-family: 'Inter', sans-serif;
            }

            .card-header {
                display: flex;
                justify-content: space-between;
                align-items: center;
                font-size: 14px;
                color: rgba(255, 255, 255, 0.85);
                letter-spacing: 0.5px;
            }

            .card-content {
                margin-top: 8px;
                flex: 1;
                display: flex;
                flex-direction: column;
                justify-content: center;
                gap: 12px;
            }

            .team-row {
                display: flex;
                align-items: center;
                font-size: 16px;
                font-weight: 600;
                text-align: left;
                color: white;
                line-height: 1.2;
                gap: 8px;
            }

            .team-crest {
                height: 24px;
                width: 24px;
                object-fit: cover;
                transition: opacity 0.3s ease;
            }

            .team-crest.hidden {
                opacity: 0;
            }

            .card-odds {
                display: flex;
                justify-content: space-between;
                margin-top: 12px;
                gap: 8px;
            }

            .odds-box {
                flex: 1;
                background: rgba(255, 255, 255, 0.9);
                border-radius: 8px;
                padding: 8px;
                text-align: center;
                color: #333;
                font-size: 14px;
                font-weight: 500;
                height: 50px;
                width: 80px;
                box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
                display: flex;
                flex-direction: column;
                justify-content: center;
            }

            .odds-box .value {
                font-size: 16px;
                font-weight: bold;
                color: #228B22;
            }
        `;

        this.shadowRoot.appendChild(style);
        this.shadowRoot.appendChild(template.content.cloneNode(true));
    }

    connectedCallback() {
        const datetimeRaw = this.getAttribute("datetime");
        const datetime = new Date(datetimeRaw);

        const day = String(datetime.getDate()).padStart(2, "0");
        const month = String(datetime.getMonth() + 1).padStart(2, "0");
        const hours = String(datetime.getHours()).padStart(2, "0");
        const minutes = String(datetime.getMinutes()).padStart(2, "0");

        this.shadowRoot.getElementById("matchDateTime").textContent = `${day}.${month} ${hours}:${minutes}`;
        this.shadowRoot.getElementById("team1").textContent = this.getAttribute("team1");
        this.shadowRoot.getElementById("team2").textContent = this.getAttribute("team2");
        this.shadowRoot.getElementById("odds1").textContent = this.getAttribute("odds1");
        this.shadowRoot.getElementById("oddsX").textContent = this.getAttribute("oddsX");
        this.shadowRoot.getElementById("odds2").textContent = this.getAttribute("odds2");

        const team1CrestUrl = this.getAttribute("team1CrestUrl");
        const team2CrestUrl = this.getAttribute("team2CrestUrl");

        this.loadImageAsync('team1CrestUrl', team1CrestUrl);
        this.loadImageAsync('team2CrestUrl', team2CrestUrl);
    }

    loadImageAsync(imageId, imageUrl) {
        const image = this.shadowRoot.getElementById(imageId);

        if (!imageUrl) {
            image.style.display = "none";
            return;
        }

        const tempImage = new Image();
        tempImage.src = imageUrl;
        tempImage.onload = () => {
            image.src = imageUrl;
            image.classList.remove("hidden");
        };
    }
}

customElements.define("match-card", MatchCard);

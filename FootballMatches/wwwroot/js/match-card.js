class MatchCard extends HTMLElement {
    constructor() {
        super();
    }

    connectedCallback() {
        const shadow = this.attachShadow({ mode: "open" });

        const wrapper = document.createElement("div");
        wrapper.setAttribute("class", "card");

        const header = document.createElement("div");
        header.setAttribute("class", "card-header");
        const matchTime = document.createElement("span");
        matchTime.setAttribute("class", "match-time");
        matchTime.textContent = this.formatDateTime(this.getAttribute("datetime"));
        header.appendChild(matchTime);

        const content = document.createElement("div");
        content.setAttribute("class", "card-content");

        const team1Row = this.createTeamRow(
            this.getAttribute("team1")
        );
        const team2Row = this.createTeamRow(
            this.getAttribute("team2")
        );

        const odds = document.createElement("div");
        odds.setAttribute("class", "card-odds");
        odds.appendChild(this.createOddsBox("1", this.getAttribute("odds1")));
        odds.appendChild(this.createOddsBox("X", this.getAttribute("oddsX")));
        odds.appendChild(this.createOddsBox("2", this.getAttribute("odds2")));

        content.appendChild(team1Row);
        content.appendChild(team2Row);

        wrapper.appendChild(header);
        wrapper.appendChild(content);
        wrapper.appendChild(odds);

        const linkElem = document.createElement("link");
        linkElem.setAttribute("rel", "stylesheet");
        linkElem.setAttribute("href", "/css/match.css"); // Path to your CSS file

        shadow.appendChild(linkElem);
        shadow.appendChild(wrapper);
    }

    createTeamRow(teamName) {
        const row = document.createElement("div");
        row.setAttribute("class", "team-row");

        const name = document.createElement("span");
        name.textContent = teamName;

        row.appendChild(name);

        return row;
    }

    createOddsBox(label, value) {
        const box = document.createElement("div");
        box.setAttribute("class", "odds-box");

        const labelSpan = document.createElement("span");
        labelSpan.textContent = label;

        const valueSpan = document.createElement("span");
        valueSpan.setAttribute("class", "value");
        valueSpan.textContent = value;

        box.appendChild(labelSpan);
        box.appendChild(valueSpan);

        return box;
    }

    formatDateTime(datetimeRaw) {
        const datetime = new Date(datetimeRaw);
        const day = String(datetime.getDate()).padStart(2, "0");
        const month = String(datetime.getMonth() + 1).padStart(2, "0");
        const hours = String(datetime.getHours()).padStart(2, "0");
        const minutes = String(datetime.getMinutes()).padStart(2, "0");
        return `${day}.${month} ${hours}:${minutes}`;
    }
}

customElements.define("match-card", MatchCard);

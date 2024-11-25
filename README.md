# FootballMatches Project



## 1. Project Structure

The **FootballMatches** solution contains two primary projects:

- **FootballMatches**: A web application providing an interactive UI for managing and viewing football match data.
- **FootballMatches.Api**: An API designed for mobile app integration and external access to football match data.

<br>

## 2. Hosted Application & Performance Metrics

The application is hosted at:  
[https://footballmatches.azurewebsites.net/](https://footballmatches.azurewebsites.net/)

Performance metrics tested via Lighthouse are available here:  
[View Lighthouse Report](https://lighthouse-metrics.com/lighthouse/checks/226a6984-32d8-46b9-bd55-f95c1fc942b3)

### Lighthouse Performance Screenshots

- **Performance**  
![Mobile Lighthouse Performance](https://drive.google.com/uc?export=view&id=10Xna9Ch5fYKLFhjF03vR-Q12Bym2U91y)

- **Lighthouse Audits**  
![Desktop Lighthouse Performance](https://drive.google.com/uc?export=view&id=1fgpDEYpH8_3kd9_yBolW-hzgcnJRmvxl)

<br>

## 3. Local Setup

Follow these steps to set up and run the project locally:

### 1. Set Environment Variable
Add a local secret for accessing the [Football Data API](https://www.football-data.org/). The key should be set as an environment variable:

```plaintext
DataApi__ApiKey=<Your API Key>
```

Replace `:` with `__` when defining the key as an environment variable.

### 2. Update the SQLite Database
Update/create the SQLite database using Entity Framework (EF) tools. You can do this using your IDE's UI or by running the following CLI command (adjust the paths as necessary):

```bash
dotnet ef database update --project FootballMatches.DataAccess\FootballMatches.DataAccess.csproj --startup-project FootballMatches\FootballMatches.csproj --context FootballMatches.DataAccess.ApplicationDbContext --configuration Debug 20241124012547_Initial
```

The database schema will be applied, and the application should be ready to use.

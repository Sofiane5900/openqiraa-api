# OpenQiraa API

OpenQiraa est un lecteur gratuit pour apprendre l'arabe fuṣḥā en français, en lisant de vrais livres libres de droits.

Pendant la lecture, chaque mot est marqué selon ton niveau : nouveau, en cours d'apprentissage ou déjà connu. Les mots que tu marques vont dans ta banque de mots personnelle, et tes flashcards sont créées à partir des phrases que tu as réellement lues, pas de listes de vocabulaire hors contexte.

## Pourquoi ce projet

Quand on apprend l'arabe standard en français, il faut jongler entre plusieurs outils. Anki révise les mots sans contexte, LingQ est payant et ne gère pas les racines ni les voyelles courtes, d'autres sont en anglais ou limités à quelques textes gratuits. OpenQiraa essaie de regrouper lecture, vocabulaire et révision au même endroit.

## Structure du dépôt

```
.
├── OpenQiraa.sln
├── Directory.Build.props
├── Directory.Packages.props
├── docker-compose.yml
├── .github/
│   └── workflows/
├── src/
│   ├── OpenQiraa.Domain/
│   ├── OpenQiraa.Application/
│   ├── OpenQiraa.Infrastructure/
│   └── OpenQiraa.Api/
└── tests/
    ├── OpenQiraa.UnitTests/
    └── OpenQiraa.IntegrationTests/

```

## Stack

- .NET 10, ASP.NET Core
- Entity Framework Core
- SQL Server en local (Docker), Azure SQL en production
- Docker
- Azure, GitHub Actions pour le déploiement

## Lancer le projet

```bash
dotnet build
```
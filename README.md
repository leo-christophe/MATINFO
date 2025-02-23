# MATINFO

## 📖 Introduction  
MATINFO est un projet de première année de BUT Informatique ayant pour but de gérer le matériel informatique via une interface ergonomique.  
Un utilisateur peut enregistrer un nouveau matériel, l'associer à une catégorie ou une personne.  
L'objectif était de concevoir une interface ergonomique en C# avec le framework WPF.

## 📂 Structure du Projet  
Le projet est composé d'une interface front-end réalisée en WPF et d'un back-end en C# utilisant `Npgsql` pour exécuter des requêtes SQL.  
Étant donné le contexte (1ère année de BUT Informatique), peu de bonnes pratiques ont été appliquées concernant les patterns de programmation.

## ⚠️ Problèmes d'Architecture  
### Requêtes SQL en dur  
- Utilisation du package `Npgsql` avec des requêtes SQL écrites directement en C#.  
- Problèmes engendrés :  
  - ❌ Risques d'injections SQL (même avec des paramètres, ce n'est pas la meilleure approche).  
  - ❌ Maintenance compliquée (requêtes dispersées dans le code, difficile à modifier).  
  - ❌ Absence d'abstraction des accès aux données (pas de repository, pas d'ORM comme Entity Framework).  
  - ❌ Couplage fort entre la logique métier et la base de données.  

### Conséquences  
- Impossible de facilement changer de SGBD.  
- Tests unitaires difficiles (nécessite une base de données pour fonctionner).  
- Code moins lisible et difficile à faire évoluer.  

## 🎨 Ergonomie et UX  
- L'interface est basique et doit être améliorée.  
- Manque de feedback utilisateur (ex : absence de messages d'erreur clairs).  
- Peu d'accessibilité (navigation clavier, contraste, etc.).  

## 🚀 Installation et Exécution  
### A) Compilation du code source  
1. **Prérequis**  
   - Avoir `.NET` et `PostgreSQL` installés sur le PC.  

2. **Configuration de la base de données**  
   - Exécuter le script SQL (présent à la racine du projet) pour créer la base de données.  
   - Créer une base de données nommée **"matinfo"** sur un schéma **"public"**.  
   - Le code est configuré pour accéder à la base de données en local.  

3. **Lancer le projet avec Visual Studio 2022 ou via PowerShell**  
   - Ouvrir `PowerShell`.  
   - Se rendre à la racine du code source (avec `dir` et `cd nom_dossier`).  
   - Exécuter la commande suivante :  
   ```sh
   dotnet run

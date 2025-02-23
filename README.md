# MATINFO

## 📖 Introduction  
Brève description du projet : objectif, contexte (1ère année de BUT Info), et ce qu'il fait.  
MATINFO est un projet de première année de BUT Informatique qui a pour but de gérer le matériel informatique avec une interface ergonomique. Un utilisateur peut enregistrer un nouveau matériel, l'associer à une catégorie ou une personne. Le but était de réaliser l'interface 
la plus ergonomique possible en C# avec le framework WPF.

## 📂 Structure du Projet  
Le projet est composée d'une interface front end réalisée en WPF et d'un back end en C# réalisé avec npgsql permettant d'exécuter des requêtes SQL en C#. Etant donné le contexte (1ère année de BUT Informatique), peu de bonnes pratiques ont été appliqué concernant les
patterns de programmation. 

## ⚠️ Problèmes d'Architecture  
### Requêtes SQL en dur  
- Utilisation du package `Npgsql` avec des requêtes SQL écrites directement en C#.  
- Explication des problèmes :  
  - ❌ Risques d'injections SQL (même si on utilise des paramètres, ce n'est pas la bonne approche).  
  - ❌ Maintenance compliquée (requêtes dispersées dans le code, difficile à modifier).  
  - ❌ Pas d'abstraction des accès aux données (pas de repository, pas d'ORM comme Entity Framework).  
  - ❌ Couplage fort entre la logique métier et la base de données.  

### Ce que ça implique  
- Impossible de facilement changer de SGBD.  
- Tests unitaires difficiles (besoin d'une base de données pour tester).  
- Code moins lisible et difficile à faire évoluer.  

## 🎨 Ergonomie et UX  
- L'interface est basique et doit être améliorée.  
- Manque de feedback utilisateur (ex : pas de messages d'erreur clairs).  
- Peu d'accessibilité (navigation clavier, contraste, etc.).  

## 🚀 Installation et Exécution 
A) En compilant le code source:
1. **Prérequis**
Avoir .NET et PostgreSQL installés sur le PC

2. **Configuration de la base de données**
Exécuter le script SQL (présent à la racine du projet) afin de créer la base de données. Le code est configuré pour accéder à la BD en local.
  
3. **Lancer le projet avec Visual Studio 2022 ou par Powershell**
Ouvrir `Powershell`, se rendre à la racine du code source (à l'aide de `dir` et `cd nomdossier`) puis à la racine, éxécuter la commande:   
   ```sh
   dotnet run
   ```

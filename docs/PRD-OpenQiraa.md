# PRD — Lecteur fuṣḥā (nom de travail : Qira'a)

Nov 19, 2026 · @Sufyan

## 1. Vision

**Qira'a est un lecteur gratuit, en français, pour apprendre l'arabe fuṣḥā en lisant de vrais livres libres.** Chaque mot est marqué selon ton niveau, tout ce que tu marques alimente ta banque de mots, et tes flashcards sont créées à partir des phrases que tu as lues.

**Objectifs**

| Objectif | Indicateur | Cible V1 |
| --- | --- | --- |
| Le produit sert vraiment à son créateur | Jours d'utilisation par semaine | 5 jours sur 7 pendant 3 mois |
| On lit de vrais textes | Passages lus par utilisateur et par semaine | 10 et plus |
| Le vocabulaire grandit | Mots passés au statut « connu » par mois | 150 et plus |
| Le projet démontre un niveau entreprise | Déploiement automatique sur Azure, couverture de tests du Domain, monitoring actif | Pipeline vert, couverture 80 % et plus, monitoring actif |

Le produit a deux finalités assumées : un outil utile pour apprendre l'arabe, et un projet de référence en .NET, Azure et architecture d'entreprise.

## 2. Problème et utilisateurs

Les outils existants obligent à jongler entre plusieurs apps, et aucun n'est pensé pour un francophone qui apprend la fuṣḥā.

| Outil | Ce qui manque |
| --- | --- |
| Anki | Les mots sont révisés hors contexte ; on ne les recroise pas assez dans des textes |
| Sluka, LingQ | Il faut trouver et importer ses textes soi-même ; LingQ est payant ; rien de spécifique à l'arabe (racines, voyelles courtes) |
| Vocabulle | Centré sur le vocabulaire d'un cursus ; pas de lecture libre de livres |
| Nusoos | Payant au-delà de 3 textes par niveau ; en anglais |

**Personas**

| Persona | Profil | Besoin principal |
| --- | --- | --- |
| L'étudiant autonome (persona principal) | Francophone, A1 à B1, étudie seul ou en cours du soir | Lire des textes à son niveau et retenir le vocabulaire sans jongler entre trois apps |
| L'apprenant d'héritage | Comprend un dialecte, ne lit pas la fuṣḥā | Passer à la lecture de l'arabe standard |
| L'administrateur de contenu | Toi, au départ | Importer des livres libres, vérifier l'analyse des mots, publier les passages |

## 3. Périmètre

La V1 couvre la boucle complète : lire → marquer → réviser → voir sa progression. Tout le reste attend.

**Dans la V1**

- Comptes utilisateurs (inscription, connexion, suppression du compte).
- Bibliothèque de livres libres, découpés en passages (les « histoires »).
- Import de livres par un administrateur, avec analyse automatique des mots.
- Lecteur avec marquage des mots en pointillés selon leur statut.
- Dictionnaire arabe-français intégré au lecteur.
- Banque de mots personnelle.
- Flashcards créées depuis la banque, avec révision espacée.
- Tableau de bord de progression.

**Hors V1**

- Journal personnel.
- Import de textes par les utilisateurs (PDF, pages web).
- Audio et synthèse vocale.
- Application mobile native (le site sera responsive).
- Fonctions sociales, classements, partage.
- Toute IA générative.

## 4. Exigences fonctionnelles — lecture

Chaque user story a un identifiant pour pouvoir la reprendre telle quelle dans le backlog (GitHub Issues ou Azure Boards). Priorités : **M** = indispensable en V1, **S** = souhaitable en V1.

### E1 — Authentification et compte

| ID | User story | Critères d'acceptation | Prio |
| --- | --- | --- | --- |
| AUTH-1 | En tant que visiteur, je crée un compte avec mon e-mail et un mot de passe | Mot de passe haché, règles de complexité ; e-mail vérifié par lien ; à la connexion, l'API émet un JWT d'accès signé, valable 15 min | M |
| AUTH-2 | En tant qu'utilisateur, je reste connecté entre deux visites | Refresh token dans un cookie HttpOnly, Secure, SameSite ; rotation à chaque utilisation ; un refresh token déjà utilisé révoque toute la session ; la déconnexion révoque le refresh token | M |
| AUTH-3 | En tant qu'utilisateur, je supprime mon compte et toutes mes données | Suppression définitive de la banque de mots, des flashcards et des statistiques sous 30 jours ; confirmation demandée | M |
| AUTH-4 | En tant qu'administrateur, j'accède à l'espace d'administration | Rôle « Admin » ; les routes d'administration renvoient 403 aux autres rôles | M |
| AUTH-5 | En tant qu'utilisateur, je réinitialise mon mot de passe oublié | Lien à usage unique envoyé par e-mail, valable 1 h ; tous les refresh tokens révoqués après le changement ; même réponse que l'e-mail existe ou non | M |
| AUTH-6 | En tant que propriétaire du service, je protège les routes d'authentification | Verrouillage du compte après 5 échecs pendant 15 min ; limitation de débit sur login, inscription et réinitialisation ; journalisation des connexions échouées | M |
| AUTH-7 | En tant qu'utilisateur, je me connecte avec Google ou Microsoft | Hors V1 : à ajouter ensuite en connexion externe OAuth, qui finit par émettre le même JWT maison | — |

### E2 — Bibliothèque

| ID | User story | Critères d'acceptation | Prio |
| --- | --- | --- | --- |
| LIB-1 | En tant qu'utilisateur, je parcours les livres disponibles | Liste avec titre, auteur, niveau estimé, nombre de passages, couverture | M |
| LIB-2 | En tant qu'utilisateur, je vois quelle part d'un livre je comprends déjà | Pourcentage de mots connus affiché sur chaque livre et chaque passage, calculé sur les lemmes | M |
| LIB-3 | En tant qu'utilisateur, je filtre par niveau et par catégorie (contes, littérature, histoire, textes religieux) | Filtres combinables ; tri par « le plus adapté à mon niveau » | S |
| LIB-4 | En tant qu'utilisateur, je reprends ma lecture là où je l'avais laissée | Dernier passage lu mémorisé par livre ; bouton « Continuer » | M |
| LIB-5 | En tant qu'utilisateur, je vois la source et la licence de chaque livre | Auteur, éditeur d'origine, licence et lien affichés sur la fiche du livre | M |

### E3 — Import de livres (administration)

| ID | User story | Critères d'acceptation | Prio |
| --- | --- | --- | --- |
| IMP-1 | En tant qu'admin, j'importe un livre au format texte ou EPUB | Fichier stocké ; livre créé au statut « En analyse » ; la page d'import répond en moins de 2 s | M |
| IMP-2 | En tant qu'admin, le livre est découpé et analysé en arrière-plan | Découpage en chapitres puis en passages de 150 à 400 mots ; chaque mot reçoit lemme, racine, catégorie et vocalisation ; traitement en arrière-plan, sans bloquer l'application | M |
| IMP-3 | En tant qu'admin, je suis l'avancement et les erreurs de l'analyse | Statuts En analyse, Prêt à relire, Publié, Échec ; message d'erreur lisible ; relance possible | M |
| IMP-4 | En tant qu'admin, je corrige l'analyse d'un mot avant publication | Modifier le lemme, la racine ou la vocalisation d'une occurrence ; option « appliquer à toutes les occurrences » | S |
| IMP-5 | En tant qu'admin, je renseigne la licence avant de publier | Publication impossible sans licence ni source | M |
| IMP-6 | En tant qu'admin, j'importe un PDF | Hors V1 : l'extraction du texte arabe depuis un PDF est peu fiable (ordre des lettres, ligatures) | — |

### E4 — Lecteur

| ID | User story | Critères d'acceptation | Prio |
| --- | --- | --- | --- |
| READ-1 | En tant qu'utilisateur, je lis un passage en arabe, de droite à gauche | Police arabe lisible, taille réglable, interligne confortable | M |
| READ-2 | En tant qu'utilisateur, je vois chaque mot marqué selon son statut | Nouveau : pointillés bleus ; En cours 1 à 4 : pointillés jaunes de plus en plus pâles ; Connu et Ignoré : sans marque | M |
| READ-3 | En tant qu'utilisateur, je touche ou survole un mot pour voir sa fiche | Traduction, racine, lemme, catégorie grammaticale ; affichage en moins de 200 ms | M |
| READ-4 | En tant qu'utilisateur, je change le statut d'un mot depuis sa fiche | Boutons Nouveau, 1, 2, 3, 4, Connu, Ignorer ; raccourcis clavier 1 à 4, K et X ; le statut s'applique au lemme, donc à toutes ses formes dans tous les livres | M |
| READ-5 | En tant qu'utilisateur, je choisis l'affichage des voyelles courtes | Trois modes : partout, seulement sur les mots non connus, jamais | M |
| READ-6 | En tant qu'utilisateur, je marque toute la page comme lue | Bouton « Page terminée » : les mots restants en Nouveau passent en Connu (option désactivable) ; statistiques mises à jour | S |
| READ-7 | En tant qu'utilisateur, j'affiche la traduction d'une phrase | Traduction de phrase seulement si elle existe dans le livre source ; sinon le bouton n'apparaît pas | S |

## 5. Exigences fonctionnelles — vocabulaire et progression

### E5 — Banque de mots

| ID | User story | Critères d'acceptation | Prio |
| --- | --- | --- | --- |
| BANK-1 | En tant qu'utilisateur, chaque mot dont je change le statut entre dans ma banque | Une entrée par lemme et par utilisateur ; date d'ajout, statut, nombre de rencontres | M |
| BANK-2 | En tant qu'utilisateur, je consulte et filtre ma banque | Filtres par statut, par racine, par date ; recherche en arabe (avec ou sans voyelles) et en français | M |
| BANK-3 | En tant qu'utilisateur, je vois ma banque regroupée par racines | Vue « Racines » : chaque racine avec ses mots connus et en cours | S |
| BANK-4 | En tant qu'utilisateur, je vois dans quelles phrases j'ai rencontré un mot | Fiche du mot : les 5 dernières phrases rencontrées, avec lien vers le passage | M |
| BANK-5 | En tant qu'utilisateur, le compteur de rencontres augmente quand je lis | Chaque passage terminé incrémente le compteur des lemmes qu'il contient, une fois par passage | M |
| BANK-6 | En tant qu'utilisateur, j'exporte ma banque | Export CSV compatible Anki | S |

### E6 — Dictionnaire

| ID | User story | Critères d'acceptation | Prio |
| --- | --- | --- | --- |
| DICT-1 | En tant qu'utilisateur, j'obtiens la traduction française d'un lemme | Dictionnaire arabe-français sous licence libre, chargé en base ; plusieurs sens possibles, le plus fréquent en premier | M |
| DICT-2 | En tant qu'utilisateur, je cherche un mot dans le dictionnaire hors lecture | Recherche par forme arabe, par racine ou par mot français | S |
| DICT-3 | En tant qu'utilisateur, j'ajoute ma propre traduction à un mot | Traduction personnelle prioritaire sur celle du dictionnaire, visible seulement par moi | M |

### E7 — Flashcards

| ID | User story | Critères d'acceptation | Prio |
| --- | --- | --- | --- |
| CARD-1 | En tant qu'utilisateur, une flashcard est créée quand un mot passe en « En cours » | Carte créée automatiquement (option désactivable) ; recto : le mot dans la phrase où je l'ai rencontré, le mot surligné ; verso : traduction, racine, lemme | M |
| CARD-2 | En tant qu'utilisateur, je révise les cartes du jour | File du jour ; 4 réponses (À revoir, Difficile, Bien, Facile) ; prochaine date calculée par un algorithme de répétition espacée | M |
| CARD-3 | En tant qu'utilisateur, le statut du mot suit mes révisions | Plusieurs « Bien » ou « Facile » d'affilée font monter le statut ; un « À revoir » le fait redescendre ; règle paramétrable | M |
| CARD-4 | En tant qu'utilisateur, la phrase de la carte change d'une révision à l'autre | Si le mot a été rencontré dans plusieurs phrases, la carte en tire une différente à chaque fois | S |
| CARD-5 | En tant qu'utilisateur, je crée une carte à la main | Depuis la fiche d'un mot ou depuis la banque | S |

### E8 — Tableau de bord

| ID | User story | Critères d'acceptation | Prio |
| --- | --- | --- | --- |
| DASH-1 | En tant qu'utilisateur, je vois mes chiffres clés | Mots connus, mots en cours, cartes à réviser aujourd'hui, passages lus cette semaine | M |
| DASH-2 | En tant qu'utilisateur, je vois l'évolution de mon vocabulaire | Courbe du nombre de mots connus par jour sur 30, 90 et 365 jours | M |
| DASH-3 | En tant qu'utilisateur, je vois ma régularité | Calendrier des jours de lecture et de révision ; pas de pénalité en cas de jour manqué | S |
| DASH-4 | En tant qu'utilisateur, on me suggère le prochain passage à lire | Le passage où je connais entre 90 et 98 % des mots et qui contient le plus de mes mots en cours | M |
| DASH-5 | En tant qu'utilisateur, je vois mes mots connus par catégorie | Répartition verbes, noms, adjectifs, particules (d'après la catégorie du lemme) ; nombre de racines couvertes ; clic sur une catégorie → la liste filtrée dans la banque | M |
| DASH-6 | En tant qu'utilisateur, je vois une projection de mon niveau | À mon rythme des 30 derniers jours, date estimée pour atteindre chaque palier de vocabulaire ; paliers indicatifs par niveau, paramétrables, affichés comme une estimation et non comme un niveau officiel | S |

## 6. Exigences non fonctionnelles

| Domaine | Exigence | Comment on la vérifie |
| --- | --- | --- |
| Performance | Ouverture d'un passage en moins de 1 s ; fiche d'un mot en moins de 200 ms ; changement de statut en moins de 300 ms | Tests de charge, métriques de monitoring |
| Disponibilité | 99 % mensuel visé (projet personnel) ; un import en échec ne bloque jamais la lecture | Health checks, alertes Azure Monitor |
| Fiabilité de l'import | Un message traité deux fois ne crée pas de doublon (traitement idempotent) ; 3 tentatives, puis l'import est mis de côté avec son erreur | Tests d'intégration sur l'import |
| Sécurité | JWT signés par l'API (clé de signature dans Key Vault, rotation de clé possible), validation de l'émetteur, de l'audience et de l'expiration ; refresh tokens stockés hachés ; secrets dans Key Vault ; HTTPS partout ; accès aux ressources Azure par identité managée, sans chaîne de connexion en clair | Revue de configuration, analyse de dépendances (Dependabot), en-têtes de sécurité |
| Isolation des données | Un utilisateur ne voit jamais la banque d'un autre | Tests d'autorisation automatisés sur chaque endpoint |
| RGPD | Données minimales (e-mail, banque de mots, statistiques) ; export et suppression du compte ; hébergement en région Europe | Fonctions d'export et de suppression testées |
| Arabe et RTL | Affichage droite-gauche correct, y compris dans les interfaces mixtes français-arabe ; recherche insensible aux voyelles courtes, à la hamza et aux variantes d'alif (normalisation) | Tests unitaires sur la normalisation ; revue visuelle |
| Accessibilité | Navigation au clavier dans le lecteur ; contrastes suffisants pour les pointillés ; taille du texte réglable | Audit d'accessibilité automatisé et revue manuelle |
| Observabilité | Logs structurés, traces distribuées de l'API au worker et au service d'analyse, métriques métier (mots marqués, passages lus) | Outils de monitoring d'Azure |
| Qualité du code | Couverture de 80 % et plus sur Domain et Application ; analyse statique ; aucun merge sans pipeline vert | Pipeline CI/CD, protection de la branche principale |
| Coûts | Moins de 20 € par mois en V1 ; alerte de budget active | Azure Cost Management |

## 7. Contraintes techniques

Le PRD fixe seulement ce que le projet doit faire pratiquer. Les autres choix techniques se prennent au fil du projet, chacun avec une note de décision (ADR).

**Imposé**

- API en ASP.NET Core.
- Authentification JWT faite maison : inscription, connexion, refresh tokens.
- Conteneurs Docker.
- Hébergement sur Azure.
- Base de données : PostgreSQL (Azure Database for PostgreSQL Flexible Server).
- CI/CD : chaque commit sur la branche principale est testé et déployé automatiquement.
- Production ready : tests automatisés, monitoring, secrets hors du code, infrastructure reproductible.

**À décider au fil du projet**

- Le composant d'analyse linguistique des mots arabes (lemme, racine, voyelles).
- Comment l'import tourne en arrière-plan.
- L'algorithme de répétition espacée.
- Le dictionnaire arabe-français.

## 8. Jalons

Chaque jalon se termine par un déploiement en production sur Azure. Les durées sont des estimations pour un développeur seul, à temps partiel.

1. **J0 — Fondations** : une API ASP.NET Core minimale, Docker, le pipeline CI/CD jusqu'à Azure.
   - Terminé quand : un commit sur la branche principale se retrouve en production sans intervention manuelle.
2. **J1 — Authentification et bibliothèque** : authentification (AUTH-1, 2, 4), un livre ajouté à la main, bibliothèque (LIB-1, 4).
   - Terminé quand : tu crées un compte, tu te connectes et tu parcours la bibliothèque.
3. **J2 — Lecteur et banque de mots** : lecteur (READ-1 à 4), banque de mots (BANK-1, 2).
   - Terminé quand : tu lis un passage et ta banque se remplit.
4. **J3 — Import automatique** : IMP-1 à 3 et 5, dictionnaire (DICT-1, 3).
   - Terminé quand : 5 livres importés et publiés sans toucher à la base à la main.
5. **J4 — Révisions** : CARD-1 à 3, compteur de rencontres (BANK-4, 5), modes des voyelles (READ-5).
   - Terminé quand : tu révises chaque jour avec des cartes issues de tes lectures.
6. **J5 — Tableau de bord et production** : DASH-1, 2, 4, 5, pourcentage compris (LIB-2), monitoring et alertes, AUTH-3, 5, 6.
   - Terminé quand : la V1 est utilisable au quotidien et surveillée.
7. **Après la V1** : les stories « S », puis le journal, l'audio et l'import par les utilisateurs.

**Fenêtre de mars 2027** : J0 à J3 sont visés avant le démarrage du poste en mars 2027. J4 et J5 sont hors de cette fenêtre et se poursuivent ensuite ; c'est un choix de périmètre assumé, pas un échec.

## 9. Risques et questions ouvertes

**Risques**

| Risque | Impact | Parade |
| --- | --- | --- |
| Erreurs de l'analyse automatique des mots (lemme, racine, voyelles) | Mots mal regroupés, mauvaise traduction | Écran de relecture admin (IMP-4) ; garder la forme d'origine à côté de l'analyse |
| Peu de textes libres de niveau intermédiaire | Trou entre contes pour enfants et classiques difficiles | Commencer par StoryWeaver et Hindawi ; écrire ou faire écrire des textes intermédiaires sous licence libre |
| Licences mal vérifiées | Retrait de contenu, problème juridique | Champ licence obligatoire (IMP-5) ; liste des livres validée avant publication |
| Analyse linguistique dépendante d'un composant externe | Import bloqué ou analyse de mauvaise qualité si le composant est indisponible, difficile à intégrer ou peu fiable | Corpus pré-analysé pour les premiers jalons, afin que le lecteur et la banque de mots n'en dépendent pas ; intégration du composant ensuite, derrière une interface remplaçable |
| Complexité trop grande pour un projet solo | Projet abandonné avant la V1 | Jalons courts, chacun déployé ; ne pas découper en microservices au-delà de ce que prévoit l'architecture |
| Coûts Azure | Facture inattendue | Alerte de budget dès J0 ; offres gratuites ; arrêt des environnements de test hors usage |

**Questions ouvertes**

- Quelle taille de passage donne la meilleure expérience de lecture ? À tester entre 150 et 400 mots.
- Quelles éditions numériques des textes religieux reprendre, et sous quelles conditions ?
- Quels paliers de vocabulaire par niveau utiliser pour la projection du tableau de bord (DASH-6) ?

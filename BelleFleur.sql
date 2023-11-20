DROP DATABASE IF EXISTS Fleurs; 
CREATE DATABASE IF NOT EXISTS Fleurs; 
USE Fleurs;

DROP TABLE IF EXISTS client;
CREATE  TABLE IF NOT EXISTS client (
  courriel_client VARCHAR(50) NOT NULL ,
  nom_client VARCHAR(20),
  prenom_client VARCHAR(20) ,
  num_tel_client INT ,
  mot_de_passe_client VARCHAR(30) ,
  adresse_facturation_client VARCHAR(100) ,
  carte_credit VARCHAR(20) ,
  statut_fidelite VARCHAR(20),
  PRIMARY KEY (courriel_client) );
  
DROP TABLE IF EXISTS commande;
CREATE  TABLE IF NOT EXISTS commande (
  num_commande INT NOT NULL ,
  courriel_client VARCHAR(50) NOT NULL ,
  date_commande DATETIME,
  adresse_livraison VARCHAR(100) ,
  message VARCHAR(200) ,
  date_livraison DATETIME,
  type_commande VARCHAR(100) ,
  contenu_commande VARCHAR(400),
  etat_commande VARCHAR(20) ,
  prix_commande VARCHAR(10),
  nom_magasin VARCHAR (50),
  PRIMARY KEY (num_commande),
  CONSTRAINT courrielclient FOREIGN KEY (courriel_client) REFERENCES client (courriel_client)); 
  
DROP TABLE IF EXISTS standards;
CREATE  TABLE IF NOT EXISTS standards (
  nom_bouquet VARCHAR(20) NOT NULL ,
  composition_fleurs VARCHAR(200),
  prix_bouquet_standards VARCHAR(10) ,
  categorie_bouquet VARCHAR(20),
  stock_bouquet INTEGER,
  PRIMARY KEY (nom_bouquet) );
  

DROP TABLE IF EXISTS produit;
CREATE  TABLE IF NOT EXISTS produit (
  nom_produit VARCHAR(20) NOT NULL ,
  prix_produit float,
  disponibilité_produit VARCHAR(20),
  stock_produit INTEGER,
  PRIMARY KEY (nom_produit) );
  
  
INSERT INTO `fleurs`.`standards` (`nom_bouquet`, `composition_fleurs`, `prix_bouquet_standards`, `categorie_bouquet`,`stock_bouquet`) VALUES ("Gros Merci","Arrangement floral avec marguerites et verdure", '45€', 'Toute occasion',100);
INSERT INTO `fleurs`.`standards` (`nom_bouquet`, `composition_fleurs`, `prix_bouquet_standards`, `categorie_bouquet`,`stock_bouquet`) VALUES ("L'amoureux","Arrangement floral avec  roses blanches et roses rouges", '65€', 'St-Valentin',100);
INSERT INTO `fleurs`.`standards` (`nom_bouquet`, `composition_fleurs`, `prix_bouquet_standards`, `categorie_bouquet`,`stock_bouquet`) VALUES ("L'Exotique","Arrangement floral avec ginger, oiseaux du paradis, roses et genet", '40€', 'Toute occasion',100);
INSERT INTO `fleurs`.`standards` (`nom_bouquet`, `composition_fleurs`, `prix_bouquet_standards`, `categorie_bouquet`,`stock_bouquet`) VALUES ("Maman","Arrangement floral avec gerbera, roses blanches, lys et alstroméria", '80€', 'Fête des mères',100);
INSERT INTO `fleurs`.`standards` (`nom_bouquet`, `composition_fleurs`, `prix_bouquet_standards`, `categorie_bouquet`,`stock_bouquet`) VALUES ("Vive la mariée","Arrangement floral avec lys et orchidées", '120$', 'Mariage',100);

INSERT INTO `fleurs`.`produit` (`nom_produit`, `prix_produit`, `disponibilité_produit`,`stock_produit`) VALUES ("Gerbera", 5, "à l'année",100);
INSERT INTO `fleurs`.`produit` (`nom_produit`, `prix_produit`, `disponibilité_produit`,`stock_produit`) VALUES ("Ginger", 4, "à l'année",100);
INSERT INTO `fleurs`.`produit` (`nom_produit`, `prix_produit`, `disponibilité_produit`,`stock_produit`) VALUES ("Glaïeul", 1, "mai à novembre",100);
INSERT INTO `fleurs`.`produit` (`nom_produit`, `prix_produit`, `disponibilité_produit`,`stock_produit`) VALUES ("Marguerite", 2.25, "à l'année",100);
INSERT INTO `fleurs`.`produit` (`nom_produit`, `prix_produit`, `disponibilité_produit`,`stock_produit`) VALUES ("Rose rouge", 2.5, "à l'année",15);


select distinct courriel_client from commande as C2 where (select count(*) from commande as C3 group by courriel_client having C3.courriel_client=C2.courriel_client)>(select avg(count) from (select count(*) as count, courriel_client from commande as C1 group by courriel_client) as T1);

select distinct Com1.courriel_client, Com2.courriel_client from commande Com1, commande Com2 where Com1.nom_magasin=Com2.nom_magasin and Com1.courriel_client<Com2.courriel_client;
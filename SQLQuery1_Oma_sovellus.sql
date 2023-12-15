CREATE TABLE autot (id_auto INTEGER IDENTITY(1,1) PRIMARY KEY, merkki TEXT, rekisterinumero VARCHAR(10), miksi_huollossa VARCHAR(100));

CREATE TABLE ty�n_tilanne (id_ty�n_tilanne INTEGER IDENTITY(1,1) PRIMARY KEY, nimi TEXT, hinta INTEGER);

CREATE TABLE ty�ntekij�t (id_ty�ntekij� INTEGER IDENTITY (1,1) PRIMARY KEY, nimi VARCHAR(50), ty�numero VARCHAR(50));

CREATE TABLE ty�njako (id_ty�njako INTEGER IDENTITY (1,1) PRIMARY KEY, ty�ntekij� VARCHAR(50), rekisterinumero VARCHAR(50));


INSERT INTO autot (merkki, rekisterinumero, miksi_huollossa) VALUES ('Volvo', 'SSS-888', 'Jarrupalat');
INSERT INTO autot (merkki, rekisterinumero, miksi_huollossa) VALUES ('Audi', 'HJK-159', 'Vuosihuolto');

INSERT INTO ty�ntekij�t (nimi, ty�numero) VALUES ('Roope Koskinen', '1224');
INSERT INTO ty�ntekij�t (nimi, ty�numero) VALUES ('Jari Purro', '1225');

INSERT INTO ty�njako (id_ty�ntekij�, id_auto) VALUES (1, 2);

DELETE  FROM ty�njako;
SELECT * FROM autot;

SELECT * FROM ty�ntekij�t;

SELECT * FROM ty�njako;
SELECT ty.ty�numero AS ty�ntekij�, a.rekisterinumero AS rekisterinumero  FROM ty�ntekij�t ty, autot a, ty�njako tyo WHERE a.rekisterinumero=tyo.rekisterinumero AND ty.ty�numero=tyo.ty�ntekij�;
SELECT ti.id AS id, a.nimi AS asiakas, tu.nimi AS tuote, ti.toimitettu AS toimitettu  FROM tilaukset ti, asiakkaat a, tuotteet tu WHERE a.id=ti.asiakas_id AND tu.id=ti.tuote_id;
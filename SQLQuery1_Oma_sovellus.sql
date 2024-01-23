CREATE TABLE autot (id_auto INTEGER IDENTITY(1,1) PRIMARY KEY, merkki TEXT, rekisterinumero VARCHAR(10), miksi_huollossa VARCHAR(100));

CREATE TABLE ty�ntekij�t (id_ty�ntekij� INTEGER IDENTITY (1,1) PRIMARY KEY, nimi VARCHAR(50), ty�numero VARCHAR(50));

CREATE TABLE ty�njako (id_ty�njako INTEGER IDENTITY (1,1) PRIMARY KEY, ty�ntekij� VARCHAR(50), rekisterinumero VARCHAR(50), valmis BIT DEFAULT 0);

CREATE TABLE autojen_tilanne (id_auti INTEGER IDENTITY (1,1) PRIMARY KEY, rekisterinumero VARCHAR(50), valmis VARCHAR(50));

INSERT INTO autot (merkki, rekisterinumero, miksi_huollossa) VALUES ('Volvo', 'SSS-888', 'Jarrupalat');
INSERT INTO autot (merkki, rekisterinumero, miksi_huollossa) VALUES ('Audi', 'HJK-159', 'Vuosihuolto');

INSERT INTO ty�ntekij�t (nimi, ty�numero) VALUES ('Roope Koskinen', '1224');
INSERT INTO ty�ntekij�t (nimi, ty�numero) VALUES ('Jari Purro', '1225');



DELETE  FROM ty�njako;
DELETE  FROM autojen_tilanne;
SELECT * FROM autot;
SELECT * FROM ty�ntekij�t;
SELECT * FROM ty�njako;
SELECT * FROM autojen_tilanne;

SELECT ty.ty�numero AS ty�ntekij�, a.rekisterinumero AS rekisterinumero  FROM ty�ntekij�t ty, autot a, ty�njako tyo WHERE a.rekisterinumero=tyo.rekisterinumero AND ty.ty�numero=tyo.ty�ntekij�;

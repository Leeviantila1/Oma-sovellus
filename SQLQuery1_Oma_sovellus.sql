CREATE TABLE autot (id_auto INTEGER IDENTITY(1,1) PRIMARY KEY, merkki TEXT, rekisterinumero VARCHAR(10), miksi_huollossa VARCHAR(100));

CREATE TABLE työn_tilanne (id_työn_tilanne INTEGER IDENTITY(1,1) PRIMARY KEY, nimi TEXT, hinta INTEGER);

CREATE TABLE työntekijät (id_työntekijä INTEGER IDENTITY (1,1) PRIMARY KEY, nimi VARCHAR(50), työnumero VARCHAR(50));

CREATE TABLE työnjako (id_työnjako INTEGER IDENTITY (1,1) PRIMARY KEY, työntekijä VARCHAR(50), rekisterinumero VARCHAR(50));


INSERT INTO autot (merkki, rekisterinumero, miksi_huollossa) VALUES ('Volvo', 'SSS-888', 'Jarrupalat');
INSERT INTO autot (merkki, rekisterinumero, miksi_huollossa) VALUES ('Audi', 'HJK-159', 'Vuosihuolto');

INSERT INTO työntekijät (nimi, työnumero) VALUES ('Roope Koskinen', '1224');
INSERT INTO työntekijät (nimi, työnumero) VALUES ('Jari Purro', '1225');

INSERT INTO työnjako (id_työntekijä, id_auto) VALUES (1, 2);

DELETE  FROM työnjako;
SELECT * FROM autot;

SELECT * FROM työntekijät;

SELECT * FROM työnjako;
SELECT ty.työnumero AS työntekijä, a.rekisterinumero AS rekisterinumero  FROM työntekijät ty, autot a, työnjako tyo WHERE a.rekisterinumero=tyo.rekisterinumero AND ty.työnumero=tyo.työntekijä;
SELECT ti.id AS id, a.nimi AS asiakas, tu.nimi AS tuote, ti.toimitettu AS toimitettu  FROM tilaukset ti, asiakkaat a, tuotteet tu WHERE a.id=ti.asiakas_id AND tu.id=ti.tuote_id;
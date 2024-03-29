-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server Version:               10.4.22-MariaDB - mariadb.org binary distribution
-- Server Betriebssystem:        Win64
-- HeidiSQL Version:             11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Exportiere Datenbank Struktur für opendelivery
CREATE DATABASE IF NOT EXISTS `opendelivery` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `opendelivery`;

-- Exportiere Struktur von Tabelle opendelivery.adressen
CREATE TABLE IF NOT EXISTS `adressen` (
  `Adressnummer` int(11) NOT NULL AUTO_INCREMENT,
  `Postleitzahl` int(11) NOT NULL,
  `Ort` varchar(255) NOT NULL,
  `Strasse` varchar(255) NOT NULL,
  `Nummer` int(11) NOT NULL,
  `Adresszusatz` varchar(255) DEFAULT NULL,
  `Koordinate` int(11) DEFAULT NULL,
  PRIMARY KEY (`Adressnummer`),
  KEY `Adressen_fk0` (`Koordinate`) USING BTREE,
  CONSTRAINT `Adressen_fk0` FOREIGN KEY (`Koordinate`) REFERENCES `koordinaten` (`KoordinatenID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

-- Daten Export vom Benutzer nicht ausgewählt

-- Exportiere Struktur von Tabelle opendelivery.bestellungen
CREATE TABLE IF NOT EXISTS `bestellungen` (
  `Bestellnummer` int(11) NOT NULL AUTO_INCREMENT,
  `Kunde` int(11) NOT NULL,
  `Route` int(11) NOT NULL,
  `Produkte` longtext NOT NULL,
  PRIMARY KEY (`Bestellnummer`),
  KEY `Bestellungen_fk0` (`Kunde`),
  KEY `Bestellungen_fk1` (`Route`),
  CONSTRAINT `Bestellungen_fk0` FOREIGN KEY (`Kunde`) REFERENCES `kunden` (`Kundennummer`),
  CONSTRAINT `Bestellungen_fk1` FOREIGN KEY (`Route`) REFERENCES `routen` (`RoutenID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

-- Daten Export vom Benutzer nicht ausgewählt

-- Exportiere Struktur von Prozedur opendelivery.createAdresse
DELIMITER //
CREATE PROCEDURE `createAdresse`(
	IN `plz` INT,
	IN `ort` CHAR(50),
	IN `strasse` VARCHAR(50),
	IN `nummer` INT,
	IN `adresszusatz` VARCHAR(50),
	IN `koordinatennummer` INT
)
BEGIN
INSERT INTO adressen (adressen.Postleitzahl, adressen.Ort, adressen.Strasse, adressen.Nummer, adressen.Adresszusatz, adressen.Koordinate)
VALUES (plz, ort, strasse, nummer, adresszusatz, koordinatennummer);
SELECT adressen.Adressnummer AS LastID FROM adressen WHERE adressen.Adressnummer = @@Identity;
END//
DELIMITER ;

-- Exportiere Struktur von Prozedur opendelivery.createBestellung
DELIMITER //
CREATE PROCEDURE `createBestellung`(
	IN `thiskunde` INT,
	IN `thisroute` INT,
	IN `thisprodukte` LONGTEXT
)
BEGIN
INSERT INTO bestellungen (bestellungen.Kunde, bestellungen.Route, bestellungen.`Produkte`)
VALUES (thiskunde, thisroute, thisprodukte);
SELECT bestellungen.Bestellnummer AS LastID FROM bestellungen WHERE bestellungen.Bestellnummer = @@Identity;
END//
DELIMITER ;

-- Exportiere Struktur von Prozedur opendelivery.createKoordinate
DELIMITER //
CREATE PROCEDURE `createKoordinate`(
	IN `lat` DOUBLE,
	IN `lon` DOUBLE
)
BEGIN
INSERT INTO opendelivery.koordinaten (koordinaten.Latitude, koordinaten.Longitude)
VALUES (lat, lon);
SELECT koordinaten.KoordinatenID AS LastID FROM koordinaten WHERE koordinaten.KoordinatenID = @@Identity;
END//
DELIMITER ;

-- Exportiere Struktur von Prozedur opendelivery.createKunde
DELIMITER //
CREATE PROCEDURE `createKunde`(
	IN `vorname` VARCHAR(50),
	IN `nachname` VARCHAR(50),
	IN `adresse` INT
)
BEGIN
INSERT INTO kunden (kunden.Vorname, kunden.Nachname, kunden.Adresse)
VALUES (vorname, nachname, adresse);
SELECT kunden.Kundennummer AS LastID FROM kunden WHERE kunden.Kundennummer = @@Identity;
END//
DELIMITER ;

-- Exportiere Struktur von Prozedur opendelivery.createProdukt
DELIMITER //
CREATE PROCEDURE `createProdukt`(
	IN `nam` VARCHAR(50),
	IN `einheit` VARCHAR(50)
)
BEGIN
INSERT INTO produkte (produkte.`Name`, produkte.Mengeneinheit)
VALUES (nam, einheit);
SELECT produkte.Artikelnummer AS LastID FROM produkte WHERE produkte.Artikelnummer = @@Identity;
END//
DELIMITER ;

-- Exportiere Struktur von Prozedur opendelivery.createRoute
DELIMITER //
CREATE PROCEDURE `createRoute`(
	IN `nam` VARCHAR(50)
)
BEGIN
INSERT INTO routen (routen.`Name`)
VALUES (nam);
SELECT routen.RoutenID AS LastID FROM routen WHERE routen.RoutenID = @@Identity;
END//
DELIMITER ;

-- Exportiere Struktur von Tabelle opendelivery.koordinaten
CREATE TABLE IF NOT EXISTS `koordinaten` (
  `KoordinatenID` int(11) NOT NULL AUTO_INCREMENT,
  `Latitude` float NOT NULL DEFAULT 0,
  `Longitude` float NOT NULL,
  PRIMARY KEY (`KoordinatenID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

-- Daten Export vom Benutzer nicht ausgewählt

-- Exportiere Struktur von Tabelle opendelivery.kunden
CREATE TABLE IF NOT EXISTS `kunden` (
  `Kundennummer` int(11) NOT NULL AUTO_INCREMENT,
  `Vorname` varchar(255) NOT NULL,
  `Nachname` varchar(255) NOT NULL,
  `Adresse` int(11) NOT NULL,
  PRIMARY KEY (`Kundennummer`),
  KEY `Kunden_fk0` (`Adresse`),
  CONSTRAINT `Kunden_fk0` FOREIGN KEY (`Adresse`) REFERENCES `adressen` (`Adressnummer`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

-- Daten Export vom Benutzer nicht ausgewählt

-- Exportiere Struktur von Tabelle opendelivery.produkte
CREATE TABLE IF NOT EXISTS `produkte` (
  `Artikelnummer` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Mengeneinheit` varchar(255) NOT NULL,
  PRIMARY KEY (`Artikelnummer`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

-- Daten Export vom Benutzer nicht ausgewählt

-- Exportiere Struktur von Tabelle opendelivery.routen
CREATE TABLE IF NOT EXISTS `routen` (
  `RoutenID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  PRIMARY KEY (`RoutenID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

-- Daten Export vom Benutzer nicht ausgewählt

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;

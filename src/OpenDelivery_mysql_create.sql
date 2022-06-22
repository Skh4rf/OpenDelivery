CREATE TABLE `Routen` (
	`RoutenID` INT NOT NULL AUTO_INCREMENT,
	`Name` VARCHAR(255) NOT NULL,
	PRIMARY KEY (`RoutenID`)
);

CREATE TABLE `Bestellungen` (
	`Bestellnummer` INT NOT NULL AUTO_INCREMENT,
	`Kunde` INT NOT NULL,
	`Route` INT NOT NULL,
	`Produkte` varchar(255) NOT NULL,
	PRIMARY KEY (`Bestellnummer`)
);

CREATE TABLE `Kunden` (
	`Kundennummer` INT NOT NULL AUTO_INCREMENT,
	`Vorname` VARCHAR(255) NOT NULL,
	`Nachname` VARCHAR(255) NOT NULL,
	`Adresse` INT NOT NULL,
	PRIMARY KEY (`Kundennummer`)
);

CREATE TABLE `Adressen` (
	`Adressnummer` INT NOT NULL AUTO_INCREMENT,
	`Postleitzahl` INT NOT NULL,
	`Strasse` VARCHAR(255) NOT NULL,
	`Nummer` INT NOT NULL,
	`Adresszusatz` VARCHAR(255),
	`Koordinaten` INT,
	PRIMARY KEY (`Adressnummer`)
);

CREATE TABLE `Koordinaten` (
	`KoordinatenID` BINARY NOT NULL AUTO_INCREMENT,
	`Latitude` FLOAT NOT NULL,
	`Longitude` FLOAT NOT NULL,
	PRIMARY KEY (`KoordinatenID`)
);

CREATE TABLE `Produkte` (
	`Artikelnummer` INT NOT NULL AUTO_INCREMENT,
	`Name` VARCHAR(255) NOT NULL,
	`Mengeneinheit` VARCHAR(255) NOT NULL,
	PRIMARY KEY (`Artikelnummer`)
);

ALTER TABLE `Bestellungen` ADD CONSTRAINT `Bestellungen_fk0` FOREIGN KEY (`Kunde`) REFERENCES `Kunden`(`Kundennummer`);

ALTER TABLE `Bestellungen` ADD CONSTRAINT `Bestellungen_fk1` FOREIGN KEY (`Route`) REFERENCES `Routen`(`RoutenID`);

ALTER TABLE `Kunden` ADD CONSTRAINT `Kunden_fk0` FOREIGN KEY (`Adresse`) REFERENCES `Adressen`(`Adressnummer`);

ALTER TABLE `Adressen` ADD CONSTRAINT `Adressen_fk0` FOREIGN KEY (`Koordinaten`) REFERENCES `Koordinaten`(`KoordinatenID`);








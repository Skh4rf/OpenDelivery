# OpenDelivery

Zustell-Dispositionssystem:

Ziel ist es ein Programm zu entwickeln welches so in einem Fahrzeug eines Zustelldienstes Platz finden könnte, um die Lieferanten bei der 
Lieferung zu unterstützen. Es sollen Route, Name und Produkte der nächsten Kunden angezeigt werden. Ebenso sollen zahlreiche Funktionen 
implementiert werden wie zum Beispiel eine automatische Rechnungserstellung zum Ende des Monats, oder für Abo bestellungen die Möglichkeit 
für den Kunden noch vor Antritt der Fahrt des Zustellers eine Änderung z.B. via App vorzunehmen. 

Scope 1:
- Map mit Route (Google oder Bings API?)
- Anzeigen der nächsten Kunden pro Route
- Rechnungserstellen (Nach erfolgter Lieferung sollen die gelieferten Produkte bestätigt werden {evtl. gab es eine Änderung} können und 
	auf eine Rechnung geschrieben werden) --> automatische PDF-Erzeugung
- Bearbeiten der Routen und Hinzufügen von Kunden vom Admin

Scope 2:
- Berechnung einer geeigneten Route aus Destinations-Array
- Web-Portal oder App für die Kunden
- gesicherte Datenbank-Anbindung
- Baukastenprinzip zur Erstellung der App für verschiedene Anwendungsfälle (Lieferdienst, Zustelldienst, Selbstvermarkter, etc.)


Technologien:
MySQL, Map API, UWP
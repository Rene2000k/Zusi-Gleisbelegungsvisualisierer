# Zusi-Gleisbelegungsvisualisierer

Dieses Tool stellt die Gleisbelegungen einer Betriebsstelle für einen Fahrplan des Zugsimulators ZUSI 3 ([Webseite](https://www.zusi.de), [Steam](https://store.steampowered.com/app/1040730/ZUSI_3__Aerosoft_Edition)) visuell dar.  
Das Tool richtet sich vor allem an Fahrplanbauer und eignet sich um bei komplexen Fahrplänen Mehrfachbelegungen von Gleisen in Betriebsstellen zu entdecken.

## Bedienung
### Hauptmenü

Das Hauptmenü ist der Startpunkt des Tools und dient zur Definition von Betriebstellen, Gleisen und Signalen zur Visualisierung sowie der Auswahl des aktuellen Fahrplans.

![main-menu](https://github.com/user-attachments/assets/2dd9f663-6ed5-49aa-804a-b52c248d6212)

1. Fahrplanauswahl  
    **a.** Der Pfad zum aktuellen Fahrplan, der analysiert werden soll. Das Dropdown enthält eine Liste der 5 letzten Pfade, welche so schnell ausgewählt werden können.  
    **b.** Öffnet ein Dialogfenster zur Suche eines Fahrplanordners im Explorer.
2. Betriebsstellen  
    **a.** Liste der bereits erstellenten Betriebstellen.  
    **b.** Name einer neu hinzuzufügenden Betriebsstelle. Der Name der Betriebsstelle muss dem Namen der Betriebsstelle in ZUSI entsprechen.
    **c.** Hinzufügen der neuen Betriebsstelle.  
    **d.** Löschen der aktuell ausgewählten Betriebsstelle.
3. Gleise  
    **a.** Liste der Gleise der aktuell ausgewählten Betriebsstelle.  
    **b.** Name eines neu hinzuzufügenden Gleises. Der Name ist ein Freitextfeld. Es können auch mehrere Gleise auf dem gleichen Gleisabschnitt (z.B. mit unterschiedlichen Signalen) definiert werden.  
    **c.** Hinzufügen des neuen Gleises.  
    **d.** Löschen des aktuell ausgewählten Gleises.
    **e.** Verschieben der Position des aktuell ausgewählten Gleises. Diese Position bestimmt die Position des Gleises in der Visualisierung.
4. Signale  
    **a.** Liste der Signale, welche für das angegebene Gleis analysiert werden sollen. Dies bestimmt, welche Züge für die Belegung dieses Gleises angezeigt werden. Ein betroffener Zug muss mindestens eins der aufgeführten Signale als Fahrplaneintrag haben, um angezeigt zu werden.  
    **b.** Name eines neu hinzuzufügenden Signals. Der Name muss exakt der Signalbezeichnung der Betriebsstelle entsprechen. Typischerweise sind dies Ausfahrsignale in **beide** Richtungen eines Gleises. Es können aber auch Einfahrsignale sein, wenn man z.B. durchfahrende Züge - welche nur einen Einfahrsignal-Eintrag haben - erfassen möchte. Letztendlich kann hier eine beliebige Signal-Kombination gewählt werden, die für die Visualisierung sinnvoll ist.  
    **c.** Hinzufügen des neuen Signals.   
    **d.** Löschen des aktuell ausgewählten Signals.

### Visualierung

Im Visualisierungs-Tab sieht man die eigentliche Übersicht der Gleisbelegungen.

![visualisation](https://github.com/user-attachments/assets/7083562f-47a8-4714-bbd5-abb0393b14c9)

1. Fahrplanauswahl & Analyse  
    **a.** Im Dropdown kann man eine der zuvor erstellen Betriebsstellen zur Analyse auswählen.  
    **b.** Durch Klick auf "Analysieren", kann die Analyse gestartet werden. Dies kann einige Sekunden dauern.  
    **c.** Über diese Checkbox können alternative Gleisbelegungen ein und ausgeschaltet werden. Alternative Belegungen sind solche, welche in einem Fahrplan-Eintrag eines Zuges nach dem ersten Signal genannt werden. Diese werden von ZUSI als alternativer Fahrweg genutzt (z.B. wenn der Fahrweg über das erstgenannte Signal belegt ist).
2. Am Rand der Tabelle werden die Gleise in der zuvor bestimmten Reihenfolge sowie ein Zeitstrahl vom Start- bis zum Endzeitpunkt des Fahrplans angezeigt.
3. Gleisbelegungen. Züge werden in der Gleisbelegung mit der Zugnummer, dem Zuglauf, sowie der Ankunfts- und Abfahrtszeit des Fahrplaneintrags angezeigt. Beim Hovern über einen Zug werden diese Informationen auch in einem Tooltip angezeigt. Dies ist für kleine Einträge hilfreich.  
    **a.** Ein haltendener Zug wird ausgefüllt dargestellt.  
    **b.** Ein durchfahrender Zug wird schraffiert dargestellt.  
    **c.** Alternative Belegungen wird nur mit Rahmen dargestellt.

![visualisation-conflict](https://github.com/user-attachments/assets/a4d630b2-97d0-4d69-888c-fe5025308a2c)

Konflikte in der Belegung werden durch das Nebeneinanderstellen der betroffenden Züge dargestellt.

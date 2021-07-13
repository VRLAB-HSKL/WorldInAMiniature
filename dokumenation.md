# Dokumentation zu World in a Miniature (WIM)

## Motivation & Beschreibung

Eine Virtual Reality (VR) Anwendung ermöglicht es Nutzern mit Hilfe eines Head-Mounted-Displays (HMD) in eine virtuelle Welt einzutauchen. 
Durch Verfolgen der Position des HMDs kann sich der Nutzer innerhalb der virtuellen Welt bewegen, indem er sich physisch in der realen Welt bewegt.
Virtuelle Welten können beliebig groß werden und z. B. Teile von Städten nachstellen. Der reale Raum, in welchem sich der Nutzer befindet, während er die VR-Anwendung 
ausführt, ist jedoch begrenzt. Üblicherweise beträgt dieser bei dem HMD Modell "HTC Vive Focus Plus" 2x2 Meter und kann auf maximal 10x10m erweitert werden [1]. 
D. h. der Nutzer könnte sich maximal in einem 10x10m Bereich innerhalb der virtuellen Welt bewegen. Das Bewegen innerhalb einer virtuellen Welt wird auch "Travelling" 
genannt. Ein weiterer Aspekt ist die Objektselektion und -manipulation. Durch Controller, welche der Nutzer in der Hand hält, kann die Position seiner Hände innerhalb 
der virtuellen Welt verfolgt werden. Dadurch ist es möglich Objekte in der virtuellen Welt zu greifen. Da ein Mensch keine unendlich langen Arme besitzt, ist die 
Reichweite zum Selektieren und Manipulieren von Objekten begrenzt. Aufgrund dieser Beschränkungen werden andere Methoden benötigt, um sich auch über größere 
Distanzen innerhalb der virtuellen Welt zu bewegen und Objekte in der Ferne zu selektieren und manipulieren. 

Eine dieser Techniken ist "World in a Miniature", kurz WIM. Beim WIM Ansatz befindet sich innerhalb der virtuellen Welt zusätzlich eine 3D Miniatur
dieser virtuellen Welt. Diese wird üblicherweise auf einer kleinen Platte gerendert, welche der Benutzer in der Hand halten kann. Die Objekte auf dieser Platte 
stellen 3D Miniaturen der Objekte der virtuellen Welt dar und wirken wie ein Proxy. D. h. werden die 3D Miniaturen bewegt, so bewegen sich die entsprechenden Objekte in der
virtuellen Welt. Um auch das Problem des begrenzten Travellings zu lösen, befindet sich in der 3D Miniatur ein Charakter, welcher den Nutzer darstellt. Wird dieser Charakter
bewegt, so bewegt sich der Spieler selbst. In [2] wird dabei zwischen drei verschiedene Möglichkeiten bzgl. dem Aktualisieren der Position unterschieden:
1. "immediate update": Das "reale" Objekt bewegt sich in Echtzeit mit, während die 3D Miniatur bewegt wird
2. "post-mortem update": Das reale Objekt wird erst aktualisiert, wenn der Nutzer die Interaktion abgeschlossen hat, z. B. das Objekt wieder losgelassen hat
3. "batch update": bei dieser Methode führt der Nutzer mehrere Änderungen in der 3D Miniatur durch und übernimmt durch einen expliziten Befehl die Änderungen in die reale Welt

## Erläuterung der eigenen Umsetzung

Es wurde ein Teil einer kleinen Stadt erstellt, innerhalb welcher sich der Nutzer bewegen kann. Die WIM wurde auf einmal auf einem nicht beweglichen Tisch platziert und eine 
weitere WIM auf einer beweglichen Platte. Diese Platte kann vom Benutzer gegriffen werden und bewegt werden. Außerdem kann die bewegliche Platte jederzeit zum Nutzer
teleportiert werden. Die Gebäude, sowie Dekorationen der Stadt können sowohl in den 3D Miniaturen, als auch in real bewegt werden. Hierzu geht ein Raycast von beiden Controllern 
aus, mit welchem die Objekte anvisiert, selektiert und bewegt werden können. In den 3D Miniaturen ist ein Charakter, welcher den Nutzer repräsentiert. Damit dieser Charakter 
leichter gefunden wird, befindet er sich in einer durchsichtigen Kugel. Diese Kugel kann an eine Position innerhalb der Miniatur bewegt werden, um sich selbst an die 
entsprechende Stelle in der Welt zu teleportieren.

Für das Aktualisieren der Objekte wurden folgende Methoden eingesetzt:
- Für das Bewegen von Objekten wurde "immediate update" verwendet, damit der Nutzer seine Änderungen direkt sehen kann, während er die Miniatur noch bewegt
- Für das Bewegen des Nutzers wurde "post-mortem update" verwendet, da "immediate update" zu Desorientierung des Benutzers führen kann [3]

## Implementierung

### Idee
Wenn die Anwendung gestartet wird, werden festgelegte Objekte geklont, in der Größe runterskaliert und auf einer kleinen Platte gerendert. Die Position und Rotation zwischen den geklonten Objekten und den realen Objekten soll synchron gehalten werden.

### Umsetzung

#### Objekte zum Klonen festlegen
Das Klonen der Objekte nach dem Start der Anwendung übernimmt das Skript `MiniWorld`. Über eine Liste von GameObjects können die Objekte festgelegt werden, welche in der Miniaturansicht inkludiert werden sollen. In der `Start`-Methode von `MonoBehaviour` werden die festgelegten Objekte geklont. Dabei wird als Parent-Objekt, das Objekt, welches das `MiniWorld` Skript besitzt angegeben.

#### Skalierung
Um die Objekte runter zu skalieren, wurde ein leeres GameObject verwendet, welches die 3D Miniatur beinhaltet. Dieses Objekt dient als Root-Objekt für alle geklonten Objekte. Bei dem Transform dieses Objekts wird dann der Scale auf den gewünschten Wert angepasst. Dadurch werden alle Kindobjekte entsprechend skaliert.

#### Positionsverfolgung von Objekten
Damit die Objekte in der 3D Miniatur an der richtigen Position stehen, entspricht die lokale Position der Objekte der lokalen Position der entsprechenden realen Objekte. Damit die Positionen der Miniaturen und realen Objekte synchron gehalten wird, wurde das Skript `TransformTracker` geschrieben. Dieses wird dem geklonten Objekt zugewiesen und dem Parameter `target` wird das entsprechende reale Objekt zugewiesen. Dadurch verfolgt das geklonte Objekt das target Objekt. Zusätzlich kann `updateTarget` gesetzt werden, um umgekehrt auch das `target` zu aktualisieren, wenn der Klon die Position ändert. Dieses Skript wird im Skript `MiniWorld` zu Beginn der Anwendung jedem geklonten Objekt hinzugefügt.

#### Bewegbare Objekte
Damit Objekte bewegbar sind, wurde das `Draggable`-Skript von Vive Input Utility verwendet. Um nicht jedem Objekt in der Scene dieses Objekt zuweisen zu müssen, wurde das Skript `MakeChildsDraggable` geschrieben. Dadurch können alle Objekte, welche Draggable sein sollen in ein leeres Objekt gesteckt werden und es muss nur dem leeren Objekt das Skript `MakeChildsDraggable` zugewiesen werden.

#### Positionsverfolgung des Spielers
Das Skript `TransformTracker` kann nicht zur Positionsverfolgung des Spielers genutzt werden, da die Camera in einem VROrigin Objekt ist. D. h. die Position des Spielers ergibt sich aus der lokalen Position von VROrigin und der lokalen Position der Camera. Außerdem wird nur die y-Rotation des Spielers verfolgt. Hierzu wurde das Skript `VRCameraTracker` geschrieben. Diesem Skript besitzt die Parameter `target` und `pivot`. Die Bezeichnung ist dabei an die von Vive Input Utility angepasst. `target` stellt das Root-Objekt der Kamera dar und `pivot` die Kamera. Da normalerweise nur eine VR-Kamera in einer Scene vorhanden ist, wäre es umständlich diese ständig zu setzen, wenn das Skript verwendet wird. Deshalb wurde Logik implementiert, welche die Kamera automatisch findet und setzt.

#### Teleportieren des Spielers
In der Miniaturansicht ist ein Charakter enthalten, dieser ist `Draggable` und besitzt das `VRCameraTracker` Skript, um die Position mit der des Spielers zu synchronisieren. Dadurch, dass die Position immer auf die des Spielers gesetzt wird ist das bewegen nicht möglich. Deshalb muss beim `Draggable` bei `AfterGrabbed`, der `VRCameraTracker` deaktiviert und bei `OnDrop` wieder aktiviert werden. Zusätzlich wird bei `OnDrop` die Teleportation ausgeführt. Hierfür wurde ein Skript `TeleportPlayerToThis` geschrieben, welche eine Methode `Teleport` implementiert. Diese teleportiert beim Aufruf den Spieler an die lokale Position, an welcher sich das Objekt mit diesem Skript befindet. Dabei wird nicht die Camera, sondern `VROrigin` (das Root-Objekt der Camera) bewegt. Es muss beachtet werden, dass wenn sich der Spieler bewegt, dann bewegt sich die Camera innerhalb des Root-Objekts. Dies muss beim Bewegen von `VROrigin` berücksichtigt werden. 

#### Bewegen und teleportieren der 3D Miniatur
Die 3D Miniatur befindet sich auf einer Platte. Die Platte wurde durch ein Cube-Objekt modelliert. Dieses Cube-Objekt besitzt das `StickyGrabbable`-Skript von Vive Input Utility. Dies ermöglicht es, die Platte mit dem Controller zu greifen und zu bewegen. `StickyGrabbable` wurde verwendet, damit der Spieler während dem Greifen den Button nicht ständig gedrückt halten muss. Es reicht ein kurzes Betätigen des Buttons zum Greifen und ein weiteres Mal betätigen zum Loslassen. Während der Spieler die Platte in der Hand hält und Objekte auf der Platte manipuliert, kann es passieren, dass er ungewünscht die Platte (statt eines Objekts) nochmals mit dem anderen Controller greift. Um dies zu verhindern, wird das `StickyGrabbable` Skript deaktiviert, sobald es gegriffen wurde und wieder aktiviert, wenn es losgelassen wird.

Falls die Platte außer Reichweite des Spielers ist, kann diese zum Spieler teleportiert werden. In einer ersten Version wurde hierzu ein Button auf dem Controller belegt, welcher das zu sich teleportieren der Platte ausführt. Dies wurde in einer weiteren Version entfernt, da es nur eine begrenzte Anzahl von Buttons gibt, welche meist schon durch andere Aktionen belegt sind (z. B. Greifen; Teleportieren, falls verwendet; VIVE button). Stattdessen wurde ein Menü implementiert, welches durch das Betätigen des Menü-Buttons vor dem Spieler erscheint. In diesem Menü kann der Spieler unter anderem die Platte zu sich teleportieren.

Für das Teleportieren eines Objekts zum Spieler wurde das Skript `TeleportThisToPlayer` geschrieben. Dieses teleportiert das Objekt, welches dieses Skript besitzt zur Spielerposition (die Position der Camera). Damit das Objekt nicht im Kopf des Spielers hängt, wurde ein Offset in Blickrichtung verwendet. Damit das Objekt nicht zu hoch platziert wird, kann noch ein y-offset (relativ zur Kopfhöhe) angegeben werden. Dadurch kann festgelegt werden, dass das Objekt z. B. 0,4 Meter unterhalb des Kopfes und 1 Meter vor den Spieler teleportiert werden soll. Dieses Skript wurde für die 3D Miniatur und für das Menü verwendet.

#### Zurücksetzen der Welt
Damit der Spieler wieder in einer neuen Welt starten kann, wurde im Menü ein Neustart-Button hinzugefügt. Über diesen wird die Scene neu geladen.

### Verwenden von WIM in einer eigenen Scene
1. In der eigenen Scene ein neues leeres Objekt erstellen und das `MiniWorld` Skript diesem Objekt hinzufügen. Die Positionierung ist nicht relevant, da diese zur Laufzeit an die von `origin` angepasst wird
2. In `origin` den Ursprung der 3D Miniatur festlegen (kann z. B. ein Tisch sein oder eine bewegbare Platte)
3. Optional kann der Origin Offset festgelegt werden, damit z. B. die 3D Miniatur nicht im origin-Objekt drin hängt
4. Alle Objekte die in der Miniatur inkludiert werden sollen, müssen im Inspector bei dem `MiniWorld` Skript unter `Real Objects` hinzugefügt werden
5. Dem leeren Objekt das `WIM Character` Prefab hinzufügen. Die Positionierung ist nicht relevant, da diese zur Laufzeit an die des Nutzers angepasst wird
6. Um die Skalierung anzupassen, den Scale Parameter von dem leeren Objekt entsprechend anpassen, z. B. 0.02 für x, y und z für eine Verhältnis von 1:50 von Miniatur zur realen Welt

## Ausblick
Die aktuelle Implementierung könnte noch wie folgt erweitert werden:
- Es könnte noch ein Zoomen und Scrollen der WIM, wie diese in [3] oder [4] beschrieben ist, implementiert werden, um auch größere Welten erkennbar darzustellen
- Zurzeit werden reale Objekte komplett in die Miniatur übernommen. Dies könnte zu Performance Problemen führen, wenn die zu klonenden Objekte eine hohe Auflösung besitzen (viele Dreiecke). Es könnte untersucht werden, ob und wie der Klon mit einer reduzierten Auflösung erstellt werden kann. Alternativ könnten z. B. bei Objekten mit vielen kleinen Details, einige Details im Klon weggelassen werden.
- Bei virtuellen Welten mit begehbaren Gebäuden ist die aktuelle Implementierung nicht geeignet, da das innere von Gebäuden nicht sichtbar ist. Die verdeckenden Kanten bzw. Mauern könnten in der Miniatur entfernt werden, damit der Nutzer auch in Gebäude hineinschauen kann (Vgl. [5], [6]). 
- Zurzeit können Objekte in allen sechs Freiheitsgraden bewegt werden. In einigen Szenarien könnte dies eventuell keinen Sinn ergeben, z. B. das auf den Kopf stellen eines Gebäudes. Deshalb könnte die Implementierung erweitert werden, um die Bewegung in ausgewählte Achsen zu sperren. Alternativ könnten bei einem ausgewählten Objekt, auch die bewegbaren Achsen angezeigt werden. Ähnlich wie im Unity Editor könnte der Nutzer dann nur die gewünschten Achsen manipulieren, z. B. nur in y-Richtung verschieben.
- eine genaue Ausrichtung von Objekten ist in der aktuellen Implementierung nur schwer möglich, da Bewegungen in der Miniatur durch die Skalierung in der realen Welt verstärkt werden. Eine Erweiterung könnte es dem Spieler ermöglichen, die Bewegungsgeschwindigkeit anzupassen, um dadurch auch genauere Positionierungen zu ermöglichen. 

## Literatur
[1] Vive: _What are the minimum and maximum play area when using more than two SteamVR Base Stations 2.0?_, URL: https://business.vive.com/eu/support/vive-pro/category_howto/minimum-and-maximum-play-area-size-for-more-than-2-base-stations.html (zuletzt besucht am 13.07.2021)

[2] Richard Stoakley, Matthew J. Conway, Randy Pausch: _Virtual Reality on a WIM: Interactive Worlds in Miniature_, 1995

[3] Chadwick A. Wingrave, Yonca Haciahmetoglu, Doug A. Bowman: _Overcoming World in Miniature Limitations by a Scaled and Scrolling WIM_, 2006

[4] David Englmeier, Wanja Sajko, Andreas Butz: _Spherical World in Miniature: Exploring the Tiny Planets Metaphor for Discrete Locomotion in Virtual Reality_, 2021

[5] Ramón Trueba, Carlos Andújar: _Dynamic Worlds in Miniature_, 2008

[6] Ramón Trueba, Carlos Andújar, Ferran Argelaguet: _World-in-Miniature Interaction for Complex Virtual Environments_, 2010

# AVR21-7
Repository for Kronhardt


# World in a Miniature (WIM)
WIM stellt eine Möglichkeit für das Travelling und die Objektselektion/-manipulation innerhalb einer virtuellen Welt dar. Dabei wird die virtuelle Welt als 3D Miniatur dargestellt, welche der Benutzer in der Hand halten kann. Darin kann er Objekte auswählen und manipulieren. In der 3D Miniatur wird der Benutzer selbst als Charakter dargestellt. Dieser Miniaturcharakter kann an eine Stelle in der 3D Miniatur bewegt werden, um sich selbst innerhalb der virtuellen Welt dorthin zu teleportieren.


## Required packages
- VIVE Wave XR Plugin; version 4.0.0-r.39
- VIVE Input Utility; version 1.13.1


## Kurzanleitung zur Benutzung
- alles bis auf Straßen, Tisch und Boden kann bewegt werden
- Raycast auf Objekt ausrichten und Trigger-Button gedrückt halten um Objekte zu bewegen
- Charakter bewegen und loslassen um zu teleportieren
- die 3D Miniatur-Platte ist "sticky", d. h. einmal Trigger betätigen zum greifen und ein weiteres mal zum Loslassen (Platte muss mit Controller berührt werden)
- Menu-Button betätigen, um das Menu zu öffnen (ermöglicht WIM zu sich zu teleportieren und Restart)


## Anleitung zur Benutzung
Nach dem Starten der Anwendung befindet sich der Benutzer in einem kleinen Teil einer virtuellen Stadt. Neben ihm schwebt eine Platte mit einer 3D Miniatur der virtuellen Welt. Zusätzlich befindet sich in der virtuellen Welt ein Tisch, auf welchem sich ebenfalls eine 3D Miniatur der Welt befindet. Es kann sowohl die "portable" Version als auch die Version auf dem Tisch manipuliert werden. Für die Manipulation können die Controller wie folgt verwendet werden:


#### Objekt Manipulation
Beide Controller strahlen einen Raycast nach vorne aus. Ein Raycast kann auf ein Objekt gerichtet werden und mit gedrücktem Trigger-Button kann das Objekt bewegt werden. Es kann sowohl die 3D Miniatur, als auch das "reallife" Objekt bewegt werden. Wird ein Objekt in der 3D Miniatur bewegt, bewegt sich das entsprechende "reallife" Objekt in Echtzeit mit. Umgekehrt gilt das selbe.

Es können bis auf die Straßen, den Tisch (mit der 3D Miniatur) und den Boden alle Objekte bewegt werden.


#### Teleportation
In den 3D Miniaturansichten befindet sich ein Charakter (umhüllt mit einer durchsichtigen Kugel). Dieser Charakter stellt den Benutzer dar und kann ebenfalls mit dem Raycast und dem Trigger-Button bewegt werden. Wird der Charakter an eine Stelle in der 3D Miniatur bewegt und losgelassen, dann wird der Benutzer an die entsprechende Stelle in der virtuellen Welt teleportiert. Es wird nur die y-Rotation des Charakters berücksichtigt, um die Blickrichtung des Benutzers auszurichten.


#### Bewegen der schwebenden 3D Miniatur
Die 3D Miniatur, welche sich nicht auf dem Tisch befindet kann bewegt werden. Hierzu muss die Platte, welche die 3D Miniatur trägt, mit einem Controller berührt werden und dann der Trigger-Button betätigt werden (das Bewegen funktioniert nicht mit dem Raycast wie bei den Objekten). Die Platte ist "sticky", d. h. der Trigger-Button muss nicht gedrückt gehalten werden. Um die Platte loszulassen, muss der Trigger-Button erneut betätigt werden. Wenn die Platte bereits mit einem Controller gehalten wird, kann sie nicht mit einem weiteren Controller "gegriffen" werden. Während die Platte mit einem Controller gehalten wird, können mit dem anderen Controller die Objekte auf der Platte bewegt werden (wie in "Objekt Manipulation" beschrieben).


#### Teleportieren der schwebenden 3D Miniatur
Die 3D Miniatur kann auch zum Spieler teleportiert werden. Hierzu muss der Menu-Button betätigt werden, um das Menu zu öffnen. Dort dann den entsprechenden Button auf dem UI auswählen.


#### Welt zurücksetzen
Um die Änderungen an der Welt zurückzusetzen, den Menu-Button betätigen, um das Menu zu öffnen. Dort dann den entsprechenden Button auf dem UI auswählen.


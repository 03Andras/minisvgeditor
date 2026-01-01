📘 Mini SVG Szerkesztő – Projekt Dokumentáció

1. A projekt rövid leírása és célja
A Mini SVG Szerkesztő egy Windows Forms alapú vektorgrafikus rajzolóprogram, amely C# nyelven készült. Az alkalmazás célja, hogy a felhasználó egyszerű eszközökkel geometriai alakzatokat rajzolhasson, majd SVG formátumban elmenthesse vagy később visszatölthesse.
A projekt elsősorban oktatási célt szolgál, mivel jól szemlélteti a vektoros grafika működését, az SVG fájlformátum felépítését, valamint az alapvető geometriai transzformációk (nagyítás, eltolás, forgatás) gyakorlati megvalósítását.

2. A megoldás felépítése és működési elve
2.1 Az alkalmazás általános felépítése
Az alkalmazás grafikus felhasználói felülete Windows Forms technológiával készült. A főablak központi eleme egy Panel vezérlő, amely rajzolási felületként szolgál. Minden grafikus művelet ezen a panelen történik, az egér eseményeinek felhasználásával.
Az alkalmazás működésének alapja egy lista (List<Shape>), amely a felhasználó által létrehozott összes alakzatot tartalmazza. Ez az adatszerkezet lehetővé teszi, hogy a rajz bármikor újrarajzolható legyen, például ablakfrissítés vagy transzformáció alkalmazása esetén.
! Megjegyzés:
A Shape osztály kialakítása, az alap rajzolási logika és az SVG mentés funkció András munkájának része, amely a végleges dokumentációban külön kerül bemutatásra.
________________________________________
2.2 Eseménykezelés és rajzolási folyamat
A rajzolás az egér eseményein keresztül valósul meg. A MouseDown esemény során az alkalmazás eltárolja a rajzolás kezdőpontját, majd a MouseMove esemény segítségével folyamatosan frissíti az ideiglenes végpontot. Ez lehetővé teszi, hogy rajzolás közben a felhasználó azonnali vizuális visszajelzést kapjon.
A MouseUp esemény lezárja a rajzolási folyamatot. Ekkor az ideiglenes adatok alapján egy végleges Shape objektum jön létre, amely bekerül az alakzatokat tároló listába. Az alakzatok megjelenítése minden esetben a Paint esemény során történik, amely végigiterál a tárolt listán, és kirajzolja az elemeket.
Ez a megoldás biztosítja, hogy a rajz mindig konzisztens maradjon, és bármikor újrarajzolható legyen.
________________________________________
2.3 Eszter által hozzáadott működési elemek
Eszter feladata volt az alkalmazás kiterjesztése olyan funkciókkal, amelyek a rajzoláson túlmutatnak, és a programot egy valódi szerkesztőhöz közelítik. Ezek közé tartozik az SVG fájlok betöltése, valamint a rajzfelület nézetének kezelése.
Az új funkciók kialakítása során fontos szempont volt, hogy a meglévő rendszerbe illeszkedjenek, és ne igényeljék az alapstruktúra átalakítását.
________________________________________
3. A legfontosabb megvalósítási részletek
3.1 SVG fájlok betöltése 
Az SVG betöltési funkció célja, hogy a felhasználó korábban elmentett rajzokat újra meg tudjon nyitni, és azokon további módosításokat végezhessen.
A betöltés során az alkalmazás XML dokumentumként kezeli az SVG fájlt, mivel az SVG formátum XML-alapú.
A program a dokumentum gyökérelemének gyermekelemein végighalad, és az egyes grafikus elemek típusától függően (line, rect, ellipse) kiolvassa a szükséges attribútumokat, például koordinátákat és színadatokat. Ezekből az adatokból új Shape objektumok jönnek létre, amelyek bekerülnek az alakzatokat tartalmazó listába.
A betöltési folyamat elején a program törli a korábbi alakzatokat, ezzel biztosítva, hogy a betöltött rajz önállóan jelenjen meg. A feldolgozás végén a rajzfelület újrarajzolása automatikusan megtörténik.
________________________________________
3.2 Zoom megvalósítása és korlátozása 
A zoom funkció célja a rajz részleteinek könnyebb megtekintése. A nagyítás és kicsinyítés az egérgörgő segítségével történik, és egy globális változó szabályozza a mértékét.
A zoom értéke meghatározott alsó és felső határok között marad, így elkerülhető a túlzott nagyítás vagy a rajz eltűnése. A nagyítás a rajzolás során grafikus skálázásként kerül alkalmazásra, így az alakzatok arányai nem torzulnak.
________________________________________
3.3 Geometriai transzformációk alkalmazása 
A zoom funkció mellett további geometriai transzformációk is megvalósításra kerültek. A forgatás és az eltolás billentyűkombinációk és egérgörgő segítségével érhető el, ami gyors és intuitív kezelést biztosít.
A transzformációk a grafikus objektumon keresztül kerülnek alkalmazásra a Paint eseményben. A transzformációk sorrendje kiemelten fontos, mivel a helytelen sorrend torz vagy nem várt megjelenítést eredményezhet. A megvalósítás során az eltolás, forgatás és skálázás előre meghatározott sorrendben történik, így a rajz megjelenítése stabil és kiszámítható marad.


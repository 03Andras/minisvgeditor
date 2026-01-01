# 🚀 Hogyan indítsd el a Mini SVG Szerkesztőt?

Ez egy rövid útmutató, hogy hogyan tudd elindítani és használni a Mini SVG Szerkesztő projektet.

---

## 📋 Követelmények

- **Visual Studio 2022** vagy újabb verzió
- **.NET Framework** (a projekt .NET Framework-re épül)
- **Windows operációs rendszer**

---

## 🔧 Projekt megnyitása

1. **Projekt letöltése/klónozása:**
   - Ha GitHubról klónozod: `git clone <repository_url>`
   - Vagy töltsd le ZIP fájlként és csomagold ki

2. **Solution megnyitása:**
   - Nyisd meg a Visual Studio 2022-t
   - Válaszd a **File → Open → Project/Solution** menüpontot
   - Navigálj a projekt mappájába
   - Válaszd ki a `minisvgeditor.sln` fájlt
   - Kattints az **Open** gombra

3. **Projekt betöltése:**
   - A Visual Studio automatikusan betölti a projektet
   - A Solution Explorer-ben láthatod a projekt fájljait

---

## ▶️ Projekt elindítása

### Módszer 1: Start gomb használata
1. A Visual Studio tetején található eszköztáron keresd meg a **zöld Start (Play)** gombot
2. Kattints rá, vagy nyomd meg az **F5** billentyűt
3. A program elindul és megjelenik a rajzolási ablak

### Módszer 2: Debug menü
1. Válaszd a **Debug → Start Debugging** menüpontot (vagy **F5**)
2. Vagy válaszd a **Debug → Start Without Debugging** menüpontot (vagy **Ctrl+F5**) ha hibakeresés nélkül szeretnéd futtatni

### Módszer 3: Fordítás és futtatás külön
1. Először fordítsd le a projektet: **Build → Build Solution** (vagy **Ctrl+Shift+B**)
2. Ha a fordítás sikeres, indítsd el: **Debug → Start Debugging** (vagy **F5**)

---

## 🎨 A program használata

Miután a program elindult, a következő lehetőségeid vannak:

### Rajzolás
1. **Válassz rajzolási módot:**
   - Kattints a **Vonal**, **Téglalap** vagy **Ellipszis** gombra

2. **Válassz színt:**
   - Kattints a **színválasztó** gombra
   - Válaszd ki a kívánt színt a palettáról
   - A gomb háttérszíne mutatja az aktuális rajzolási színt

3. **Rajzolj:**
   - Kattints és tartsd lenyomva az egérgombot a rajzolási területen
   - Húzd az egeret a kívánt irányba
   - Engedd el az egérgombot a rajz befejezéséhez

### Mentés és betöltés
- **Mentés:** Kattints a **Mentés** gombra, válaszd ki a mentési helyet és add meg a fájl nevét (pl. `rajzom.svg`)
- **Betöltés:** Kattints a **Betöltés** gombra, válaszd ki a megnyitni kívánt SVG fájlt

### Nézet módosítása
- **Zoom (nagyítás/kicsinyítés):** Görgess az egérgörgővel felfelé (nagyítás) vagy lefelé (kicsinyítés)
- **Eltolás:** Tartsd nyomva a **Shift** billentyűt és görgess az egérgörgővel
- **Forgatás:** Tartsd nyomva a **Ctrl** billentyűt és görgess az egérgörgővel

---

## ⚠️ Gyakori problémák és megoldások

### A projekt nem fordul le
- **Ellenőrizd a .NET Framework telepítését:** Győződj meg róla, hogy a megfelelő .NET Framework verzió telepítve van
- **NuGet csomagok helyreállítása:** Jobb klikk a Solution-re → **Restore NuGet Packages**
- **Tisztítsd meg és fordítsd újra:** **Build → Clean Solution**, majd **Build → Rebuild Solution**

### A Start gomb szürke/inaktív
- Ellenőrizd, hogy a projekt helyesen be van-e töltve
- Állítsd be a `minisvgeditor` projektet startup projektként: jobb klikk a projektre → **Set as Startup Project**

### A program elindul, de nem látszik az ablak
- Ellenőrizd, hogy a `Form1` megfelelően van-e beállítva a `Program.cs`-ben
- Próbáld újraindítani a Visual Studio-t

### Hibás SVG betöltés
- Ellenőrizd, hogy a betölteni kívánt fájl valóban SVG formátumú
- Győződj meg róla, hogy az SVG fájl az alkalmazás által támogatott elemeket tartalmaz (line, rect, ellipse)

---

## 📝 Megjegyzések

- A projekt Windows Forms alkalmazás, így **csak Windows** rendszeren futtatható
- A program **Visual Studio 2022**-ben készült, de újabb verziókban is gond nélkül működik
- Az elmentett SVG fájlok bármilyen böngészőben vagy SVG-t támogató alkalmazásban megnyithatók

---

## 🎓 Fejlesztői információk

Ha szeretnéd továbbfejleszteni a projektet:
- A főablak logikája a `Form1.cs` fájlban található
- Az alakzatok adatstruktúrája a `Shape.cs` fájlban van definiálva
- A felhasználói felület a `Form1.Designer.cs` fájlban van leírva (ezt általában a vizuális szerkesztővel módosítod)

---

**Jó rajzolást! 🎨**

*Ha bármilyen problémád van, ellenőrizd, hogy a Visual Studio megfelelően van-e telepítve és beállítva.*

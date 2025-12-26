
# 📘 Mini SVG Szerkesztő – Projekt Dokumentáció

---

## 1️⃣ **GitHub és Visual Studio összekapcsolása**

Ha Eszter szeretne csatlakozni a projekthez és párhuzamosan dolgozni, a következő lépéseket kell követni:

**Eszter csatlakozása**
   - Eszter klónozza a repository-t:  
     **GitHub → Code → Clone URL → Visual Studio → Clone a repository**.
   - Ezután létrehozhat saját branch-et:
     - `eszter-dev` → fejlesztési feladatokhoz.
   - Pull Request használata a módosítások összevonásához.

---

## 2️⃣ **2025-12-14 – András megoldotta**

### **Elkészült funkciók és alapok**
- **Projekt létrehozása**: Windows Forms App (.NET Framework), C# nyelven.
- **Rajzolási felület**: Panel (`panel1`) hozzáadva, méret és háttérszín beállítva.
- **Shape osztály**:  
  - Tárolja az alakzat típusát (Line, Rect, Ellipse), kezdő- és végpontját, valamint a színt.
- **Globális változók**:  
  - `shapes` lista az alakzatokhoz.
  - `startPoint`, `endPoint` a rajzoláshoz.
  - `isDrawing` jelző, `currentShapeType`, `currentColor`.
- **Rajzolási logika**:
  - `MouseDown` → kezdőpont mentése.
  - `MouseMove` → ideiglenes rajz.
  - `MouseUp` → végpont mentése, alakzat hozzáadása a listához.
- **Paint esemény**:
  - Kirajzolja az összes végleges alakzatot.
  - Megjeleníti az ideiglenes alakzatot rajzolás közben.
- **Rajzolási mód váltó gombok**:
  - Vonal, Téglalap, Ellipszis.
- **Színválasztó gomb**:
  - ColorDialog segítségével szín kiválasztása.
  - A gomb háttérszíne frissül a kiválasztott színre.
- **SVG mentés funkció**:
  - `SaveToSVG` metódus: XmlWriter segítségével menti az alakzatokat SVG formátumba.
  - Mentés gomb: SaveFileDialog megnyitása, fájl mentése, visszajelzés.

### **Jelenlegi képességek**
- Rajzolás (vonal, téglalap, ellipszis).
- Színválasztás vizuális visszajelzéssel.
- SVG fájl mentése és böngészőben megnyitása.

---

## 3️⃣ **Eszter- fejlesztési tervek (tananyag alapján)**

### **Feladatok és célok**
Eszter feladata a projekt bővítése a következő funkciókkal:

#### **1. SVG betöltés (Import funkció)**
- Új gomb: `Betöltés SVG-ből`.
- Eseménykezelő:
  - Fájlválasztó ablak megnyitása.
  - SVG fájl beolvasása XML formátumból.
  - `<line>`, `<rect>`, `<ellipse>` elemek feldolgozása.
  - Minden elemhez `Shape` objektum létrehozása és hozzáadása a `shapes` listához.
  - Rajzfelület újrarajzolása.

#### **2. Zoom funkció (Egérgörgő)**
- Globális `zoom` változó létrehozása.
- `MouseWheel` esemény kezelése:
  - Görgetés irányától függően növelje vagy csökkentse a zoom értékét.
- A `Paint` eseményben alkalmazza a skálázást.

#### **3. Alap transzformációk**
- Eltolás és forgatás implementálása:
  - Példa: Ctrl + egérgörgő → forgatás.
  - Shift + egérgörgő → eltolás.
  - (ezek példák- bármi kombináció lehet - rád bízom)
- A transzformációk alkalmazása a rajzolási logikában.

#### **4. Extra funkciók (opcionális)**
- Bézier-görbék rajzolása (de Casteljau algoritmus).
- Rétegek kezelése.
- Export PNG-be (raszteres mentés).

---

### **Irányelvek Eszter számára**
- Használja a meglévő `Shape` osztályt.
- Import előtt törölje a `shapes` listát (`shapes.Clear()`).
- Tesztelés: rajzolás → mentés → betöltés → ellenőrzés.
- A zoom és transzformációk implementálásakor ügyeljen a sorrendre (skálázás → eltolás → forgatás).

---

## ✅ **Várható eredmény**
A projekt képes lesz:
- SVG fájlok betöltésére és újrarajzolására.
- Zoomolásra és alap transzformációkra.
- Felhasználói élmény jelentős javítására.

---

📌 **Megjegyzés:** A fejlesztési terv a kurzus tananyagán alapul (vektoros képek, transzformációk, SVG formátum – lásd: *Számítógépes grafikazet, 9–12. fejezet).

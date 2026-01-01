# 📘 Mini SVG Szerkesztő – Dokumentáció

**Készítette:** Nagy András és Eszter Lakó  
**Dátum:** 2025.12.30  
**Kurzus:** A számítógépes grafika  
**Projekt típus:** Szemesztrális munka  
**Projekt megnevezése:** Saját SVG-szerkesztő mini program megvalósítása

---

## 1. Bevezetés

A Mini SVG Szerkesztő egy egyszerű, de funkciógazdag vektorgrafikus rajzolóprogram, amit Windows Forms segítségével valósítottunk meg C# nyelven. A program lehetővé teszi, hogy a felhasználó egyszerűen rajzolhasson geometriai alakzatokat (vonalakat, téglalapokat, ellipsziseket), majd ezeket SVG formátumban elmenthesse vagy később újra betölthesse.

A projekt célja kettős volt: egyrészt gyakorlati tapasztalatot szerezni a vektoros grafika kezelésében és az SVG fájlformátum felépítésében, másrészt megvalósítani egy működő alkalmazást, amely szemlélteti az alapvető geometriai transzformációk (zoom, eltolás, forgatás) gyakorlati alkalmazását.

---

## 2. A projekt felépítése és működése

### 2.1 Általános felépítés

Az alkalmazás Windows Forms technológiával készült, ami biztosítja a grafikus felületet. A főablak központi eleme egy `Panel` vezérlő, amely rajzolási felületként szolgál. Minden grafikus művelet ezen a panelen történik meg.

A program alapja egy `List<Shape>` adatszerkezet, amely az összes létrehozott alakzatot tárolja. Ez a megoldás biztosítja, hogy a rajz bármikor újrarajzolható legyen, például ablakfrissítés vagy transzformáció alkalmazása esetén.

### 2.2 A Shape osztály

A `Shape` osztály az alapegység, ami egy rajzolt alakzatot reprezentál. Az osztály definíciója:

```csharp
internal class Shape
{
    public string Type { get; set; }    // "Line", "Rect", "Ellipse"
    public Point Start { get; set; }    // Kezdőpont
    public Point End { get; set; }      // Végpont
    public Color Color { get; set; }    // Szín
    public int Layer { get; set; } = 0; // Réteg (későbbi bővítéshez)
}
```

Minden alakzat típusa, kezdő- és végpontja, valamint színe tárolódik. A `Layer` tulajdonság későbbi bővítési lehetőséget biztosít rétegkezeléshez.

### 2.3 Rajzolási mechanizmus

A rajzolás az egér eseményein keresztül történik:

1. **MouseDown esemény**: Amikor a felhasználó lenyomja az egeret, eltároljuk a kezdőpontot és beállítjuk az `isDrawing` jelzőt `true`-ra.

```csharp
private void panel_MouseDown(object sender, MouseEventArgs e)
{
    isDrawing = true;
    startPoint = e.Location;
}
```

2. **MouseMove esemény**: Rajzolás közben folyamatosan frissítjük a végpontot és újrarajzoljuk a panelt, így a felhasználó látja az ideiglenes alakzatot.

```csharp
private void panel_MouseMove(object sender, MouseEventArgs e)
{
    if (isDrawing)
    {
        endPoint = e.Location;
        panel1.Invalidate();    // Újrarajzolás kérése
    }
}
```

3. **MouseUp esemény**: Az egér elengedésekor véglegesítjük az alakzatot, létrehozunk egy új `Shape` objektumot és hozzáadjuk a listához.

```csharp
private void panel1_MouseUp(object sender, MouseEventArgs e)
{
    if (isDrawing)
    {
        isDrawing = false;
        endPoint = e.Location;
        
        Shape newShape = new Shape
        {
            Type = currentShapeType,
            Start = startPoint,
            End = endPoint,
            Color = currentColor
        };
        
        shapes.Add(newShape);
        panel1.Invalidate();
    }
}
```

4. **Paint esemény**: Itt történik a tényleges kirajzolás. A program végigmegy az összes eltárolt alakzaton és kirajzolja őket.

```csharp
private void panel1_Paint(object sender, PaintEventArgs e)
{
    // Transzformációk alkalmazása
    e.Graphics.TranslateTransform(offsetX, offsetY);
    e.Graphics.RotateTransform(rotationAngle);
    e.Graphics.ScaleTransform(zoom, zoom);
    
    // Végleges alakzatok rajzolása
    foreach (var shape in shapes)
    {
        using (Pen pen = new Pen(shape.Color, 2))
        {
            if (shape.Type == "Line")
                e.Graphics.DrawLine(pen, shape.Start, shape.End);
            else if (shape.Type == "Rect")
                e.Graphics.DrawRectangle(pen, GetRectangle(shape.Start, shape.End));
            else if (shape.Type == "Ellipse")
                e.Graphics.DrawEllipse(pen, GetRectangle(shape.Start, shape.End));
        }
    }
    
    // Ideiglenes alakzat rajzolása (ha éppen rajzolunk)
    if (isDrawing)
    {
        using (Pen pen = new Pen(currentColor, 2))
        {
            if (currentShapeType == "Line")
                e.Graphics.DrawLine(pen, startPoint, endPoint);
            // ... stb.
        }
    }
}
```

---

## 3. Alapfunkciók

### 3.1 Rajzolási módok

A program három alapvető alakzatot támogat:
- **Vonal** (Line): Egyszerű egyenes szakasz két pont között
- **Téglalap** (Rectangle): Négyszög alakzat
- **Ellipszis** (Ellipse): Ovális alakzat

A felhasználó gombokkal válthat a rajzolási módok között. A kiválasztott mód határozza meg, hogy milyen alakzat jön létre az egér műveletei alapján.

### 3.2 Színválasztás

A felhasználó szabadon választhat színt a `ColorDialog` segítségével. A kiválasztott szín a színválasztó gomb háttérszínén is látható, így mindig egyértelmű, hogy milyen színnel rajzolunk.

```csharp
private void colorvalaszto_Click(object sender, EventArgs e)
{
    using (ColorDialog cd = new ColorDialog())
    {
        if (cd.ShowDialog() == DialogResult.OK)
        {
            currentColor = cd.Color;
            colorvalaszto.BackColor = currentColor; // Vizuális visszajelzés
        }
    }
}
```

---

## 4. SVG mentés és betöltés

### 4.1 Mi az SVG?

Az SVG (Scalable Vector Graphics) egy XML-alapú vektorgrafikus fájlformátum. Az SVG előnye, hogy a képek minőségvesztés nélkül méretezhetők, mivel matematikai leírásokat tárolnak, nem pedig pixeleket.

Egy egyszerű SVG fájl példa:

```xml
<svg xmlns="http://www.w3.org/2000/svg" width="800" height="600">
    <line x1="10" y1="10" x2="100" y2="100" stroke="#000000" stroke-width="2"/>
    <rect x="50" y="50" width="100" height="80" stroke="#FF0000" fill="none" stroke-width="2"/>
    <ellipse cx="200" cy="150" rx="50" ry="30" stroke="#0000FF" fill="none" stroke-width="2"/>
</svg>
```

### 4.2 SVG mentés

A program az `XmlWriter` osztályt használja SVG fájlok létrehozására. A mentési folyamat során:
1. Létrehozunk egy XML dokumentumot SVG gyökérelemmel
2. Beállítjuk a rajzolási terület szélességét és magasságát
3. Végigmegyünk az összes alakzaton és XML elemként írjuk őket

```csharp
private void SaveToSVG(string filePath)
{
    using (XmlWriter writer = XmlWriter.Create(filePath, new XmlWriterSettings { Indent = true }))
    {
        writer.WriteStartDocument();
        writer.WriteStartElement("svg", "http://www.w3.org/2000/svg");
        writer.WriteAttributeString("width", panel1.Width.ToString());
        writer.WriteAttributeString("height", panel1.Height.ToString());
        
        foreach (var shape in shapes)
        {
            if (shape.Type == "Line")
            {
                writer.WriteStartElement("line");
                writer.WriteAttributeString("x1", shape.Start.X.ToString());
                writer.WriteAttributeString("y1", shape.Start.Y.ToString());
                writer.WriteAttributeString("x2", shape.End.X.ToString());
                writer.WriteAttributeString("y2", shape.End.Y.ToString());
                writer.WriteAttributeString("stroke", ColorTranslator.ToHtml(shape.Color));
                writer.WriteAttributeString("stroke-width", "2");
                writer.WriteEndElement();
            }
            // ... téglalap és ellipszis hasonlóan
        }
        
        writer.WriteEndElement();
        writer.WriteEndDocument();
    }
}
```

**Példa egy mentett vonalra:**
- A program egy `Shape` objektumot (`Type="Line"`, `Start=(10,20)`, `End=(100,150)`, `Color=Black`) a következő SVG elemként menti:
```xml
<line x1="10" y1="20" x2="100" y2="150" stroke="#000000" stroke-width="2"/>
```

### 4.3 SVG betöltés

A betöltési funkció lehetővé teszi, hogy korábban elmentett rajzokat újra megnyithassunk és szerkeszthessünk. A betöltés során:
1. XML dokumentumként beolvassuk az SVG fájlt
2. Törljük a jelenlegi alakzatokat (`shapes.Clear()`)
3. Végigmegyünk az SVG elemeken és minden elemnél létrehozunk egy megfelelő `Shape` objektumot

```csharp
private void LoadFromSVG(string filePath)
{
    XmlDocument doc = new XmlDocument();
    doc.Load(filePath);
    
    shapes.Clear(); // Régi alakzatok törlése
    
    XmlNodeList nodes = doc.DocumentElement.ChildNodes;
    
    foreach (XmlNode node in nodes)
    {
        if (node.NodeType != XmlNodeType.Element)
            continue;
        
        if (node.Name == "line")
        {
            int x1 = int.Parse(node.Attributes["x1"].Value);
            int y1 = int.Parse(node.Attributes["y1"].Value);
            int x2 = int.Parse(node.Attributes["x2"].Value);
            int y2 = int.Parse(node.Attributes["y2"].Value);
            Color color = ColorTranslator.FromHtml(node.Attributes["stroke"].Value);
            
            shapes.Add(new Shape
            {
                Type = "Line",
                Start = new Point(x1, y1),
                End = new Point(x2, y2),
                Color = color
            });
        }
        // ... téglalap és ellipszis hasonlóan
    }
    
    panel1.Invalidate(); // Újrarajzolás
}
```

---

## 5. Extra funkciók

A program alap elkészítése után több extra funkciót is implementáltunk, amelyek a felhasználói élményt javítják és közelebb hozzák a programot egy professzionális rajzolóalkalmazáshoz.

### 5.1 Zoom (nagyítás/kicsinyítés)

A zoom funkció lehetővé teszi a rajz részleteinek könnyebb megtekintését. Az egérgörgő egyszerű használatával nagyíthatunk vagy kicsinyíthetünk.

**Működés:**
- Egérgörgő fel: nagyítás (zoom növelése)
- Egérgörgő le: kicsinyítés (zoom csökkentése)
- A zoom értéke 0.1 és 5.0 között mozoghat (10%-tól 500%-ig)

```csharp
private float zoom = 1.0f; // 100% alap zoom

private void panel1_MouseWheel(object sender, MouseEventArgs e)
{
    const float zoomStep = 0.1f;
    
    if (!isCtrl && !isShift)
    {
        zoom += e.Delta > 0 ? zoomStep : -zoomStep;
        zoom = Math.Max(0.1f, Math.Min(5.0f, zoom)); // Korlátozás
    }
    
    panel1.Invalidate();
}
```

A zoom alkalmazása a `Paint` eseményben történik a `ScaleTransform` segítségével:
```csharp
e.Graphics.ScaleTransform(zoom, zoom);
```

### 5.2 Eltolás (panning)

Az eltolás funkció lehetővé teszi, hogy a rajzfelületet mozgassuk anélkül, hogy az alakzatokat módosítanánk. Ez különösen hasznos nagyított nézetben.

**Működés:**
- Shift + egérgörgő: vízszintes eltolás

```csharp
private float offsetX = 0f;
private float offsetY = 0f;

private void panel1_MouseWheel(object sender, MouseEventArgs e)
{
    const float panStep = 10f;
    bool isShift = (ModifierKeys & Keys.Shift) == Keys.Shift;
    
    if (isShift && !isCtrl)
    {
        offsetX += e.Delta > 0 ? panStep : -panStep;
    }
    
    panel1.Invalidate();
}
```

### 5.3 Forgatás (rotation)

A forgatás funkció lehetővé teszi a teljes rajzfelület elforgatását.

**Működés:**
- Ctrl + egérgörgő: forgatás 5 fokos lépésekben

```csharp
private float rotationAngle = 0f;

private void panel1_MouseWheel(object sender, MouseEventArgs e)
{
    const float rotateStep = 5f;
    bool isCtrl = (ModifierKeys & Keys.Control) == Keys.Control;
    
    if (isCtrl && !isShift)
    {
        rotationAngle += e.Delta > 0 ? rotateStep : -rotateStep;
    }
    
    panel1.Invalidate();
}
```

### 5.4 Transzformációk sorrendje

A geometriai transzformációk alkalmazásának sorrendje kritikus fontosságú. A helytelen sorrend torz vagy nem várt eredményt okozhat. A programban az alábbi sorrendet használjuk:

1. **Eltolás** (TranslateTransform)
2. **Forgatás** (RotateTransform)
3. **Skálázás** (ScaleTransform)

```csharp
private void panel1_Paint(object sender, PaintEventArgs e)
{
    e.Graphics.TranslateTransform(offsetX, offsetY);   // 1. Eltolás
    e.Graphics.RotateTransform(rotationAngle);         // 2. Forgatás
    e.Graphics.ScaleTransform(zoom, zoom);             // 3. Zoom
    
    // ... alakzatok rajzolása
}
```

Ez a sorrend biztosítja, hogy a transzformációk természetes módon működjenek együtt.

---

## 6. Felhasználói felület

A program egyszerű és átlátható felhasználói felülettel rendelkezik:

**Gombok:**
- **Vonal**: Vonal rajzolási mód aktiválása
- **Téglalap**: Téglalap rajzolási mód aktiválása
- **Ellipszis**: Ellipszis rajzolási mód aktiválása
- **Színválasztó**: Rajzolási szín kiválasztása (a gomb háttere mutatja az aktuális színt)
- **Mentés**: SVG fájl mentése
- **Betöltés**: SVG fájl betöltése

**Rajzolási terület:**
- Nagy panel a képernyő nagy részén
- Fehér háttérrel
- Egerrel történő rajzolás

**Gyorsbillentyűk és gesztusok:**
- Egérgörgő: zoom
- Shift + egérgörgő: eltolás
- Ctrl + egérgörgő: forgatás

---

## 7. Technikai részletek

**Fejlesztői környezet:**
- Visual Studio 2022
- .NET Framework
- C# programozási nyelv
- Windows Forms technológia

**Használt osztályok és névterek:**
- `System.Drawing`: Grafikai műveletek (Point, Color, Pen, Graphics)
- `System.Windows.Forms`: Felhasználói felület
- `System.Xml`: SVG fájlok kezelése

**Fő komponensek:**
- `Form1`: Főablak és az alkalmazás központi logikája
- `Shape`: Alakzatok reprezentálása
- `Panel`: Rajzolási felület

---

## 8. Fejlesztési lehetőségek

A projekt tovább bővíthető lenne az alábbi funkciókkal:
- **Többszintű visszavonás (Undo/Redo)**: Stack adatszerkezet használatával
- **Rétegek kezelése**: Alakzatok csoportosítása rétegekbe
- **Bézier-görbék**: Összetettebb görbék rajzolása
- **Kitöltött alakzatok**: Nem csak a kontúr, hanem a belső terület is színezhető
- **Alakzatok kijelölése és módosítása**: Már létrehozott alakzatok szerkesztése
- **Export más formátumokba**: PNG, JPEG, stb.
- **Több rajzolási stílus**: Szaggatott vonalak, különböző vonalvastagságok

---

## 9. Összegzés

A Mini SVG Szerkesztő projekt sikeresen demonstrálja a vektorgrafika és az SVG fájlformátum gyakorlati alkalmazását. A program alapfunkcióinak elkészítése és az extra funkciók implementálása során gyakorlati tapasztalatot szereztünk a Windows Forms alkalmazásfejlesztésben, a grafikai műveletek programozásában és az XML feldolgozásban.

A projekt során megvalósított funkciók:
- ✅ Három alapvető alakzat rajzolása (vonal, téglalap, ellipszis)
- ✅ Szabadon választható rajzolási szín
- ✅ SVG formátumú fájlok mentése
- ✅ SVG fájlok betöltése és újrarajzolása
- ✅ Zoom funkció az egérgörgővel
- ✅ Eltolás funkció (Shift + egérgörgő)
- ✅ Forgatás funkció (Ctrl + egérgörgő)

A projekt jól szemlélteti, hogy viszonylag egyszerű eszközökkel is lehet működő és hasznos grafikus alkalmazást készíteni, amely valós problémákat old meg.

---

**Köszönjük a figyelmet!**

*Nagy András és Eszter Lakó*  
*2025.12.30*

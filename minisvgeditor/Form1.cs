
using System;
using System.Collections.Generic;
using System.Drawing; // Point és Color miatt
using System.Windows.Forms;
using System.Xml;

namespace minisvgeditor
{
    public partial class Form1 : Form
    {
        // Lista az alakzatokhoz
        private List<Shape> shapes = new List<Shape>();

        // Rajzoláshoz szükséges változók
        private Point startPoint;
        private Point endPoint;
        private bool isDrawing = false;
        private string currentShapeType = "Line"; // Alapértelmezett: vonal
        private Color currentColor = Color.Black; // Alapértelmezett szín

        private float zoom = 1.0f;           // 100% alap zoom
        private float offsetX = 0f;          // vízszintes eltolás
        private float offsetY = 0f;          // függőleges eltolás
        private float rotationAngle = 0f;    // forgatás fokban

        public Form1()
        {
            InitializeComponent();

            // MouseWheel esemény regisztrálása
            panel1.MouseWheel += panel1_MouseWheel;

            // Panel fókusz biztosítása
            panel1.Focus();
        }

        //Események:

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            startPoint = e.Location; // Kezdõpont mentése
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                endPoint = e.Location; // Frissítjük a végpontot
                panel1.Invalidate();    // Újrarajzolás kérése
            }
        }


        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                isDrawing = false;
                endPoint = e.Location; // Végpont mentése

                // Új alakzat létrehozása és hozzáadása a listához
                Shape newShape = new Shape
                {
                    Type = currentShapeType,
                    Start = startPoint,
                    End = endPoint,
                    Color = currentColor
                };

                shapes.Add(newShape);

                panel1.Invalidate(); // Újrarajzolás
            }
        }

        // Zoom esemény
        private void panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            const float zoomStep = 0.1f;
            const float rotateStep = 5f;
            const float panStep = 10f;

            bool isCtrl = (ModifierKeys & Keys.Control) == Keys.Control;
            bool isShift = (ModifierKeys & Keys.Shift) == Keys.Shift;

            if (isCtrl && !isShift)
            {
                // Ctrl + görgő → forgatás
                rotationAngle += e.Delta > 0 ? rotateStep : -rotateStep;
            }
            else if (isShift && !isCtrl)
            {
                // Shift + görgő → eltolás
                offsetX += e.Delta > 0 ? panStep : -panStep;
            }
            else if (!isCtrl && !isShift)
            {
                // sima görgő → zoom
                zoom += e.Delta > 0 ? zoomStep : -zoomStep;
                zoom = Math.Max(0.1f, Math.Min(5.0f, zoom));
            }
            // ha Ctrl+Shift együtt, nem csinál semmit
            panel1.Invalidate();
        }


        // Transzformációk
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(offsetX, offsetY);   // eltolás
            e.Graphics.RotateTransform(rotationAngle);        // forgatás
            e.Graphics.ScaleTransform(zoom, zoom);           // zoom

            // 1. Kirajzoljuk az összes végleges alakzatot
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

            // 2. Ha éppen rajzolunk, kirajzoljuk az ideiglenes alakzatot
            if (isDrawing)
            {
                using (Pen pen = new Pen(currentColor, 2))
                {
                    if (currentShapeType == "Line")
                        e.Graphics.DrawLine(pen, startPoint, endPoint);

                    else if (currentShapeType == "Rect")
                        e.Graphics.DrawRectangle(pen, GetRectangle(startPoint, endPoint));

                    else if (currentShapeType == "Ellipse")
                        e.Graphics.DrawEllipse(pen, GetRectangle(startPoint, endPoint));
                }
            }
        }

        // Téglalap kiszamitasa
        private Rectangle GetRectangle(Point p1, Point p2)
        {
            return new Rectangle(
                Math.Min(p1.X, p2.X),
                Math.Min(p1.Y, p2.Y),
                Math.Abs(p1.X - p2.X),
                Math.Abs(p1.Y - p2.Y)
            );
        }

        // SVG mentese
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
                    else if (shape.Type == "Rect")
                    {
                        writer.WriteStartElement("rect");
                        writer.WriteAttributeString("x", Math.Min(shape.Start.X, shape.End.X).ToString());
                        writer.WriteAttributeString("y", Math.Min(shape.Start.Y, shape.End.Y).ToString());
                        writer.WriteAttributeString("width", Math.Abs(shape.Start.X - shape.End.X).ToString());
                        writer.WriteAttributeString("height", Math.Abs(shape.Start.Y - shape.End.Y).ToString());
                        writer.WriteAttributeString("stroke", ColorTranslator.ToHtml(shape.Color));
                        writer.WriteAttributeString("fill", "none");
                        writer.WriteAttributeString("stroke-width", "2");
                        writer.WriteEndElement();
                    }
                    else if (shape.Type == "Ellipse")
                    {
                        writer.WriteStartElement("ellipse");
                        int cx = (shape.Start.X + shape.End.X) / 2;
                        int cy = (shape.Start.Y + shape.End.Y) / 2;
                        int rx = Math.Abs(shape.Start.X - shape.End.X) / 2;
                        int ry = Math.Abs(shape.Start.Y - shape.End.Y) / 2;

                        writer.WriteAttributeString("cx", cx.ToString());
                        writer.WriteAttributeString("cy", cy.ToString());
                        writer.WriteAttributeString("rx", rx.ToString());
                        writer.WriteAttributeString("ry", ry.ToString());
                        writer.WriteAttributeString("stroke", ColorTranslator.ToHtml(shape.Color));
                        writer.WriteAttributeString("fill", "none");
                        writer.WriteAttributeString("stroke-width", "2");
                        writer.WriteEndElement();
                    }
                }

                writer.WriteEndElement(); // </svg>
                writer.WriteEndDocument();
            }
        }

        // SVG betöltése
        private void LoadFromSVG(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            // Régi alakzatok törlése
            shapes.Clear();

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
                else if (node.Name == "rect")
                {
                    int x = int.Parse(node.Attributes["x"].Value);
                    int y = int.Parse(node.Attributes["y"].Value);
                    int width = int.Parse(node.Attributes["width"].Value);
                    int height = int.Parse(node.Attributes["height"].Value);

                    Color color = ColorTranslator.FromHtml(node.Attributes["stroke"].Value);

                    shapes.Add(new Shape
                    {
                        Type = "Rect",
                        Start = new Point(x, y),
                        End = new Point(x + width, y + height),
                        Color = color
                    });
                }
                else if (node.Name == "ellipse")
                {
                    int cx = int.Parse(node.Attributes["cx"].Value);
                    int cy = int.Parse(node.Attributes["cy"].Value);
                    int rx = int.Parse(node.Attributes["rx"].Value);
                    int ry = int.Parse(node.Attributes["ry"].Value);

                    Color color = ColorTranslator.FromHtml(node.Attributes["stroke"].Value);

                    shapes.Add(new Shape
                    {
                        Type = "Ellipse",
                        Start = new Point(cx - rx, cy - ry),
                        End = new Point(cx + rx, cy + ry),
                        Color = color
                    });
                }
            }

            panel1.Invalidate(); // Újrarajzolás
        }


        // Gombok 
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "SVG Files|*.svg";
            sfd.Title = "Mentés SVG formátumban";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                SaveToSVG(sfd.FileName);
                MessageBox.Show("Mentés sikeres!", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentShapeType = "Line";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentShapeType = "Rect";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentShapeType = "Ellipse";
        }

        private void colorvalaszto_Click(object sender, EventArgs e)
        {

            using (ColorDialog cd = new ColorDialog())
            {
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    currentColor = cd.Color;
                    colorvalaszto.BackColor = currentColor;
                }
            }

        }

        private void button_mentes_Click(object sender, EventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "SVG Files|*.svg";
            sfd.Title = "Mentés SVG formátumban";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                SaveToSVG(sfd.FileName);
                MessageBox.Show("Mentés sikeres!", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        // svg betöltés
        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "SVG Files|*.svg";
            ofd.Title = "SVG betöltése";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadFromSVG(ofd.FileName);
                MessageBox.Show("Betöltés sikeres!", "Információ",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn1
{
    public class ResizableForm : Form
    {
        private Dictionary<Control, RectangleF> originalControlBounds = new Dictionary<Control, RectangleF>();
        private SizeF originalFormSize;
        private float originalFontSize;

        public ResizableForm()
        {
            this.Load += new EventHandler(this.ResizableForm_Load);
            this.Resize += new EventHandler(this.ResizableForm_Resize);
        }

        private void ResizableForm_Load(object sender, EventArgs e)
        {
            // Lưu kích thước ban đầu của Form và kích thước, vị trí, và kích thước font chữ ban đầu của các control
            originalFormSize = this.Size;
            foreach (Control control in this.Controls)
            {
                originalControlBounds[control] = new RectangleF(control.Location, control.Size);
                originalFontSize = control.Font.Size;
            }
        }

        private void ResizableForm_Resize(object sender, EventArgs e)
        {
            // Thay đổi kích thước, vị trí, và kích thước font chữ của các control khi kích thước của Form thay đổi
            float xRatio = (float)this.Width / originalFormSize.Width;
            float yRatio = (float)this.Height / originalFormSize.Height;
            foreach (Control control in this.Controls)
            {
                if (originalControlBounds.ContainsKey(control))
                {
                    RectangleF originalBounds = originalControlBounds[control];
                    control.Location = new Point((int)(originalBounds.X * xRatio), (int)(originalBounds.Y * yRatio));
                    control.Size = new Size((int)(originalBounds.Width * xRatio), (int)(originalBounds.Height * yRatio));
                    control.Font = new Font(control.Font.FontFamily, originalFontSize * Math.Min(xRatio, yRatio), control.Font.Style);
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ResizableForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "ResizableForm";
            this.Load += new System.EventHandler(this.ResizableForm_Load_1);
            this.ResumeLayout(false);

        }

        private void ResizableForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}

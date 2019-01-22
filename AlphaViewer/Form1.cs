using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlphaViewer {
    public partial class frmMain : Form {
        public frmMain() {
            InitializeComponent();
        }

        private void frmMain_DragDrop(object sender, DragEventArgs e) {
            Mat m = new Mat(((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString(), ImreadModes.Unchanged);
            if (m.Channels() == 4) {
                List<Mat> ms = new List<Mat>(m.Split());
                Mat mrgb = new Mat();
                Cv2.Merge(ms.Take(3).ToArray(), mrgb);
                Cv2.ImShow("Alpha", ms[3]);
                Cv2.ImShow("RGB", mrgb);


                mrgb.Dispose();
                for (int i = 0; i < 4; ++i)
                    ms[i].Dispose();

            } else {
                MessageBox.Show("图片没有Alpha通道！");
            }
            m.Dispose();
        }

        private void frmMain_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }
    }
}

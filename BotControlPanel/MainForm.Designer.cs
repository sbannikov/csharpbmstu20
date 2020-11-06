namespace BotControlPanel
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tool = new System.Windows.Forms.ToolStrip();
            this.startButton = new System.Windows.Forms.ToolStripButton();
            this.stopButton = new System.Windows.Forms.ToolStripButton();
            this.service = new System.ServiceProcess.ServiceController();
            this.list = new System.Windows.Forms.ListBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.queryButton = new System.Windows.Forms.ToolStripButton();
            this.startTraceButton = new System.Windows.Forms.ToolStripButton();
            this.stopTraceButton = new System.Windows.Forms.ToolStripButton();
            this.tool.SuspendLayout();
            this.SuspendLayout();
            // 
            // tool
            // 
            this.tool.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startButton,
            this.stopButton,
            this.queryButton,
            this.startTraceButton,
            this.stopTraceButton});
            this.tool.Location = new System.Drawing.Point(0, 0);
            this.tool.Name = "tool";
            this.tool.Size = new System.Drawing.Size(813, 31);
            this.tool.TabIndex = 0;
            this.tool.Text = "toolStrip1";
            // 
            // startButton
            // 
            this.startButton.Image = ((System.Drawing.Image)(resources.GetObject("startButton.Image")));
            this.startButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(101, 28);
            this.startButton.Text = "Запустить";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Image = ((System.Drawing.Image)(resources.GetObject("stopButton.Image")));
            this.stopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(114, 28);
            this.stopButton.Text = "Остановить";
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // service
            // 
            this.service.ServiceName = "BmstuBotService";
            // 
            // list
            // 
            this.list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list.FormattingEnabled = true;
            this.list.ItemHeight = 16;
            this.list.Location = new System.Drawing.Point(0, 31);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(813, 419);
            this.list.TabIndex = 1;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // queryButton
            // 
            this.queryButton.Image = ((System.Drawing.Image)(resources.GetObject("queryButton.Image")));
            this.queryButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.queryButton.Name = "queryButton";
            this.queryButton.Size = new System.Drawing.Size(163, 28);
            this.queryButton.Text = "Поинтересоваться";
            this.queryButton.Click += new System.EventHandler(this.queryButton_Click);
            // 
            // startTraceButton
            // 
            this.startTraceButton.Image = ((System.Drawing.Image)(resources.GetObject("startTraceButton.Image")));
            this.startTraceButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startTraceButton.Name = "startTraceButton";
            this.startTraceButton.Size = new System.Drawing.Size(130, 28);
            this.startTraceButton.Text = "Начать трассу";
            this.startTraceButton.Click += new System.EventHandler(this.startTraceButton_Click);
            // 
            // stopTraceButton
            // 
            this.stopTraceButton.Image = ((System.Drawing.Image)(resources.GetObject("stopTraceButton.Image")));
            this.stopTraceButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopTraceButton.Name = "stopTraceButton";
            this.stopTraceButton.Size = new System.Drawing.Size(153, 28);
            this.stopTraceButton.Text = "Закончить трассу";
            this.stopTraceButton.Click += new System.EventHandler(this.stopTraceButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 450);
            this.Controls.Add(this.list);
            this.Controls.Add(this.tool);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Управление ботом";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tool.ResumeLayout(false);
            this.tool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tool;
        private System.Windows.Forms.ToolStripButton startButton;
        private System.Windows.Forms.ToolStripButton stopButton;
        private System.ServiceProcess.ServiceController service;
        private System.Windows.Forms.ListBox list;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripButton queryButton;
        private System.Windows.Forms.ToolStripButton startTraceButton;
        private System.Windows.Forms.ToolStripButton stopTraceButton;
    }
}


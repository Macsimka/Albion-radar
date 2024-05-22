namespace Holo;

partial class MainForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        rbLog = new System.Windows.Forms.RichTextBox();
        bSettings = new System.Windows.Forms.Button();
        gbPlayerPosition = new System.Windows.Forms.GroupBox();
        gbMapID = new System.Windows.Forms.GroupBox();
        lMapID = new System.Windows.Forms.Label();
        gbPosition = new System.Windows.Forms.GroupBox();
        lPlayerPos = new System.Windows.Forms.Label();
        gbPlayerPosition.SuspendLayout();
        gbMapID.SuspendLayout();
        gbPosition.SuspendLayout();
        SuspendLayout();
        //
        // rbLog
        //
        rbLog.Location = new System.Drawing.Point(12, 158);
        rbLog.Name = "rbLog";
        rbLog.ReadOnly = true;
        rbLog.Size = new System.Drawing.Size(495, 180);
        rbLog.TabIndex = 4;
        rbLog.Text = "";
        //
        // bSettings
        //
        bSettings.Location = new System.Drawing.Point(214, 21);
        bSettings.Name = "bSettings";
        bSettings.Size = new System.Drawing.Size(94, 29);
        bSettings.TabIndex = 5;
        bSettings.Text = "Settings";
        bSettings.UseVisualStyleBackColor = true;
        bSettings.Click += bSettings_Click;
        //
        // gbPlayerPosition
        //
        gbPlayerPosition.Controls.Add(gbMapID);
        gbPlayerPosition.Controls.Add(gbPosition);
        gbPlayerPosition.Location = new System.Drawing.Point(12, 12);
        gbPlayerPosition.Name = "gbPlayerPosition";
        gbPlayerPosition.Size = new System.Drawing.Size(196, 140);
        gbPlayerPosition.TabIndex = 6;
        gbPlayerPosition.TabStop = false;
        gbPlayerPosition.Text = "Player status";
        //
        // gbMapID
        //
        gbMapID.Controls.Add(lMapID);
        gbMapID.Location = new System.Drawing.Point(6, 83);
        gbMapID.Name = "gbMapID";
        gbMapID.Size = new System.Drawing.Size(184, 48);
        gbMapID.TabIndex = 2;
        gbMapID.TabStop = false;
        gbMapID.Text = "Map ID";
        //
        // lMapID
        //
        lMapID.AutoSize = true;
        lMapID.ForeColor = System.Drawing.Color.Red;
        lMapID.Location = new System.Drawing.Point(6, 23);
        lMapID.Name = "lMapID";
        lMapID.Size = new System.Drawing.Size(78, 20);
        lMapID.TabIndex = 0;
        lMapID.Text = "Undefined";
        //
        // gbPosition
        //
        gbPosition.Controls.Add(lPlayerPos);
        gbPosition.Location = new System.Drawing.Point(6, 26);
        gbPosition.Name = "gbPosition";
        gbPosition.Size = new System.Drawing.Size(184, 51);
        gbPosition.TabIndex = 1;
        gbPosition.TabStop = false;
        gbPosition.Text = "Position";
        //
        // lPlayerPos
        //
        lPlayerPos.AutoSize = true;
        lPlayerPos.Location = new System.Drawing.Point(6, 23);
        lPlayerPos.Name = "lPlayerPos";
        lPlayerPos.Size = new System.Drawing.Size(50, 20);
        lPlayerPos.TabIndex = 0;
        lPlayerPos.Text = "label1";
        //
        // MainForm
        //
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(516, 345);
        Controls.Add(gbPlayerPosition);
        Controls.Add(bSettings);
        Controls.Add(rbLog);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
        MaximizeBox = false;
        Name = "MainForm";
        Text = "Holo (Albion Online Radar)";
        FormClosing += MainForm_FormClosing;
        gbPlayerPosition.ResumeLayout(false);
        gbMapID.ResumeLayout(false);
        gbMapID.PerformLayout();
        gbPosition.ResumeLayout(false);
        gbPosition.PerformLayout();
        ResumeLayout(false);
    }

    #endregion
    private System.Windows.Forms.RichTextBox rbLog;
    private System.Windows.Forms.Button bSettings;
    private System.Windows.Forms.GroupBox gbPlayerPosition;
    private System.Windows.Forms.Label lPlayerPos;
    private System.Windows.Forms.GroupBox gbPosition;
    private System.Windows.Forms.GroupBox gbMapID;
    private System.Windows.Forms.Label lMapID;
}

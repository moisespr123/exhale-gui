<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.InputTxt = New System.Windows.Forms.TextBox()
        Me.OutputTxt = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.InputBrowseBtn = New System.Windows.Forms.Button()
        Me.OutputBrowseBtn = New System.Windows.Forms.Button()
        Me.StartBtn = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.exhaleVersionLabel = New System.Windows.Forms.Label()
        Me.PresetNumberBox = New System.Windows.Forms.NumericUpDown()
        Me.enableMultithreading = New System.Windows.Forms.CheckBox()
        Me.InputFileBtn = New System.Windows.Forms.Button()
        Me.ffmpegVersionLabel = New System.Windows.Forms.Label()
        Me.GoogleDriveButton = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.CPUThreads = New System.Windows.Forms.NumericUpDown()
        CType(Me.PresetNumberBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CPUThreads, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(274, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Step 1: Browse for and input file or folder with music files:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(293, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Step 2: Browse for output folder for encoded xHE-AAC file(s):"
        '
        'InputTxt
        '
        Me.InputTxt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InputTxt.Location = New System.Drawing.Point(15, 45)
        Me.InputTxt.Name = "InputTxt"
        Me.InputTxt.Size = New System.Drawing.Size(374, 20)
        Me.InputTxt.TabIndex = 2
        '
        'OutputTxt
        '
        Me.OutputTxt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OutputTxt.Location = New System.Drawing.Point(15, 87)
        Me.OutputTxt.Name = "OutputTxt"
        Me.OutputTxt.Size = New System.Drawing.Size(464, 20)
        Me.OutputTxt.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 116)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Preset"
        '
        'InputBrowseBtn
        '
        Me.InputBrowseBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InputBrowseBtn.Location = New System.Drawing.Point(476, 42)
        Me.InputBrowseBtn.Name = "InputBrowseBtn"
        Me.InputBrowseBtn.Size = New System.Drawing.Size(84, 23)
        Me.InputBrowseBtn.TabIndex = 5
        Me.InputBrowseBtn.Text = "Browse Folder"
        Me.InputBrowseBtn.UseVisualStyleBackColor = True
        '
        'OutputBrowseBtn
        '
        Me.OutputBrowseBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OutputBrowseBtn.Location = New System.Drawing.Point(485, 87)
        Me.OutputBrowseBtn.Name = "OutputBrowseBtn"
        Me.OutputBrowseBtn.Size = New System.Drawing.Size(75, 23)
        Me.OutputBrowseBtn.TabIndex = 6
        Me.OutputBrowseBtn.Text = "Browse"
        Me.OutputBrowseBtn.UseVisualStyleBackColor = True
        '
        'StartBtn
        '
        Me.StartBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StartBtn.Location = New System.Drawing.Point(359, 116)
        Me.StartBtn.Name = "StartBtn"
        Me.StartBtn.Size = New System.Drawing.Size(204, 37)
        Me.StartBtn.TabIndex = 8
        Me.StartBtn.Text = "Start"
        Me.StartBtn.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 163)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Progress:"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(18, 180)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(545, 23)
        Me.ProgressBar1.Step = 1
        Me.ProgressBar1.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 264)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(119, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "GUI by Moises Cardona"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(520, 264)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(28, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "v1.0"
        '
        'exhaleVersionLabel
        '
        Me.exhaleVersionLabel.AutoSize = True
        Me.exhaleVersionLabel.Location = New System.Drawing.Point(15, 214)
        Me.exhaleVersionLabel.Name = "exhaleVersionLabel"
        Me.exhaleVersionLabel.Size = New System.Drawing.Size(78, 13)
        Me.exhaleVersionLabel.TabIndex = 14
        Me.exhaleVersionLabel.Text = "exhale version:"
        '
        'PresetNumberBox
        '
        Me.PresetNumberBox.Location = New System.Drawing.Point(18, 133)
        Me.PresetNumberBox.Maximum = New Decimal(New Integer() {320, 0, 0, 0})
        Me.PresetNumberBox.Name = "PresetNumberBox"
        Me.PresetNumberBox.Size = New System.Drawing.Size(61, 20)
        Me.PresetNumberBox.TabIndex = 15
        '
        'enableMultithreading
        '
        Me.enableMultithreading.AutoSize = True
        Me.enableMultithreading.Location = New System.Drawing.Point(85, 136)
        Me.enableMultithreading.Name = "enableMultithreading"
        Me.enableMultithreading.Size = New System.Drawing.Size(121, 17)
        Me.enableMultithreading.TabIndex = 16
        Me.enableMultithreading.Text = "Use Multi-Threading"
        Me.enableMultithreading.UseVisualStyleBackColor = True
        '
        'InputFileBtn
        '
        Me.InputFileBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.InputFileBtn.Location = New System.Drawing.Point(395, 43)
        Me.InputFileBtn.Name = "InputFileBtn"
        Me.InputFileBtn.Size = New System.Drawing.Size(75, 23)
        Me.InputFileBtn.TabIndex = 18
        Me.InputFileBtn.Text = "Browse File"
        Me.InputFileBtn.UseVisualStyleBackColor = True
        '
        'ffmpegVersionLabel
        '
        Me.ffmpegVersionLabel.AutoSize = True
        Me.ffmpegVersionLabel.Location = New System.Drawing.Point(15, 229)
        Me.ffmpegVersionLabel.Name = "ffmpegVersionLabel"
        Me.ffmpegVersionLabel.Size = New System.Drawing.Size(79, 13)
        Me.ffmpegVersionLabel.TabIndex = 19
        Me.ffmpegVersionLabel.Text = "ffmpeg version:"
        '
        'GoogleDriveButton
        '
        Me.GoogleDriveButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GoogleDriveButton.Location = New System.Drawing.Point(398, 13)
        Me.GoogleDriveButton.Name = "GoogleDriveButton"
        Me.GoogleDriveButton.Size = New System.Drawing.Size(165, 23)
        Me.GoogleDriveButton.TabIndex = 20
        Me.GoogleDriveButton.Text = "Google Drive"
        Me.GoogleDriveButton.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(204, 116)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 13)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "CPU Threads:"
        '
        'CPUThreads
        '
        Me.CPUThreads.Location = New System.Drawing.Point(207, 133)
        Me.CPUThreads.Name = "CPUThreads"
        Me.CPUThreads.Size = New System.Drawing.Size(71, 20)
        Me.CPUThreads.TabIndex = 22
        '
        'Form1
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(572, 290)
        Me.Controls.Add(Me.CPUThreads)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.GoogleDriveButton)
        Me.Controls.Add(Me.ffmpegVersionLabel)
        Me.Controls.Add(Me.InputFileBtn)
        Me.Controls.Add(Me.enableMultithreading)
        Me.Controls.Add(Me.PresetNumberBox)
        Me.Controls.Add(Me.exhaleVersionLabel)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.StartBtn)
        Me.Controls.Add(Me.OutputBrowseBtn)
        Me.Controls.Add(Me.InputBrowseBtn)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.OutputTxt)
        Me.Controls.Add(Me.InputTxt)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "exhale GUI"
        CType(Me.PresetNumberBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CPUThreads, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents InputTxt As TextBox
    Friend WithEvents OutputTxt As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents InputBrowseBtn As Button
    Friend WithEvents OutputBrowseBtn As Button
    Friend WithEvents StartBtn As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents exhaleVersionLabel As Label
    Friend WithEvents PresetNumberBox As NumericUpDown
    Friend WithEvents enableMultithreading As CheckBox
    Friend WithEvents InputFileBtn As Button
    Friend WithEvents ffmpegVersionLabel As Label
    Friend WithEvents GoogleDriveButton As Button
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Label7 As Label
    Friend WithEvents CPUThreads As NumericUpDown
End Class

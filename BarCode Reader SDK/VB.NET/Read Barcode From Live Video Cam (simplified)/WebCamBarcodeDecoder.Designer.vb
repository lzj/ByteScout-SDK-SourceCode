Partial Class WebCamBarcodeDecoder
	''' <summary>
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary>
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(disposing As Boolean)
		If disposing AndAlso (components IsNot Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	#Region "Windows Form Designer generated code"

	''' <summary>
	''' Required method for Designer support - do not modify
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
        Me.pictureVideoPreview = New System.Windows.Forms.PictureBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        CType(Me.pictureVideoPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pictureVideoPreview
        '
        Me.pictureVideoPreview.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pictureVideoPreview.BackColor = System.Drawing.Color.White
        Me.pictureVideoPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pictureVideoPreview.Location = New System.Drawing.Point(12, 12)
        Me.pictureVideoPreview.Name = "pictureVideoPreview"
        Me.pictureVideoPreview.Size = New System.Drawing.Size(640, 480)
        Me.pictureVideoPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pictureVideoPreview.TabIndex = 8
        Me.pictureVideoPreview.TabStop = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(562, 497)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(90, 26)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'WebCamBarcodeDecoder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(664, 536)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.pictureVideoPreview)
        Me.MaximizeBox = False
        Me.Name = "WebCamBarcodeDecoder"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "QR Code decoder"
        CType(Me.pictureVideoPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private pictureVideoPreview As System.Windows.Forms.PictureBox
    Private WithEvents btnCancel As System.Windows.Forms.Button
End Class


Public NotInheritable Class AboutBox1

    Private Sub AboutBox1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Establezca el título del formulario.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("Acerca de {0}", ApplicationTitle)
        ' Inicialice todo el texto mostrado en el cuadro Acerca de.
        ' TODO: personalice la información del ensamblado de la aplicación en el panel "Aplicación" del 
        '    cuadro de diálogo propiedades del proyecto (bajo el menú "Proyecto").
        Me.LabelProductName.Text = "Projecte: Joc Bingo"
        Me.LabelVersion.Text = "Versio 1.0.0.0"
        Me.LabelCopyright.Text = "CopyRight: No"
        Me.LabelCompanyName.Text = "Autor: Aleix Viladrich Silva"
        Me.TextBoxDescription.Text = "Descripció: Projecte del Bingo fet per jo l'Aleix, de unes 400 linies, no hi ha  module"
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub
End Class

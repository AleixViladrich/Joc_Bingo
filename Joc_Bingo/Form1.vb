Public Class Form1
    'Dim BingoCard(8, 2) As Integer
    Dim Button2Clicked As Boolean = False
    Dim PictureBoxBingo(8, 2) As Control
    Dim PictureBoxBingo2(8, 2) As Control
    Dim textValNumsControl(9, 8) As Control
    Dim TrueBingo(89) As Integer
    Dim audio As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.PerformClick()
        Button2.PerformClick()
        'Creacio ArrayGeneral
        Dim counter As Integer = 0
        Dim valorLeft As Integer = 30
        Dim valorTop As Integer = 30
        For c = 0 To 8
            For f = 0 To 9
                textValNumsControl(f, c) = New PictureBox
                textValNumsControl(f, c).BackgroundImage = Balls.Images(counter)
                textValNumsControl(f, c).BackgroundImageLayout = ImageLayout.Stretch
                textValNumsControl(f, c).Size = New Size(30, 30)
                textValNumsControl(f, c).Top = 15 + valorTop
                textValNumsControl(f, c).Left = 0 + valorLeft
                textValNumsControl(f, c).Tag = counter
                Me.Controls.Add(textValNumsControl(f, c))
                counter = counter + 1
                valorLeft = valorLeft + 30
            Next
            valorLeft = 30
            valorTop = valorTop + 30
        Next
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Static firstTime2 As Boolean = False
        Button2Clicked = True
        If (firstTime2 = True) Then
            ClearPictureBox()
        Else
            firstTime2 = True
        End If
        PictureBoxBingo2 = controller()
        For c = 0 To 2
            For f = 0 To 8
                If (PictureBoxBingo2(f, c).Tag = 0) Then
                    PictureBoxBingo2(f, c).BackgroundImage = NullValue.Images(0)
                End If
                If (PictureBoxBingo2(f, c).Tag <> 0) Then
                    PictureBoxBingo2(f, c).BackgroundImage = Balls.Images(PictureBoxBingo2(f, c).Tag - 1)
                End If
                PictureBoxBingo2(f, c).BackgroundImageLayout = ImageLayout.Stretch
                Me.Controls.Add(PictureBoxBingo2(f, c))
            Next
        Next
        Button2Clicked = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Button3.Enabled = False
        Button1.Enabled = False
        Button2.Enabled = False
        Randomize()
        Timer1.Enabled = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Static firstTime As Boolean = False
        If (firstTime = True) Then
            ClearPictureBox()
        Else
            firstTime = True
        End If
        PictureBoxBingo = controller()
        For c = 0 To 2
            For f = 0 To 8
                If (PictureBoxBingo(f, c).Tag = 0) Then
                    PictureBoxBingo(f, c).BackgroundImage = NullValue.Images(0)
                End If
                If (PictureBoxBingo(f, c).Tag <> 0) Then
                    PictureBoxBingo(f, c).BackgroundImage = Balls.Images(PictureBoxBingo(f, c).Tag - 1)
                End If
                PictureBoxBingo(f, c).BackgroundImageLayout = ImageLayout.Stretch
                Me.Controls.Add(PictureBoxBingo(f, c))
            Next
        Next

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim linea As String = "linea"
        Dim Bingo As String = "bingo"
        Static draw = 0
        Static counterToVictory1 = 0
        Static counterToVictory2 = 0
        Dim IsBingo2 As Boolean = False
        Static line As Boolean = False
        Static finished As Integer
        Dim num As Integer = 0
        num = TrueBingo(draw)
        PictureBox1.BackgroundImage = Balls.Images(TrueBingo(draw))
        PictureBox1.BackgroundImageLayout = ImageLayout.Stretch
        Application.DoEvents()
        audio = Application.StartupPath & "\sonidos\" & num + 1 & ".wav"
        My.Computer.Audio.Play(audio, AudioPlayMode.WaitToComplete)
        For c = 0 To 8
            For f = 0 To 9
                For o = 0 To 89
                    If (textValNumsControl(f, c).Tag = num) Then
                        textValNumsControl(f, c).BackgroundImage = CheckedBalls.Images(textValNumsControl(f, c).Tag)
                    End If
                Next
            Next
        Next
        Application.DoEvents()
        For c = 0 To 2
            For f = 0 To 8
                If (PictureBoxBingo(f, c).Tag = num + 1) Then
                    PictureBoxBingo(f, c).BackgroundImage = CheckedBalls.Images(num)
                    line = GetBingoLine(c, IsBingo2)
                    If (line = True And finished = 0) Then
                        TextBox1.Text = "El jugador 1 ha marcat linea"
                        Timer1.Enabled = False
                        audio = Application.StartupPath & "\sonidos\" & linea & ".wav"
                        My.Computer.Audio.Play(audio, AudioPlayMode.WaitToComplete)
                        Application.DoEvents()
                        Threading.Thread.Sleep(3000)
                        TextBox1.Clear()
                        Timer1.Enabled = True
                        finished = finished + 1
                    End If
                    counterToVictory1 = counterToVictory1 + 1
                End If
                If (PictureBoxBingo2(f, c).Tag = num + 1) Then
                    PictureBoxBingo2(f, c).BackgroundImage = CheckedBalls.Images(num)
                    IsBingo2 = True
                    line = GetBingoLine(c, IsBingo2)
                    If (line = True And finished = 0) Then
                        Timer1.Enabled = False
                        TextBox1.Text = "El jugador 2 ha marcat linea"
                        audio = Application.StartupPath & "\sonidos\" & linea & ".wav"
                        My.Computer.Audio.Play(audio, AudioPlayMode.WaitToComplete)
                        Application.DoEvents()
                        Threading.Thread.Sleep(3000)
                        TextBox1.Clear()
                        Timer1.Enabled = True
                        finished = finished + 1
                    End If
                    IsBingo2 = False
                    counterToVictory2 = counterToVictory2 + 1
                End If
            Next
        Next
        draw = draw + 1
        Application.DoEvents()
        If (counterToVictory1 = 15) Then
            Timer1.Enabled = False
            audio = Application.StartupPath & "\sonidos\" & Bingo & ".wav"
            My.Computer.Audio.Play(audio, AudioPlayMode.WaitToComplete)
            TextBox1.Text = "El jugador 1 ha fet bingo"
        End If
        If (counterToVictory2 = 15) Then
            Timer1.Enabled = False
            audio = Application.StartupPath & "\sonidos\" & Bingo & ".wav"
            My.Computer.Audio.Play(audio, AudioPlayMode.WaitToComplete)
            TextBox1.Text = "El jugador 2 ha fet bingo"
        End If
    End Sub

    Function controller()
        Dim temp(8, 2) As Control
        Dim ArrayToCheck As Integer(,)
        Dim BingoCard(8, 2) As Integer
        BingoCard = getBracket()
        ArrayToCheck = PutZeros()
        For c = 0 To 2
            For f = 0 To 8
                If (ArrayToCheck(f, c) = 0) Then
                    BingoCard(f, c) = 0
                End If
            Next
        Next
        temp = CreatePictureBoxArray(BingoCard)
        Return temp
    End Function
    Function getBracket() As Integer(,)
        Dim BingoCard(8, 2) As Integer
        Static random As New Random
        Dim counter As Integer = 0
        Dim multiplicative As Integer = 0
        Dim min As Integer
        Dim max As Integer
        Dim numToCheck As Integer
        Dim temp As Integer
        For f = 0 To 8
            For c = 0 To 2
                min = 1 + multiplicative
                max = 11 + multiplicative
                BingoCard(f, c) = random.Next(min, max)
                counter = counter + 1
                If (counter = 3) Then
                    ' Randomitzar que no siguin repetits 
                    Do While (BingoCard(f, c - 2) = BingoCard(f, c) Or BingoCard(f, c - 1) = BingoCard(f, c) Or BingoCard(f, c - 2) = BingoCard(f, c - 1))
                        temp = BingoCard(f, c)
                        temp = temp - 1
                        BingoCard(f, c) = temp
                        If (BingoCard(f, c - 2) = BingoCard(f, c - 1)) Then
                            temp = BingoCard(f, c - 1)
                            temp = temp - 1
                            BingoCard(f, c - 1) = temp
                        End If
                        For O = 0 To 2
                            numToCheck = BingoCard(f, c - O)
                            If (numToCheck <= min) Then
                                BingoCard(f, c - O) = 5 + multiplicative
                            End If
                        Next
                    Loop
                    'Per ordenar de mes gran a mes petit
                    If (BingoCard(f, c - 2) > BingoCard(f, c) Or BingoCard(f, c - 1) > BingoCard(f, c)) Then
                        If (BingoCard(f, c - 2) > BingoCard(f, c)) Then
                            temp = BingoCard(f, c)
                            BingoCard(f, c) = BingoCard(f, c - 2)
                            BingoCard(f, c - 2) = temp
                        End If
                        If (BingoCard(f, c - 1) > BingoCard(f, c)) Then
                            temp = BingoCard(f, c)
                            BingoCard(f, c) = BingoCard(f, c - 1)
                            BingoCard(f, c - 1) = temp
                        End If
                    End If
                    If (BingoCard(f, c - 2) > BingoCard(f, c - 1)) Then
                        temp = BingoCard(f, c - 1)
                        BingoCard(f, c - 1) = BingoCard(f, c - 2)
                        BingoCard(f, c - 2) = temp
                    End If
                    multiplicative = multiplicative + 10
                    counter = 0
                End If
            Next
        Next
        Return BingoCard
    End Function

    Function PutZeros() As Integer(,)
        Dim superArray(8, 2) As Integer
        Dim ar1() As Integer = {0, 0, 0, 0, 1, 1, 1, 1, 1}
        Dim ar2() As Integer = {0, 0, 0, 0, 1, 1, 1, 1, 1}
        Dim ar3() As Integer = {0, 0, 0, 0, 1, 1, 1, 1, 1}
        superArray = checkArray(ar1, ar2, ar3)
        Return superArray
    End Function

    Function RandomizeArray(ar, ar1, ar2)
        Dim temp As Integer
        Dim n As Integer
        Dim random As New Random
        For o = 0 To ar.Length - 1
            n = random.Next(0, o + 1)
            temp = ar(o)
            ar(o) = ar(n)
            ar(n) = temp
        Next
        For o = 0 To ar1.Length - 1
            n = random.Next(0, o + 1)
            temp = ar1(o)
            ar1(o) = ar1(n)
            ar1(n) = temp
        Next
        For o = 0 To ar2.Length - 1
            n = random.Next(0, o + 1)
            temp = ar2(o)
            ar2(o) = ar2(n)
            ar2(n) = temp
        Next
        checkArray(ar, ar1, ar2)
        Return Nothing
    End Function

    Function checkArray(ar1, ar2, ar3) As Integer(,)
        Dim SuperArray(8, 2) As Integer
        Dim counter As Integer = 0
        For k = 0 To 8
            If (ar1(k) = 1 And ar2(k) = 1 And ar3(k) = 1) Then
                RandomizeArray(ar1, ar2, ar3)
            End If
            If (ar1(k) = 0 And ar2(k) = 0 And ar3(k) = 0) Then
                RandomizeArray(ar1, ar2, ar3)
            End If
        Next
        For c = 0 To 2
            For f = 0 To 8
                SuperArray(f, c) = ar1(f)
                If (counter = 1) Then
                    SuperArray(f, c) = ar2(f)
                End If
                If (counter = 2) Then
                    SuperArray(f, c) = ar3(f)
                End If
            Next
            counter = counter + 1
        Next
        Return SuperArray
    End Function

    Function CreatePictureBoxArray(BingoCard) As Control(,)
        Dim marg As Integer = 40
        Dim left As Integer = marg
        Dim top As Integer = 450
        Dim org As Integer
        If (Button2Clicked = True) Then
            org = 640
        Else
            org = 0
        End If
        If (Button2Clicked = False) Then
            For c = 0 To 2
                For f = 0 To 8
                    PictureBoxBingo(f, c) = New PictureBox
                    PictureBoxBingo(f, c).Top = top
                    PictureBoxBingo(f, c).Left = org + left
                    PictureBoxBingo(f, c).Tag = BingoCard(f, c)
                    PictureBoxBingo(f, c).Size = New Size(marg, marg)
                    left = left + marg
                Next
                left = marg
                top = top + marg
            Next
            Return PictureBoxBingo
        End If
        If (Button2Clicked = True) Then
            For c = 0 To 2
                For f = 0 To 8
                    PictureBoxBingo2(f, c) = New PictureBox
                    PictureBoxBingo2(f, c).Top = top
                    PictureBoxBingo2(f, c).Left = org + left
                    PictureBoxBingo2(f, c).Tag = BingoCard(f, c)
                    PictureBoxBingo2(f, c).Size = New Size(marg, marg)
                    left = left + marg
                Next
                left = marg
                top = top + marg
            Next
            Return PictureBoxBingo2
        End If
        Return Nothing
    End Function

    Function Randomize()
        Dim random As New Random
        Dim number As Integer
        Dim temp As Integer
        For o = 0 To 88
            TrueBingo(o) = o + 1
        Next
        For o = 0 To 89
            number = random.Next(0, 90)
            temp = TrueBingo(o)
            TrueBingo(o) = TrueBingo(number)
            TrueBingo(number) = temp
        Next
        Return Nothing
    End Function

    Function GetBingoLine(LineNum As Integer, isBingo2 As Boolean) As Boolean
        Static CounterToLine1 As Integer
        Static CounterToLine2 As Integer
        Static CounterToLine3 As Integer
        Static CounterToLine21 As Integer
        Static CounterToLine22 As Integer
        Static CounterToLine23 As Integer
        Dim five As Boolean = False
        If (LineNum = 0 And isBingo2 = False) Then
            CounterToLine1 = CounterToLine1 + 1
            If (CounterToLine1 = 5) Then
                five = True
            End If
        End If
        If (LineNum = 0 And isBingo2 = True) Then
            CounterToLine21 = CounterToLine21 + 1
            If (CounterToLine21 = 5) Then
                five = True
            End If
        End If
        If (LineNum = 1 And isBingo2 = False) Then
            CounterToLine2 = CounterToLine2 + 1
            If (CounterToLine2 = 5) Then
                five = True
            End If
        End If
        If (LineNum = 1 And isBingo2 = True) Then
            CounterToLine22 = CounterToLine22 + 1
            If (CounterToLine22 = 5) Then
                five = True
            End If
        End If
        If (LineNum = 2 And isBingo2 = False) Then
            CounterToLine3 = CounterToLine3 + 1
            If (CounterToLine3 = 5) Then
                five = True
            End If
        End If
        If (LineNum = 2 And isBingo2 = True) Then
            CounterToLine23 = CounterToLine23 + 1
            If (CounterToLine23 = 5) Then
                five = True
            End If
        End If
        Return five
    End Function

    Function ClearPictureBox()
        If (Button2Clicked = True) Then
            For c = 0 To 2
                For f = 0 To 8
                    PictureBoxBingo2(f, c).Dispose()
                Next
            Next
        Else
            For c = 0 To 2
                For f = 0 To 8
                    PictureBoxBingo(f, c).Dispose()
                Next
            Next
        End If
        Return Nothing
    End Function

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Application.Restart()
    End Sub

    Private Sub AcercaDeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AcercaDeToolStripMenuItem.Click
        AboutBox1.Show()
    End Sub
End Class



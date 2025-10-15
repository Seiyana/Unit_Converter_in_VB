Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set up the form
        Me.Text = "Unit Converter"
        Me.Size = New Size(450, 520)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.BackColor = Color.WhiteSmoke

        ' Initialize converter type dropdown
        ComboBox1.Items.AddRange({"Length", "Weight", "Temperature"})
        ComboBox1.SelectedIndex = 0

        ' Position and style the dropdown controls
        SetupTopControls()

        ' Initialize with length units
        LoadLengthUnits()

        ' Set up calculator buttons
        SetupCalculatorButtons()

        ' Make display read-only
        TextBox1.ReadOnly = True
        TextBox2.ReadOnly = True

        ' Set initial display
        TextBox1.Text = "0"
    End Sub

    Private Sub SetupTopControls()
        ' Style and position the top controls
        Label1.Text = "Converter Type:"
        Label1.Location = New Point(20, 20)
        Label1.Size = New Size(100, 20)
        Label1.Font = New Font("Segoe UI", 9, FontStyle.Bold)

        ComboBox1.Location = New Point(130, 18)
        ComboBox1.Size = New Size(120, 25)
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList

        ' From unit controls
        Label2.Text = "From:"
        Label2.Location = New Point(20, 60)
        Label2.Size = New Size(40, 20)
        Label2.Font = New Font("Segoe UI", 9, FontStyle.Bold)

        ComboBox2.Location = New Point(70, 58)
        ComboBox2.Size = New Size(100, 25)
        ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList

        ' To unit controls
        Label3.Text = "To:"
        Label3.Location = New Point(190, 60)
        Label3.Size = New Size(25, 20)
        Label3.Font = New Font("Segoe UI", 9, FontStyle.Bold)

        ComboBox3.Location = New Point(220, 58)
        ComboBox3.Size = New Size(100, 25)
        ComboBox3.DropDownStyle = ComboBoxStyle.DropDownList

        ' Input display
        Label4.Text = "Input Value:"
        Label4.Location = New Point(20, 100)
        Label4.Size = New Size(80, 20)
        Label4.Font = New Font("Segoe UI", 9, FontStyle.Bold)

        TextBox1.Location = New Point(20, 125)
        TextBox1.Size = New Size(300, 30)
        TextBox1.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        TextBox1.TextAlign = HorizontalAlignment.Right
        TextBox1.BackColor = Color.White

        ' Result display
        Label5.Text = "Result:"
        Label5.Location = New Point(20, 170)
        Label5.Size = New Size(50, 20)
        Label5.Font = New Font("Segoe UI", 9, FontStyle.Bold)

        TextBox2.Location = New Point(20, 195)
        TextBox2.Size = New Size(300, 30)
        TextBox2.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        TextBox2.TextAlign = HorizontalAlignment.Right
        TextBox2.BackColor = Color.LightBlue
        TextBox2.ForeColor = Color.DarkBlue
    End Sub

    Private Sub SetupCalculatorButtons()
        ' Create number buttons (0-9) with better styling
        For i As Integer = 0 To 9
            Dim btn As New Button()
            btn.Text = i.ToString()
            btn.Size = New Size(55, 45)
            btn.Font = New Font("Segoe UI", 14, FontStyle.Bold)
            btn.Name = "btnNum" & i.ToString()
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderColor = Color.Gray
            btn.BackColor = Color.White
            btn.ForeColor = Color.Black

            ' Position buttons in calculator layout (starting from y=240)
            Select Case i
                Case 0
                    btn.Location = New Point(80, 405) ' Bottom center
                    btn.Size = New Size(115, 45) ' Make 0 button wider
                Case 1 To 3
                    btn.Location = New Point(20 + (i - 1) * 60, 355) ' Row 1
                Case 4 To 6
                    btn.Location = New Point(20 + (i - 4) * 60, 305) ' Row 2
                Case 7 To 9
                    btn.Location = New Point(20 + (i - 7) * 60, 255) ' Row 3
            End Select

            AddHandler btn.Click, AddressOf NumberButton_Click
            Me.Controls.Add(btn)
        Next

        ' Create decimal point button
        Dim btnDecimal As New Button()
        btnDecimal.Text = "."
        btnDecimal.Size = New Size(55, 45)
        btnDecimal.Location = New Point(20, 405)
        btnDecimal.Font = New Font("Segoe UI", 18, FontStyle.Bold)
        btnDecimal.Name = "btnDecimal"
        btnDecimal.FlatStyle = FlatStyle.Flat
        btnDecimal.FlatAppearance.BorderColor = Color.Gray
        btnDecimal.BackColor = Color.White
        btnDecimal.ForeColor = Color.Black
        AddHandler btnDecimal.Click, AddressOf DecimalButton_Click
        Me.Controls.Add(btnDecimal)

        ' Create negative button
        Dim btnNegative As New Button()
        btnNegative.Text = "+/-"
        btnNegative.Size = New Size(55, 45)
        btnNegative.Location = New Point(200, 355)
        btnNegative.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        btnNegative.Name = "btnNegative"
        btnNegative.FlatStyle = FlatStyle.Flat
        btnNegative.FlatAppearance.BorderColor = Color.Gray
        btnNegative.BackColor = Color.LightGray
        btnNegative.ForeColor = Color.Black
        AddHandler btnNegative.Click, AddressOf NegativeButton_Click
        Me.Controls.Add(btnNegative)

        ' Create clear button
        Dim btnClear As New Button()
        btnClear.Text = "Clear"
        btnClear.Size = New Size(70, 45)
        btnClear.Location = New Point(200, 255)
        btnClear.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        btnClear.Name = "btnClear"
        btnClear.FlatStyle = FlatStyle.Flat
        btnClear.FlatAppearance.BorderColor = Color.DarkRed
        btnClear.BackColor = Color.IndianRed
        btnClear.ForeColor = Color.White
        AddHandler btnClear.Click, AddressOf ClearButton_Click
        Me.Controls.Add(btnClear)

        ' Create backspace button
        Dim btnBackspace As New Button()
        btnBackspace.Text = "⌫"
        btnBackspace.Size = New Size(70, 45)
        btnBackspace.Location = New Point(200, 305)
        btnBackspace.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        btnBackspace.Name = "btnBackspace"
        btnBackspace.FlatStyle = FlatStyle.Flat
        btnBackspace.FlatAppearance.BorderColor = Color.Black
        btnBackspace.BackColor = Color.LightGray
        btnBackspace.ForeColor = Color.Black
        AddHandler btnBackspace.Click, AddressOf BackspaceButton_Click
        Me.Controls.Add(btnBackspace)
    End Sub

    Private Sub NumberButton_Click(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim number As String = btn.Text

        If TextBox1.Text = "0" Then
            TextBox1.Text = number
        Else
            TextBox1.Text &= number
        End If

        ' Auto-convert as numbers are entered
        ConvertValue()
    End Sub

    Private Sub DecimalButton_Click(sender As Object, e As EventArgs)
        If Not TextBox1.Text.Contains(".") Then
            If TextBox1.Text = "0" Then
                TextBox1.Text = "0."
            Else
                TextBox1.Text &= "."
            End If
        End If
    End Sub

    Private Sub NegativeButton_Click(sender As Object, e As EventArgs)
        If TextBox1.Text.StartsWith("-") Then
            TextBox1.Text = TextBox1.Text.Substring(1)
        Else
            If TextBox1.Text <> "0" Then
                TextBox1.Text = "-" & TextBox1.Text
            End If
        End If
        ConvertValue()
    End Sub

    Private Sub ClearButton_Click(sender As Object, e As EventArgs)
        TextBox1.Text = "0"
        TextBox2.Text = ""
    End Sub

    Private Sub BackspaceButton_Click(sender As Object, e As EventArgs)
        If TextBox1.Text.Length > 1 Then
            TextBox1.Text = TextBox1.Text.Substring(0, TextBox1.Text.Length - 1)
        Else
            TextBox1.Text = "0"
        End If
        ConvertValue()
    End Sub

    Private Sub ConvertValue()
        If String.IsNullOrEmpty(TextBox1.Text) OrElse TextBox1.Text = "0" OrElse TextBox1.Text = "-" OrElse TextBox1.Text = "." Then
            TextBox2.Text = "0"
            Return
        End If

        Dim inputValue As Double
        If Not Double.TryParse(TextBox1.Text, inputValue) Then
            Return
        End If

        If ComboBox2.SelectedItem Is Nothing OrElse ComboBox3.SelectedItem Is Nothing Then
            Return
        End If

        Dim result As Double = 0

        Select Case ComboBox1.SelectedItem.ToString()
            Case "Length"
                result = ConvertLength(inputValue, ComboBox2.SelectedItem.ToString(), ComboBox3.SelectedItem.ToString())
            Case "Weight"
                result = ConvertWeight(inputValue, ComboBox2.SelectedItem.ToString(), ComboBox3.SelectedItem.ToString())
            Case "Temperature"
                result = ConvertTemperature(inputValue, ComboBox2.SelectedItem.ToString(), ComboBox3.SelectedItem.ToString())
        End Select

        ' Format result nicely
        If Math.Abs(result) >= 1000000 Then
            TextBox2.Text = result.ToString("E2") ' Scientific notation for very large numbers
        ElseIf Math.Abs(result) < 0.001 AndAlso result <> 0 Then
            TextBox2.Text = result.ToString("E4") ' Scientific notation for very small numbers
        Else
            TextBox2.Text = result.ToString("F6").TrimEnd("0"c).TrimEnd("."c) ' Remove trailing zeros
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ' Clear previous values
        TextBox1.Text = "0"
        TextBox2.Text = ""

        ' Load appropriate units based on selection
        Select Case ComboBox1.SelectedItem.ToString()
            Case "Length"
                LoadLengthUnits()
            Case "Weight"
                LoadWeightUnits()
            Case "Temperature"
                LoadTemperatureUnits()
        End Select
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        ConvertValue()
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        ConvertValue()
    End Sub

    Private Sub LoadLengthUnits()
        ComboBox2.Items.Clear()
        ComboBox3.Items.Clear()

        Dim lengthUnits() As String = {"Meters", "Feet", "Inches", "Centimeters", "Millimeters", "Yards"}
        ComboBox2.Items.AddRange(lengthUnits)
        ComboBox3.Items.AddRange(lengthUnits)

        ComboBox2.SelectedIndex = 0
        ComboBox3.SelectedIndex = 1
    End Sub

    Private Sub LoadWeightUnits()
        ComboBox2.Items.Clear()
        ComboBox3.Items.Clear()

        Dim weightUnits() As String = {"Kilograms", "Pounds", "Ounces", "Grams", "Tons", "Stones"}
        ComboBox2.Items.AddRange(weightUnits)
        ComboBox3.Items.AddRange(weightUnits)

        ComboBox2.SelectedIndex = 0
        ComboBox3.SelectedIndex = 1
    End Sub

    Private Sub LoadTemperatureUnits()
        ComboBox2.Items.Clear()
        ComboBox3.Items.Clear()

        Dim tempUnits() As String = {"Celsius", "Fahrenheit", "Kelvin"}
        ComboBox2.Items.AddRange(tempUnits)
        ComboBox3.Items.AddRange(tempUnits)

        ComboBox2.SelectedIndex = 0
        ComboBox3.SelectedIndex = 1
    End Sub

    Private Function ConvertLength(value As Double, fromUnit As String, toUnit As String) As Double
        ' Convert to meters first (base unit)
        Dim meters As Double = 0

        Select Case fromUnit
            Case "Meters"
                meters = value
            Case "Feet"
                meters = value * 0.3048
            Case "Inches"
                meters = value * 0.0254
            Case "Centimeters"
                meters = value * 0.01
            Case "Millimeters"
                meters = value * 0.001
            Case "Yards"
                meters = value * 0.9144
        End Select

        ' Convert from meters to target unit
        Select Case toUnit
            Case "Meters"
                Return meters
            Case "Feet"
                Return meters / 0.3048
            Case "Inches"
                Return meters / 0.0254
            Case "Centimeters"
                Return meters / 0.01
            Case "Millimeters"
                Return meters / 0.001
            Case "Yards"
                Return meters / 0.9144
            Case Else
                Return 0
        End Select
    End Function

    Private Function ConvertWeight(value As Double, fromUnit As String, toUnit As String) As Double
        ' Convert to kilograms first (base unit)
        Dim kilograms As Double = 0

        Select Case fromUnit
            Case "Kilograms"
                kilograms = value
            Case "Pounds"
                kilograms = value * 0.453592
            Case "Ounces"
                kilograms = value * 0.0283495
            Case "Grams"
                kilograms = value * 0.001
            Case "Tons"
                kilograms = value * 1000
            Case "Stones"
                kilograms = value * 6.35029
        End Select

        ' Convert from kilograms to target unit
        Select Case toUnit
            Case "Kilograms"
                Return kilograms
            Case "Pounds"
                Return kilograms / 0.453592
            Case "Ounces"
                Return kilograms / 0.0283495
            Case "Grams"
                Return kilograms / 0.001
            Case "Tons"
                Return kilograms / 1000
            Case "Stones"
                Return kilograms / 6.35029
            Case Else
                Return 0
        End Select
    End Function

    Private Function ConvertTemperature(value As Double, fromUnit As String, toUnit As String) As Double
        ' Convert to Celsius first (base unit)
        Dim celsius As Double = 0

        Select Case fromUnit
            Case "Celsius"
                celsius = value
            Case "Fahrenheit"
                celsius = (value - 32) * 5 / 9
            Case "Kelvin"
                celsius = value - 273.15
        End Select

        ' Convert from Celsius to target unit
        Select Case toUnit
            Case "Celsius"
                Return celsius
            Case "Fahrenheit"
                Return celsius * 9 / 5 + 32
            Case "Kelvin"
                Return celsius + 273.15
            Case Else
                Return 0
        End Select
    End Function
End Class
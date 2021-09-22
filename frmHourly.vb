'***********************************************************************
'       Tom Lytle
'       Final project
'       payroll calculator
'***********************************************************************

Public Class frmHourly
    Private Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click

        'button will calculate payroll and display outputs in list box

        'reset textboxes and combo box background to white
        txtFirstName.BackColor = Color.White
        txtLastName.BackColor = Color.White
        txtHourlyPayRate.BackColor = Color.White
        txtHoursWorked.BackColor = Color.White
        txtYTDWages.BackColor = Color.White
        cboState.BackColor = Color.White

        'declare varialbes here
        Dim strFirstName As String
        Dim strLastName As String
        Dim dblHourlyRate As Double
        Dim dblHoursWorked As Double
        Dim dblYTDGrossWages As Double
        Dim strState As String
        Dim strValid As Boolean

        'valiadate inputs here

        If ValidateInputs(strFirstName, strLastName, dblHourlyRate, dblHoursWorked, dblYTDGrossWages, strState, strValid) = True Then

            'assign variables here
            Dim dblGrossPay As Double



            'call functions to run calculations here
            dblStateRate = AssignStateRate(strState)
            dblGrossPay = GrossPayCalc(dblHourlyRate, dblHoursWorked)
            dblFICA = FICACalc(dblYTDGrossWages, dblGrossPay)
            dblStateTaxesDue = StateTaxCalc(dblGrossPay, dblStateRate)
            dblFederalTaxesDue = FederalTaxCalc(dblGrossPay)
            dblNetPay = NetPayCalc(dblGrossPay, dblFICA, dblFederalTaxesDue, dblStateTaxesDue)

            'display outputs here
            DisplayOutputs(dblGrossPay, dblFICA, dblFederalTaxesDue, dblStateTaxesDue, dblNetPay)

        End If


    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click

        ' button will reset frmHourly

        'reset background to white on all text boxes and combo boxes
        txtFirstName.BackColor = Color.White
        txtLastName.BackColor = Color.White
        txtHourlyPayRate.BackColor = Color.White
        txtHoursWorked.BackColor = Color.White
        txtYTDWages.BackColor = Color.White
        cboState.BackColor = Color.White

        'clear inputs in textboxes
        txtFirstName.ResetText()
        txtLastName.ResetText()
        txtHourlyPayRate.ResetText()
        txtHoursWorked.ResetText()
        txtYTDWages.ResetText()
        cboState.ResetText()

        'clear outputs in listbox

        lstOutput.Items.Clear()

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click

        'button will close and exit frmHourly
        Close()
    End Sub


    Private Function ValidateInputs(ByRef strFirstName As String, ByRef strLastName As String, ByRef dblHourlyRate As Double, ByRef dblHoursWorked As Double, ByRef dblYTDGrossWages As Double, ByRef strState As String, ByRef strValid As Boolean) As Boolean

        'Function will validate inputs 


        If ValidateFirstName(strFirstName) = True Then
            If ValidateLastName(strLastName) = True Then
                If ValidateHourlyRate(dblHourlyRate) = True Then
                    If ValidateHoursWorked(dblHoursWorked) = True Then
                        ValidateYTDGrossWages(dblYTDGrossWages, strValid)
                        If strValid = True Then


                            ValidateStateSelected(strState, strValid)
                            If strValid = True Then

                                Return True

                            End If
                        End If
                    End If
                End If
            End If
        End If
        Return False
    End Function

    Private Function ValidateFirstName(ByRef strFirstName As String) As Boolean

        'will vailidate txtFirstName is not empty
        If txtFirstName.Text = String.Empty Then

            'display message box if empty
            MessageBox.Show("Must enter employees first name")
            txtFirstName.BackColor = Color.Yellow
            txtFirstName.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function ValidateLastName(ByRef strLastName As String) As Boolean

        'will validate txtLastName is not empty

        If txtLastName.Text = String.Empty Then

            'will display message box if empty
            MessageBox.Show("Must enter employees last name")
            txtLastName.BackColor = Color.Yellow
            txtLastName.Focus()
            Return False

        End If

        Return True
    End Function

    Private Function ValidateHourlyRate(ByRef dblHourlyRate As Double) As Boolean

        'will validate txtHourlyPayRate is not empty and is a number above 0

        If IsNumeric(txtHourlyPayRate.Text) = True Then

            dblHourlyRate = txtHourlyPayRate.Text

            If dblHourlyRate > 0 Then

                Return True

            Else
                MessageBox.Show("Must enter a pay rate above 0")
                txtHourlyPayRate.BackColor = Color.Yellow
                txtHourlyPayRate.Focus()
                Return False
            End If

        Else
            MessageBox.Show("Must enter pay rate as numbers only")
            txtHourlyPayRate.BackColor = Color.Yellow
            txtHourlyPayRate.Focus()
            Return False
        End If

    End Function

    Private Function ValidateHoursWorked(ByRef dblHoursWorked As Double) As Boolean

        'will validate txtHoursWorked is not empty and is a number above 0

        If IsNumeric(txtHoursWorked.Text) = True Then

            dblHoursWorked = txtHoursWorked.Text

            If dblHoursWorked > 0 Then

                Return True

            Else

                MessageBox.Show("Must enter hours worked as a positive number only")
                txtHoursWorked.BackColor = Color.Yellow
                txtHoursWorked.Focus()
                Return False
            End If

        Else
            MessageBox.Show("Must enter hours worked as numbers only")
            txtHoursWorked.BackColor = Color.Yellow
            txtHoursWorked.Focus()
            Return False
        End If

    End Function

    Private Sub ValidateYTDGrossWages(ByRef dblYTDGrossWages As Double, ByRef strValid As Boolean)

        'will validate txtYTDGrossWages is not empty and is a positive number

        If IsNumeric(txtYTDWages.Text) = True Then

            dblYTDGrossWages = txtYTDWages.Text

            If dblYTDGrossWages >= 0 Then

                strValid = True

            Else

                MessageBox.Show("Must enter YTD gross wages as a positive number")
                txtYTDWages.BackColor = Color.Yellow
                txtYTDWages.Focus()
                strValid = False
            End If

        Else
            MessageBox.Show("Must enter YTD gross wages as a positive number")
            txtYTDWages.BackColor = Color.Yellow
            txtYTDWages.Focus()
            strValid = False

        End If

    End Sub

    Private Sub ValidateStateSelected(ByRef strState As String, ByRef strValid As Boolean)

        'will validate a state has been selected in the combobox

        If cboState.Text = String.Empty Then

            MessageBox.Show("Must selet a state from the drop down list")
            cboState.BackColor = Color.Yellow
            cboState.Focus()
            strValid = False

        Else
            strState = cboState.Text
            strValid = True

        End If

    End Sub

    Private Function GrossPayCalc(ByRef dblHourlyRate As Double, ByRef dblHoursWorked As Double) As Double

        'const for overtime
        Const intOT As Integer = 40
        Const dblOTPay As Double = 1.5

        Dim dblGrossPay As Double

        'will calculate gross pay

        If dblHoursWorked <= intOT Then
            dblGrossPay = dblHourlyRate * dblHoursWorked

        Else
            dblGrossPay = (dblHourlyRate * intOT) + ((dblHoursWorked - intOT) * dblHourlyRate * dblOTPay)

        End If

        Return dblGrossPay

    End Function


    Private Sub DisplayOutputs(ByRef dblGrossPay, dblFICA, dblFederalTaxesDue, dblStateTaxesDue, dblNetPay)



        'display payroll amounts in output list box
        lstOutput.Items.Add("Net Pay:" & vbTab & vbTab & vbTab & FormatCurrency(dblNetPay))
        lstOutput.Items.Add("FICA:" & vbTab & vbTab & vbTab & FormatCurrency(dblFICA))
        lstOutput.Items.Add("State Tax:" & vbTab & vbTab & FormatCurrency(dblStateTaxesDue))
        lstOutput.Items.Add("Federal Tax:" & vbTab & vbTab & FormatCurrency(dblFederalTaxesDue))
        lstOutput.Items.Add("Gross Pay:" & vbTab & vbTab & FormatCurrency(dblGrossPay))
        lstOutput.Items.Add("")
        lstOutput.Items.Add("")

    End Sub

    Private Sub CalculateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CalculateToolStripMenuItem.Click

        'will call btnCalculate subroutine
        btnCalculate_Click(sender, e)
    End Sub

    Private Sub ClearToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem.Click

        'will call btnClear subroutine
        btnClear_Click(sender, e)

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click

        'will call btnExit sub routine
        btnExit_Click(sender, e)
    End Sub
End Class




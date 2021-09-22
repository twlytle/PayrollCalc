'**********************************************************************
'       Tom Lytle
'       finaly Project
'       payroll calculator
'**********************************************************************



Public Class frmSalary
    Private Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click

        'reset back color to white when submit is clicked
        txtFirstName.BackColor = Color.White
        txtLastName.BackColor = Color.White
        txtSalary.BackColor = Color.White
        txtYTDWages.BackColor = Color.White
        cboState.BackColor = Color.White



        'declare vairables here
        Dim strFirstName As String
        Dim strLastName As String
        Dim dblSalary As Double
        Dim dblYTDGrossWages As Double
        Dim strState As String
        Dim strValid As Boolean

        'validate inputs 
        If ValidateInputs(strFirstName, strLastName, dblSalary, dblYTDGrossWages, strState, strValid) = True Then

            Dim dblGrossPay As Double

            'run calculations here
            dblStateRate = AssignStateRate(strState)
            dblGrossPay = SalaryGrossPayCalc(dblSalary)
            dblFICA = FICACalc(dblYTDGrossWages, dblGrossPay)
            dblStateTaxesDue = StateTaxCalc(dblGrossPay, dblStateRate)
            dblFederalTaxesDue = FederalTaxCalc(dblGrossPay)
            dblNetPay = NetPayCalc(dblGrossPay, dblFICA, dblFederalTaxesDue, dblStateTaxesDue)


            'display outputs here
            DisplayOutputs(dblGrossPay, dblFICA, dblFederalTaxesDue, dblStateTaxesDue, dblNetPay)

        End If

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click

        'button will clear input text boxes, output listbox and all backgrounds back to white
        'reset background to white on all text boxes and combo boxes
        txtFirstName.BackColor = Color.White
        txtLastName.BackColor = Color.White
        txtYTDWages.BackColor = Color.White
        txtSalary.BackColor = Color.White
        cboState.BackColor = Color.White

        'clear inputs in textboxes
        txtFirstName.ResetText()
        txtLastName.ResetText()
        txtSalary.ResetText()
        txtYTDWages.ResetText()
        cboState.ResetText()

        'clear outputs in listbox

        lstOutPut.Items.Clear()

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click

        'button will close and exit program
        Close()
    End Sub







    Private Function ValidateInputs(ByRef strFirstName As String, ByRef strLastName As String, ByRef dblSalary As Double, ByRef dblYTDGrossWages As Double, ByRef strState As String, ByRef strValid As Boolean) As Boolean

        'Function will validate inputs 


        If ValidateFirstName(strFirstName) = True Then
            If ValidateLastName(strLastName) = True Then
                If ValidateSalary(dblSalary) = True Then
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

    Private Function ValidateSalary(ByRef dblSalary As Double) As Boolean

        'will validate salary is a number above 0
        If IsNumeric(txtSalary.Text) = True Then
            dblSalary = txtSalary.Text

            If dblSalary <= 0 Then
                MessageBox.Show("Salary must be a positive number only")
                txtSalary.BackColor = Color.Yellow
                txtSalary.Focus()
                Return False

            Else
                Return True

            End If

        Else
            MessageBox.Show("Salary must be a positive number only")
            txtSalary.BackColor = Color.Yellow
            txtSalary.Focus()
            Return False
        End If

    End Function

    Private Sub ValidateYTDGrossWages(ByRef dblYTDGrossWages As Double, ByRef strValid As Boolean)

        'will validate txtYTDGrossWages is not empty and is a positive number

        If IsNumeric(txtYTDWages.Text) = True Then

            dblYTDGrossWages = txtYTDWages.Text

            If dblYTDGrossWages >= 0 Then

                dblYTDGrossWages = txtYTDWages.Text
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


    Private Function SalaryGrossPayCalc(ByRef dblSalary As Double) As Double

        ' will calculate weekly pay for a salary employee

        'local const for pay periods per year. I based it on 52 periods
        Const intPayPeriods As Integer = 52

        Dim dblGrossPay As Double

        dblGrossPay = dblSalary / intPayPeriods

        Return dblGrossPay
    End Function


    Private Sub DisplayOutputs(ByRef dblGrossPay, dblFICA, dblFederalTaxesDue, dblStateTaxesDue, dblNetPay)



        'display payroll amounts in output list box
        lstOutPut.Items.Add("Net Pay:" & vbTab & vbTab & vbTab & FormatCurrency(dblNetPay))
        lstOutPut.Items.Add("FICA:" & vbTab & vbTab & vbTab & FormatCurrency(dblFICA))
        lstOutPut.Items.Add("State Tax:" & vbTab & vbTab & FormatCurrency(dblStateTaxesDue))
        lstOutPut.Items.Add("Federal Tax:" & vbTab & vbTab & FormatCurrency(dblFederalTaxesDue))
        lstOutPut.Items.Add("Gross Pay:" & vbTab & vbTab & FormatCurrency(dblGrossPay))
        lstOutPut.Items.Add("")
        lstOutPut.Items.Add("")

    End Sub

    Private Sub ClearToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CalculateToolStripMenuItem.Click

        'will call btnCalculate
        btnCalculate_Click(sender, e)

    End Sub

    Private Sub ClearToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem1.Click

        'will call btnCleaer
        btnClear_Click(sender, e)
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click

        'will call btnExit
        btnExit_Click(sender, e)
    End Sub
End Class
'***********************************************************************
'       Tom Lytle
'       Final Project
'       Payroll calculator
'***********************************************************************

Module Module1

    'declare public variables here
    Public dblStateRate As Double
    Public dblStateTaxesDue As Double
    Public dblFICA As Double
    Public dblFederalTaxesDue As Double
    Public dblNetPay As Double

    Public Function AssignStateRate(ByVal strState As String) As Double

        'will assing tax rate based on state selected

        Const dblOhio As Double = 0.065
        Const dblKentucky As Double = 0.06
        Const dblIndiana As Double = 0.055


        If strState = "Ohio" Then
            dblStateRate = dblOhio
            Return dblStateRate

        ElseIf strState = "Kentucky" Then
            dblStateRate = dblKentucky
            Return dblStateRate

        Else
            dblStateRate = dblIndiana
            Return dblStateRate
        End If



    End Function


    Public Function FICACalc(ByVal dblYTDGROSSWages As Double, ByRef dblGrossPay As Double) As Double

        'constants to be used in function
        Const dblSSNMax As Double = 0.062
        Const dblSSNMin As Double = 0.0145
        Const intMaxSSNPay As Double = 125000

        'will calculate FICA due 
        'if employee already maxed out SS Taxes for teh year then they do not pay SS Taxes
        If dblYTDGROSSWages > intMaxSSNPay Then


            dblFICA = dblGrossPay * dblSSNMin

            'If employee has not maxed out SS taxes but is this pay cycle this will calculate SS due
        ElseIf (dblYTDGROSSWages + dblGrossPay) > intMaxSSNPay Then

            dblFICA = ((intMaxSSNPay - dblYTDGROSSWages) * dblSSNMax) + dblGrossPay * dblSSNMin



            'If employee has not maxed out SS taxes and will not this pay period this will calculate SS taxes due
        ElseIf (dblYTDGROSSWages + dblGrossPay) < intMaxSSNPay Then

            dblFICA = (dblGrossPay * dblSSNMax) + (dblGrossPay * dblSSNMin)

        End If

        Return dblFICA
    End Function


    Public Function StateTaxCalc(ByRef dblGrossPay As Double, ByRef dblStateTaxRate As Double) As Double

        'function will calculate state taxes due based on state selected

        dblStateTaxesDue = dblGrossPay * dblStateRate

        Return dblStateTaxesDue

    End Function


    Public Function FederalTaxCalc(ByRef dblGrossPay As Double) As Double

        'will calculate federal taxes due based on tiers on income
        'declare constaints to use in function
        Const dbl0_50 As Double = 0
        Const dbl51_500 As Double = 0.1
        Const dbl501_2500 As Double = 0.15
        Const dbl2501_5000 As Double = 0.2
        Const dblOver5000 As Double = 0.25
        Const dblTaxMaxAt500 As Double = 45
        Const dblTaxMaxAt2500 As Double = 345
        Const dblTaxMaxAt5000 As Double = 845

        'check to see how much federal taxes are due based on tier levels of income
        If dblGrossPay <= 50 Then
            dblFederalTaxesDue = dblGrossPay * dbl0_50

        ElseIf dblGrossPay <= 500 Then
            dblFederalTaxesDue = (dblGrossPay - 50) * dbl51_500

        ElseIf dblGrossPay <= 2500 Then
            dblFederalTaxesDue = (dblGrossPay - 500) * dbl501_2500 + dblTaxMaxAt500

        ElseIf dblGrossPay <= 5000 Then
            dblFederalTaxesDue = (dblGrossPay - 2500) * dbl2501_5000 + dblTaxMaxAt2500

        Else
            dblFederalTaxesDue = (dblGrossPay - 5000) * dblOver5000 + dblTaxMaxAt5000

        End If

        Return dblFederalTaxesDue
    End Function


    Public Function NetPayCalc(ByRef dblGrossPay As Double, ByRef dblFica As Double, dblFederalTaxesDue As Double, dblStateTaxesDue As Double) As Double

        'will calculate net pay
        dblNetPay = dblGrossPay - dblFica - dblFederalTaxesDue - dblStateTaxesDue

        Return dblNetPay
    End Function
End Module



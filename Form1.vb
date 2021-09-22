'************************************************************************
'       Tom Lytle
'       Final Project
'       Payroll calculator
'************************************************************************


Public Class frmMain
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click

        'Will close the program
        Close()

    End Sub

    Private Sub btnSalary_Click(sender As Object, e As EventArgs) Handles btnSalary.Click

        'will open frmSalary form

        Dim Salary As New frmSalary


        Salary.ShowDialog()

    End Sub

    Private Sub btnHourly_Click(sender As Object, e As EventArgs) Handles btnHourly.Click

        'will open frmHourly form

        Dim Hourly As New frmHourly


        Hourly.ShowDialog()


    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click

        'will call exit subroutine
        btnExit_Click(sender, e)

    End Sub

    Private Sub HourlyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HourlyToolStripMenuItem.Click

        'will call Hourly subroutine
        btnHourly_Click(sender, e)
    End Sub

    Private Sub SalaryCalculatorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalaryCalculatorToolStripMenuItem.Click

        'will call Salary subroutine
        btnSalary_Click(sender, e)

    End Sub
End Class

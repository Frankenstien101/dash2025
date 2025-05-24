
Imports System.ComponentModel

Imports System.Data.SqlClient

Imports Microsoft.ML
Imports Microsoft.ML.Data

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Math


Public Class Transaction_Order_Plan_Re_Assign_Per_Seller_Set_Sub_Batch

    Private Sub GunaLabel2_Click(sender As Object, e As EventArgs) Handles GunaLabel2.Click

        Close()

    End Sub

    Private Sub GunaAdvenceButton2_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton2.Click

        Try

            Dim K As String = "DELETE Dash_Sub_Batch_Creation WHERE BATCH_ID = '" & BATCHNUMBER.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "'"

            Data(K)

        Catch ex As Exception

        End Try

        Dim qty As Integer
        Dim i As Integer

        If Integer.TryParse(NUMOFBATCH.Value, qty) Then

            If qty > 0 Then

                Me.Enabled = False

                For i = 1 To qty

                    subbatch.Text = BATCHNUMBER.Text & "-" & i

                    SaveProcess(i)

                Next

                Me.Enabled = True

                Try

                    views1("SELECT SUB_BATCH_ID FROM Dash_Sub_Batch_Creation WHERE BATCH_ID = '" & BATCHNUMBER.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "'", "BSPIDB", DTGSUBBATCH)

                Catch ex As Exception

                End Try

                CKLIST.Items.Clear()

                Try

                    con.Open()

                    Dim cmd As SqlCommand = New SqlCommand("SELECT SELLER_NAME
                                                        	  
                                                          FROM [dbo].[Dash_SO_Plan_Batch_Details]
                                                        
                                                          LEFT JOIN PRFR_SO_UPLOAD ON PRFR_SO_UPLOAD.ORDER_ID = Dash_SO_Plan_Batch_Details.SO_NUMBER
                                                        
                                                        WHERE Dash_SO_Plan_Batch_Details.COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND Dash_SO_Plan_Batch_Details.SITE_ID = '" & Form1.SITEID.Text & "' AND Dash_SO_Plan_Batch_Details.SO_PLAN_NUMBER = '" & BATCHNUMBER.Text & "' AND SELLER_NAME != '' GROUP BY SELLER_NAME ", con)

                    Dim rd As SqlDataReader = cmd.ExecuteReader()

                    While rd.Read()

                        CKLIST.Items.Add(rd("SELLER_NAME").ToString(), CheckState.Unchecked)

                    End While

                    con.Close()

                Catch ex As Exception

                    MsgBox(ex.ToString)

                End Try

                Try

                    views2("SELECT PLATE_NUM FROM Dash_Vehicles WHERE IS_SUB = 'YES' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "'", "BSPIDB", DTGSUBDA)

                Catch ex As Exception

                End Try


            Else

                MessageBox.Show("Please enter a positive quantity.")

            End If

        Else

            MessageBox.Show("Invalid quantity entered. Please enter a valid number.")

        End If

    End Sub


    Private Sub SaveProcess(ByVal itemIndex As Integer)

        Try

            Dim a As String = "INSERT INTO Dash_Sub_Batch_Creation(COMPANY_ID,SITE_ID,BATCH_ID,SUB_BATCH_ID,VEHICLE)" _
          & "VALUES('" & Form1.COMPANYID.Text & "'," _
                   & "'" & Form1.SITEID.Text & "'," _
          & "'" & BATCHNUMBER.Text & "'," _
              & "'" & subbatch.Text & "'," _
         & "'')"

            Data(a)

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

    End Sub

    Private Sub TRANSACTION_UPDATE_SO_PLAN_PROCESS_SUB_BATCH_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DTDEL = Reports_Order_Plan_Re_Assign_Per_Seller.DTDELIVERY.Value
        COMPANYID = Form1.COMPANYID.Text
        SITEID = Form1.SITEID.Text

        COMID.Text = Form1.COMPANYID.Text
        SITID.Text = Form1.SITEID.Text

    End Sub

    Private Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click

        SEARCH.Text = ""

        For Each value As String In CKLIST.CheckedItems

            SEARCH.Text = SEARCH.Text & "'" & value & "',"

        Next

        SEARCH.Text = SEARCH.Text.Remove(SEARCH.Text.Length - 1)

        If SEARCH.Text.Trim = "" Then

            MsgBox("PLEASE SELECT SELLER TO ASSIGN", MsgBoxStyle.Exclamation, "SORRY")

        ElseIf SUBBATCHTXT.Text = "" Then

            MsgBox("PLEASE SELECT SUB BATCH TO ASSIGN", MsgBoxStyle.Exclamation, "SORRY")

        ElseIf VEHICLE.Text = "" Then

            MsgBox("PLEASE SELECT VEHICLE TO ASSIGN", MsgBoxStyle.Exclamation, "SORRY")

        Else

            Try

                views("SELECT STORE_CODE FROM PRFR_SO_UPLOAD WHERE ORDER_DATE ='" & Reports_Order_Plan_Re_Assign_Per_Seller.DTDELIVERY.Value & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND SELLER_NAME IN (" & SEARCH.Text & ") GROUP BY STORE_CODE ", "BSPIDB", DTGCHECK)

            Catch ex As Exception

            End Try

            Me.Enabled = False

            BackgroundWorker1.RunWorkerAsync()

        End If

    End Sub

    Private Sub DTGSUBBATCH_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGSUBBATCH.CellClick

        SUBBATCHTXT.Text = ""

        Try

            SUBBATCHTXT.Text = DTGSUBBATCH.CurrentRow.Cells(0).Value

        Catch ex As Exception

        End Try

    End Sub

    Private Sub DTGSUBDA_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGSUBDA.CellClick

        VEHICLE.Text = ""
        da.Text = ""

        Try

            VEHICLE.Text = DTGSUBDA.CurrentRow.Cells(0).Value

        Catch ex As Exception

        End Try

        Try

            views3("SELECT AGENT FROM Dash_Vehicle_Agent WHERE VEHICLE = '" & VEHICLE.Text & "' AND COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "'", "BSPIDB", DTGCHECK)
            da.Text = DTGCHECK.CurrentRow.Cells(0).Value

        Catch ex As Exception

            ' MsgBox(ex.ToString)

        End Try

        If da.Text = "" Then

            MsgBox("NO ASSIGNED AGENT FOR THIS VEHICLE, PLEASE SELECT AGENT TO ASSIGN", MsgBoxStyle.Exclamation, "SORRY")

            Transaction_Order_Plan_Re_Assign_Per_Seller_Set_Sub_Batch_Set_Agent.ShowDialog()

        Else

        End If

    End Sub

    Dim DTDEL As String
    Dim COMPANYID As String
    Dim SITEID As String

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        For i = 0 To DTGCHECK.Rows.Count - 1 Step +1

            If DTGCHECK.Rows.Count > 1 Then

                Dim x As Decimal
                'Dim y As Decimal
                Dim z As Decimal
                Dim a As Decimal

                x = DTGCHECK.Rows.Count - 1

                z = i / x

                a = z * 100

                GunaProgressBar1.Value = a
                '  percentlbl.Text = a

            Else

                GunaProgressBar1.Value = 100
                ' percentlbl.Text = "100%"

            End If

            Try

                Dim A As String = "UPDATE Dash_SO_Plan_Batch_Details SET SUB_BATCH = '" & SUBBATCHTXT.Text & "' , SUB_DA = '" & da.Text & "' , VEHICLE_IDS = '" & VEHICLE.Text & "' WHERE ORDER_DATE ='" & DTDEL & "' AND COMPANY_ID = '" & COMPANYID & "' AND SITE_ID = '" & SITEID & "' AND CUSTOMER_ID = '" & DTGCHECK.Rows(i).Cells(0).Value & "'"

                Data(A)

            Catch ex As Exception

                MsgBox(ex.ToString)

            End Try

        Next

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

        Me.Enabled = True

        MsgBox("TRANSACTION COMPLETE", MsgBoxStyle.Information, "COMPLETE")

    End Sub

    Private Sub GunaAdvenceButton4_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton4.Click

        Try

            Dim K As String = "UPDATE Dash_SO_Plan_Batch_Details SET SUB_BATCH = '' WHERE ORDER_DATE = '" & Reports_Order_Plan_Re_Assign_Per_Seller.DTDELIVERY.Value & "' AND SO_PLAN_NUMBER = '" & BATCHNUMBER.Text & "'"

            Data(K)

        Catch ex As Exception

        End Try


        Try

            views("SELECT			  
				   [SO_NUMBER]
                  ,[CUSTOMER_ID]
                  ,[CUSTOMER_NAME]
                  ,[TOTAL_AMOUNT]
                  ,[STORE_LAT]
                  ,[STORE_LONG]
        
                  FROM [dbo].[Dash_SO_Plan_Batch_Details] 

				  LEFT JOIN Dash_SO_Plan_Transaction ON Dash_SO_Plan_Transaction.SO_PLAN_NUMBER = Dash_SO_Plan_Batch_Details.SO_PLAN_NUMBER
				  
				  WHERE Dash_SO_Plan_Batch_Details.COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND Dash_SO_Plan_Batch_Details.SITE_ID = '" & Form1.SITEID.Text & "' AND ORDER_DATE = '" & Reports_Order_Plan_Re_Assign_Per_Seller.DTDELIVERY.Value & "' AND Dash_SO_Plan_Batch_Details.STATUS != 'NEW' AND Dash_SO_Plan_Batch_Details.SO_PLAN_NUMBER = '" & BATCHNUMBER.Text & "'", "BSPIDB", DTGSTORES)

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

        Try

            views1("SELECT LINE_ID,
                         PLATE_NUM
                        ,STORES_LIMIT
                        , WAREHOUSE_LAT
                        ,WAREHOUSE_LONG

                    FROM [dbo].[Dash_Vehicles] 
                WHERE COMPANY_ID = '" & COMID.Text & "' AND SITE_ID = '" & SITID.Text & "' AND IS_SUB = 'YES' ORDER BY PRIORITY_COUNT ASC", "BSPIDB", DTGSUBVEHICLES)

            TXTWAREHOUSELAT.Text = DTGSUBVEHICLES.CurrentRow.Cells(3).Value
            TXTWAREHOUSELONG.Text = DTGSUBVEHICLES.CurrentRow.Cells(4).Value

            DTGSUBVEHICLES.Columns(0).Visible = False

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

        Try

            TOTALSTORES.Text = DTGSTORES.Rows.Count

        Catch ex As Exception

        End Try

    End Sub

    Private Sub GunaLinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles GunaLinkLabel1.LinkClicked

        Setup_Van_Master_Manual.TRIGGER.Text = "SUB"

        Setup_Van_Master_Manual.ShowDialog()

    End Sub

    Public Sub GunaAdvenceButton3_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton3.Click

        If BATCHNUMBER.Text = "" Then

            MsgBox("NO TRANSACTION TO PROCESS", MsgBoxStyle.Exclamation, "SORRY")

        ElseIf NUMOFSUBBATCH.Value < 1 Then

            MsgBox("NUMBER OF SUB BATCH IS NOT ALLOWED", MsgBoxStyle.Exclamation, "SORRY")

        Else

            Dim K As MsgBoxResult

            K = MsgBox("ARE YOU SURE TO PROCESS ROUTING TRANSACTION?", MsgBoxStyle.YesNo, "CONFIRM")

            If K = MsgBoxResult.Yes Then

                Me.Enabled = False

                Try
                    Dim qty As Integer
                    Dim i As Integer

                    Dim qty1 As Integer
                    Dim i1 As Integer

                    If Integer.TryParse(NUMOFSUBBATCH.Value, qty) Then
                        If qty > 0 Then
                            For i = 0 To qty - 1  ' Ensure zero-based index

                                ' Ensure row exists before accessing
                                If i >= DTGSUBVEHICLES.Rows.Count Then
                                    MsgBox("DTGSUBVEHICLES does not have enough rows!")
                                    Exit For
                                End If

                                STORELIMIT.Text = DTGSUBVEHICLES.Rows(i).Cells(2).Value
                                SUBBATCHID.Text = BATCHNUMBER.Text & "-" & (i + 1)

                                Try
                                    ' Fetch Data for DTGRESULT
                                    views2("DECLARE @user_latitude FLOAT = " & TXTWAREHOUSELAT.Text & ";
                                DECLARE @user_longitude FLOAT = " & TXTWAREHOUSELONG.Text & ";
                                DECLARE @max_distance FLOAT = 500.0;
                                
                                SELECT TOP " & STORELIMIT.Text & "
                                    SO_NUMBER,
                                    CUSTOMER_ID,
                                    CUSTOMER_NAME,
                                    STORE_LAT AS store_latitude,
                                    STORE_LONG AS store_longitude,
                                    DistanceCalculation.distance / 1000.0 AS distance_km
                                FROM Dash_SO_Plan_Batch_Details
                                CROSS APPLY (
                                    SELECT GEOGRAPHY::Point(STORE_LAT, STORE_LONG, 4326)
                                               .STDistance(GEOGRAPHY::Point(@user_latitude, @user_longitude, 4326)) AS distance
                                ) AS DistanceCalculation
                                WHERE
                                    STORE_LAT IS NOT NULL
                                    AND STORE_LONG IS NOT NULL
                                    AND DistanceCalculation.distance / 1000.0 <= @max_distance
                                    AND (SUB_BATCH IS NULL OR SUB_BATCH = '')  
                                    AND COMPANY_ID = '" & COMID.Text & "'
                                    AND SITE_ID = '" & SITID.Text & "'
                                    AND (ORDER_DATE IS NULL OR ORDER_DATE = '" & Reports_Order_Plan_Re_Assign_Per_Seller.DTDELIVERY.Value & "')  
                                    AND (SO_PLAN_NUMBER IS NULL OR SO_PLAN_NUMBER = '" & BATCHNUMBER.Text & "') 
                                ORDER BY DistanceCalculation.distance;", "BSPIDB", DTGRESULT)

                                Catch ex As Exception
                                    MsgBox("ERROR LIST: " & ex.ToString)
                                End Try

                                ' Ensure DTGRESULT has data before proceeding
                                If DTGRESULT.Rows.Count = 0 Then
                                    MsgBox("No data found for sub-batch " & (i + 1) & "!")
                                    Continue For
                                End If

                                If Integer.TryParse(STORELIMIT.Text, qty1) Then
                                    For i1 = 0 To qty1 - 1  ' Ensure zero-based index

                                        ' Prevent accessing rows that don't exist
                                        If i1 >= DTGRESULT.Rows.Count Then
                                            MsgBox("DTGRESULT does not have enough rows!")
                                            Exit For
                                        End If

                                        DASHORDERID.Text = DTGRESULT.Rows(i1).Cells(0).Value
                                        SaveProcess1(i1)

                                    Next


                                End If

                            Next

                            Me.Enabled = True

                            MsgBox("SUB BATCH ASSIGNMENT COMPLETE", MsgBoxStyle.Information, "COMPLETE")

                        Else
                            MessageBox.Show("Please enter a positive quantity.")
                        End If
                    Else
                        MessageBox.Show("Invalid quantity entered. Please enter a valid number.")
                    End If

                Catch ex As Exception
                    MsgBox("Unexpected Error: " & ex.ToString)
                Finally
                    Me.Enabled = True
                End Try

            End If

        End If

    End Sub

    Private Sub SaveProcess1(ByVal itemIndex As Integer)

        Try

            Dim a As String = "UPDATE Dash_SO_Plan_Batch_Details SET SUB_BATCH = '" & SUBBATCHID.Text & "' WHERE SO_NUMBER = '" & DASHORDERID.Text & "' AND ORDER_DATE = '" & Reports_Order_Plan_Re_Assign_Per_Seller.DTDELIVERY.Value & "'"

            Data(a)

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

    End Sub


End Class
Imports System.ComponentModel

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.SqlClient

Imports Microsoft.Web.WebView2.WinForms

Imports System.Text.Json

Public Class Transaction_Delivery_Plan
    Private Sub Transaction_Delivery_Plan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        COMPANYID.Text = Form1.GETCOMPANYID()

        SITEID.Text = Form1.GETSITEID()

        Control.CheckForIllegalCrossThreadCalls = False
        DTORDER.Value = Form1.DTCALENDAR.Value

        ' Get the current date from the DateTimePicker
        Dim currentDate1 As DateTime = DTORDER.Value

        ' Add one day to the current date
        Dim newDate1 As DateTime = currentDate1.AddDays(-1)

        ' Update the DateTimePicker with the new date
        DTORDER.Value = newDate1

        Try

            views("SELECT 
                        [COMPANY_ID]
                        ,[SITE_ID]
                        ,[SO_PLAN_NUMBER]
                        ,[DATE_SO]
                        ,[VEHICLE_ID]
                        ,[SO_PICK_BATCH]
                        ,[STATUS]
                    FROM [dbo].[Dash_SO_Plan_Transaction]

                    WHERE COMPANY_ID = '" & COMPANY_ID.Text & "' AND SITE_ID = '" & SITE_ID.Text & "' AND DATE_SO = '" & DTORDER.Value & "'", "BSPIDB", DTGSO)

        Catch ex As Exception

            ' MsgBox(ex.ToString)

        End Try

        DTDELIVERY.Value = Form1.DTCALENDAR.Value

        ' Get the current date from the DateTimePicker
        Dim currentDate As DateTime = DTDELIVERY.Value

        ' Add one day to the current date
        Dim newDate As DateTime = currentDate.AddDays(1)

        ' Update the DateTimePicker with the new date
        DTDELIVERY.Value = newDate

        DTINVOICE.Value = Form1.DTCALENDAR.Value

        COMPANY_ID.Text = Form1.GETCOMPANYID
        SITE_ID.Text = Form1.GETSITEID

        DTINVOICE.Value = Form1.DTCALENDAR.Value

        Try

            views("SELECT [LINE_ID]
                               ,[COMPANY_ID]
                               ,[SITE_ID]
                               ,[SO_PLAN_NUMBER]
                               ,[SO_NUMBER]
                               ,[CUSTOMER_ID]
                               ,[CUSTOMER_NAME]
                               ,[TOTAL_AMOUNT]
                               ,[STORE_LAT]
                               ,[STORE_LONG]
                               ,[ORDER_DATE]
                               ,[STATUS]
                           FROM [dbo].[Dash_SO_Plan_Batch_Details]

                   WHERE COMPANY_ID = '" & COMPANY_ID.Text & "' AND SITE_ID = '" & SITE_ID.Text & "' AND ORDER_DATE = '" & DTORDER.Value & "'", "BSPIDB", DTGSO)

            DTGSO.Columns(0).Visible = False
            DTGSO.Columns(1).Visible = False
            DTGSO.Columns(2).Visible = False

            DTGSO.Columns(10).Visible = False
            DTGSO.Columns(11).Visible = False

        Catch ex As Exception

            ' MsgBox(ex.ToString)

        End Try

        DTDELIVERY.Value = Form1.DTCALENDAR.Value

        DTDELIVERY.Value = newDate1




    End Sub

    Private Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click

        If DTGSO.Rows.Count = 0 Then

            MsgBox("NO ORDER FOR ORDER DATE SELECTED", MsgBoxStyle.Exclamation, "SORRY")

        Else

            TOTALSO.Text = DTGSO.Rows.Count

            Try

                views1("SELECT COMPANY_ID,SITE_ID,[CUSTOMER_ID] ,[CUSTOMER_NAME], INVOICE_NUMBER,TOTAL_VALUE

                         FROM [dbo].[PRFR_Invoice_Transaction]

                         WHERE DATE_INVOICE =  '" & DTINVOICE.Value & "' AND COMPANY_ID = '" & COMPANY_ID.Text & "' AND SITE_ID = '" & SITE_ID.Text & "' AND STATUS = 'INVOICED'

                     ", "BSPIDB", DTGINVOICES)

            Catch ex As Exception

            End Try

            If DTGINVOICES.Rows.Count = 0 Then

                MsgBox("NO UPLOADED INVOICES FOR INVOICE DATE SELECTED", MsgBoxStyle.Exclamation, "SORRY")

            Else

                TOTALINVOICE.Text = DTGINVOICES.Rows.Count

                '   BATCHNUMBER.Text = ""
                '
                '   Try
                '
                '       views2("SELECT BATCH_ID FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & COMPANY_ID.Text & "' AND SITE_ID = '" & SITE_ID.Text & "' AND DATE_TO_DELIVER = '" & DTDELIVERY.Value & "' AND STATUS = 'PROCESSED'", "BSPIDB", DTGCHECK)
                '       BATCHNUMBER.Text = DTGCHECK.CurrentRow.Cells(0).Value
                '
                '   Catch ex As Exception
                '
                '       ' MsgBox(ex.ToString)
                '
                '   End Try
                '
                '   If BATCHNUMBER.Text = "" Then

                lblprocess.Text = "PROCESSING FILE"

                Try

                    views2("SELECT BATCH_ID FROM Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & COMPANY_ID.Text & "' AND SITE_ID = '" & SITE_ID.Text & "' AND ORDER_DATE = '" & DTORDER.Value & "'", "BSPIDB", DTGCHECK)
                    BATCHNUMBER.Text = DTGCHECK.CurrentRow.Cells(0).Value

                Catch ex As Exception

                    '  MsgBox(ex.ToString)

                End Try

                Try

                    views2("SELECT IS_PLAN FROM Dash_SO_Plan_Batch_Details WHERE IS_PLAN = '1' AND  COMPANY_ID = '" & COMPANY_ID.Text & "' AND SITE_ID = '" & SITE_ID.Text & "' AND ORDER_DATE = '" & DTORDER.Value & "' GROUP BY IS_PLAN", "BSPIDB", DTGCHECK)
                    CHECKID.Text = DTGCHECK.CurrentRow.Cells(0).Value

                Catch ex As Exception

                    '  MsgBox(ex.ToString)

                End Try

                BATCHNUMBER.Text = ""

                If DTGCHECK.Rows.Count > 0 Then

                    Dim k As MsgBoxResult

                    k = MsgBox("THERE ARE ALREADY PROCESSED TRANSACTION, ONLY UNPROCESSED ORDER WILL BE INCLUDED ON PLAN. WOULD YOU LIKE TO PROCEED?", MsgBoxStyle.YesNo, "CONFIRM")

                    ' End If
                    If k = MsgBoxResult.Yes Then

                        Try

                            views("SELECT [LINE_ID]
                                      ,[COMPANY_ID]
                                      ,[SITE_ID]
                                      ,[SO_PLAN_NUMBER]
                                      ,[SO_NUMBER]
                                      ,[CUSTOMER_ID]
                                      ,[CUSTOMER_NAME]
                                      ,[TOTAL_AMOUNT]
                                      ,[STORE_LAT]
                                      ,[STORE_LONG]
                                      ,[ORDER_DATE]
                                      ,[STATUS]
                                  FROM [dbo].[Dash_SO_Plan_Batch_Details]

                          WHERE COMPANY_ID = '" & COMPANY_ID.Text & "' AND SITE_ID = '" & SITE_ID.Text & "' AND ORDER_DATE = '" & DTORDER.Value & "' AND (IS_PLAN = '0' OR IS_PLAN IS NULL)", "BSPIDB", DTGSO)

                            DTGSO.Columns(0).Visible = False
                            DTGSO.Columns(1).Visible = False
                            DTGSO.Columns(2).Visible = False
                            '  DTGSO.Columns(3).Visible = False
                            ' DTGSO.Columns(4).Visible = False
                            ' DTGSO.Columns(8).Visible = False
                            '  DTGSO.Columns(9).Visible = False
                            DTGSO.Columns(10).Visible = False
                            DTGSO.Columns(11).Visible = False

                        Catch ex As Exception

                            ' MsgBox(ex.ToString)

                        End Try

                        storecount.Text = "1"

                        Me.Enabled = False

                        BackgroundWorker1.RunWorkerAsync()

                    End If

                Else

                    Try

                        views("SELECT [LINE_ID]
                                      ,[COMPANY_ID]
                                      ,[SITE_ID]
                                      ,[SO_PLAN_NUMBER]
                                      ,[SO_NUMBER]
                                      ,[CUSTOMER_ID]
                                      ,[CUSTOMER_NAME]
                                      ,[TOTAL_AMOUNT]
                                      ,[STORE_LAT]
                                      ,[STORE_LONG]
                                      ,[ORDER_DATE]
                                      ,[STATUS]
                                  FROM [dbo].[Dash_SO_Plan_Batch_Details]

                          WHERE COMPANY_ID = '" & COMPANY_ID.Text & "' AND SITE_ID = '" & SITE_ID.Text & "' AND ORDER_DATE = '" & DTORDER.Value & "' AND (IS_PLAN = '0' OR IS_PLAN IS NULL)", "BSPIDB", DTGSO)

                        DTGSO.Columns(0).Visible = False
                        DTGSO.Columns(1).Visible = False
                        DTGSO.Columns(2).Visible = False
                        '  DTGSO.Columns(3).Visible = False
                        ' DTGSO.Columns(4).Visible = False
                        ' DTGSO.Columns(8).Visible = False
                        '  DTGSO.Columns(9).Visible = False
                        DTGSO.Columns(10).Visible = False
                        DTGSO.Columns(11).Visible = False

                    Catch ex As Exception

                        ' MsgBox(ex.ToString)

                    End Try

                    storecount.Text = "1"

                    Me.Enabled = False

                    BackgroundWorker1.RunWorkerAsync()

                End If

                '   Else

                ' MsgBox("CANNOT PROCESS RE-ROUTING WHEN SOME TRANSACTIONS ARE ALREADY PROCESSED", MsgBoxStyle.Exclamation, "SORRY")

            End If

        End If

    End Sub

    Dim NUMOFINV As Integer
    Dim TOTALDROPS As Integer

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        Try

            For i = 0 To DTGINVOICES.Rows.Count - 1 Step +1

                '  GunaProgressBar1.Value = i

                Dim x As Decimal
                'Dim y As Decimal
                Dim z As Decimal
                Dim a As Decimal

                x = DTGINVOICES.Rows.Count - 1

                z = i / x

                a = z * 100

                GunaProgressBar1.Value = a
                LBLCOUNT.Text = a

                Try

                    LBLCOUNT.Text = Decimal.Parse(LBLCOUNT.Text).ToString("n2") & "%"

                Catch ex As Exception

                End Try

                Try

                    INVOICENUMBER.Text = DTGINVOICES.Rows(i).Cells(4).Value
                    CUSTOMERID.Text = DTGINVOICES.Rows(i).Cells(2).Value
                    CUSTOMERNAME.Text = DTGINVOICES.Rows(i).Cells(3).Value
                    Dim lA As String
                    lA = CUSTOMERNAME.Text

                    lA = lA.Replace("'", "")

                    CUSTOMERNAME.Text = lA

                    TOTALVALUE.Text = DTGINVOICES.Rows(i).Cells(5).Value

                Catch ex As Exception

                End Try

                Try

                    views2("SELECT CUSTOMER_ID,CUSTOMER_NAME,Dash_SO_Plan_Transaction.SO_PLAN_NUMBER,STORE_LAT,STORE_LONG,VEHICLE_ID,DISTANCE,
                      COALESCE(SUB_BATCH, '') AS SUB_BATCH,
                      COALESCE(SUB_DA, '') AS SUB_DA,
                       COALESCE(VEHICLE_IDS, '') AS VEHICLE_ID
                        ,Dash_SO_Plan_Batch_Details.LINE_ID

                      FROM Dash_SO_Plan_Batch_Details
                      
                      LEFT JOIN Dash_SO_Plan_Transaction ON Dash_SO_Plan_Transaction.SO_PLAN_NUMBER = Dash_SO_Plan_Batch_Details.SO_PLAN_NUMBER
                      
                      WHERE Dash_SO_Plan_Transaction.COMPANY_ID = '" & COMPANY_ID.Text & "' AND Dash_SO_Plan_Transaction.SITE_ID = '" & SITE_ID.Text & "' AND ORDER_DATE = '" & DTORDER.Value & "' AND CUSTOMER_ID = '" & CUSTOMERID.Text & "' AND (Dash_SO_Plan_Batch_Details.IS_PLAN = '0' OR Dash_SO_Plan_Batch_Details.IS_PLAN IS NULL)", "BSPIDB", DTGSOCHECK)

                    DTGSOCHECK.Columns(10).Visible = False

                    BATCHNUMBER.Text = DTGSOCHECK.CurrentRow.Cells(2).Value
                    LATFROM.Text = DTGSOCHECK.CurrentRow.Cells(3).Value
                    LONGFROM.Text = DTGSOCHECK.CurrentRow.Cells(4).Value
                    DISTANCETXT.Text = DTGSOCHECK.CurrentRow.Cells(6).Value

                    SUBBATCH.Text = DTGSOCHECK.CurrentRow.Cells(7).Value
                    SUB_DA.Text = DTGSOCHECK.CurrentRow.Cells(8).Value
                    VEHICLEIDS.Text = DTGSOCHECK.CurrentRow.Cells(9).Value
                    LINEID.Text = DTGSOCHECK.CurrentRow.Cells(10).Value

                Catch ex As Exception

                    '  MsgBox("DDA" & ex.ToString)

                End Try

                If DTGSOCHECK.Rows.Count > 0 Then

                    Try

                        Dim L As String = "UPDATE Dash_SO_Plan_Batch_Details SET IS_PLAN = '1' WHERE LINE_ID = '" & LINEID.Text & "'"

                        Data(L)


                    Catch ex As Exception

                    End Try

                    Try
                        views2("SELECT SUM(CS) AS CS_TOTAL

                      FROM PRFR_Invoice_Detailed
                      
                      WHERE DISTRIBUTOR_CODE = '" & COMPANY_ID.Text & "' AND [BRANCH_CODE] = '" & SITE_ID.Text & "' AND [DOCUMENT_NUMBER] = '" & INVOICENUMBER.Text & "'", "BSPIDB", DTGCHECK)

                        TOTALVOLUME.Text = DTGCHECK.CurrentRow.Cells(0).Value

                    Catch ex As Exception

                        '   MsgBox("DDA" & ex.ToString)

                    End Try


                    Dim TOTVAL As Decimal
                    Dim TOTDEC As Decimal

                    Try

                        TOTVAL = TOTALVALUE.Text
                        TOTDEC = DISTANCETXT.Text

                    Catch EX As Exception

                    End Try

                    Try

                        Dim kf As String = "INSERT INTO Dash_Plan_Batch_Details(COMPANY_ID,SITE_ID,BATCH,INVOICE_NUMBER,TOTAL_AMOUNT,INVOICE_VOLUME,DISTANCE,DISTANCE_IN_DECIMAL,STATUS,DATE_TO_DELIVER,STORE_LAT,STORE_LONG,CUSTOMER_ID,CUSTOMER_NAME,AGENT_ID,ORDER_DATE,SUB_BATCH,SUB_DA,IS_RECEIVED,VEHICLE_IDS)" _
                           & "VALUES('" & COMPANY_ID.Text & "'," _
                          & "'" & SITE_ID.Text & "'," _
                            & "'" & "DLV" & BATCHNUMBER.Text & "'," _
                                & "'" & INVOICENUMBER.Text & "'," _
                                  & "'" & TOTVAL & "'," _
                                & " '" & TOTALVOLUME.Text & "', " _
                                & "'" & DISTANCETXT.Text & "'," _
                                   & "'" & TOTDEC & "'," _
                                  & "'READY'," _
                                      & "'" & DTDELIVERY.Value & "'," _
                                          & "'" & LATFROM.Text & "'," _
                                           & "'" & LONGFROM.Text & "'," _
                                            & "'" & CUSTOMERID.Text & "'," _
                                               & "'" & CUSTOMERNAME.Text & "'," _
                                                  & "''," _
                                                         & "'" & DTORDER.Value & "'," _
                                                      & "'" & "DLV" & SUBBATCH.Text & "'," _
                                               & "'" & SUB_DA.Text & "'," _
                                                  & "'0'," _
                                       & "'" & VEHICLEIDS.Text & "')"

                        Data(kf)

                    Catch ex As Exception

                    End Try

                End If

            Next

            Try

                views2("SELECT 
               [COMPANY_ID]
               ,[SITE_ID]
               ,[BATCH]
               ,COUNT(INVOICE_NUMBER) AS NUM_OF_INV
               ,SUM(TOTAL_AMOUNT) AS TOTAL_AMOUNT
                  ,SUM(INVOICE_VOLUME) AS VOLUME

           FROM [dbo].[Dash_Plan_Batch_Details]

           WHERE  COMPANY_ID = '" & COMPANY_ID.Text & "' AND SITE_ID = '" & SITE_ID.Text & "' AND DATE_TO_DELIVER = '" & DTDELIVERY.Value & "'
            
           GROUP BY  [COMPANY_ID]
               ,[SITE_ID]
                 ,[BATCH] ", "BSPIDB", DTGBATCH)

            Catch ex As Exception

            End Try

            DTGBATCH.Columns(0).Visible = False
            DTGBATCH.Columns(1).Visible = False

            For i = 0 To DTGBATCH.Rows.Count - 1 Step +1

                '  MsgBox("NEW")
                BATCHNUMBER.Text = Mid(DTGBATCH.Rows(i).Cells(2).Value.ToString(), 4)

                Try

                    views3("SELECT  [VEHICLE_ID]
               
                       FROM [dbo].[Dash_SO_Plan_Transaction] WHERE COMPANY_ID = '" & COMPANY_ID.Text & "' AND SITE_ID = '" & SITE_ID.Text & "' AND SO_PLAN_NUMBER = '" & BATCHNUMBER.Text & "'", "BSPIDB", DTGCHECK)

                    VEHICLEID.Text = DTGCHECK.CurrentRow.Cells(0).Value

                Catch ex As Exception

                    '  MsgBox(ex.ToString)

                End Try

                ' NUMOFINV = DTGCHECK.Rows.Count

                Try

                    views3("SELECT 
                              count(CUSTOMER_ID)
          
                         FROM [dbo].[Dash_Plan_Batch_Details] WHERE COMPANY_ID = '" & COMPANY_ID.Text & "' AND SITE_ID = '" & SITE_ID.Text & "' AND BATCH = '" & BATCHNUMBER.Text & "'", "BSPIDB", DTGCHECK)

                    TOTALDROPS = DTGCHECK.CurrentRow.Cells(0).Value

                Catch ex As Exception

                    '  MsgBox(ex.ToString)

                End Try

                '  Try
                '
                '      Dim K As String = "UPDATE Dash_Plan_Batch_Details SET BATCH = 'DLV'+ BATCH WHERE COMPANY_ID = '" & COMPANY_ID.Text & "' AND SITE_ID = '" & SITE_ID.Text & "' AND BATCH = '" & BATCHNUMBER.Text & "'"
                '
                '      Data(K)
                '
                '  Catch ex As Exception
                '
                '  End Try

                Try

                    views3("SELECT BATCH_ID from Dash_Plan_Batch_Transaction
                              
                         WHERE COMPANY_ID = '" & COMPANY_ID.Text & "' AND SITE_ID = '" & SITE_ID.Text & "' AND BATCH_ID = 'DLV" & BATCHNUMBER.Text & "'", "BSPIDB", DTGCHECK)

                Catch ex As Exception

                    '  MsgBox(ex.ToString)

                End Try

                If DTGCHECK.Rows.Count > 0 Then

                Else

                    Try

                        Dim TOTVAL As Decimal
                        Dim TOTDEC As Decimal

                        TOTVAL = DTGBATCH.Rows(i).Cells(4).Value
                        TOTDEC = 0

                        Dim kf1 As String = "INSERT INTO Dash_Plan_Batch_Transaction(COMPANY_ID,SITE_ID,BATCH_ID,DROP_COUNT,NUM_OF_INVOICES,TOTAL_VALUE,TOTAL_VOLUME,WEIGHT,STATUS,VEHICLE_ID,AGENT,DATE_TO_DELIVER,ORDER_DATE)" _
                       & "VALUES('" & COMPANY_ID.Text & "'," _
                      & "'" & SITE_ID.Text & "'," _
                        & "'" & "DLV" & BATCHNUMBER.Text & "'," _
                            & "'" & TOTALDROPS & "'," _
                              & "'" & DTGBATCH.Rows(i).Cells(3).Value & "'," _
                            & " '" & TOTVAL & "', " _
                            & "'" & DTGBATCH.Rows(i).Cells(5).Value & "'," _
                                & "'" & TOTDEC & "'," _
                                     & "'READY'," _
                                         & "'" & VEHICLEID.Text & "'," _
                                           & "''," _
                                                & "'" & DTDELIVERY.Value & "'," _
                                   & "'" & DTORDER.Value & "')"

                        Data(kf1)

                        '  MsgBox("SAVE TRANSACTION")

                    Catch ex As Exception

                        '  MsgBox("4" & ex.ToString)

                    End Try

                End If

            Next

        Catch ex As Exception

            MsgBox("ALL " & ex.ToString)

        End Try

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

        Me.Enabled = True

        TOTALBATCH.Text = DTGBATCH.Rows.Count

        GunaButton1.PerformClick()

        MsgBox("DELIVERY PLAN CREATED", MsgBoxStyle.Information, "COMPLETE")

    End Sub

    Private Sub DTORDER_ValueChanged(sender As Object, e As EventArgs) Handles DTORDER.ValueChanged

        DTINVOICE.Value = Form1.DTCALENDAR.Value

        Try

            views("SELECT [LINE_ID]
                                      ,[COMPANY_ID]
                                      ,[SITE_ID]
                                      ,[SO_PLAN_NUMBER]
                                      ,[SO_NUMBER]
                                      ,[CUSTOMER_ID]
                                      ,[CUSTOMER_NAME]
                                      ,[TOTAL_AMOUNT]
                                      ,[STORE_LAT]
                                      ,[STORE_LONG]
                                      ,[ORDER_DATE]
                                      ,[STATUS]
                                  FROM [dbo].[Dash_SO_Plan_Batch_Details]

                          WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND ORDER_DATE = '" & DTORDER.Value & "'", "BSPIDB", DTGSO)

            DTGSO.Columns(0).Visible = False
            DTGSO.Columns(1).Visible = False
            DTGSO.Columns(2).Visible = False
            '  DTGSO.Columns(3).Visible = False
            ' DTGSO.Columns(4).Visible = False
            ' DTGSO.Columns(8).Visible = False
            '  DTGSO.Columns(9).Visible = False
            DTGSO.Columns(10).Visible = False
            DTGSO.Columns(11).Visible = False

        Catch ex As Exception

            ' MsgBox(ex.ToString)

        End Try

        DTDELIVERY.Value = Form1.DTCALENDAR.Value

        ' Get the current date from the DateTimePicker
        Dim currentDate1 As DateTime = DTDELIVERY.Value

        ' Add one day to the current date
        Dim newDate1 As DateTime = currentDate1.AddDays(1)

        ' Update the DateTimePicker with the new date
        DTDELIVERY.Value = newDate1

    End Sub

    Private Sub DTINVOICE_ValueChanged(sender As Object, e As EventArgs) Handles DTINVOICE.ValueChanged

        Try

            views1("SELECT COMPANY_ID,SITE_ID,[CUSTOMER_ID] ,[CUSTOMER_NAME], INVOICE_NUMBER,TOTAL_VALUE

              FROM [dbo].[PRFR_Invoice_Transaction]

              WHERE DATE_INVOICE =  '" & DTINVOICE.Value & "' AND COMPANY_ID = '" & COMPANY_ID.Text & "' AND SITE_ID = '" & SITE_ID.Text & "' AND STATUS = 'INVOICED'

          ", "BSPIDB", DTGINVOICES)

        Catch ex As Exception

            '  MsgBox(ex.ToString)

        End Try

    End Sub


    Private Async Sub GunaButton1_Click(sender As Object, e As EventArgs) Handles GunaButton1.Click

        Try
            Await WebView21.EnsureCoreWebView2Async(Nothing)
        Catch ex As Exception
            MsgBox("Error initializing WebView2: " & ex.Message)
        End Try
        ' DTDELIVERY.Value = Form1.DTCALENDAR.Value

        ' Add handler for WebView2 messages
        '  AddHandler WebView21.CoreWebView2.WebMessageReceived, AddressOf WebView21_WebMessageReceived

        DTGROUTES.DataSource = Nothing

        Try

            views3("SELECT
                    	BATCH,
                          [DATE_TO_DELIVER]
                          ,[STORE_LAT]
                          ,[STORE_LONG]
                          ,[CUSTOMER_ID]
                          ,[CUSTOMER_NAME]
                    
                      FROM [dbo].[Dash_Plan_Batch_Details]
                    
                      WHERE [DATE_TO_DELIVER] = '" & DTDELIVERY.Value & "' AND COMPANY_ID = '" & COMPANY_ID.Text & "' AND SITE_ID = '" & SITE_ID.Text & "'",
                   "BSPIDB", DTGROUTES)

            ' Prepare HTML
            Dim htmlContent As String = "
            <html>
            <head>
                <title>Bing Maps</title>
                <script type='text/javascript' src='https://www.bing.com/api/maps/mapcontrol?key=u8bVA0NAuVbJ5r3MZgUR~fk8RCjst0j6nqTGJqGeK6w~Aj-CdHthPN5zeNnT6yh7YYRy1RGcZ3--xAUdQNkjP7_5t7ysVrqpNUvA6QYV-fYC'></script>
                <script type='text/javascript'>
                    var map;
                    var locations = [];
                    var uniqueSOPlans = {};

                    function getPushpinColor(soPlanNumber) {
                        var hash = 0;
                        for (var i = 0; i < soPlanNumber.length; i++) {
                            hash = (hash << 5) - hash + soPlanNumber.charCodeAt(i);
                            hash = hash & hash;
                        }
                        var colorIndex = Math.abs(hash % 12);
                        var colors = ['#FF0000', '#00FF00', '#0000FF', '#FFFF00', '#FF00FF', '#00FFFF', '#800000', '#008000', '#000080', '#808000', '#800080', '#008080'];
                        return colors[colorIndex];
                    }

                    function initialize() {
                        map = new Microsoft.Maps.Map('#myMap', {
                            center: new Microsoft.Maps.Location(13.7563, 100.5018),
                            zoom: 8
                        });
            "

        ' Calculate map bounds
        Dim minLat As Double = Double.MaxValue
        Dim maxLat As Double = Double.MinValue
        Dim minLng As Double = Double.MaxValue
        Dim maxLng As Double = Double.MinValue

        For Each row As DataGridViewRow In DTGROUTES.Rows
            If row.Cells("STORE_LAT").Value IsNot DBNull.Value AndAlso row.Cells("STORE_LONG").Value IsNot DBNull.Value Then
                Dim lat As String = row.Cells("STORE_LAT").Value.ToString()
                Dim lng As String = row.Cells("STORE_LONG").Value.ToString()
                    Dim soPlanNumber As String = row.Cells("BATCH").Value.ToString()
                    Dim customerId As String = row.Cells("CUSTOMER_ID").Value.ToString().Replace("'", "\'")
                Dim customerName As String = row.Cells("CUSTOMER_NAME").Value.ToString().Replace("'", "\'")

                Dim latitude As Double = Convert.ToDouble(lat)
                Dim longitude As Double = Convert.ToDouble(lng)
                minLat = Math.Min(minLat, latitude)
                maxLat = Math.Max(maxLat, latitude)
                minLng = Math.Min(minLng, longitude)
                maxLng = Math.Max(maxLng, longitude)

                ' Add pushpins
                htmlContent &= "locations.push({latitude: " & lat & ", longitude: " & lng & ", soPlanNumber: '" & soPlanNumber & "', customerId: '" & customerId & "', customerName: '" & customerName & "'});"
                htmlContent &= "uniqueSOPlans['" & soPlanNumber & "'] = true;"
            End If
        Next

        htmlContent &= "
                        for (var i = 0; i < locations.length; i++) {
                            var location = new Microsoft.Maps.Location(locations[i].latitude, locations[i].longitude);
                            var color = getPushpinColor(locations[i].soPlanNumber);
                            var pushpin = new Microsoft.Maps.Pushpin(location, { color: color });

                            Microsoft.Maps.Events.addHandler(pushpin, 'click', (function(loc) {
                                return function() {
                                    window.chrome.webview.postMessage({
                                        customerId: loc.customerId,
                                        customerName: loc.customerName,
                                        soPlanNumber: loc.soPlanNumber
                                    });
                                };
                            })(locations[i]));

                            map.entities.push(pushpin);
                        }

                        // Fit bounds
                        var bounds = Microsoft.Maps.LocationRect.fromCorners(
                            new Microsoft.Maps.Location(" & minLat.ToString() & ", " & minLng.ToString() & "),
                            new Microsoft.Maps.Location(" & maxLat.ToString() & ", " & maxLng.ToString() & ")
                        );
                        map.setView({ bounds: bounds });

                        // Create Legend
                        var legendHtml = '<div style=""position:absolute;top:10px;left:10px;background:white;padding:10px;border-radius:5px;box-shadow:0 0 5px rgba(0,0,0,0.5);z-index:999;"">';
                        legendHtml += '<b>Legend:</b><br>';
                        for (var soPlan in uniqueSOPlans) {
                            var color = getPushpinColor(soPlan);
                            legendHtml += '<div style=""display:flex;align-items:center;margin-bottom:5px;"">';
                            legendHtml += '<div style=""background-color:' + color + ';width:20px;height:20px;margin-right:5px;""></div>' + soPlan;
                            legendHtml += '</div>';
                        }
                        legendHtml += '</div>';
                        document.body.insertAdjacentHTML('beforeend', legendHtml);
                    }

                    window.onload = initialize;
                </script>
            </head>
            <body style='margin:0;padding:0;'>
                <div id='myMap' style='position:relative;width:100%;height:100%;'></div>
            </body>
            </html>"

        ' Load to WebView
        WebView21.NavigateToString(htmlContent)

        Catch ex As Exception
        MsgBox("Error: " & ex.ToString())
        End Try

    End Sub

    Private Sub GunaAdvenceButton2_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton2.Click

        Dim L As MsgBoxResult

        L = MsgBox("ORDER DATE TRANSACTIONS HAS ALREADY PLAN, WOULD YOU LIKE TO OVERRIDE CURRENT ROUTE?", MsgBoxStyle.YesNo, "CONFIRM")

        If L = MsgBoxResult.Yes Then

            Try

                Dim A As String = "UPDATE PRFR_Invoice_Transaction SET IS_PLAN = '0' WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND ORDER_DATE = '" & DTORDER.Value & "'"

                Data(A)

            Catch ex As Exception

            End Try

            Try

                Dim A As String = "UPDATE Dash_SO_Plan_Batch_Details SET IS_PLAN = '0' WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND ORDER_DATE = '" & DTORDER.Value & "'"

                Data(A)

            Catch ex As Exception

            End Try

            Try

                Dim A As String = "DELETE Dash_Plan_Batch_Transaction WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND ORDER_DATE = '" & DTORDER.Value & "'"

                Data(A)

            Catch ex As Exception

            End Try

            Try

                Dim A As String = "DELETE Dash_Plan_Batch_Details WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND ORDER_DATE = '" & DTORDER.Value & "'"

                Data(A)

            Catch ex As Exception

            End Try

            ' MsgBox("START")

            ''' START NORMAL

            MsgBox("DELIVERY PLAN REVERTED", MsgBoxStyle.Information, "COMPLETE")

        End If

    End Sub

End Class
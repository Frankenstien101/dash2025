Public Class Setup_Van_Master_Manual
    Private Sub GunaLabel2_Click(sender As Object, e As EventArgs) Handles GunaLabel2.Click

        Close()

    End Sub

    Private Sub Setup_Van_Master_Manual_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If TRIGGER.Text = "SUB" Then

            Try

                views("SELECT LINE_ID,
                   [PLATE_NUM]
                  ,[TYPE]
                  ,[BODY_TYPE]
                  ,[WHEEL_TYPE]
                  ,[VOLUME_LIMIT]
                  ,[WAREHOUSE_LAT]
                  ,[WAREHOUSE_LONG]
                  ,[PRIORITY_COUNT]
                  ,[STORES_LIMIT]
              FROM [dbo].[Dash_Vehicles] WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND IS_SUB = 'YES' ORDER BY PRIORITY_COUNT ASC", "BSPIDB", DTGDATA)

                DTGDATA.Columns(0).Visible = False

            Catch ex As Exception

                MsgBox("3" & ex.ToString)

            End Try

        Else

            Try

                views("SELECT VEHICLE_LIMIT,
                  
              FROM [dbo].[Dash_Sites] WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "'", "BSPIDB", DTGDATA)

                NUMOFTRUCKS.Value = DTGDATA.CurrentRow.Cells(0).Value

            Catch ex As Exception

            End Try

            Try

                views("SELECT LINE_ID,
                   [PLATE_NUM]
                  ,[TYPE]
                  ,[BODY_TYPE]
                  ,[WHEEL_TYPE]
                  ,[VOLUME_LIMIT]
                  ,[WAREHOUSE_LAT]
                  ,[WAREHOUSE_LONG]
                  ,[PRIORITY_COUNT]
                  ,[STORES_LIMIT]
              FROM [dbo].[Dash_Vehicles] WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' ORDER BY  PRIORITY_COUNT ASC", "BSPIDB", DTGDATA)

                DTGDATA.Columns(0).Visible = False

            Catch ex As Exception

                '  MsgBox("3" & ex.ToString)

            End Try

        End If

    End Sub

    Dim LINEID As String

    Private Sub DTGDATA_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DTGDATA.CellClick

        LINEID = ""
        PLANTENUM.Text = ""
        TYPE.Text = ""
        BODYTYPE.Text = ""
        WHEELTYPE.Text = ""
        VOLLIMIT.Text = ""
        LATFROM.Text = ""
        LONGFROM.Text = ""
        PRIORNUM.Text = ""
        STORELIMIT.Text = ""

        Try

            LINEID = DTGDATA.CurrentRow.Cells(0).Value
            PLANTENUM.Text = DTGDATA.CurrentRow.Cells(1).Value
            TYPE.Text = DTGDATA.CurrentRow.Cells(2).Value
            BODYTYPE.Text = DTGDATA.CurrentRow.Cells(3).Value
            WHEELTYPE.Text = DTGDATA.CurrentRow.Cells(4).Value
            VOLLIMIT.Text = DTGDATA.CurrentRow.Cells(5).Value
            LATFROM.Text = DTGDATA.CurrentRow.Cells(6).Value
            LONGFROM.Text = DTGDATA.CurrentRow.Cells(7).Value
            PRIORNUM.Text = DTGDATA.CurrentRow.Cells(8).Value
            STORELIMIT.Text = DTGDATA.CurrentRow.Cells(9).Value

        Catch ex As Exception

        End Try

    End Sub

    Private Sub GunaAdvenceButton1_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton1.Click

        If LINEID = "" Then

            MsgBox("NO DATA TO UPDATE", MsgBoxStyle.Exclamation, "SORRY")

        Else

            Dim L As MsgBoxResult

            L = MsgBox("ARE YOU SURE TO UPDATE NEW CHANGES?", MsgBoxStyle.YesNo, "CONFIRM")

            If L = MsgBoxResult.Yes Then

                Try

                    Dim P As String = "UPDATE Dash_Vehicles SET PLATE_NUM = '" & PLANTENUM.Text & "', 
                                                            TYPE = '" & TYPE.Text & "', 
                                                             BODY_TYPE = '" & BODYTYPE.Text & "',
                                                            WHEEL_TYPE = '" & WHEELTYPE.Text & "',
                                                             VOLUME_LIMIT = '" & VOLLIMIT.Text & "',
                                                            WAREHOUSE_LAT = '" & LATFROM.Text & "',
                                                             WAREHOUSE_LONG = '" & LONGFROM.Text & "',
                                                            PRIORITY_COUNT = '" & PRIORNUM.Text & "',
                                                             STORES_LIMIT = '" & STORELIMIT.Text & "' WHERE LINE_ID = '" & LINEID & "'
"

                    Data(P)

                    MsgBox("VEHICLE UPDATED", MsgBoxStyle.Information, "COMPLETE")

                Catch ex As Exception

                End Try

                If TRIGGER.Text = "SUB" Then

                    Try

                        views("SELECT LINE_ID,
                         [PLATE_NUM]
                        ,[TYPE]
                        ,[BODY_TYPE]
                        ,[WHEEL_TYPE]
                        ,[VOLUME_LIMIT]
                        ,[WAREHOUSE_LAT]
                        ,[WAREHOUSE_LONG]
                        ,[PRIORITY_COUNT]
                        ,[STORES_LIMIT]
                    FROM [dbo].[Dash_Vehicles] WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND IS_SUB = 'YES' ORDER BY PRIORITY_COUNT ASC", "BSPIDB", DTGDATA)

                        DTGDATA.Columns(0).Visible = False

                    Catch ex As Exception

                        '  MsgBox("3" & ex.ToString)

                    End Try

                Else

                    Try

                        views("SELECT LINE_ID,
                         [PLATE_NUM]
                        ,[TYPE]
                        ,[BODY_TYPE]
                        ,[WHEEL_TYPE]
                        ,[VOLUME_LIMIT]
                        ,[WAREHOUSE_LAT]
                        ,[WAREHOUSE_LONG]
                        ,[PRIORITY_COUNT]
                        ,[STORES_LIMIT]
                    FROM [dbo].[Dash_Vehicles] WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' ORDER BY  PRIORITY_COUNT ASC", "BSPIDB", DTGDATA)

                        DTGDATA.Columns(0).Visible = False

                    Catch ex As Exception

                        '  MsgBox("3" & ex.ToString)

                    End Try

                End If

                LINEID = ""
                PLANTENUM.Text = ""
                TYPE.Text = ""
                BODYTYPE.Text = ""
                WHEELTYPE.Text = ""
                VOLLIMIT.Text = ""
                LATFROM.Text = ""
                LONGFROM.Text = ""
                PRIORNUM.Text = ""
                STORELIMIT.Text = ""

            End If

        End If

    End Sub

    Private Sub Setup_Van_Master_Manual_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        If TRIGGER.Text = "SUB" Then

            Try

                views("SELECT LINE_ID,
                         [PLATE_NUM]
                        ,[TYPE]
                        ,[BODY_TYPE]
                        ,[WHEEL_TYPE]
                        ,[VOLUME_LIMIT]
                        ,[WAREHOUSE_LAT]
                        ,[WAREHOUSE_LONG]
                        ,[PRIORITY_COUNT]
                        ,[STORES_LIMIT]
                    FROM [dbo].[Dash_Vehicles] WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' AND IS_SUB = 'YES' ORDER BY PRIORITY_COUNT ASC", "BSPIDB", DTGDATA)

                DTGDATA.Columns(0).Visible = False

            Catch ex As Exception

                '  MsgBox("3" & ex.ToString)

            End Try

        Else

            Try

                views("SELECT VEHICLE_LIMIT,
                        
                    FROM [dbo].[Dash_Sites] WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "'", "BSPIDB", DTGDATA)

                NUMOFTRUCKS.Value = DTGDATA.CurrentRow.Cells(0).Value

            Catch ex As Exception

            End Try


            Try

                views("SELECT LINE_ID,
                         [PLATE_NUM]
                        ,[TYPE]
                        ,[BODY_TYPE]
                        ,[WHEEL_TYPE]
                        ,[VOLUME_LIMIT]
                        ,[WAREHOUSE_LAT]
                        ,[WAREHOUSE_LONG]
                        ,[PRIORITY_COUNT]
                        ,[STORES_LIMIT]
                    FROM [dbo].[Dash_Vehicles] WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "' ORDER BY  PRIORITY_COUNT ASC", "BSPIDB", DTGDATA)

                DTGDATA.Columns(0).Visible = False

            Catch ex As Exception

                '  MsgBox("3" & ex.ToString)

            End Try

        End If

    End Sub

    Private Sub GunaAdvenceButton2_Click(sender As Object, e As EventArgs) Handles GunaAdvenceButton2.Click

        Dim O As MsgBoxResult

        O = MsgBox("ARE YOU SURE TO UPDATE TRUCK LIMIT?", MsgBoxStyle.YesNo, "CONFIRM")

        If O = MsgBoxResult.Yes Then

            Try

                Dim L As String = "UPDATE Dash_Sites SET VEHICLE_LIMIT = '" & NUMOFTRUCKS.Value & "' WHERE COMPANY_ID = '" & Form1.COMPANYID.Text & "' AND SITE_ID = '" & Form1.SITEID.Text & "'"

                Data(L)

            Catch ex As Exception

            End Try

            MsgBox("TRUCKS LIMIT UPDATED", MsgBoxStyle.Information, "COMPLETE")

        End If

    End Sub

End Class
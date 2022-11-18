---
title: "HelloData Code"
description: "HelloData Code"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "HelloData sample application [ADO], sample code"
---
# HelloData Code
```vb
'BeginHelloData  
Option Explicit  
  
Dim m_oRecordset As ADODB.Recordset  
Dim m_sConnStr As String  
Dim m_flgPriceUpdated As Boolean  
  
Private Sub cmdGetData_Click()  
    GetData  
  
    If Not m_oRecordset Is Nothing Then  
        If m_oRecordset.State = adStateOpen Then  
            ' Set the proper states for the buttons.  
            cmdGetData.Enabled = False  
            cmdExamineData.Enabled = True  
        End If  
    End If  
End Sub  
  
Private Sub cmdExamineData_Click()  
    ExamineData  
End Sub  
  
Private Sub cmdEditData_Click()  
    EditData  
End Sub  
  
Private Sub cmdUpdateData_Click()  
    UpdateData  
  
    ' Set the proper states for the buttons.  
    cmdUpdateData.Enabled = False  
End Sub  
  
Private Sub GetData()  
    On Error GoTo GetDataError  
  
    Dim sSQL As String  
    Dim oConnection1 As ADODB.Connection  
  
    m_sConnStr = "Provider='SQLOLEDB';Data Source='MySqlServer';" & _  
                "Initial Catalog='Northwind';Integrated Security='SSPI';"  
  
    ' Create and Open the Connection object.  
    Set oConnection1 = New ADODB.Connection  
    oConnection1.CursorLocation = adUseClient  
    oConnection1.Open m_sConnStr  
  
    sSQL = "SELECT ProductID, ProductName, CategoryID, UnitPrice " & _  
             "FROM Products"  
  
    ' Create and Open the Recordset object.  
    Set m_oRecordset = New ADODB.Recordset  
    m_oRecordset.Open sSQL, oConnection1, adOpenStatic, _  
                        adLockBatchOptimistic, adCmdText  
  
    m_oRecordset.MarshalOptions = adMarshalModifiedOnly  
  
    ' Disconnect the Recordset.  
    Set m_oRecordset.ActiveConnection = Nothing  
    oConnection1.Close  
    Set oConnection1 = Nothing  
  
    ' Bind Recordset to the DataGrid for display.  
    Set grdDisplay1.DataSource = m_oRecordset  
  
    Exit Sub  
  
GetDataError:  
    If Err <> 0 Then  
        If oConnection1 Is Nothing Then  
           HandleErrs "GetData", m_oRecordset.ActiveConnection  
        Else  
           HandleErrs "GetData", oConnection1  
        End If  
    End If  
  
    If Not oConnection1 Is Nothing Then  
        If oConnection1.State = adStateOpen Then oConnection1.Close  
        Set oConnection1 = Nothing  
    End If  
End Sub  
  
Private Sub ExamineData()  
    On Err GoTo ExamineDataErr  
  
    Dim iNumRecords As Integer  
    Dim vBookmark As Variant  
  
    iNumRecords = m_oRecordset.RecordCount  
  
    DisplayMsg "There are " & CStr(iNumRecords) & _  
                " records in the current Recordset."  
  
    ' Loop through the Recordset and print the  
    ' value of the AbsolutePosition property.  
    DisplayMsg "****** Start AbsolutePosition Loop ******"  
  
    Do While Not m_oRecordset.EOF  
        ' Store the bookmark for the 3rd record,  
        ' for demo purposes.  
        If m_oRecordset.AbsolutePosition = 3 Then _  
            vBookmark = m_oRecordset.Bookmark  
  
        DisplayMsg m_oRecordset.AbsolutePosition  
  
        m_oRecordset.MoveNext  
    Loop  
  
    DisplayMsg "****** End AbsolutePosition Loop ******" & vbCrLf  
  
    ' Use our bookmark to move back to 3rd record.  
    m_oRecordset.Bookmark = vBookmark  
    MsgBox vbCr & "Moved back to position " & _  
            m_oRecordset.AbsolutePosition & " using bookmark.", , _  
            "Hello Data"  
  
    ' Display meta-data about each field. See WalkFields() sub.  
    Call WalkFields  
  
    ' Apply a filter on the type field.  
    MsgBox "Filtering on type field. (CategoryID=2)", _  
            vbOKOnly, "Hello Data"  
  
    m_oRecordset.Filter = "CategoryID=2"  
  
    ' Set the proper states for the buttons.  
    cmdExamineData.Enabled = False  
    cmdEditData.Enabled = True  
  
    Exit Sub  
  
ExamineDataErr:  
    HandleErrs "ExamineData", m_oRecordset.ActiveConnection  
End Sub  
  
Private Sub EditData()  
    On Error GoTo EditDataErr  
  
    'Recordset still filtered on CategoryID=2.  
    'Increase price by 10% for filtered records.  
    MsgBox "Increasing unit price by 10%" & vbCr & _  
        "for all records with CategoryID = 2.", , "Hello Data"  
  
    m_oRecordset.MoveFirst  
  
    Dim cVal As Currency  
    Do While Not m_oRecordset.EOF  
        cVal = m_oRecordset.Fields("UnitPrice").Value  
        m_oRecordset.Fields("UnitPrice").Value = (cVal * 1.1)  
        m_oRecordset.MoveNext  
    Loop  
  
    ' Set the proper states for the buttons.  
    cmdEditData.Enabled = False  
    cmdUpdateData.Enabled = True  
  
    Exit Sub  
  
EditDataErr:  
    HandleErrs "EditData", m_oRecordset.ActiveConnection  
End Sub  
  
Private Sub UpdateData()  
    On Error GoTo UpdateDataErr  
  
    Dim oConnection2 As New ADODB.Connection  
  
    MsgBox "Removing Filter (adFilterNone).", , "Hello Data"  
    m_oRecordset.Filter = adFilterNone  
  
    Set grdDisplay1.DataSource = Nothing  
    Set grdDisplay1.DataSource = m_oRecordset  
  
    MsgBox "Applying Filter (adFilterPendingRecords).", , "Hello Data"  
    m_oRecordset.Filter = adFilterPendingRecords  
  
    Set grdDisplay1.DataSource = Nothing  
    Set grdDisplay1.DataSource = m_oRecordset  
  
    DisplayMsg "*** PRE-UpdateBatch values for 'UnitPrice' field. ***"  
  
    ' Display Value, UnderlyingValue, and OriginalValue for  
    ' type field in first record.  
    If m_oRecordset.Supports(adMovePrevious) Then  
        m_oRecordset.MoveFirst  
        DisplayMsg "OriginalValue   = " & _  
            m_oRecordset.Fields("UnitPrice").OriginalValue  
        DisplayMsg "Value           = " & _  
            m_oRecordset.Fields("UnitPrice").Value  
    End If  
  
    oConnection2.ConnectionString = m_sConnStr  
    oConnection2.Open  
  
    Set m_oRecordset.ActiveConnection = oConnection2  
    m_oRecordset.UpdateBatch  
  
    m_flgPriceUpdated = True  
  
    DisplayMsg "*** POST-UpdateBatch values for 'UnitPrice' field ***"  
  
    If m_oRecordset.Supports(adMovePrevious) Then  
         m_oRecordset.MoveFirst  
         DisplayMsg "OriginalValue   = " & _  
             m_oRecordset.Fields("UnitPrice").OriginalValue  
         DisplayMsg "Value           = " & _  
             m_oRecordset.Fields("UnitPrice").Value  
    End If  
  
    MsgBox "See value comparisons in txtDisplay.", , _  
           "Hello Data"  
  
    'Clean up  
    oConnection2.Close  
    Set oConnection2 = Nothing  
    Exit Sub  
  
UpdateDataErr:  
    If Err <> 0 Then  
        HandleErrs "UpdateData", oConnection2  
    End If  
  
    If Not oConnection2 Is Nothing Then  
        If oConnection2.State = adStateOpen Then oConnection2.Close  
        Set oConnection2 = Nothing  
    End If  
End Sub  
  
Private Sub WalkFields()  
    On Error GoTo WalkFieldsErr  
  
    Dim iFldCnt As Integer  
    Dim oFields As ADODB.Fields  
    Dim oField As ADODB.Field  
    Dim sMsg As String  
  
    Set oFields = m_oRecordset.Fields  
  
    DisplayMsg "****** BEGIN FIELDS WALK ******"  
  
    For iFldCnt = 0 To (oFields.Count - 1)  
        Set oField = oFields(iFldCnt)  
        sMsg = ""  
        sMsg = sMsg & oField.Name  
        sMsg = sMsg & vbTab & "Type: " & GetTypeAsString(oField.Type)  
        sMsg = sMsg & vbTab & "Defined Size: " & oField.DefinedSize  
        sMsg = sMsg & vbTab & "Actual Size: " & oField.ActualSize  
  
        grdDisplay1.SelStartCol = iFldCnt  
        grdDisplay1.SelEndCol = iFldCnt  
        DisplayMsg sMsg  
        MsgBox sMsg, , "Hello Data"  
    Next iFldCnt  
  
    DisplayMsg "****** END FIELDS WALK ******" & vbCrLf  
  
    'Clean up  
    Set oField = Nothing  
    Set oFields = Nothing  
    Exit Sub  
  
WalkFieldsErr:  
    Set oField = Nothing  
    Set oFields = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
End Sub  
  
Private Function GetTypeAsString(dtType As ADODB.DataTypeEnum) As String  
    ' To save space, we are only checking for data types  
    ' that we know are present.  
    Select Case dtType  
        Case adChar  
            GetTypeAsString = "adChar"  
        Case adVarChar  
            GetTypeAsString = "adVarChar"  
        Case adVarWChar  
            GetTypeAsString = "adVarWChar"  
        Case adCurrency  
            GetTypeAsString = "adCurrency"  
        Case adInteger  
            GetTypeAsString = "adInteger"  
    End Select  
End Function  
  
Private Sub HandleErrs(sSource As String, ByRef m_oConnection As ADODB.Connection)  
    DisplayMsg "ADO (OLE) ERROR IN " & sSource  
    DisplayMsg vbTab & "Error: " & Err.Number  
    DisplayMsg vbTab & "Description: " & Err.Description  
    DisplayMsg vbTab & "Source: " & Err.Source  
  
    If Not m_oConnection Is Nothing Then  
        If m_oConnection.Errors.Count <> 0 Then  
            DisplayMsg "PROVIDER ERROR"  
            Dim oError1 As ADODB.Error  
            For Each oError1 In m_oConnection.Errors  
                DisplayMsg vbTab & "Error: " & oError1.Number  
                DisplayMsg vbTab & "Description: " & oError1.Description  
                DisplayMsg vbTab & "Source: " & oError1.Source  
                DisplayMsg vbTab & "Native Error:" & oError1.NativeError  
                DisplayMsg vbTab & "SQL State: " & oError1.SQLState  
            Next oError1  
            m_oConnection.Errors.Clear  
            Set oError1 = Nothing  
        End If  
    End If  
  
    MsgBox "Error(s) occurred. See txtDisplay1 for specific information.", , _  
           "Hello Data"  
  
    Err.Clear  
End Sub  
  
Private Sub DisplayMsg(sText As String)  
    txtDisplay1.Text = (txtDisplay1.Text & vbCrLf & sText)  
End Sub  
  
Private Sub Form_Resize()  
    grdDisplay1.Move 100, 700, Me.ScaleWidth - 200, (Me.ScaleHeight - 800) / 2  
    txtDisplay1.Move 100, grdDisplay1.Top + grdDisplay1.Height + 100, _  
                    Me.ScaleWidth - 200, (Me.ScaleHeight - 1000) / 2  
End Sub  
  
Private Sub Form_Load()  
    cmdGetData.Enabled = True  
    cmdExamineData.Enabled = False  
    cmdEditData.Enabled = False  
    cmdUpdateData.Enabled = False  
  
    grdDisplay1.AllowAddNew = False  
    grdDisplay1.AllowDelete = False  
    grdDisplay1.AllowUpdate = False  
    m_flgPriceUpdated = False  
End Sub  
  
Private Sub Form_Unload(Cancel As Integer)  
    On Error GoTo ErrHandler:  
  
    Dim oConnection3 As New ADODB.Connection  
    Dim sSQL As String  
    Dim lAffected As Long  
  
    ' Undo the changes we've made to the database on the server.  
    If m_flgPriceUpdated Then  
        sSQL = "UPDATE Products SET UnitPrice=(UnitPrice/1.1) " & _  
            "WHERE CategoryID=2"  
        oConnection3.Open m_sConnStr  
        oConnection3.Execute sSQL, lAffected, adCmdText  
  
        MsgBox "Restored prices for " & CStr(lAffected) & _  
            " records affected.", , "Hello Data"  
    End If  
  
    'Clean up  
    oConnection3.Close  
    Set oConnection3 = Nothing  
    m_oRecordset.Close  
    Set m_oRecordset = Nothing  
    Exit Sub  
  
ErrHandler:  
  
    If Not oConnection3 Is Nothing Then  
        If oConnection3.State = adStateOpen Then oConnection3.Close  
        Set oConnection3 = Nothing  
    End If  
    If Not m_oRecordset Is Nothing Then  
        If m_oRecordset.State = adStateOpen Then m_oRecordset.Close  
        Set m_oRecordset = Nothing  
    End If  
End Sub  
  
'EndHelloData  
```

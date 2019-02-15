---
title: "Calling a Stored Procedure with a Command | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "calling stored procedures [ADO]"
  - "stored procedures [ADO]"
  - "commands [ADO]"
ms.assetid: 685f7652-2271-4ede-b552-2eeb8c756b4c
author: MightyPen
ms.author: genemi
manager: craigg
---
# Calling a Stored Procedure with a Command
You can use a command to call a stored procedure. The code sample at the end of this topic refers to a stored procedure in the Northwind sample database, called CustOrdersOrders, which is defined as follows.  
  
```  
CREATE PROCEDURE CustOrdersOrders @CustomerID nchar(5) AS  
SELECT OrderID, OrderDate, RequiredDate, ShippedDate  
FROM Orders  
WHERE CustomerID = @CustomerID  
ORDER BY OrderID  
```  
  
 See your SQL Server documentation for more information about how to define and call stored procedures.  
  
 This stored procedure is similar to the command used in [Command Object Parameters](../../../ado/guide/data/command-object-parameters.md). It takes a customer ID parameter and returns information about that customer's orders. The following code sample uses this stored procedure as the source for an ADO **Recordset**.  
  
 Using the stored procedure allows you to access another capability of ADO: the **Parameters** collection **Refresh** method. By using this method, ADO can automatically fill in all information about the parameters required by the command at run time. There is a performance penalty in using this technique, because ADO must query the data source for the information about the parameters.  
  
 Other important differences exist between the following code sample and the code in [Command Object Parameters](../../../ado/guide/data/command-object-parameters.md), where the parameters were entered manually. First, this code does not set the **Prepared** property to **True** because it is a SQL Server stored procedure and is precompiled by definition. Second, the **CommandType** property of the **Command** object changed to **adCmdStoredProc** in the second example to inform ADO that the command was a stored procedure.  
  
 Finally, in the second example the parameter must be referred to by index when setting the value, because you might not know the name of the parameter at design time. If you do know the name of the parameter, you can set the new [NamedParameters](../../../ado/reference/ado-api/namedparameters-property-ado.md) property of the **Command** object to True and refer to the property's name. You might wonder why the position of the first parameter mentioned in the stored procedure (@CustomerID) is 1 instead of 0 (`objCmd(1) = "ALFKI"`). This is because parameter 0 contains a return value from the SQL Server stored procedure.  
  
```  
'BeginAutoParamCmd  
    On Error GoTo ErrHandler:  
  
    Dim objConn As New ADODB.Connection  
    Dim objCmd As New ADODB.Command  
    Dim objParm1 As New ADODB.Parameter  
    Dim objRs As New ADODB.Recordset  
  
    ' Set CommandText equal to the stored procedure name.  
    objCmd.CommandText = "CustOrdersOrders"  
    objCmd.CommandType = adCmdStoredProc  
  
    ' Connect to the data source.  
    Set objConn = GetNewConnection  
    objCmd.ActiveConnection = objConn  
  
    ' Automatically fill in parameter info from stored procedure.  
    objCmd.Parameters.Refresh  
  
    ' Set the param value.  
    objCmd(1) = "ALFKI"  
  
    ' Execute once and display...  
    Set objRs = objCmd.Execute  
  
    Debug.Print objParm1.Value  
    Do While Not objRs.EOF  
        Debug.Print vbTab & objRs(0) & vbTab & objRs(1) & vbTab & _  
                    objRs(2) & vbTab & objRs(3)  
        objRs.MoveNext  
    Loop  
  
    ' ...then set new param value, re-execute command, and display.  
    objCmd(1) = "CACTU"  
    Set objRs = objCmd.Execute  
  
    Debug.Print objParm1.Value  
    Do While Not objRs.EOF  
        Debug.Print vbTab & objRs(0) & vbTab & objRs(1) & vbTab & _  
                    objRs(2) & vbTab & objRs(3)  
        objRs.MoveNext  
    Loop  
  
    'clean up  
    objRs.Close  
    objConn.Close  
    Set objRs = Nothing  
    Set objConn = Nothing  
    Set objCmd = Nothing  
    Set objParm1 = Nothing  
    Exit Sub  
  
ErrHandler:  
    'clean up  
    If objRs.State = adStateOpen Then  
        objRs.Close  
    End If  
  
    If objConn.State = adStateOpen Then  
        objConn.Close  
    End If  
  
    Set objRs = Nothing  
    Set objConn = Nothing  
    Set objCmd = Nothing  
    Set objParm1 = Nothing  
  
    If Err <> 0 Then  
        MsgBox Err.Source & "-->" & Err.Description, , "Error"  
    End If  
'EndAutoParamCmd  
  
'BeginNewConnection  
Private Function GetNewConnection() As ADODB.Connection  
    Dim oCn As New ADODB.Connection  
    Dim sCnStr As String  
  
    sCnStr = "Provider='SQLOLEDB';Data Source='MySqlServer';" & _  
             "Integrated Security='SSPI';Initial Catalog='Northwind';"  
    oCn.Open sCnStr  
  
    If oCn.State = adStateOpen Then  
        Set GetNewConnection = oCn  
    End If  
  
End Function  
'EndNewConnection  
```  
  
## See Also  
 [Knowledge Base article 117500](https://go.microsoft.com/fwlink/?LinkId=117500)

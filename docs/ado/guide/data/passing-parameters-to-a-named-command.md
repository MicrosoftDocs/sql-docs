---
title: "Passing Parameters to a Named Command"
description: "Passing Parameters to a Named Command"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "named commands [ADO]"
  - "commands [ADO], passing parameters to a named command"
---
# Passing Parameters to a Named Command
Just as the result of the command is passed out as an *out* variable of the named command, parameters for a parameterized command can been passed in as *in* variables to the named command.  
  
 The following code example tries to retrieve all the orders placed by the customer whose **CustomerID** is "ALKFI" from the Northwind database. The value of **CustomerID** is supplied at the time when the named command is called.  
  
```  
Const DS = "MySqlServer"  
Const DB = "Northwind"  
Const DP = "SQLOLEDB"  
  
Dim objConn As New ADODB.Connection  
Dim objRs As New ADODB.Recordset  
Dim objComm As New ADODB.Command  
  
CommandText = "SELECT OrderID, OrderDate, " & _  
                     "RequiredDate, ShippedDate " & _  
                     "FROM Orders " & _  
                     "WHERE CustomerID = ? " & _  
                     "ORDER BY OrderID"  
  
ConnectionString = "Provider=" & DP & _  
                   ";Data Source=" & DS & _  
                   ";Initial Catalog=" & DB & _  
                   ";Integrated Security=SSPI;"  
  
' Connect to the data source.  
objConn.Open ConnectionString  
  
' Set a named command.  
objComm.CommandText = CommandText  
objComm.CommandType = adCmdText  
objComm.Name = "GetOrdersOf"  
Set objComm.ActiveConnection = objConn  
  
' Call the named command, passing a CustomerID value  
' as the input parameter.   
'    "ALFKI" is the required input parameter,  
'    objRs is the resultant output variable.  
objConn.GetOrdersOf "ALKFI", objRs  
  
' Display the result.  
Debug.Print "All orders by ALFKI:"  
Do While Not objRs.EOF  
    Debug.Print vbTab & objRs(0) & vbTab & objRs(1) & vbTab & _  
                objRs(2) & vbTab & objRs(3)  
    objRs.MoveNext  
Loop  
  
' Clean up.  
objRs.Close  
objConn.Close  
Set objRs = Nothing  
Set objConn = Nothing  
Set objComm = Nothing  
```  
  
 Notice that all the input parameters must precede any output variable and the data types of parameters must match or can be converted to those of the corresponding fields. The following statement-  
  
```  
objConn.GetOrdersOf 12345, objRs  
```  
  
 -will result in an error of mismatched data types, because the required input parameter is of a **String** type, not of an **Integer** type.  
  
 The following call-  
  
```  
objConn.GetOrdersOf "12345", objRs  
```  
  
 -is valid, but will yield an empty result set because no such records exist in the database.  
  
## See Also  
 [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)

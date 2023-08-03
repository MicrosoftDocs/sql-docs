---
title: "Calling a Stored Procedure as a Method on a Connection object"
description: "Calling a Stored Procedure as a Method on a Connection object"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "calling stored procedures [ADO]"
  - "stored procedures [ADO]"
  - "commands [ADO]"
---
# Calling a Stored Procedure as a Method on a Connection object
You can call a stored procedure as if it were a native method on the associated open **Connection** object. This is similar to calling a named command on the **Connection** object.  
  
 The following Visual Basic code example calls a stored procedure in the Northwind sample database, called CustOrdersOrders, which is listed here again for your convenience.  
  
```  
CREATE PROCEDURE CustOrdersOrders @CustomerID nchar(5) AS  
SELECT OrderID, OrderDate, RequiredDate, ShippedDate  
FROM Orders  
WHERE CustomerID = @CustomerID  
ORDER BY OrderID  
```  
  
 The following code example demonstrates how to call a stored procedure as if it were a native method on an associated open **Connection** object.  
  
```  
Const DS = "MySQLServer"  
Const DB = "Northwind"  
Const DP = "SQLOLEDB"  
  
Dim objConn As New ADODB.Connection  
Dim objRs As New ADODB.Recordset  
Dim objComm As New ADODB.Command  
  
ConnectionString = "Provider=" & DP & _  
                   ";Data Source=" & DS & _  
                   ";Initial Catalog=" & DB & _  
                   ";Integrated Security=SSPI;"  
  
' Connect to the data source.  
objConn.Open ConnectionString  
  
' Set a stored procedure  
  
Set objComm.ActiveConnection = objConn  
  
' Execute the stored procedure on  
' the active connection object...  
'    "ALFKI" is the required input parameter,  
'    objRs is the resultant output variable.  
objComm(1) = "ALFKI"
Set objRs = objComm.Execute

' Display the result.  
Debug.Print "Results returned from sp_CustOrdersOrders for ALFKI: "  
Do While Not objRs.EOF  
    Debug.Print vbTab & objRs(0) & vbTab & objRs(1) & vbTab & _  
                objRs(2) & vbTab & objRs(3)  
    objRs.MoveNext  
 Loop  
  
'Clean up.  
objRs.Close  
objConn.Close  
Set objRs = Nothing  
Set objConn = Nothing  
Set objComm = Nothing  
```  
  
## See Also  
 [Connection Object (ADO)](../../reference/ado-api/connection-object-ado.md)

---
title: "NextRecordset Method (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "03/20/2018"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "NextRecordset"
  - "Recordset15::NextRecordset"
  - "Recordset15::raw_NextRecordset"
helpviewer_keywords: 
  - "NextRecordset method [ADO]"
ms.assetid: ab1fa449-a695-4987-b1ee-bc68f89418dd
author: MightyPen
ms.author: genemi
manager: craigg
---
# NextRecordset Method (ADO)
Clears the current [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object and returns the next **Recordset** by advancing through a series of commands.  
  
## Syntax  
  
```  
  
Set recordset2 = recordset1.NextRecordset(RecordsAffected )  
```  
  
## Return Value  
 Returns a **Recordset** object. In the syntax model, *recordset1* and *recordset2* can be the same **Recordset** object, or you can use separate objects. When using separate **Recordset** objects, resetting the **ActiveConnection** property on the original **Recordset** (*recordset1*) after **NextRecordset** has been called will generate an error.  
  
#### Parameters  
 *RecordsAffected*  
 Optional. A **Long** variable to which the provider returns the number of records that the current operation affected.  
  
> [!NOTE]
>  This parameter only returns the number of records affected by an operation; it does not return a count of records from a select statement used to generate the **Recordset**.  
  
## Remarks  
 Use the **NextRecordset** method to return the results of the next command in a compound command statement or of a stored procedure that returns multiple results. If you open a **Recordset** object based on a compound command statement (for example, "SELECT \* FROM table1;SELECT \* FROM table2") using the [Execute](../../../ado/reference/ado-api/execute-method-ado-command.md) method on a [Command](../../../ado/reference/ado-api/command-object-ado.md) or the [Open](../../../ado/reference/ado-api/open-method-ado-recordset.md) method on a **Recordset**, ADO executes only the first command and returns the results to *recordset*. To access the results of subsequent commands in the statement, call the **NextRecordset** method.  
  
 As long as there are additional results and the **Recordset** containing the compound statements is not disconnected or marshaled across process boundaries, the **NextRecordset** method will continue to return **Recordset** objects. If a row-returning command executes successfully but returns no records, the returned **Recordset** object will be open but empty. Test for this case by verifying that the [BOF](../../../ado/reference/ado-api/bof-eof-properties-ado.md) and [EOF](../../../ado/reference/ado-api/bof-eof-properties-ado.md) properties are both **True**. If a non-row-returning command executes successfully, the returned **Recordset** object will be closed, which you can verify by testing the [State](../../../ado/reference/ado-api/state-property-ado.md) property on the **Recordset**. When there are no more results, *recordset* will be set to *Nothing*.  
  
 The **NextRecordset** method is not available on a disconnected **Recordset** object, where [ActiveConnection](../../../ado/reference/ado-api/activeconnection-property-ado.md) has been set to **Nothing** (in Microsoft Visual Basic) or NULL (in other languages).  
  
 If an edit is in progress while in immediate update mode, calling the **NextRecordset** method generates an error; call the [Update](../../../ado/reference/ado-api/update-method.md) or [CancelUpdate](../../../ado/reference/ado-api/cancelupdate-method-ado.md) method first.  
  
 To pass parameters for more than one command in the compound statement by filling the [Parameters](../../../ado/reference/ado-api/parameters-collection-ado.md) collection, or by passing an array with the original **Open** or **Execute** call, the parameters must be in the same order in the collection or array as their respective commands in the command series. You must finish reading all the results before reading output parameter values.  
  
 Your OLE DB provider determines when each command in a compound statement is executed. The [Microsoft OLE DB Provider for SQL Server](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-sql-server.md), for example, executes all commands in a batch upon receiving the compound statement. The resulting **Recordsets** are simply returned when you call **NextRecordset**.  
  
 However, other providers may execute the next command in a statement only after NextRecordset is called. For these providers, if you explicitly close the **Recordset** object before stepping through the entire command statement, ADO never executes the remaining commands.  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [NextRecordset Method Example (VB)](../../../ado/reference/ado-api/nextrecordset-method-example-vb.md)   
 [NextRecordset Method Example (VC++)](../../../ado/reference/ado-api/nextrecordset-method-example-vc.md)   

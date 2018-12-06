---
title: "Cancel Method (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset20::Cancel"
  - "_Record::Cancel"
  - "_Connection::Cancel"
  - "Command25::Cancel"
  - "_Stream::Cancel"
helpviewer_keywords: 
  - "Cancel method [ADO]"
ms.assetid: e0db4e15-6787-41e2-8f13-9e9b524d620a
author: MightyPen
ms.author: genemi
manager: craigg
---
# Cancel Method (ADO)
Cancels execution of a pending asynchronous method call.  
  
## Syntax  
  
```  
  
object.Cancel  
```  
  
## Remarks  
 Use the **Cancel** method to terminate execution of an asynchronous method call: that is, a method invoked with the **adAsyncConnect**, **adAsyncExecute**, or **adAsyncFetch** option.  
  
 The following table shows what task is terminated when you use the **Cancel** method on a particular type of object.  
  
|If *object* is a|The last asynchronous call to this method is terminated|  
|----------------------|-------------------------------------------------------------|  
|[Command](../../../ado/reference/ado-api/command-object-ado.md)|[Execute](../../../ado/reference/ado-api/execute-method-ado-command.md)|  
|[Connection](../../../ado/reference/ado-api/connection-object-ado.md)|[Execute](../../../ado/reference/ado-api/execute-method-ado-connection.md) or [Open](../../../ado/reference/ado-api/open-method-ado-connection.md)|  
|[Record](../../../ado/reference/ado-api/record-object-ado.md)|[CopyRecord](../../../ado/reference/ado-api/copyrecord-method-ado.md), [DeleteRecord](../../../ado/reference/ado-api/deleterecord-method-ado.md), [MoveRecord](../../../ado/reference/ado-api/moverecord-method-ado.md), or [Open](../../../ado/reference/ado-api/open-method-ado-record.md)|  
|[Recordset](../../../ado/reference/ado-api/recordset-object-ado.md)|[Open](../../../ado/reference/ado-api/open-method-ado-recordset.md)|  
|[Stream](../../../ado/reference/ado-api/stream-object-ado.md)|[Open](../../../ado/reference/ado-api/open-method-ado-stream.md)|  
  
## Applies To  
  
||||  
|-|-|-|  
|[Command Object (ADO)](../../../ado/reference/ado-api/command-object-ado.md)|[Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)|[Record Object (ADO)](../../../ado/reference/ado-api/record-object-ado.md)|  
|[Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)|[Stream Object (ADO)](../../../ado/reference/ado-api/stream-object-ado.md)||  
  
## See Also  
 [Cancel Method Example (VB)](../../../ado/reference/ado-api/cancel-method-example-vb.md)   
 [Cancel Method Example (VBScript)](../../../ado/reference/rds-api/cancel-method-example-vbscript.md)   
 [Cancel Method Example (VC++)](../../../ado/reference/ado-api/cancel-method-example-vc.md)   
 [Cancel Method (RDS)](../../../ado/reference/rds-api/cancel-method-rds.md)   
 [CancelBatch Method (ADO)](../../../ado/reference/ado-api/cancelbatch-method-ado.md)   
 [CancelUpdate Method (ADO)](../../../ado/reference/ado-api/cancelupdate-method-ado.md)   
 [CancelUpdate Method (RDS)](../../../ado/reference/rds-api/cancelupdate-method-rds.md)   
 [Execute Method (ADO Command)](../../../ado/reference/ado-api/execute-method-ado-command.md)   
 [Execute Method (ADO Connection)](../../../ado/reference/ado-api/execute-method-ado-connection.md)   
 [Open Method (ADO Connection)](../../../ado/reference/ado-api/open-method-ado-connection.md)   
 [Open Method (ADO Recordset)](../../../ado/reference/ado-api/open-method-ado-recordset.md)

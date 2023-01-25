---
title: "Cancel Method (ADO)"
description: "Cancel Method (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Recordset20::Cancel"
  - "_Record::Cancel"
  - "_Connection::Cancel"
  - "Command25::Cancel"
  - "_Stream::Cancel"
helpviewer_keywords:
  - "Cancel method [ADO]"
apitype: "COM"
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
|[Command](./command-object-ado.md)|[Execute](./execute-method-ado-command.md)|  
|[Connection](./connection-object-ado.md)|[Execute](./execute-method-ado-connection.md) or [Open](./open-method-ado-connection.md)|  
|[Record](./record-object-ado.md)|[CopyRecord](./copyrecord-method-ado.md), [DeleteRecord](./deleterecord-method-ado.md), [MoveRecord](./moverecord-method-ado.md), or [Open](./open-method-ado-record.md)|  
|[Recordset](./recordset-object-ado.md)|[Open](./open-method-ado-recordset.md)|  
|[Stream](./stream-object-ado.md)|[Open](./open-method-ado-stream.md)|  
  
## Applies To  

:::row:::
    :::column:::
        [Command Object (ADO)](./command-object-ado.md)  
        [Connection Object (ADO)](./connection-object-ado.md)  
    :::column-end:::
    :::column:::
        [Record Object (ADO)](./record-object-ado.md)  
        [Recordset Object (ADO)](./recordset-object-ado.md)  
    :::column-end:::
    :::column:::
        [Stream Object (ADO)](./stream-object-ado.md)  
    :::column-end:::
:::row-end:::

## See Also  
 [Cancel Method Example (VB)](./cancel-method-example-vb.md)   
 [Cancel Method Example (VBScript)](../rds-api/cancel-method-example-vbscript.md)   
 [Cancel Method Example (VC++)](./cancel-method-example-vc.md)   
 [Cancel Method (RDS)](../rds-api/cancel-method-rds.md)   
 [CancelBatch Method (ADO)](./cancelbatch-method-ado.md)   
 [CancelUpdate Method (ADO)](./cancelupdate-method-ado.md)   
 [CancelUpdate Method (RDS)](../rds-api/cancelupdate-method-rds.md)   
 [Execute Method (ADO Command)](./execute-method-ado-command.md)   
 [Execute Method (ADO Connection)](./execute-method-ado-connection.md)   
 [Open Method (ADO Connection)](./open-method-ado-connection.md)   
 [Open Method (ADO Recordset)](./open-method-ado-recordset.md)
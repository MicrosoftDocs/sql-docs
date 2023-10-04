---
title: "Clear Method (ADO)"
description: "Clear Method (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Errors::raw_Clear"
  - "Errors::Clear"
helpviewer_keywords:
  - "Clear method [ADO]"
apitype: "COM"
---
# Clear Method (ADO)
Removes all the [Error](./error-object.md) objects from the [Errors](./errors-collection-ado.md) collection.  
  
## Syntax  
  
```  
  
Errors.Clear  
```  
  
## Remarks  
 Use the **Clear** method on the [Errors](./errors-collection-ado.md) collection to remove all existing [Error](./error-object.md) objects from the collection. When an error occurs, ADO automatically clears the **Errors** collection and fills it with **Error** objects based on the new error.  
  
 Some properties and methods return warnings that appear as **Error** objects in the **Errors** collection but do not halt a program's execution. Before you call the [Resync](./resync-method.md), [UpdateBatch](./updatebatch-method.md), or [CancelBatch](./cancelbatch-method-ado.md) methods on a [Recordset](./recordset-object-ado.md) object; the [Open](./open-method-ado-connection.md) method on a [Connection](./connection-object-ado.md) object; or set the [Filter](./filter-property.md) property on a **Recordset** object, call the **Clear** method on the **Errors** collection. That way, you can read the [Count](./count-property-ado.md) property of the **Errors** collection to test for returned warnings.  
  
## Applies To  
 [Errors Collection (ADO)](./errors-collection-ado.md)  
  
## See Also  
 [Execute, Requery, and Clear Methods Example (VB)](./execute-requery-and-clear-methods-example-vb.md)   
 [Execute, Requery, and Clear Methods Example (VBScript)](./execute-requery-and-clear-methods-example-vbscript.md)   
 [Execute, Requery, and Clear Methods Example (VC++)](./execute-requery-and-clear-methods-example-vc.md)   
 [CancelBatch Method (ADO)](./cancelbatch-method-ado.md)   
 [Delete Method (ADO Fields Collection)](./delete-method-ado-fields-collection.md)   
 [Delete Method (ADO Parameters Collection)](./delete-method-ado-parameters-collection.md)   
 [Delete Method (ADO Recordset)](./delete-method-ado-recordset.md)   
 [Filter Property](./filter-property.md)   
 [Resync Method](./resync-method.md)   
 [UpdateBatch Method](./updatebatch-method.md)
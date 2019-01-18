---
title: "Clear Method (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Errors::raw_Clear"
  - "Errors::Clear"
helpviewer_keywords: 
  - "Clear method [ADO]"
ms.assetid: 0a61ba7a-20b8-426a-91a0-9040e7c5a98a
author: MightyPen
ms.author: genemi
manager: craigg
---
# Clear Method (ADO)
Removes all the [Error](../../../ado/reference/ado-api/error-object.md) objects from the [Errors](../../../ado/reference/ado-api/errors-collection-ado.md) collection.  
  
## Syntax  
  
```  
  
Errors.Clear  
```  
  
## Remarks  
 Use the **Clear** method on the [Errors](../../../ado/reference/ado-api/errors-collection-ado.md) collection to remove all existing [Error](../../../ado/reference/ado-api/error-object.md) objects from the collection. When an error occurs, ADO automatically clears the **Errors** collection and fills it with **Error** objects based on the new error.  
  
 Some properties and methods return warnings that appear as **Error** objects in the **Errors** collection but do not halt a program's execution. Before you call the [Resync](../../../ado/reference/ado-api/resync-method.md), [UpdateBatch](../../../ado/reference/ado-api/updatebatch-method.md), or [CancelBatch](../../../ado/reference/ado-api/cancelbatch-method-ado.md) methods on a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object; the [Open](../../../ado/reference/ado-api/open-method-ado-connection.md) method on a [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object; or set the [Filter](../../../ado/reference/ado-api/filter-property.md) property on a **Recordset** object, call the **Clear** method on the **Errors** collection. That way, you can read the [Count](../../../ado/reference/ado-api/count-property-ado.md) property of the **Errors** collection to test for returned warnings.  
  
## Applies To  
 [Errors Collection (ADO)](../../../ado/reference/ado-api/errors-collection-ado.md)  
  
## See Also  
 [Execute, Requery, and Clear Methods Example (VB)](../../../ado/reference/ado-api/execute-requery-and-clear-methods-example-vb.md)   
 [Execute, Requery, and Clear Methods Example (VBScript)](../../../ado/reference/ado-api/execute-requery-and-clear-methods-example-vbscript.md)   
 [Execute, Requery, and Clear Methods Example (VC++)](../../../ado/reference/ado-api/execute-requery-and-clear-methods-example-vc.md)   
 [CancelBatch Method (ADO)](../../../ado/reference/ado-api/cancelbatch-method-ado.md)   
 [Delete Method (ADO Fields Collection)](../../../ado/reference/ado-api/delete-method-ado-fields-collection.md)   
 [Delete Method (ADO Parameters Collection)](../../../ado/reference/ado-api/delete-method-ado-parameters-collection.md)   
 [Delete Method (ADO Recordset)](../../../ado/reference/ado-api/delete-method-ado-recordset.md)   
 [Filter Property](../../../ado/reference/ado-api/filter-property.md)   
 [Resync Method](../../../ado/reference/ado-api/resync-method.md)   
 [UpdateBatch Method](../../../ado/reference/ado-api/updatebatch-method.md)

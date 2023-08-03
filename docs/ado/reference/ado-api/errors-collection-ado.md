---
title: "Errors Collection (ADO)"
description: "Errors Collection (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Connection15::Errors"
  - "Connection15::get_Errors"
  - "Connection15::GetErrors"
helpviewer_keywords:
  - "Errors collection [ADO]"
apitype: "COM"
---
# Errors Collection (ADO)
Contains all the [Error](../../../ado/reference/ado-api/error-object.md) objects created in response to a single provider-related failure.  
  
## Remarks  
 Any operation involving ADO objects can generate one or more provider errors. As each error occurs, one or more **Error** objects can be placed in the **Errors** collection of the [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object. When another ADO operation generates an error, the **Errors** collection is cleared, and the new set of **Error** objects can be placed in the **Errors** collection.  
  
 Each **Error** object represents a specific provider error, not an ADO error. ADO errors are exposed to the run-time exception-handling mechanism. For example, in Microsoft Visual Basic, the occurrence of an ADO-specific error will trigger an [onError](../../../ado/reference/rds-api/onerror-event-rds.md) event and appear in the **Err** object.  
  
 ADO operations that don't generate an error have no effect on the **Errors** collection. Use the [Clear](../../../ado/reference/ado-api/clear-method-ado.md) method to manually clear the **Errors** collection.  
  
 The set of **Error** objects in the **Errors** collection describes all errors that occurred in response to a single statement. Enumerating the specific errors in the **Errors** collection enables your error-handling routines to more precisely determine the cause and origin of an error, and take appropriate steps to recover.  
  
 Some properties and methods return warnings that appear as **Error** objects in the **Errors** collection but do not halt a program's execution. Before you call the [Resync](../../../ado/reference/ado-api/resync-method.md), [UpdateBatch](../../../ado/reference/ado-api/updatebatch-method.md), or [CancelBatch](../../../ado/reference/ado-api/cancelbatch-method-ado.md) methods on a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object, the [Open](../../../ado/reference/ado-api/open-method-ado-connection.md) method on a **Connection** object, or set the [Filter](../../../ado/reference/ado-api/filter-property.md) property on a **Recordset** object, call the **Clear** method on the **Errors** collection. That way you can read the [Count](../../../ado/reference/ado-api/count-property-ado.md) property of the **Errors** collection to test for returned warnings.  
  
> [!NOTE]
>  See the **Error** object topic for a more detailed explanation of the way a single ADO operation can generate multiple errors.  
  
 This section contains the following topic.  
  
-   [Errors Collection Properties, Methods, and Events](../../../ado/reference/ado-api/errors-collection-properties-methods-and-events.md)  
  
## See Also  
 [Error Object](../../../ado/reference/ado-api/error-object.md)   
 [Appendix A: Providers](../../../ado/guide/appendixes/appendix-a-providers.md)

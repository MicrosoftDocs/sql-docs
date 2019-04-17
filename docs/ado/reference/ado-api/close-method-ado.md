---
title: "Close Method (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset15::Close"
  - "_Stream::Close"
  - "_Record::Close"
helpviewer_keywords: 
  - "Close method [ADO]"
ms.assetid: 3cdf27d1-a180-4cff-8e42-95dec5fb1b55
author: MightyPen
ms.author: genemi
manager: craigg
---
# Close Method (ADO)
Closes an open object and any dependent objects.  
  
## Syntax  
  
```  
  
object.Close  
```  
  
## Remarks  
 Use the **Close** method to close a [Connection](../../../ado/reference/ado-api/connection-object-ado.md), a [Record](../../../ado/reference/ado-api/record-object-ado.md), a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md), or a [Stream](../../../ado/reference/ado-api/stream-object-ado.md) object to free any associated system resources. Closing an object does not remove it from memory; you can change its property settings and open it again later. To completely eliminate an object from memory, close the object and then set the object variable to *Nothing* (in Visual Basic).  
  
## Connection  
 Using the **Close** method to close a **Connection** object also closes any active **Recordset** objects associated with the connection. A [Command](../../../ado/reference/ado-api/command-object-ado.md) object associated with the **Connection** object you are closing will persist, but it will no longer be associated with a **Connection** object; that is, its [ActiveConnection](../../../ado/reference/ado-api/activeconnection-property-ado.md) property will be set to **Nothing**. Also, the **Command** object's [Parameters](../../../ado/reference/ado-api/parameters-collection-ado.md) collection will be cleared of any provider-defined parameters.  
  
 You can later call the [Open](../../../ado/reference/ado-api/open-method-ado-connection.md) method to re-establish the connection to the same, or another, data source. While the **Connection** object is closed, calling any methods that require an open connection to the data source generates an error.  
  
 Closing a **Connection** object while there are open **Recordset** objects on the connection rolls back any pending changes in all of the **Recordset** objects. Explicitly closing a **Connection** object (calling the **Close** method) while a transaction is in progress generates an error. If a **Connection** object falls out of scope while a transaction is in progress, ADO automatically rolls back the transaction.  
  
## Recordset, Record, Stream  
 Using the **Close** method to close a **Recordset**, **Record**, or **Stream** object releases the associated data and any exclusive access you may have had to the data through this particular object. You can later call the [Open](../../../ado/reference/ado-api/open-method-ado-recordset.md) method to reopen the object with the same, or modified, attributes.  
  
 While a **Recordset** object is closed, calling any methods that require a live cursor generates an error.  
  
 If an edit is in progress while in immediate update mode, calling the **Close** method generates an error; instead, call the [Update](../../../ado/reference/ado-api/update-method.md) or [CancelUpdate](../../../ado/reference/ado-api/cancelupdate-method-ado.md) method first. If you close the **Recordset** object while in batch update mode, all changes since the last [UpdateBatch](../../../ado/reference/ado-api/updatebatch-method.md) call are lost.  
  
 If you use the [Clone](../../../ado/reference/ado-api/clone-method-ado.md) method to create copies of an open **Recordset** object, closing the original or a clone does not affect any of the other copies.  
  
## Applies To  
  
|||  
|-|-|  
|[Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)|[Record Object (ADO)](../../../ado/reference/ado-api/record-object-ado.md)|  
|[Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)|[Stream Object (ADO)](../../../ado/reference/ado-api/stream-object-ado.md)|  
  
## See Also  
 [Open and Close Methods Example (VB)](../../../ado/reference/ado-api/open-and-close-methods-example-vb.md)   
 [Open and Close Methods Example (VBScript)](../../../ado/reference/ado-api/open-and-close-methods-example-vbscript.md)   
 [Open and Close Methods Example (VC++)](../../../ado/reference/ado-api/open-and-close-methods-example-vc.md)   
 [Open Method (ADO Connection)](../../../ado/reference/ado-api/open-method-ado-connection.md)   
 [Open Method (ADO Recordset)](../../../ado/reference/ado-api/open-method-ado-recordset.md)   
 [Save Method](../../../ado/reference/ado-api/save-method.md)

---
title: "Requery Method | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset15::Requery"
  - "Recordset15::raw_Requery"
helpviewer_keywords: 
  - "Requery method [ADO]"
ms.assetid: d81ab76f-1aa8-4ccf-92ec-b65254dc3ea1
author: MightyPen
ms.author: genemi
manager: craigg
---
# Requery Method
Updates the data in a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object by re-executing the query on which the object is based.  
  
## Syntax  
  
```  
  
recordset.Requery Options  
```  
  
#### Parameters  
 *Options*  
 Optional. A bitmask that contains [ExecuteOptionEnum](../../../ado/reference/ado-api/executeoptionenum.md) and [CommandTypeEnum](../../../ado/reference/ado-api/commandtypeenum.md) values affecting this operation.  
  
> [!NOTE]
>  If *Options* is set to **adAsyncExecute**, this operation will execute asynchronously and a [RecordsetChangeComplete](../../../ado/reference/ado-api/willchangerecordset-and-recordsetchangecomplete-events-ado.md) event will be issued when it concludes. The **ExecuteOpenEnum** values of **adExecuteNoRecords** or **adExecuteStream** should not be used with **Requery**.  
  
## Remarks  
 Use the **Requery** method to refresh the entire contents of a **Recordset** object from the data source by reissuing the original command and retrieving the data a second time. Calling this method is equivalent to calling the [Close](../../../ado/reference/ado-api/close-method-ado.md) and [Open](../../../ado/reference/ado-api/open-method-ado-recordset.md) methods in succession. If you are editing the current record or adding a new record, an error occurs.  
  
 While the **Recordset** object is open, the properties that define the nature of the cursor ([CursorType](../../../ado/reference/ado-api/cursortype-property-ado.md), [LockType](../../../ado/reference/ado-api/locktype-property-ado.md), [MaxRecords](../../../ado/reference/ado-api/maxrecords-property-ado.md), and so forth) are read-only. Thus, the **Requery** method can only refresh the current cursor. To change any of the cursor properties and view the results, you must use the [Close](../../../ado/reference/ado-api/close-method-ado.md) method so that the properties become read/write again. You can then change the property settings and call the [Open](../../../ado/reference/ado-api/open-method-ado-recordset.md) method to reopen the cursor.  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [Execute, Requery, and Clear Methods Example (VB)](../../../ado/reference/ado-api/execute-requery-and-clear-methods-example-vb.md)   
 [Execute, Requery, and Clear Methods Example (VBScript)](../../../ado/reference/ado-api/execute-requery-and-clear-methods-example-vbscript.md)   
 [Execute, Requery, and Clear Methods Example (VC++)](../../../ado/reference/ado-api/execute-requery-and-clear-methods-example-vc.md)   
 [CommandText Property (ADO)](../../../ado/reference/ado-api/commandtext-property-ado.md)

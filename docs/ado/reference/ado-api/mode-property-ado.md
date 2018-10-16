---
title: "Mode Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Connection15::Mode"
  - "_Stream::Mode"
  - "_Record::Mode"
helpviewer_keywords: 
  - "Mode property [ADO]"
ms.assetid: 808661eb-0d7c-4e6d-8e40-9dc3bef3d77a
author: MightyPen
ms.author: genemi
manager: craigg
---
# Mode Property (ADO)
Indicates the available permissions for modifying data in a [Connection](../../../ado/reference/ado-api/connection-object-ado.md), [Record](../../../ado/reference/ado-api/record-object-ado.md), or [Stream](../../../ado/reference/ado-api/stream-object-ado.md) object.  
  
## Settings and Return Values  
 Sets or returns a [ConnectModeEnum](../../../ado/reference/ado-api/connectmodeenum.md) value. The default value for a **Connection** is **adModeUnknown**. The default value for a **Record** object is **adModeRead**. The default value for a **Stream** associated with an underlying source (opened with a URL as the source, or as the default **Stream** of a **Record**) is **adModeRead**. The default value for a **Stream** not associated with an underlying source (instantiated in memory) is **adModeUnknown**.  
  
## Remarks  
 Use the **Mode** property to set or return the access permissions in use by the provider on the current connection. You can set the **Mode** property only when the **Connection** object is closed.  
  
 For a **Stream** object, if the access mode is not specified, it is inherited from the source used to open the **Stream** object. For example, if a **Stream** is opened from a **Record** object, by default it is opened in the same mode as the **Record**.  
  
 This property is read/write while the object is closed and read-only while the object is open.  
  
> [!NOTE]
>  **Remote Data Service Usage** When used on a client-side **Connection** object, the **Mode** property can only be set to **adModeUnknown**.  
  
## Applies To  
  
||||  
|-|-|-|  
|[Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)|[Record Object (ADO)](../../../ado/reference/ado-api/record-object-ado.md)|[Stream Object (ADO)](../../../ado/reference/ado-api/stream-object-ado.md)|  
  
## See Also  
 [IsolationLevel and Mode Properties Example (VB)](../../../ado/reference/ado-api/isolationlevel-and-mode-properties-example-vb.md)   
 [IsolationLevel and Mode Properties Example (VC++)](../../../ado/reference/ado-api/isolationlevel-and-mode-properties-example-vc.md)   

---
title: "Mode Property (ADO)"
description: "Mode Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Connection15::Mode"
  - "_Stream::Mode"
  - "_Record::Mode"
helpviewer_keywords:
  - "Mode property [ADO]"
apitype: "COM"
---
# Mode Property (ADO)
Indicates the available permissions for modifying data in a [Connection](./connection-object-ado.md), [Record](./record-object-ado.md), or [Stream](./stream-object-ado.md) object.  
  
## Settings and Return Values  
 Sets or returns a [ConnectModeEnum](./connectmodeenum.md) value. The default value for a **Connection** is **adModeUnknown**. The default value for a **Record** object is **adModeRead**. The default value for a **Stream** associated with an underlying source (opened with a URL as the source, or as the default **Stream** of a **Record**) is **adModeRead**. The default value for a **Stream** not associated with an underlying source (instantiated in memory) is **adModeUnknown**.  
  
## Remarks  
 Use the **Mode** property to set or return the access permissions in use by the provider on the current connection. You can set the **Mode** property only when the **Connection** object is closed.  
  
 For a **Stream** object, if the access mode is not specified, it is inherited from the source used to open the **Stream** object. For example, if a **Stream** is opened from a **Record** object, by default it is opened in the same mode as the **Record**.  
  
 This property is read/write while the object is closed and read-only while the object is open.  
  
> [!NOTE]
>  **Remote Data Service Usage** When used on a client-side **Connection** object, the **Mode** property can only be set to **adModeUnknown**.  
  
## Applies To  

:::row:::
    :::column:::
        [Connection Object (ADO)](./connection-object-ado.md)  
    :::column-end:::
    :::column:::
        [Record Object (ADO)](./record-object-ado.md)  
    :::column-end:::
    :::column:::
        [Stream Object (ADO)](./stream-object-ado.md)  
    :::column-end:::
:::row-end:::

## See Also  
 [IsolationLevel and Mode Properties Example (VB)](./isolationlevel-and-mode-properties-example-vb.md)   
 [IsolationLevel and Mode Properties Example (VC++)](./isolationlevel-and-mode-properties-example-vc.md)
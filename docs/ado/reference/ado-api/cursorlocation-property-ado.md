---
title: "CursorLocation Property (ADO)"
description: "CursorLocation Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Connection15::CursorLocation"
  - "Recordset15::CursorLocation"
helpviewer_keywords:
  - "CursorLocation property [ADO]"
apitype: "COM"
---
# CursorLocation Property (ADO)
Indicates the location of the cursor service.  
  
## Settings And Return Values  
 Sets or returns a **Long** value that can be set to one of the [CursorLocationEnum](./cursorlocationenum.md) values.  
  
## Remarks  
 This property allows you to choose between various cursor libraries accessible to the provider. Usually, you can choose between using a client-side cursor library or one that is located on the server.  
  
 This property setting affects connections established only after the property has been set. Changing the **CursorLocation** property has no effect on existing connections.  
  
 Cursors returned by the [Execute](./execute-method-ado-connection.md) method inherit this setting. **Recordset** objects will automatically inherit this setting from their associated connections.  
  
 This property is read/write on a [Connection](./connection-object-ado.md) or a closed [Recordset](./recordset-object-ado.md), and read-only on an open **Recordset**.  
  
> [!NOTE]
>  **Remote Data Service Usage** When used on a client-side **Recordset** or **Connection** object, the **CursorLocation** property can only be set to **adUseClient**.  
  
## Applies To  

:::row:::
    :::column:::
        [Connection Object (ADO)](./connection-object-ado.md)  
    :::column-end:::
    :::column:::
        [Recordset Object (ADO)](./recordset-object-ado.md)  
    :::column-end:::
:::row-end:::

## See Also  
 [Appendix A: Providers](../../guide/appendixes/appendix-a-providers.md)
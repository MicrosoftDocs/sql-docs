---
title: "CursorLocation Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Connection15::CursorLocation"
  - "Recordset15::CursorLocation"
helpviewer_keywords: 
  - "CursorLocation property [ADO]"
ms.assetid: 39c8d86e-7ee9-4182-be5e-aad5ce952f84
author: MightyPen
ms.author: genemi
manager: craigg
---
# CursorLocation Property (ADO)
Indicates the location of the cursor service.  
  
## Settings And Return Values  
 Sets or returns a **Long** value that can be set to one of the [CursorLocationEnum](../../../ado/reference/ado-api/cursorlocationenum.md) values.  
  
## Remarks  
 This property allows you to choose between various cursor libraries accessible to the provider. Usually, you can choose between using a client-side cursor library or one that is located on the server.  
  
 This property setting affects connections established only after the property has been set. Changing the **CursorLocation** property has no effect on existing connections.  
  
 Cursors returned by the [Execute](../../../ado/reference/ado-api/execute-method-ado-connection.md) method inherit this setting. **Recordset** objects will automatically inherit this setting from their associated connections.  
  
 This property is read/write on a [Connection](../../../ado/reference/ado-api/connection-object-ado.md) or a closed [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md), and read-only on an open **Recordset**.  
  
> [!NOTE]
>  **Remote Data Service Usage** When used on a client-side **Recordset** or **Connection** object, the **CursorLocation** property can only be set to **adUseClient**.  
  
## Applies To  
  
|||  
|-|-|  
|[Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)|[Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)|  
  
## See Also  
 [Appendix A: Providers](../../../ado/guide/appendixes/appendix-a-providers.md)

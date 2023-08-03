---
title: "MarshalOptions Property (ADO)"
description: "MarshalOptions Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Recordset15::MarshalOptions"
helpviewer_keywords:
  - "MarshalOptions property [ADO]"
apitype: "COM"
---
# MarshalOptions Property (ADO)
Indicates which records of the [Recordset](./recordset-object-ado.md) are to be marshaled back to the server.  
  
## Settings And Return Values  
 Sets or returns a [MarshalOptionsEnum](./marshaloptionsenum.md) value. The default value is **adMarshalAll**.  
  
## Remarks  
 When using a client-side [Recordset](./recordset-object-ado.md), records that have been modified on the client are written back to the middle tier or Web server through a technique called marshaling, the process of packaging and sending interface method parameters across thread or process boundaries. Setting the **MarshalOptions** property can improve performance when modified remote data is marshaled for updating back to the middle tier or Web server.  
  
> [!NOTE]
>  **Remote Data Service Usage** This property is used only on a client-side **Recordset**.  
  
## Applies To  
 [Recordset Object (ADO)](./recordset-object-ado.md)  
  
## See Also  
 [MarshalOptions Property Example (VB)](./marshaloptions-property-example-vb.md)   
 [MarshalOptions Property Example (VC++)](./marshaloptions-property-example-vc.md)
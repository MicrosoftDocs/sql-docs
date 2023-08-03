---
title: "StayInSync Property"
description: "StayInSync Property"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Recordset20::StayInSync"
  - "Recordset20::put_StayInSync"
  - "Recordset20::PutStayInSync"
  - "Recordset20::get_StayInSync"
  - "Recordset20::GetStayInSync"
helpviewer_keywords:
  - "StayInSync property"
apitype: "COM"
---
# StayInSync Property
Indicates, in a hierarchical [Recordset](./recordset-object-ado.md) object, whether the reference to the underlying child records (that is, the *chapter*) changes when the parent row position changes.  
  
## Settings and Return Values  
 Sets or returns a **Boolean** value. The default value is **True**. If **True**, the chapter will be updated if the parent **Recordset** object changes row position; if **False**, the chapter will continue to refer to data in the previous chapter even though the parent **Recordset** object has changed row position.  
  
## Remarks  
 This property applies to hierarchical recordsets, such as those supported by the [Microsoft Data Shaping Service for OLE DB](../../guide/appendixes/microsoft-data-shaping-service-for-ole-db-ado-service-provider.md), and must be set on the parent **Recordset** before the child **Recordset** is retrieved. This property simplifies navigating hierarchical recordsets.  
  
## Applies To  
 [Recordset Object (ADO)](./recordset-object-ado.md)  
  
## See Also  
 [StayInSync Property Example (VB)](./stayinsync-property-example-vb.md)   
 [Microsoft Data Shaping Service for OLE DB (ADO Service Provider)](../../guide/appendixes/microsoft-data-shaping-service-for-ole-db-ado-service-provider.md)
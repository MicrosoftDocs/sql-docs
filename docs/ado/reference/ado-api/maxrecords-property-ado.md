---
title: "MaxRecords Property (ADO)"
description: "MaxRecords Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Recordset15::MaxRecords"
helpviewer_keywords:
  - "MaxRecords property [ADO]"
apitype: "COM"
---
# MaxRecords Property (ADO)
Indicates the maximum number of records to return to a [Recordset](./recordset-object-ado.md) from a query.  
  
## Settings and Return Values  
 Sets or returns a **Long** value that indicates the maximum number of records to return. Default is zero (**0**), which means no limit.  
  
## Remarks  
 Use the **MaxRecords** property to limit the number of records that the provider returns from the data source. The default setting of this property is zero, which means the provider returns all requested records.  
  
 The **MaxRecords** property is read/write when the **Recordset** is closed and read-only when it is open.  
  
## Applies To  
 [Recordset Object (ADO)](./recordset-object-ado.md)  
  
## See Also  
 [MaxRecords Property Example (VB)](./maxrecords-property-example-vb.md)   
 [MaxRecords Property Example (VC++)](./maxrecords-property-example-vc.md)
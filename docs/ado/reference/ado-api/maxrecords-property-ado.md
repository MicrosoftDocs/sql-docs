---
title: "MaxRecords Property (ADO) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
apitype: "COM"
f1_keywords: 
  - "Recordset15::MaxRecords"
helpviewer_keywords: 
  - "MaxRecords property [ADO]"
ms.assetid: 20c76571-8c9a-482c-a99e-726ab1d93f8b
caps.latest.revision: 11
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# MaxRecords Property (ADO)
Indicates the maximum number of records to return to a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) from a query.  
  
## Settings and Return Values  
 Sets or returns a **Long** value that indicates the maximum number of records to return. Default is zero (**0**), which means no limit.  
  
## Remarks  
 Use the **MaxRecords** property to limit the number of records that the provider returns from the data source. The default setting of this property is zero, which means the provider returns all requested records.  
  
 The **MaxRecords** property is read/write when the **Recordset** is closed and read-only when it is open.  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [MaxRecords Property Example (VB)](../../../ado/reference/ado-api/maxrecords-property-example-vb.md)   
 [MaxRecords Property Example (VC++)](../../../ado/reference/ado-api/maxrecords-property-example-vc.md)   

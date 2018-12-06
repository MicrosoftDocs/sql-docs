---
title: "Recordset, SourceRecordset Properties (RDS) | Microsoft Docs"
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: 
ms.prod: sql  
ms.prod_service: connectivity
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "Recordset property [ADO]"
ms.assetid: a29e3fb9-306d-497a-9a59-1856a914e5e9
author: MightyPen
ms.author: genemi
manager: craigg
---
# Recordset, SourceRecordset Properties (RDS)
Indicates the **Recordset** object returned from a custom business object.  
  
 **Applies To:** [DataControl Object (RDS)](../../../ado/reference/rds-api/datacontrol-object-rds.md)  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## Syntax  
  
```  
  
DataControl.SourceRecordset = Recordset  
Recordset = DataControl.Recordset   
```  
  
#### Parameters  
 *DataControl*  
 An object variable that represents an [RDS.DataControl](../../../ado/reference/rds-api/datacontrol-object-rds.md) object.  
  
 *Recordset*  
 An object variable that represents a **Recordset** object.  
  
## Remarks  
 You can set the **SourceRecordset** property to a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) returned from a custom business object.  
  
 These properties allow an application to handle the binding process by means of a custom process. They receive a rowset wrapped in a **Recordset** so that you can interact directly with the **Recordset**, performing actions such as setting properties or iterating through the **Recordset**.  
  
 You can set the **SourceRecordset** property or read the **Recordset** property at run time in scripting code.  
  
 **SourceRecordset** is a write-only property, in contrast to the **Recordset** property, which is a read-only property.  
  
## Applies To  
 [DataControl Object (RDS)](../../../ado/reference/rds-api/datacontrol-object-rds.md)  
  
## See Also  
 [Recordset and SourceRecordset Properties Example (VBScript)](../../../ado/reference/rds-api/recordset-and-sourcerecordset-properties-example-vbscript.md)   
 [CreateRecordset Method (RDS)](../../../ado/reference/rds-api/createrecordset-method-rds.md)   
 [Query Method (RDS)](../../../ado/reference/rds-api/query-method-rds.md)



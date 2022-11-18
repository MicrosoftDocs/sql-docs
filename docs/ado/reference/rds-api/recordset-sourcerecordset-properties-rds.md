---
title: "Recordset, SourceRecordset Properties (RDS)"
description: "Recordset, SourceRecordset Properties (RDS)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Recordset property [ADO]"
apitype: "COM"
---
# Recordset, SourceRecordset Properties (RDS)
Indicates the **Recordset** object returned from a custom business object.  
  
 **Applies To:** [DataControl Object (RDS)](./datacontrol-object-rds.md)  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
## Syntax  
  
```  
  
DataControl.SourceRecordset = Recordset  
Recordset = DataControl.Recordset   
```  
  
#### Parameters  
 *DataControl*  
 An object variable that represents an [RDS.DataControl](./datacontrol-object-rds.md) object.  
  
 *Recordset*  
 An object variable that represents a **Recordset** object.  
  
## Remarks  
 You can set the **SourceRecordset** property to a [Recordset](../ado-api/recordset-object-ado.md) returned from a custom business object.  
  
 These properties allow an application to handle the binding process by means of a custom process. They receive a rowset wrapped in a **Recordset** so that you can interact directly with the **Recordset**, performing actions such as setting properties or iterating through the **Recordset**.  
  
 You can set the **SourceRecordset** property or read the **Recordset** property at run time in scripting code.  
  
 **SourceRecordset** is a write-only property, in contrast to the **Recordset** property, which is a read-only property.  
  
## Applies To  
 [DataControl Object (RDS)](./datacontrol-object-rds.md)  
  
## See Also  
 [Recordset and SourceRecordset Properties Example (VBScript)](./recordset-and-sourcerecordset-properties-example-vbscript.md)   
 [CreateRecordset Method (RDS)](./createrecordset-method-rds.md)   
 [Query Method (RDS)](./query-method-rds.md)
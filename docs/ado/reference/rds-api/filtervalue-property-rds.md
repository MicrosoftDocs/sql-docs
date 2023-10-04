---
title: "FilterValue Property (RDS)"
description: "FilterValue Property (RDS)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "FilterValue property [ADO]"
apitype: "COM"
---
# FilterValue Property (RDS)
Indicates the value with which to filter records.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
## Syntax  
  
```  
  
DataControl.FilterValue = String  
```  
  
#### Parameters  
 *DataControl*  
 An object variable that represents an [RDS.DataControl](./datacontrol-object-rds.md) object.  
  
 *String*  
 A **String** value that represents a data value with which to filter records (for example, `'Programmer'` or `125`).  
  
## Remarks  
 The [SortColumn](./sortcolumn-property-rds.md), [SortDirection](./sortdirection-property-rds.md), **FilterValue**, [FilterCriterion](./filtercriterion-property-rds.md), and [FilterColumn](./filtercolumn-property-rds.md) properties provide sorting and filtering functionality on the client-side cache. The sorting functionality orders records by values from one column. The filtering functionality displays a subset of records based on find criteria, while the full [Recordset](../ado-api/recordset-object-ado.md) is maintained in the cache. The [Reset](./reset-method-rds.md) method will execute the criteria and replace the current **Recordset** with an updatable **Recordset**.  
  
 Null values result in a type mismatch error.  
  
## Applies To  
 [DataControl Object (RDS)](./datacontrol-object-rds.md)  
  
## See Also  
 [FilterColumn, FilterCriterion, FilterValue, SortColumn, and SortDirection Properties and Reset Method Example (VBScript)](./filter-column-criterion-value-sortcolumn-sortdirection-example-vbscript.md)   
 [FilterColumn Property (RDS)](./filtercolumn-property-rds.md)   
 [FilterCriterion Property (RDS)](./filtercriterion-property-rds.md)   
 [SortColumn Property (RDS)](./sortcolumn-property-rds.md)   
 [SortDirection Property (RDS)](./sortdirection-property-rds.md)
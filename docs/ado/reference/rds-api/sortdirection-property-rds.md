---
title: "SortDirection Property (RDS) | Microsoft Docs"
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: 
ms.prod: sql  
ms.prod_service: connectivity
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "SortDirection property [RDS]"
ms.assetid: 1d9d8715-e4ad-4ff3-bf7f-f1dc0532d8c2
author: MightyPen
ms.author: genemi
manager: craigg
---
# SortDirection Property (RDS)
Indicates whether a sort order is ascending or descending.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## Syntax  
  
```  
  
DataControl.SortDirection = value  
```  
  
#### Parameters  
 *DataControl*  
 An object variable that represents an [RDS.DataControl](../../../ado/reference/rds-api/datacontrol-object-rds.md) object.  
  
 *Value*  
 A **Boolean** value that, when set to **True**, indicates the sort direction is ascending. **False** indicates descending order.  
  
## Remarks  
 The [SortColumn](../../../ado/reference/rds-api/sortcolumn-property-rds.md), **SortDirection**, [FilterValue](../../../ado/reference/rds-api/filtervalue-property-rds.md), [FilterCriterion](../../../ado/reference/rds-api/filtercriterion-property-rds.md), and [FilterColumn](../../../ado/reference/rds-api/filtercolumn-property-rds.md) properties provide sorting and filtering functionality on the client-side cache. The sorting functionality orders records by using values from one column. The filtering functionality displays a subset of records based on find criteria, while the full [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) is maintained in the cache. The [Reset](../../../ado/reference/rds-api/reset-method-rds.md) method will execute the criteria and replace the current **Recordset** with an updatable **Recordset**.  
  
## Applies To  
 [DataControl Object (RDS)](../../../ado/reference/rds-api/datacontrol-object-rds.md)  
  
## See Also  
 [FilterColumn, FilterCriterion, FilterValue, SortColumn, and SortDirection Properties and Reset Method Example (VBScript)](../../../ado/reference/rds-api/filter-column-criterion-value-sortcolumn-sortdirection-example-vbscript.md)   
 [Sort Property](../../../ado/reference/ado-api/sort-property.md)   
 [SortColumn Property (RDS)](../../../ado/reference/rds-api/sortcolumn-property-rds.md)



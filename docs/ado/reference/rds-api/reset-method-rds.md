---
title: "Reset Method (RDS) | Microsoft Docs"
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: 
ms.prod: sql  
ms.prod_service: connectivity
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "Reset method [ADO]"
ms.assetid: 3957197a-f543-4d6b-9e11-67a77c2063b7
author: MightyPen
ms.author: genemi
manager: craigg
---
# Reset Method (RDS)
Executes the sort or filter on a client-side **Recordset** based on the specified sort and filter properties.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## Syntax  
  
```  
  
DataControl.Reset(value)  
```  
  
#### Parameters  
 *DataControl*  
 An object variable that represents an [RDS.DataControl](../../../ado/reference/rds-api/datacontrol-object-rds.md) object.  
  
 *value*  
 Optional. A **Boolean** value that is **True** (default) if you want to filter on the current "filtered" rowset. **False** indicates that you filter on the original rowset, removing any previous filter options.  
  
## Remarks  
 The [SortColumn](../../../ado/reference/rds-api/sortcolumn-property-rds.md), [SortDirection](../../../ado/reference/rds-api/sortdirection-property-rds.md), [FilterValue](../../../ado/reference/rds-api/filtervalue-property-rds.md), [FilterCriterion](../../../ado/reference/rds-api/filtercriterion-property-rds.md), and [FilterColumn](../../../ado/reference/rds-api/filtercolumn-property-rds.md) properties provide sorting and filtering functionality on the client-side cache. The sorting functionality orders records by values from one column. The filtering functionality displays a subset of records based on a find criteria, while the full [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) is maintained in the cache. The **Reset** method will execute the criteria and replace the current **Recordset** with an updatable **Recordset**.  
  
 If there are changes to the original data that have not been submitted, the **Reset** method will fail. First, use the [SubmitChanges](../../../ado/reference/rds-api/submitchanges-method-rds.md) method to save any changes in a read/write **Recordset**, and then use the **Reset** method to sort or filter the records.  
  
 If you want to perform more than one filter on your rowset, you can use the optional *Boolean* argument with the **Reset** method. The following example shows how to do this:  
  
```  
ADC.SQL = "Select au_lname from authors"  
ADC.Refresh    ' Get the new rowset.  
  
ADC.FilterColumn = "au_lname"  
ADC.FilterCriterion = "<"  
ADC.FilterValue = "'M'"  
ADC.Reset         ' Rowset now has all Last Names < "M".  
  
ADC.FilterCriterion = ">"  
ADC.FilterValue = "'F'"  
' Passing True is not necessary, because it is the   
' default filter on the current "filtered" rowset.  
ADC.Reset(TRUE)     ' Rowset now has all Last   
                    ' Names < "M" and > "F".  
  
ADC.FilterCriterion = ">"  
ADC.FilterValue = "'T'"  
' Filter on the original rowset, throwing out the  
' previous filter options.  
ADC.Reset(FALSE)   ' Rowset now has all Last Names > "T".  
```  
  
## Applies To  
 [DataControl Object (RDS)](../../../ado/reference/rds-api/datacontrol-object-rds.md)  
  
## See Also  
 [FilterColumn, FilterCriterion, FilterValue, SortColumn, and SortDirection Properties and Reset Method Example (VBScript)](../../../ado/reference/rds-api/filter-column-criterion-value-sortcolumn-sortdirection-example-vbscript.md)   
 [SubmitChanges Method (RDS)](../../../ado/reference/rds-api/submitchanges-method-rds.md)




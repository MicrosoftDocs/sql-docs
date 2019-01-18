---
title: "SQL Property | Microsoft Docs"
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: 
ms.prod: sql  
ms.prod_service: connectivity
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "SQL property [RDS]"
ms.assetid: e0dabf23-a159-4fe5-a962-3df544a21f5c
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQL Property
Indicates the query string used to retrieve the [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md).  
  
 You can set the **SQL** property at design time in the [RDS.DataControl](../../../ado/reference/rds-api/datacontrol-object-rds.md) object's OBJECT tags, or at run time in scripting code.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## Syntax  
  
```  
  
Design time: <PARAM NAME="SQL" VALUE="QueryString">  
Run time: DataControl.SQL = "QueryString"  
```  
  
#### Parameters  
 *QueryString*  
 A **String** value that contains a valid SQL data request.  
  
 *DataControl*  
 An object variable that represents an **RDS.DataControl** object.  
  
## Remarks  
 In general, this is an SQL statement (using the dialect of the database server), such as `"Select * from NewTitles"`. To ensure that records are matched and updated accurately, an updatable query must contain a field other than a Long Binary field or a computed field.  
  
 The **SQL** property is optional if a custom server-side business object retrieves the data for the client.  
  
## Applies To  
 [DataControl Object (RDS)](../../../ado/reference/rds-api/datacontrol-object-rds.md)  
  
## See Also  
 [SQL Property Example (VBScript)](../../../ado/reference/rds-api/sql-property-example-vbscript.md)   
 [Connect Property (RDS)](../../../ado/reference/rds-api/connect-property-rds.md)   
 [Query Method (RDS)](../../../ado/reference/rds-api/query-method-rds.md)   
 [Refresh Method (RDS)](../../../ado/reference/rds-api/refresh-method-rds.md)   
 [SubmitChanges Method (RDS)](../../../ado/reference/rds-api/submitchanges-method-rds.md)



---
title: "SQL Property"
description: "SQL Property"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "SQL property [RDS]"
apitype: "COM"
---
# SQL Property
Indicates the query string used to retrieve the [Recordset](../ado-api/recordset-object-ado.md).  
  
 You can set the **SQL** property at design time in the [RDS.DataControl](./datacontrol-object-rds.md) object's OBJECT tags, or at run time in scripting code.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
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
 [DataControl Object (RDS)](./datacontrol-object-rds.md)  
  
## See Also  
 [SQL Property Example (VBScript)](./sql-property-example-vbscript.md)   
 [Connect Property (RDS)](./connect-property-rds.md)   
 [Query Method (RDS)](./query-method-rds.md)   
 [Refresh Method (RDS)](./refresh-method-rds.md)   
 [SubmitChanges Method (RDS)](./submitchanges-method-rds.md)
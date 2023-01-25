---
title: "RDS Properties"
description: "RDS Properties"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "RDS properties [ADO]"
  - "properties [ADO], RDS"
---
# RDS Properties
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
|Property|Description|  
|-|-|  
|[Connect (RDS)](./connect-property-rds.md)|Indicates the database name from which the query and update operations are run.|  
|[ExecuteOptions (RDS)](./executeoptions-property-rds.md)|Indicates whether asynchronous execution is enabled.|  
|[FetchOptions (RDS)](./fetchoptions-property-rds.md)|Indicates the type of asynchronous fetching.|  
|[FilterColumn (RDS)](./filtercolumn-property-rds.md)|Indicates the column on which to evaluate the filter criteria.|  
|[FilterCriterion (RDS)](./filtercriterion-property-rds.md)|Indicates the evaluation operator to use in the filter value.|  
|[FilterValue (RDS)](./filtervalue-property-rds.md)|Indicates the value to filter records.|  
|[Handler (RDS)](./handler-property-rds.md)|Indicates the name of a server-side customization program (*handler*) that extends the functionality of the **RDSServer.DataFactory**, and any parameters used by the *handler*.|  
|[InternetTimeout (RDS)](./internettimeout-property-rds.md)|Indicates the number of milliseconds to wait before a request times out.|  
|[ReadyState (RDS)](./readystate-property-rds.md)|Indicates the progress of a **DataControl** object as it fetches data into its **Recordset** object.|  
|[Recordset and SourceRecordset (RDS)](./recordset-sourcerecordset-properties-rds.md)|Indicates the **Recordset** object returned from a custom business object.|  
|[Server (RDS)](./server-property-rds.md)|Indicates the Internet Information Services (IIS) name and communication protocol.|  
|[SortColumn (RDS)](./sortcolumn-property-rds.md)|Indicates by which column to sort the records.|  
|[SortDirection (RDS)](./sortdirection-property-rds.md)|Indicates whether a sort order is ascending or descending.|  
|[SQL (RDS)](./sql-property.md)|Indicates the query string used to retrieve the **Recordset**.|  
|[URL (RDS)](./url-property-rds.md)|Indicates a string that contains a relative or absolute URL.|
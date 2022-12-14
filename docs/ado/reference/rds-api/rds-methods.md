---
title: "RDS Methods"
description: "RDS Methods"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "RDS methods [ADO]"
  - "methods [ADO], RDS"
---
# RDS Methods
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
|Method|Description|  
|-|-|  
|[Cancel (RDS)](./cancel-method-rds.md)|Cancels execution of a pending, asynchronous method call.|  
|[CancelUpdate (RDS)](./cancelupdate-method-rds.md)|Cancels any changes made to the current or new row of a **Recordset** object.|  
|[ConvertToString (RDS)](./converttostring-method-rds.md)|Converts a **Recordset** to a MIME string that represents the recordset data.|  
|[CreateObject (RDS)](./createobject-method-rds.md)|Creates the proxy for the target business object and returns a pointer to it.|  
|[CreateRecordset (RDS)](./createrecordset-method-rds.md)|Creates an empty, disconnected **Recordset**.|  
|[Execute Method (RDS)](./execute-method-rds.md)|Execute the request and create an advanced data rowset (for use with ADO 2.5 and later).|  
|[Execute21 Method (RDS)](./execute21-method-rds.md)|Execute the request and create an advanced data rowset (for use with ADO 2.1).|  
|[InvokeService (RDS)](./invokeservice-rds.md)|Returns a pointer to the requested interface on a more capable version of the object.|  
|[MoveFirst, MoveLast, MoveNext, MovePrevious (RDS)](./movefirst-movelast-movenext-and-moveprevious-methods-rds.md)|Moves to the first, last, next, or previous record in a specified **Recordset** object.|  
|[Query (RDS)](./query-method-rds.md)|Uses a valid SQL query string to return a **Recordset**.|  
|[Refresh (RDS)](./refresh-method-rds.md)|Requeries the data source specified in the **Connect** property and updates the query results.|  
|[Reset (RDS)](./reset-method-rds.md)|Executes the sort or filter on a client-side **Recordset**, based on the specified sort and filter properties.|  
|[SubmitChanges (RDS)](./submitchanges-method-rds.md)|Submits pending changes of the locally cached and updatable **Recordset** to the data source specified in the **Connect** property.|  
|[Synchronize Method (RDS)](./synchronize-method-rds.md)|Synchronize the given recordset with the database specified by the connection string (for use with ADO 2.5 and later).|  
|[Synchronize21 Method (RDS)](./synchronize21-method-rds.md)|Synchronize the given recordset with the database specified by the connection string (for use with ADO 2.1).|
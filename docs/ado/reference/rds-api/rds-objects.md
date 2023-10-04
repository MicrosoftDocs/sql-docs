---
title: "RDS Objects"
description: "RDS Objects"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "objects [ADO], RDS"
  - "RDS objects [ADO]"
---
# RDS Objects
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
|Object|Description|  
|-|-|  
|[DataControl (RDS)](./datacontrol-object-rds.md)|Binds a data query **Recordset** to one or more controls (for example, a text box, grid control, or combo box) to display the **Recordset** data on a Web page.<br /><br /> The **DataControl** object is safe for scripting.|  
|[DataFactory (RDSServer)](./datafactory-object-rdsserver.md)|Implements methods that provide read/write data access to specified data sources for client-side applications.<br /><br /> The **DataFactory** object is not safe for scripting.|  
|[DataSpace (RDS)](./dataspace-object-rds.md)|Creates client-side proxies to custom business objects located on the middle tier.<br /><br /> The **DataSpace** object is safe for scripting.|  
|[IRDSService Interface (RDS)](./irdsservice-interface-rds.md)|Exposes the [InvokeService (RDS)](./invokeservice-rds.md) method, which is used to return a pointer to the requested interface on a more capable version of the object.|  
  
## See Also  
 [RDS API Reference](./rds-api-reference.md)
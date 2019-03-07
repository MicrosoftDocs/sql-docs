---
title: "RDS Object Model Summary | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: 11/09/2018
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "RDS objects [ADO], object model summary"
  - "RDS object model [ADO]"
ms.assetid: 909f9af7-31db-4eec-ad52-650ce74dac2f
author: MightyPen
ms.author: genemi
manager: craigg
---
# RDS Object Model Summary
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
|Object|Description|  
|------------|-----------------|  
|[RDS.DataSpace](../../../ado/reference/rds-api/dataspace-object-rds.md)|This object contains a method to obtain a server proxy. The proxy may be the default or a custom server program (business object). The server program may be invoked on the Internet, an intranet, a local area network, or be a local dynamic-link library.<br /><br /> The **DataSpace** object is safe for scripting.|  
|[RDSServer.DataFactory](../../../ado/reference/rds-api/datafactory-object-rdsserver.md)|This object represents the default server program. It executes the default RDS data retrieval and update behavior.<br /><br /> The **DataFactory** object is not safe for scripting.|  
|[RDS.DataControl](../../../ado/reference/rds-api/datacontrol-object-rds.md)|This object can automatically invoke the **RDS.DataSpace** and **RDSServer.DataFactory** objects.<br /><br /> Use this object to invoke the default RDS data retrieval or update behavior.<br /><br /> This object also provides the means for visual controls to access the returned **Recordset** object.<br /><br /> The **DataControl** object is safe for scripting.|  
  
## See Also  
 [RDS Fundamentals](../../../ado/guide/remote-data-service/rds-fundamentals.md)   
 [RDS Scenario](../../../ado/guide/remote-data-service/rds-scenario.md)   
 [RDS Tutorial](../../../ado/guide/remote-data-service/rds-tutorial.md)   
 [RDS Usage and Security](../../../ado/guide/remote-data-service/rds-usage-and-security.md)



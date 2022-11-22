---
title: "RDS Programming Model with Objects"
description: "RDS Programming Model with Objects"
author: rothja
ms.author: jroth
ms.date: 11/09/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "RDS programming model [ADO]"
  - "RDS objects [ADO]"
---
# RDS Programming Model with Objects
The goal of RDS is to gain access to and update data sources through an intermediary such as IIS. The programming model specifies the sequence of activities necessary to accomplish this goal. The object model specifies the objects whose methods and properties affect the programming model.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
 RDS provides the means to perform the following sequence of actions:  
  
-   Specify the program to be invoked on the server, and obtain a way (proxy) to refer to it from the client ([RDS.DataSpace](../../reference/rds-api/dataspace-object-rds.md)).  
  
-   Invoke the server program. Pass parameters to the server program that identifies the data source and the command to issue (proxy or [RDS.DataControl](../../reference/rds-api/datacontrol-object-rds.md)).  
  
-   The server program obtains a [Recordset](../../reference/ado-api/recordset-object-ado.md) object from the data source, typically by using ADO. Optionally, the **Recordset** object is processed on the server ([RDSServer.DataFactory](../../reference/rds-api/datafactory-object-rdsserver.md)).  
  
-   The server program returns the final **Recordset** object to the client application (proxy).  
  
-   On the client, the **Recordset** object is put into a form that can be easily used by visual controls (visual control and **RDS.DataControl**).  
  
-   Changes to the **Recordset** object are sent back to the server and used to update the data source (**RDS.DataControl** or **RDSServer.DataFactory**).  
  
## See Also  
 [RDS Object Model Summary](./rds-object-model-summary.md)   
 [DataControl Object (RDS)](../../reference/rds-api/datacontrol-object-rds.md)   
 [DataFactory Object (RDSServer)](../../reference/rds-api/datafactory-object-rdsserver.md)   
 [DataSpace Object (RDS)](../../reference/rds-api/dataspace-object-rds.md)   
 [RDS Scenario](./rds-scenario.md)   
 [RDS Tutorial](./rds-tutorial.md)   
 [Recordset Object (ADO)](../../reference/ado-api/recordset-object-ado.md)   
 [RDS Usage and Security](./rds-usage-and-security.md)
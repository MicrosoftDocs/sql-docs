---
title: "DataFactory Customization"
description: "DataFactory Customization"
author: rothja
ms.author: jroth
ms.date: 11/09/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "DataFactory customization in RDS [ADO]"
---
# DataFactory Customization
Remote Data Service (RDS) provides a way to easily perform data access in a three-tier client/server system. A client data control specifies connection and command string parameters to perform a query on a remote data source, or connection string and [Recordset](../../reference/ado-api/recordset-object-ado.md) object parameters to perform an update.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
 The parameters are passed to a server program, which performs the data-access operation on the remote data source. RDS provides a default server program called the [RDSServer.DataFactory](../../reference/rds-api/datafactory-object-rdsserver.md) object. The **RDSServer.DataFactory** object returns any **Recordset** object produced by a query to the client.  
  
 However, the **RDSServer.DataFactory** is limited to performing queries and updates. It cannot perform any validation or processing on the connection or command strings.  
  
 With ADO, you can specify that the **DataFactory** work in conjunction with another type of server program called a *handler*. The handler can modify client connection and command strings before they are used to access the data source. In addition, the handler can enforce access rights, which govern the ability of the client to read and write data to the data source.  
  
 The parameters the handler uses to modify client parameters and access rights are specified in sections of a customization file.  
  
 The following topics provide more information about customizing the **DataFactory** object.  
  
-   [Understanding the Customization File](./understanding-the-customization-file.md)  
  
-   [Customization File Connect Section](./customization-file-connect-section.md)  
  
-   [Customization File SQL Section](./customization-file-sql-section.md)  
  
-   [Customization File UserList Section](./customization-file-userlist-section.md)  
  
-   [Customization File Logs Section](./customization-file-logs-section.md)  
  
-   [Required Client Settings](./required-client-settings.md)  
  
-   [Writing Your Own Customized Handler](./writing-your-own-customized-handler.md)
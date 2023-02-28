---
title: "SqlServerAlias Class"
description: "SqlServerAlias Class"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.topic: "reference"
helpviewer_keywords:
  - "SqlServerAlias class"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "SqlServerAlias Class"
apitype: "MOFDef"
---
# SqlServerAlias Class
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  The [SqlServerAlias Class](../../../relational-databases/wmi-provider-configuration-classes/sqlserveralias-class/sqlserveralias-class.md) class represents a server connection alias.  
  
 A server connection alias is required when both the following occur:  
  
-   The client is connecting to an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] over a network transport that is not the default network transport.  
  
-   The instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to which the client is connected listens on an alternate named pipe.  
  
 **Note:** The [SqlServerAlias Class](../../../relational-databases/wmi-provider-configuration-classes/sqlserveralias-class/sqlserveralias-class.md) inherits the **Put** method from the Provider class. However, it does not return any results as indicated by the **Provider::Put** method. For more information, see the WMI documentation.  
  
## See Also  
 [Configure Client Protocols](../../../database-engine/configure-windows/configure-client-protocols.md)  
  

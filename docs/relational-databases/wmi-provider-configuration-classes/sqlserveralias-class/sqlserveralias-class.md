---
title: "SqlServerAlias Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 

ms.topic: "reference"
apiname: 
  - "SqlServerAlias Class"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "SqlServerAlias class"
ms.assetid: 475662b9-6985-45bf-b1e9-b0f26ef50443
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# SqlServerAlias Class
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  The [SqlServerAlias Class](../../../relational-databases/wmi-provider-configuration-classes/sqlserveralias-class/sqlserveralias-class.md) class represents a server connection alias.  
  
 A server connection alias is required when both the following occur:  
  
-   The client is connecting to an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] over a network transport that is not the default network transport.  
  
-   The instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to which the client is connected listens on an alternate named pipe.  
  
 **Note:** The [SqlServerAlias Class](../../../relational-databases/wmi-provider-configuration-classes/sqlserveralias-class/sqlserveralias-class.md) inherits the **Put** method from the Provider class. However, it does not return any results as indicated by the **Provider::Put** method. For more information, see the WMI documentation.  
  
## See Also  
 [Configure Client Protocols](https://technet.microsoft.com/library/ms181035.aspx)  
  
  

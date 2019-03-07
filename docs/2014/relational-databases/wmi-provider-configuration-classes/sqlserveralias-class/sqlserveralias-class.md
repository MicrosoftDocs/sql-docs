---
title: "SqlServerAlias Class | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: "reference"
api_name: 
  - "SqlServerAlias Class"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SqlServerAlias class"
ms.assetid: 475662b9-6985-45bf-b1e9-b0f26ef50443
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# SqlServerAlias Class
  The [SqlServerAlias Class](sqlserveralias-class.md) class represents a server connection alias.  
  
 A server connection alias is required when both the following occur:  
  
-   The client is connecting to an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] over a network transport that is not the default network transport.  
  
-   The instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to which the client is connected listens on an alternate named pipe.  
  
 **Note:** The [SqlServerAlias Class](sqlserveralias-class.md) inherits the `Put` method from the Provider class. However, it does not return any results as indicated by the `Provider::Put` method. For more information, see the WMI documentation.  
  
## See Also  
 [Configure Client Protocols](https://technet.microsoft.com/library/ms181035.aspx)  
  
  

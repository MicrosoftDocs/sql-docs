---
title: "Distributed Replay Client Configuration | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: ccf03e32-6bd9-43c0-b9b6-9fe0d9163339
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Distributed Replay Client Configuration
  Use the **Distributed Replay Client Configuration** page of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Wizard to specify the users you want to grant administrative permissions to for the Distributed Replay client service.  
  
 Users that have administrative permissions will have unlimited access to the Distributed Replay client service.  
  
## Options  
 **Controller Name**  
 This is an optional parameter, and the default value is \<*blank*>.  
  
 Enter the name of the controller that the client computer will communicate with for the Distributed Replay client service. Note the following:  
  
-   The name must be a fully qualified domain name (FQDN). For example, a host called server1 in the products hierarchy at Microsoft may have an FQDN of server1.products.microsoft.com.  
  
-   If you have already set up a controller, enter the name of the controller while configuring each client.  
  
-   If you have net yet set up a controller, you can leave the controller name blank. However, you must manually enter the controller name in the **client configuration** file.  
  
 **Working Directory**  
 Specify the working directory for the Distributed Replay client service.  
  
 The default working directory is \<*drive letter*>:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\DReplayClient\WorkingDir\\.  
  
 **Result Directory**  
 Specify the result directory for the Distributed Replay client service.  
  
 The default result directory is \<*drive letter*>:\Program Files\\[!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]\DReplayClient\ResultDir\\.  
  
  

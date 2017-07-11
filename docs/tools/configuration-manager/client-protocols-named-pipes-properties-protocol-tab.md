---
title: "Client Protocols - Named Pipes Properties (Protocol Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "pipes [SQL Server], connecting to"
  - "Named Pipes [SQL Server], default pipe"
  - "client protocols [SQL Server]"
ms.assetid: 30fbae62-2f2e-4d36-9c6e-3444fff68781
caps.latest.revision: 23
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Client Protocols - Named Pipes Properties (Protocol Tab)
  In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager use the **Protocol** tab on the **Named Pipes Properties** dialog box to view or modify the description of default pipe. To connect to a different pipe, type the pipe in the **Default Pipe** box. For more information about connection strings, see [Creating a Valid Connection String Using Named Pipes](http://msdn.microsoft.com/library/90930ff2-143b-4651-8ae3-297103600e4f).  
  
## Options  
 **Default Pipe**  
 Specifies the default pipe the Named Pipes Net-library will use to attempt to connect to the target instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] listens on: `\\.\pipe\sql\query`  
  
 To connect to the default pipe, enter `sql\query`  
  
 **Enabled**  
 Possible values are **Yes** and **No**.  
  
## See Also  
 [Choosing a Network Protocol](http://msdn.microsoft.com/library/6565fb7d-b076-4447-be90-e10d0dec359a)  
  
  
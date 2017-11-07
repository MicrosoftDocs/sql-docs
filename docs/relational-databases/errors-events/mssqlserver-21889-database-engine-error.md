---
title: "MSSQLSERVER_21889 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
helpviewer_keywords: 
  - "21889 (Database Engine error)"
ms.assetid: ae199540-7986-4cc2-b782-cd22793236d3
caps.latest.revision: 6
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_21889
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|21889|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SQLErrorNum21889|  
|Message Text|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance '%s' is not a replication publisher. Run **sp_adddistributor** on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance '%s' with distributor '%s' in order to enable the instance to host the publishing database '%s'. Make certain to specify the same login and password as that used for the original publisher.|  
  
## Explanation  
In order to host the publisher database, the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be a replication publisher. **sp_validate_redirected_publisher** calls **sp_helpdistributor** at the remote server to determine whether the server is a replication publisher. This error is returned when execution of the stored procedure **sp_helpdistributor** indicates that the target instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not a replication publisher.  
  
## User Action  
Execute **sp_adddistributor** at the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that hosts the publisher database. When running **sp_adddistributor**, specify the correct distributor. Use the same value for the *@password* parameter as that used when **sp_adddistributor** was initially run at the distributor.  
  

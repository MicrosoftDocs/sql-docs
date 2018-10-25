---
title: "Remove calls to the deprecated DBCC CONCURRENCYVIOLATION command | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 2cc9f6ff-de36-4e94-bd04-59f5c45c4911
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Remove calls to the deprecated DBCC CONCURRENCYVIOLATION command
  Upgrade Advisor detected the use of the DBCC CONCURRENCYVIOLATION command. This command is no longer available. Executing this command returns error 2526.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Description  
 No recent version of edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] includes a workload governor; therefore the command has been removed.  
  
## Corrective Action  
 Update applications and scripts to remove references to this deprecated command.  
  
## External Resources  
  

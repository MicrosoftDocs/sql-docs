---
title: "Agent XPs Server Configuration Option"
description: Discover how to use the Agent XPs option to enable SQL Server Agent extended stored procedures. View an example that uses this option.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/02/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "Agent XPs option"
  - "extended stored procedures [SQL Server], SQL Server Agent"
---
# Agent XPs Server Configuration Option
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Use the **Agent XPs** option to enable the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent extended stored procedures on this server. When this option is not enabled, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent node is not available in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Object Explorer.  
  
 When you use the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] tool to start the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service, these extended stored procedures are enabled automatically. For more information, see [Surface Area Configuration](../../relational-databases/security/surface-area-configuration.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] Object Explorer does not display the contents of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]Agent node unless these extended stored procedures are enabled regardless of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service state.  
  
 The possible values are:  
  
-   **0**, indicating that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent extended stored procedures are not available (the default).  
  
-   **1**, indicating that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent extended stored procedures are available.  
  
 The setting takes effect immediately without a server stop and restart.  
  
## Example
 The following example enables the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent extended stored procedures.  

1. From Microsoft SQL Server Management Studio connect to the Database Engine.

2.  From the Standard bar, click **New Query.**

3.  Copy and paste the following example into the query window and click **Execute**. 
  
```sql 
sp_configure 'show advanced options', 1;  
GO  
RECONFIGURE;  
GO  
sp_configure 'Agent XPs', 1;  
GO  
RECONFIGURE  
GO  
```  
  
## See Also  
 [Automated Administration Tasks &#40;SQL Server Agent&#41;](../../ssms/agent/automated-administration-tasks-sql-server-agent.md)   
 [Start, Stop, or Pause the SQL Server Agent Service](../../ssms/agent/start-stop-or-pause-the-sql-server-agent-service.md)  

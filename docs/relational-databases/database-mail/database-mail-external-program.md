---
description: "Database Mail External Program"
title: "Database Mail External Program | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "external programs [Database Mail]"
  - "DatabaseMail90.exe"
  - "Database Mail [SQL Server], external programs"
ms.assetid: bc124164-eb6e-4b7f-bf66-98a3113d02f7
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Database Mail External Program
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The Database Mail external executable is **DatabaseMail.exe**, located in the **MSSQL\Binn directory** of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation. Database Mail uses Service Broker activation to start the external program when there are e-mail messages to be processed. Database Mail starts one instance of the external program. The external program runs in the security context of the service account for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 **In this Topic:**  
  
-   [Database Mail External Program Concepts](#ComponentsAndConcepts)  
  
-   [Tasks Related to Configuring Database Mail External Program](#RelatedTasks)  
  
##  <a name="ComponentsAndConcepts"></a> Database Mail External Program Concepts  
 When the external program starts, the program connects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Windows Authentication and begins processing e-mail messages. When there have been no messages to send for the specified time-out period, the program exits. You can configure the amount of time that the program waits before exiting by using either Database Mail Configuration Wizard or the Database Mail stored procedures. For more information, see [sysmail_configure_sp &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sysmail-configure-sp-transact-sql.md).  
  
 The external program stores information in system tables in the **msdb** database. If the external program cannot communicate with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the program logs errors to the Microsoft Windows application event log. Additional message logging is provided when the logging level is set to **Verbose** in the **Configure System Parameters** dialog box of the **Database Mail Configuration Wizard**.  
  
 Notice that, for efficiency, the external program caches account and profile information. Therefore, configuration changes to accounts and profiles may not be reflected in the external program for a few minutes.  
  
##  <a name="RelatedTasks"></a> Tasks Related to Configuring Database Mail External Program  
  
|Configuration Task|Topic Link|  
|------------------------|----------------|  
|Specify the time that the External Program before exiting.|[sysmail_configure_sp &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sysmail-configure-sp-transact-sql.md)|  
  
## See Also  
 [SQL Server Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md)   
 [Database Mail Log and Audits](../../relational-databases/database-mail/database-mail-log-and-audits.md)   
 [Database Mail](../../relational-databases/database-mail/database-mail.md)  
  
  

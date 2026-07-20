---
title: "Database Mail External Program"
description: "Learn more about DatabaseMail.exe, the Database Mail external program."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 05/16/2025
ms.service: sql
ms.topic: concept-article
helpviewer_keywords:
  - "external programs [Database Mail]"
  - "DatabaseMail90.exe"
  - "Database Mail [SQL Server], external programs"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Database Mail external program

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  The Database Mail external executable is `DatabaseMail.exe`, located in the `\MSSQL\Binn directory` of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation. Database Mail uses Service Broker activation to start the external program when there are e-mail messages to be processed. Database Mail starts one instance of the external program. The external program runs in the security context of the service account for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  

<a id="ComponentsAndConcepts"></a>

## Database Mail external program concepts

 When the external program starts, the program connects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Windows Authentication and begins processing e-mail messages. When there have been no messages to send for the specified time-out period, the program exits. You can configure the amount of time that the program waits before exiting by using either Database Mail Configuration Wizard or the Database Mail stored procedures. For more information, see [sysmail_configure_sp (Transact-SQL)](../system-stored-procedures/sysmail-configure-sp-transact-sql.md).  

 The external program stores information in system tables in the `msdb` system database. If the external program cannot communicate with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the program logs errors to the Microsoft Windows Application event log. Additional message logging is provided when the logging level is set to **Verbose** in the **Configure System Parameters** dialog box of the **Database Mail Configuration Wizard**.  

 For efficiency, the external program caches account and profile information. Therefore, configuration changes to accounts and profiles might not be reflected in the external program for a few minutes.  

<a id="RelatedTasks"></a>

## Configure the Database Mail external program

|Configuration Task|Topic Link|  
|------------------------|----------------|  
| Specify the time that the External Program runs before exiting. |[sysmail_configure_sp (Transact-SQL)](../system-stored-procedures/sysmail-configure-sp-transact-sql.md)|  

## Related content

- [Service Broker](../../database-engine/configure-windows/sql-server-service-broker.md)
- [Database Mail Log and Audits](database-mail-log-and-audits.md)
- [Database Mail](database-mail.md)

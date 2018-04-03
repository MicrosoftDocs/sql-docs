---
title: "Service Accounts (Configure Database Mirroring Security Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-high-availability"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql12.swb.configdbmsecurwiz.serviceaccounts.f1"
ms.assetid: d58d8f93-7888-4d66-af4d-969ef6a2dbee
caps.latest.revision: 32
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Service Accounts (Configure Database Mirroring Security Wizard)
  When using Windows Authentication, if the server instances use different accounts, specify the service accounts for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. These service accounts must all be domain accounts (in the same or trusted domains).  
  
 If all the server instances use the same domain account or use certificate-based authentication, leave the fields blank. Simply click **Finish**, and the wizard automatically configures the accounts based on the account of the current wizard.  
  
> [!IMPORTANT]  
>  If the database mirroring endpoints of the server instances are configured to use certificates, you must leave the service account fields empty.  
  
 **To configure database mirroring by using SQL Server Management Studio**  
  
-   [Establish a Database Mirroring Session Using Windows Authentication &#40;SQL Server Management Studio&#41;](../../2014/database-engine/establish-database-mirroring-session-windows-authentication.md)  
  
-   [Start the Configuring Database Mirroring Security Wizard &#40;SQL Server Management Studio&#41;](../../2014/database-engine/start-the-configuring-database-mirroring-security-wizard.md)  
  
## Options  
 **Principal**  
 Specify the service account of the principal server instance. Enter the domain name in upper case:  
  
 *DOMAINNAME*\\*username*  
  
 **Mirror**  
 Specify the service account of the mirror server instance. Enter the domain name in upper case:  
  
 *DOMAINNAME*\\*username*  
  
 **Witness**  
 Specify the service account of the witness server instance. Enter the domain name in upper case:  
  
 *DOMAINNAME*\\*username*  
  
## See Also  
 [Database Properties &#40;Mirroring Page&#41;](../../2014/database-engine/database-properties-mirroring-page.md)   
 [Start Database Mirroring Monitor &#40;SQL Server Management Studio&#41;](../../2014/database-engine/start-database-mirroring-monitor-sql-server-management-studio.md)   
 [Database Mirroring &#40;SQL Server&#41;](../../2014/database-engine/database-mirroring-sql-server.md)   
 [Set Up Login Accounts for Database Mirroring or AlwaysOn Availability Groups &#40;SQL Server&#41;](../../2014/database-engine/set-up-login-accounts-database-mirroring-always-on-availability.md)  
  
  
---
title: "Service Accounts (Configure Database Mirroring Security Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "05/17/2016"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.configdbmsecurwiz.serviceaccounts.f1"
ms.assetid: d58d8f93-7888-4d66-af4d-969ef6a2dbee
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Service Accounts (Configure Database Mirroring Security Wizard)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  When using Windows Authentication, if the server instances use different accounts, specify the service accounts for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. These service accounts must all be domain accounts (in the same or trusted domains).  
  
 If all the server instances use the same domain account or use certificate-based authentication, leave the fields blank. Simply click **Finish**, and the wizard automatically configures the accounts based on the account of the current wizard.  
  
> [!IMPORTANT]  
>  If the database mirroring endpoints of the server instances are configured to use certificates, you must leave the service account fields empty.  
  
 **To configure database mirroring by using SQL Server Management Studio**  
  
-   [Establish a Database Mirroring Session Using Windows Authentication &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/establish-database-mirroring-session-windows-authentication.md)  
  
-   [Start the Configuring Database Mirroring Security Wizard &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/start-the-configuring-database-mirroring-security-wizard.md)  
  
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
 [Database Properties &#40;Mirroring Page&#41;](../../relational-databases/databases/database-properties-mirroring-page.md)   
 [Start Database Mirroring Monitor &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/start-database-mirroring-monitor-sql-server-management-studio.md)   
 [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md)   
 [Set Up Login Accounts for Database Mirroring or Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/database-mirroring/set-up-login-accounts-database-mirroring-always-on-availability.md)  
  
  

---
title: "Configure Security Wizard: Service Accounts"
description: "Describes the 'Service Accounts' page of the 'Configure Database Mirroring Security Wizard' in SQL Server Management Studio."
author: MikeRayMSFT
ms.author: mikeray
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: database-mirroring
ms.topic: conceptual
ms.custom: seo-lt-2019
f1_keywords:
  - "sql13.swb.configdbmsecurwiz.serviceaccounts.f1"
---
# Configure Database Mirroring Security Wizard: Service Accounts
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
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
  
  

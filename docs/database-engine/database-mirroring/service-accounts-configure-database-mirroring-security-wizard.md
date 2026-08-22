---
title: "Configure Security Wizard: Service Accounts"
description: "Describes the 'Service Accounts' page of the 'Configure Database Mirroring Security Wizard' in SQL Server Management Studio."
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: database-mirroring
ms.topic: ui-reference
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
  
## Related content

- [Database Properties (Mirroring Page)](../../relational-databases/databases/database-properties-mirroring-page.md)
- [Start Database Mirroring Monitor (SQL Server Management Studio)](start-database-mirroring-monitor-sql-server-management-studio.md)
- [Database Mirroring (SQL Server)](database-mirroring-sql-server.md)
- [Set Up Login Accounts - Database Mirroring Always On Availability](set-up-login-accounts-database-mirroring-always-on-availability.md)

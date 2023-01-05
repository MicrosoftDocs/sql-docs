---
title: "Add an encrypted database to an availability group"
description: "Learn the steps to add an encrypted (or recently decrypted) database to an Always On availability group."
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: how-to
ms.custom: seodec18
helpviewer_keywords:
  - "Transparent Data Encryption, AlwaysOn Availability Groups"
  - "Transparent Data Encryption, Always On Availability Groups"
  - "TDE, AlwaysOn Availability Groups"
  - "TDE, Always On Availability Groups"
  - "Availability Groups [SQL Server], interoperability"
---
# Add an encrypted database to an Always On availability group
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

  This topic contains information about the using currently encrypted or recently decrypted databases with [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)].  
  
 
##  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   If a database is encrypted or even contains a Database Encryption Key (DEK), you cannot use the [!INCLUDE[ssAoNewAgWiz](../../../includes/ssaonewagwiz-md.md)] or [!INCLUDE[ssAoAddDbWiz](../../../includes/ssaoadddbwiz-md.md)] to add the database to an availability group. Even if an encrypted database has been decrypted, its log backups might contain encrypted data. In this case, full initial data synchronization could fail on the database. This is because the restore log operation might require the certificate that was used by the database encryption keys (DEKs), and that certificate might be unavailable.  
  
     To make a decrypted database eligible to add to an availability group using the wizard:  
  
    1.  Create a full database backup of the primary database. 
  
    2.  Create a log backup of the primary database.  
  
    3.  Restore the database backup on the server instance that hosts the secondary replica.  
    
    4.  Restore the log backup on the secondary database.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Manually Prepare a Secondary Database for an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md)  
  
-   [Use the Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-availability-group-wizard-sql-server-management-studio.md)  
  
-   [Use the Add Database to Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/availability-group-add-database-to-group-wizard.md)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Transparent Data Encryption &#40;TDE&#41;](../../../relational-databases/security/encryption/transparent-data-encryption.md)  
  
  

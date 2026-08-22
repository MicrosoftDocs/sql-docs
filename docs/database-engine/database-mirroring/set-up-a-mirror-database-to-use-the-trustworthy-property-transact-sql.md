---
title: "Enable trustworthy property for a mirrored database"
description: Learn how to enable the TRUSTWORTHY database property on a newly mirrored database by using Transact-SQL in SQL Server.
author: MashaMSFT
ms.author: mathoma
ms.date: "03/09/2017"
ms.service: sql
ms.subservice: database-mirroring
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "TRUSTWORTHY database option"
  - "mirror database [SQL Server]"
  - "database mirroring [SQL Server], security"
---
# Set Up a Mirror Database to Use the Trustworthy Property (Transact-SQL)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

> [!CAUTION]
> [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] For high availability, use [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] instead.  

> [!IMPORTANT]
> Database Mirroring in SQL Server is a distinct technology from [Microsoft Fabric Database Mirroring](/fabric/database/mirrored-database/overview). Mirroring to Fabric provides better analytical performance, the ability to unify your data estate with OneLake in Fabric, and open access to your data in Delta Parquet format.
>
> With Mirroring to Microsoft Fabric, you can continuously replicate your existing data estate directly into OneLake in Fabric, including data from SQL Server 2016+, Azure SQL Database, Azure SQL Managed Instance, Cosmos DB, Oracle, Snowflake, and more.

  When a database is backed up, the TRUSTWORTHY database property is set to OFF. Therefore, on a new mirror database TRUSTWORTHY is always OFF. If the database needs to be trustworthy after a failover, extra setup steps are necessary after mirroring begins.  
  
  For information about this database property, see [TRUSTWORTHY Database Property](../../relational-databases/security/trustworthy-database-property.md).  
  
## Procedure  
  
#### To setup a mirror database to use the Trustworthy Property  
  
1.  On the principal server instance, verify that the principal database has the Trustworthy property turned on.  
  
    ```  
    SELECT name, database_id, is_trustworthy_on FROM sys.databases   
    ```  
  
     For more information, see [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md).  
  
2.  After starting mirroring, verify that the database is currently the principal database, the session is using a synchronous operating mode, and the session is already synchronized.  
  
    ```  
    SELECT database_id, mirroring_role, mirroring_safety_level_desc, mirroring_state_desc FROM sys.database_mirroring  
    ```  
  
     For more information, see [sys.database_mirroring &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-mirroring-transact-sql.md).  
  
3.  Once the mirroring session is synchronized, manually fail over to the mirror database.  
  
     This can be done in either SQL Server Management Studio or using Transact-SQL:  
  
    -   [Manually Fail Over a Database Mirroring Session &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/manually-fail-over-a-database-mirroring-session-sql-server-management-studio.md)  
  
    -   [Manually Fail Over a Database Mirroring Session &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/manually-fail-over-a-database-mirroring-session-transact-sql.md)  
  
4.  Turn on the trustworthy database property using the following ALTER DATABASE command:  
  
    ```  
    ALTER DATABASE <database_name> SET TRUSTWORTHY ON  
    ```  
  
     For more information, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).  
  
5.  Optionally, manually failover again to return to the original principal.  
  
6.  Optionally, switch to asynchronous, high-performance mode by setting SAFETY to OFF and ensuring that WITNESS is also set to OFF.  
  
     In Transact-SQL:  
  
    -   [Change Transaction Safety in a Database Mirroring Session &#40;Transact-SQL&#41;](../../database-engine/database-mirroring/change-transaction-safety-in-a-database-mirroring-session-transact-sql.md)  
  
    -   [Remove the Witness from a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/remove-the-witness-from-a-database-mirroring-session-sql-server.md)  
  
     In SQL Server Management Studio:  
  
    -   [Establish a Database Mirroring Session Using Windows Authentication &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/establish-database-mirroring-session-windows-authentication.md)  
  
## Related content

- [TRUSTWORTHY database property](../../relational-databases/security/trustworthy-database-property.md)
- [Set Up an Encrypted Mirror Database](set-up-an-encrypted-mirror-database.md)

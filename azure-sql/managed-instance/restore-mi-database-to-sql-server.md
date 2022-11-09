---
title: Restore database to SQL Server
titleSuffix: Azure SQL Managed Instance
description: Learn how to restore a database to SQL Server 2022 from Azure SQL Managed Instance
author: mladjoa
ms.author: mlandzic
ms.reviewer: mathoma, danil
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: data-movement
ms.topic: how-to
ms.custom: 
---
# Restore database to SQL Server from Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches you to restore your database backup from Azure SQL Managed Instance to SQL Server 2022. 

## Overview

The database format alignment between SQL Managed Instance and SQL Server 2022 gives you an easy way to copy or move your databases from SQL Managed Instance to SQL Server 2022 instances hosted on-premises, virtual machines in Azure, or in other clouds. 

Restoring databases from SQL Managed Instance to to SQL Server 2022 unlocks the following scenarios:

- Database mobility between SQL Managed Instance and SQL Server-based products.
- Provide database copies to end customers or eligible parties. 
- Refresh environments outside of SQL Managed Instance.

The ability to restore databases from SQL Managed Instance to SQL Server 2022 is available by default in all existing and any new deployed instances.


## Take backup on SQL Managed Instance 

First, create a credential to access the storage account from SQL Managed Instance, and then take a copy-only backup of your database. 

You can create your credential by using an SAS token, or a managed identity. 

### [SAS token](#tab/sas-token)

To create a credential using an [SAS token](/sql/relational-databases/tutorial-use-azure-blob-storage-service-with-sql-server-2016), run the following sample T-SQL command: 

```sql
CREATE CREDENTIAL [https://<mystorageaccountname>.blob.core.windows.net/<containername>] 
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',  
SECRET = '<SAS_TOKEN>';  
```

### [Managed identity](#tab/managed-identity)

To create a credential using a [managed identity](/azure/active-directory/managed-identities-azure-resources/howto-assign-access-portal), run the following sample T-SQL command: 

```sql
CREATE CREDENTIAL [https://<mystorageaccountname>.blob.core.windows.net/<containername>] 
WITH IDENTITY = 'MANAGED IDENTITY'
```

---

Next, take a COPY_ONLY backup of your database by running the following sample T-SQL command: 


```sql 
BACKUP DATABASE [SampleDB]
TO URL = 'https://<mystorageaccountname>.blob.core.windows.net/<containername>/SampleDB.bak'
WITH COPY_ONLY
```


## Restore to SQL Server 

Restore the database to SQL Server by using the `WITH MOVE` command, and providing explicit file paths for your files on the destination server.

To restore your database to SQL Server, run the following sample T-SQL command: 

```sql
RESTORE DATABASE [SampleDB]
FROM URL = 'https://<mystorageaccountname>.blob.core.windows.net/<containername>/SampleDB.bak'
WITH
MOVE 'data_0' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SampleDB_data_0.mdf',
MOVE 'log' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SampleDBlog.ldf',
MOVE 'XTP' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SampleDB_xtp.xtp'
```

> [!NOTE]
> To restore databases that are encrypted at-rest (by using [TDE](../database/transparent-data-encryption-tde-overview.md)), the destination instance of SQL Server must have access to the same key used to protect the source database through the [SQL Server Connector for Azure Key Vault](https://techcommunity.microsoft.com/t5/azure-sql-blog/sql-server-connector-for-azure-key-vault-is-generally-available/ba-p/386105).


## Consideration

Consider the following:

- When restoring to SQL Server, you must use the `WITH MOVE` qualifier, and provide explicit paths for the data files. 
- Databases encrypted with service-managed TDE keys cannot be restored to SQL Server. You can only restore an encrypted database to SQL Server if it was encrypted with a customer-managed key assuming the destination server has access to the same key used to encrypt the database. 
- It's possible that, in the future, some features may be introduced to Azure SQL Managed Instance that require changes to the database format, making backups incompatible with SQL Server 2022. Access to such features will require explicit opt in. 

## Next steps

- To learn how to create your first managed instance, see [Quickstart guide](instance-create-quickstart.md).
- For a features and comparison list, see [SQL common features](../database/features-comparison.md).
- For more information about VNet configuration, see [SQL Managed Instance VNet configuration](connectivity-architecture-overview.md).
- For a quickstart that creates a managed instance and restores a database from a backup file, see [Create a managed instance](instance-create-quickstart.md).
- For a tutorial about using Azure Database Migration Service for migration, see [SQL Managed Instance migration using Database Migration Service](/azure/dms/tutorial-sql-server-to-managed-instance).
- For advanced monitoring of SQL Managed Instance database performance with built-in troubleshooting intelligence, see [Monitor Azure SQL Managed Instance using Azure SQL Analytics](/azure/azure-monitor/insights/azure-sql).
- For pricing information, see [SQL Database pricing](https://azure.microsoft.com/pricing/details/sql-database/managed/).
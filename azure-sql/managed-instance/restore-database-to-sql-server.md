---
title: Restore a database to a SQL Server instance
titleSuffix: Azure SQL Managed Instance
description: Learn how to restore a database to a SQL Server 2022 instance from an Azure SQL Managed Instance deployment.
author: mladjoa
ms.author: mlandzic
ms.reviewer: mathoma, danil
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: data-movement
ms.topic: how-to
ms.custom: 
---
# Restore a database to a SQL Server instance from a managed instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article describes how to restore a database backup from an Azure SQL Managed Instance deployment to a SQL Server 2022 instance. 

## Overview

The database format alignment between SQL Managed Instance and SQL Server 2022 gives you an easy way to copy or move databases from your managed instance to a SQL Server 2022 instance that's hosted on-premises, on virtual machines in Azure, or in other clouds. 

Restoring databases from managed instances to SQL Server 2022 instances unlocks the following scenarios:

- Ensures database mobility between SQL Managed Instance and SQL Server-based products.
- Provides database copies to customers and other eligible parties. 
- Refreshes environments outside SQL Managed Instance.

The ability to restore copy-only full backups of databases from a SQL Managed Instance deployment to a SQL Server 2022 instance is available by default in all existing and any new deployed managed instances.

> [!IMPORTANT]
> The ability to restore copy-only full backups of databases from SQL Managed Instance to SQL Server 2022 will be available until the end of [mainstream support for SQL Server 2022](../lifecycle/products/sql-server-2022). When this period expires, the ability to restore copy-only full backups of SQL Managed Instance databases will be available only to the next major version of SQL Server after SQL Server 2022.

## Take a backup of your managed instance 

First, create a credential to access the storage account from your managed instance, and then take a copy-only backup of your database. 

You can create your credential by using a shared access signature (SAS) token or a managed identity. 

### [SAS token](#tab/sas-token)

To create a credential by using a [shared access signature token](/sql/relational-databases/tutorial-use-azure-blob-storage-service-with-sql-server-2016), run the following sample T-SQL command: 

```sql
CREATE CREDENTIAL [https://<mystorageaccountname>.blob.core.windows.net/<containername>] 
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',  
SECRET = '<SAS_TOKEN>';  
```
### [Managed identity](#tab/managed-identity)

To create a credential by using a [managed identity](/azure/active-directory/managed-identities-azure-resources/howto-assign-access-portal), run the following sample T-SQL command: 

```sql
CREATE CREDENTIAL [https://<mystorageaccountname>.blob.core.windows.net/<containername>] 
WITH IDENTITY = 'MANAGED IDENTITY'
```

---

Next, take a `COPY_ONLY` backup of your database by running the following sample T-SQL command: 


```sql 
BACKUP DATABASE [SampleDB]
TO URL = 'https://<mystorageaccountname>.blob.core.windows.net/<containername>/SampleDB.bak'
WITH COPY_ONLY
```


## Restore to a SQL Server instance 

Restore the database to the SQL Server instance by using the `WITH MOVE` option of the RESTORE DATABASE T-SQL command, and then provide explicit file paths for your files on the destination server instance.

To restore your database to the SQL Server instance, run the following sample T-SQL command with file paths appropriate to your environment: 

```sql
RESTORE DATABASE [SampleDB]
FROM URL = 'https://<mystorageaccountname>.blob.core.windows.net/<containername>/SampleDB.bak'
WITH
MOVE 'data_0' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SampleDB_data_0.mdf',
MOVE 'log' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SampleDBlog.ldf',
MOVE 'XTP' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SampleDB_xtp.xtp'
```

> [!NOTE]
> To restore databases that are encrypted at rest by using [Transparent Data Encryption (TDE)](../database/transparent-data-encryption-tde-overview.md), the destination managed instance of SQL Server must have access to the same key that's used to protect the source database through the SQL Server Connector for Azure Key Vault. For details, review [Set up SQL Server TDE with AKV](/sql/relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault).


## Considerations

When you're restoring a database to a SQL Server instance, consider the following:

- You must use the `WITH MOVE` qualifier and provide explicit paths for the data files. 
- Databases that are encrypted with service-managed TDE keys can't be restored to a SQL Server instance. You can restore an encrypted database to SQL Server only if it was encrypted with a customer-managed key and the destination server has access to the same key that's used to encrypt the database. For more information, see [Set up SQL Server TDE with Azure Key Vault](/sql/relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault). 
- In the future, some features might be introduced to Azure SQL Managed Instance that require changes to the database format, making backups incompatible with SQL Server 2022. Access to such features will require explicit opt-in. 

## Next steps

- To learn how to create your first managed instance, see [Quickstart guide](instance-create-quickstart.md).
- For a features and comparison list, see [SQL common features](../database/features-comparison.md).
- For more information about virtual network configuration, see [SQL Managed Instance virtual network configuration](connectivity-architecture-overview.md).
- For a quickstart that creates a managed instance and restores a database from a backup file, see [Create a managed instance](instance-create-quickstart.md).
- For a tutorial about using Azure Database Migration Service for migration, see [SQL Managed Instance migration using Database Migration Service](/azure/dms/tutorial-sql-server-to-managed-instance).
- For advanced monitoring of SQL Managed Instance database performance with built-in troubleshooting intelligence, see [Monitor an Azure SQL Managed Instance deployment by using Azure SQL Analytics](/azure/azure-monitor/insights/azure-sql).
- For pricing information, see [SQL Database pricing](https://azure.microsoft.com/pricing/details/sql-database/managed/).

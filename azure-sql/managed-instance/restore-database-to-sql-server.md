---
title: Restore database to SQL Server
titleSuffix: Azure SQL Managed Instance
description: Learn how to restore a database to SQL Server 2022 from Azure SQL Managed Instance
author: mladjoa
ms.author: mlandzic
ms.reviewer: mathoma, danil
ms.date: 12/02/2022
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

Restoring databases from SQL Managed Instance to SQL Server 2022 unlocks the following scenarios:

- Database mobility between SQL Managed Instance and SQL Server-based products.
- Provide database copies to end customers or eligible parties. 
- Refresh environments outside of SQL Managed Instance.

The ability to restore copy-only full backups of databases from SQL Managed Instance to SQL Server 2022 is available by default in all existing and any new deployed instances.

> [!IMPORTANT]
> The ability to restore copy-only full backups of databases from SQL Managed Instance to SQL Server 2022 will be available until the end of the [mainstream support for SQL Server 2022](/lifecycle/products/sql-server-2022). Upon the expiry of this period, ability to restore copy-only full backups of SQL Managed Instance databases will be available only to the next major version of SQL Server, following the SQL Server 2022 version.

## Take backup on SQL Managed Instance

First, create a credential for accessing the storage account from SQL Managed Instance, and then take a copy-only backup of your database and store it . 

You can create your credential by using an SAS token, or a managed identity. 

### [Managed identity](#tab/managed-identity)

A **managed identity** is a feature of Azure Active Directory (Azure AD) that provides instances of Azure services - like Azure SQL Managed Instance - with an automatically managed identity in Azure AD, the system assigned managed identity. This identity can be used to authorize requests for data access to other Azure resources including storage accounts. Services like Azure SQL Managed Instance have a system assigned managed identity, and can also have one or more [user assigned managed identities](authentication-azure-ad-user-assigned-managed-identity-create-managed-instance.md). You can use either system assigned managed identities or user assigned managed identities to authorize.

Before writing backup file to a storage account, the Azure storage administrator must grant permissions to managed identity to write the data. Granting permissions to the managed identity of the managed instance is done the same way as granting permission to any other Azure AD user. For example:

1. In the Azure portal, in the **Access Control (IAM)** page of a storage account, select **Add role assignment**.  
1. Choose the **Storage Blob Data Contributor** built-in Azure RBAC role. This will provide read/write access to the managed identity for the necessary Azure Blob Storage containers.
    - Instead of granting the managed identity the **Storage Blob Data Contributor** Azure RBAC role, you can also grant more granular permissions. Learn more about how to [set ACLs in Azure Data Lake Storage Gen2](/azure/storage/blobs/data-lake-storage-explorer-acl.md).
1. On the next page, select **Assign access to** **Managed identity**. **+ Select members**, and under the **Managed identity** drop-down list, select the desired managed identity. For more information, see [Assign Azure roles using the Azure portal](/azure/role-based-access-control/role-assignments-portal).
1. Then, creating the database scoped credential for managed identity authentication is simple. Note in the following example that `'Managed Identity'` is a hard-coded string, and you need to replace the generic storage account name with the actual name of the storage account: 

```sql
CREATE CREDENTIAL [https://<mystorageaccountname>.blob.core.windows.net/<containername>] 
WITH IDENTITY = 'MANAGED IDENTITY'
```

### [SAS token](#tab/sas-token)

To create a credential using an [SAS token](/sql/relational-databases/tutorial-use-azure-blob-storage-service-with-sql-server-2016), run the following sample T-SQL command: 

```sql
CREATE CREDENTIAL [https://<mystorageaccountname>.blob.core.windows.net/<containername>] 
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',  
SECRET = '<SAS_TOKEN>';  
```

---

Next, take a COPY_ONLY backup of your database by running the following sample T-SQL command: 


```sql 
BACKUP DATABASE [SampleDB]
TO URL = 'https://<mystorageaccountname>.blob.core.windows.net/<containername>/SampleDB.bak'
WITH COPY_ONLY
```


## Restore to SQL Server

Restore the database to SQL Server by using the `WITH MOVE` option of the RESTORE DATABASE T-SQL command, and providing explicit file paths for your files on the destination server.

To restore your database to SQL Server, run the following sample T-SQL command with file paths appropriate to your environment: 

```sql
RESTORE DATABASE [SampleDB]
FROM URL = 'https://<mystorageaccountname>.blob.core.windows.net/<containername>/SampleDB.bak'
WITH
MOVE 'data_0' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SampleDB_data_0.mdf',
MOVE 'log' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SampleDBlog.ldf',
MOVE 'XTP' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SampleDB_xtp.xtp'
```

> [!NOTE]
> To restore databases that are encrypted at-rest (by using [Transparent Data Encryption - TDE](../database/transparent-data-encryption-tde-overview.md)), the destination instance of SQL Server must have access to the same key used to protect the source database through the SQL Server Connector for Azure Key Vault. For details, review [Set up SQL Server TDE with AKV](/sql/relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault).

## Considerations

Consider the following:

- When restoring to SQL Server, you must use the `WITH MOVE` qualifier, and provide explicit paths for the data files. 
- Databases encrypted with service-managed Transparent Data Encryption (TDE) keys cannot be restored to SQL Server. You can only restore an encrypted database to SQL Server if it was encrypted with a customer-managed key and the destination server has access to the same key used to encrypt the database. Review [Set up SQL Server TDE with AKV](/sql/relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault) to learn more. 
- It's possible that, in the future, some features may be introduced to Azure SQL Managed Instance that require changes to the database format, making backups incompatible with SQL Server 2022. Access to such features will require explicit opt-in. 

## Next steps

- To learn how to create your first managed instance, see [Quickstart guide](instance-create-quickstart.md).
- For a features and comparison list, see [SQL common features](../database/features-comparison.md).
- For more information about VNet configuration, see [SQL Managed Instance VNet configuration](connectivity-architecture-overview.md).
- For a quickstart that creates a managed instance and restores a database from a backup file, see [Create a managed instance](instance-create-quickstart.md).
- For a tutorial about using Azure Database Migration Service for migration, see [SQL Managed Instance migration using Database Migration Service](/azure/dms/tutorial-sql-server-to-managed-instance).
- For advanced monitoring of SQL Managed Instance database performance with built-in troubleshooting intelligence, see [Monitor Azure SQL Managed Instance using Azure SQL Analytics](/azure/azure-monitor/insights/azure-sql).
- For pricing information, see [SQL Database pricing](https://azure.microsoft.com/pricing/details/sql-database/managed/).

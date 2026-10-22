---
title: Restore a Database to SQL Server
titleSuffix: Azure SQL Managed Instance
description: Learn how to restore a database to SQL Server 2022, or SQL Server 2025, from Azure SQL Managed Instance.
author: mladjoa
ms.author: mlandzic
ms.reviewer: danil
ms.date: 09/15/2025
ms.service: azure-sql-managed-instance
ms.subservice: data-movement
ms.topic: how-to
ms.custom:
---

# Restore a database to SQL Server from Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article describes how to restore a database backup *from* Azure SQL Managed Instance *to* SQL Server 2022 *or* SQL Server 2025.

## Overview

When you configure your SQL Managed Instance with an [update policy](update-policy.md), you align your internal database format with a specific version of SQL Server. Database format alignment between SQL Managed Instance and SQL Server gives you an easy way to copy or move databases from your SQL managed instance to an Enterprise, Developer, or Standard edition of SQL Server hosted on-premises, on virtual machines in Azure, or in other clouds.

Restoring databases from SQL managed instances to SQL Server 2022, or SQL Server 2025, instances unlocks the following scenarios:

- Ensures database mobility between SQL Managed Instance and SQL Server-based products.
- Provides database copies to customers and other eligible parties.
- Refreshes environments outside SQL Managed Instance.

Consider the following: 
- The ability to restore copy-only full backups of databases from SQL Managed Instance to SQL Server 2022 is available by default for all existing and newly deployed instances. This ability is available until the end of [mainstream support for SQL Server 2022](/lifecycle/products/sql-server-2022). Once the update policy of an instance is changed to **SQL Server 2025**, or **Always-up-to-date**, restoring a database to SQL Server 2022 is no longer possible.
- The ability to restore copy-only full backups of databases from SQL Managed Instance to SQL Server 2025 is only available to instances configured with the [SQL Server 2025 update policy](update-policy.md#sql-server-2025-update-policy). This ability is available until the end of mainstream support for SQL Server 2025. Once the update policy of an instance is changed to **Always-up-to-date**, restoring a database to SQL Server 2025 is no longer possible.


## Take a backup on SQL Managed Instance

First, create a credential to access the storage account from your instance, take a copy-only backup of your database, and then store it.

You can create your credential by using a managed identity or a shared access signature (SAS) token.

### [Managed identity](#tab/managed-identity)

A *managed identity* is a feature of Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) that provides instances of Azure services, such as Azure SQL Managed Instance, with an automatically managed identity in Microsoft Entra ID, the system-assigned managed identity.

You can use this identity to authorize requests for data access to other Azure resources, including storage accounts. Services such as Azure SQL Managed Instance have a system assigned managed identity, and can also have one or more [user-assigned managed identities](authentication-azure-ad-user-assigned-managed-identity-create-managed-instance.md). You can use either system-assigned managed identities or user-assigned managed identities to authorize the requests.

Before the Azure storage administrator writes a backup file to a storage account, they must grant permissions to the managed identity to write the data. Granting permissions to the managed identity of the instance is done the same way as granting permissions to any other Microsoft Entra user. For example:

1. In the Azure portal, on the **Access Control (IAM)** pane of a storage account, select **Add role assignment**.

1. Select the **Storage Blob Data Contributor** built-in Azure role-based access control (RBAC) role. This provides read/write access to the managed identity for the necessary Azure Blob Storage containers.

   Instead of granting the managed identity the Storage Blob Data Contributor Azure RBAC role, you can grant more granular permissions. To learn more, see [Set ACLs in Azure Data Lake Storage Gen2](/azure/storage/blobs/data-lake-storage-explorer-acl).

1. On the next page, for **Assign access to**, select **Managed identity**.

1. Choose **Select members** and then, in the **Managed identity** dropdown list, select the appropriate managed identity. For more information, see [Assign Azure roles by using the Azure portal](/azure/role-based-access-control/role-assignments-portal).

Now, creating the database-scoped credential for managed identity authentication is simple.

In the following example, note that `Managed Identity` is a hard-coded string, and you need to replace the generic storage account name with the name of the actual storage account:

```sql
CREATE CREDENTIAL [https://<mystorageaccountname>.blob.core.windows.net/<containername>]
WITH IDENTITY = 'MANAGED IDENTITY';
```

### [SAS token](#tab/sas-token)

To create a credential using a [SAS token](/sql/relational-databases/tutorial-use-azure-blob-storage-service-with-sql-server-2016), run the following sample T-SQL command:

```sql
CREATE CREDENTIAL [https://<mystorageaccountname>.blob.core.windows.net/<containername>]
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
     SECRET = '<SAS_TOKEN>';
```

---

Next, take a `COPY_ONLY` backup of your database by running the following sample T-SQL command:

```sql
BACKUP DATABASE [SampleDB]
TO URL = 'https://<mystorageaccountname>.blob.core.windows.net/<containername>/SampleDB.bak'
WITH COPY_ONLY;
```

## Restore to SQL Server

Restore the database to SQL Server by using the `WITH MOVE` option of the RESTORE DATABASE T-SQL command and providing explicit file paths for your files on the destination server.

To restore your database to SQL Server, run the following sample T-SQL command with file paths appropriate to your environment:

```sql
RESTORE DATABASE [SampleDB]
FROM URL = 'https://<mystorageaccountname>.blob.core.windows.net/<containername>/SampleDB.bak'
WITH
    MOVE 'data_0' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SampleDB_data_0.mdf',
    MOVE 'log' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SampleDBlog.ldf',
    MOVE 'XTP' TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SampleDB_xtp.xtp';
```

> [!NOTE]  
> To restore databases that are encrypted at rest by using [Transparent data encryption (TDE)](../database/transparent-data-encryption-tde-overview.md), the destination instance of SQL Server must have access to the same key that's used to protect the source database through the SQL Server Connector for Azure Key Vault. For details, review [Set up SQL Server TDE Extensible Key Management by using Azure Key Vault](/sql/relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault).

## Considerations

When you're restoring a database to SQL Server, consider the following:

- You must use the `WITH MOVE` qualifier and provide explicit paths for the data files.

- Databases that are encrypted with service-managed TDE keys can't be restored to SQL Server. You can restore an encrypted database to SQL Server only if it was encrypted with a customer-managed key and the destination server has access to the same key that's used to encrypt the database. For more information, see [Set up SQL Server TDE Extensible Key Management by using Azure Key Vault](/sql/relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault).

- The update policy for your SQL Managed Instance must match, or be a higher, version of your SQL Server instance. Databases restored to SQL Server 2022 must come from instances with the [**SQL Server 2022** update policy](update-policy.md#sql-server-2022-update-policy). Likewise, databases restored to SQL Server 2025 must come from instances with the [**SQL Server 2025** update policy](update-policy.md#sql-server-2025-update-policy). It's also possible to restore a database from an instance with a **SQL Server 2022** update policy to an instance with a **SQL Server 2025** update policy. Once a database is restored to an instance with a higher version update policy, that database can no longer be restored to an instance with a lower version update policy. Restoring databases from instances with a lower version update policy isn't supported. 

- After restoring an Azure SQL Managed Instance database to SQL Server, and dropping an index, or a table with an index, you might see [Error 8992](doc-changes-updates-known-issues.md#error-8992-when-running-dbcc-checkdb-on-a-sql-server-database-that-originated-from-sql-managed-instance) when running the `DBCC CHECKDB` command.

  > [!CAUTION]  
  > If you create a partitioned index on a table after dropping an index as described in this scenario, the table becomes inaccessible.

## Related content

- To learn how to create your first SQL managed instance, see the [Quickstart guide](instance-create-quickstart.md).
- For a features and comparison list, see [SQL common features](../database/features-comparison.md).
- For more information about virtual network configuration, see [SQL Managed Instance virtual network configuration](connectivity-architecture-overview.md).
- For a quickstart that creates a SQL managed instance and restores a database from a backup file, see [Create a SQL managed instance](instance-create-quickstart.md).
- For a tutorial about using Azure Database Migration Service for migration, see [SQL Managed Instance migration using Database Migration Service](/azure/dms/tutorial-sql-server-to-managed-instance).
- For advanced monitoring of SQL Managed Instance, see [database watcher](../database-watcher-overview.md).
- For pricing information, see [SQL Database pricing](https://azure.microsoft.com/pricing/details/sql-database/managed/).

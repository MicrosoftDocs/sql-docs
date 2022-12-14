---
title: "CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)"
description: CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "04/13/2022"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DATABASE SCOPED CREDENTIAL"
  - "DATABASE_SCOPED_CREDENTIAL_TSQL"
  - "SCOPED_TSQL"
  - "CREATE_DATABASE_SCOPED_CREDENTIAL"
  - "CREATE_DATABASE_SCOPED_CREDENTIAL_TSQL"
  - "SCOPED_CREDENTIAL_TSQL"
  - "SCOPED_CREDENTIAL"
helpviewer_keywords:
  - "DATABASE SCOPED CREDENTIAL statement"
  - "credentials [SQL Server], DATABASE SCOPED CREDENTIAL statement"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=aps-pdw-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Creates a database credential. A database credential is not mapped to a server login or database user. The credential is used by the database to access to the external location anytime the database is performing an operation that requires access.

![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
CREATE DATABASE SCOPED CREDENTIAL credential_name
WITH IDENTITY = 'identity_name'
    [ , SECRET = 'secret' ]

```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *credential_name*

Specifies the name of the database scoped credential being created. *credential_name* cannot start with the number (#) sign. System credentials start with ##.

#### IDENTITY **='**_identity\_name_**'**

Specifies the name of the account to be used when connecting outside the server. 

- To import a file from Azure Blob Storage or Azure Data Lake Storage using a shared key, the identity name must be `SHARED ACCESS SIGNATURE`. For more information about shared access signatures, see [Using Shared Access Signatures (SAS)](/azure/storage/storage-dotnet-shared-access-signature-part-1).
- To import a file from Azure Blob Storage using a managed identity, the identity name must be `MANAGED IDENTITY`.
- To load data into Azure Synapse Analytics, any valid value can be used for identity. 
- When using Kerberos (Windows Active Directory or MIT KDC) do not use the domain name in the IDENTITY argument. It should just be the account name.
- When on SQL Server, if creating a database scoped credential with a Storage Access Key used as the SECRET, IDENTITY is ignored.

> [!IMPORTANT]
> The only PolyBase external data source that supports Kerberos authentication is Hadoop. All other external data sources (SQL Server, Oracle, Teradata, MongoDB, generic ODBC) only support Basic Authentication.

> [!NOTE]
> WITH IDENTITY is not required if the container in Azure Blob storage is enabled for anonymous access. For an example querying Azure Blob storage, see [Importing into a table from a file stored on Azure Blob storage](../functions/openrowset-transact-sql.md#j-importing-into-a-table-from-a-file-stored-on-azure-blob-storage).

#### SECRET **='**_secret_**'**

Specifies the secret required for outgoing authentication. `SECRET` is required to import a file from Azure Blob storage. To load from Azure Blob storage into Azure Synapse Analytics or Parallel Data Warehouse, the Secret must be the Azure Storage Key.

> [!WARNING]
> The SAS key value might begin with a '?' (question mark). When you use the SAS key, you must remove the leading '?'. Otherwise your efforts might be blocked.

## Remarks

A database scoped credential is a record that contains the authentication information that is required to connect to a resource outside [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Most credentials include a Windows user and password.

Before creating a database scoped credential, the database must have a master key to protect the credential. For more information, see [CREATE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-master-key-transact-sql.md).

When IDENTITY is a Windows user, the secret can be the password. The secret is encrypted using the service master key. If the service master key is regenerated, the secret is re-encrypted using the new service master key.

When granting permissions for a shared access signatures (SAS) for use with a PolyBase external table, select both **Container** and **Object** as allowed resource types. If not granted, you may receive error 16535 or 16561 when attempting to access the external table.


Information about database scoped credentials is visible in the [sys.database_scoped_credentials](../../relational-databases/system-catalog-views/sys-database-scoped-credentials-transact-sql.md) catalog view.

Here are some applications of database scoped credentials:

- [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] uses a database scoped credential to access non-public Azure Blob Storage or Kerberos-secured Hadoop clusters with PolyBase. To learn more, see [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md).

- [!INCLUDE[ssSDW_md](../../includes/sssdw-md.md)] uses a database scoped credential to access non-public Azure Blob Storage with PolyBase. To learn more, see [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md). For more information about Azure Synapse storage authentication, see [Use external tables with Synapse SQL](/azure/synapse-analytics/sql/develop-tables-external-tables).

- [!INCLUDE[ssSDS](../../includes/sssds-md.md)] uses database scoped credentials for its global query feature. This is the ability to query across multiple database shards.

- [!INCLUDE[ssSDS](../../includes/sssds-md.md)] uses database scoped credentials to write extended event files to Azure Blob Storage.

- [!INCLUDE[ssSDS](../../includes/sssds-md.md)] uses database scoped credentials for elastic pools. For more information, see [Tame explosive growth with elastic databases](/azure/azure-sql/database/elastic-pool-overview)

- [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) and [OPENROWSET](../../t-sql/functions/openrowset-transact-sql.md) use database scoped credentials to access data from Azure Blob Storage. For more information, see [Examples of Bulk Access to Data in Azure Blob Storage](../../relational-databases/import-export/examples-of-bulk-access-to-data-in-azure-blob-storage.md).

## Permissions

Requires **CONTROL** permission on the database.

## SQL Server 2022

Starting in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] a new type of connector was introduced, using REST-API calls replacing HADOOP.  For Azure Blob Storage and Azure Data Lake Gen 2 the only supported authentication method supported is `SHARED ACCESS SIGNATURE`.

Please refer to [create external data source](../../t-sql/statements/create-external-data-source-transact-sql.md) for more information.


## Examples

### A. Creating a database scoped credential for your application

The following example creates the database scoped credential called `AppCred`. The database scoped credential contains the Windows user `Mary5` and a password.

```sql
-- Create a db master key if one does not already exist, using your own password.
CREATE MASTER KEY ENCRYPTION BY PASSWORD='<EnterStrongPasswordHere>';

-- Create a database scoped credential.
CREATE DATABASE SCOPED CREDENTIAL AppCred WITH IDENTITY = 'Mary5',
    SECRET = '<EnterStrongPasswordHere>';
```

### B. Creating a database scoped credential for a shared access signature

The following example creates a database scoped credential that can be used to create an [external data source](../../t-sql/statements/create-external-data-source-transact-sql.md), which can do bulk operations, such as [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) and [OPENROWSET](../../t-sql/functions/openrowset-transact-sql.md). Shared Access Signatures cannot be used with PolyBase in SQL Server, APS or Azure Synapse Analytics.

```sql
-- Create a db master key if one does not already exist, using your own password.
CREATE MASTER KEY ENCRYPTION BY PASSWORD='<EnterStrongPasswordHere>';

-- Create a database scoped credential.
CREATE DATABASE SCOPED CREDENTIAL MyCredentials
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
SECRET = 'QLYMgmSXMklt%2FI1U6DcVrQixnlU5Sgbtk1qDRakUBGs%3D';
```

### C. Creating a database scoped credential for PolyBase Connectivity to Azure Data Lake Store

The following example creates a database scoped credential that can be used to create an [external data source](../../t-sql/statements/create-external-data-source-transact-sql.md), which can be used by PolyBase in [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)].

Azure Data Lake Store uses an Azure Active Directory Application for Service to Service Authentication.
Please [create an AAD application](/azure/data-lake-store/data-lake-store-authenticate-using-active-directory)  and document your client_id, OAuth_2.0_Token_EndPoint, and Key before you try to create a database scoped credential.

```sql
-- Create a db master key if one does not already exist, using your own password.
CREATE MASTER KEY ENCRYPTION BY PASSWORD='<EnterStrongPasswordHere>';

-- Create a database scoped credential.
CREATE DATABASE SCOPED CREDENTIAL ADL_User
WITH
    IDENTITY = '<client_id>@<OAuth_2.0_Token_EndPoint>',
    SECRET = '<key>'
;
```

## More information

- [Credentials &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/credentials-database-engine.md)
- [ALTER DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-scoped-credential-transact-sql.md)
- [DROP DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/drop-database-scoped-credential-transact-sql.md)
- [sys.database_scoped_credentials](../../relational-databases/system-catalog-views/sys-database-scoped-credentials-transact-sql.md)
- [CREATE CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-credential-transact-sql.md)
- [sys.credentials &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-credentials-transact-sql.md)

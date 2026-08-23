---
title: "CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)"
description: Creates a database credential used by the database to access to the external location, anytime the database is performing an operation that requires access.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest, wiassaf
ms.date: 03/27/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - sfi-ropc-blocked
  - ignite-2025
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
ai-usage: ai-assisted
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=aps-pdw-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

Creates a database credential. A database credential isn't mapped to a server login or database user. The database uses the credential to access the external resource, when it performs an operation that requires access.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
CREATE DATABASE SCOPED CREDENTIAL credential_name
WITH IDENTITY = 'identity_name'
    [ , SECRET = 'secret' ]
[ ; ]
```

## Arguments

#### *credential_name*

Specifies the name of the database scoped credential being created. *credential_name* can't start with the number (`#`) sign. System credentials start with `##`. The maximum length of *credential_name* is 128 characters.

#### IDENTITY = '*identity_name*'

Specifies the name of the account to use when connecting outside the server.

- To import a file from Azure Blob Storage or Azure Data Lake Storage using a shared key, the identity name must be `SHARED ACCESS SIGNATURE`. For more information about shared access signatures, see [Using Shared Access Signatures (SAS)](/azure/storage/storage-dotnet-shared-access-signature-part-1). Only use `IDENTITY = SHARED ACCESS SIGNATURE` for a shared access signature.

- To import a file from Azure Blob Storage using a managed identity, the identity name must be `MANAGED IDENTITY`.

- When using Kerberos (Windows Active Directory or MIT KDC), don't use the domain name in the `IDENTITY` argument. It should just be the account name.

- In a SQL Server instance, if you create a database scoped credential with a Storage Access Key used as the `SECRET`, `IDENTITY` is ignored.

- `WITH IDENTITY` isn't required if the container in Azure Blob storage is enabled for anonymous access. For an example querying Azure Blob storage with `OPENROWSET BULK`, see [Import into a table from a file stored on Azure Blob storage](../functions/openrowset-bulk-transact-sql.md#j-use-openrowset-to-access-several-delta-tables-from-azure-data-lake-gen2).

- In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, the REST-API connector replaces HADOOP. For Azure Blob Storage and Azure Data Lake Gen 2, the only supported authentication method is shared access signature. For more information, see [CREATE EXTERNAL DATA SOURCE](create-external-data-source-transact-sql.md).

- In [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)], the only PolyBase external data source that supports Kerberos authentication is Hadoop. All other external data sources (SQL Server, Oracle, Teradata, MongoDB, generic ODBC) only support Basic Authentication.

- SQL pools in Azure Synapse Analytics include the following notes:

  - To load data into Azure Synapse Analytics, you can use any valid value for `IDENTITY`.
  - In an Azure Synapse Analytics serverless SQL pool, database scoped credentials can specify a workspace managed identity, service principal name, or shared access signature (SAS) token. Access via a user identity, enabled by [User sign-in with Microsoft Entra pass-through authentication](/entra/identity/hybrid/connect/how-to-connect-pta), is also possible with a database scoped credential, as is anonymous access to publicly available storage. For more information, see [Supported storage authorization types](/azure/synapse-analytics/sql/develop-storage-files-storage-access-control?tabs=user-identity#supported-storage-authorization-types).
  - In an Azure Synapse Analytics dedicated SQL pool, database scoped credentials can specify shared access signature (SAS) token, custom application identity, workspace managed identity, or storage access key.

| **Authentication** | **T-SQL** | **Supported** | **Notes** |
| --- | --- | --- | --- |
| **Shared Access Signature (SAS)** | `CREATE DATABASE SCOPED CREDENTIAL <credential_name> WITH IDENTITY = 'SHARED ACCESS SIGNATURE', SECRET = 'secret';` | SQL Server 2022 and later, Azure SQL Managed Instance, Azure Synapse Analytics, Azure SQL Database | |
| **Managed Identity** | `CREATE DATABASE SCOPED CREDENTIAL <credential_name> WITH IDENTITY = 'MANAGED IDENTITY';` | Azure SQL Database, Azure SQL Managed Instance, SQL Server 2025 with Azure Arc | To enable Azure Arc, see [Managed identity for SQL Server enabled by Azure Arc](../../sql-server/azure-arc/managed-identity.md) |
| **Microsoft Entra pass-through authentication via User Identity** | `CREATE DATABASE SCOPED CREDENTIAL <credential_name> WITH IDENTITY = 'USER IDENTITY';` | [!INCLUDE [Azure SQL Database](../../includes/applies-to-version/_asdb.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)] <sup>1</sup> | In Azure Synapse, see [User sign-in with Microsoft Entra pass-through authentication](/entra/identity/hybrid/connect/how-to-connect-pta) |
| **S3 Access Key Basic authentication** | `CREATE DATABASE SCOPED CREDENTIAL <credential_name> WITH IDENTITY = 'S3 ACCESS KEY', SECRET = '<accesskey>:<secretkey>';` | SQL Server 2022 and later versions | |
| **ODBC Data sources or Kerberos (MIT KDC)** | `CREATE DATABASE SCOPED CREDENTIAL <credential_name> WITH IDENTITY = '<identity_name>', SECRET = '<secret>'`; | SQL Server 2019 and later versions | |

<sup>1</sup> In [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], if you don't specify a database-scoped credential, the authentication method defaults to 'USER IDENTITY' and uses the Microsoft Entra ID user account as context.

#### SECRET = '*secret*'

Specifies the secret required for outgoing authentication. `SECRET` is required to import a file from Azure Blob storage. To load from Azure Blob storage into Azure Synapse Analytics or Parallel Data Warehouse, the Secret must be the Azure Storage Key.

> [!WARNING]  
> The SAS key value might begin with a question mark (`?`). When you use the SAS key, remove the leading `?`.

## Remarks

A database scoped credential is a record that contains the authentication information that is required to connect to a resource outside [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Most credentials include a Windows user and password.

To protect the sensitive information inside the database scoped credential, a database master key (DMK) is required. The DMK is a symmetric key that encrypts the secret in the database scoped credential. The database must have a DMK before you can create any database scoped credentials. Encrypt the DMK with a strong password. Azure SQL Database creates a DMK with a strong, randomly selected password as part of creating the database scoped credential, or as part of creating a server audit.

Users can't create the DMK on a logical `master` database. The DMK password is unknown to Microsoft and not discoverable after creation. Create a DMK before creating a database scoped credential. For more information, see [CREATE MASTER KEY](create-master-key-transact-sql.md).

When `IDENTITY` is a Windows user, the secret can be the password. The secret is encrypted with the service master key (SMK). If you regenerate the SMK, the secret is re-encrypted with the new SMK.

When granting permissions for a shared access signature (SAS) for use with a PolyBase external table, select both **Container** and **Object** as allowed resource types. If you don't grant these permissions, you might receive error 16535 or 16561 when attempting to access the external table.

For information about database scoped credentials, see the [sys.database_scoped_credentials](../../relational-databases/system-catalog-views/sys-database-scoped-credentials-transact-sql.md) catalog view.

Here are some applications of database scoped credentials:

- [!INCLUDE [ssNoVersion_md](../../includes/ssnoversion-md.md)] uses a database scoped credential to access non-public Azure Blob Storage or Kerberos-secured Hadoop clusters with PolyBase. For more information, see [CREATE EXTERNAL DATA SOURCE](create-external-data-source-transact-sql.md).

- [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] uses a database scoped credential to access non-public Azure Blob Storage with PolyBase. For more information, see [CREATE EXTERNAL DATA SOURCE](create-external-data-source-transact-sql.md). For more information about Azure Synapse storage authentication, see [Use external tables with Synapse SQL](/azure/synapse-analytics/sql/develop-tables-external-tables).

- [!INCLUDE [ssSDS](../../includes/sssds-md.md)] uses database scoped credentials for its [elastic query](/azure/azure-sql/database/elastic-query-overview) feature, which allows queries across multiple database shards.

- [!INCLUDE [ssSDS](../../includes/sssds-md.md)] uses database scoped credentials to write extended event files to Azure Blob Storage.

- [!INCLUDE [ssSDS](../../includes/sssds-md.md)] uses database scoped credentials for elastic pools. For more information, see [Elastic pools help you manage and scale multiple databases in Azure SQL Database](/azure/azure-sql/database/elastic-pool-overview)

- [BULK INSERT](bulk-insert-transact-sql.md) and [OPENROWSET](../functions/openrowset-transact-sql.md) use database scoped credentials to access data from Azure Blob Storage. For more information, see [Examples of bulk access to data in Azure Blob Storage](../../relational-databases/import-export/examples-of-bulk-access-to-data-in-azure-blob-storage.md).

- Use database scoped credentials with [PolyBase](../../relational-databases/polybase/overview.md) and [Azure SQL Managed Instance data virtualization](/azure/azure-sql/managed-instance/data-virtualization-overview?view=azuresqlmi-current&preserve-view=true) features.

- For `BACKUP TO URL` and `RESTORE FROM URL`, use a server-level credential via [CREATE CREDENTIAL](create-credential-transact-sql.md) instead.

- Use database scoped credentials with [CREATE EXTERNAL MODEL](create-external-model-transact-sql.md#credential)

## Permissions

Requires `CONTROL` permission on the database.

## Examples

<a id="a-creating-a-database-scoped-credential-for-your-application"></a>

### A. Create a database scoped credential for your application

The following example creates the database scoped credential called `AppCred`. The database scoped credential contains the Windows user `Mary5` and a password.

Create a DMK if one doesn't already exist. [!INCLUDE [ssnotestrongpass-md](../../includes/ssnotestrongpass-md.md)]

```sql
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>';
```

Create a database scoped credential:

```sql
CREATE DATABASE SCOPED CREDENTIAL AppCred
WITH IDENTITY = 'Mary5',
     SECRET = '<password>';
```

<a id="b-creating-a-database-scoped-credential-for-a-shared-access-signature"></a>

### B. Create a database scoped credential for a shared access signature

The following example creates a database scoped credential that you can use to create an [external data source](create-external-data-source-transact-sql.md). This credential can do bulk operations, such as [BULK INSERT](bulk-insert-transact-sql.md) and [OPENROWSET BULK](../functions/openrowset-bulk-transact-sql.md).

Create a DMK if one doesn't already exist. [!INCLUDE [ssnotestrongpass-md](../../includes/ssnotestrongpass-md.md)]

```sql
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>';
```

Create a database scoped credential. Replace `<key>` with the appropriate value.

```sql
CREATE DATABASE SCOPED CREDENTIAL MyCredentials
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
     SECRET = '<key>';
```

### C. Create a database scoped credential for PolyBase connectivity to Azure Data Lake Store

The following example creates a database scoped credential that can be used to create an [external data source](create-external-data-source-transact-sql.md), which can be used by PolyBase in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)].

Azure Data Lake Store uses a Microsoft Entra application for service-to-service authentication.

[Create a Microsoft Entra application](/azure/data-lake-store/data-lake-store-authenticate-using-active-directory) and document your client_id, OAuth_2.0_Token_EndPoint, and Key before you try to create a database scoped credential.

Create a DMK if one doesn't already exist. [!INCLUDE [ssnotestrongpass-md](../../includes/ssnotestrongpass-md.md)]

```sql
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>';
```

Create a database scoped credential. Replace `<key>` with the appropriate value.

```sql
CREATE DATABASE SCOPED CREDENTIAL ADL_User
WITH IDENTITY = '<client_id>@<OAuth_2.0_Token_EndPoint>',
     SECRET = '<key>';
```

### D. Create a database scoped credential using Managed Identity

**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions

[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] introduces support for [Microsoft Entra managed identities](/entra/identity/managed-identities-azure-resources/overview). For information on how to use a managed identity with SQL Server enabled by Azure Arc, see [Managed identity for SQL Server enabled by Azure Arc](../../sql-server/azure-arc/managed-identity.md).

```sql
EXECUTE sp_configure 'allow server scoped db credentials', 1;
RECONFIGURE;
GO

CREATE DATABASE SCOPED CREDENTIAL [managed_id]
WITH IDENTITY = 'Managed Identity';
```

### E. Create a database scoped credential using Microsoft Entra ID

**Applies to**: [!INCLUDE [Azure SQL Database](../../includes/applies-to-version/_asdb.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]

In [!INCLUDE [Azure SQL Database](../../includes/applies-to-version/_asdb.md)] and [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], you can use your own Microsoft Entra ID account to authenticate an external data source.

In [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], if you don't specify a database-scoped credential, the authentication method defaults to `USER IDENTITY` and uses the Entra ID user account as context.

```sql
CREATE DATABASE SCOPED CREDENTIAL MyCredential
WITH IDENTITY = 'User Identity';
```

### F. Create a database scoped credential for cross-instance queries with integrated security

The following example creates a database scoped credential for [elastic queries](/azure/azure-sql/database/elastic-query-overview) or [PolyBase](../../relational-databases/polybase/polybase-guide.md) distributed queries between two SQL Server instances that use Windows (integrated) authentication.

When you use Kerberos authentication between SQL Server instances, don't include the domain name in the `IDENTITY` argument. Use only the username that the remote SQL Server instance recognizes.

```sql
-- On the local instance, create a DMK if one doesn't already exist
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<password>';
GO

-- Create a credential for the remote instance by using integrated security
CREATE DATABASE SCOPED CREDENTIAL RemoteSQLCredential
WITH IDENTITY = 'DomainUser',
     SECRET = '<password>';
GO

-- Create the external data source referencing the remote instance
CREATE EXTERNAL DATA SOURCE RemoteSQLServer
WITH (
    LOCATION = 'sqlserver://remote-server-name',
    CREDENTIAL = RemoteSQLCredential
);
```

> [!IMPORTANT]
> Cross-instance queries with integrated security (Kerberos) require that both SQL Server instances have properly configured Service Principal Names (SPNs) and that Kerberos constrained delegation is enabled in your Active Directory environment. For more information, see [Register a Service Principal Name for Kerberos connections](../../database-engine/configure-windows/register-a-service-principal-name-for-kerberos-connections.md).

## Related content

- [Credentials (Database Engine)](../../relational-databases/security/authentication-access/credentials-database-engine.md)
- [ALTER DATABASE SCOPED CREDENTIAL (Transact-SQL)](alter-database-scoped-credential-transact-sql.md)
- [DROP DATABASE SCOPED CREDENTIAL (Transact-SQL)](drop-database-scoped-credential-transact-sql.md)
- [sys.database_scoped_credentials (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-scoped-credentials-transact-sql.md)
- [CREATE CREDENTIAL (Transact-SQL)](create-credential-transact-sql.md)
- [sys.credentials (Transact-SQL)](../../relational-databases/system-catalog-views/sys-credentials-transact-sql.md)

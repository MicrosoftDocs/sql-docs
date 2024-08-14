---
title: "CREATE CREDENTIAL (Transact-SQL)"
description: CREATE CREDENTIAL (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.reviewer: wiassaf
ms.date: 02/02/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CREDENTIAL_TSQL"
  - "SQL13.SWB.CREDENTIAL.GENERAL.F1"
  - "CREATE CREDENTIAL"
  - "CREATE_CREDENTIAL_TSQL"
  - "CREDENTIAL"
helpviewer_keywords:
  - "SECRET clause"
  - "authentication [SQL Server], credentials"
  - "CREATE CREDENTIAL statement"
  - "credentials [SQL Server], CREATE CREDENTIAL statement"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016||>=sql-server-linux-2017"
---
# CREATE CREDENTIAL (Transact-SQL)

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

Creates a server-level credential. A credential is a record that contains the authentication information that is required to connect to a resource outside SQL Server. Most credentials include a Windows user and password. For example, saving a database backup to some location might require SQL Server to provide special credentials to access that location. For more information, see [Credentials (Database Engine)](../../relational-databases/security/authentication-access/credentials-database-engine.md).

> [!NOTE]
> To make the credential at the database-level use [CREATE DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-database-scoped-credential-transact-sql.md).
> Create a server-level credential with `CREATE CREDENTIAL` when you need to use the same credential for multiple databases on the server.
>
> - Create a database scoped credential with `CREATE DATABASE SCOPED CREDENTIAL` to make the database more portable. When a database is moved to a new server, the database scoped credential will move with it.
> - Use database scoped credentials on [!INCLUDE[ssSDS](../../includes/sssds-md.md)].
> - Use database scoped credentials with [PolyBase](../../relational-databases/polybase/polybase-guide.md) and [Azure SQL Managed Instance data virtualization](/azure/azure-sql/managed-instance/data-virtualization-overview?view=azuresqlmi-current&preserve-view=true) features.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
CREATE CREDENTIAL credential_name
WITH IDENTITY = 'identity_name'
    [ , SECRET = 'secret' ]
        [ FOR CRYPTOGRAPHIC PROVIDER cryptographic_provider_name ]
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *credential_name*
Specifies the name of the credential being created. *credential_name* cannot start with the number (#) sign. System credentials start with ##.

> [!IMPORTANT]
> When using a shared access signature (SAS), this name must match the container path, start with https and must not contain a forward slash. See [example D](#d-creating-a-credential-using-a-sas-token).

When used for backup/restore using a to external data platforms, such as Azure Blob Storage or S3-compatible platforms, the following table provides common paths:

| External Data Source    | Location path                                         |  Example |
| ----------------------- | --------------- | ----------------------------------------------------- |  :-- |  :-- |
| Azure Blob Storage (V2) | `https://<mystorageaccountname>.blob.core.windows.net/<mystorageaccountcontainername>` |  [Example D.](#d-creating-a-credential-using-a-sas-token) |
| S3-compatible object storage | - S3-compatible storage: `s3://<server_name>:<port>/`<br />- AWS S3: `s3://<bucket_name>.S3.<region>.amazonaws.com[:port]/<folder>` </br>or `s3://s3.<region>.amazonaws.com[:port]/<bucket_name>/<folder>` | [Example F.](#f-create-a-credential-for-backuprestore-to-s3-compatible-storage) |

#### IDENTITY **='**_identity\_name_**'**

Specifies the name of the account to be used when connecting outside the server. When the credential is used to access the Azure Key Vault, the **IDENTITY** is the name of the key vault. See example C below. When the credential is using a shared access signature (SAS), the **IDENTITY** is *SHARED ACCESS SIGNATURE*. See example D below.

> [!IMPORTANT]
> Azure SQL Database only supports Azure Key Vault and Shared Access Signature identities. Windows user identities are not supported.

#### SECRET **='**_secret_**'**

Specifies the secret required for outgoing authentication.

When the credential is used to access Azure Key Vault, the **SECRET** argument must be formatted as a service principal's *\<client ID>* (without hyphens) and *\<secret>*, passed together without a space between them. See example C below. When the credential is using a shared access signature, the **SECRET** is the shared access signature token. See example D below. For information about creating a stored access policy and a shared access signature on an Azure container, see [Lesson 1: Create a stored access policy and a shared access signature on an Azure container](../../relational-databases/tutorial-use-azure-blob-storage-service-with-sql-server-2016.md#1---create-stored-access-policy-and-shared-access-storage).

#### FOR CRYPTOGRAPHIC PROVIDER *cryptographic_provider_name*

 Specifies the name of an *Enterprise Key Management Provider (EKM)*. For more information about Key Management, see [Extensible Key Management &#40;EKM&#41;](../../relational-databases/security/encryption/extensible-key-management-ekm.md).

## Remarks

When IDENTITY is a Windows user, the secret can be the password. The secret is encrypted using the service master key. If the service master key is regenerated, the secret is re-encrypted using the new service master key.

After creating a credential, you can map it to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login by using [CREATE LOGIN](../../t-sql/statements/create-login-transact-sql.md) or [ALTER LOGIN](../../t-sql/statements/alter-login-transact-sql.md). A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login can be mapped to only one credential, but a single credential can be mapped to multiple [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. For more information, see [Credentials &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/credentials-database-engine.md). A server-level credential can only be mapped to a login, not to a database user. 

Information about credentials is visible in the [sys.credentials](../../relational-databases/system-catalog-views/sys-credentials-transact-sql.md) catalog view.

If there is no login mapped credential for the provider, the credential mapped to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account is used.

A login can have multiple credentials mapped to it as long as they are used with distinctive providers. There must be only one mapped credential per provider per login. The same credential can be mapped to other logins.

## Permissions

Requires **ALTER ANY CREDENTIAL** permission.

## Examples

### A. Creating a Credential for Windows Identity

The following example creates the credential called `AlterEgo`. The credential contains the Windows user `Mary5` and a password.

```sql
CREATE CREDENTIAL AlterEgo WITH IDENTITY = 'Mary5',
    SECRET = '<EnterStrongPasswordHere>';
GO
```

### B. Creating a Credential for EKM

The following example uses a previously created account called `User1OnEKM` on an EKM module through the EKM's Management tools, with a basic account type and password. The **sysadmin** account on the server creates a credential that is used to connect to the EKM account, and assigns it to the `User1` [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] account:

```sql
CREATE CREDENTIAL CredentialForEKM
    WITH IDENTITY='User1OnEKM', SECRET='<EnterStrongPasswordHere>'
    FOR CRYPTOGRAPHIC PROVIDER MyEKMProvider;
GO

/* Modify the login to assign the cryptographic provider credential */
ALTER LOGIN User1
ADD CREDENTIAL CredentialForEKM;
```

### C. Creating a Credential for EKM Using the Azure Key Vault

The following example creates a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] credential for the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to use when accessing the Azure Key Vault using the **SQL Server Connector for Microsoft Azure Key Vault**. For a complete example of using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Connector, see [Extensible Key Management Using Azure Key Vault &#40;SQL Server&#41;](../../relational-databases/security/encryption/extensible-key-management-using-azure-key-vault-sql-server.md).

> [!IMPORTANT]
> The **IDENTITY** argument of **CREATE CREDENTIAL** requires the key vault name. The **SECRET** argument of **CREATE CREDENTIAL** requires the *\<Client ID>* (without hyphens) and *\<Secret>* to be passed together without a space between them.

In the following example, the **Client ID** (`11111111-2222-3333-4444-555555555555`) is stripped of the hyphens and entered as the string `11111111222233334444555555555555` and the **Secret** is represented by the string `SECRET_DBEngine`.

```sql
USE master;
CREATE CREDENTIAL Azure_EKM_TDE_cred
    WITH IDENTITY = 'ContosoKeyVault',
    SECRET = '11111111222233334444555555555555SECRET_DBEngine'
    FOR CRYPTOGRAPHIC PROVIDER AzureKeyVault_EKM_Prov ;
```

The following example creates the same credential by using variables for the **Client ID** and **Secret** strings, which are then concatenated together to form the **SECRET** argument. The **REPLACE** function is used to remove the hyphens from the Client ID.

```sql
DECLARE @AuthClientId uniqueidentifier = '11111111-AAAA-BBBB-2222-CCCCCCCCCCCC';
DECLARE @AuthClientSecret varchar(200) = 'SECRET_DBEngine';
DECLARE @pwd varchar(max) = REPLACE(CONVERT(varchar(36), @AuthClientId) , '-', '') + @AuthClientSecret;

EXEC ('CREATE CREDENTIAL Azure_EKM_TDE_cred
    WITH IDENTITY = ''ContosoKeyVault'', SECRET = ''' + @PWD + '''
    FOR CRYPTOGRAPHIC PROVIDER AzureKeyVault_EKM_Prov ;');
```

### D. Creating a credential using a SAS Token

**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [current version](/troubleshoot/sql/general/determine-version-edition-update-level) and Azure SQL Managed Instance.

The following example creates a shared access signature credential using a SAS token. For a tutorial on creating a stored access policy and a shared access signature on an Azure container, and then creating a credential using the shared access signature, see [Tutorial: Use Microsoft Azure Blob Storage with SQL Server databases](../../relational-databases/tutorial-use-azure-blob-storage-service-with-sql-server-2016.md).

> [!IMPORTANT]
> THE **CREDENTIAL NAME** argument requires that the name match the container path, start with https and not contain a trailing forward slash. The **IDENTITY** argument requires the name, *SHARED ACCESS SIGNATURE*. The **SECRET** argument requires the shared access signature token.
>
> The **SHARED ACCESS SIGNATURE secret** should not have the leading **?**.

```sql
USE master
CREATE CREDENTIAL [https://<mystorageaccountname>.blob.core.windows.net/<mystorageaccountcontainername>] -- this name must match the container path, start with https and must not contain a trailing forward slash.
    WITH IDENTITY='SHARED ACCESS SIGNATURE' -- this is a mandatory string and do not change it.
    , SECRET = 'sharedaccesssignature' -- this is the shared access signature token
GO
```

### E. Creating a credential for Managed Identity

The following example creates the credential that represent Managed Identity of Azure SQL or Azure Synapse service. Password and secret are not applicable in this case.

```sql
CREATE CREDENTIAL ServiceIdentity WITH IDENTITY = 'Managed Identity';
GO
```

### F. Create a credential for backup/restore to S3-compatible storage

**Applies to**: [!INCLUDE[ssSQL22](../../includes/sssql22-md.md)] and later versions

The open S3-compatible standard provides for storage paths and details that may differ based on the storage platform. For more information, see [SQL Server backup to URL for S3-compatible object storage](../../relational-databases/backup-restore/sql-server-backup-to-url-s3-compatible-object-storage.md).

For most S3-compatible storage, this example creates a server level credential and performs a `BACKUP TO URL`.

<!-- See similar examples in docs\relational-databases\backup-restore\sql-server-backup-to-url-s3-compatible-object-storage.md -->

```sql
USE [master];
CREATE CREDENTIAL [s3://<endpoint>:<port>/<bucket>]
WITH
        IDENTITY    = 'S3 Access Key',
        SECRET      = '<AccessKeyID>:<SecretKeyID>';
GO

BACKUP DATABASE [SQLTestDB]
TO      URL = 's3://<endpoint>:<port>/<bucket>/SQLTestDB.bak'
WITH    FORMAT /* overwrite any existing backup sets */
,       STATS = 10
,       COMPRESSION;
```

However, AWS S3 supports two different standards of URL.

- `S3://<BUCKET_NAME>.S3.<REGION>.AMAZONAWS.COM/<FOLDER>` (default)
- `S3://S3.<REGION>.AMAZONAWS.COM/<BUCKET_NAME>/<FOLDER>`

There are multiple approaches to successfully creating a credential for AWS S3:

- Provide the bucket name and path and region in the credential name.

    ```sql
    -- S3 bucket name: datavirtualizationsample
    -- S3 bucket region: us-west-2
    -- S3 bucket folder: backup
    
    CREATE CREDENTIAL [s3://datavirtualizationsample.s3.us-west-2.amazonaws.com/backup]
    WITH    
            IDENTITY    = 'S3 Access Key'
    ,       SECRET      = 'accesskey:secretkey';
    GO
    
    BACKUP DATABASE [AdventureWorks2022]
    TO URL  = 's3://datavirtualizationsample.s3.us-west-2.amazonaws.com/backup/AdventureWorks2022.bak'
    WITH COMPRESSION, FORMAT, MAXTRANSFERSIZE = 20971520;
    GO
    ```

    Or,

    ```sql
    CREATE CREDENTIAL [s3://s3.us-west-2.amazonaws.com/datavirtualizationsample/backup]
    WITH    
            IDENTITY    = 'S3 Access Key'
    ,       SECRET      = 'accesskey:secretkey';
    GO
    
    BACKUP DATABASE [AdventureWorks2022]
    TO URL  = 's3://s3.us-west-2.amazonaws.com/datavirtualizationsample/backup/AdventureWorks2022.bak'
    WITH COMPRESSION, FORMAT, MAXTRANSFERSIZE = 20971520;
    GO
    ```

- Or, provide the bucket name and path in the credential name, but parameterize the region within each `BACKUP`/`RESTORE` command. Use the S3-specific region string in the `BACKUP_OPTIONS` and `RESTORE_OPTIONS`, for example, `'{"s3": {"region":"us-west-2"}}'`.

    ```sql
    -- S3 bucket name: datavirtualizationsample
    -- S3 bucket region: us-west-2
    -- S3 bucket folder: backup

    CREATE CREDENTIAL   [s3://datavirtualizationsample.s3.amazonaws.com/backup]
    WITH    
            IDENTITY    = 'S3 Access Key'
    ,       SECRET      = 'accesskey:secretkey';
    GO
    
    BACKUP DATABASE [AdventureWorks2022]
    TO URL  = 's3://datavirtualizationsample.s3.amazonaws.com/backup/AdventureWorks2022.bak'
    WITH
      BACKUP_OPTIONS = '{"s3": {"region":"us-west-2"}}' -- REGION AS PARAMETER)
    , COMPRESSION, FORMAT, MAXTRANSFERSIZE = 20971520;
    GO

    RESTORE DATABASE AdventureWorks2022_1 
    FROM URL = 's3://datavirtualizationsample.s3.amazonaws.com/backup/AdventureWorks2022.bak'
    WITH 
      MOVE 'AdventureWorks2022' 
      TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\AdventureWorks2022_1.mdf'
    , MOVE 'AdventureWorks2022_log' 
      TO 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\AdventureWorks2022_1.ldf'
    , STATS = 10, RECOVERY
    , REPLACE, RESTORE_OPTIONS = '{"s3": {"region":"us-west-2"}}'; -- REGION AS PARAMETER)
    GO
    ```

## Related content

- [Credentials &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/credentials-database-engine.md)
- [ALTER CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-credential-transact-sql.md)
- [DROP CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/drop-credential-transact-sql.md)
- [CREATE DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)
- [CREATE LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/create-login-transact-sql.md)
- [ALTER LOGIN &#40;Transact-SQL&#41;](../../t-sql/statements/alter-login-transact-sql.md)
- [sys.credentials &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-credentials-transact-sql.md)
- [Lesson 2: Create a SQL Server credential using a shared access signature](../../relational-databases/tutorial-use-azure-blob-storage-service-with-sql-server-2016.md#2---create-a-sql-server-credential-using-a-shared-access-signature)
- [Shared Access Signatures](/azure/storage/common/storage-sas-overview)

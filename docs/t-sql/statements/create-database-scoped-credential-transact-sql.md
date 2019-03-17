---
title: "CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "02/28/2018"
ms.prod: sql
ms.prod_service: "sql-data-warehouse, database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
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
ms.assetid: fe830577-11ca-44e5-953b-2d589d54d045
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=aps-pdw-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Creates a database credential. A database credential is not mapped to a server login or database user. The credential is used by the database to access to the external location anytime the database is performing an operation that requires access.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
 
CREATE DATABASE SCOPED CREDENTIAL credential_name   
WITH IDENTITY = 'identity_name'  
    [ , SECRET = 'secret' ]  
  
```  
  
## Arguments  
 *credential_name*  
 Specifies the name of the database scoped credential being created. *credential_name* cannot start with the number (#) sign. System credentials start with ##.  
  
 IDENTITY **='**_identity\_name_**'**  
 Specifies the name of the account to be used when connecting outside the server. To import a file from Azure Blob storage using share key, the identity name must be `SHARED ACCESS SIGNATURE`. To load data into SQL DW, any valid value can be used for identity. For more information about shared access signatures, see [Using Shared Access Signatures (SAS)](https://docs.microsoft.com/azure/storage/storage-dotnet-shared-access-signature-part-1).  
  
 SECRET **='**_secret_**'**  
 Specifies the secret required for outgoing authentication. `SECRET` is required to import a file from Azure Blob storage. To load from Azure Blob storage into SQL DW or Parallel Data Warehouse, the Secret must be the Azure Storage Key.  
> [!WARNING]
>  The SAS key value might begin with a '?' (question mark). When you use the SAS key, you must remove the leading '?'. Otherwise your efforts might be blocked.  
  
## Remarks  
 A database scoped credential is a record that contains the authentication information that is required to connect to a resource outside [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Most credentials include a Windows user and password.  
  
 Before creating a database scoped credential, the database must have a master key to protect the credential. For more information, see [CREATE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-master-key-transact-sql.md).  
  
 When IDENTITY is a Windows user, the secret can be the password. The secret is encrypted using the service master key. If the service master key is regenerated, the secret is re-encrypted using the new service master key.  
   
 Information about database scoped credentials is visible in the [sys.database_scoped_credentials](../../relational-databases/system-catalog-views/sys-database-scoped-credentials-transact-sql.md) catalog view.  
  
 
 Hereare some applications of database scoped credentials:  
  
- [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] uses a database scoped credential to access non-public Azure blob storage or Kerberos-secured Hadoop clusters with PolyBase. To learn more, see [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md).  

- [!INCLUDE[ssSDW_md](../../includes/sssdw-md.md)] uses a database scoped credential to access non-public Azure blob storage with PolyBase. To learn more, see [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md).
  
- [!INCLUDE[ssSDS](../../includes/sssds-md.md)] uses database scoped credentials for its global query feature. This is the ability to query across multiple database shards.  
  
- [!INCLUDE[ssSDS](../../includes/sssds-md.md)] uses database scoped credentials to write extended event files to Azure blob storage.  
  
- [!INCLUDE[ssSDS](../../includes/sssds-md.md)] uses database scoped credentials for elastic pools. For more information, see [Tame explosive growth with elastic databases](https://azure.microsoft.com/documentation/articles/sql-database-elastic-pool/)  

- [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) and [OPENROWSET](../../t-sql/functions/openrowset-transact-sql.md) use database scoped credentials to access data from Azure blob storage. For more information, see [Examples of Bulk Access to Data in Azure Blob Storage](../../relational-databases/import-export/examples-of-bulk-access-to-data-in-azure-blob-storage.md). 
  
## Permissions  
 Requires **CONTROL** permission on the database.  
  
## Examples  
### A. Creating a database scoped credential for your application.
 The following example creates the database scoped credential called `AppCred`. The database scoped credential contains the Windows user `Mary5` and a password.  
  
```sql  
-- Create a db master key if one does not already exist, using your own password.  
CREATE MASTER KEY ENCRYPTION BY PASSWORD='<EnterStrongPasswordHere>';  
  
-- Create a database scoped credential.  
CREATE DATABASE SCOPED CREDENTIAL AppCred WITH IDENTITY = 'Mary5',   
    SECRET = '<EnterStrongPasswordHere>';  
GO  
```  

### B. Creating a database scoped credential for a shared access signature.   
The following example creates a database scoped credential that can be used to create an [external data source](../../t-sql/statements/create-external-data-source-transact-sql.md), which can do bulk operations, such as [BULK INSERT](../../t-sql/statements/bulk-insert-transact-sql.md) and [OPENROWSET](../../t-sql/functions/openrowset-transact-sql.md). Shared Access Signatures cannot be used with PolyBase in SQL Server, APS or SQL DW.
```sql
CREATE DATABASE SCOPED CREDENTIAL MyCredentials  
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
SECRET = 'QLYMgmSXMklt%2FI1U6DcVrQixnlU5Sgbtk1qDRakUBGs%3D';
```
  
### C. Creating a database scoped credential for PolyBase Connectivity to Azure Data Lake Store.  
The following example creates a database scoped credential that can be used to create an [external data source](../../t-sql/statements/create-external-data-source-transact-sql.md), which can be used by PolyBase in Azure SQL Data Warehouse.

Azure Data Lake Store uses an Azure Active Directory Application for Service to Service Authentication.
Please [create an AAD application](https://docs.microsoft.com/azure/data-lake-store/data-lake-store-authenticate-using-active-directory)  and document your client_id, OAuth_2.0_Token_EndPoint, and Key before you try to create a database scoped credential.

```sql
CREATE DATABASE SCOPED CREDENTIAL ADL_User
WITH
    IDENTITY = '<client_id>@\<OAuth_2.0_Token_EndPoint>'
    SECRET = '<key>'
;
```  
  
  
  
## More information  
 [Credentials &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/credentials-database-engine.md)   
 [ALTER DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-scoped-credential-transact-sql.md)   
 [DROP DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/drop-database-scoped-credential-transact-sql.md)   
 [sys.database_scoped_credentials](../../relational-databases/system-catalog-views/sys-database-scoped-credentials-transact-sql.md)   
 [CREATE CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-credential-transact-sql.md)   
 [sys.credentials &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-credentials-transact-sql.md)  
  
  

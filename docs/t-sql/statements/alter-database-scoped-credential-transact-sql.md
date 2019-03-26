---
title: "ALTER DATABASE SCOPED CREDENTIAL (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "02/27/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER DATABASE SCOPED CREDENTIAL"
  - "ALTER_DATABASE_SCOPED_CREDENTIAL_TSQL"
helpviewer_keywords: 
  - "ALTER DATABASE SCOPED CREDENTIAL statement"
  - "credentials [SQL Server], ALTER DATABASE SCOPED CREDENTIAL statement"
ms.assetid: 966b75b5-ca87-4203-8bf9-95c4e00cb0b5
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: "= azuresqldb-current || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# ALTER DATABASE SCOPED CREDENTIAL (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Changes the properties of a database scoped credential.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
ALTER DATABASE SCOPED CREDENTIAL credential_name WITH IDENTITY = 'identity_name'  
    [ , SECRET = 'secret' ]  
```  
  
## Arguments  
 *credential_name*  
 Specifies the name of the database scoped credential that is being altered.  
  
 IDENTITY **='***identity_name***'**  
 Specifies the name of the account to be used when connecting outside the server. To import a file from Azure Blob storage, the identity name must be `SHARED ACCESS SIGNATURE`.  For more information about shared access signatures, see [Using Shared Access Signatures (SAS)](https://docs.microsoft.com/azure/storage/storage-dotnet-shared-access-signature-part-1).  
    
  
 SECRET **='***secret***'**  
 Specifies the secret required for outgoing authentication. *secret* is required to import a file from Azure Blob storage. *secret* may be optional for other purposes.   
> [!WARNING]
>  The SAS key value might begin with a '?' (question mark). When you use the SAS key, you must remove the leading '?'. Otherwise your efforts might be blocked.    
  
## Remarks  
 When a database scoped credential is changed, the values of both *identity_name* and *secret* are reset. If the optional SECRET argument is not specified, the value of the stored secret will be set to NULL.  
  
 The secret is encrypted by using the service master key. If the service master key is regenerated, the secret is reencrypted by using the new service master key.  
  
 Information about database scoped credentials is visible in the [sys.database_scoped_credentials](../../relational-databases/system-catalog-views/sys-database-scoped-credentials-transact-sql.md) catalog view.  
  
## Permissions  
 Requires `ALTER` permission on the credential.  
  
## Examples  
  
### A. Changing the password of a database scoped credential  
 The following example changes the secret stored in a database scoped credential called `Saddles`. The database scoped credential contains the Windows login `RettigB` and its password. The new password is added to the database scoped credential using the SECRET clause.  
  
```  
ALTER DATABASE SCOPED CREDENTIAL AppCred WITH IDENTITY = 'RettigB',   
    SECRET = 'sdrlk8$40-dksli87nNN8';  
GO  
```  
  
### B. Removing the password from a credential  
 The following example removes the password from a database scoped credential named `Frames`. The database scoped credential contains Windows login `Aboulrus8` and a password. After the statement is executed, the database scoped credential will have a NULL password because the SECRET option is not specified.  
  
```  
ALTER DATABASE SCOPED CREDENTIAL Frames WITH IDENTITY = 'Aboulrus8';  
GO  
```  
  
## See Also  
 [Credentials &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/credentials-database-engine.md)   
 [CREATE DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)   
 [DROP DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/drop-database-scoped-credential-transact-sql.md)   
 [sys.database_scoped_credentials](../../relational-databases/system-catalog-views/sys-database-scoped-credentials-transact-sql.md)   
 [CREATE CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-credential-transact-sql.md)   
 [sys.credentials &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-credentials-transact-sql.md)  
  
  

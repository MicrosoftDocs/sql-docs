---
title: "Remote Blob Store (RBS) (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "11/03/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: filestream
ms.topic: conceptual
helpviewer_keywords: 
  - "Remote Blob Store (RBS) [SQL Server]"
  - "RBS (Remote Blob Store) [SQL Server]"
ms.assetid: 31c947cf-53e9-4ff4-939b-4c1d034ea5b1
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Remote Blob Store (RBS) (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Remote BLOB Store (RBS) is an optional add-on component that lets database administrators store binary large objects in commodity storage solutions instead of directly on the main database server.  
  
 RBS is included on the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] installation media, but is not installed by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup program.  
  
 
  
## Why RBS?  
  
### Optimized database storage and performance  
 Storing BLOBs in the database can consume large amounts of file space and expensive server resources. RBS transfers the BLOBs to a dedicated storage solution you choose and stores references to the BLOBs in the database. This frees server storage for structured data, and frees server resources for database operations.  
  
### Efficient BLOB management  
 Several RBS features support stored BLOBs management:  
  
-   BLOBS are managed with ACID (atomic, consistent, isolatable, durable) transactions.  
  
-   BLOBs are organized into collections.  
  
-   Garbage collection, consistency checking, and other maintenance functions are included.  
  
### Standardized API  
 RBS defines a set of APIs that provide a standardized programming model for applications to access and modify any BLOB store. Each BLOB store can specify its own provider library which plugs into the RBS client library and specifies how BLOBs are stored and accessed.  
  
 A number of third-party storage solution vendors have developed RBS providers that conform to these standard APIs and support BLOB storage on various storage platforms.  
  
## RBS Requirements  
 - RBS requires [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Enterprise for the main database server in which the BLOB metadata is stored.  However, if you use the supplied FILESTREAM provider, you can store the BLOBs themselves on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Standard. To connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], RBS requires at least ODBC driver version 11 for [!INCLUDE[ssSQL14_md](../../includes/sssql14-md.md)] and ODBC Driver version 13 for [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)]. Drivers are available at [Download ODBC Driver for SQL Server](https://msdn.microsoft.com/library/mt703139.aspx).    
  
 RBS includes a FILESTREAM provider that lets you use RBS to store BLOBs on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you want use RBS to store BLOBs in a different storage solution, you have to use a third party RBS provider developed for that storage solution, or develop a custom RBS provider using the RBS API. A sample provider that stores BLOBs in the NTFS file system is available as a learning resource on [Codeplex](https://go.microsoft.com/fwlink/?LinkId=210190).  
  
## RBS Security  
 The SQL Remote Blob Storage Team Blog is a good source of information about this feature. The RBS security model is described in the post at [RBS Security Model](https://blogs.msdn.com/b/sqlrbs/archive/2010/08/05/rbs-security-model.aspx).  
  
### Custom providers  
 When you use a custom provider to store BLOBs outside of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], make sure that you protect the stored BLOBs with permissions and encryption options that are appropriate to the storage medium used by the custom provider.  
  
### Credential store symmetric key  
 If a provider requires the setup and use of a secret stored within the credential store, RBS uses a symmetric key to encrypt the  provider secrets which a client may use to gain authorization to the provider's blob store.  
  
-   RBS 2016 uses an **AES_128** symmetric key. [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] does not allow the creation of new **TRIPLE_DES** keys except for backwards compatibility reasons. For more information, see [CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-symmetric-key-transact-sql.md).  
  
-   RBS 2014 and prior versions use a credential store which holds secrets encrypted using the **TRIPLE_DES** symmetric key algorithm which is outdated. If you are currently using **TRIPLE_DES**[!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you enhance your security by following the steps in this topic to rotate your key to a stronger encryption method.  
  
 You can determine the RBS credential store symmetric key properties by executing the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement in the RBS database:   
`SELECT * FROM sys.symmetric_keys WHERE name = 'mssqlrbs_encryption_skey';` If the output from that statement shows that **TRIPLE_DES** is still used, then you should rotate this key.  
  
### Rotating the symmetric key  
 When using RBS, you should periodically rotate the credential store symmetric key. This is a common security best practice to meet organizational security policies.  One way to rotate the RBS credential store symmetric key, is to use the [script below](#Key_rotation) in the RBS database.  You can also use this script to migrate to stronger encryption strength properties, such as algorithm or key length. Backup your database prior to key rotation.  At the script's conclusion, it has some verification steps.  
If your security policies require different key properties (e.g., algorithm or key length) from the ones provided, then the script may be used as a template. Modify the key properties in two places: 1) the creation of the temporary key 2) the creation of the permanent key.  
  
##  <a name="rbsresources"></a> RBS resources  
  
 **RBS samples**  
 The RBS samples available on [Codeplex](https://go.microsoft.com/fwlink/?LinkId=210190) demonstrate how to develop an RBS application, and how to develop and install a custom RBS provider.  
  
 **RBS blog**  
 The [RBS blog](https://go.microsoft.com/fwlink/?LinkId=210315) provides additional information to help you understand, deploy, and maintain RBS.  
  
##  <a name="Key_rotation"></a> Key rotation script  
 This example creates a stored procedure named `sp_rotate_rbs_symmetric_credential_key` to replace the currently used RBS credential store symmetric key  
with one of your choosing.  You may want to do this if there is a security policy requiring   
periodic key rotation or if there are specific algorithm requirements.  
 In this stored procedure, a symmetric key using **AES_256** will replace the current one.  As a result of  
the symmetric key replacement, secrets need to be re-encrypted with the new key.  This stored   
procedure will also re-encrypt the secrets.  The database should be backed up prior to key rotation.  
  
```  
CREATE PROC sp_rotate_rbs_symmetric_credential_key  
AS  
BEGIN  
BEGIN TRANSACTION;  
BEGIN TRY  
CLOSE ALL SYMMETRIC KEYS;  
  
/* Prove that all secrets can be re-encrypted, by creating a   
temporary key (#mssqlrbs_encryption_skey) and create a   
temp table (#myTable) to hold the re-encrypted secrets.    
Check to see if all re-encryption worked before moving on.*/  
  
CREATE TABLE #myTable(sql_user_sid VARBINARY(85) NOT NULL,  
    blob_store_id SMALLINT NOT NULL,  
    credential_name NVARCHAR(256) COLLATE Latin1_General_BIN2 NOT NULL,  
    old_secret VARBINARY(MAX), -- holds secrets while existing symmetric key is deleted  
    credential_secret VARBINARY(MAX)); -- holds secrets with the new permanent symmetric key  
  
/* Create a new temporary symmetric key with which the credential store secrets   
can be re-encrypted. These will be used once the existing symmetric key is deleted.*/  
CREATE SYMMETRIC KEY #mssqlrbs_encryption_skey    
    WITH ALGORITHM = AES_256 ENCRYPTION BY   
    CERTIFICATE [cert_mssqlrbs_encryption];  
  
OPEN SYMMETRIC KEY #mssqlrbs_encryption_skey    
    DECRYPTION BY CERTIFICATE [cert_mssqlrbs_encryption];  
  
INSERT INTO #myTable   
    SELECT cred_store.sql_user_sid, cred_store.blob_store_id, cred_store.credential_name,   
    encryptbykey(  
        key_guid('#mssqlrbs_encryption_skey'),   
        decryptbykeyautocert(cert_id('cert_mssqlrbs_encryption'),   
            NULL, cred_store.credential_secret)  
        ),   
    NULL  
    FROM [mssqlrbs_resources].[rbs_internal_blob_store_credentials] AS cred_store;  
  
IF( EXISTS(SELECT * FROM #myTable WHERE old_secret IS NULL))  
BEGIN  
    PRINT 'Abort. Failed to read some values';  
    SELECT * FROM #myTable;  
    ROLLBACK;  
END;  
ELSE  
BEGIN  
/* Re-encryption worked, so go ahead and drop the existing RBS credential store   
 symmetric key and replace it with a new symmetric key.*/  
DROP SYMMETRIC KEY [mssqlrbs_encryption_skey];  
  
CREATE SYMMETRIC KEY [mssqlrbs_encryption_skey]   
WITH ALGORITHM = AES_256   
ENCRYPTION BY CERTIFICATE [cert_mssqlrbs_encryption];  
  
OPEN SYMMETRIC KEY [mssqlrbs_encryption_skey]   
DECRYPTION BY CERTIFICATE [cert_mssqlrbs_encryption];  
  
/*Re-encrypt using the new permanent symmetric key.    
Verify if encryption provided a result*/  
UPDATE #myTable   
SET [credential_secret] =   
    encryptbykey(key_guid('mssqlrbs_encryption_skey'), decryptbykey(old_secret))  
  
IF( EXISTS(SELECT * FROM #myTable WHERE credential_secret IS NULL))  
BEGIN  
    PRINT 'Aborted. Failed to re-encrypt some values'  
    SELECT * FROM #myTable  
    ROLLBACK  
END  
ELSE  
BEGIN  
  
/* Replace the actual RBS credential store secrets with the newly   
encrypted secrets stored in the temp table #myTable.*/                
SET NOCOUNT ON;  
DECLARE @sql_user_sid varbinary(85);  
DECLARE @blob_store_id smallint;  
DECLARE @credential_name varchar(256);  
DECLARE @credential_secret varbinary(256);  
DECLARE curSecretValue CURSOR   
    FOR SELECT sql_user_sid, blob_store_id, credential_name, credential_secret   
FROM #myTable ORDER BY sql_user_sid, blob_store_id, credential_name;  
  
OPEN curSecretValue;  
FETCH NEXT FROM curSecretValue   
    INTO @sql_user_sid, @blob_store_id, @credential_name, @credential_secret  
WHILE @@FETCH_STATUS = 0  
BEGIN  
    UPDATE [mssqlrbs_resources].[rbs_internal_blob_store_credentials]   
        SET [credential_secret] = @credential_secret   
        FROM [mssqlrbs_resources].[rbs_internal_blob_store_credentials]   
        WHERE sql_user_sid = @sql_user_sid AND blob_store_id = @blob_store_id AND   
            credential_name = @credential_name  
FETCH NEXT FROM curSecretValue   
    INTO @sql_user_sid, @blob_store_id, @credential_name, @credential_secret  
END  
CLOSE curSecretValue  
DEALLOCATE curSecretValue  
  
DROP TABLE #myTable;  
CLOSE ALL SYMMETRIC KEYS;  
DROP SYMMETRIC KEY #mssqlrbs_encryption_skey;  
  
/* Verify that you can decrypt all encrypted credential store entries using the certificate.*/  
IF( EXISTS(SELECT * FROM [mssqlrbs_resources].[rbs_internal_blob_store_credentials]   
WHERE decryptbykeyautocert(cert_id('cert_mssqlrbs_encryption'),   
    NULL, credential_secret) IS NULL))  
BEGIN  
    print 'Aborted. Failed to verify key rotation'  
    ROLLBACK;  
END;  
ELSE  
    COMMIT;  
END;  
END;  
END TRY  
BEGIN CATCH  
     PRINT 'Exception caught: ' + cast(ERROR_NUMBER() as nvarchar) + ' ' + ERROR_MESSAGE();  
     ROLLBACK  
END CATCH  
END;  
GO  
```  
  
 Now you can use the `sp_rotate_rbs_symmetric_credential_key` stored procedure to rotate the RBS credential store symmetric key, and the secrets remain the same before and after the key rotation.  
  
```  
SELECT *, decryptbykeyautocert(cert_id('cert_mssqlrbs_encryption'), NULL, credential_secret)   
FROM [mssqlrbs_resources].[rbs_internal_blob_store_credentials];  
  
EXEC sp_rotate_rbs_symmetric_credential_key;  
  
SELECT *, decryptbykeyautocert(cert_id('cert_mssqlrbs_encryption'), NULL, credential_secret)   
FROM [mssqlrbs_resources].[rbs_internal_blob_store_credentials];  
  
/* See that the RBS credential store symmetric key properties reflect the new changes*/  
SELECT * FROM sys.symmetric_keys WHERE name = 'mssqlrbs_encryption_skey';  
```  
  
## See Also  
[Remote Blob Store and Always On Availability Groups (SQL Server)](../../database-engine/availability-groups/windows/remote-blob-store-rbs-and-always-on-availability-groups-sql-server.md)   
 [CREATE SYMMETRIC KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-symmetric-key-transact-sql.md)  
  
  

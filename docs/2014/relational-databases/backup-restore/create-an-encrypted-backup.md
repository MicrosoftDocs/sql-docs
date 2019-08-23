---
title: "Create an Encrypted Backup | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
ms.assetid: e29061d3-c2ab-4d98-b9be-8e90a11d17fe
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Create an Encrypted Backup
  This topic describes the steps necessary to create an encrypted backup using Transact-SQL.  
  
## Backup to Disk with Encryption  
 **Prerequisites:**  
  
-   Access to a local disk or to storage with adequate space to create a backup of the database.  
  
-   A Database Master Key for the master database, and a certificate or asymmetric key available on the instance of SQL Server. For encryption requirements and permissions, see [Backup Encryption](backup-encryption.md).  
  
 Use the following steps to create an encrypted backup of a database to a local disk. This example uses a user database called MyTestDB.  
  
1.  **Create a Database Master Key of the master database:** Choose a password for encrypting the copy of the master key that will be stored in the database. Connect to the database engine, start a new query windows and copy and paste the following example and click **Execute**.  
  
    ```  
    -- Creates a database master key.   
    -- The key is encrypted using the password "<master key password>"  
    USE master;  
    GO  
    CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<master key password>';  
    GO  
  
    ```  
  
2.  **Create a Backup Certificate:** Create a backup certificate in the master database. Copy and paste the following example into the query window and click **Execute**  
  
    ```  
    Use Master  
    GO  
    CREATE CERTIFICATE MyTestDBBackupEncryptCert  
       WITH SUBJECT = 'MyTestDB Backup Encryption Certificate';  
    GO  
  
    ```  
  
3.  **Backup the database:** Specify the encryption algorithm and certificate to use. Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    BACKUP DATABASE [MyTestDB]  
    TO DISK = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Backup\MyTestDB.bak'  
    WITH  
      COMPRESSION,  
      ENCRYPTION   
       (  
       ALGORITHM = AES_256,  
       SERVER CERTIFICATE = MyTestDBBackupEncryptCert  
       ),  
      STATS = 10  
    GO  
  
    ```  
  
 For an example of encrypting a backup protected by an EKM, see [Extensible Key Management Using Azure Key Vault &#40;SQL Server&#41;](../security/encryption/extensible-key-management-using-azure-key-vault-sql-server.md).  
  
### Backup to Windows Azure Storage with Encryption  
 If you are creating a backup to Windows Azure storage using the **SQL Server Backup to URL** option, the encryption steps are the same, but you must use URL as the destination and a SQL Credential to authenticate to the Windows Azure storage. If you want to configure [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] with encryption options, see [Setting up SQL Server Managed Backup to Windows Azure](enable-sql-server-managed-backup-to-microsoft-azure.md) and [Setting up SQL Server Managed Backup to Windows Azure for Availability Groups](../../database-engine/setting-up-sql-server-managed-backup-to-windows-azure-for-availability-groups.md).  
  
 **Prerequisites:**  
  
-   A windows storage account and a container. For more information, see. [Lesson 1: Create Windows Azure Storage Objects](../../tutorials/lesson-1-create-windows-azure-storage-objects.md).  
  
-   A Database Master Key for the master database, and a certificate or asymmetric key  on the instance of SQL Server. For encryption requirements and permissions, see [Backup Encryption](backup-encryption.md).  
  
1.  **Create SQL Server Credential:** To create a SQL Server credential, connect to the Database Engine, open a new query window, and copy and paste the following example and click **Execute**.  
  
    ```  
    CREATE CREDENTIAL mycredential   
    WITH IDENTITY= 'mystorageaccount' - this is the name of the storage account you specified when creating a storage account    
    , SECRET = '<storage account access key>' - this should be either the Primary or Secondary Access Key for the storage account  
    ```  
  
2.  **Create a Database Master Key:** Choose a password for encrypting the copy of the master key that will be stored in the database. Connect to the database engine, start a new query windows and copy and paste the following example and click **Execute**.  
  
    ```  
    -- Creates a database master key.  
    -- The key is encrypted using the password "<master key password>"  
    USE Master;  
    GO  
    CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<master key password>';  
    GO  
  
    ```  
  
3.  **Create a Backup Certificate:** Create a Backup Certificate in the master database. Copy and paste the following example in the query window and click **Execute**  
  
    ```  
    USE Master;  
    GO  
    CREATE CERTIFICATE MyTestDBBackupEncryptCert  
       WITH SUBJECT = 'MyTestDBBackupEncryptCert ';  
    GO  
  
    ```  
  
4.  **Backup the database:** Specify the encryption algorithm and the certificate to use. Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    BACKUP DATABASE [MyTestDB]  
    TO URL = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Backup\MyTestDB.bak'  
    WITH  
      CREDENTIAL 'mycredential' - this is the name of the credential created in the first step.  
      ,COMPRESSION  
      ,ENCRYPTION   
       (  
       ALGORITHM = AES_256,  
       SERVER CERTIFICATE = MyTestDBBackupEncryptCert  
       ),  
      STATS = 10  
    GO  
  
    ```  
  
  

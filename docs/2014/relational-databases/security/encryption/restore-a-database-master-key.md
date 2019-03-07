---
title: "Restore a Database Master Key | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "database master key [SQL Server], importing"
ms.assetid: 16897cc5-db8f-43bb-a38e-6855c82647cf
author: aliceku
ms.author: aliceku
manager: craigg
---
# Restore a Database Master Key
  This topic describes how to restore the database master key in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[tsql](../../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   [To restore the database master key using Transact-SQL](#SSMSProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   When the master key is restored, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] decrypts all the keys that are encrypted with the currently active master key, and then encrypts these keys with the restored master key. This resource-intensive operation should be scheduled during a period of low demand. If the current database master key is not open or cannot be opened, or if any of the keys that are encrypted by it cannot be decrypted, the restore operation fails.  
  
-   If any one of the decryptions fails, the restore will fail. You can use the FORCE option to ignore errors, but this option will cause the loss of any data that cannot be decrypted.  
  
-   If the master key was encrypted by the service master key, the restored master key will also be encrypted by the service master key.  
  
-   If there is no master key in the current database, RESTORE MASTER KEY creates a master key. The new master key will not be automatically encrypted with the service master key.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires CONTROL permission on the database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio with Transact-SQL  
  
#### To restore the database master key  
  
1.  Retrieve a copy of the backed-up database master key, either from a physical backup medium or a directory on the local file system.  
  
2.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
3.  On the Standard bar, click **New Query**.  
  
4.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- Restores the database master key of the AdventureWorks2012 database.  
    USE AdventureWorks2012;  
    GO  
    RESTORE MASTER KEY   
        FROM FILE = 'c:\backups\keys\AdventureWorks2012_master_key'   
        DECRYPTION BY PASSWORD = '3dH85Hhk003#GHkf02597gheij04'   
        ENCRYPTION BY PASSWORD = '259087M#MyjkFkjhywiyedfgGDFD';  
    GO  
    ```  
  
    > [!NOTE]  
    >  The file path to the key and the key's password (if it exists) will be different than what is indicated above. Please make sure that both are specific to your server and key set-up.  
  
 For more information, see [RESTORE MASTER KEY &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-master-key-transact-sql)  
  
  

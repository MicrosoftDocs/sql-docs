---
title: "Back Up the Service Master Key | Microsoft Docs"
description: Learn how to back up the service master key in SQL Server by using Transact-SQL. The service master key is the root of the encryption hierarchy.
ms.custom: ""
ms.date: "04/02/2021"
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "service master key [SQL Server], exporting"
ms.assetid: f60b917c-6408-48be-b911-f93b05796904
author: jaszymas
ms.author: jaszymas
---
# Back Up the Service Master Key
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  This article describes how to back up the Service Master key in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE[tsql](../../../includes/tsql-md.md)]. The service master key is the root of the encryption hierarchy. It should be backed up and stored in a secure, off-site location. Creating this backup should be one of the first administrative actions performed on the server.  
  
We recommend that you back up the master key as soon as it is created, and store the backup in a secure, off-site location.  
  
## Permissions

Requires CONTROL permission on the database.  
  
## Using Transact-SQL  
  
### To back up the Service Master key
  
1. In [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], connect to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance containing the service master key you wish to back up.  
  
2. Choose a password that will be used to encrypt the service master key on the backup medium. This password is subject to complexity checks. For more information, see [Password Policy](../../../relational-databases/security/password-policy.md).  
  
3. Obtain a removable backup medium for storing a copy of the backed-up key.  
  
4. Identify an NTFS directory in which to create the backup of the key. This directory is where you will create the file specified in the next step. The directory should be protected with highly restrictive access control lists (ACLs).  
  
5. In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
6. On the Standard bar, click **New Query**.  
  
7. Copy and paste the following example into the query window and click **Execute**.  
  
    ```sql
    -- Creates a backup of the service master key.
    USE master;
    GO
    BACKUP SERVICE MASTER KEY TO FILE = 'c:\temp_backups\keys\service_master_ key'
        ENCRYPTION BY PASSWORD = '3dH85Hhk003GHk2597gheij4';
    GO
    ```  
  
    > [!NOTE]  
    > The file path to the key and the key's password (if it exists) will be different than what is indicated above. Make sure that both are specific to your server and key set-up.
  
8. Copy the file to the backup medium and verify the copy.  
  
9. Store the backup in a secure, off-site location.  

## Next steps

- [Restore the Service Master Key](restore-the-service-master-key.md)

## See also

- [OPEN MASTER KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/open-master-key-transact-sql.md)
- [BACKUP MASTER KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/backup-master-key-transact-sql.md)

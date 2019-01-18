---
title: "Back Up the Service Master Key | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "service master key [SQL Server], exporting"
ms.assetid: f60b917c-6408-48be-b911-f93b05796904
author: aliceku
ms.author: aliceku
manager: craigg
---
# Back Up the Service Master Key
  This topic describes how to back-up the Service Master key in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[tsql](../../../includes/tsql-md.md)]. The service master key is the root of the encryption hierarchy. It should be backed up and stored in a secure, off-site location. Creating this backup should be one of the first administrative actions performed on the server.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   [To back-up the Service Master key](#Procedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The master key must be open and, therefore, decrypted before it is backed up. If it is encrypted with the service master key, the master key does not have to be explicitly opened; however, if the master key is encrypted only with a password, it must be explicitly opened.  
  
-   We recommend that you back up the master key as soon as it is created, and store the backup in a secure, off-site location.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires CONTROL permission on the database.  
  
##  <a name="Procedure"></a> Using Transact-SQL  
  
#### To back-up the Service Master key  
  
1.  In [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], connect to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance containing the service master key you wish to back up.  
  
2.  Choose a password that will be used to encrypt the service master key on the backup medium. This password is subject to complexity checks. For more information, see [Password Policy](../password-policy.md).  
  
3.  Obtain a removable backup medium for storing a copy of the backed-up key.  
  
4.  Identify an NTFS directory in which to create the backup of the key. This is where you will create the file specified in the next step. The directory should be protected with highly restrictive access control lists (ACLs).  
  
5.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
6.  On the Standard bar, click **New Query**.  
  
7.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- Creates a backup of the "AdventureWorks2012" master key.  
    -- Because this master key is not encrypted by the service master key, a password must be specified when it is opened.  
    USE AdventureWorks2012;  
    GO  
    BACKUP SERVICE MASTER KEY TO FILE = 'c:\temp_backups\keys\service_master_ key'   
        ENCRYPTION BY PASSWORD = '3dH85Hhk003GHk2597gheij4';  
    GO  
    ```  
  
    > [!NOTE]  
    >  The file path to the key and the key's password (if it exists) will be different than what is indicated above. Make sure that both are specific to your server and key set-up.  
  
8.  Copy the file to the backup medium and verify the copy.  
  
9. Store the backup in a secure, off-site location.  
  
 For more information, see [OPEN MASTER KEY &#40;Transact-SQL&#41;](/sql/t-sql/statements/open-master-key-transact-sql) and [BACKUP MASTER KEY &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-master-key-transact-sql).  
  
  

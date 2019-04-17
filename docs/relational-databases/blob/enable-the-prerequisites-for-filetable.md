---
title: "Enable the Prerequisites for FileTable | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: filestream
ms.topic: conceptual
helpviewer_keywords: 
  - "FileTables [SQL Server], prerequisites"
ms.assetid: 6286468c-9dc9-4eda-9961-071d2a36ebd6
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Enable the Prerequisites for FileTable
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Describes how to enable the prerequisites for creating and using FileTables.  
  
##  <a name="EnablePrereq"></a> Enabling the Prerequisites for FileTable  
 To enable the prerequisites for creating and using FileTables, enable the following items:  
  
-   **At the instance level:**  
  
    -   [Enabling FILESTREAM at the Instance Level](#BasicsFilestream)  
  
-   **At the database level:**  
  
    -   [Providing a FILESTREAM Filegroup at the Database Level](#filegroup)  
  
    -   [Enabling Non-Transactional Access at the Database Level](#BasicsNTAccess)  
  
    -   [Specifying a Directory for FileTables at the Database Level](#BasicsDirectory)  
  
##  <a name="BasicsFilestream"></a> Enabling FILESTREAM at the Instance Level  
 FileTables extend the capabilities of the FILESTREAM feature of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Therefore you have to enable FILESTREAM for file I/O access at the Windows level and on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] before you can create and use FileTables.  
  
###  <a name="HowToFilestream"></a> How To: Enable FILESTREAM at the Instance Level  
 For information about how to enable FILESTREAM, see [Enable and Configure FILESTREAM](../../relational-databases/blob/enable-and-configure-filestream.md).  
  
 When you call **sp_configure** to enable FILESTREAM at the instance level, you have to set the filestream_access_level option to 2. For more information, see [filestream access level Server Configuration Option](../../database-engine/configure-windows/filestream-access-level-server-configuration-option.md).  
  
###  <a name="firewall"></a> How To: Allow FILESTREAM through the Firewall  
 For information about how to allow FILESTREAM through the firewall, see [Configure a Firewall for FILESTREAM Access](../../relational-databases/blob/configure-a-firewall-for-filestream-access.md).  
  
##  <a name="filegroup"></a> Providing a FILESTREAM Filegroup at the Database Level  
 Before you can create FileTables in a database, the database must have a FILESTREAM filegroup. For more information about this prerequisite, see [Create a FILESTREAM-Enabled Database](../../relational-databases/blob/create-a-filestream-enabled-database.md).  
  
##  <a name="BasicsNTAccess"></a> Enabling Non-Transactional Access at the Database Level  
 FileTables let Windows applications obtain a Windows file handle to FILESTREAM data without requiring a transaction. To allow this non-transactional access to files stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you have to specify the desired level of non-transactional access at the database level for each database that will contain FileTables.  
  
###  <a name="HowToCheckAccess"></a> How To: Check Whether Non-Transactional Access Is Enabled on Databases  
 Query the catalog view [sys.database_filestream_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-filestream-options-transact-sql.md) and check the **non_transacted_access** and **non_transacted_access_desc** columns.  
  
```sql  
SELECT DB_NAME(database_id), non_transacted_access, non_transacted_access_desc  
    FROM sys.database_filestream_options;  
GO  
```  
  
###  <a name="HowToNTAccess"></a> How To: Enable Non-Transactional Access at the Database Level  
 The available levels of non-transactional access are FULL, READ_ONLY, and OFF.  
  
 **Specify the level of non-transactional access by using Transact-SQL**  
 -   When you **create a new database**, call the [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-sql-server-transact-sql.md) statement with the **NON_TRANSACTED_ACCESS** FILESTREAM option.  
  
    ```sql  
    CREATE DATABASE database_name  
        WITH FILESTREAM ( NON_TRANSACTED_ACCESS = FULL, DIRECTORY_NAME = N'directory_name' )  
    ```  
  
-   When you **alter an existing database**, call the [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md) statement with the **NON_TRANSACTED_ACCESS** FILESTREAM option.  
  
    ```sql  
    ALTER DATABASE database_name  
        SET FILESTREAM ( NON_TRANSACTED_ACCESS = FULL, DIRECTORY_NAME = N'directory_name' )  
    ```  
  
 **Specify the level of non-transactional access by using SQL Server Management Studio**  
 You can specify the level of non-transactional access in the **FILESTREAM Non-transacted Access** field of the **Options** page of the **Database Properties** dialog box. For more information about this dialog box, see [Database Properties &#40;Options Page&#41;](../../relational-databases/databases/database-properties-options-page.md).  
  
##  <a name="BasicsDirectory"></a> Specifying a Directory for FileTables at the Database Level  
 When you enable non-transactional access to files at the database level, you can optionally provide a directory name at the same time by using the **DIRECTORY_NAME** option. If you do not provide a directory name when you enable non-transactional access, then you have to provide it later before you can create FileTables in the database.  
  
 In the FileTable folder hierarchy, this database-level directory becomes the child of the share name specified for FILESTREAM at the instance level, and the parent of the FileTables created in the database. For more information, see [Work with Directories and Paths in FileTables](../../relational-databases/blob/work-with-directories-and-paths-in-filetables.md).  
  
###  <a name="HowToDirectory"></a> How To: Specify a Directory for FileTables at the Database Level  
 The name that you specify must be unique across the instance for database-level directories.  
  
 **Specify a directory for FileTables by using Transact-SQL**  
 -   When you **create a new database**, call the [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-sql-server-transact-sql.md) statement with the **DIRECTORY_NAME** FILESTREAM option.  
  
    ```sql  
    CREATE DATABASE database_name  
        WITH FILESTREAM ( NON_TRANSACTED_ACCESS = FULL, DIRECTORY_NAME = N'directory_name' );  
    GO  
    ```  
  
-   When you **alter an existing database**, call the [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md) statement with the **DIRECTORY_NAME** FILESTREAM option. When you use these options to change the directory name, the database must be exclusively locked, with no open file handles.  
  
    ```sql  
    ALTER DATABASE database_name  
        SET FILESTREAM ( NON_TRANSACTED_ACCESS = FULL, DIRECTORY_NAME = N'directory_name' );  
    GO  
    ```  
  
-   When you **attach a database**, call the [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-sql-server-transact-sql.md) statement with the **FOR ATTACH** option and with the **DIRECTORY_NAME** FILESTREAM option.  
  
    ```sql  
    CREATE DATABASE database_name  
        FOR ATTACH WITH FILESTREAM ( DIRECTORY_NAME = N'directory_name' );  
    GO  
    ```  
  
-   When you **restore a database**, call the [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md) statement with the **DIRECTORY_NAME** FILESTREAM option.  
  
    ```sql  
    RESTORE DATABASE database_name  
        WITH FILESTREAM ( DIRECTORY_NAME = N'directory_name' );  
    GO  
    ```  
  
 **Specify a directory for FileTables by using SQL Server Management Studio**  
 You can specify a directory name in the **FILESTREAM Directory Name** field of the **Options** page of the **Database Properties** dialog box. For more information about this dialog box, see [Database Properties &#40;Options Page&#41;](../../relational-databases/databases/database-properties-options-page.md).  
  
###  <a name="viewnames"></a> How to: View Existing Directory Names for the Instance  
 To view the list of existing directory names for the instance, query the catalog view [sys.database_filestream_options &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-filestream-options-transact-sql.md) and check the **filestream_database_directory_name** column.  
  
```sql  
SELECT DB_NAME ( database_id ), directory_name  
    FROM sys.database_filestream_options;  
GO  
```  
  
###  <a name="ReqDirectory"></a> Requirements and Restrictions for the Database-Level Directory  
  
-   Setting the **DIRECTORY_NAME** is optional when you call **CREATE DATABASE** or **ALTER DATABASE**. If you do not specify a value for **DIRECTORY_NAME**, then the directory name remains null. However you cannot create FileTables in the database until you specify a value for **DIRECTORY_NAME** at the database level.  
  
-   The directory name that you provide must comply with the requirements of the file system for a valid directory name.  
  
-   When the database contains FileTables, you cannot set the **DIRECTORY_NAME** back to a null value.  
  
-   When you attach or restore a database, the operation fails if the new database has a value for **DIRECTORY_NAME** that already exists in the target instance. Specify a unique value for **DIRECTORY_NAME** when you call **CREATE DATABASE FOR ATTACH** or **RESTORE DATABASE**.  
  
-   When you upgrade an existing database to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], the value of **DIRECTORY_NAME** is null.  
  
-   When you enable or disable non-transactional access at the database level, the operation does not check whether the directory name has been specified or whether it is unique.  
  
-   When you drop a database that was enabled for FileTables, the database-level directory and all the directory stuctures of all the FileTables under it are removed.  
  
  

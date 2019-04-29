---
title: "Create, Alter, and Drop FileTables | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: filestream
ms.topic: conceptual
helpviewer_keywords: 
  - "FileTables [SQL Server], altering"
  - "FileTables [SQL Server], dropping"
  - "FileTables [SQL Server], creating"
ms.assetid: 47d69e37-8778-4630-809b-2261b5c41c2c
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Create, Alter, and Drop FileTables
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Describes how to create a new FileTable, or alter or drop an existing FileTable.  
  
##  <a name="BasicsCreate"></a> Creating a FileTable  
 A FileTable is a specialized user table that has a pre-defined and fixed schema. This schema stores FILESTREAM data, file and directory information, and file attributes. For information about the FileTable schema, see [FileTable Schema](../../relational-databases/blob/filetable-schema.md).  
  
 You can create a new FileTable by using Transact-SQL or [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Since a FileTable has a fixed schema, you do not have to specify a list of columns. The simple syntax for creating a FileTable lets you specify:  
  
-   A directory name. In the FileTable folder hierarchy, this table-level directory becomes the child of the database directory specified at the database level, and the parent of the files or directories stored in the table.  
  
-   The name of the collation to be used for file names in the **Name** column of the FileTable.  
  
-   The names to be used for the 3 primary key and unique constraints that are automatically created.  
  
###  <a name="HowToCreate"></a> How To: Create a FileTable  
 **Create a FileTable by Using Transact-SQL**  
 Create a FileTable by calling the [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md) statement with the **AS FileTable** option. Since a FileTable has a fixed schema, you do not have to specify a list of columns. You can specify the following settings for the new FileTable:  
  
1.  **FILETABLE_DIRECTORY**. Specifies the directory that serves as the root directory for all the files and directories stored in the FileTable. This name should be unique among all the FileTable directory names in the database. Comparison for uniqueness is case-insensitive, regardless of the current collation settings.  
  
    -   This value has a data type of **nvarchar(255)** and uses a fixed collation of **Latin1_General_CI_AS_KS_WS**.  
  
    -   The directory name that you provide must comply with the requirements of the file system for a valid directory name.  
  
    -   This name should be unique among all the FileTable directory names in the database. Comparison for uniqueness is case-insensitive, regardless of the current collation settings.  
  
    -   If you do not provide a directory name when you create the FileTable, then the name of the FileTable itself is used as the directory name.  
  
2.  **FILETABLE_COLLATE_FILENAME**. Specifies the name of the collation to be applied to the **Name** column in the FileTable.  
  
    1.  The specified collation must be **case-insensitive** to comply with Windows file naming semantics.  
  
    2.  If you do not provide a value for **FILETABLE_COLLATE_FILENAME**, or you specify **database_default**, the column inherits the collation of the current database. If the current database collation is case-sensitive, an error is raised and the **CREATE TABLE** operation fails.  
  
3.  You can also specify the names to be used for the 3 primary key and unique constraints that are automatically created. If you do not provide names, then the system generates names as described later in this topic.  
  
    -   **FILETABLE_PRIMARY_KEY_CONSTRAINT_NAME**  
  
    -   **FILETABLE_STREAMID_UNIQUE_CONSTRAINT_NAME**  
  
    -   **FILETABLE_FULLPATH_UNIQUE_CONSTRAINT_NAME**  
  
 **Examples**  
  
 The following example creates a new FileTable and specifies user-defined values for both **FILETABLE_DIRECTORY** and **FILETABLE_COLLATE_FILENAME**.  
  
```sql  
CREATE TABLE DocumentStore AS FileTable  
    WITH (   
          FileTable_Directory = 'DocumentTable',  
          FileTable_Collate_Filename = database_default  
         );  
GO  
```  
  
 The following example also creates a new FileTable. Since user-defined values are not specified, the value of **FILETABLE_DIRECTORY** becomes the name of the FileTable, the value of **FILETABLE_COLLATE_FILENAME** becomes database_default, and the primary key and unique contraints receive system-generated names.  
  
```sql  
CREATE TABLE DocumentStore AS FileTable;  
GO  
```  
  
 **Create a FileTable by Using SQL Server Management Studio**  
 In Object Explorer, expand the objects under the selected database, then right-click on the **Tables** folder, and then select **New FileTable**.  
  
 This option opens a new script window which contains a Transact-SQL script template that you can customize and run to create a FileTable. Use the **Specify Values for Template Parameters** option on the **Query** menu to customize the script easily.  
  
###  <a name="ReqCreate"></a> Requirements and Restrictions for Creating a FileTable  
  
-   You cannot alter an existing table to convert it into a FileTable.  
  
-   The parent directory previously specified at the database level must have a non-null value. For information about specifying the database-level directory, see [Enable the Prerequisites for FileTable](../../relational-databases/blob/enable-the-prerequisites-for-filetable.md).  
  
-   A FileTable requires a valid FILESTREAM filegroup, since a FileTable contains a FILESTREAM column. You can optionally specify a valid FILESTREAM filegroup as part of the **CREATE TABLE** command for creating a FileTable. If you do not specify a filegroup, then the FileTable uses the default FILESTREAM filegroup for the database. If the database does not have a FILESTREAM filegroup, then an error is raised.  
  
-   You cannot create a table constraint as part of a **CREATE TABLE...AS FILETABLE** statement. However you can add the constraint later by using an **ALTER TABLE** statement.  
  
-   You cannot create a FileTable in the **tempdb** database or in any of the other system databases.  
  
-   You cannot create a FileTable as a temporary table.  
  
##  <a name="BasicsAlter"></a> Altering a FileTable  
 Since a FileTable has a pre-defined and fixed schema, you cannot add or change its columns. However, you can add custom indexes, triggers, constraints, and other options to a FileTable.  
  
 For information about using the ALTER TABLE statement to enable or disable the FileTable namespace, including the system-defined constraints, see [Manage FileTables](../../relational-databases/blob/manage-filetables.md).  
  
###  <a name="HowToChange"></a> How To: Change the Directory for a FileTable  
 **Change the Directory for a FileTable by Using Transact-SQL**  
 Call the ALTER TABLE statement and provide a valid new value for the **FILETABLE_DIRECTORY** SET option.  
  
 **Example**  
  
```sql  
ALTER TABLE filetable_name  
    SET ( FILETABLE_DIRECTORY = N'directory_name' );  
GO  
```  
  
 **Change the Directory for a FileTable by Using SQL Server Management Studio**  
 In Object Explorer, right-click on the FileTable and select **Properties** to open the **Table Properties** dialog box. On the **FileTable** page, enter a new value for **FileTable directory name**.  
  
###  <a name="ReqAlter"></a> Requirements and Restrictions for Altering a FileTable  
  
-   You cannot alter the value of **FILETABLE_COLLATE_FILENAME**.  
  
-   You cannot change, drop, or disable the system-defined columns of a FileTable.  
  
-   You cannot add new user columns, computed columns, or persisted computed columns to a FileTable.  
  
##  <a name="BasicsDrop"></a> Dropping a FileTable  
 You can drop a FileTable by using the ordinary syntax for the [DROP TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-table-transact-sql.md) statement.  
  
 When you drop a FileTable, the following objects are also dropped:  
  
-   All the columns of the FileTable and all the objects associated with the table, such as indexes, constraints, and triggers, are also dropped.  
  
-   The FileTable directory and the sub-directories that it contained disappear from the FILESTREAM file and directory hierarchy of the database.  
  
 The DROP TABLE command fails if there are open file handles in the FileTable's file namespace. For information about closing open handles, see [Manage FileTables](../../relational-databases/blob/manage-filetables.md).  
  
##  <a name="BasicsOtherObjects"></a> Other Database Objects Are Created When You Create a FileTable  
 When you create a new FileTable, some system-defined indexes and constraints are also created. You cannot alter or drop these objects; they disappear only when the FileTable itself is dropped. To see the list of these objects, query the catalog view [sys.filetable_system_defined_objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-filetable-system-defined-objects-transact-sql.md).  
  
```sql  
--View all objects for all filetables, unsorted  
SELECT * FROM sys.filetable_system_defined_objects;  
GO  
  
--View sorted list with friendly names  
SELECT OBJECT_NAME(parent_object_id) AS 'FileTable', OBJECT_NAME(object_id) AS 'System-defined Object'  
    FROM sys.filetable_system_defined_objects  
    ORDER BY FileTable, 'System-defined Object';  
GO  
```  
  
 **Indexes that are created when you create a new FileTable**  
 When you create a new FileTable, the following system-defined indexes are also created:  
  
|||  
|-|-|  
|**Columns**|**Index type**|  
|[path_locator] ASC|Primary Key, non-clustered|  
|[parent_path_locator] ASC,<br /><br /> [name] ASC|Unique, non-clustered|  
|[stream_id] ASC|Unique, non-clustered|  
  
 **Constraints that are created when you create a new FileTable**  
 When you create a new FileTable, the following system-defined constraints are also created:  
  
|Constraints|Enforces|  
|-----------------|--------------|  
|Default constraints on the following columns:<br /><br /> creation_time<br /><br /> is_archive<br /><br /> is_directory<br /><br /> is_hidden<br /><br /> is_offline<br /><br /> is_readonly<br /><br /> is_system<br /><br /> is_temporary<br /><br /> last_access_time<br /><br /> last_write_time<br /><br /> path_locator<br /><br /> stream_id|The system-defined default constraints enforce default values for the specified columns.|  
|Check constraints|The system-defined check constraints enforce the following requirements:<br /><br /> Valid filenames.<br /><br /> Valid file attributes.<br /><br /> Parent object must be a directory.<br /><br /> Namespace hierarchy is locked during file manipulation.|  
  
 **Naming convention for the system-defined constraints**  
 The system-defined constaints described above are named in the format **\<constraintType>_\<tablename>[\_\<columnname>]\_\<uniquifier>** where:  
  
-   *<constraint_type>* is CK (check constraint), DF (default constraint), FK (foreign key), PK (primary key), or UQ (unique constraint).  
  
-   *\<uniquifier>* is a system-generated string to make the name unique. This string may contain the FileTable name and a unique identifier.  
  
## See Also  
 [Manage FileTables](../../relational-databases/blob/manage-filetables.md)  
  
  

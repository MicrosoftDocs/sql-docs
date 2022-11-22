---
title: "FileTables (SQL Server) | Microsoft Docs"
description: Explore the benefits and functionality of FileTables, the SQL Server feature that uses a directory structure to store files. Learn how to work with FileTables.
ms.custom: ""
ms.date: "10/24/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: filestream
ms.topic: conceptual
helpviewer_keywords: 
  - "FileTables [SQL Server], overview"
  - "FileTables [SQL Server]"
  - "FileTable [SQL Server], see FileTables [SQL Server]"
  - "FileTable [SQL Server]"
ms.assetid: a57b629c-e9ed-48fd-9a48-ed3787d80c8f
author: MikeRayMSFT
ms.author: mikeray
---
# FileTables (SQL Server)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The FileTable feature brings support for the Windows file namespace and compatibility with Windows applications to the file data stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. FileTable lets an application integrate its storage and data management components, and provides integrated [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services - including full-text search and semantic search - over unstructured data and metadata.  
  
 In other words, you can store files and documents in special tables in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] called FileTables, but access them from Windows applications as if they were stored in the file system, without making any changes to your client applications.  
  
 The FileTable feature builds on top of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] FILESTREAM technology. To learn more about FILESTREAM, see [FILESTREAM &#40;SQL Server&#41;](../../relational-databases/blob/filestream-sql-server.md).  
  
##  <a name="Goals"></a> Benefits of the FileTable Feature  
 The goals of the FileTable feature include the following:  
  
-   Windows API compatibility for file data stored within a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. Windows API compatibility includes the following:  
  
    -   Non-transactional streaming access and in-place updates to FILESTREAM data.  
  
    -   A hierarchical namespace of directories and files.  
  
    -   Storage of file attributes, such as created date and modified date.  
  
    -   Support for Windows file and directory management APIs.  
  
-   Compatibility with other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features including management tools, services, and relational query capabilities over FILESTREAM and file attribute data.  
  
 Thus FileTables remove a significant barrier to the use of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for the storage and management of unstructured data that is currently residing as files on file servers. Enterprises can move this data from file servers into FileTables to take advantage of integrated administration and services provided by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. At the same time, they can maintain Windows application compatibility for their existing Windows applications that see this data as files in the file system.  
 
  
##  <a name="Description"></a> What Is a FileTable?  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides a special **table of files**, also referred to as a **FileTable**, for applications that require file and directory storage in the database, with Windows API compatibility and non-transactional access. A FileTable is a specialized user table with a pre-defined schema that stores FILESTREAM data, as well as file and directory hierarchy information and file attributes.  
  
 A FileTable provides the following functionality:  
  
-   A FileTable represents a hierarchy of directories and files. It stores data related to all the nodes in that hierarchy, for both directories and the files they contain. This hierarchy starts from a root directory that you specify when you create the FileTable.  
  
-   Every row in a FileTable represents a file or a directory.  
  
-   Every row contains the following items. For more information about the schema of a FileTable, see [FileTable Schema](../../relational-databases/blob/filetable-schema.md).  
  
    -   A **file_stream** column for stream data and a **stream_id** (GUID) identifier. (The **file_stream** column is NULL for a directory.)  
  
    -   Both **path_locator** and **parent_path_locator** columns for representing and maintaining the current item (file or directory) and directory hierarchy.  
  
    -   10 file attributes such as created date and modified date that are useful with file I/O APIs.  
  
    -   A type column that supports full-text search and semantic search over files and documents.  
  
-   A FileTable enforces certain system-defined constraints and triggers to maintain file namespace semantics.  
  
-   When the database is configured for non-transactional access, the file and directory hierarchy represented in the FileTable is exposed under the FILESTREAM share configured for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. This provides file system access for Windows applications.  
  
 Some additional characteristics of FileTables include the following:  
  
-   The file and directory data stored in a FileTable is exposed through a Windows share for non-transactional file access for Windows API based applications. For a Windows application, this looks like a normal share with its files and directories. Applications can use a rich set of Windows APIs to manage the files and directories under this share.  
  
-   The directory hierarchy surfaced through the share is a purely logical directory structure that is maintained within the FileTable.  
  
-   Calls to create or change a file or directory through the Windows share are intercepted by a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] component and reflected in the corresponding relational data in the FileTable.  
  
-   Windows API operations are non-transactional in nature, and are not associated with user transactions. However, transactional access to FILESTREAM data stored in a FileTable is fully supported, as is the case for any FILESTREAM column in a regular table.  
  
-   FileTables can also be queried and updated through normal [!INCLUDE[tsql](../../includes/tsql-md.md)] access. They are also integrated with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] management tools, and features such as backup.  

-   You are unable to send an email request through dbmail and attach a file located in a filestream directory (and therefore filetable). The filesystem filter driver RsFx0420 inspects incoming I/O requests going in and out of the filestream folder. If the request is not both from the SQLServer executable and Filestream code, they are explicitly disallowed.
  
##  <a name="additional"></a> Additional Considerations for Using FileTables  
  
###  <a name="DBA"></a> Administrative Considerations  
 **About FILESTREAM and FileTables**  
  
-   You configure FileTables separately from FILESTREAM. Therefore you can continue to use the FILESTREAM feature without enabling non-transactional access or creating FileTables.  
  
-   There is no non-transactional access to FILESTREAM data except through FileTables. Therefore, when you enable non-transactional access, the behavior of existing FILESTREAM columns and applications is not affected.  
  
 **About FileTables and non-transactional access**  
  
-   You can enable or disable non-transactional access at the database level.  
  
-   You can configure or fine-tune non-transactional access at the database level by turning it off, or by enabling read only or full read/write access.  
   
###  <a name="memory"></a> FileTables Do Not Support Memory-Mapped Files  
 FileTables do not support memory-mapped files. Notepad and Paint are two common examples of applications that use memory-mapped files. You cannot use these applications on the same computer as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to open files that are stored in a FileTable. However you can use these applications from a remote computer to open files that are stored in a FileTable, because in these circumstances the memory-mapping feature is not used.  
   
##  <a name="reltasks"></a> Related Tasks  
 [Enable the Prerequisites for FileTable](../../relational-databases/blob/enable-the-prerequisites-for-filetable.md)  
 Describes how to enable the prerequisites for creating and using FileTables.  
  
 [Create, Alter, and Drop FileTables](../../relational-databases/blob/create-alter-and-drop-filetables.md)  
 Describes how to create a new FileTable, or alter or drop an existing FileTable.  
  
 [Load Files into FileTables](../../relational-databases/blob/load-files-into-filetables.md)  
 Describes how to load or migrate files into FileTables.  
  
 [Work with Directories and Paths in FileTables](../../relational-databases/blob/work-with-directories-and-paths-in-filetables.md)  
 Describes the directory structure in which the files are stored in FileTables.  
  
 [Access FileTables with Transact-SQL](../../relational-databases/blob/access-filetables-with-transact-sql.md)  
 Describes how Transact-SQL data manipulation language (DML) commands work with FileTables.  
  
 [Access FileTables with File Input-Output APIs](../../relational-databases/blob/access-filetables-with-file-input-output-apis.md)  
 Describes how file system I/O works on a FileTable.  
  
 [Manage FileTables](../../relational-databases/blob/manage-filetables.md)  
 Describes common administrative tasks for managing FileTables.  
  
##  <a name="relcontent"></a> Related Content  
 [FileTable Schema](../../relational-databases/blob/filetable-schema.md)  
 Describes the pre-defined and fixed schema of a FileTable.  
  
 [FileTable Compatibility with Other SQL Server Features](../../relational-databases/blob/filetable-compatibility-with-other-sql-server-features.md)  
 Describes how FileTables work with other features of SQL Server.  
  
 [FileTable DDL, Functions, Stored Procedures, and Views](../../relational-databases/blob/filetable-ddl-functions-stored-procedures-and-views.md)  
 Lists the [!INCLUDE[tsql](../../includes/tsql-md.md)] statements and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database objects that have been added or changed to support the FileTable feature.  

## See Also
[Filestream and FileTable Dynamic Management Views (Transact-SQL)](../system-dynamic-management-views/filestream-and-filetable-dynamic-management-views-transact-sql.md)
<br>[Filestream and FileTable Catalog Views (Transact-SQL)](../system-catalog-views/filestream-and-filetable-catalog-views-transact-sql.md)
<br>[Filestream and FileTable System Stored Procedures (Transact-SQL)](../system-stored-procedures/filestream-and-filetable-system-stored-procedures.md)


  
  

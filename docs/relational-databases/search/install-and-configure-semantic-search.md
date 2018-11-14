---
title: "Install and Configure Semantic Search | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "search, sql-database"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "semantic search [SQL Server], installing"
  - "semantic search [SQL Server], configuring"
ms.assetid: 2cdd0568-7799-474b-82fb-65d79df3057c
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Install and Configure Semantic Search
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Describes the prerequisites for statistical semantic search and how to install or check them.  
  
## Install Semantic Search  
  
###  <a name="HowToCheckInstalled"></a> Check whether Semantic Search is installed  
 Query the **IsFullTextInstalled** property of the [SERVERPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/serverproperty-transact-sql.md) metadata function.  
  
 A return value of 1 indicates that Full-Text Search and Semantic Search are installed; a return value of 0 indicates that they are not installed.  
  
```sql  
SELECT SERVERPROPERTY('IsFullTextInstalled');  
GO  
```  
  
###  <a name="BasicsSemanticSearch"></a> Install Semantic Search  
 To install Semantic Search, select **Full-Text and Semantic Extractions for Search** on the **Features to Install** page during SQL Server setup.  
  
 Statistical Semantic Search depends on Full-Text Search. These two optional features of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are installed together.  
  
## Install the Semantic Language Statistics Database  
 Semantic Search has an additional external dependency that is called the semantic language statistics database. This database contains the statistical language models required by semantic search. A single semantic language statistics database contains the language models for all the languages that are supported for semantic indexing.  
  
###  <a name="HowToCheckDatabase"></a> Check whether the Semantic Language Statistics Database is installed  
 Query the catalog view [sys.fulltext_semantic_language_statistics_database &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-semantic-language-statistics-database-transact-sql.md).  
  
 If the semantic language statistics database is installed and registered for the instance, then the query results contain a single row of information about the database.  
  
```sql  
SELECT * FROM sys.fulltext_semantic_language_statistics_database;  
GO  
```  
  
###  <a name="HowToInstallModel"></a> Install, attach, and register the Semantic Language Statistics Database  
 The semantic language statistics database is not installed by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup program. To set up the Semantic Language Statistics database as a prerequisite for semantic indexing, do the following things:  
  
 **1. Install the semantic language statistics database.**  
 
 1.  Locate the semantic language statistics database on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation media or download it from the Web.  
  
        1.  Locate the Windows installer package named **SemanticLanguageDatabase.msi** on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation media.  
  
        2.  Download the installer package from the [Microsoft® SQL Server® 2016 Semantic Language Statistics](https://www.microsoft.com/download/details.aspx?id=52681) page on the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Download Center.  
  
2.  Run the **SemanticLanguageDatabase.msi** Windows installer package to extract the database and log file.  
  
     You can optionally change the destination directory. By default, the installer extracts the files to a folder named **Microsoft Semantic Language Database** in the Program Files folder. The MSI file contains a compressed database file and log file.  
  
3.  Move the extracted database file and log file to a suitable location in the file system.  
  
     If you leave the files in their default location, it will not be possible to extract another copy of the database for another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
    > [!IMPORTANT]  
    >  When the semantic language statistics database is extracted, restricted permissions are assigned to the database file and log file in the default location in the file system. As a result, you may not have permission to attach the database if you leave it in the default location. If an error is raised when you try to attach the database, move the files, or check and fix file system permissions as appropriate.  
  
 **2. Attach the semantic language statistics database.**
   
 Attach the database to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] or by calling [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-sql-server-transact-sql.md) with the **FOR ATTACH** syntax. For more information, see [Database Detach and Attach &#40;SQL Server&#41;](../../relational-databases/databases/database-detach-and-attach-sql-server.md).  
  
 By default, the name of the database is **semanticsdb**. You can optionally give the database a different name when you attach it. You have to provide this name when you register the database in the subsequent step.  
  
```sql  
CREATE DATABASE semanticsdb  
            ON ( FILENAME = 'C:\Microsoft Semantic Language Database\semanticsdb.mdf' )  
            LOG ON ( FILENAME = 'C:\Microsoft Semantic Language Database\semanticsdb_log.ldf' )  
            FOR ATTACH;  
GO  
```  
  
 This code sample assumes that you have moved the database from its default location to a new location.  
  
 **3. Register the semantic language statistics database.** 
  
 Call the stored procedure [sp_fulltext_semantic_register_language_statistics_db &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-fulltext-semantic-register-language-statistics-db-transact-sql.md) and provide the name that you gave to the database when you attached it.  
  
```sql  
EXEC sp_fulltext_semantic_register_language_statistics_db @dbname = N'semanticsdb';  
GO  
```  

##  <a name="reqinstall"></a> Requirements and restrictions for the Semantic Language Statistics Database  
  
-   You can only attach and register one semantic language statistics database on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
     Each instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a single computer requires a separate physical copy of the semantic language statistics database. Attach one copy to each instance.  
  
-   You cannot detach a valid and registered semantic language statistics database and replace it with an arbitrary database that has the same name. Doing so will cause active or future index populations to fail.  
  
-   The semantic language statistics database is read-only. You cannot customize this database. If you alter the content of the database in any way, the results for future semantic indexing are indeterministic. To restore the original state of this data, you can drop the altered database, and download and attach a new and unaltered copy of the database.  
  
-   It is possible to detach or drop the semantic language statistics database. If there are any active indexing operations that have read locks on the database, then the detach or drop operation will fail or time out. This is consistent with existing behavior. After the database is removed, semantic indexing operations will fail.  
 
##  <a name="HowToUnregister"></a> Remove the Semantic Language Statistics Database  

###  Unregister, detach, and remove the Semantic Language Statistics Database 

 **1. Unregister the semantic language statistics database.**
   
 Call the stored procedure [sp_fulltext_semantic_unregister_language_statistics_db &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-fulltext-semantic-unregister-language-statistics-db-transact-sql.md). You do not have to provide the name of the database since an instance can have only one semantic language statistics database.  
  
```sql  
EXEC sp_fulltext_semantic_unregister_language_statistics_db;  
GO  
```  
  
 **2. Detach the semantic language statistics database.**  
 
 Call the stored procedure [sp_detach_db &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-detach-db-transact-sql.md) and provide the name of the database.  
  
```sql  
USE master;  
GO  
  
EXEC sp_detach_db @dbname = N'semanticsdb';  
GO  
```  
  
 **3. Remove the semantic language statistics database.**  
 
 After unregistering and detaching the database, you can simply delete the database file. There is no uninstall program and there is no entry in **Programs and Features** in the Control Panel.  
  
## Install optional support for newer document types  
  
###  <a name="office"></a> Install the latest filters for Microsoft Office and other Microsoft document types  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installs the latest [!INCLUDE[msCoName](../../includes/msconame-md.md)] word breakers and stemmers, but does not install the latest filters for [!INCLUDE[msCoName](../../includes/msconame-md.md)] Office documents and other [!INCLUDE[msCoName](../../includes/msconame-md.md)] document types. These filters are required for indexing documents created with recent versions of [!INCLUDE[msCoName](../../includes/msconame-md.md)] Office and other [!INCLUDE[msCoName](../../includes/msconame-md.md)] applications. To download the latest filters, see [Microsoft Office 2010 Filter Packs](https://go.microsoft.com/fwlink/?LinkId=218293). (There does not appear to be a Filter Pack release for Office 2013 or Office 2016.)
  
  

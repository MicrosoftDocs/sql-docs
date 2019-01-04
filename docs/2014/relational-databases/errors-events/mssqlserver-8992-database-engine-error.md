---
title: "MSSQLSERVER_8992 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "8992 (Database Engine error)"
ms.assetid: 68467e6a-09d8-478f-8bd9-3bb09453ada3
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_8992
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|8992|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC3_CHECK_CATALOG|  
|Message Text|Check Catalog Msg ERROR Level LEVEL State STATE: MESSAGE.|  
  
## Explanation  
 DBCC CHECKCATALOG or DBCC CHECKDB found an inconsistency in the system metadata tables for the specified object. That is, there is an inconsistency between the recorded object ID and the object specified in error message.  
  
 This error can occur when one or more system tables has been manually updated in a way that creates an inconsistency in the system metadata. For example, a user may have manually deleted an object from the **sysobjects** table without removing associated rows in other tables such as **sysindexes** and **syscolumns**.  
  
 This error can occur when running DBCC CHECKDB against a database that has been upgraded from SQL Server 2000 to SQL Server 2005 or later. In SQL Server 2000, DBCC CHECKDB did not include DBCC CHECKCATALOG functionality, so the error would not be caught before upgrade unless DBCC CHECKCATALOG was specifically executed against the database in SQL Server 2000.  
  
 You may see any of the following errors in conjunction with error 8992:  
  
 Msg 3851 - An invalid row (%ls) was found in the system table sys.%ls%ls.  
  
 Msg 3852 - Row (%ls) in sys.%ls%ls does not have a matching row (%ls) in sys.%ls%ls.  
  
 3853 - Attribute (%ls) of row (%ls) in sys.%ls%ls does not have a matching row (%ls) in sys.%ls%ls.  
  
 3854 - Attribute (%ls) of row (%ls) in sys.%ls%ls has a matching row (%ls) in sys.%ls%ls that is invalid.  
  
 3855 - Attribute (%ls) exists without a row (%ls) in sys.%ls%ls.  
  
 3856 - Attribute (%ls) exists but should not for row (%ls) in sys.%ls%ls.  
  
 3857 - The attribute (%ls) is required but is missing for row (%ls) in sys.%ls%ls.  
  
 3858 - The attribute (%ls) of row (%ls) in sys.%ls%ls has an invalid value.  
  
## User Action  
  
### Drop and Re-create the Specified Object  
 If possible, drop and recreate the specified object. For example, if the object is a stored procedure or user-defined type, recreating the object may resolve the problem.  
  
### Restore from Backup  
 If the problem is not hardware related and a known clean backup is available, restore the database from the backup. This action is only applicable if the backup does not contain the metadata error.  
  
### Export the Data to a New Database  
 If the backup also contains the metadata inconsistency, you need to create a new database and export the contents of the existing database to the new database.  
  
### DBCC CHECKDB Cannot Repair This Error  
 This error cannot be repaired.  If you cannot restore the database from a backup, contact [!INCLUDE[msCoName](../../includes/msconame-md.md)] Customer Service and Support (CSS).  
  
### Do Not Manually Update System Tables  
 Do not make manual updates to system tables. SQL Server does not support any manual changes to system databases. If you update a system table in a SQL Server database, two events (event ID 17659 and event ID 3859) are logged. For more information, see KB article 2688307, "Event ID 17659 and event ID 3859 are logged when you update system tables in a SQL Server database".  
  
## See Also  
 [Event ID 17659 and event ID 3859 are logged when you update system tables in a SQL Server database](https://support.microsoft.com/kb/2688307/EN-US)  
  
  

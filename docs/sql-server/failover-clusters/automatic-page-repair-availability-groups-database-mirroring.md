---
title: "Automatic page repair for availability groups & database mirroring"
description: Learn how to automatically repair certain types of page corruption when a database participates in an Always On availability group or database mirroring.
ms.custom: "seo-lt-2019"
ms.date: "05/17/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: high-availability
ms.topic: troubleshooting
helpviewer_keywords: 
  - "automatic page repair"
  - "Availability Groups [SQL Server], automatic page repair"
  - "database mirroring [SQL Server], automatic page repair"
  - "suspect pages [SQL Server]"
ms.assetid: cf2e3650-5fac-4f34-b50e-d17765578a8e
author: MashaMSFT
ms.author: mathoma
---
# Automatic Page Repair (Availability Groups: Database Mirroring)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Automatic page repair is supported by database mirroring and by [!INCLUDE[ssHADR](../../includes/sshadr-md.md)]. After certain types of errors corrupt a page, making it unreadable, a database mirroring partner (principal or mirror) or an availability replica (primary or secondary) attempts to automatically recover the page. The partner/replica that cannot read the page requests a fresh copy of the page from its partner or from another replica. If this request succeeds, the unreadable page is replaced by the readable copy, and this usually resolves the error.  
  
 Generally speaking, database mirroring and [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] handle I/O errors in equivalent ways. The few differences are explicitly called out here.  
  
> [!NOTE]  
>  Automatic page repair differs from DBCC repair. All of the data is preserved by an automatic page repair. In contrast, correcting errors by using the DBCC REPAIR_ALLOW_DATA_LOSS option might require that some pages, and therefore data, be deleted.  
  
-   [Error types that cause an automatic page-repair attempt](#ErrorTypes)  
  
-   [Page types that cannot be automatically repaired](#UnrepairablePageTypes)  
  
-   [Handling i/o errors on the principal/primary database](#PrimaryIOErrors)  
  
-   [Handling i/o errors on the mirror/secondary database](#SecondaryIOErrors)  
  
-   [Developer best practice](#DevBP)  
  
-   [How to: view automatic page-repair attempts](#ViewAPRattempts)  
  
##  <a name="ErrorTypes"></a> Error Types That Cause an Automatic Page-Repair Attempt  
 Database mirroring automatic page repair tries to repair only pages in a data file on which an operation has failed for one of the errors listed in the following table.  
  
|Error number|Description|Instances that cause automatic page-repair attempt|  
|------------------|-----------------|---------------------------------------------------------|  
|823|Action is taken only if the operating system performed a cyclic redundancy check (CRC) that failed on the data.|ERROR_CRC. The operating-system value for this error is 23.|  
|824|Logical errors.|Logical data errors, such as torn write or bad page checksum.|  
|829|A page has been marked as restore pending.|All.|  
  
 To view recent 823 CRC errors and 824 errors, see the [suspect_pages](../../relational-databases/system-tables/suspect-pages-transact-sql.md) table in the [msdb](../../relational-databases/databases/msdb-database.md) database.  

  
##  <a name="UnrepairablePageTypes"></a> Page Types That Cannot Be Automatically Repaired  
 Automatic page repair cannot repair the following control page types:  
  
-   File header page (page ID 0).  
  
-   Page 9 (the database boot page).  
  
-   Allocation pages: Global Allocation Map (GAM) pages, Shared Global Allocation Map (SGAM) pages, and Page Free Space (PFS) pages.  
  
 
##  <a name="PrimaryIOErrors"></a> Handling I/O Errors on the Principal/Primary Database  
 On the principal/primary database, automatic page repair is tried only when the database is in the SYNCHRONIZED state and the principal/primary is still sending log records for the database to the mirror/secondary. The basic sequence of actions in an automatic page-repair attempt are as follows:  
  
1.  When a read error occurs on a data page in the principal/primary database, the principal/primary inserts a row in the [suspect_pages](../../relational-databases/system-tables/suspect-pages-transact-sql.md) table with the appropriate error status. For database mirroring, the principal then requests a copy of the page from the mirror. For [!INCLUDE[ssHADR](../../includes/sshadr-md.md)], the primary broadcasts the request to all the secondaries and gets the page from the first to respond. The request specifies the page ID and the LSN that is currently at the end of the flushed log. The page is marked as *restore pending*. This makes it inaccessible during the automatic page-repair attempt. Attempts to access this page during the repair attempt will fail with error 829 (restore pending).  
  
2.  After receiving the page request, the mirror/secondary waits until it has redone the log up to the LSN specified in the request. Then, the mirror/secondary tries to access the page in its copy of the database. If the page can be accessed, the mirror/secondary sends the copy of the page to the principal/primary. Otherwise, the mirror/secondary returns an error to the principal/primary, and the automatic page-repair attempt fails.  
  
3.  The principal/primary processes the response that contains the fresh copy of the page.  
  
4.  After the automatic page-repair attempt fixes a suspect page, the page is marked in the **suspect_pages** table as restored (**event_type** = 5).  
  
5.  If the page I/O error caused any [deferred transactions](../../relational-databases/backup-restore/deferred-transactions-sql-server.md), after you repair the page, the principal/primary tries to resolve those transactions.  
  
 
##  <a name="SecondaryIOErrors"></a> Handling I/O Errors on the Mirror/Secondary Database  
 I/O errors on data pages that occur on the mirror/secondary database are handled in generally the same way by database mirroring and by [!INCLUDE[ssHADR](../../includes/sshadr-md.md)].  
  
1.  With database mirroring, if the mirror encounters one or more page I/O errors when it redoes a log record, the mirroring session enters the SUSPENDED state. With [!INCLUDE[ssHADR](../../includes/sshadr-md.md)], if a secondary replica encounters one or more page I/O errors when it redoes a log record, the secondary database enters the SUSPENDED state. At that point, the mirror/secondary inserts a row in the **suspect_pages** table with the appropriate error status. The mirror/secondary then requests a copy of the page from the principal/primary.  
  
2.  The principal/primary tries to access the page in its copy of the database. If the page can be accessed, the principal/primary sends the copy of the page to the mirror/secondary.  
  
3.  If the mirror/secondary receives copies of every page it has requested, the mirror/secondary tries to resume the mirroring session. If an automatic page-repair attempt fixes a suspect page, the page is marked in the **suspect_pages** table as restored (**event_type** = 4).  
  
     If a mirror/secondary does not receive a page that it requested from the principal/primary, the automatic page-repair attempt fails. With database mirroring, the mirroring session remains suspended. With [!INCLUDE[ssHADR](../../includes/sshadr-md.md)], the secondary database remains suspended. If the mirroring session or secondary database is resumed manually, the corrupted pages will be hit again during the synchronization phase.  
  
 
##  <a name="DevBP"></a> Developer Best Practice  
 An automatic page repair is an asynchronous process that runs in the background. Therefore, a database operation that requests an unreadable page fails and returns the error code for whatever condition caused the failure. When developing an application for a mirrored database or an availability database, you should intercept exceptions for failed operations. If the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error code is 823, 824, or 829, you should retry the operation later.  
  

##  <a name="ViewAPRattempts"></a> How To: View Automatic Page-Repair Attempts  
 The following dynamic management views return rows for the latest automatic page-repair attempts on a given availability database or mirrored database, with a maximum of 100 rows per database.  
  
-   **Always On Availability Groups:**  
  
     [sys.dm_hadr_auto_page_repair &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-hadr-auto-page-repair-transact-sql.md)  
  
     Returns a row for every automatic page-repair attempt on any availability database on an availability replica that is hosted for any availability group by the server instance.  
  
-   **Database mirroring:**  
  
     [sys.dm_db_mirroring_auto_page_repair &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-mirroring-sys-dm-db-mirroring-auto-page-repair.md)  
  
     Returns a row for every automatic page-repair attempt on any mirrored database on the server instance.  
  
 
## See Also  
 [Manage the suspect_pages Table &#40;SQL Server&#41;](../../relational-databases/backup-restore/manage-the-suspect-pages-table-sql-server.md)   
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md)  
  
  



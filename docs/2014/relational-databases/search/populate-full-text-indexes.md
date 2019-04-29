---
title: "Populate Full-Text Indexes | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "index populations [full-text search]"
  - "incremental populations [full-text search]"
  - "populations [full-text search]"
  - "full-text search [SQL Server], populations"
  - "crawls [full-text search]"
  - "automatic full-text index updates"
  - "change tracking-based populations [full-text search]"
  - "manual updates [full-text search]"
  - "manual populations [full-text search]"
  - "auto populations [full-text search]"
  - "full-text indexes [SQL Server], key column"
  - "full populations [full-text search]"
  - "full-text indexes [SQL Server], populations"
ms.assetid: 76767b20-ef55-49ce-8dc4-e77cb8ff618a
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Populate Full-Text Indexes
  Creating and maintaining a full-text index involves populating the index by using a process called a *population* (also known as a *crawl*).  
  
##  <a name="types"></a> Types of Population  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports the following types of population: full population, change tracking-based automatic or manual population, and incremental timestamp-based population.  
  
### Full Population  
 During a full population, index entries are built for all the rows of a table or indexed view. A full population of a full-text index, builds index entries for all the rows of the base table or indexed view.  
  
 By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] populates a new full-text index fully as soon as it is created. However, a full population can consume a significant amount of resources. Therefore, when creating a full-text index during peak periods, it is often a best practice to delay the full population until an off-peak time, particularly if the base table of an full-text index is large. However, the full-text catalog to which the index belongs is not usable until all of its full-text indexes are populated. To create a full-text index without populating it immediately, specify the CHANGE_TRACKING OFF, NO POPULATION clause in the CREATE FULLTEXT INDEX statement. If you specify CHANGE_TRACKING MANUAL, the Full-Text Engine uses statement. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will not populate the new full-text index until you execute an ALTER FULLTEXT INDEX statement using the START FULL POPULATION or START INCREMENTAL POPULATION clause. For more information, see examples "A. Creating a full-text index without running a full population" and "B. Running a full population on table," later in this topic.  
  

  
### Change Tracking-Based Population  
 Optionally, you can use change tracking to maintain a full-text index after its initial full population. There is a small overhead associated with change tracking because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] maintains a table in which it tracks changes to the base table since the last population. When change tracking is used, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] maintains a record of the rows in the base table or indexed view that have been modified by updates, deletes, or inserts. Data changes through WRITETEXT and UPDATETEXT are not reflected in the full-text index, and are not picked up with change tracking.  
  
> [!NOTE]  
>  For tables containing a `timestamp` column, you can use incremental populations.  
  
 When change tracking is enabled during index creation, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] fully populates the new full-text index immediately after it is created. Thereafter, changes are tracked and propagated to the full-text index. There are two types of change tracking, automatic (CHANGE_TRACKING AUTO option) and manual (CHANGE_TRACKING MANUAL option). Automatic change tracking is the default behavior.  
  
 The type of change tracking determines how the full-text index is populated, as follows:  
  
-   Automatic population  
  
     By default, or if you specify CHANGE_TRACKING AUTO, the Full-Text Engine uses automatic population on the full-text index. After the initial full population completes, changes are tracked as data is modified in the base table, and the tracked changes are propagated automatically. The full-text index is updated in the background, however, so propagated changes might not be reflected immediately in the index.  
  
     **To set up tracking changes with automatic population**  
  
    -   [CREATE FULLTEXT INDEX](/sql/t-sql/statements/create-fulltext-index-transact-sql) ... WITH CHANGE_TRACKING AUTO  
  
    -   [ALTER FULLTEXT INDEX](/sql/t-sql/statements/alter-fulltext-index-transact-sql) ... SET CHANGE_TRACKING AUTO  
  
     For more information, see example "E. Altering a full-text index to use automatic change tracking," later in this topic.  
  
-   Manual population  
  
     If you specify CHANGE_TRACKING MANUAL, the Full-Text Engine uses manual population on the full-text index. After the initial full population completes, changes are tracked as data is modified in the base table. However, they are not propagated to the full-text index until you execute an ALTER FULLTEXT INDEX ... START UPDATE POPULATION statement. You can use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to call this [!INCLUDE[tsql](../../includes/tsql-md.md)] statement periodically.  
  
     **To start tracking changes with manual population**  
  
    -   [CREATE FULLTEXT INDEX](/sql/t-sql/statements/create-fulltext-index-transact-sql) ... WITH CHANGE_TRACKING MANUAL  
  
    -   [ALTER FULLTEXT INDEX](/sql/t-sql/statements/alter-fulltext-index-transact-sql) ... SET CHANGE_TRACKING MANUAL  
  
     For more information, see examples "C. Creating a full-text index with manual change tracking" and "D. Running a manual population," later in this topic.  
  
 **To turn off change tracking**  
  
-   [CREATE FULLTEXT INDEX](/sql/t-sql/statements/create-fulltext-index-transact-sql) ... WITH CHANGE_TRACKING OFF  
  
-   [ALTER FULLTEXT INDEX](/sql/t-sql/statements/alter-fulltext-index-transact-sql) ... SET CHANGE_TRACKING OFF  
  

  
### Incremental Timestamp-Based Population  
 An incremental population is an alternative mechanism for manually populating a full-text index. You can run an incremental population for a full-text index that has CHANGE_TRACKING set to MANUAL or OFF. If the first population on a full-text index is an incremental population, it indexes all rows, making it equivalent to a full population.  
  
 The requirement for incremental population is that the indexed table must have a column of the `timestamp` data type. If a `timestamp` column does not exist, incremental population cannot be performed. A request for incremental population on a table without a `timestamp` column results in a full population operation. Also, if any metadata that affects the full-text index for the table has changed since the last population, incremental population requests are implemented as full populations. This includes metadata changes caused by altering any column, index, or full-text index definitions.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the `timestamp` column to identify rows that have changed since the last population. The incremental population then updates the full-text index for rows added, deleted, or modified after the last population, or while the last population was in progress. If a table experiences a high volume of inserts, using incremental population can be more efficient that using manual population.  
  
 At the end of a population, the Full-Text Engine records a new `timestamp` value. This value is the largest `timestamp` value that SQL Gatherer has encountered. This value will be used when a subsequent incremental population starts.  
  
 To run an incremental population, execute an ALTER FULLTEXT INDEX statement using the START INCREMENTAL POPULATION clause.  
  

  
##  <a name="examples"></a> Examples of Populating Full-Text Indexes  
  
> [!NOTE]  
>  The examples in this section use the `Production.Document` or `HumanResources.JobCandidate` table of the `AdventureWorks` sample database.  
  
### A. Creating a full-text index without running a full population  
 The following example creates a full-text index on the `Production.Document` table of the `AdventureWorks` sample database. This example uses WITH CHANGE_TRACKING OFF, NO POPULATION to delay the initial full population.  
  
```  
CREATE UNIQUE INDEX ui_ukDoc ON Production.Document(DocumentID);  
CREATE FULLTEXT CATALOG AW_Production_FTCat;  
CREATE FULLTEXT INDEX ON Production.Document  
(  
    Document                         --Full-text index column name   
        TYPE COLUMN FileExtension    --Name of column that contains file type information  
        Language 1033                 --1033 is LCID for the English language  
)  
    KEY INDEX ui_ukDoc  
    ON AW_Production_FTCat  
    WITH CHANGE_TRACKING OFF, NO POPULATION;  
GO  
  
```  
  
### B. Running a full population on table  
 The following example runs a full population on the `Production.Document` table of the `AdventureWorks` sample database.  
  
```  
ALTER FULLTEXT INDEX ON Production.Document  
   START FULL POPULATION;  
```  
  
### C. Creating a full-text index with manual change tracking  
 The following example creates a full-text index that will use change tracking with manual population on the `HumanResources.JobCandidate` table of the `AdventureWorks` sample database.  
  
```  
USE AdventureWorks;  
GO  
CREATE UNIQUE INDEX ui_ukJobCand ON HumanResources.JobCandidate(JobCandidateID);  
CREATE FULLTEXT CATALOG ft AS DEFAULT;  
CREATE FULLTEXT INDEX ON HumanResources.JobCandidate(Resume)   
   KEY INDEX ui_ukJobCand   
   WITH CHANGE_TRACKING=MANUAL;  
GO  
```  
  
### D. Running a manual population  
 The following example runs a manual population on the change-tracked full-text index of the `HumanResources.JobCandidate` table of the `AdventureWorks` sample database.  
  
```  
USE AdventureWorks;  
GO  
ALTER FULLTEXT INDEX ON HumanResources.JobCandidate START UPDATE POPULATION;  
GO  
```  
  
### E. Altering a full-text index to use automatic change tracking  
 The following example changes the full-text index of the `HumanResources.JobCandidate` table of the `AdventureWorks` sample database to use change tracking with automatic population.  
  
```  
USE AdventureWorks;  
GO  
ALTER FULLTEXT INDEX ON HumanResources.JobCandidate SET CHANGE_TRACKING AUTO;  
GO   
```  
  

  
##  <a name="create"></a> Creating or Changing a Schedule for Incremental Population  
  
#### To create or change a schedule for incremental population in Management Studio  
  
1.  In Object Explorer, expand the server.  
  
2.  Expand **Databases**, and then expand the database that contains the full-text index.  
  
3.  Expand **Tables**.  
  
 Right-click the table on which the full-text index is defined, select **Full-Text index**, and on the **Full-Text index** context menu, click **Properties**. This opens the **Full-text index Properties** dialog box.  
  
1.  In the **Select a page** pane, select Schedules.  
  
     Use this page to create or manage schedules for a SQL Server Agent job that starts an incremental table population on the base table or indexed view of the full-text index.  
  
    > [!IMPORTANT]  
    >  If the base table or view does not contain a column of the `timestamp` data type, a full population is performed.  
  
     The options are as follows:  
  
    -   To create a new schedule, click **New**.  
  
         This opens the **New Full-Text Indexing Table Schedule** dialog box, where you can create a schedule. To save the schedule, click **OK**.  
  
        > [!IMPORTANT]  
        >  A SQL Server Agent job (Start Incremental Table Population on *database_name*.*table_name*) is associated with a new schedule after you exit the **Full-Text Index Properties** dialog box. If you create multiple schedules for the full-text index, they all use the same job.  
  
    -   To change a schedule, select it and click **Edit**.  
  
         This opens the **New Full-Text Indexing Table Schedule** dialog box, where you can modify the schedule.  
  
        > [!NOTE]  
        >  For information about modifying a job, see [Modify a Job](../../ssms/agent/modify-a-job.md).  
  
    -   To remove a schedule, select it and click **Delete**.  
  
2.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  

  
##  <a name="crawl"></a> Troubleshooting Errors in a Full-Text Population (Crawl)  
 When an error occurs during a crawl, the Full-Text Search crawl logging facility creates and maintains a crawl log, which is a plain text file. Each crawl log corresponds to a particular full-text catalog. By default crawl logs for a given instance, in this case, the first instance, are located in %ProgramFiles%\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\LOG folder. The crawl log file follows the following naming scheme:  
  
 SQLFT\<DatabaseID>\<FullTextCatalogID>.LOG[\<n>]  
  
 <`DatabaseID`>  
 The ID of a database. <`dbid`> is a five digit number with leading zeros.  
  
 <`FullTextCatalogID`>  
 Full-text catalog ID. <`catid`> is a five digit number with leading zeros.  
  
 <`n`>  
 Is an integer that indicates one or more crawl logs of the same full-text catalog exist.  
  
 For example, SQLFT0000500008.2 is the crawl log file for a database with database ID = 5, and full-text catalog ID = 8. The 2 at the end of the file name indicates that there are two crawl log files for this database/catalog pair.  
  

  
## See Also  
 [sys.dm_fts_index_population &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-fts-index-population-transact-sql)   
 [Get Started with Full-Text Search](get-started-with-full-text-search.md)   
 [Create and Manage Full-Text Indexes](create-and-manage-full-text-indexes.md)   
 [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-fulltext-index-transact-sql)   
 [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-fulltext-index-transact-sql)  
  
  

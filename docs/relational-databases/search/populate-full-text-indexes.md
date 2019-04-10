---
title: "Populate Full-Text Indexes | Microsoft Docs"
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "search, sql-database"
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
author: pmasl
ms.author: pelopes
ms.reviewer: mikeray
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Populate Full-Text Indexes
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  Creating and maintaining a full-text index involves populating the index by using a process called a *population* (also known as a *crawl*).  
  
##  <a name="types"></a> Types of population  
A full-text index supports the following types of population:
-   **Full** population
-   Automatic or manual population based on **change tracking**
-   Incremental population based on a **timestamp**
  
## Full population  
 During a full population, index entries are built for all the rows of a table or indexed view. A full population of a full-text index, builds index entries for all the rows of the base table or indexed view.  
  
By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] populates a new full-text index fully as soon as it is created.
-   On the one hand, a full population can consume a significant amount of resources. Therefore, when creating a full-text index during peak periods, it is often a best practice to delay the full population until an off-peak time, particularly if the base table of an full-text index is large.
-   On the other hand, the full-text catalog to which the index belongs is not usable until all of its full-text indexes are populated.

To create a full-text index without populating it immediately, specify the `CHANGE_TRACKING OFF, NO POPULATION` clause in the `CREATE FULLTEXT INDEX` statement. If you specify `CHANGE_TRACKING MANUAL`, the Full-Text Engine doesn't populate the new full-text index until you execute an `ALTER FULLTEXT INDEX` statement using the `START FULL POPULATION` or `START INCREMENTAL POPULATION` clause. 

### Example - Create a full-text index without running a full population  
 The following example creates a full-text index on the `Production.Document` table of the `AdventureWorks` sample database. This example uses `WITH CHANGE_TRACKING OFF, NO POPULATION` to delay the initial full population.  
  
```sql
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
  
### Example - Run a full population on a table  
 The following example runs a full population on the `Production.Document` table of the `AdventureWorks` sample database.  
  
```sql
ALTER FULLTEXT INDEX ON Production.Document  
   START FULL POPULATION;  
```  
   
## Population based on change tracking
 Optionally, you can use change tracking to maintain a full-text index after its initial full population. There is a small overhead associated with change tracking because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] maintains a table in which it tracks changes to the base table since the last population. When you use change tracking, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] maintains a record of the rows in the base table or indexed view that have been modified by updates, deletes, or inserts. Data changes made through WRITETEXT and UPDATETEXT are not reflected in the full-text index, and are not picked up with change tracking.  
  
> [!NOTE]  
>  For tables containing a **timestamp** column, you can use incremental population instead of change tracking.  
  
 When you enable change tracking during index creation, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] fully populates the new full-text index immediately after it is created. Thereafter, changes are tracked and propagated to the full-text index.

### Enable change tracking
There are two types of change tracking:
-   Automatic (`CHANGE_TRACKING AUTO` option). Automatic change tracking is the default behavior.
-   Manual (`CHANGE_TRACKING MANUAL` option).   
  
 The type of change tracking determines how the full-text index is populated, as follows:  
  
-   **Automatic population**  
  
     By default, or if you specify `CHANGE_TRACKING AUTO`, the Full-Text Engine uses automatic population on the full-text index. After the initial full population completes, changes are tracked as data is modified in the base table, and the tracked changes are propagated automatically. The full-text index is updated in the background, however, so propagated changes might not be reflected immediately in the index.  
  
     **To start tracking changes with automatic population**  
  
    -   [CREATE FULLTEXT INDEX](../../t-sql/statements/create-fulltext-index-transact-sql.md) ... WITH CHANGE_TRACKING AUTO  
  
    -   [ALTER FULLTEXT INDEX](../../t-sql/statements/alter-fulltext-index-transact-sql.md) ... SET CHANGE_TRACKING AUTO  
  
    **Example - Alter a full-text index to use automatic change tracking**  
    The following example changes the full-text index of the `HumanResources.JobCandidate` table of the `AdventureWorks` sample database to use change tracking with automatic population.  
  
    ```sql  
    USE AdventureWorks;  
    GO  
    ALTER FULLTEXT INDEX ON HumanResources.JobCandidate SET CHANGE_TRACKING AUTO;  
    GO   
    ```  
  
-   **Manual population**  
  
     If you specify CHANGE_TRACKING MANUAL, the Full-Text Engine uses manual population on the full-text index. After the initial full population completes, changes are tracked as data is modified in the base table. However, they are not propagated to the full-text index until you execute an ALTER FULLTEXT INDEX ... START UPDATE POPULATION statement. You can use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to call this [!INCLUDE[tsql](../../includes/tsql-md.md)] statement periodically.  
  
     **To start tracking changes with manual population**  
  
    -   [CREATE FULLTEXT INDEX](../../t-sql/statements/create-fulltext-index-transact-sql.md) ... WITH CHANGE_TRACKING MANUAL  
  
    -   [ALTER FULLTEXT INDEX](../../t-sql/statements/alter-fulltext-index-transact-sql.md) ... SET CHANGE_TRACKING MANUAL  
  
    **Example - Create a full-text index with manual change tracking**  
    The following example creates a full-text index that will use change tracking with manual population on the `HumanResources.JobCandidate` table of the `AdventureWorks` sample database.  
  
    ```sql
    USE AdventureWorks;  
    GO  
    CREATE UNIQUE INDEX ui_ukJobCand ON HumanResources.JobCandidate(JobCandidateID);  
    CREATE FULLTEXT CATALOG ft AS DEFAULT;  
    CREATE FULLTEXT INDEX ON HumanResources.JobCandidate(Resume)   
       KEY INDEX ui_ukJobCand   
       WITH CHANGE_TRACKING=MANUAL;  
    GO  
    ```  
  
    **Example - Run a manual population**  
    The following example runs a manual population on the change-tracked full-text index of the `HumanResources.JobCandidate` table of the `AdventureWorks` sample database.  
  
    ```sql 
    USE AdventureWorks;  
    GO  
    ALTER FULLTEXT INDEX ON HumanResources.JobCandidate START UPDATE POPULATION;  
    GO  
    ```
   
### Disable change tracking 
  
-   [CREATE FULLTEXT INDEX](../../t-sql/statements/create-fulltext-index-transact-sql.md) ... WITH CHANGE_TRACKING OFF  
  
-   [ALTER FULLTEXT INDEX](../../t-sql/statements/alter-fulltext-index-transact-sql.md) ... SET CHANGE_TRACKING OFF  
   
  
## Incremental population based on a timestamp  
 An incremental population is an alternative mechanism for manually populating a full-text index. If a table experiences a high volume of inserts, using incremental population can be more efficient that using manual population.
 
 You can run an incremental population for a full-text index that has CHANGE_TRACKING set to MANUAL or OFF. 
  
 The requirement for incremental population is that the indexed table must have a column of the **timestamp** data type. If a **timestamp** column does not exist, incremental population cannot be performed.   

 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the **timestamp** column to identify rows that have changed since the last population. The incremental population then updates the full-text index for rows added, deleted, or modified after the last population, or while the last population was in progress. At the end of a population, the Full-Text Engine records a new **timestamp** value. This value is the largest **timestamp** value that SQL Gatherer has found. This value will be used when the next incremental population starts.  
 
In some cases, the request for an incremental population results in a full population.
-   A request for incremental population on a table without a **timestamp** column results in a full population operation.
-   If the first population on a full-text index is an incremental population, it indexes all rows, making it equivalent to a full population. 
-   If any metadata that affects the full-text index for the table has changed since the last population, incremental population requests are implemented as full populations. This includes metadata changes caused by altering any column, index, or full-text index definitions. 

### Run an incremental population
  
 To run an incremental population, execute an `ALTER FULLTEXT INDEX` statement using the `START INCREMENTAL POPULATION` clause.  
  
###  <a name="create"></a> Create or change a schedule for incremental population   
  
1.  In Management Studio, in Object Explorer, expand the server.  
  
2.  Expand **Databases**, and then expand the database that contains the full-text index.  
  
3.  Expand **Tables**.  
  
    Right-click the table on which the full-text index is defined, select **Full-Text index**, and on the **Full-Text index** context menu, click **Properties**. This opens the **Full-text index Properties** dialog box.  

    > [!IMPORTANT]  
    >  If the base table or view does not contain a column of the **timestamp** data type, incremental population is not possible.
      
1.  In the **Select a page** pane, select **Schedules**.  
  
     Use this page to create or manage schedules for a SQL Server Agent job that starts an incremental table population on the base table or indexed view of the full-text index.  

     The options are as follows:  
  
    -   To **create** a new schedule, click **New**.  
  
        This opens the **New Full-Text Indexing Table Schedule** dialog box, where you can create a schedule. To save the schedule, click **OK**.  
  
        > [!IMPORTANT]  
        >  A SQL Server Agent job (Start Incremental Table Population on *database_name*.*table_name*) is associated with a new schedule after you exit the **Full-Text Index Properties** dialog box. If you create multiple schedules for the same full-text index, they all use the same job.  
  
    -   To **change** an existing schedule, select the existing schedule and click **Edit**.  
  
         This opens the **New Full-Text Indexing Table Schedule** dialog box, where you can modify the schedule.  
  
        > [!NOTE]  
        >  For information about modifying a SQL Server Agent job, see [Modify a Job](../../ssms/agent/modify-a-job.md).  
  
    -   To **remove** an existing schedule, select the existing schedule and click **Delete**.  
  
2.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]   
  
##  <a name="crawl"></a> Troubleshoot errors in a full-text population (crawl)  
When an error occurs during a crawl, the Full-Text Search crawl logging facility creates and maintains a crawl log, which is a plain text file. Each crawl log corresponds to a particular full-text catalog. By default, crawl logs for a given instance (in this example, the default instance) are located in `%ProgramFiles%\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\LOG` folder.
 
The crawl log file follows the following naming scheme:  
  
`SQLFT<DatabaseID><FullTextCatalogID>.LOG[<n>]`
  
The variable parts of the crawl log file name are the following.
-   <**DatabaseID**> - The ID of a database. \<**dbid**> is a five digit number with leading zeros.  
-   <**FullTextCatalogID**> - Full-text catalog ID. \<**catid**> is a five digit number with leading zeros.  
-   <**n**> - Is an integer that indicates one or more crawl logs of the same full-text catalog exist.  
  
 For example, `SQLFT0000500008.2` is the crawl log file for a database with database ID = 5, and full-text catalog ID = 8. The 2 at the end of the file name indicates that there are two crawl log files for this database/catalog pair.  

## See Also  
 [sys.dm_fts_index_population &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-population-transact-sql.md)   
 [Get Started with Full-Text Search](../../relational-databases/search/get-started-with-full-text-search.md)   
 [Create and Manage Full-Text Indexes](../../relational-databases/search/create-and-manage-full-text-indexes.md)   
 [CREATE FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-fulltext-index-transact-sql.md)   
 [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-index-transact-sql.md)  
  
  

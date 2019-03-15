---
title: "Create and Manage Full-Text Catalogs | Microsoft Docs"
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "search, sql-database"
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "full-text catalogs [SQL Server], creating"
  - "full-text search [SQL Server], using SQL Server Management Studio"
ms.assetid: 824b7131-44a6-4815-89e6-62b7bab060e3
author: pmasl
ms.author: pelopes
ms.reviewer: mikeray
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create and Manage Full-Text Catalogs
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
A full-text catalog is a logical container for a group of full-text indexes. You have to create a full-text catalog before you can create a full-text index.

A full-text catalog is a virtual object that does not belong to any filegroup.
  
##  <a name="creating"></a> Create a Full-Text Catalog  

### Create a full-text catalog with Transact-SQL
Use [CREATE FULLTEXT CATALOG](../../t-sql/statements/create-fulltext-catalog-transact-sql.md). For example:

```sql 
USE AdventureWorks;  
GO  
CREATE FULLTEXT CATALOG ftCatalog AS DEFAULT;  
GO  
``` 

### Create a full-text catalog with Management Studio
1.  In Object Explorer, expand the server, expand **Databases**, and expand the database in which you want to create the full-text catalog.  
  
2.  Expand **Storage**, and then right-click **Full Text Catalogs**.  
  
3.  Select **New Full-Text Catalog**.  
  
4.  In the **New Full-Text Catalog** dialog box, specify the information for the catalog that you are re-creating. For more information, see [New Full-Text Catalog &#40;General Page&#41;](https://msdn.microsoft.com/library/5ed6f7cd-d9af-4439-9f33-fc935b883d91).  
  
    > [!NOTE]  
    >  Full-text catalog IDs begin at 00005 and are incremented by one for each new catalog created.  
  
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
##  <a name="props"></a> Get the properties of a full-text catalog  
Use the [!INCLUDE[tsql](../../includes/tsql-md.md)] function **FULLTEXTCATALOGPROPERTY** to get the value of various properties related to full-text catalogs. For more info, see [FULLTEXTCATALOGPROPERTY](../../t-sql/functions/fulltextcatalogproperty-transact-sql.md).

For example, run the following query to get the count of indexes in the full-text catalog `Catalog1`.

```sql 
USE <database>;  
GO  
SELECT fulltextcatalogproperty('Catalog1', 'ItemCount');  
GO  
```  
  
The following table lists the properties that are related to full-text catalogs. This information may be useful for administering and troubleshooting full-text search. 
  
|Property|Description|  
|--------------|-----------------|  
|**AccentSensitivity**|Accent-sensitivity setting.|
|**ImportStatus**|Whether the full-text catalog is being imported.|  
|**IndexSize**|Size of the full-text catalog in megabytes (MB).| 
|**ItemCount**|Number of full-text indexed items currently in the full-text catalog.|  
|**MergeStatus**|Whether a master merge is in progress.| 
|**PopulateCompletionAge**|Difference in seconds between the completion of the last full-text index population and 01/01/1990 00:00:00.| 
|**PopulateStatus**|Populate status.<br /><br /> [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]|  
|**UniqueKeyCount**|Number of unique keys in the full-text catalog.| 

##  <a name="rebuildone"></a> Rebuild a full-text catalog  

Run the Transact-SQL statement [ALTER FULLTEXT CATALOG ... REBUILD](
../../t-sql/statements/alter-fulltext-catalog-transact-sql.md), or do the following things in SQL Server Management Studio (SSMS).

1.  In SSMS, in Object Explorer, expand the server, expand **Databases**, and then expand the database that contains the full-text catalog that you want to rebuild.  
  
2.  Expand **Storage**, and then expand **Full Text Catalogs**.  
  
3.  Right-click the name of the full-text catalog that you want to rebuild, and select **Rebuild**.  
  
4.  To the question **Do you want to delete the full-text catalog and rebuild it?**, click **OK**.  
  
5.  In the **Rebuild Full-Text Catalog** dialog box, click **Close**.  
   
##  <a name="rebuildall"></a> Rebuild all full-text catalogs for a database  

1.  In SSMS, in Object Explorer, expand the server, expand **Databases**, and then expand the database that contains the full-text catalogs that you want to rebuild.  
  
2.  Expand **Storage**, and then right-click **Full Text Catalogs**.  
  
3.  Select **Rebuild All**.  
  
4.  To the question, **Do you want to delete all full-text catalogs and rebuild them?**, click **OK**.  
  
5.  In the **Rebuild All Full-Text Catalogs** dialog box, click **Close**.  
  
  
  
##  <a name="removing"></a> Remove a full-text catalog from a database  

Run the Transact-SQL statement [DROP FULLTEXT CATALOG](
../../t-sql/statements/drop-fulltext-catalog-transact-sql.md), or do the following things in SQL Server Management Studio (SSMS).

1.  In SSMS, in Object Explorer, expand the server, expand **Databases**, and expand the database that contains the full-text catalog you want to remove.  
  
2.  Expand **Storage**, and expand **Full Text Catalogs**.  
  
3.  Right-click the full-text catalog that you want to remove, and then select **Delete**.  
  
4.  In the **Delete Objects** dialog box, click **OK**.  

## Next step
[Create and Manage Full-Text Indexes](../../relational-databases/search/create-and-manage-full-text-indexes.md)

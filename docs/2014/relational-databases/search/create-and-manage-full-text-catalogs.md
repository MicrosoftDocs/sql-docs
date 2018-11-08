---
title: "Create and Manage Full-Text Catalogs | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: search
ms.topic: conceptual
helpviewer_keywords: 
  - "full-text catalogs [SQL Server], creating"
  - "full-text search [SQL Server], using SQL Server Management Studio"
ms.assetid: 824b7131-44a6-4815-89e6-62b7bab060e3
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Create and Manage Full-Text Catalogs
  A full-text catalog is a virtual object that does not belong to any filegroup; it is a logical concept that refers to a group of full-text indexes.  
  
##  <a name="creating"></a> Creating a Full-Text Catalog  
  
#### To create a full-text catalog  
  
1.  In Object Explorer, expand the server, expand **Databases**, and expand the database in which you want to create the full-text catalog.  
  
2.  Expand **Storage**, and then right-click **Full Text Catalogs**.  
  
3.  Select **New Full-Text Catalog**.  
  
4.  In the **New Full-Text Catalog** dialog box, specify the information for the catalog that you are re-creating. For more information, see [New Full-Text Catalog &#40;General Page&#41;](../../integration-services/general-page-of-integration-services-designers-options.md).  
  
    > [!NOTE]  
    >  Full-text catalog IDs begin at 00005 and are incremented by one for each new catalog created.  
  
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
  
  
##  <a name="props"></a> Viewing the Properties of a Full-Text Catalog  
 [!INCLUDE[tsql](../../includes/tsql-md.md)] functions such as FULLTEXTCATALOGPROPERTY can be used to obtain the value of various properties related to full-text indexing. This information is useful for administering and troubleshooting full-text search.  
  
 The following table lists the properties that are related to full-text catalogs.  
  
|Property|Description|Function|  
|--------------|-----------------|--------------|  
|`AccentSensitivity`|Accent-sensitivity setting.|[FULLTEXTCATALOGPROPERTY](/sql/t-sql/functions/fulltextcatalogproperty-transact-sql)|  
|`ImportStatus`|Whether the full-text catalog is being imported.|FULLTEXTCATALOGPROPERTY|  
|`IndexSize`|Size of the full-text catalog in megabytes (MB).|FULLTEXTCATALOGPROPERTY|  
|`ItemCount`|Number of full-text indexed items currently in the full-text catalog.|FULLTEXTCATALOGPROPERTY|  
|`MergeStatus`|Whether a master merge is in progress.|FULLTEXTCATALOGPROPERTY|  
|`PopulateCompletionAge`|Difference in seconds between the completion of the last full-text index population and 01/01/1990 00:00:00.|FULLTEXTCATALOGPROPERTY|  
|`PopulateStatus`|Populate status.<br /><br /> [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]|FULLTEXTCATALOGPROPERTY|  
|`UniqueKeyCount`|Number of unique keys in the full-text catalog.|FULLTEXTCATALOGPROPERTY|  
  
  
  
##  <a name="rebuildone"></a> Rebuilding a Full-Text Catalog  
  
#### To rebuild a full-text catalog  
  
1.  In Object Explorer, expand the server, expand **Databases**, and then expand the database that contains the full-text catalog that you want to rebuild.  
  
2.  Expand **Storage**, and then expand **Full Text Catalogs**.  
  
3.  Right-click the name of the full-text catalog that you want to rebuild, and select **Rebuild**.  
  
4.  To the question **Do you want to delete the full-text catalog and rebuild it?**, click **OK**.  
  
5.  In the **Rebuild Full-Text Catalog** dialog box, click **Close**.  
  
  
  
##  <a name="rebuildall"></a> Rebuilding All Full-Text Catalogs for a Database  
  
#### To rebuild the full-text catalogs for a database  
  
1.  In Object Explorer, expand the server, expand **Databases**, and then expand the database that contains the full-text catalogs that you want to rebuild.  
  
2.  Expand **Storage**, and then right-click **Full Text Catalogs**.  
  
3.  Select **Rebuild All**.  
  
4.  To the question, **Do you want to delete all full-text catalogs and rebuild them?**, click **OK**.  
  
5.  In the **Rebuild All Full-Text Catalogs** dialog box, click **Close**.  
  
  
  
##  <a name="removing"></a> Removing a Full-Text Catalog from a Database  
  
#### To remove a full-text catalog from a database  
  
1.  In Object Explorer, expand the server, expand **Databases**, and expand the database that contains the full-text catalog you want to remove.  
  
2.  Expand **Storage**, and expand **Full Text Catalogs**.  
  
3.  Right-click the full-text catalog that you want to remove, and then select **Delete**.  
  
4.  In the **Delete Objects** dialog box, click **OK**.  
  
  
  
## See Also  
 [CREATE FULLTEXT CATALOG &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-fulltext-catalog-transact-sql)  
  
  

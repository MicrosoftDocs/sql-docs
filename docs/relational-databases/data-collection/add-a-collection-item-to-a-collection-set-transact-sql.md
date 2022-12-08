---
description: "Add a Collection Item to a Collection Set (Transact-SQL)"
title: "Add Collection Item to Collection Set (T-SQL)"
ms.date: 06/03/2020
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "collection items [SQL Server]"
  - "collection sets [SQL Server], adding items"
ms.assetid: 9fe6454e-8c0e-4b50-937b-d9871b20fd13
author: MashaMSFT
ms.author: mathoma
ms.custom: "seo-lt-2019"
---
# Add a Collection Item to a Collection Set (Transact-SQL)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  You can add a collection item to an existing collection set using the stored procedures that are provided with the data collector.  
  
 Carry out the following steps using Query Editor in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
### Add a collection item to a collection set  
  
1.  Stop the collection set that you want to add the item to by running the **sp_syscollector_stop_collection_set** stored procedure. For example, to stop a collection set that is named "Test Collection Set", run the following statements:  
  
    ```sql  
    USE msdb;
    DECLARE @csid int;

    SELECT @csid = collection_set_id  
      FROM syscollector_collection_sets
      WHERE name = 'Test Collection Set';

    SELECT @csid;
    EXEC dbo.sp_syscollector_stop_collection_set @collection_set_id = @csid;
    ```  
  
    > [!NOTE]  
    >  You can also stop the collection set by using Object Explorer in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For more information, see [Start or Stop a Collection Set](../../relational-databases/data-collection/start-or-stop-a-collection-set.md).  
  
2.  Declare the collection set that you want to add the collection item to. The following code provides an example of how to declare the collection set ID.  
  
    ```sql  
    DECLARE @collection_set_id_1 int;

    SELECT @collection_set_id_1 = collection_set_id
      FROM [msdb].[dbo].[syscollector_collection_sets]
      WHERE name = N'Test Collection Set'; -- name of collection set  
    ```  
  
3.  Declare the collector type. The following code provides an example of how to declare the Generic T-SQL Query collector type.  
  
    ```sql  
    DECLARE @collector_type_uid_1 uniqueidentifier;

    SELECT @collector_type_uid_1 = collector_type_uid
       FROM [msdb].[dbo].[syscollector_collector_types]
       WHERE name = N'Generic T-SQL Query Collector Type';  
    ```  
  
     You can run the following code to obtain a list of the installed collector types:  
  
    ```sql  
    USE msdb;
    SELECT * from syscollector_collector_types;
    GO  
    ```  
  
4.  Run the **sp_syscollector_create_collection_item** stored procedure to create the collection item. You must declare the schema for the collection item so that it maps to the required schema for the desired collector type. The following example uses the Generic T-SQL Query input schema.  
  
    ```sql  
    DECLARE @collection_item_id int;  

    EXEC [msdb].[dbo].[sp_syscollector_create_collection_item]   
      @name=N'OS Wait Stats', --name of collection item  
      @parameters=N'  
        <ns:TSQLQueryCollector xmlns:ns="DataCollectorType">  
         <Query>  
          <Value>select * from sys.dm_os_wait_stats</Value>  
          <OutputTable>os_wait_stats</OutputTable>  
        </Query>  
        </ns:TSQLQueryCollector>',  
      @collection_item_id = @collection_item_id OUTPUT,  
      @frequency = 60,  
      @collection_set_id = @collection_set_id_1, --- Provides the collection set ID number  
      @collector_type_uid = @collector_type_uid_1; -- Provides the collector type UID  
    
    SELECT @collection_item_id;
    ```  
  
5.  Before starting the updated collection set, run the following query to verify that the new collection item has been created:  
  
    ```sql
    USE msdb;
    GO
    SELECT * from syscollector_collection_sets;
    SELECT * from syscollector_collection_items;
    GO  
    ```  
  
     The collection sets and their collection items are displayed in the **Results** tab.  
  
## See Also  
 [Create a Custom Collection Set That Uses the Generic T-SQL Query Collector Type &#40;Transact-SQL&#41;](../../relational-databases/data-collection/create-custom-collection-set-generic-t-sql-query-collector-type.md)   
 [Data Collector Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/data-collector-stored-procedures-transact-sql.md)  
  
  

---
title: "Parameterized Row Filters | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "publications [SQL Server replication], dynamic filters"
  - "merge replication [SQL Server replication], dynamic filters"
  - "parameterized filters [SQL Server replication]"
  - "filters [SQL Server replication], dynamic"
  - "merge replication parameterized filters [SQL Server replication]"
  - "publications [SQL Server replication], parameterized filters"
  - "parameterized filters [SQL Server replication], about parameterized filters"
  - "filters [SQL Server replication], parameterized"
  - "dynamic filters [SQL Server replication]"
ms.assetid: b48a6825-068f-47c8-afdc-c83540da4639
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Parameterized Filters - Parameterized Row Filters
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Parameterized row filters allow different partitions of data to be sent to different Subscribers without requiring multiple publications to be created (parameterized filters were referred to as dynamic filters in previous versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]). A partition is a subset of the rows in a table; depending on the settings chosen when creating a parameterized row filter, each row in a published table can belong to one partition only (which produces nonoverlapping partitions) or to two or more partitions (which produces overlapping partitions).  
  
 Nonoverlapping partitions can be shared among subscriptions or they can be restricted so that only one subscription receives a given partition. The settings that control partition behavior are described in "Using the Appropriate Filtering Options" later in this topic. Using these settings you can tailor parameterized filtering according to application and performance requirements. In general, overlapping partitions allow for greater flexibility, and nonoverlapping partitions replicated to a single subscription provide better performance.  
  
 Parameterized filters are used on a single table and are typically combined with join filters to extend filtering to related tables. For more information, see [Join Filters](../../../relational-databases/replication/merge/join-filters.md).  
  
 To define or modify a parameterized row filter, see [Define and Modify a Parameterized Row Filter for a Merge Article](../../../relational-databases/replication/publish/define-and-modify-a-parameterized-row-filter-for-a-merge-article.md).  
  
## How Parameterized Filters Work  
 A parameterized row filter uses a WHERE clause to select the appropriate data to be published. Rather than specifying a literal value in the clause (as you do with a static row filter), you specify one or both of the following system functions: SUSER_SNAME() and HOST_NAME(). User-defined functions can also be used, but they must include SUSER_SNAME() or HOST_NAME() in the body of the function, or evaluate one of these system functions (such as `MyUDF(SUSER_SNAME()`). If a user-defined function includes SUSER_SNAME() or HOST_NAME() in the body of the function, you cannot pass parameters to the function.  
  
 The system functions SUSER_SNAME() and HOST_NAME() are not specific to merge replication, but they are used by merge replication for parameterized filtering:  
  
-   SUSER_SNAME() returns login information for connections made to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. When used in a parameterized filter, it returns the login used by the Merge Agent to connect to the Publisher (you specify a login when you create a subscription).  
  
-   HOST_NAME() returns the name of the computer that is connecting to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. When used in a parameterized filter, by default it returns the name of the computer on which the Merge Agent is running. For pull subscriptions it is the name of the Subscriber; for push subscriptions it is the name of the Distributor.  
  
     It is also possible to override this function with a value other than the name of the Subscriber or Distributor. Typically applications override this function with more meaningful values, such as a salesperson name or salesperson ID. For more information, see the section "Overriding the HOST_NAME() Value" in this topic.  
  
 The value returned by the system function is compared to a column you specify in the table you are filtering, and the appropriate data is downloaded to the Subscriber. This comparison is made when the subscription is initialized (so only the appropriate data is contained in the initial snapshot) and every time the subscription is synchronized. By default, if a change at the Publisher results in a row being moved out of a partition, the row is deleted at the Subscriber (this behavior is controlled using the **@allow_partition_realignment** parameter of [sp_addmergepublication &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergepublication-transact-sql.md)).  
  
> [!NOTE]  
>  When comparisons are made for parameterized filters, the database collation is always used. For example, if the database collation is case insensitive, but the table or column collation is case sensitive, the comparison will be case insensitive.  
  
### Filtering with SUSER_SNAME()  
 Consider the **Employee Table** in the [!INCLUDE[ssSampleDBCoShort](../../../includes/sssampledbcoshort-md.md)] sample database. This table includes the column **LoginID**, which contains the login for each employee in the form '*domain\login*'. To filter this table so that employees receive only the data related to them, specify a filter clause of:  
  
```  
LoginID = SUSER_SNAME()  
```  
  
 For example, the value for one of the employees is 'adventure-works\john5'. When the Merge Agent connects to the Publisher, it uses the login you specified when creating the subscription (in this case 'adventure-works\john5'). The Merge Agent then compares the value returned by SUSER_SNAME() to the values in the table and downloads only the row that contains a value of 'adventure-works\john5' in the **LoginID** column.  
  
### Filtering with HOST_NAME()  
 Consider the **HumanResources.Employee** table. Suppose this table contained a column such as **ComputerName** with the name of each employee's computer in the form '*name_computertype*'. To filter this table so that employees receive only the data related to them, specify a filter clause of:  
  
```  
ComputerName = HOST_NAME()  
```  
  
 For example, the value for one of the employees could be 'john5_laptop'. When the Merge Agent connects to the Publisher, it compares the value returned by HOST_NAME() to the values in the table and downloads only the row that contains a value of 'john5_laptop' in the **ComputerName** column.  
  
 It is also possible to combine the functions in a filter. For example, if you wanted to ensure that an employee received data only if they used their login on their computer, the filter clause could be:  
  
```  
LoginID = SUSER_SNAME() AND ComputerName = HOST_NAME()  
```  
  
 Unless you are overriding the HOST_NAME() value, filtering with HOST_NAME() is typically used only with pull subscriptions. The value returned by the function is the name of the computer on which the Merge Agent is running. For pull subscriptions, the value is different for each subscription, but for push subscriptions, the value is the same (all Merge Agents run at the Distributor for push subscriptions).  
  
> [!IMPORTANT]  
>  The value for the HOST_NAME() function can be overridden; therefore it is not possible to use filters that include HOST_NAME() to control access to partitions of data. To control access to partitions of data, use SUSER_SNAME(), SUSER_SNAME() in combination with HOST_NAME(), or use static row filters.  
  
#### Overriding the HOST_NAME() Value  
 As noted earlier, HOST_NAME() by default returns the name of the computer that is connecting to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. When using parameterized filters, it is common to override this value by supplying a value when you create a subscription. The HOST_NAME() function then returns the value you specify rather than the name of the computer.  
  
> [!NOTE]  
>  If you override HOST_NAME(), all calls to the HOST_NAME() function will return the value you specify. Ensure that other applications are not depending on HOST_NAME() returning the computer name.  
  
 Consider the **HumanResources.Employee** table. This table includes the column **EmployeeID**. To filter this table so that each employee receives only the data related to them, specify a filter clause of:  
  
 `EmployeeID = CONVERT(int,HOST_NAME())`  
  
 For example, employee Pamela Ansman-Wolfe has been assigned an employee ID of 280. Specify the value of the employee ID (280 in our example) for the HOST_NAME() value when creating a subscription for this employee. When the Merge Agent connects to the Publisher, it compares the value returned by HOST_NAME() to the values in the table and downloads only the row that contains a value of 280 in the **EmployeeID** column.  
  
> [!IMPORTANT]
>  The HOST_NAME() function returns an **nchar** value, so you must use CONVERT if the column in the filter clause is of a numeric data type, as it is in the example above. For performance reasons, we recommended that you do not apply functions to column names in parameterized row filter clauses, such as `CONVERT(nchar,EmployeeID) = HOST_NAME()`. Instead, we recommend using the approach shown in the example: `EmployeeID = CONVERT(int,HOST_NAME())`. This clause can be used for the **@subset_filterclause** parameter of [sp_addmergearticle](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md), but it typically cannot be used in the New Publication Wizard (the wizard executes the filter clause to validate it, which fails because the computer name cannot be converted to an **int**). If you use the New Publication Wizard, we recommend specifying `CONVERT(nchar,EmployeeID) = HOST_NAME()` in the wizard and then use [sp_changemergearticle](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md) to change the clause to `EmployeeID = CONVERT(int,HOST_NAME())` before creating a snapshot for the publication.  
  
 **To override the HOST_NAME() value**  
  
 Use one of the following methods to override the HOST_NAME() value:  
  
-   [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)]: specify a value on the **HOST\_NAME\(\) Values** page of the New Subscription Wizard. For more information about creating subscriptions, see [Subscribe to Publications](../../../relational-databases/replication/subscribe-to-publications.md).  
  
-   Replication [!INCLUDE[tsql](../../../includes/tsql-md.md)] programming: specify a value for the **@hostname** parameter of [sp_addmergesubscription &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergesubscription-transact-sql.md) (for push subscriptions) or [sp_addmergepullsubscription_agent &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addmergepullsubscription-agent-transact-sql.md) (for pull subscriptions).  
  
-   Merge Agent: specify a value for the **-Hostname** parameter at the command line or through an agent profile. For more information about the Merge Agent, see [Replication Merge Agent](../../../relational-databases/replication/agents/replication-merge-agent.md). For more information about agent profiles, see [Replication Agent Profiles](../../../relational-databases/replication/agents/replication-agent-profiles.md).  
  
## Initializing a Subscription to a Publication with Parameterized Filters  
 When parameterized row filters are used in merge publications, replication initializes each subscription with a two-part snapshot. For more information, see [Snapshots for Merge Publications with Parameterized Filters](../../../relational-databases/replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md).  
  
## Using the Appropriate Filtering Options  
 There are two key areas over which you have control when using parameterized filters:  
  
-   How the filters are processed by merge replication, which is controlled by one of two publication settings: **use partition groups** and **keep partition changes**.  
  
-   How the data is shared among Subscribers, which must be reflected by the article setting **partition options**.  
  
 To set filtering options, see [Optimize Parameterized Row Filters](../../../relational-databases/replication/publish/optimize-parameterized-row-filters.md).  
  
### Setting 'use partition groups' and 'keep partition changes'  
 Both the **use partition groups** and **keep partition changes** options improve the synchronization performance for publications with filtered articles by storing additional metadata in the publication database. The **use partition groups** option provides greater performance improvement through the use of the precomputed partitions feature. This option is set to **true** by default if the articles in your publication adhere to a set of requirements. For more information about these requirements, see [Optimize Parameterized Filter Performance with Precomputed Partitions](../../../relational-databases/replication/merge/parameterized-filters-optimize-for-precomputed-partitions.md). If your articles do not meet the requirements for using precomputed partitions, the **keep partition changes** option to is set to **true**.  
  
### Setting 'partition options'  
 You specify a value for the **partition options** property when creating an article, according to the way in which data in the filtered table will be shared by Subscribers. The property can be set to one of four values using [sp_addmergearticle](../../../relational-databases/system-stored-procedures/sp-addmergearticle-transact-sql.md), [sp_changemergearticle](../../../relational-databases/system-stored-procedures/sp-changemergearticle-transact-sql.md), and the **Article Properties** dialog box. The property can be set to one of two values using the **Add Filter** or **Edit Filter** dialog boxes, which are available from the New Publication Wizard and the **Publication Properties** dialog box. The following table summarizes the available values:  
  
|Description|Value in Add Filter and Edit Filter|Value in Article Properties|Value in stored procedures|  
|-----------------|-----------------------------------------|---------------------------------|--------------------------------|  
|Data in the partitions is overlapping, and the Subscriber can update columns referenced in a parameterized filter.|**A row from this table will go to multiple subscriptions**|**Overlapping**|**0**|  
|Data in the partitions is overlapping, and the Subscriber cannot update columns referenced in a parameterized filter.|N/A*|**Overlapping, disallow out-of-partition data changes**|**1**|  
|Data in the partitions is not overlapping, and the data is shared between subscriptions. The Subscriber cannot update columns referenced in a parameterized filter.|N/A*|**Nonoverlapping, shared between subscriptions**|**2**|  
|Data in the partitions is not overlapping, and there is a single subscription per partition. The Subscriber cannot update columns referenced in a parameterized filter.**|**A row from this table will go to only one subscription**|**Nonoverlapping, single subscription**|**3**|  
  
 \*If the underlying filtering option is set to **0**, or **1**, or **2**, the **Add Filter** and **Edit Filter** dialog boxes will display **A row from this table will go to multiple subscriptions**.  
  
 **If you specify this option, there can only be a single subscription for each partition of data in that article. If a second subscription is created in which the filtering criterion of the new subscription resolves to the same partition as the existing subscription, the existing subscription is dropped.  
  
> [!IMPORTANT]  
>  The **partition options** value must be set according to how data is shared by Subscribers. If, for example, you specify that a partition is nonoverlapping with a single subscription per partition, but data is then updated at another Subscriber, the Merge Agent can fail during synchronization and non-convergence can occur.  
  
#### Selecting the Appropriate Partition Option  
 Nonoverlapping partitions work in conjunction with precomputed partitions to improve performance in situations where some functional limitations are acceptable. Precomputed partitions quicken downloads to Subscribers, but slow uploads. Nonoverlapping partitions minimize the upload cost associated with precomputed partitions. The performance benefit of nonoverlapping partitions is more noticeable when the parameterized filters and join filters used are more complex.  
  
 Consider the following scenarios when deciding which partition options to use in a publication.  
  
-   [!INCLUDE[ssSampleDBCoShort](../../../includes/sssampledbcoshort-md.md)] has a mobile sales force with each sales person responsible for customers in a given zip code. The application requires that  the zip code be updated if a customer moves from one sales territory to another, so that the customer is assigned to a different sales person. The parameterized filter is based on the customer's zip code, and the update removes the zip code from one sales person's partition and inserts it into another sales person's partition. This requires overlapping partitions with the ability to update columns referenced in a parameterized filter. This option maximizes flexibility but might not perform as well as nonoverlapping partitions.  
  
-   An employment agency has data that is supplied to regional offices in each county of the state. The data does not overlap; each row in the table at the agency's headquarters is included in only one partition, but that partition is sent to multiple offices in the same county. The nonoverlapping partition option with partitions shared between subscriptions is appropriate, providing a performance improvement over overlapping partitions while satisfying the application requirements.  
  
-   If you have nonoverlapping partitions and only one subscription receives and updates the data in a partition, further performance benefits can be realized. This scenario is common for point of sale systems, and field force applications in which data is primarily collected at the Subscriber and uploaded to the Publisher. Consider a **Package** table in a delivery application: as each package is loaded onto a truck, the status of the package is changed in the **Package** table, and the change is replicated back to headquarters. Drivers would not update the status of the same package on two different trucks, so the **Package** table is a good candidate for a nonoverlapping partition with a single subscription per partition.  
  
#### Considerations for Nonoverlapping Partitions  
 Keep the following considerations in mind when using nonoverlapping partitions.  
  
##### General Considerations  
  
-   The publication must use precomputed partitions.  
  
-   A row must belong to only one partition.  
  
-   Articles cannot be part of a logical record.  
  
-   Alternate synchronization partners are not supported (this feature is deprecated).  
  
-   The Subscriber cannot update columns referenced in a parameterized filter.  
  
-   If an insert at a Subscriber does not belong to the partition, it is not deleted. However, it will not be replicated to other Subscribers.  
  
-   In some circumstances with overlapping partitions, identity ranges are adjusted when the Merge Agent inserts data. With nonoverlapping partitions, ranges can only be adjusted during inserts by a user who has permission to adjust identity ranges in the subscription database. The user must either own the table, or be a member of the **sysadmin** fixed server role, the **db_owner** fixed database role, or the **db_ddladmin** fixed database role.  
  
##### Additional Considerations for Nonoverlapping Partitions with a Single Subscription per Partition  
  
-   Articles can exist in only one publication; articles cannot be republished.  
  
-   The publication must allow Subscribers to initiate the snapshot process. For more information, see [Snapshots for Merge Publications with Parameterized Filters](../../../relational-databases/replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md).  
  
##### Additional Considerations for Join Filters  
  
-   In a join filter hierarchy, an article with an overlapping partition cannot appear above an article with a nonoverlapping partition. In other words, a parent article must use nonoverlapping partitions if the child article does. For information about join filters, see [Join Filters](../../../relational-databases/replication/merge/join-filters.md).  
  
-   A join filter in which the nonoverlapping partition is a child must have the **join unique key** property set to 1. For more information, see [Join Filters](../../../relational-databases/replication/merge/join-filters.md).  
  
-   The article should only have one parameterized filter or join filter. Having a parameterized filter and being the parent in a join filter is allowed. Having a parameterized filter and being the child in a join filter is not allowed. Having more than one join filter is also not allowed.  
  
-   If two tables at the Publisher have a join filter relationship and the child table has rows that have no corresponding row in the parent table, an insert of the missing parent row will not result in the related rows being downloaded to the Subscriber (the rows would be downloaded with overlapping partitions). For example, if the **SalesOrderDetail** table has rows with no corresponding row in the **SalesOrderHeader** table, and you insert the missing row in **SalesOrderHeader**, the row is downloaded to the Subscriber, but the corresponding rows in **SalesOrderDetail** are not.  
  
## See Also  
 [Best Practices for Time-Based Row Filters](../../../relational-databases/replication/merge/best-practices-for-time-based-row-filters.md)   
 [Filter Published Data](../../../relational-databases/replication/publish/filter-published-data.md)   
 [Filter Published Data for Merge Replication](../../../relational-databases/replication/merge/filter-published-data-for-merge-replication.md)  
  
  

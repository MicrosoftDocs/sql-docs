---
title: "Specify how changes are propagated (Transactional)"
description: Learn how to specify how change are propagated for a Transactional Publication in SQL Server. 
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "transactional replication, propagation methods"
ms.assetid: a10c5001-22cc-4667-8f0b-3d0818dca2e9
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Transactional Articles - Specify How Changes Are Propagated
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  Transactional replication allows you to specify how data changes are propagated from the Publisher to Subscribers. For each published table, you can specify one of four ways that each operation (INSERT, UPDATE, or DELETE) should be propagated to the Subscriber:  
  
-   Specify that transactional replication should script out and subsequently call a stored procedure to propagate changes to Subscribers (the default).  
  
-   Specify that the change should be propagated using an INSERT, UPDATE, or DELETE statement (the default for non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers).  
  
-   Specify that a custom stored procedure should be used.  
  
-   Specify that this action should not be performed at any Subscriber. Transactions of that type are not replicated.  
  
 By default, transactional replication propagates changes to Subscribers through a set of stored procedures that are installed on each Subscriber. When an insert, update or delete occurs on a table at the Publisher, the operation is translated into a call to a stored procedure at the Subscriber. The stored procedure accepts parameters that map to the columns in the table, allowing those columns to be changed at the Subscriber.  
  
 To set the propagation method for data changes to transactional articles, see [Set the Propagation Method for Data Changes to Transactional Articles](../../../relational-databases/replication/publish/set-the-propagation-method-for-data-changes-to-transactional-articles.md).  
  
## Default and custom stored procedures  
 The three procedures that replication creates by default for each table article are:  
  
-   **sp_MSins_\<** *tablename* **>**, which handles inserts.  
  
-   **sp_MSupd_\<** *tablename* **>**, which handles updates.  
  
-   **sp_MSdel_\<** *tablename* **>**, which handles deletes.  
  
 The **\<**_tablename_**>** used in the procedure depends on how the article was added to the publication and whether the subscription database contains a table of the same name with a different owner.  
  
 Any of these procedures can be replaced with a custom procedure that you specify when adding an article to a publication. Custom procedures are used if an application requires custom logic, such as inserting data into an audit table when a row is updated at a Subscriber. For more information about specifying custom stored procedures, see the how to topics listed above.  
  
 If you specify the default replication procedures or custom procedures, you also specify call syntax for each procedure (replication selects defaults if you use the default procedures). The call syntax determines the structure of the parameters provided to the procedure and how much information is sent to the Subscriber with each data change. For more information, see the section "Call Syntax for Stored Procedures" in this topic.  
  
### Considerations for Using Custom Stored Procedures  
 Keep the following considerations in mind when using custom stored procedures:  
  
-   You must support the logic in the stored procedure; [!INCLUDE[msCoName](../../../includes/msconame-md.md)] does not provide support for custom logic.  
  
-   In order to avoid conflicts with the transactions used by replication, explicit transactions should not be used in custom procedures.  
  
-   The schema at the Subscriber is typically identical to the schema at the Publisher, but can also be a subset of the Publisher schema if column filtering is used. However, if you need to transform the schema as the data is moved such that the schema on the Subscriber is not a subset of the schema on the Publisher, [!INCLUDE[ssISCurrent](../../../includes/ssiscurrent-md.md)] is the recommended solution. For more information, see [SQL Server Integration Services](../../../integration-services/sql-server-integration-services.md).  
  
-   If you make schema changes to a published table, the custom procedures must be regenerated. For more information, see [Regenerate Custom Transactional Procedures to Reflect Schema Changes](../../../relational-databases/replication/transactional/transactional-articles-regenerate-to-reflect-schema-changes.md).  
  
-   If you use a value greater than 1 for **-SubscriptionStreams** parameter of the Distribution Agent, you must ensure that updates to primary key columns are successful. For example:  
  
    ```  
    update ... set pk = 2 where pk = 1 -- update 1  
    update ... set pk = 3 where pk = 2 -- update 2  
    ```  
  
     If the Distribution Agent uses more than one connection, these two updates might be replicated over different connections. If update 1 is applied first, there is no problem; if update 2 is applied first it will return '0 rows affected' because update 1 has not yet occurred. This situation is handled in the default procedures by raising an error if no rows are affected on an update:  
  
    ```  
    if @@rowcount = 0  
        if @@microsoftversion>0x07320000  
            exec sys.sp_MSreplraiserror 20598  
    ```  
  
     Raising the error forces the Distribution Agent to retry the updates over a single connection, which will succeed. Custom stored procedures must include similar logic.  
  
### Call syntax for stored procedures  
 There are five options for the syntax used to call the procedures used by transactional replication:  
  
-   CALL syntax. Can be used for inserts, updates, and deletes. By default, replication uses this syntax for inserts and deletes.  
  
-   SCALL syntax. Can be used for updates only. By default, replication uses this syntax for updates.  
  
-   MCALL syntax. Can be used for updates only.  
  
-   XCALL syntax. Can be used for updates and deletes.  
  
-   VCALL. Used for updatable subscriptions. Internal use only.  
  
 Each method differs in the amount of data that is propagated to the Subscriber. For example, SCALL passes in values only for the columns that are actually affected by an update. XCALL, by contrast, requires all columns (whether affected by an update or not) and all the old data values for each column. In many cases, SCALL is appropriate for updates, but if your application requires all the data values during an update, XCALL allows for this.  
  
#### CALL Syntax  
 INSERT stored procedures  
 Stored procedures handling INSERT statements will be passed the inserted values for all columns:  
  
```  
c1, c2, c3,... cn  
```  
  
 UPDATE stored procedures  
 Stored procedures handling UPDATE statements will be passed the updated values for all columns defined in the article, followed by the original values for the primary key columns (no attempt is made to determine which columns were changed.):  
  
```  
c1, c2, c3,... cn, pkc1, pkc2, pkc3,... pkcn  
```  
  
 DELETE stored procedures  
 Stored procedures handling DELETE statements will be passed values for the primary key columns:  
  
```  
pkc1, pkc2, pkc3,... pkcn  
```  
  
#### SCALL Syntax  
 UPDATE stored procedures  
 Stored procedures handling UPDATE statements will be passed the updated values only for those columns that have changed, followed by the original values for the primary key columns, followed by a bitmask (**binary(n)**) parameter that indicates the changed columns. In the following example, column 2 (c2) has not changed:  
  
```  
c1, , c3,... cn, pkc1, pkc2, pkc3,... pkcn, bitmask  
```  
  
#### MCALL Syntax  
 UPDATE stored procedures  
 Stored procedures handling UPDATE statements will be passed the updated values for all columns defined in the article, followed by the original values for the primary key columns, followed by a bitmask (**binary(n)**) parameter that indicates the changed columns:  
  
```  
c1, c2, c3,... cn, pkc1, pkc2, pkc3,... pkcn, bitmask  
```  
  
#### XCALL Syntax  
 UPDATE stored procedures  
 Stored procedures handling UPDATE statements will be passed the original values (the before image) for all columns defined in the article, followed by the updated values (the after image) for all columns defined in the article:  
  
```  
old-c1, old-c2, old-c3,... old-cn, c1, c2, c3,... cn,  
```  
  
 DELETE stored procedures  
 Stored procedures handling DELETE statements will be passed the original (the before image) values for all columns defined in the article:  
  
```  
old-c1, old-c2, old-c3,... old-cn  
```  
  
> [!NOTE]  
>  When using XCALL, the before image values for **text** and **image** columns are expected to be NULL.  
  
## Examples  
 The following procedures are the default procedures created for the `Vendor Table` in the [!INCLUDE[ssSampleDBCoShort](../../../includes/sssampledbcoshort-md.md)] sample database.  
  
```  
--INSERT procedure using CALL syntax  
create procedure [sp_MSins_PurchasingVendor]   
  @c1 int,@c2 nvarchar(15),@c3 nvarchar(50),@c4 tinyint,@c5 bit,@c6 bit,@c7 nvarchar(1024),@c8 datetime  
as   
begin   
insert into [Purchasing].[Vendor]([VendorID]  
,[AccountNumber]  
,[Name]  
,[CreditRating]  
,[PreferredVendorStatus]  
,[ActiveFlag]  
,[PurchasingWebServiceURL]  
,[ModifiedDate])  
values (   
 @c1  
,@c2  
,@c3  
,@c4  
,@c5  
,@c6  
,@c7  
,@c8  
 )   
end  
go  
  
--UPDATE procedure using SCALL syntax  
create procedure [sp_MSupd_PurchasingVendor]   
 @c1 int = null,@c2 nvarchar(15) = null,@c3 nvarchar(50) = null,@c4 tinyint = null,@c5 bit = null,@c6 bit = null,@c7 nvarchar(1024) = null,@c8 datetime = null,@pkc1 int  
,@bitmap binary(2)  
as  
begin  
update [Purchasing].[Vendor] set   
 [AccountNumber] = case substring(@bitmap,1,1) & 2 when 2 then @c2 else [AccountNumber] end  
,[Name] = case substring(@bitmap,1,1) & 4 when 4 then @c3 else [Name] end  
,[CreditRating] = case substring(@bitmap,1,1) & 8 when 8 then @c4 else [CreditRating] end  
,[PreferredVendorStatus] = case substring(@bitmap,1,1) & 16 when 16 then @c5 else [PreferredVendorStatus] end  
,[ActiveFlag] = case substring(@bitmap,1,1) & 32 when 32 then @c6 else [ActiveFlag] end  
,[PurchasingWebServiceURL] = case substring(@bitmap,1,1) & 64 when 64 then @c7 else [PurchasingWebServiceURL] end  
,[ModifiedDate] = case substring(@bitmap,1,1) & 128 when 128 then @c8 else [ModifiedDate] end  
where [VendorID] = @pkc1  
if @@rowcount = 0  
    if @@microsoftversion>0x07320000  
        exec sp_MSreplraiserror 20598  
end  
go  
  
--DELETE procedure using CALL syntax  
create procedure [sp_MSdel_PurchasingVendor]   
  @pkc1 int  
as   
begin   
delete [Purchasing].[Vendor]  
where [VendorID] = @pkc1  
if @@rowcount = 0  
    if @@microsoftversion>0x07320000  
        exec sp_MSreplraiserror 20598  
end   
go  
```  
  
## See Also  
 [Article Options for Transactional Replication](../../../relational-databases/replication/transactional/article-options-for-transactional-replication.md)  
  
  

---
title: Specify How Changes are Propagated (Transactional)
description: Transactional replication in SQL Server propagates changes through stored procedures, DML statements, or custom logic. Explore call syntax options and examples.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "transactional replication, propagation methods"
monikerRange: "=azuresqldb-mi-current || >=sql-server-2017"
---
# Transactional articles - specify how changes are propagated
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  Transactional replication allows you to specify how data changes are propagated from the Publisher to Subscribers. For each published table, you can specify one of four ways that each operation (INSERT, UPDATE, or DELETE) should be propagated to the Subscriber:  
  
-   Specify that transactional replication should script out and subsequently call a stored procedure to propagate changes to Subscribers (the default).  
  
-   Specify that the change should be propagated using an INSERT, UPDATE, or DELETE statement (the default for non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers).  
  
-   Specify that a custom stored procedure should be used.  
  
-   Specify that this action should not be performed at any Subscriber. Transactions of that type are not replicated.  
  
 By default, transactional replication propagates changes to Subscribers through a set of stored procedures that are installed on each Subscriber. When an insert, update or delete occurs on a table at the Publisher, the operation is translated into a call to a stored procedure at the Subscriber. The stored procedure accepts parameters that map to the columns in the table, allowing those columns to be changed at the Subscriber.  
  
 To set the propagation method for data changes to transactional articles, see [Set the Propagation Method for Data Changes to Transactional Articles](../../../relational-databases/replication/publish/set-the-propagation-method-for-data-changes-to-transactional-articles.md).  
  
## Default and custom stored procedures  
 Replication creates three default stored procedures for each table article:  
  
-   **sp_MSins_\<** *tablename* **>**, which handles inserts.  
  
-   **sp_MSupd_\<** *tablename* **>**, which handles updates.  
  
-   **sp_MSdel_\<** *tablename* **>**, which handles deletes.  
  
 The **\<**_tablename_**>** used in the procedure depends on how you add the article to the publication and whether the subscription database contains a table with the same name but a different owner.  
  
 You can replace any of these procedures with a custom procedure that you specify when adding an article to a publication. Use custom procedures if your application requires custom logic, such as inserting data into an audit table when a row is updated at a Subscriber. For more information about specifying custom stored procedures, see the how-to articles listed in the previous section.  
  
 When you specify either the default replication procedures or custom procedures, you also specify call syntax for each procedure. Replication selects default call syntax if you use the default procedures. The call syntax determines the structure of the parameters provided to the procedure and how much information is sent to the Subscriber with each data change. For more information, see the section "Call Syntax for Stored Procedures" in this article.  
  
### Considerations for using custom stored procedures  
 Keep the following considerations in mind when using custom stored procedures:  
  
-   You must support the logic in the stored procedure; [!INCLUDE[msCoName](../../../includes/msconame-md.md)] doesn't provide support for custom logic.  
  
-   To avoid conflicts with the transactions used by replication, don't use explicit transactions in custom procedures.  
  
-   The schema at the Subscriber is typically identical to the schema at the Publisher, but it can also be a subset of the Publisher schema if you use column filtering. If you need to transform the schema as the data moves so that the schema at the Subscriber isn't a subset of the schema at the Publisher, use [!INCLUDE[ssISCurrent](../../../includes/ssiscurrent-md.md)]. For more information, see [SQL Server Integration Services](../../../integration-services/sql-server-integration-services.md).  
  
-   If you make schema changes to a published table, regenerate the custom procedures. For more information, see [Regenerate Custom Transactional Procedures to Reflect Schema Changes](../../../relational-databases/replication/transactional/transactional-articles-regenerate-to-reflect-schema-changes.md).  
  
-   If you use a value greater than 1 for the **-SubscriptionStreams** parameter of the Distribution Agent, ensure that updates to primary key columns succeed. For example:  
  
    ```  
    update ... set pk = 2 where pk = 1 -- update 1  
    update ... set pk = 3 where pk = 2 -- update 2  
    ```  
  
     If the Distribution Agent uses more than one connection, these two updates might replicate over different connections. If update 1 is applied first, there's no problem. If update 2 is applied first, it returns `0 rows affected` because update 1 didn't occur yet. The default procedures handle this situation by raising an error if no rows are affected on an update:  
  
    ```  
    if @@rowcount = 0  
        if @@microsoftversion>0x07320000  
            exec sys.sp_MSreplraiserror 20598  
    ```  
  
     Raising the error forces the Distribution Agent to retry the updates over a single connection, which succeeds. Custom stored procedures must include similar logic.  
  
### Call syntax for stored procedures  
 You can use five different syntax options to call the procedures that transactional replication uses:  
  
-   CALL syntax. Use this syntax for inserts, updates, and deletes. By default, replication uses this syntax for inserts and deletes.  
  
-   SCALL syntax. Use this syntax for updates only. By default, replication uses this syntax for updates.  
  
-   MCALL syntax. Use this syntax for updates only.  
  
-   XCALL syntax. Use this syntax for updates and deletes.  
  
-   VCALL. Use this syntax for updatable subscriptions. Internal use only.  
  
 Each method differs in the amount of data that it sends to the Subscriber. For example, SCALL passes in values only for the columns that an update actually affects. XCALL requires all columns, whether an update affects them or not, and all the old data values for each column. In many cases, SCALL is appropriate for updates, but if your application requires all the data values during an update, XCALL supports this need.  
  
#### CALL Syntax  
 INSERT stored procedures  
 Stored procedures that handle `INSERT` statements receive the inserted values for all columns:  
  
```  
c1, c2, c3,... cn  
```  
  
 UPDATE stored procedures  
 Stored procedures that handle `UPDATE` statements receive the updated values for all columns defined in the article, followed by the original values for the primary key columns. The process doesn't attempt to determine which columns changed:  
  
```  
c1, c2, c3,... cn, pkc1, pkc2, pkc3,... pkcn  
```  
  
 DELETE stored procedures  
 Stored procedures that handle `DELETE` statements receive values for the primary key columns:  
  
```  
pkc1, pkc2, pkc3,... pkcn  
```  
  
#### SCALL Syntax  
 UPDATE stored procedures  
 Stored procedures that handle `UPDATE` statements receive the updated values only for those columns that changed, followed by the original values for the primary key columns, and then a bitmask (`binary(n)`) parameter that indicates the changed columns. In the following example, column 2 (`c2`) didn't change:  
  
```  
c1, , c3,... cn, pkc1, pkc2, pkc3,... pkcn, bitmask  
```  
  
#### MCALL Syntax  
 UPDATE stored procedures  
 Stored procedures that handle `UPDATE` statements receive the updated values for all columns defined in the article, followed by the original values for the primary key columns, and then a bitmask (`binary(n)`) parameter that indicates the changed columns:  
  
```  
c1, c2, c3,... cn, pkc1, pkc2, pkc3,... pkcn, bitmask  
```  
  
#### XCALL Syntax  
 UPDATE stored procedures  
 Stored procedures that handle `UPDATE` statements receive the original values (the before image) for all columns defined in the article, followed by the updated values (the after image) for all columns defined in the article:  
  
```  
old-c1, old-c2, old-c3,... old-cn, c1, c2, c3,... cn,  
```  
  
 DELETE stored procedures  
 Stored procedures that handle `DELETE` statements receive the original values (the before image) for all columns defined in the article:  
  
```  
old-c1, old-c2, old-c3,... old-cn  
```  
  
> [!NOTE]  
>  When you use XCALL, the before image values for `text` and `image` columns should be `NULL`.  
  
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
  
## Related content

- [Article Options for Transactional Replication](article-options-for-transactional-replication.md)

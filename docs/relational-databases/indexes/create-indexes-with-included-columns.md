---
title: "Create Indexes with Included Columns | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: sql
ms.prod_service: "table-view-index, sql-database"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "index size [SQL Server]"
  - "index keys [SQL Server]"
  - "index columns [SQL Server]"
  - "size [SQL Server], indexes"
  - "key columns [SQL Server]"
  - "included columns"
  - "nonclustered indexes [SQL Server], included columns"
  - "designing indexes [SQL Server], included columns"
  - "nonkey columns"
ms.assetid: d198648d-fea5-416d-9f30-f9d4aebbf4ec
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create Indexes with Included Columns
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

  This topic describes how to add included (or nonkey) columns to extend the functionality of nonclustered indexes in [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. By including nonkey columns, you can create nonclustered indexes that cover more queries. This is because the nonkey columns have the following benefits:  
  
-   They can be data types not allowed as index key columns.  
  
-   They are not considered by the [!INCLUDE[ssDE](../../includes/ssde-md.md)] when calculating the number of index key columns or index key size.  
  
 An index with nonkey columns can significantly improve query performance when all columns in the query are included in the index either as key or nonkey columns. Performance gains are achieved because the query optimizer can locate all the column values within the index; table or clustered index data is not accessed resulting in fewer disk I/O operations.  
  
> [!NOTE]  
> When an index contains all the columns referenced by a query it is typically referred to as *covering the query*.  
   
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="DesignRecs"></a> Design Recommendations  
  
-   Redesign nonclustered indexes with a large index key size so that only columns used for searching and lookups are key columns. Make all other columns that cover the query into nonkey columns. In this way, you will have all columns needed to cover the query, but the index key itself is small and efficient.  
  
-   Include nonkey columns in a nonclustered index to avoid exceeding the current index size limitations of a maximum of 32 key columns and a maximum index key size of 1,700 bytes (16 key columns and 900 bytes prior to [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)]). The [!INCLUDE[ssDE](../../includes/ssde-md.md)] does not consider nonkey columns when calculating the number of index key columns or index key size.  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   Nonkey columns can only be defined on nonclustered indexes.  
  
-   All data types except **text**, **ntext**, and **image** can be used as nonkey columns.  
  
-   Computed columns that are deterministic and either precise or imprecise can be nonkey columns. For more information, see [Indexes on Computed Columns](../../relational-databases/indexes/indexes-on-computed-columns.md).  
  
-   Computed columns derived from **image**, **ntext**, and **text** data types can be nonkey columns as long as the computed column data type is allowed as a nonkey index column.  
  
-   Nonkey columns cannot be dropped from a table unless that table's index is dropped first.  
  
-   Nonkey columns cannot be changed, except to do the following:  
  
    -   Change the nullability of the column from NOT NULL to NULL.  
  
    -   Increase the length of **varchar**, **nvarchar**, or **varbinary** columns.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table or view. User must be a member of the **sysadmin** fixed server role or the **db_ddladmin** and **db_owner** fixed database roles.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To create an index with nonkey columns  
  
1.  In Object Explorer, click the plus sign to expand the database that contains the table on which you want to create an index with nonkey columns.  
  
2.  Click the plus sign to expand the **Tables** folder.  
  
3.  Click the plus sign to expand the table on which you want to create an index with nonkey columns.  
  
4.  Right-click the **Indexes** folder, point to **New Index**, and select **Non-Clustered Index...**.  
  
5.  In the **New Index** dialog box, on the **General** page, enter the name of the new index in the **Index name** box.  
  
6.  Under the **Index key columns** tab, click **Add...**.  
  
7.  In the **Select Columns from**_table\_name_ dialog box, select the check box or check boxes of the table column or columns to be added to the index.  
  
8.  Click **OK**.  
  
9. Under the **Included columns** tab, click **Add...**.  
  
10. In the **Select Columns from**_table\_name_ dialog box, select the check box or check boxes of the table column or columns to be added to the index as nonkey columns.  
  
11. Click **OK**.  
  
12. In the **New Index** dialog box, click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To create an index with nonkey columns  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```sql  
    USE AdventureWorks2012;  
    GO  
    -- Creates a nonclustered index on the Person.Address table with four included (nonkey) columns.   
    -- index key column is PostalCode and the nonkey columns are  
    -- AddressLine1, AddressLine2, City, and StateProvinceID.  
    CREATE NONCLUSTERED INDEX IX_Address_PostalCode  
    ON Person.Address (PostalCode)  
    INCLUDE (AddressLine1, AddressLine2, City, StateProvinceID);  
    GO  
    ```  

## Related content  
[CREATE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-index-transact-sql.md)    
[SQL Server Index Design Guide](../../relational-databases/sql-server-index-design-guide.md)   

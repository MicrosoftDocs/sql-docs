---
title: "Use Column Sets | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2015"
ms.prod: sql
ms.prod_service: "table-view-index, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "sparse columns, column sets"
  - "column sets"
ms.assetid: a4f9de95-dc8f-4ad8-b957-137e32bfa500
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Use Column Sets
[!INCLUDE[tsql-appliesto-ss2016-all-md](../../includes/tsql-appliesto-ss2016-all-md.md)]

  Tables that use sparse columns can designate a column set to return all sparse columns in the table. A column set is an untyped XML representation that combines all the sparse columns of a table into a structured output. A column set is like a calculated column in that the column set is not physically stored in the table. A column set differs from a calculated column in that the column set is directly updatable.  
  
 You should consider using column sets when the number of columns in a table is large, and operating on them individually is cumbersome. Applications might see some performance improvement when they select and insert data by using column sets on tables that have lots of columns. However, the performance of column sets can be reduced when many indexes are defined on the columns in the table. This is because the amount of memory that is required for an execution plan increases.  
  
 To define a column set, use the *<column_set_name>* FOR ALL_SPARSE_COLUMNS keywords in the[CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md) or [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) statements.  
  
## Guidelines for Using Column Sets  
 When you use column sets, consider the following guidelines:  
  
-   Sparse columns and a column set can be added as part of the same statement.  
  
-   A column set cannot be added to a table if that table already contains sparse columns.  
  
-   The column set cannot be changed. To change a column set, you must delete and re-create the sparse columns and the column set.  
  
-   A column set can be added to a table that does not include any sparse columns. If sparse columns are later added to the table, they will appear in the column set.  
  
-   Only one column set is allowed per table.  
  
-   A column set is optional and is not required to use sparse columns.  
  
-   Constraints or default values cannot be defined on a column set.  
  
-   Computed columns cannot contain column set columns.  
  
-   Distributed queries are not supported on tables that contain column sets.  
  
-   Replication does not support column sets.  
  
-   Change data capture does not support column sets.  
  
-   A column set cannot be part of any kind of index. This includes XML indexes, full-text indexes, and indexed views. A column set cannot be added as an included column in any index.  
  
-   A column set cannot be used in the filter expression of a filtered index or filtered statistics.  
  
-   When a view includes a column set, the column set appears in the view as an XML column.  
  
-   A column set cannot be included in an indexed view definition.  
  
-   Partitioned views that include tables that contain column sets are updatable when the partitioned view specifies the sparse columns by name. A partitioned view is not updatable when it references the column set.  
  
-   Query notifications that refer to column sets are not permitted.  
  
-   XML data has a size limit of 2 GB. If the combined data of all the nonnull sparse columns in a row exceeds this limit, the query or DML operation will produce an error.  
  
-   For information about the data that is returned by the COLUMNS_UPDATED function, see [Use Sparse Columns](../../relational-databases/tables/use-sparse-columns.md).  
  
## Guidelines for Selecting Data from a Column Set  
 Consider the following guidelines for selecting data from a column set:  
  
-   Conceptually, a column set is a type of updatable, computed XML column that aggregates a set of underlying relational columns into a single XML representation. The column set only supports the ALL_SPARSE_COLUMNS property. This property is used to aggregate all nonnull values from all sparse columns for a particular row.  
  
-   In the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] table editor, column sets are displayed as an editable XML field. Define column sets in the format:  
  
    ```  
    <column_name_1>value1</column_name_1><column_name_2>value2</column_name_2>...  
    ```  
  
     Examples of column set values are as follows:  
  
    -   `<sparseProp1>10</sparseProp1><sparseProp3>20</sparseProp3>`  
  
    -   `<DocTitle>Bicycle Parts List</DocTitle><Region>West</Region>`  
  
-   Sparse columns that contain null values are omitted from the XML representation for the column set.  
  
> [!WARNING]  
>  Adding a column set changes the behavior of SELECT * queries. The query will return the column set as an XML column and not return the individual sparse columns. Schema designers and software developers must be careful not to break existing applications.  
  
## Inserting or Modifying Data in a Column Set  
 Data manipulation of a sparse column can be performed by using the name of the individual columns, or by referencing the name of the column set and specifying the values of the column set by using the XML format of the column set. Sparse columns can appear in any order in the XML column.  
  
 When sparse column values are inserted or updated by using the XML column set, the values that are inserted into the underlying sparse columns are implicitly converted from the **xml** data type. In the case of numeric columns, a blank value in the XML for the numeric column converts to an empty string. This causes a zero to be inserted into the numeric column as shown in the following example.  
  
```  
CREATE TABLE t (i int SPARSE, cs xml column_set FOR ALL_SPARSE_COLUMNS);  
GO  
INSERT t(cs) VALUES ('<i/>');  
GO  
SELECT i FROM t;  
GO  
```  
  
 In this example, no value was specified for the column `i`, but the value `0` was inserted.  
  
## Using the sql_variant Data Type  
 The **sql_variant** date type can store multiple different data types, such as **int**, **char**, and **date**. Column sets output the data type information such as scale, precision, and locale information that is associated with a **sql_variant** value as attributes in the generated XML column. If you try to provide these attributes in a custom-generated XML statement as an input for an insert or update operation on a column set, some of these attributes are required and some of them are assigned a default value. The following table lists the data types and the default values that the server generates when the value is not provided.  
  
|Data type|localeID*|sqlCompareOptions|sqlCollationVersion|SqlSortId|Maximum length|Precision|Scale|  
|---------------|----------------|-----------------------|-------------------------|---------------|--------------------|---------------|-----------|  
|**char**, **varchar**, **binary**|-1|'Default'|0|0|8000|Not applicable**|Not applicable|  
|**nvarchar**|-1|'Default'|0|0|4000|Not applicable|Not applicable|  
|**decimal**, **float**, **real**|Not applicable|Not applicable|Not applicable|Not applicable|Not applicable|18|0|  
|**integer**, **bigint**, **tinyint**, **smallint**|Not applicable|Not applicable|Not applicable|Not applicable|Not applicable|Not applicable|Not applicable|  
|**datetime2**|Not applicable|Not applicable|Not applicable|Not applicable|Not applicable|Not applicable|7|  
|**datetime offset**|Not applicable|Not applicable|Not applicable|Not applicable|Not applicable|Not applicable|7|  
|**datetime**, **date**, **smalldatetime**|Not applicable|Not applicable|Not applicable|Not applicable|Not applicable|Not applicable|Not applicable|  
|**money**, **smallmoney**|Not applicable|Not applicable|Not applicable|Not applicable|Not applicable|Not applicable|Not applicable|  
|**time**|Not applicable|Not applicable|Not applicable|Not applicable|Not applicable|Not applicable|7|  
  
 \*  localeID -1 means the default locale. The English locale is 1033.  
  
 **  Not applicable = No values are output for these attributes during a select operation on the column set. Generates an error when a value is specified for this attribute by the caller in the XML representation provided for a column set in an insert or update operation.  
  
## Security  
 The security model for a column set works similar to the security model that exists between table and columns. Column sets can be visualized as a minitable and a select operation is like a SELECT * operation on this minitable. But, the relationship between column set to sparse columns is a grouping relationship instead of strictly a container. The security model checks the security on the column set column, and honors the DENY operations on the underlying sparse columns. Additional characteristics of the security model are as follows:  
  
-   Security permissions can be granted and revoked from the column set column, similar to any other column in the table.  
  
-   A GRANT or REVOKE of SELECT, INSERT, UPDATE, DELETE, and REFERENCES permission on a column set column does not propagate to the underlying member columns of that set. It applies only to the usage of the column set column. DENY permission on a column set does propagate to the underlying sparse columns of the table.  
  
-   Executing SELECT, INSERT, UPDATE, and DELETE statements on the column set column require that the user has corresponding permissions on the column set column, and also the corresponding permission on all the sparse columns in the table. Because the column set represents all the sparse columns in the table, you must have permission on all the sparse columns, and this includes sparse columns that you might not be changing.  
  
-   Executing a REVOKE statement on a sparse column or column set defaults the security to their parent object.  
  
## Examples  
 In the following examples, a document table contains the common set of columns `DocID` and `Title`. The Production group wants a `ProductionSpecification` and `ProductionLocation` column for all production documents. The Marketing group wants a `MarketingSurveyGroup` column for marketing documents.  
  
### A. Creating a table that has a column set  
 The following example creates the table that uses sparse columns and includes the column set `SpecialPurposeColumns`. The example inserts two rows into the table, and then selects data from the table.  
  
> [!NOTE]  
>  This table has only five columns to make it easier to display and read.  
  
```  
USE AdventureWorks2012;  
GO  
  
CREATE TABLE DocumentStoreWithColumnSet  
    (DocID int PRIMARY KEY,  
     Title varchar(200) NOT NULL,  
     ProductionSpecification varchar(20) SPARSE NULL,  
     ProductionLocation smallint SPARSE NULL,  
     MarketingSurveyGroup varchar(20) SPARSE NULL,  
     MarketingProgramID int SPARSE NULL,  
     SpecialPurposeColumns XML COLUMN_SET FOR ALL_SPARSE_COLUMNS);  
GO  
```  
  
### B. Inserting data to a table by using the names of the sparse columns  
 The following examples insert two rows into the table that is created in example A. The examples use the names of the sparse columns and do not reference the column set.  
  
```  
INSERT DocumentStoreWithColumnSet (DocID, Title, ProductionSpecification, ProductionLocation)  
VALUES (1, 'Tire Spec 1', 'AXZZ217', 27);  
GO  
  
INSERT DocumentStoreWithColumnSet (DocID, Title, MarketingSurveyGroup)  
VALUES (2, 'Survey 2142', 'Men 25 - 35');  
GO  
```  
  
### C. Inserting data to a table by using the name of the column set  
 The following example inserts a third row into the table that is created in example A. This time the names of the sparse columns are not used. Instead, the name of the column set is used, and the insert provides the values for two of the four sparse columns in XML format.  
  
```  
INSERT DocumentStoreWithColumnSet (DocID, Title, SpecialPurposeColumns)  
VALUES (3, 'Tire Spec 2', '<ProductionSpecification>AXW9R411</ProductionSpecification><ProductionLocation>38</ProductionLocation>');  
GO  
```  
  
### D. Observing the results of a column set when SELECT * is used  
 The following example selects all the columns from the table that contains a column set. It returns an XML column with the combined values of the sparse columns. It does not return the sparse columns individually.  
  
```  
SELECT DocID, Title, SpecialPurposeColumns FROM DocumentStoreWithColumnSet ;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `DocID Title        SpecialPurposeColumns`  
  
 `1      Tire Spec 1  <ProductionSpecification>AXZZ217</ProductionSpecification><ProductionLocation>27</ProductionLocation>`  
  
 `2      Survey 2142  <MarketingSurveyGroup>Men 25 - 35</MarketingSurveyGroup>`  
  
 `3      Tire Spec 2  <ProductionSpecification>AXW9R411</ProductionSpecification><ProductionLocation>38</ProductionLocation>`  
  
### E. Observing the results of selecting the column set by name  
 Because the Production department is not interested in the marketing data, this example adds a `WHERE` clause to restrict the output. The example uses the name of the column set.  
  
```  
SELECT DocID, Title, SpecialPurposeColumns  
FROM DocumentStoreWithColumnSet  
WHERE ProductionSpecification IS NOT NULL ;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `DocID Title        SpecialPurposeColumns`  
  
 `1     Tire Spec 1  <ProductionSpecification>AXZZ217</ProductionSpecification><ProductionLocation>27</ProductionLocation>`  
  
 `3     Tire Spec 2  <ProductionSpecification>AXW9R411</ProductionSpecification><ProductionLocation>38</ProductionLocation>`  
  
### F. Observing the results of selecting sparse columns by name  
 When a table contains a column set, you can still query the table by using the individual column names as shown in the following example.  
  
```  
SELECT DocID, Title, ProductionSpecification, ProductionLocation   
FROM DocumentStoreWithColumnSet  
WHERE ProductionSpecification IS NOT NULL ;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `DocID Title        ProductionSpecification ProductionLocation`  
  
 `1     Tire Spec 1  AXZZ217                 27`  
  
 `3     Tire Spec 2  AXW9R411                38`  
  
### G. Updating a table by using a column set  
 The following example updates the third record with new values for both sparse columns that are used by that row.  
  
```  
UPDATE DocumentStoreWithColumnSet  
SET SpecialPurposeColumns = '<ProductionSpecification>ZZ285W</ProductionSpecification><ProductionLocation>38</ProductionLocation>'  
WHERE DocID = 3 ;  
GO  
```  
  
> [!IMPORTANT]  
>  An UPDATE statement that uses a column set updates all the sparse columns in the table. Sparse columns that are not referenced are updated to NULL.  
  
 The following example updates the third record, but only specifies the value of one of the two populated columns. The second column `ProductionLocation` is not included in the `UPDATE` statement and is updated to NULL.  
  
```  
UPDATE DocumentStoreWithColumnSet  
SET SpecialPurposeColumns = '<ProductionSpecification>ZZ285W</ProductionSpecification>'  
WHERE DocID = 3 ;  
GO  
```  
  
## See Also  
 [Use Sparse Columns](../../relational-databases/tables/use-sparse-columns.md)  
  
  

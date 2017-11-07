---
title: "PolyBase Queries | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "03/09/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine-polybase"
ms.tgt_pltfrm: ""
ms.topic: "article"
keywords: 
  - "PolyBase"
helpviewer_keywords: 
  - "PolyBase, import and export"
  - "Hadoop, import with PolyBase"
  - "Hadoop, export with PolyBase"
  - "Azure blob storage, import with PolyBase"
  - "Azure blob storage, export with PolyBase"
ms.assetid: 2c5aa2bd-af7d-4f57-9a28-9673c2a4c07e
caps.latest.revision: 18
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# PolyBase Queries
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Here are example queries using the [PolyBase Guide](../../relational-databases/polybase/polybase-guide.md) feature of SQL Server 2016. Before using these examples, you must also understand the T-SQL statements required to setup PolyBase (See [PolyBase T-SQL objects](../../relational-databases/polybase/polybase-t-sql-objects.md).)  
  
## Queries  
 Run Transact-SQL statements against external tables or use BI tools to query external tables.  
  
## SELECT from external table  
 A simple query that returns data from a defined external table.  
  
```tsql  
SELECT TOP 10 * FROM [dbo].[SensorData];   
```  
  
 A simple query that includes a predicate.  
  
```  
SELECT * FROM [dbo].[SensorData]   
WHERE Speed > 65;   
```  
  
## JOIN external tables with local tables  
  
```  
SELECT InsuranceCustomers.FirstName,   
                           InsuranceCustomers.LastName,   
                           SensorData.Speed  
FROM InsuranceCustomers INNER JOIN SensorData    
ON InsuranceCustomers.CustomerKey = SensorData.CustomerKey   
WHERE SensorData.Speed > 65   
ORDER BY SensorData.Speed DESC  
  
```  
  
## Pushdown computation to Hadoop  
 Variations of pushdown are shown here.  
  
### Pushdown for selecting a subset of rows  
 Use predicate pushdown to improve performance for a query that selects a subset of rows from an external table.  
  
 Here SQL Server 2016 initiates a map-reduce job to retrieve the rows that match the predicate customer.account_balance < 200000 on Hadoop. Since the query can complete successfully without scanning all of the rows in the table, only the rows that meet the predicate criteria are copied to SQL Server. This saves significant time and requires less temporary storage space when the number of customer balances < 200000 is small in comparison with the number of customers with account balances >= 200000.  
  Copy imageCopy Code   
SELECT * FROM customer WHERE customer.account_balance < 200000.  
  
```  
SELECT * FROM SensorData WHERE Speed > 65;  
```  
  
### Pushdown for selecting a subset of columns  
 Use predicate pushdown to improve performance for a query that selects a subset of columns from an external table.  
  
 In this query, SQL Server initiates a map-reduce job to pre-process the Hadoop delimited-text file so that only the data for the two columns, customer.name and customer.zip_code, will be copied to SQL Server PDW.  
  
```  
SELECT customer.name, customer.zip_code FROM customer WHERE customer.account_balance < 200000  
  
```  
  
### Pushdown for basic expressions and operators  
 SQL Server allows the following basic expressions and operators for predicate pushdown.  
  
-   Binary comparison operators ( \<, >, =, !=, <>, >=, <= ) for numeric, date, and time values.  
  
-   Arithmetic operators ( +, -, *, /, % ).  
  
-   Logical operators (AND, OR).  
  
-   Unary operators (NOT, IS NULL, IS NOT NULL).  
  
 The operators BETWEEN, NOT, IN, and LIKE might be pushed-down. This depends on how the query optimizer rewrites them as a series of statements that use basic relational operators.  
  
 This query has multiple predicates that can be pushed down to Hadoop. SQL Server can push map-reduce jobs to Hadoop to perform the predicate customer.account_balance <= 200000. The expression BETWEEN 92656 and 92677 is comprised of binary and logical operations that can be pushed to Hadoop. The logical AND of customer.account_balance and customer.zipcode is a final expression.  
  
 Putting this altogether, the map-reduce jobs can perform all of the WHERE clause. Only the data that meets the SELECT criteria will be copied back to SQL Server PDW.  
  
```  
SELECT * FROM customer WHERE customer.account_balance <= 200000 AND customer.zipcode BETWEEN 92656 AND 92677  
  
```  
  
### Force pushdown  
  
```  
SELECT * FROM [dbo].[SensorData]   
WHERE Speed > 65  
OPTION (FORCE EXTERNALPUSHDOWN);   
```  
  
### Disable pushdown  
  
```  
SELECT * FROM [dbo].[SensorData]   
WHERE Speed > 65  
OPTION (DISABLE EXTERNALPUSHDOWN);  
```  
  
## Import data  
 Import data from Hadoop or Azure Storage into SQL Server for persistent storage. Use SELECT INTO to import data referenced by an external table for persistent storage in SQL Server. Create a relational table on-the-fly and then create a column-store index on top of the table in a second step.  
  
```sql  
-- PolyBase Scenario 2: Import external data into SQL Server.  
-- Import data for fast drivers into SQL Server to do more in-depth analysis and  
-- leverage Columnstore technology.  
  
SELECT DISTINCT   
        Insured_Customers.FirstName, Insured_Customers.LastName,   
        Insured_Customers.YearlyIncome, Insured_Customers.MaritalStatus  
INTO Fast_Customers from Insured_Customers INNER JOIN   
(  
        SELECT * FROM CarSensor_Data where Speed > 35   
) AS SensorD  
ON Insured_Customers.CustomerKey = SensorD.CustomerKey  
ORDER BY YearlyIncome  
  
CREATE CLUSTERED COLUMNSTORE INDEX CCI_FastCustomers ON Fast_Customers;  
```  
  
## Export data  
Export data from SQL Server to Hadoop or Azure Storage. First, enable export functionality by setting the sp_configure value of 'allow polybase export' to 1. Next, create an external table that points to the destination directory. Then, use INSERT INTO to export data from a local SQL Server table to an external data source. The INSERT INTO statement creates the destination directory if it does not exist and the results of the SELECT statement are exported to the specified location in the specified file format. The external files are named *QueryID_date_time_ID.format*, where *ID* is an incremental identifier and *format* is the exported data format. For example, QID776_20160130_182739_0.orc.  
  
```sql  
-- PolyBase Scenario 3: Export data from SQL Server to Hadoop.  
-- Create an external table.   
CREATE EXTERNAL TABLE [dbo].[FastCustomers2009] (  
        [FirstName] char(25) NOT NULL,   
        [LastName] char(25) NOT NULL,   
        [YearlyIncome] float NULL,   
        [MaritalStatus] char(1) NOT NULL  
)  
WITH (  
        LOCATION='/old_data/2009/customerdata',  
        DATA_SOURCE = HadoopHDP2,  
        FILE_FORMAT = TextFileFormat,  
        REJECT_TYPE = VALUE,  
        REJECT_VALUE = 0  
);  
  
-- Export data: Move old data to Hadoop while keeping it query-able via an external table.  
INSERT INTO dbo.FastCustomer2009  
SELECT T.* FROM Insured_Customers T1 JOIN CarSensor_Data T2  
ON (T1.CustomerKey = T2.CustomerKey)  
WHERE T2.YearMeasured = 2009 and T2.Speed > 40;  
```  
  
## New catalog views  
 The following new catalog views show external resources.  
  
```sql  
SELECT * FROM sys.external_data_sources;   
SELECT * FROM sys.external_file_formats;  
SELECT * FROM sys.external_tables;  
```  
  
 Determine if a table is an external table by using `is_external`  
  
```sql  
SELECT name, type, is_external FROM sys.tables WHERE name='myTableName'   
```  
  
## Next steps  
 To learn more about troubleshooting, see [PolyBase troubleshooting](../../relational-databases/polybase/polybase-troubleshooting.md).  
  
  

---
title: "Queries on External Tables With Predicate Pushdown (SQL Server PDW)"
ms.custom: na
ms.date: 07/21/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 917c74c6-6679-4576-a107-b90200c611a9
caps.latest.revision: 5
author: BarbKess
---
# Queries on External Tables With Predicate Pushdown (SQL Server PDW)
Explanations and examples of using predicate pushdown when querying Hadoop data with a SQL Server PDW query. Use this to improve performance of queries on external tables by performing some of the query work on Hadoop in order to reduce the size of the data that must be copied to SQL Server PDW.  
  
## Examples  
All of these queries assume that the job tracker ID was defined for the Hadoop data source.  
  
### Predicate pushdown for selecting a subset of rows  
You can use predicate pushdown to improve performance for a query that selects a subset of rows from an external table.  
  
In this query, SQL Server PDW will initiate a map-reduce job to retrieve the rows that match the predicate `customer.account_balance < 200000` on Hadoop. Since the query can complete successfully without scanning all of the rows in the table, only the rows that meet the predicate criteria are copied to SQL Server PDW. This saves significant time and requires less temporary storage space when the number of customer balances < 200000 is small in comparison with the number of customers with account balances >= 200000.  
  
```  
SELECT * FROM customer WHERE customer.account_balance < 200000  
```  
  
### Predicate pushdown for selecting a subset of columns  
You can use predicate pushdown to improve performance for a query that selects a subset of columns from an external table.  
  
In this query, SQL Server PDW will initiate a map-reduce job to pre-process the Hadoop delimited-text file so that only the data for the two columns, customer.name and customer.zip_code, will be copied to SQL Server PDW.  
  
```  
SELECT customer.name, customer.zip_code FROM customer WHERE customer.account_balance < 200000  
```  
  
### Predicate pushdown for basic expressions and operators  
SQL Server PDW will consider the following basic expressions and operators for predicate pushdown.  
  
-   Binary comparison operators ( <, >, =, !=, <>, >=, <= ) for numeric, date, and time values.  
  
-   Arithmetic operators ( +, -, *, /, % ).  
  
-   Logical operators (AND, OR).  
  
-   Unary operators (NOT, IS NULL, IS NOT NULL).  
  
The operators BETWEEN, NOT, IN, and LIKE might be pushed-down. This depends on how the query optimizer rewrites them as a series of statements that use basic relational operators.  
  
The following query has multiple predicates that can be pushed down to Hadoop. SQL Server PDW can push map-reduce jobs to Hadoop to perform the predicate `customer.account_balance <= 200000`. The expression BETWEEN 92656 and 92677 is comprised of binary and logical operations that can be pushed to Hadoop. The logical AND of customer.account_balance and customer.zipcode is a final expression.  
  
Putting this altogether, the map-reduce jobs can perform all of the WHERE clause. Only the data that meets the SELECT criteria will be copied back to SQL Server PDW.  
  
```  
SELECT * FROM customer WHERE customer.account_balance <= 200000 AND customer.zipcode BETWEEN 92656 AND 92677  
```  
  

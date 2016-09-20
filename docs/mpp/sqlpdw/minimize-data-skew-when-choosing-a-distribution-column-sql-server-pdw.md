---
title: "Minimize Data Skew When Choosing a Distribution Column (SQL Server PDW)"
ms.custom: na
ms.date: 07/21/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 574234b8-bf17-4cc1-9eb1-17fdc7f66736
caps.latest.revision: 4
author: BarbKess
---
# Minimize Data Skew When Choosing a Distribution Column (SQL Server PDW)
This article describes best practices for choosing a distribution column that minimizes data skew for a SQL Server PDW distributed table. Choosing a good distribution column is an important aspect of achieving storage efficiency and highly performant queries. This is possibly the most important design choice you will make in SQL Server PDW.  
  
The best choice for a distribution column depends on the data characteristics and the queries you intend to process. The goal is to choose a distribution column that minimizes data skew among the compute nodes and minimizes data movement during query processing. These are both important and sometimes competing considerations.  
  
## Best Practices  
The following best practices facilitate storing a similar number of rows in each distribution, which minimizes data skew.  
  
### Choose a column with high cardinality that does not have data that is heavily “skewed” – where a disproportionately large number of rows are associated with one or a very few column values.  
No distribution key value should have a disproportionate number of duplicates in comparison to the other distribution key values. For example, a common scenario in data warehousing is storing the clickstream data for a web site. In clickstream data, a disproportionate number of clicks can occur on only a few of the web site pages such as the Home page. If you use the web page URL as the distribution key, the table rows for each page will all be stored in the same distribution. If 30% of the clicks occur on the Home page, then 30% of the rows will all be stored in one distribution.  
  
Conversely, a distribution column can contain values with a large number of duplicates provided there are a lot of values with a large number of duplicates. Hence, high cardinality is important. For example, if you have 10,000 values with high counts and 100,000 values with low counts, all of the values can be distributed evenly across all of the distributions.  
  
Here's another example for why high cardinality is important. Suppose you have 80 distributions and are storing sales receipts for only 100 stores. If you use the store ID number for the distribution key, it is likely that there are not enough stores to spread the table rows evenly among the distributions. Even in the best case, where each distribution stores sales receipts for either  1 or  2 stores, some distributions would have twice as many table rows than other distributions. More likely, with only 100 different key values for 80 distributions, many distributions will end up being completely empty.  
  
### Choose a column that does not allow NULL, or where NULL values are as rare as any other column value.  
If a distribution key can be NULL, all rows with the NULL value will be stored in the same distribution. This is potentially a huge portion of rows that are skewed to the same distribution. For example, if 40% of the tables rows have NULL for the distribution key, then 40% of the table rows will be stored in the same distribution.  
  
### Choose an Integer column when possible  
Integer columns are preferable for the distribution column rather than varchar, decimal and datetime columns.  
  
### Avoid using datetime columns unless time is accurate to at least the minute.  
A datetime column might or might not be a good choice for the distribution column. For example, if a datetime column is accurate to the day, and typical queries access a single day or small number of days, most of the work for such queries will be restricted to a single distribution or small number of distributions, defeating the benefits of the MPP architecture.  However, if the datetime column is accurate to the minute or fraction of a second, data for even a single day query will be evenly spread across all distributions, and the column can therefore be a good choice for the distribution column.  
  
### Avoid choosing a key that is used as a single value in a WHERE clause.  
Some of the most popular queries in a sales data warehouse are queries on yesterday’s data. For example, if your query has WHERE DATE = ‘20100809’ and the table is distributed on the DATE key, the entire query will be run within one distribution. This is because all rows in the table that have the same date will be stored in the same distribution. As a result, you won’t get the benefits of parallelism.  
  
## Background  
Data skew is a condition that occurs when the rows of a distributed table are not spread uniformly across all of the distributions. When the number of rows in each distribution varies significantly, this can negatively impact query performance.  
  
Avoiding data skew means you want to avoid having a disproportionately large number of table rows in one distribution in comparison to the number of rows in other distributions. Since queries rely on work being performed on all distributions, even if queries finish fast in smaller distributions, you will still need to wait for the queries to finish on larger distributions before you can get the query result.  
  
### How to determine if data skew is an issue  
Don’t worry about making a mistake when you choose the distribution column. It’s easy to re-create the table using another distribution column by using the CREATE TABLE AS SELECT (CTAS) statement.  
  
To test your workloads with different distribution keys, you can create copies of the same table and assign a different distribution column to each table. You can then test your workloads against each table to see which distribution key gives the best overall performance. This type of iterative design and experimentation works well with SQL Server PDW architecture.  
  
To see the counts of rows per distribution in a table, use the command:  
  
DBCC PDW_SHOWSPACEUSED (dbo.<tablename>)  
  
For easy analysis, copy the results into a spreadsheet. Examine the results to determine whether any distributions contain disproportionately larger or smaller numbers of rows than the average. Rowcounts skewed 30% or higher could have a negative impact on query performance. The amount of acceptable data skew depends on your workload and performance requirements.  
  
You should absolutely choose a different distribution strategy if several distributions show 0 rows (for a large table), or if a small number of distributions show an order of magnitude more rows than the others.  
  
### Before you make changes to minimize data skew  
Before you decide to take further action, make sure you really do have a skew problem and understand what it will do to the overall workload on the appliance.  Generally, a 10% difference in the number of rows among distributions is acceptable. The amount of acceptable data skew depends on your workload and performance requirements.  
  
We have seen that 10-20% skew can be tolerable in PDW when considering the big scheme of things where many queries are going through the system concurrently.  Obviously, single-run queries that are affected by the skew will naturally run slower on the skewed distributions, but before you declare defeat, make sure that your customer (end user, not the DBA!) isn’t unhappy with performance.  
  
Just because data may be skewed on one or a few distributions doesn’t spell disaster.  Look at the customer’s queries and ensure that they actually will be affected.  
  
Naturally, if the queries are doing full distribution scans, then skew will affect them… but this is very rare in the real world.   If the distributions are also partitioned (e.g. by date) and the queries are restricted by date, then a good (possibly mixed-grain) partitioning strategy can work wonders.  Yes, the queries may still run a bit slower on the skewed partitions, but the performance is relative to the scan rate so it may not be that much slower.  Testing will tell for sure.  
  

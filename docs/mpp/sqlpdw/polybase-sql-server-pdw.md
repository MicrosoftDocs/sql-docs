---
title: "PolyBase (SQL Server PDW)"
ms.custom: na
ms.date: 08/10/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 7c7ae6ed-9c36-44aa-b286-5730f53a0a20
caps.latest.revision: 21
author: BarbKess
---
# PolyBase (SQL Server PDW)
Describes PolyBase for Hadoop Integration with SQL Server PDW.  
  
PolyBase is a fundamental breakthrough in data processing used in SQL Server PDW. It enables truly integrated querying across Hadoop non-relational data, Azure blob storage data, and SQL Server PDW relational data in a fully parallelized fashion. With Polybase you can query data stored in these external data sources by using standard SQL statements without learning MapReduce techniques.  
  
## Before You Begin  
  
### Software Prerequisites  
If not already configured, ask your appliance administrator to configure the appliance for Hadoop connectivity. For configuration instructions, see [Configure PolyBase Connectivity to External Data &#40;Analytics Platform System&#41;](../management/configure-polybase-connectivity-to-external-data-analytics-platform-system.md). For a list of supported Hadoop versions, see [sp_configure &#40;SQL Server PDW&#41;](../sqlpdw/sp-configure-sql-server-pdw.md).  
  
## Basics  
  
### Key Terms  
The following are key terms and concepts that you will need to know in order to better understand how to move data between a Hadoop Cluster and SQL Server PDW.  
  
Hadoop Distributed File System (HDFS)  
The Hadoop Distributed File System (HDFS) is the file system that Hadoop uses to store nonrelational data.  
  
external table  
Refers to data that resides outside of the appliance. The table definition is stored in the SQL Server PDW metadata. In this release, delimited text files are the only supported file format for accessing data that is stored in external tables.  
  
PolyBase for Hadoop Integration uses external tables to import Hadoop data into SQL Server PDW, query Hadoop data from SQL Server PDW, and export data from SQL Server PDW to Hadoop.  
  
### PolyBase Configuration  
Beginning with APS AU2, you can set a `polybase.pushdown.dfs.replication` parameter in either the `core-site.xml` or `hdfs-site.xml` files on the Control node. This performance related setting defines the data replication ratio for pushdown output. When set, it takes integer values between 1 and the maximum replication value allowed by the target cluster. If not set, this value defaults to 1. When set to a larger value, it may require more time for mappers to finish writing results to HDFS. The setting has the same effect whether set in `core-site` or `hdfs-site`. If set in both and `core-site` is not marked as final, the setting in `hdfs-site` is used. (This is the standard behavior of Hadoop.) This property only applies to data inserted by using Polybase.  
  
The Hadoop configuration files (such as **yarn-site.xml** and **core-site.xml**) used by PolyBase are stored on the Control Node at **C:\program files\Microsoft SQL Server Parallel Data Warehouse\100\Hadoop\conf\\**. You add or must modify the **yarn.application.classpath** configuration in the **yarn-site.xml** file to provide the APS Hadoop file location values specific to your environment. The **yarn.application.classpath** configuration setting in the **yarn-site.xml** files must be identical on both APS (for PolyBase) and on the Hadoop target that PolyBase is connecting to.  
  
## Related Statements  
  
### A. CREATE EXTERNAL DATA SOURCE  
[CREATE EXTERNAL DATA SOURCE &#40;SQL Server PDW&#41;](../sqlpdw/create-external-data-source-sql-server-pdw.md) creates an external data source. This objects contains the location of the data and an optional Hadoop job tracker ID. For a Hadoop cluster, the location is a Hadoop Distributed File System (HDFS) URL (e.g. 'hdfs://ip_address:port'). For WASB, the location is the WASB container and path (e.g., 'wasbs://[ container@ ] account_name.blob.core.windows.net/path').  
  
### B. CREATE EXTERNAL FILE FORMAT  
[CREATE EXTERNAL FILE FORMAT &#40;SQL Server PDW&#41;](../sqlpdw/create-external-file-format-sql-server-pdw.md) creates a file format definition for data stored in an external fileHadoop.  
  
### c. CREATE EXTERNAL TABLE  
[CREATE EXTERNAL TABLE &#40;SQL Server PDW&#41;](../sqlpdw/create-external-table-sql-server-pdw.md) creates the external table definition and stores the definition in SQL Server PDW metadata.  The external table definition points to text-delimited files that reside on a Hadoop Cluster. The data in the Hadoop text-delimited files is populated by your own Hadoop process.  
  
Use CREATE EXTERNAL TABLE to define an external table so that you can run SQL Server PDWSQL queries to select or import Hadoop data.  
  
### D. CREATE TABLE AS SELECT  
[CREATE TABLE AS SELECT &#40;SQL Server PDW&#41;](../sqlpdw/create-table-as-select-sql-server-pdw.md) creates a new table in SQL Server PDW that is populated with the results of a SELECT statement. By referencing an external table in the SELECT FROM clause, you can import data from Hadoop for persistent storage in either a distributed or replicated SQL Server PDW table.  
  
To import Hadoop data from an external table, use CREATE TABLE AS SELECT and select from the external table.  
  
### C. CREATE EXTERNAL TABLE AS SELECT  
[CREATE EXTERNAL TABLE AS SELECT &#40;SQL Server PDW&#41;](../sqlpdw/create-external-table-as-select-sql-server-pdw.md) creates a new external table, and exports the results of the SELECT statement to Hadoop. The data are stored in text-delimited files in Hadoop. The location of the files is specified in the table definition. exports the results of the SELECT statement to the text-delimited files on a Hadoop cluster.  
  
Use CREATE EXTERNAL TABLE AS SELECT to export data from SQL Server PDW to Hadoop where the data will be stored as text-delimited files.  
  
### D. DROP EXTERNAL TABLE  
[DROP TABLE &#40;SQL Server PDW&#41;](../sqlpdw/drop-table-sql-server-pdw.md) includes the DROP EXTERNAL TABLE statement. This drops the external table. Dropping an external table removes the table definition from SQL Server PDW. It does not remove any data that exists on the Hadoop.  
  
## Related Tasks  
  
|Task|Task Description|  
|--------|--------------------|  
|Configure the appliance for Hadoop connections.|[Configure PolyBase Connectivity to External Data &#40;Analytics Platform System&#41;](../management/configure-polybase-connectivity-to-external-data-analytics-platform-system.md)|  
|Create an external table for the purpose of importing data from Hadoop into SQL Server PDW.|[CREATE EXTERNAL TABLE &#40;SQL Server PDW&#41;](../sqlpdw/create-external-table-sql-server-pdw.md)|  
|Query Hadoop data, stored in an external table, from SQL Server PDW.|[SELECT &#40;SQL Server PDW&#41;](../sqlpdw/select-sql-server-pdw.md), [FROM &#40;SQL Server PDW&#41;](../sqlpdw/from-sql-server-pdw.md)<br /><br />Specify the name of the external table as the table source in the FROM clause of a SELECT statement.|  
|Import data from Hadoop and store it into a SQL Server PDW table.|[CREATE TABLE AS SELECT &#40;SQL Server PDW&#41;](../sqlpdw/create-table-as-select-sql-server-pdw.md)<br /><br />Use CREATE TABLE AS SELECT to create a new table in SQL Server PDW and populate it with the results of a SELECT statement by specifying the name of the external table as the table source in the FROM clause.|  
|Export data from SQL Server PDW and save it in Hadoop.|[CREATE EXTERNAL TABLE AS SELECT &#40;SQL Server PDW&#41;](../sqlpdw/create-external-table-as-select-sql-server-pdw.md)<br /><br />Use CREATE EXTERNAL TABLE AS SELECT to create a new external table, and use the SELECT statement to define which data you want to export to Hadoop. SQL Server PDW will export the results of the SELECT statement to the external table stored on Hadoop.|  
  
## See Also  
[PolyBase](http://www.microsoft.com/en-us/sqlserver/solutions-technologies/data-warehousing/polybase.aspx)  
[Understanding SQL Server PDW &#40;SQL Server PDW&#41;](../sqlpdw/understanding-sql-server-pdw-sql-server-pdw.md)  
  

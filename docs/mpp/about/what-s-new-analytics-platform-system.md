---
title: "What&#39;s New (Analytics Platform System)"
ms.custom: na
ms.date: 08/10/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 41c1c040-7e9a-4a93-bfe5-28c77f8d3ac6
caps.latest.revision: 190
author: BarbKess
---
# What&#39;s New (Analytics Platform System)
Learn about what’s new in Microsoft® Analytics Platform System, a data platform that hosts SQL Server Parallel Data Warehouse (PDW) and HDInsight (HDI) workloads all on the same appliance.  
  
## Contents  
Appliance Update 5  
  
-   [AU5: Transact-SQL](#au5tsql)  
  
-   [AU5: PolyBase](#au5polybase)  
  
-   [AU5: Data Loading](#au5dataloading)  
  
Appliance Update 4  
  
-   [AU4 November 2015 hotfix: bcp](#au4bcp)  
  
-   [AU4: PolyBase](#au4polybase)  
  
-   [AU4: Transact-SQL](#au4tsql)  
  
Appliance Update 3  
  
-   [AU3: Documentation and Tools are Public](#public)  
  
-   [AU3: Transact-SQL Error Handling](#TSQLAU3)  
  
-   [AU3: Software Versions](#versions)  
  
-   [AU3: PolyBase Supports ORC Files](#orc)  
  
-   [AU3: Microsoft Data Management Gateway](#gateway)  
  
-   [AU3: Appliance Domain Simplification](#au3domain)  
  
Appliance Update 2  
  
-   [AU2: HDInsight Workload](#HDI)  
  
-   [AU2: Transact-SQL Compatibility](#TSQLAU2)  
  
-   [AU2: System Views](#AU2System)  
  
-   [AU2: PolyBase Enhancements](#AU2PolyBase)  
  
-   [AU2: Send Feedback to Microsoft](#feedback)  
  
-   [AU2: Get the latest SQL Server database tooling (formerly called SSDT)](#tooling)  
  
-   [AU2: Integration Services 2014 destination adapters](#SSIS)  
  
Appliance Update 1  
  
-   [AU1: New HDInsight Workload](#HDINSIGHT)  
  
-   [AU1: PolyBase for External Data Integration](#PolyBase1)  
  
-   [AU1: Using SQL Server PDW Appliance Update 1](#UsingPDW)  
  
Before you upgrade to the latest release, see:  
  
-   [Breaking Changes &#40;Analytics Platform System&#41;](../about/breaking-changes-analytics-platform-system.md)  
  
-   [Known Issues &#40;Analytics Platform System&#41;](../about/known-issues-analytics-platform-system.md)  
  
## <a name="au5tsql"></a>AU5: Transact-SQL  
AU5 now supports these Transact-SQL compatibility improvements to make it easier to migrate your scripts from SQL Server to SQL Server PDW.  
  
-   [sp_prepexec (Transact-SQL)](https://msdn.microsoft.com/library/ff848812.aspx) to preprocess dynamic queries.  
  
-   [SET NOCOUNT (Transact-SQL)](https://msdn.microsoft.com/library/ms189837.aspx) and [SET PARSEONLY (Transact-SQL)](https://msdn.microsoft.com/library/ms178629.aspx).  
  
-   [IS_MEMBER() (Transact-SQL)](https://msdn.microsoft.com/library/ms186271.aspx) and [IS_ROLEMEMBER()](https://msdn.microsoft.com/library/ee677633.aspx) in support of Windows Authentication.  
  
## <a name="au5polybase"></a>AU5: PolyBase  
PolyBase now supports these features and capabilities:  
  
-   Hortonworks (HDP 2.3) and Cloudera (CHD 5.5)  
  
-   String predicate pushdown into HDFS. This improves query performance for predicates that compare string values, especially since we have SQL column collation support for both SQL Server PDW and Linux HDFS clusters.  
  
-   Data ingestion from government cloud storage. This includes access to the China cloud storage system.  
  
-   Parquet columnar data to improve PolyBase query performance.  
  
## <a name="au5dataloading"></a>AU5: Data Loading  
SQL Server PDW now these these client tools and libraries:  
  
-   BCP for additional data loading scenarios  
  
-   bcp.exe command line interface for simple data import/export scenarios  
  
-   .NET SqlBulkCopy class  for custom application integration  
  
-   Integration Services SQL Server destination adapters to simplify ETL package management.  
  
-   SQL Server Native Client OLE DB and the Bulk Copy API to unlock access by many 3rd party ETL, reporting and analytic tools  
  
## <a name="au4bcp"></a>AU4 November 2015 hotfix: bcp  
After installing this hotfix, you can use [bcp Utility](https://msdn.microsoft.com/en-us/library/ms162802.aspx) to load data into SQL Server PDW.   The documentation is on MSDN in the link provided. The build number for this hotfix is 10.0.7568.0.  
  
## <a name="au4polybase"></a>AU4: PolyBase  
PolyBase supports these Hadoop distributions:  
  
-   Hortonworks HDP 1.3, 2.0, 2.1, 2.2 for both Windows and Linux  
  
-   Cloudera CDH 4.3, 5.1 on Linux  
  
To configure PolyBase on Analytics Platform System, see [Configure PolyBase Connectivity to External Data &#40;Analytics Platform System&#41;](../management/configure-polybase-connectivity-to-external-data-analytics-platform-system.md). For the updated list of configuration options, see  [PolyBase Configuration Options (Transact-SQL)](https://msdn.microsoft.com/library/mt143174.aspx%20on%20MSDN). For examples, see [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](https://msdn.microsoft.com/library/dn935022.aspx) on MSDN.  
  
## <a name="au4tsql"></a>AU4: Transact-SQL  
Scalar-valued user-defined functions are now supported. See [CREATE FUNCTION &#40;SQL Server PDW&#41;](../sqlpdw/create-function-sql-server-pdw.md).  
  
The round robin distribution type is now supported. For details, see [CREATE TABLE &#40;SQL Server PDW&#41;](../sqlpdw/create-table-sql-server-pdw.md).  
  
The pivot and unpivot operators are supported. For details, see [Using Pivot and Unpivot](https://msdn.microsoft.com/library/ms177410.aspx) on MSDN.  
  
## <a name="public"></a>AU3: Documentation and Tools are Public  
This documentation file and the client tools are now available publicly on the [Microsoft Download Center](http://www.microsoft.com/downloads/details.aspx?FamilyID=91b7e487-a12c-44b3-85cb-e9f90b639d8f).  
  
## <a name="TSQLAU3"></a>AU3: Transact-SQL Error Handling  
Functions and statements for error handling are now supported. See [@@ERROR](../sqlpdw/error-sql-server-pdw.md), [ERROR_MESSAGE](../sqlpdw/error-message-sql-server-pdw.md), [ERROR_NUMBER](../sqlpdw/error-number-sql-server-pdw.md), [ERROR_PROCEDURE](../sqlpdw/error-procedure-sql-server-pdw.md), [ERROR_SEVERITY](../sqlpdw/error-severity-sql-server-pdw.md), [ERROR_STATE](../sqlpdw/error-state-sql-server-pdw.md), [PRINT](../sqlpdw/print-sql-server-pdw.md), [RAISERROR](../sqlpdw/raiserror-sql-server-pdw.md), [THROW](../sqlpdw/throw-sql-server-pdw.md), [TRY...CATCH](../sqlpdw/try-catch-sql-server-pdw.md), [XACT_STATE](../sqlpdw/xact-state-sql-server-pdw.md).  
  
## <a name="versions"></a>AU3: Software Versions  
APS now runs on Windows Server 2012 R2. User and system data is stored in SQL Server 2014 databases.  
  
## <a name="orc"></a>AU3: PolyBase Supports ORC Files  
PolyBase now supports the Optimized Row Columnar (ORC) file format for external files stored on HDFS. This option requires Hive version 0.11 or higher. In conjunction with Hortonworks Data Platform (HDP) 2.0, ORC file formats offer better compression and performance than the RCFILE formats. To use the ORC file, see [CREATE EXTERNAL FILE FORMAT &#40;SQL Server PDW&#41;](../sqlpdw/create-external-file-format-sql-server-pdw.md)  
  
## <a name="gateway"></a>AU3: Microsoft Data Management Gateway  
Now you can use Microsoft Data Management Gateway to enable Power BI scenarios on PDW. For example, Power Query and Power Map for Excel, and Power BI Q&A can now access on-premises PDW data through secure OData feeds. Data Management Gateway is pre-installed on the Control node and pre-configured by Microsoft Support. To configure Power BI to access PDW, see [Connect with Power BI or Power Query by using Data Management Gateway &#40;Analytics Platform System&#41;](../sqlpdw/connect-with-power-bi-or-power-query-by-using-data-management-gateway-analytics-platform-system.md).  
  
## <a name="au3domain"></a>AU3: Appliance Domain Simplification  
The APS appliance now has one domain. For high availability, there are two replicated Active Directory domain controllers that run on the orchestration host and the passive host. There is no longer a need for the domain controller to failover to a passive host; if one fails, the other is already online.  
  
This change has enabled flexibility for all virtual machines configured for failover. Since the domain controller does not failover, there is no longer a need for all of the virtual machines running on the same host to failover. As a result, the corralling service is no longer needed to set the start order for virtual machines. Now, a Control node, Compute node, or appliance fabric virtual machine can fail to any available passive host.  
  
DNS entries and connection strings pointing to the PDW domain must be redirected to the appliance domain. For example a pointer to ***PDW_domain*-CTL01.*PDW_domain*.pdw.local** should be adjusted to be ***PDW_region*-CTL01.*appliance_domain*.local**.  
  
For more information and some diagrams, see [PDW and Appliance Fabric Physical Components &#40;Analytics Platform System&#41;](../management/pdw-and-appliance-fabric-physical-components-analytics-platform-system.md).  
  
## <a name="HDI"></a>AU2: HDInsight Workload  
The HDInsight region is updated to Hortonworks Data Platform (HDP) 2.0. The YARN project is introduced as part of Hadoop 2.0. It takes over responsibility for all cluster resource management from the MapReduce project. For further details about YARN, see the [Hortonworks](http://hortonworks.com/hadoop/yarn/) web site.  
  
For updated port specifications for external data sources with HDP 2.0, see [CREATE EXTERNAL DATA SOURCE &#40;SQL Server PDW&#41;](../sqlpdw/create-external-data-source-sql-server-pdw.md).  
  
## <a name="TSQLAU2"></a>AU2: Transact-SQL Compatibility  
  
### User-Defined Schemas  
User-defined schemas are supported. For more information, see [CREATE SCHEMA &#40;SQL Server PDW&#41;](../sqlpdw/create-schema-sql-server-pdw.md).  
  
### Statistics  
The following statistics elements are now supported:  
  
-   Filtered statistics are in [CREATE STATISTICS &#40;SQL Server PDW&#41;](../sqlpdw/create-statistics-sql-server-pdw.md).  
  
-   Sampling by PERCENT is in [CREATE STATISTICS &#40;SQL Server PDW&#41;](../sqlpdw/create-statistics-sql-server-pdw.md). You can use this option on both regular and external tables.  
  
-   [DBCC SHOW_STATISTICS &#40;PDW&#41;](../sqlpdw/dbcc-show-statistics-pdw.md) shows the header, density vector, and histogram for Control node statistics.  
  
-   [STATS_DATE &#40;PDW&#41;](../sqlpdw/stats-date-pdw.md) is added to show the date of the most recent Control node statistics.  
  
### Functions  
The following functions are now supported: [ACOS](../sqlpdw/acos-sql-server-pdw.md), [ASCII](../sqlpdw/ascii-sql-server-pdw.md), [ATN2](../sqlpdw/atn2-sql-server-pdw.md), [CHAR](../sqlpdw/char-sql-server-pdw.md), [CONCAT](../sqlpdw/concat-sql-server-pdw.md), [COT](../sqlpdw/cot-sql-server-pdw.md), [DATALENGTH](../sqlpdw/datalength-sql-server-pdw.md), [DATEFROMPARTS](../sqlpdw/datefromparts-sql-server-pdw.md), [DATENAME](../sqlpdw/datename-sql-server-pdw.md), [DATETIMEFROMPARTS](../sqlpdw/datetimefromparts-sql-server-pdw.md), [DATETIME2FROMPARTS](../sqlpdw/datetime2fromparts-sql-server-pdw.md), [DATETIMEOFFSETFROMPARTS](../sqlpdw/datetimeoffsetfromparts-sql-server-pdw.md), [DEGREES](../sqlpdw/degrees-sql-server-pdw.md), [DIFFERENCE](../sqlpdw/difference-sql-server-pdw.md), [EOMONTH](../sqlpdw/eomonth-sql-server-pdw.md), [GETUTCDATE](../sqlpdw/getutcdate-sql-server-pdw.md), [HASHBYTES](../sqlpdw/hashbytes-sql-server-pdw.md), [ISDATE](../sqlpdw/isdate-sql-server-pdw.md), [NCHAR](../sqlpdw/nchar-sql-server-pdw.md), [PARSENAME &#40;SQL Server PDW&#41;](../sqlpdw/parsename-sql-server-pdw.md), [PI](../sqlpdw/pi-sql-server-pdw.md), [RADIANS](../sqlpdw/radians-sql-server-pdw.md), [REVERSE](../sqlpdw/reverse-sql-server-pdw.md), [SMALLDATETIMEFROMPARTS](../sqlpdw/smalldatetimefromparts-sql-server-pdw.md), [SOUNDEX](../sqlpdw/soundex-sql-server-pdw.md), [SPACE](../sqlpdw/space-sql-server-pdw.md), [SQL_VARIANT_PROPERTY](../sqlpdw/sql-variant-property-sql-server-pdw.md), [STUFF](../sqlpdw/stuff-sql-server-pdw.md), [SWITCHOFFSET](../sqlpdw/switchoffset-sql-server-pdw.md), [SYSDATETIME &#40;SQL Server PDW&#41;](../sqlpdw/sysdatetime-sql-server-pdw.md), [SYSDATETIMEOFFSET](../sqlpdw/sysdatetimeoffset-sql-server-pdw.md), [SYSUTCDATETIME](../sqlpdw/sysutcdatetime-sql-server-pdw.md), [TERTIARY_WEIGHTS](../sqlpdw/tertiary-weights-sql-server-pdw.md), [TIMEFROMPARTS](../sqlpdw/timefromparts-sql-server-pdw.md), [TODATETIMEOFFSET](../sqlpdw/todatetimeoffset-sql-server-pdw.md), [TYPE_ID](../sqlpdw/type-id-sql-server-pdw.md), [TYPE_NAME](../sqlpdw/type-name-sql-server-pdw.md), [TYPEPROPERTY](../sqlpdw/typeproperty-sql-server-pdw.md), [UNICODE](../sqlpdw/unicode-sql-server-pdw.md), and [ODBC Scalar Functions](../sqlpdw/odbc-scalar-functions-sql-server-pdw.md).  
  
The DAX functions EXACT and REPLACE are now supported in DirectQuery operations against a PDW data source.  
  
## <a name="AU2System"></a>AU2: System Views  
[sys.pdw_nodes_pdw_physical_databases &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-nodes-pdw-physical-databases-sql-server-pdw.md) was added in AU1, but was not mentioned in the docs until now.  
  
## <a name="AU2PolyBase"></a>AU2: PolyBase Enhancements  
PolyBase now supports connections to Hortonworks (HDP 2.0) for Windows Server, HDInsight on Analytics Platform System (version AU2) (HDP 2.0), Azure blob storage on Microsoft Azure (WASB[S]), Hortonworks (HDP 2.0) for Linux. For more information, see [sp_configure &#40;SQL Server PDW&#41;](../sqlpdw/sp-configure-sql-server-pdw.md).  
  
A new query hint (EXTERNALPUSHDOWN) can force or disable the pushdown of the computation of qualifying expressions or operators in Hadoop. For more information, see [OPTION &#40;SQL Server PDW&#41;](../sqlpdw/option-sql-server-pdw.md).  
  
## <a name="feedback"></a>AU2: Send Feedback to Microsoft  
Analytics Platform System has an optional telemetry feature that sends Admin Console data to Microsoft. We encourage you to enable this to help us improve the product. For more information, see [Send Telemetry Feedback to Microsoft &#40;SQL Server PDW&#41;](../management/send-telemetry-feedback-to-microsoft-sql-server-pdw.md).  
  
## <a name="tooling"></a>AU2: Get the latest SQL Server database tooling (formerly called SSDT)  
SQL Server database tooling, formerly called SQL Server Data Tools, is available in Visual Studio 2012 and 2013. AU2 support is included in the July 2014 update for both versions.  
  
To install the updates, open Visual Studio and go to the Tools > Extensions and Updates… menu. In the Extensions and Updates dialog, click on **Updates**. Depending on which updates you have already applied, you could see **Microsoft SQL Server Update for database tooling** and **Visual Studio 2013 Update 3** in the list.  
  
If you have Visual Studio 2013 Ultimate, Premium, or Professional, you need to update SQL Server database tooling component. If you have Visual Studio 2013 Express for Windows Desktop, you need to apply Visual Studio Update 3 in addition to the SQL Server database tooling update. We recommend that you install Visual Studio Update 3, regardless of the edition you have to ensure you have the latest bug fixes from Visual Studio.  
  
Install the updates you need. Restart Visual Studio after updates are applied if it was not closed during the update.  
  
If you have Visual Studio 2012, install the latest download of SQL Server data tooling from [SQL Server Data Tools for Visual Studio 2012](http://msdn.microsoft.com/en-us/data/hh297027).  
  
## <a name="SSIS"></a>AU2: Integration Services 2014 destination adapters  
Integration Services 2014 destination adapters are available in the C:\PDWINST\ClientTools directory on the Control node. For more information, see [Install Integration Services Destination Adapters &#40;SQL Server PDW&#41;](../sqlpdw/install-integration-services-destination-adapters-sql-server-pdw.md).  
  
## <a name="HDINSIGHT"></a>AU1: New HDInsight Workload  
SQL Server PDW has evolved into Microsoft Analytics Platform System, a single fabric that can host both SQL Server PDW and Microsoft’s HDInsight running on separate regions of nodes, all within the same appliance.  
  
Microsoft’s HDInsight is a 100 percent Apache Hadoop-based data platform that manages data of any type, whether relational or nonrelational, and of any size. By having the HDInsight region, you can store and analyze HDInsight data on your own premises, integrate SQL Server PDW’s relational data with HDInsight’s non-relational data, and integrate your on-premises HDInsight data with Azure blob storage. Business intelligence solutions can quickly integrate relational and non-relational data that is stored on the same appliance since the appliance InfiniBand supports fast data transfer up to 56 Gb/s, and the data transfers in parallel between HDInsight’s DataNodes and PDW’s Compute nodes.  
  
The HDInsight workload is optional. Ask your sales consultant about pricing and licensing options for each workload.  
  
For more information about HDInsight, see [HDInsight  &#40;Analytics Platform System&#41;](../hdinsight/hdinsight-analytics-platform-system.md) and [HDInsight Topology &#40;Analytics Platform System&#41;](../management/hdinsight-topology-analytics-platform-system.md).  
  
## <a name="PolyBase1"></a>AU1: PolyBase for External Data Integration  
  
### Queries perform predicate pushdown  
For performance improvements when querying external tables, PolyBase now performs predicate pushdown for queries on external Hadoop tables. To do this, SQL Server PDW issues map/reduce jobs to pre-compute some query predicate expressions in order to reduce the size of the data that must be copied to SQL Server PDW for query execution. For more information, see [Queries on External Tables With Predicate Pushdown &#40;SQL Server PDW&#41;](../sqlpdw/queries-on-external-tables-with-predicate-pushdown-sql-server-pdw.md)  
  
### New configuration options  
You can now import and export data between SQL Server PDW and Azure blob storage. In addition to the external Hadoop platforms supported in the previous release, you can use HDInsight on Analytics Platform System as a Hadoop source.  
  
PolyBase now supports these external data sources:  
  
-   Hortonworks on Windows Server (HDP 1.3 and HDP 2.0)  
  
-   Hortonworks on Linux (HDP 1.3 and 2.0)  
  
-   Cloudera CDH 4.3 on Linux  
  
-   HDInsight’s Azure blob storage (WASB[S])  
  
-   HDInsight on Analytics Platform System  
  
To configure Hadoop and Azure blob storage connectivity for the appliance, see [sp_configure &#40;SQL Server PDW&#41;](../sqlpdw/sp-configure-sql-server-pdw.md).  
  
### Statistics on external tables  
You can now create and drop statistics on external tables. To use statistics with external tables, simply use the name of an external table when you call [CREATE STATISTICS &#40;SQL Server PDW&#41;](../sqlpdw/create-statistics-sql-server-pdw.md), and [DROP STATISTICS &#40;SQL Server PDW&#41;](../sqlpdw/drop-statistics-sql-server-pdw.md). For more information, see [Understanding Query Optimization Statistics &#40;SQL Server PDW&#41;](../sqlpdw/understanding-query-optimization-statistics-sql-server-pdw.md).  
  
### New statements and syntax changes  
To provide a framework for external data, this release includes two new data definition language (DDL) statements, [CREATE EXTERNAL DATA SOURCE &#40;SQL Server PDW&#41;](../sqlpdw/create-external-data-source-sql-server-pdw.md) and [CREATE EXTERNAL FILE FORMAT &#40;SQL Server PDW&#41;](../sqlpdw/create-external-file-format-sql-server-pdw.md), that create a data source object and a file format object respectively.  
  
The syntax for [CREATE EXTERNAL TABLE &#40;SQL Server PDW&#41;](../sqlpdw/create-external-table-sql-server-pdw.md) and [CREATE EXTERNAL TABLE AS SELECT &#40;SQL Server PDW&#41;](../sqlpdw/create-external-table-as-select-sql-server-pdw.md) has changed to refer to these new objects. The previous syntax for these two statements is deprecated in this release. You can continue to use the previous syntax, but need to update scripts to use the new syntax in preparation for the next release.  
  
New DDL statements:  
  
-   [CREATE EXTERNAL DATA SOURCE &#40;SQL Server PDW&#41;](../sqlpdw/create-external-data-source-sql-server-pdw.md)  
  
-   [CREATE EXTERNAL FILE FORMAT &#40;SQL Server PDW&#41;](../sqlpdw/create-external-file-format-sql-server-pdw.md)  
  
-   [DROP EXTERNAL DATA SOURCE &#40;SQL Server PDW&#41;](../sqlpdw/drop-external-data-source-sql-server-pdw.md)  
  
-   [DROP EXTERNAL FILE FORMAT &#40;SQL Server PDW&#41;](../sqlpdw/drop-external-file-format-sql-server-pdw.md)  
  
Modifed DDL statements  
  
-   [CREATE EXTERNAL TABLE &#40;SQL Server PDW&#41;](../sqlpdw/create-external-table-sql-server-pdw.md)  
  
-   [CREATE EXTERNAL TABLE AS SELECT &#40;SQL Server PDW&#41;](../sqlpdw/create-external-table-as-select-sql-server-pdw.md)  
  
New system views  
  
-   [sys.external_tables &#40;SQL Server PDW&#41;](../sqlpdw/sys-external-tables-sql-server-pdw.md) (formerly sys.pdw_external_tables)  
  
-   [sys.external_file_formats &#40;SQL Server PDW&#41;](../sqlpdw/sys-external-file-formats-sql-server-pdw.md)  
  
-   [sys.external_data_sources &#40;SQL Server PDW&#41;](../sqlpdw/sys-external-data-sources-sql-server-pdw.md)  
  
-   [sys.dm_pdw_hadoop_operations &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-hadoop-operations-sql-server-pdw.md)  
  
-   [sys.dm_pdw_dms_external_work &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-dms-external-work-sql-server-pdw.md)  
  
## <a name="UsingPDW"></a>AU1: Using SQL Server PDW Appliance Update 1  
SQL Server Parallel Data Warehouse Appliance Update 1 now runs as one of the workloads on Analytics Platform System. PDW runs on an enhanced version of SQL Server 2012 Service Pack 1 that includes updates for in-memory-optimized clustered columnstore indexes. It also includes the new cardinality estimator that is in SQL Server 2014.  
  
### Adding PDW scale units is faster  
Adding SQL Server PDW scale units to the appliance is faster and requires less downtime than the previous release. The downtime is reduced because SQL Server PDW can stay online while a Microsoft support engineer configures the new scale units, and because it is no longer necessary to backup and restore SQL Server PDW databases in order to redistribute the data after adding scale units. When the new scale units are ready for data, SQL Server PDW automatically redistributes the existing data to the additional nodes.  
  
There is still downtime for adding scale units since the appliance must be completely shut down to physically add the new hardware, and PDW must be offline while it redistributes user data to all the Compute nodes. The time required for the redistribution depends on the size of the data.  
  
The redistribution requires some free space to exist in the existing SQL Server PDW region before you add scale units. The free space required is approximately the size of the largest existing table. Contact Microsoft support to find out how much free space you have and how much is required to expand your capacity.  
  
### Integrated Authentication  
SQL Server PDW now supports Windows authentication logins. For more information, see [Security - Windows Authentication and SQL Server Authentication &#40;SQL Server PDW&#41;](../sqlpdw/security-windows-authentication-and-sql-server-authentication-sql-server-pdw.md).  
  
### Transparent Data Encryption  
SQL Server PDW now supports transparent data encryption. For more information, see [Transparent Data Encryption &#40;SQL Server PDW&#41;](../sqlpdw/transparent-data-encryption-sql-server-pdw.md).  
  
To support TDE, a master key and certificates can now be created in the master database. A database encryption key can be created in a user database. For more information, see [CREATE MASTER KEY &#40;SQL Server PDW&#41;](../sqlpdw/create-master-key-sql-server-pdw.md), [CREATE CERTIFICATE &#40;SQL Server PDW&#41;](../sqlpdw/create-certificate-sql-server-pdw.md), and [CREATE DATABASE ENCRYPTION KEY &#40;SQL Server PDW&#41;](../sqlpdw/create-database-encryption-key-sql-server-pdw.md). Additional DMV's and stored procedures are added to support TDE. For more information, see [Transparent Data Encryption &#40;SQL Server PDW&#41;](../sqlpdw/transparent-data-encryption-sql-server-pdw.md).  
  
### Appliance Domain Model  
The appliance domain configuration has changed to support HDInsight and Windows authentication. For more information, see [PDW Domain Security &#40;SQL Server PDW&#41;](../sqlpdw/pdw-domain-security-sql-server-pdw.md), [Security - Configure Domain Trusts &#40;SQL Server PDW&#41;](../sqlpdw/security-configure-domain-trusts-sql-server-pdw.md), and [Understanding the Security Model of the HDInsight Region &#40;Analytics Platform System&#41;](../hdinsight/understanding-the-security-model-of-the-hdinsight-region-analytics-platform-system.md).  
  
### dwloader Loads Data Directly to the Compute Nodes  
dwloader now loads data directly to each of the Compute nodes without passing the data through the Control node. To load data, dwloader communicates with the Control node to gain network access to the Compute nodes, and then it transfers data in a round-robin manner to the Compute nodes.  
  
Data Movement Service (DMS) on each Compute node receives the raw data from dwloader, processes it, and then moves the processed data in parallel to the correct Compute node instance of SQL Server.  
  
For more information, see [Load &#40;SQL Server PDW&#41;](../sqlpdw/load-sql-server-pdw.md).  
  
### dwloader uses less restrictive data type conversion rules  
[dwloader Command-Line Loader &#40;SQL Server PDW&#41;](../sqlpdw/dwloader-command-line-loader-sql-server-pdw.md) now aligns more closely with SQL Server behavior for reading input data from the input file and converting it to the target data type. The new behavior enforces the specified order of year, month, and day, but allows flexibility for how each is formatted.  
  
To specify a date format use one of the new parameters for the –D option.  
  
-D { mdy | myd | **ymd** | ydm | dmy | dym | *custom_date_format* }  
  
The new conversion rules are highly compatible with the previous release, however, there are some differences. Please be sure to read the [Breaking Changes &#40;Analytics Platform System&#41;](../about/breaking-changes-analytics-platform-system.md) to see a list of differences in behavior.  
  
### dwloader batch size is calculated dynamically  
You do not need to specify the batchsize (-b batchsize) in dwloader. Beginning with SQL Server 2012 PDW, the Control node dynamically computes the batch size for the bulk copy that DMS performs into SQL Server instances on the Compute nodes.  
  
The **–B***batchsize* parameter should only be used at the suggestion of Microsoft Support. For more information, see [dwloader Command-Line Loader &#40;SQL Server PDW&#41;](../sqlpdw/dwloader-command-line-loader-sql-server-pdw.md).  
  
### SQL Server Data Tools update  
To use SQL Server Data Tools with SQL Server 2012 PDW Appliance Update 1, install the January 2014 release, or later, of SQL Server Data Tools for Visual Studio 2012 from the [Microsoft SQL Server Data Tools](http://msdn.microsoft.com/en-us/data/hh297027) download page on MSDN.  
  
In the January release, SQL Server Data Tools for Visual Studio 2012 is the only supported version for SQL Server PDW Appliance Update 1. SQL Server Data Tools for Visual Studio 2010 is no longer supported. Note that Visual Studio 2013 does not require a separate download because it already has SQL Server Data Tools integrated; however, SQL Server Data Tools in Visual Studio 2013 does not yet have support for SQL Server PDW. This will change in an upcoming release of Visual Studio 2013.  
  
The January release of SQL Server Data Tools for Visual Studio 2012 supports SQL Server 2012 PDW, SQL Server PDW Appliance Update 0.5, and SQL Server PDW Appliance Update 1. When connected to Appliance Update 1, you will see these new features:  
  
-   For PolyBase, there is now an **External Resources** folder in SQL Server Object Explorer that shows the new external data source and external file format objects.  
  
-   Logins for Windows users and groups are now visible in the object explorer under [Database Instance]->Security->Logins.  
  
-   The security objects Master Key, Certificates, and Database Encryption Keys are now visible in object explorer.  
  
For more information, see [Install SQL Server database tooling  for Visual Studio &#40;SQL Server PDW&#41;](../sqlpdw/install-sql-server-database-tooling-for-visual-studio-sql-server-pdw.md).  
  
### Upgrade sets all databases to compatibility level 110  
After upgrading to SQL Server 2012 PDW Appliance Update 1, all databases will use database compatibility level 110, which is the compatibility level for SQL Server 2012. This ensures that all databases will be able to use all of the SQL Server 2012 functionality that PDW uses. For example, SQL Server 2008 R2 databases cannot use all of the SQL functions, such as PERCENTILE_CONT, and the new cardinality estimator until they are upgraded to compatibility level 110.  
  
> [!NOTE]  
> Note, both SQL Server 2012 PDW and SQL Server 2012 PDW Appliance Update 1 have the benefits of using the new cardinality estimator that is in SQL Server 2014.  
  
Databases that were created on SQL Server 2012 PDW already have compatibility level 110. With this change, any existing databases that were created with SQL Server 2008 R2 PDW will be set to compatibility level 110 after the upgrade. Also, any SQL Server 2008 R2 PDW databases that are restored to SQL Server 2012 PDW Appliance Update 1 will have compatibility level 110 after the restore.  
  
## See Also  
[Breaking Changes &#40;Analytics Platform System&#41;](../about/breaking-changes-analytics-platform-system.md)  
[Known Issues &#40;Analytics Platform System&#41;](../about/known-issues-analytics-platform-system.md)  
  

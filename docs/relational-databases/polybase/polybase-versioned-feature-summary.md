---
title: "PolyBase features and limitations"
description: "PolyBase features available for SQL Server products and services, including a list of T-SQL operators supported for pushdown and known limitations."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 08/30/2022
ms.service: sql
ms.subservice: polybase
ms.topic: conceptual
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# PolyBase features and limitations

[!INCLUDE[appliesto-ss2016-asdb-asdw-pdw-md](../../includes/tsql-appliesto-ss2016-all-md.md)]

This article is a summary of PolyBase features available for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] products and services.

## Feature summary for product releases

This table lists the key features for PolyBase and the products in which they're available.

|**Feature** |**[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]** (Beginning with 2016) |**Azure SQL Database** |**[!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)]** |**Parallel Data Warehouse** |
|---------|---------|---------|---------|---------|
|Query Hadoop data with [!INCLUDE[tsql](../../includes/tsql-md.md)]|Yes|No|No|Yes|
|Import data from Hadoop|Yes|No|No|Yes|
|Export data to Hadoop  |Yes|No|No| Yes|
|Query, import from, export to Azure HDInsight |No|No|No|No
|Push down query computations to Hadoop|Yes|No|No|Yes|  
|Import data from Azure Blob storage|Yes|Yes<sup>*</sup>|Yes|Yes|
|Export data to Azure Blob storage|Yes|No|Yes|Yes|  
|Import data from Azure Data Lake Store|No|No|Yes|No|
|Export data to Azure Data Lake Store|No|No|Yes|No|
|Run PolyBase queries from Microsoft BI tools|Yes|No|Yes|Yes|

<sup>*</sup> Introduced in [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)], see [Examples of bulk access to data in Azure Blob storage](../import-export/examples-of-bulk-access-to-data-in-azure-blob-storage.md).


## Known limitations

PolyBase has the following limitations:

- Before [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], the maximum possible row size, which includes the full length of variable length columns, can't exceed 32 KB in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or 1 MB in [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)]. Starting with [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], this limitation is lifted. The limit remains 1 MB for Hadoop data sources, but is limited only by the maximum [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] limit for other data sources.

- When data is exported into an ORC file format from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)], text-heavy columns might be limited. They can be limited to as few as 50 columns because of Java out-of-memory error messages. To work around this issue, export only a subset of the columns.

- PolyBase can't connect to any Hadoop instance if Knox is enabled.

- If you use Hive tables with transactional = true, PolyBase can't access the data in the Hive table's directory.

- PolyBase services require SQL Server service to have TCP/IP network protocol enabled to function correctly. Additionally, if TCP/IP Protocol configuration setting **Listen All** is set to **No**, you must still have an entry for the correct listener port in either **TCP Dynamic Ports** or **TCP Ports** under **IPAll** in TCP/IP Properties. This is required due to the way PolyBase services resolve the listener port of the SQL Server Engine.

- PolyBase on SQL Server on Linux will not function if IPv6 is disabled in the kernel. See [Release notes for SQL Server 2019 on Linux](../../linux/sql-server-linux-release-notes-2019.md#networking) for further information on this limitation.

- PolyBase services require Shared Memory protocol to be enabled to function properly.

- If you have a default SQL Server instance that is configured to listen on TCP port other than 1433, you cannot use it as a head node in a PolyBase scale-out group. When executing `sp_polybase_join_group`, if you pass 'MSSQLSERVER' as the instance name, SQL Server will assume port 1433 is the listener port, so the Data Movement service will be unable to connect to the head node when starting.

- Oracle synonyms are not supported for usage with PolyBase.

- UTF-8 collations are not supported for Hadoop external data sources.

- Starting in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], Hadoop is no longer supported.

<!--SQL Server 2016-->
::: moniker range="= sql-server-2016 "

- [PolyBase doesn't install when you add a node to a [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] failover cluster](https://support.microsoft.com/help/3173087/fix-polybase-feature-doesn-t-install-when-you-add-a-node-to-a-sql-server-2016-failover-cluster).

::: moniker-end

## Next steps

For more information about PolyBase, see [Introducing data virtualization with PolyBase](polybase-guide.md).

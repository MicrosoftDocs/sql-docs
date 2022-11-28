---
title: "tablediff Utility"
description: Use the tablediff utility to compare the data in two tables for non-convergence and troubleshoot non-convergence in a replication topology.
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: tools-other
ms.topic: conceptual
helpviewer_keywords: 
  - "comparing data"
  - "tablediff utility"
  - "tables [SQL Server replication]"
  - "table comparisons [SQL Server]"
  - "command prompt utilities [SQL Server], tablediff"
  - "troubleshooting [SQL Server replication], non-convergence"
  - "non-convergence [SQL Server]"
ms.assetid: 3c3cb865-7a4d-4d66-98f2-5935e28929fc
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017"
---
# tablediff Utility
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
  The **tablediff** utility is used to compare the data in two tables for non-convergence, and is particularly useful for troubleshooting non-convergence in a replication topology. This utility can be used from the command prompt or in a batch file to perform the following tasks:  
  
-   A row by row comparison between a source table in an instance of [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] acting as a replication Publisher and the destination table at one or more instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] acting as replication Subscribers.  
  
-   Perform a fast comparison by only comparing row counts and schema.  
  
-   Perform column-level comparisons.  
  
-   Generate a [!INCLUDE[tsql](../includes/tsql-md.md)] script to fix discrepancies at the destination server to bring the source and destination tables into convergence.  
  
-   Log results to an output file or into a table in the destination database.  
  
## Syntax  
  
```  
  
tablediff   
[ -? ] |   
{  
        -sourceserver source_server_name[\instance_name]  
        -sourcedatabase source_database  
        -sourcetable source_table_name   
    [ -sourceschema source_schema_name ]  
    [ -sourcepassword source_password ]  
    [ -sourceuser source_login ]  
    [ -sourcelocked ]  
        -destinationserver destination_server_name[\instance_name]  
        -destinationdatabase subscription_database   
        -destinationtable destination_table   
    [ -destinationschema destination_schema_name ]  
    [ -destinationpassword destination_password ]  
    [ -destinationuser destination_login ]  
    [ -destinationlocked ]  
    [ -b large_object_bytes ]   
    [ -bf number_of_statements ]   
    [ -c ]   
    [ -dt ]   
    [ -et table_name ]   
    [ -f [ file_name ] ]   
    [ -o output_file_name ]   
    [ -q ]   
    [ -rc number_of_retries ]   
    [ -ri retry_interval ]   
    [ -strict ]  
    [ -t connection_timeouts ]   
}  
```  
  
## Arguments  
 [ **-?** ]  
 Returns the list of supported parameters.  
  
 **-sourceserver** _source_server_name_[**\\**_instance\_name_]  
 Is the name of the source server. Specify *source_server_name* for the default instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Specify _source_server_name_**\\**_instance_name_ for a named instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 **-sourcedatabase** _source_database_  
 Is the name of the source database.  
  
 **-sourcetable** _source_table_name_  
 Is the name of the source table being checked.  
  
 **-sourceschema** _source_schema_name_  
 The schema owner of the source table. By default, the table owner is assumed to be dbo.  
  
 **-sourcepassword** _source_password_  
 Is the password for the login used to connect to the source server using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication.  
  
> [!IMPORTANT]  
>  When possible, supply security credentials at runtime. If you must store credentials in a script file, you should secure the file to prevent unauthorized access.  
  
 **-sourceuser** _source_login_  
 Is the login used to connect to the source server using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication. If *source_login* is not supplied, then Windows Authentication is used when connecting to the source server. [!INCLUDE[ssNoteWinAuthentication](../includes/ssnotewinauthentication-md.md)]  
  
 **-sourcelocked**  
 The source table is locked during the comparison using the TABLOCK and HOLDLOCK table hints.  
  
 **-destinationserver** _destination_server_name_[**\\**_instance_name_]  
 Is the name of the destination server. Specify *destination_server_name* for the default instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Specify _destination_server_name_**\\**_instance_name_ for a named instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 **-destinationdatabase** _subscription_database_  
 Is the name of the destination database.  
  
 **-destinationtable** _destination_table_  
 Is the name of the destination table.  
  
 **-destinationschema** _destination_schema_name_  
 The schema owner of the destination table. By default, the table owner is assumed to be dbo.  
  
 **-destinationpassword** _destination_password_  
 Is the password for the login used to connect to the destination server using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication.  
  
> [!IMPORTANT]  
>  When possible, supply security credentials at runtime. If you must store credentials in a script file, you should secure the file to prevent unauthorized access.  
  
 **-destinationuser** _destination_login_  
 Is the login used to connect to the destination server using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication. If *destination_login* is not supplied, then Windows Authentication is used when connecting to the server. [!INCLUDE[ssNoteWinAuthentication](../includes/ssnotewinauthentication-md.md)]  
  
 **-destinationlocked**  
 The destination table is locked during the comparison using the TABLOCK and HOLDLOCK table hints.  
  
 **-b** _large_object_bytes_  
 Is the number of bytes to compare for large object data type columns, which includes: **text**, **ntext**, **image**, **varchar(max)**, **nvarchar(max)** and **varbinary(max)**. *large_object_bytes* defaults to the size of the column. Any data above *large_object_bytes* will not be compared.  
  
 **-bf**  _number_of_statements_  
 Is the number of [!INCLUDE[tsql](../includes/tsql-md.md)] statements to write to the current [!INCLUDE[tsql](../includes/tsql-md.md)] script file when the **-f** option is used. When the number of [!INCLUDE[tsql](../includes/tsql-md.md)] statements exceeds *number_of_statements*, a new [!INCLUDE[tsql](../includes/tsql-md.md)] script file is created.  
  
 **-c**  
 Compare column-level differences.  
  
 **-dt**  
 Drop the result table specified by *table_name*, if the table already exists.  
  
 **-et** _table_name_  
 Specifies the name of the result table to create. If this table already exists, **-DT** must be used or the operation will fail.  
  
 **-f** [ *file_name* ]  
 Generates a [!INCLUDE[tsql](../includes/tsql-md.md)] script to bring the table at the destination server into convergence with the table at the source server. You can optionally specify a name and path for the generated [!INCLUDE[tsql](../includes/tsql-md.md)] script file. If *file_name* is not specified, the [!INCLUDE[tsql](../includes/tsql-md.md)] script file is generated in the directory where the utility runs.  
  
 **-o** _output_file_name_  
 Is the full name and path of the output file.  
  
 **-q**  
 Perform a fast comparison by only comparing row counts and schema.  
  
 **-rc** _number_of_retries_  
 Number of times that the utility retries a failed operation.  
  
 **-ri**  _retry_interval_  
 Interval, in seconds, to wait between retries.  
  
 **-strict**  
 Source and destination schema are strictly compared.  
  
 **-t** _connection_timeouts_  
 Sets the connection timeout period, in seconds, for connections to the source server and destination server.  
  
## Return Value  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|Success|  
|**1**|Critical error|  
|**2**|Table differences|  
  
## Remarks  
 The **tablediff** utility cannot be used with non- [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] servers.  
  
 Tables with **sql_variant** data type columns are not supported.  
  
 By default, the **tablediff** utility supports the following data type mappings between source and destination columns.  
  
|Source data type|Destination data type|  
|----------------------|---------------------------|  
|**tinyint**|**smallint**, **int**, or **bigint**|  
|**smallint**|**int** or **bigint**|  
|**int**|**bigint**|  
|**timestamp**|**varbinary**|  
|**varchar(max)**|**text**|  
|**nvarchar(max)**|**ntext**|  
|**varbinary(max)**|**image**|  
|**text**|**varchar(max)**|  
|**ntext**|**nvarchar(max)**|  
|**image**|**varbinary(max)**|  
  
 Use the **-strict** option to disallow these mappings and perform a strict validation.  
  
 The source table in the comparison must contain at least one primary key, identity, or ROWGUID column. When you use the **-strict** option, the destination table must also have a primary key, identity, or ROWGUID column.  
  
 The [!INCLUDE[tsql](../includes/tsql-md.md)] script generated to bring the destination table into convergence does not include the following data types:  
  
-   **varchar(max)**  
  
-   **nvarchar(max)**  
  
-   **varbinary(max)**  
  
-   **timestamp**  
  
-   **xml**  
  
-   **text**  
  
-   **ntext**  
  
-   **image**  
  
## Permissions  
 To compare tables, you need SELECT ALL permissions on the table objects being compared.  
  
 To use the **-et** option, you must be a member of the db_owner fixed database role, or at least have CREATE TABLE permission in the subscription database and ALTER permission on the destination owner schema at the destination server.  
  
 To use the **-dt** option, you must be a member of the db_owner fixed database role, or at least have ALTER permission on the destination owner schema at the destination server.  
  
 To use the **-o** or **-f** options, you must have write permissions to the specified file directory location.  
  
## See Also  
 [Compare Replicated Tables for Differences &#40;Replication Programming&#41;](../relational-databases/replication/administration/compare-replicated-tables-for-differences-replication-programming.md)  
  
  

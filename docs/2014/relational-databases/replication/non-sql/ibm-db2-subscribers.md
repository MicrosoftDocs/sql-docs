---
title: "IBM DB2 Subscribers | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "non-SQL Server Subscribers, IBM DB2"
  - "data types [SQL Server replication], non-SQL Server Subscribers"
  - "IBM DB2 Subscribers"
  - "mapping data types [SQL Server replication]"
  - "heterogeneous Subscribers, IBM DB2"
ms.assetid: a1a27b1e-45dd-4d7d-b6c0-2b608ed175f6
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# IBM DB2 Subscribers
  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports push subscriptions to IBM DB2/AS 400, DB2/MVS, and DB2/Universal Database through the OLE DB providers that are included with [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Host Integration Server.  
  
## Configuring an IBM DB2 Subscriber  
 To configure an IBM DB2 Subscriber, follow these steps:  
  
1.  Install the latest version of the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] OLE DB Provider for DB2 on the Distributor:  
  
    -   If you are using [!INCLUDE[ssEnterpriseEd11](../../../includes/ssenterpriseed11-md.md)], on the [SQL Server 2008 Downloads](https://go.microsoft.com/fwlink/?LinkId=149256) Web page, in the **Related Downloads** section, click the link to the latest version of the Microsoft SQL Server 2008 Feature Pack. On the **Microsoft SQL Server 2008 Feature Pack** Web page, search for **Microsoft OLE DB Provider for DB2**.  
  
    -   If you are using [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] Standard, install the latest version of the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Host [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] (HIS) server, which includes the provider.  
  
     In addition to installing the provider, we recommend that you install the Data Access Tool, which is used in the next step (it is installed by default with the download for [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] Enterprise). For more information about installing and using the Data Access Tool, see the provider documentation or the HIS documentation.  
  
2.  Create a connection string for the Subscriber. The connection string can be created in any text editor, but we recommend that you use the Data Access Tool. To create the string in the Data Access Tool:  
  
    1.  Click **Start**, **Programs**, **Microsoft OLE DB Provider for DB2**, and then **Data Access Tool**.  
  
    2.  In the **Data Access Tool**, follow the steps to provide information about the DB2 server. When you complete the tool, a universal data link (UDL) is created with an associated connection string (the UDL is not actually used by replication, but the connection string is).  
  
    3.  Access the connection string: right-click the UDL in the Data Access Tool and select **Display Connection String**.  
  
     The connection string will be similar to (line breaks are for readability):  
  
    ```  
    Provider=DB2OLEDB;Initial Catalog=MY_SUBSCRIBER_DB;Network Transport Library=TCP;Host CCSID=1252;  
    PC Code Page=1252;Network Address=MY_SUBSCRIBER;Network Port=50000;Package Collection=MY_PKGCOL;  
    Default Schema=MY_SCHEMA;Process Binary as Character=False;Units of Work=RUW;DBMS Platform=DB2/NT;  
    Persist Security Info=False;Connection Pooling=True;  
    ```  
  
     Most of the options in the string are specific to the DB2 server you are configuring, but the `Process Binary as Character` option should always be set to `False`. A value is required for the `Initial Catalog` option to identify the subscription database. The connection string will be entered in the New Subscription Wizard when you create the subscription.  
  
3.  Create a snapshot or transactional publication, enable it for non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers, and then create a push subscription for the Subscriber. For more information, see [Create a Subscription for a Non-SQL Server Subscriber](../create-a-subscription-for-a-non-sql-server-subscriber.md).  
  
4.  Optionally, specify a custom creation script for one or more articles. When a table is published, a CREATE TABLE script is created for that table. For non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers, the script is created in the [!INCLUDE[tsql](../../../includes/tsql-md.md)] dialect, and it is then translated to a more generic SQL dialect by the Distribution Agent before being applied at the Subscriber. To specify a custom creation script, either modify the existing [!INCLUDE[tsql](../../../includes/tsql-md.md)] script or create a complete script that uses the DB2 SQL dialect; if a DB2 script is created, use the **bypass_translation** directive so that the Distribution Agent will apply the script at the Subscriber without translation.  
  
     Scripts can be modified for a number of reasons, but the most common reason is to alter data type mappings. For more information, see the "Data Type Mapping Considerations" section in this topic. If you modify the [!INCLUDE[tsql](../../../includes/tsql-md.md)] script, changes should be restricted to data type mapping changes (and the script should not contain any comments). If more substantial changes are required, create a DB2 script.  
  
     **To modify an article script and supply it as a custom creation script**  
  
    1.  After the snapshot has been generated for the publication, navigate to the snapshot folder for the publication.  
  
    2.  Locate the .sch file with the same name as the article, such as MyArticle.sch.  
  
    3.  Open this file using Notepad or another text editor.  
  
    4.  Modify the file and save it to a different directory.  
  
    5.  Execute sp_changearticle, specifying the file path and name for the *creation_script* property. For more information, see [sp_changearticle &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-changearticle-transact-sql).  
  
     **To create an article script and supply it as a custom creation script**  
  
    1.  Create an article script using the DB2 SQL dialect. Ensure the first line of the file is **bypass_translation**, with nothing else on the line.  
  
    2.  Execute sp_changearticle, specifying the file path and name for the *creation_script* property.  
  
## Considerations for IBM DB2 Subscribers  
 In addition to the considerations covered in the topic [Non-SQL Server Subscribers](non-sql-server-subscribers.md), consider the following issues when replicating to DB2 Subscribers:  
  
-   The data and indexes for each replicated table are assigned to a DB2 tablespace. The page size of a DB2 tablespace controls the maximum number of columns and the maximum row size of the tables belonging to the tablespace. Ensure that the tablespace associated with replicated tables is appropriate based on the number of replicated columns and the maximum row size of the tables.  
  
-   Do not publish tables to DB2 Subscribers using transactional replication if one or more primary key columns in the table is of data type DECIMAL(32-38, 0-38) or NUMERIC(32-38, 0-38). Transactional replication identifies rows using the primary key; this can result in failures because these data types are mapped to VARCHAR(41) at the Subscriber. Tables with primary keys that use these data types can be published using snapshot replication.  
  
-   If you want to pre-create tables at the Subscriber, rather than having replication create them, use the replication support only option. For more information, see [Initialize a Transactional Subscription Without a Snapshot](../initialize-a-transactional-subscription-without-a-snapshot.md).  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] allows longer table names and column names than DB2:  
  
    -   If the publication database includes tables with names longer than those supported on the DB2 version at the Subscriber, specify an alternative name for the destination_table article property. For more information about setting properties when creating a publication, see [Create a Publication](../publish/create-a-publication.md) and [Define an Article](../publish/define-an-article.md).  
  
    -   It is not possible to specify alternative column names. You must ensure that published tables do not include column names longer than those supported on the DB2 version at the Subscriber.  
  
## Mapping Data Types from SQL Server to IBM DB2  
 The following table shows the data type mappings that are used when data is replicated to a Subscriber running IBM DB2.  
  
|SQL Server data type|IBM DB2 data type|  
|--------------------------|-----------------------|  
|`bigint`|DECIMAL(19,0)|  
|`binary(1-254)`|CHAR(1-254) FOR BIT DATA|  
|`binary(255-8000)`|VARCHAR(255-8000) FOR BIT DATA|  
|`bit`|SMALLINT|  
|`char(1-254)`|CHAR(1-254)|  
|`char(255-8000)`|VARCHAR(255-8000)|  
|`date`|DATE|  
|`datetime`|TIMESTAMP|  
|`datetime2(0-7)`|VARCHAR(27)|  
|`datetimeoffset(0-7)`|VARCHAR(34)|  
|`decimal(1-31, 0-31)`|DECIMAL(1-31, 0-31)|  
|`decimal(32-38, 0-38)`|VARCHAR(41)|  
|`float(53)`|DOUBLE|  
|`float`|FLOAT|  
|`geography`|IMAGE|  
|`geometry`|IMAGE|  
|`hierarchyid`|IMAGE|  
|`image`|VARCHAR(0) FOR BIT DATA<sup>1</sup>|  
|`into`|INT|  
|`money`|DECIMAL(19,4)|  
|`nchar(1-4000)`|VARCHAR(1-4000)|  
|`ntext`|VARCHAR(0)<sup>1</sup>|  
|`numeric(1-31, 0-31)`|DECIMAL(1-31,0-31)|  
|`numeric(32-38, 0-38)`|VARCHAR(41)|  
|`nvarchar(1-4000)`|VARCHAR(1-4000)|  
|`nvarchar(max)`|VARCHAR(0)<sup>1</sup>|  
|`real`|REAL|  
|`smalldatetime`|TIMESTAMP|  
|`smallint`|SMALLINT|  
|`smallmoney`|DECIMAL(10,4)|  
|`sql_variant`|N/A|  
|`sysname`|VARCHAR(128)|  
|`text`|VARCHAR(0)<sup>1</sup>|  
|`time(0-7)`|VARCHAR(16)|  
|`timestamp`|CHAR(8) FOR BIT DATA|  
|`tinyint`|SMALLINT|  
|`uniqueidentifier`|CHAR(38)|  
|`varbinary(1-8000)`|VARCHAR(1-8000) FOR BIT DATA|  
|`varchar(1-8000)`|VARCHAR(1-8000)|  
|`varbinary(max)`|VARCHAR(0) FOR BIT DATA<sup>1</sup>|  
|`varchar(max)`|VARCHAR(0)<sup>1</sup>|  
|`xml`|VARCHAR(0)<sup>1</sup>|  
  
 <sup>1</sup> See the next section for more information about mappings to VARCHAR(0).  
  
### Data Type Mapping Considerations  
 Consider the following data type mapping issues when replicating to DB2 Subscribers:  
  
-   When mapping [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] `char`, `varchar`, `binary` and `varbinary` to DB2 CHAR, VARCHAR, CHAR FOR BIT DATA, and VARCHAR FOR BIT DATA, respectively, replication sets the length of the DB2 data type to be the same as that of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] type.  
  
     This allows the generated table to be successfully created at the Subscriber, as long as the DB2 page size constraint is large enough to accommodate the maximum size of the row. Ensure that the login used to access the DB2 database has permissions to access table spaces of a sufficient size for the tables being replicated to DB2.  
  
-   DB2 can support VARCHAR columns as large as 32 kilobytes (KB); therefore it is possible that some [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] large object columns can be appropriately mapped to DB2 VARCHAR columns. However, the OLE DB provider that replication uses for DB2 does not support mapping [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] large objects to DB2 large objects. For this reason, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] `text`, `varchar(max)`, `ntext`, and `nvarchar(max)` columns are mapped to VARCHAR(0) in the generated create scripts. The length value of 0 must be changed to an appropriate value prior to applying the script to the Subscriber. If the data type length is not changed, DB2 will raise error 604 when the table create is attempted at the DB2 Subscriber (error 604 indicates that the precision or length attribute of a data type is not valid).  
  
     Based upon your knowledge of the source table that you are replicating, determine whether it is appropriate to map a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] large object to a variable length DB2 item, and specify an appropriate maximum length in a custom creation script. For information about specifying a custom creation script, see step 5 in the section "Configuring an IBM DB2 Subscriber" in this topic.  
  
    > [!NOTE]  
    >  The specified length for the DB2 type, when combined with other column lengths, cannot exceed the maximum row size based upon the DB2 table space that the table data is assigned to.  
  
     If there is no appropriate mapping for a large object column, consider using column filtering on the article so that the column is not replicated. For more information, see [Filter Published Data](../publish/filter-published-data.md).  
  
-   When replicating [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] `nchar` and `nvarchar` to DB2 CHAR and VARCHAR, replication uses the same length-specifier for the DB2 type as for the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] type. However, the data type length might too small for the generated DB2 table.  
  
     In some DB2 environments, a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] `char` data item is not restricted to single-byte characters; the length of a CHAR or VARCHAR item must take this into account. You must also take into account *shift in* and *shift out* characters if they are needed. If you are replicating tables with `nchar` and `nvarchar` columns, you might need to specify a larger maximum length for the data type in a custom creation script. For information about specifying a custom creation script, see step 5 in the section "Configuring an IBM DB2 Subscriber" in this topic.  
  
## See Also  
 [Non-SQL Server Subscribers](non-sql-server-subscribers.md)   
 [Subscribe to Publications](../subscribe-to-publications.md)  
  
  

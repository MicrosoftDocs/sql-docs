---
title: IBM Db2 Subscribers
description: IBM Db2 Subscribers let SQL Server replicate data through push subscriptions. Learn how to configure the OLE DB provider, connection strings, and data type mappings.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "non-SQL Server Subscribers, IBM DB2"
  - "data types [SQL Server replication], non-SQL Server Subscribers"
  - "IBM DB2 Subscribers"
  - "mapping data types [SQL Server replication]"
  - "heterogeneous Subscribers, IBM DB2"
---
# IBM Db2 Subscribers
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports push subscriptions to IBM Db2/AS 400, DB2/MVS, and DB2/Universal Database through the OLE DB providers that are included with [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Host Integration Server.  
  
## Configuring an IBM Db2 Subscriber  
 To configure an IBM Db2 Subscriber, follow these steps:  
  
1.  Install the latest version of the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] OLE DB Provider for DB2 on the Distributor:  
  
    -   If you're using [!INCLUDE[ssNoVersion_md](../../../includes/ssnoversion-md.md)] Enterprise Edition, on the [SQL Server Downloads](https://go.microsoft.com/fwlink/?LinkId=149256) Web page, in the **Related Downloads** section, select the link to the latest version of the Microsoft SQL Server Feature Pack. On the **Microsoft SQL Server Feature Pack** Web page, search for **OLE DB Provider for DB2**.  
  
    -   If you're using [!INCLUDE[ssNoVersion_md](../../../includes/ssnoversion-md.md)] Standard Edition, install the latest version of the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Host [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] (HIS) server, which includes the provider.  
  
     In addition to installing the provider, install the Data Access Tool, which is used in the next step. The tool is installed by default with the download for  [!INCLUDE[ssNoVersion_md](../../../includes/ssnoversion-md.md)] Enterprise Edition. For more information about installing and using the Data Access Tool, see the provider documentation or the HIS documentation.  
  
1.  Create a connection string for the Subscriber. You can create the connection string in any text editor, but use the Data Access Tool. To create the string in the Data Access Tool:  
  
    1.  Select **Start**, **Programs**, OLE DB Provider for DB2, and then **Data Access Tool**.    
  
    1.  In the **Data Access Tool**, follow the steps to provide information about the DB2 server. When you complete the tool, it creates a universal data link (UDL) with an associated connection string. The UDL isn't used by replication, but the connection string is.  
  
    1.  Access the connection string: right-click the UDL in the Data Access Tool and select **Display Connection String**.  
  
        The connection string is similar to the following (line breaks are for readability):  
  
    ```  
    Provider=DB2OLEDB;Initial Catalog=MY_SUBSCRIBER_DB;Network Transport Library=TCP;Host CCSID=1252;  
    PC Code Page=1252;Network Address=MY_SUBSCRIBER;Network Port=50000;Package Collection=MY_PKGCOL;  
    Default Schema=MY_SCHEMA;Process Binary as Character=False;Derive Parameters=False;Units of Work=RUW;DBMS Platform=DB2/NT;  
    Persist Security Info=False;Connection Pooling=True;  
    ```  
  
     Most of the options in the string are specific to the DB2 server you're configuring, but you should always set the `Process Binary as Character` and `Derive Parameters` options to `False`. You need to provide a value for the `Initial Catalog` option to identify the subscription database. Enter the connection string in the New Subscription Wizard when you create the subscription.  
  
1.  Create a snapshot or transactional publication, enable it for non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers, and then create a push subscription for the Subscriber. For more information, see [Create a Subscription for a Non-SQL Server Subscriber](../../../relational-databases/replication/create-a-subscription-for-a-non-sql-server-subscriber.md).  
  
1.  Optionally, specify a custom creation script for one or more articles. When a table is published, a `CREATE TABLE` script is created for that table. For non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers, you create the script in the [!INCLUDE[tsql](../../../includes/tsql-md.md)] dialect, and the Distribution Agent translates it to a more generic SQL dialect before applying it at the Subscriber. To specify a custom creation script, either modify the existing [!INCLUDE[tsql](../../../includes/tsql-md.md)] script or create a complete script that uses the DB2 SQL dialect. If you create a DB2 script, use the **bypass_translation** directive so the Distribution Agent applies the script at the Subscriber without translation.  
  
     You can modify scripts for a number of reasons, but the most common reason is to alter data type mappings. For more information, see the "Data Type Mapping Considerations" section in this article. If you modify the [!INCLUDE[tsql](../../../includes/tsql-md.md)] script, restrict changes to data type mapping changes and don't include any comments. If you need more substantial changes, create a DB2 script.  
  
     **To modify an article script and supply it as a custom creation script**  
  
    1.  After the snapshot is generated for the publication, go to the snapshot folder for the publication.  
  
    2.  Locate the `.sch` file with the same name as the article, such as `MyArticle.sch`.  
  
    3.  Open this file by using Notepad or another text editor.  
  
    4.  Modify the file and save it to a different directory.  
  
    5.  Execute `sp_changearticle`, specifying the file path and name for the *creation_script* property. For more information, see [sp_changearticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-changearticle-transact-sql.md).  
  
     **To create an article script and supply it as a custom creation script**  
  
    1.  Create an article script by using the Db2 SQL dialect. Ensure the first line of the file is **bypass_translation**, with nothing else on the line.  
  
    2.  Execute sp_changearticle, specifying the file path and name for the *creation_script* property.  
  
## Considerations for IBM Db2 Subscribers  
 In addition to the considerations covered in the article [Non-SQL Server Subscribers](../../../relational-databases/replication/non-sql/non-sql-server-subscribers.md), consider the following issues when replicating to Db2 Subscribers:  
  
-   The data and indexes for each replicated table are assigned to a Db2 tablespace. The page size of a Db2 tablespace controls the maximum number of columns and the maximum row size of the tables belonging to the tablespace. Ensure that the tablespace associated with replicated tables is appropriate based on the number of replicated columns and the maximum row size of the tables.  
  
-   Don't publish tables to Db2 Subscribers by using transactional replication if one or more primary key columns in the table is of data type DECIMAL(32-38, 0-38) or NUMERIC(32-38, 0-38). Transactional replication identifies rows by using the primary key. This method can result in failures because these data types are mapped to VARCHAR(41) at the Subscriber. You can publish tables with primary keys that use these data types by using snapshot replication.  
  
-   If you want to create tables at the Subscriber, rather than having replication create them, use the replication support only option. For more information, see [Initialize a Transactional Subscription Without a Snapshot](../../../relational-databases/replication/initialize-a-transactional-subscription-without-a-snapshot.md).    
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] allows longer table names and column names than Db2:  
  
    -   If the publication database includes tables with names longer than those supported on the Db2 version at the Subscriber, specify an alternative name for the destination_table article property. For more information about setting properties when creating a publication, see [Create a Publication](../../../relational-databases/replication/publish/create-a-publication.md) and [Define an Article](../../../relational-databases/replication/publish/define-an-article.md).  
  
    -   You can't specify alternative column names. Ensure that published tables don't include column names longer than those supported on the Db2 version at the Subscriber.  
  
## Mapping data types from SQL Server to IBM Db2  
 The following table shows the data type mappings that are used when data is replicated to a Subscriber running IBM Db2.  
  
|SQL Server data type|IBM Db2 data type|  
|--------------------------|-----------------------|  
|**bigint**|DECIMAL(19,0)|  
|**binary(1-254)**|CHAR(1-254) FOR BIT DATA|  
|**binary(255-8000)**|VARCHAR(255-8000) FOR BIT DATA|  
|**bit**|SMALLINT|  
|**char(1-254)**|CHAR(1-254)|  
|**char(255-8000)**|VARCHAR(255-8000)|  
|**date**|DATE|  
|**datetime**|TIMESTAMP|  
|**datetime2(0-7)**|VARCHAR(27)|  
|**datetimeoffset(0-7)**|VARCHAR(34)|  
|**decimal(1-31, 0-31)**|DECIMAL(1-31, 0-31)|  
|**decimal(32-38, 0-38)**|VARCHAR(41)|  
|**float(53)**|DOUBLE|  
|**float**|FLOAT|  
|**geography**|IMAGE|  
|**geometry**|IMAGE|  
|**hierarchyid**|IMAGE|  
|**image**|VARCHAR(0) FOR BIT DATA*|  
|**into**|INT|  
|**money**|DECIMAL(19,4)|  
|**nchar(1-4000)**|VARCHAR(1-4000)|  
|**ntext**|VARCHAR(0)*|  
|**numeric(1-31, 0-31)**|DECIMAL(1-31,0-31)|  
|**numeric(32-38, 0-38)**|VARCHAR(41)|  
|**nvarchar(1-4000)**|VARCHAR(1-4000)|  
|**nvarchar(max)**|VARCHAR(0)*|  
|**real**|REAL|  
|**smalldatetime**|TIMESTAMP|  
|**smallint**|SMALLINT|  
|**smallmoney**|DECIMAL(10,4)|  
|**sql_variant**|N/A|  
|**sysname**|VARCHAR(128)|  
|**text**|VARCHAR(0)*|  
|**time(0-7)**|VARCHAR(16)|  
|**timestamp**|CHAR(8) FOR BIT DATA|  
|**tinyint**|SMALLINT|  
|**uniqueidentifier**|CHAR(38)|  
|**varbinary(1-8000)**|VARCHAR(1-8000) FOR BIT DATA|  
|**varchar(1-8000)**|VARCHAR(1-8000)|  
|**varbinary(max)**|VARCHAR(0) FOR BIT DATA*|  
|**varchar(max)**|VARCHAR(0)*|  
|**xml**|VARCHAR(0)*|  
  
* See the next section for more information about mappings to VARCHAR(0).  
  
### Data type mapping considerations  
 Consider the following data type mapping issues when replicating to DB2 Subscribers:  
  
-   When mapping [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] **char**, **varchar**, **binary**, and **varbinary** to Db2 **CHAR**, **VARCHAR**, **CHAR FOR BIT DATA**, and **VARCHAR FOR BIT DATA**, respectively, replication sets the length of the DB2 data type to be the same as that of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] type.  
  
     This approach allows the generated table to be successfully created at the Subscriber, as long as the DB2 page size constraint is large enough to accommodate the maximum size of the row. Ensure that the login you use to access the Db2 database has permissions to access table spaces of a sufficient size for the tables being replicated to Db2.  
  
-   DB2 supports **VARCHAR** columns as large as 32 kilobytes (KB); therefore, some [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] large object columns can be appropriately mapped to DB2 **VARCHAR** columns. However, the OLE DB provider that replication uses for DB2 doesn't support mapping [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] large objects to DB2 large objects. For this reason, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] **text**, **varchar(max)**, **ntext**, and **nvarchar(max)** columns are mapped to **VARCHAR(0)** in the generated create scripts. You must change the length value of 0 to an appropriate value before applying the script to the Subscriber. If you don't change the data type length, DB2 raises error 604 when the table create is attempted at the DB2 Subscriber (error 604 indicates that the precision or length attribute of a data type is not valid).  
  
     Based on your knowledge of the source table that you're replicating, determine whether it's appropriate to map a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] large object to a variable length DB2 item, and specify an appropriate maximum length in a custom creation script. For information about specifying a custom creation script, see step 5 in the section "Configuring an IBM Db2 Subscriber" in this article.  
  
    > [!NOTE]  
    >  The specified length for the DB2 type, when combined with other column lengths, can't exceed the maximum row size based on the DB2 table space that the table data is assigned to.  
  
     If there's no appropriate mapping for a large object column, consider using column filtering on the article so that the column isn't replicated. For more information, see [Filter Published Data](../../../relational-databases/replication/publish/filter-published-data.md).  
  
-   When replicating [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] **nchar** and **nvarchar** to DB2 **CHAR** and **VARCHAR**, replication uses the same length specifier for the DB2 type as for the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] type. However, the data type length might be too small for the generated DB2 table.  
  
     In some DB2 environments, a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] **char** data item isn't restricted to single-byte characters; the length of a **CHAR** or **VARCHAR** item must take this condition into account. You must also take into account *shift in* and *shift out* characters if they're needed. If you're replicating tables with **nchar** and **nvarchar** columns, you might need to specify a larger maximum length for the data type in a custom creation script. For information about specifying a custom creation script, see step 5 in the section "Configuring an IBM Db2 Subscriber" in this article.  
  
## Related content

- [Non-SQL Server Subscribers](non-sql-server-subscribers.md)
- [Subscribe to Publications](../subscribe-to-publications.md)

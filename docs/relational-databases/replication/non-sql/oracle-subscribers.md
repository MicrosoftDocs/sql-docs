---
description: "Oracle Subscribers"
title: "Oracle Subscribers | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "data types [SQL Server replication], non-SQL Server Subscribers"
  - "Oracle Subscribers [SQL Server replication]"
  - "non-SQL Server Subscribers, Oracle"
  - "heterogeneous Subscribers, Oracle"
  - "mapping data types [SQL Server replication]"
ms.assetid: 591c0313-82ce-4689-9fc1-73752ff122cf
author: "MashaMSFT"
ms.author: "mathoma"
---
# Oracle Subscribers
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Beginning with [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports push subscriptions to Oracle through the Oracle OLE DB provider supplied by Oracle.  
  
## Configuring an Oracle Subscriber  
 To configure an Oracle Subscriber, follow these steps:  
  
1.  Install and configure Oracle client networking software and the Oracle OLE DB provider on the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor, so that the Distributor can make connections to the Oracle Subscriber. The Oracle client networking software should be the most recent version available. Oracle recommends that users install the most recent versions of client software. The client software is therefore often a more recent version than the database software. The most straightforward way to install the software is to use the Oracle Universal Installer on the Oracle Client disk. In the Oracle Universal Installer, you will supply the following information:  
  
    |Information|Description|  
    |-----------------|-----------------|  
    |Oracle Home|This is the path to the install directory for the Oracle software. Accept the default (C:\oracle\ora90 or similar) or enter another path. For more information about the Oracle Home, see the section "Considerations for Oracle Home" later in this topic.|  
    |Oracle home name|An alias for the Oracle home path.|  
    |Installation type|In Oracle 10g, select the **Runtime** or **Administrator** installation option.|  
  
2.  Create a TNS name for the Subscriber. TNS (Transparent Network Substrate) is a communication layer used by Oracle databases. The TNS Service Name is the name by which an Oracle database instance is known on a network. You assign a TNS Service Name when you configure connectivity to the Oracle database. Replication uses the TNS Service name to identify the Subscriber and to establish connections.  
  
     After the Oracle Universal Installer is complete, use the Net Configuration Assistant to configure network connectivity. You must supply four pieces of information to configure network connectivity. The Oracle database administrator configures the network configuration when setting up the database and listener and should be able to provide this information if you do not have it. You must do the following:  
  
    |Action|Description|  
    |------------|-----------------|  
    |Identify the database|There are two methods for identifying the database. The first method uses the Oracle System Identifier (SID) and is available in every Oracle release. The second method uses the Service Name, which is available starting with Oracle release 8.0. Both methods use a value that is configured when the database is created and it is important that the client networking configuration use the same naming method that the administrator used when configuring the listener for the database.|  
    |Identify a network alias for the database|You must specify a network alias, which is used to access the Oracle database. The network alias is essentially a pointer to the remote SID or Service Name that was configured when the database was created; it has been referred to by several names in different Oracle releases and products, including Net Service Name and TNS Alias. SQL*Plus prompts for this alias as the "Host String" parameter when you log in.|  
    |Select the network protocol|Select the appropriate protocols you would like to support. Most applications use TCP.|  
    |Specify the host information to identify the database listener|The host is the name or DNS alias of the computer on which the Oracle listener is running, which is typically the same computer on which the database resides. For some protocols, you must provide additional information. For example, if you select TCP, you must supply the port on which the listener is listening for connection requests to the target database. The default TCP configuration uses port 1521.|  
  
3.  Create a snapshot or transactional publication, enable it for non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers, and then create a push subscription for the Subscriber. For more information, see [Create a Subscription for a Non-SQL Server Subscriber](../../../relational-databases/replication/create-a-subscription-for-a-non-sql-server-subscriber.md).  

### Setting directory permissions  
 The account under which the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service on the Distributor runs must be granted read and execute permissions for the directory (and all subdirectories) where the Oracle client networking software is installed.  
  
### Testing connectivity between the SQL Server Distributor and the Oracle Publisher  
 Near the end of the Net Configuration Assistant there might be an option to test the connection to the Oracle Subscriber. Before you test the connection, ensure that the Oracle database instance is online and that the Oracle Listener is running. If the test is unsuccessful, contact the Oracle DBA responsible for the database to which you are trying to connect.  
  
 After you have made a successful connection to the Oracle Subscriber, attempt to log in to the database using the same account and password as you configured for the Distribution Agent for the subscription:  
  
1.  Click **Start**, and then click **Run**.  
  
2.  Type `cmd` and click **OK**.  
  
3.  At the command prompt, type:  
  
     `sqlplus <UserSchemaLogin>/<UserSchemaPassword>@<NetServiceName>`  
  
     For example: `sqlplus replication/$tr0ngPasswerd@Oracle90Server`  
  
4.  If the networking configuration was successful, the login will succeed and you will see a `SQL` prompt.  
  
### Considerations for Oracle Home  
 Oracle supports side-by-side installation of application binaries, but only one set of binaries can be used by replication at a given time. Each set of binaries is associated with an Oracle Home; the binaries are in the directory %ORACLE_HOME%\bin. You must ensure that the correct set of binaries (specifically the latest version of the client networking software) is used when replication makes connections to the Oracle Subscriber.  
  
 Log into the Distributor with the accounts used by the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service and the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent service and set the appropriate environment variables. The %ORACLE_HOME% variable should be set to refer to the installation point you specified when you installed the client networking software. The %PATH% must include the %ORACLE_HOME% \bin directory as the first Oracle entry that is encountered. For information about setting environment variables, see the Windows documentation.  
  
> [!NOTE]  
>  If you have more than one Oracle home on the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor, ensure that the Distribution Agent is using the most recent Oracle OLE DB provider. In some cases, Oracle does not update the OLE DB provider by default when you update the client components on the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor. Uninstall the old OLE DB provider and install the latest OLE DB provider. For more information about installing and uninstalling the provider, see the Oracle documentation.  
  
## Considerations for Oracle Subscribers  
 In addition to the considerations covered in the topic [Non-SQL Server Subscribers](../../../relational-databases/replication/non-sql/non-sql-server-subscribers.md), consider the following issues when replicating to Oracle Subscribers:  
  
-   Oracle treats both empty strings and NULL values as NULL. This is important if you define a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] column as NOT NULL, and are replicating the column to an Oracle Subscriber. To avoid failures when applying changes to the Oracle Subscriber, you must do one of the following:  
  
    -   Ensure that empty strings are not inserted into the published table as column values.  
  
    -   Use the **-SkipErrors** parameter for the Distribution Agent if it is acceptable to be notified of failures in the Distribution Agent history log and to continue processing. Specify the Oracle error code 1400 (**-SkipErrors1400**).  
  
    -   Modify the generated create table script, removing the NOT NULL attribute from any character columns that may have associated empty strings, and supply the modified script as a custom create script for the article using the @creation_script parameter of [sp_addarticle](../../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md).  
  
-   Oracle Subscribers support a schema option of 0x4071. For more information about schema options, see [sp_addarticle &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addarticle-transact-sql.md).  
  
## Mapping Data Types from SQL Server to Oracle  
 The following table shows the data type mappings that are used when data is replicated to a Subscriber running Oracle.  
  
|SQL Server data type|Oracle Data type|  
|--------------------------|----------------------|  
|**bigint**|NUMBER(19,0)|  
|**binary(1-2000)**|RAW(1-2000)|  
|**binary(2001-8000)**|BLOB|  
|**bit**|NUMBER(1)|  
|**char(1-2000)**|CHAR(1-2000)|  
|**char(2001-4000)**|VARCHAR2(2001-4000)|  
|**char(4001-8000)**|CLOB|  
|**date**|DATE|  
|**datetime**|DATE|  
|**datetime2(0-7)**|TIMESTAMP(7) for Oracle 9 and Oracle 10; VARCHAR(27) for Oracle 8|  
|**datetimeoffset(0-7)**|TIMESTAMP(7) WITH TIME ZONE for Oracle 9 and Oracle 10; VARCHAR(34) for Oracle 8|  
|**decimal(1-38, 0-38)**|NUMBER(1-38, 0-38)|  
|**float(53)**|FLOAT|  
|**float**|FLOAT|  
|**geography**|BLOB|  
|**geometry**|BLOB|  
|**hierarchyid**|BLOB|  
|**image**|BLOB|  
|**int**|NUMBER(10,0)|  
|**money**|NUMBER(19,4)|  
|**nchar(1-1000)**|CHAR(1-1000)|  
|**nchar(1001-4000)**|NCLOB|  
|**ntext**|NCLOB|  
|**numeric(1-38, 0-38)**|NUMBER(1-38, 0-38)|  
|**nvarchar(1-1000)**|VARCHAR2(1-2000)|  
|**nvarchar(1001-4000)**|NCLOB|  
|**nvarchar(max)**|NCLOB|  
|**real**|REAL|  
|**smalldatetime**|DATE|  
|**smallint**|NUMBER(5,0)|  
|**smallmoney**|NUMBER(10,4)|  
|**sql_variant**|N/A|  
|**sysname**|VARCHAR2(128)|  
|**text**|CLOB|  
|**time(0-7)**|VARCHAR(16)|  
|**timestamp**|RAW(8)|  
|**tinyint**|NUMBER(3,0)|  
|**uniqueidentifier**|CHAR(38)|  
|**varbinary(1-2000)**|RAW(1-2000)|  
|**varbinary(2001-8000)**|BLOB|  
|**varchar(1-4000)**|VARCHAR2(1-4000)|  
|**varchar(4001-8000)**|CLOB|  
|**varbinary(max)**|BLOB|  
|**varchar(max)**|CLOB|  
|**xml**|NCLOB|  
  
## See Also  
 [Non-SQL Server Subscribers](../../../relational-databases/replication/non-sql/non-sql-server-subscribers.md)   
 [Subscribe to Publications](../../../relational-databases/replication/subscribe-to-publications.md)  
  
  

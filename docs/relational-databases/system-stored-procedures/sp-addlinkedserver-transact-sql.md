---
title: "sp_addlinkedserver (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/12/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_addlinkedserver_TSQL"
  - "sp_addlinkedserver"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_addlinkedserver"
ms.assetid: fed3adb0-4c15-4a1a-8acd-1b184aff558f
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# sp_addlinkedserver (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates a linked server. A linked server allows for access to distributed, heterogeneous queries against OLE DB data sources. After a linked server is created by using **sp_addlinkedserver**, distributed queries can be run against this server. If the linked server is defined as an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], remote stored procedures can be executed.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addlinkedserver [ @server= ] 'server' [ , [ @srvproduct= ] 'product_name' ]   
     [ , [ @provider= ] 'provider_name' ]  
     [ , [ @datasrc= ] 'data_source' ]   
     [ , [ @location= ] 'location' ]   
     [ , [ @provstr= ] 'provider_string' ]   
     [ , [ @catalog= ] 'catalog' ]   
```  
  
## Arguments  
`[ @server = ] 'server'`
 Is the name of the linked server to create. *server* is **sysname**, with no default.  
  
`[ @srvproduct = ] 'product_name'`
 Is the product name of the OLE DB data source to add as a linked server. *product_name* is **nvarchar(**128**)**, with a default of NULL. If **SQL Server**, *provider_name*, *data_source*, *location*, *provider_string*, and *catalog* do not have to be specified.  
  
`[ @provider = ] 'provider_name'`
 Is the unique programmatic identifier (PROGID) of the OLE DB provider that corresponds to this data source. *provider_name* must be unique for the specified OLE DB provider installed on the current computer. *provider_name* is **nvarchar(**128**)**, with a default of NULL; however, if *provider_name* is omitted, SQLNCLI is used. (Use SQLNCLI and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will redirect to the latest version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider.) The OLE DB provider is expected to be registered with the specified PROGID in the registry.  
  
`[ @datasrc = ] 'data_source'`
 Is the name of the data source as interpreted by the OLE DB provider. *data_source* is **nvarchar(**4000**)**. *data_source* is passed as the DBPROP_INIT_DATASOURCE property to initialize the OLE DB provider.  
  
`[ @location = ] 'location'`
 Is the location of the database as interpreted by the OLE DB provider. *location* is **nvarchar(**4000**)**, with a default of NULL. *location* is passed as the DBPROP_INIT_LOCATION property to initialize the OLE DB provider.  
  
`[ @provstr = ] 'provider_string'`
 Is the OLE DB provider-specific connection string that identifies a unique data source. *provider_string* is **nvarchar(**4000**)**, with a default of NULL. *provstr* is either passed to IDataInitialize or set as the DBPROP_INIT_PROVIDERSTRING property to initialize the OLE DB provider.  
  
 When the linked server is created against the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider, the instance can be specified by using the SERVER keyword as SERVER=*servername*\\*instancename* to specify a specific instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. *servername* is the name of the computer on which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running, and *instancename* is the name of the specific instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to which the user will be connected.  
  
> [!NOTE]
>  To access a mirrored database, a connection string must contain the database name. This name is necessary to enable failover attempts by the data access provider. The database can be specified in the **@provstr** or **@catalog** parameter. Optionally, the connection string can also supply a failover partner name.  
  
`[ @catalog = ] 'catalog'`
 Is the catalog to be used when a connection is made to the OLE DB provider. *catalog* is **sysname**, with a default of NULL. *catalog* is passed as the DBPROP_INIT_CATALOG property to initialize the OLE DB provider. When the linked server is defined against an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], catalog refers to the default database to which the linked server is mapped.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None.  
  
## Remarks  
 The following table shows the ways that a linked server can be set up for data sources that can be accessed through OLE DB. A linked server can be set up more than one way for a particular data source; there can be more than one row for a data source type. This table also shows the **sp_addlinkedserver** parameter values to be used for setting up the linked server.  
  
|Remote OLE DB data source|OLE DB provider|product_name|provider_name|data_source|location|provider_string|catalog|  
|-------------------------------|---------------------|-------------------|--------------------|------------------|--------------|----------------------|-------------|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] <sup>1</sup> (default)||||||  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider||**SQLNCLI**|Network name of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (for default instance)|||Database name (optional)|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB Provider||**SQLNCLI**|*servername*\\*instancename* (for specific instance)|||Database name (optional)|  
|Oracle, version 8 and later|Oracle Provider for OLE DB|Any|**OraOLEDB.Oracle**|Alias for the Oracle database||||  
|Access/Jet|Microsoft OLE DB Provider for Jet|Any|**Microsoft.Jet.OLEDB.4.0**|Full path of Jet database file||||  
|ODBC data source|Microsoft OLE DB Provider for ODBC|Any|**MSDASQL**|System DSN of ODBC data source||||  
|ODBC data source|[!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB Provider for ODBC|Any|**MSDASQL**|||ODBC connection string||  
|File system|[!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB Provider for Indexing Service|Any|**MSIDXS**|Indexing Service catalog name||||  
|[!INCLUDE[msCoName](../../includes/msconame-md.md)] Excel Spreadsheet|[!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB Provider for Jet|Any|**Microsoft.Jet.OLEDB.4.0**|Full path of Excel file||Excel 5.0||  
|IBM DB2 Database|[!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB Provider for DB2|Any|**DB2OLEDB**|||See [!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB Provider for DB2 documentation.|Catalog name of DB2 database|  
  
 <sup>1</sup> This way of setting up a linked server forces the name of the linked server to be the same as the network name of the remote instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Use *data_source* to specify the server.  
  
 <sup>2</sup> "Any" indicates that the product name can be anything.  
  
 The [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider is the provider that is used with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] if no provider name is specified or if [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is specified as the product name. Even if you specify the older provider name, SQLOLEDB, it will be changed to SQLNCLI when persisted to the catalog.  
  
 The *data_source*, *location*, *provider_string*, and *catalog* parameters identify the database or databases the linked server points to. If any one of these parameters is NULL, the corresponding OLE DB initialization property is not set.  
  
 In a clustered environment, when you specify file names to point to OLE DB data sources, use the universal naming convention name (UNC) or a shared drive to specify the location.  
  
 **sp_addlinkedserver** cannot be executed within a user-defined transaction.  
  
> [!IMPORTANT]
>  When a linked server is created by using **sp_addlinkedserver**, a default self-mapping is added for all local logins. For non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] providers, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authenticated logins may be able to gain access to the provider under the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account. Administrators should consider using `sp_droplinkedsrvlogin <linkedserver_name>, NULL` to remove the global mapping.  
  
## Permissions  
 The `sp_addlinkedserver` statement requires the `ALTER ANY LINKED SERVER` permission. (The SSMS **New Linked Server** dialog box is implemented in a way that requires membership in the `sysadmin` fixed server role.)  
  
## Examples  
  
### A. Using the Microsoft SQL Server Native Client OLE DB Provider  
 The following example creates a linked server named `SEATTLESales`. The product name is `SQL Server`, and no provider name is used.  
  
```  
USE master;  
GO  
EXEC sp_addlinkedserver   
   N'SEATTLESales',  
   N'SQL Server';  
GO  
```  
  
 The following example creates a linked server `S1_instance1` on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client OLE DB provider.  
  
```  
EXEC sp_addlinkedserver     
   @server=N'S1_instance1',   
   @srvproduct=N'',  
   @provider=N'SQLNCLI',   
   @datasrc=N'S1\instance1';  
```  
  
### B. Using the Microsoft OLE DB Provider for Microsoft Access  
 The Microsoft.Jet.OLEDB.4.0 provider connects to Microsoft Access databases that use the 2002-2003 format. The following example creates a linked server named `SEATTLE Mktg`.  
  
> [!NOTE]  
>  This example assumes that both [!INCLUDE[msCoName](../../includes/msconame-md.md)] Access and the sample **Northwind** database are installed and that the **Northwind** database resides in C:\Msoffice\Access\Samples.  
  
```  
EXEC sp_addlinkedserver   
   @server = N'SEATTLE Mktg',   
   @provider = N'Microsoft.Jet.OLEDB.4.0',   
   @srvproduct = N'OLE DB Provider for Jet',  
   @datasrc = N'C:\MSOffice\Access\Samples\Northwind.mdb';  
GO  
```  
  
 The Microsoft.ACE.OLEDB.12.0 provider connects to Microsoft Access databases that use the 2007 format. The following example creates a linked server named `SEATTLE Mktg`.  
  
> [!NOTE]  
>  This example assumes that both [!INCLUDE[msCoName](../../includes/msconame-md.md)] Access and the sample **Northwind** database are installed and that the **Northwind** database resides in C:\Msoffice\Access\Samples.  
  
```  
EXEC sp_addlinkedserver   
   @server = N'SEATTLE Mktg',   
   @provider = N'Microsoft.ACE.OLEDB.12.0',   
   @srvproduct = N'OLE DB Provider for ACE',  
   @datasrc = N'C:\MSOffice\Access\Samples\Northwind.accdb';  
GO  
```  
  
### C. Using the Microsoft OLE DB Provider for ODBC with the data_source parameter  
 The following example creates a linked server named `SEATTLE Payroll` that uses the [!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB Provider for ODBC (`MSDASQL`) and the *data_source* parameter.  
  
> [!NOTE]  
>  The specified ODBC data source name must be defined as System DSN in the server before you use the linked server.  
  
```  
EXEC sp_addlinkedserver   
   @server = N'SEATTLE Payroll',   
   @srvproduct = N'',  
   @provider = N'MSDASQL',   
   @datasrc = N'LocalServer';  
GO  
```  
  
### D. Using the Microsoft OLE DB Provider for Excel spreadsheet  
 To create a linked server definition using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB Provider for Jet to access an Excel spreadsheet in the 1997 - 2003 format, first create a named range in Excel by specifying the columns and rows of the Excel worksheet to select. The name of the range can then be referenced as a table name in a distributed query.  
  
```  
EXEC sp_addlinkedserver 'ExcelSource',  
   'Jet 4.0',  
   'Microsoft.Jet.OLEDB.4.0',  
   'c:\MyData\DistExcl.xls',  
   NULL,  
   'Excel 5.0';  
GO  
```  
  
 To access data from an Excel spreadsheet, associate a range of cells with a name. The following query can be used to access the specified named range `SalesData` as a table by using the linked server set up previously.  
  
```  
SELECT *  
   FROM ExcelSource...SalesData;  
GO  
```  
  
 If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running under a domain account that has access to a remote share, a UNC path can be used instead of a mapped drive.  
  
```  
EXEC sp_addlinkedserver 'ExcelShare',  
   'Jet 4.0',  
   'Microsoft.Jet.OLEDB.4.0',  
   '\\MyServer\MyShare\Spreadsheets\DistExcl.xls',  
   NULL,  
   'Excel 5.0';  
```  
  
 To connect to an Excel spreadsheet in the Excel 2007 format use the ACE provider.  
  
```  
EXEC sp_addlinkedserver @server = N'ExcelDataSource',   
@srvproduct=N'ExcelData', @provider=N'Microsoft.ACE.OLEDB.12.0',   
@datasrc=N'C:\DataFolder\People.xlsx',  
@provstr=N'EXCEL 12.0' ;  
  
```  
  
### E. Using the Microsoft OLE DB Provider for Jet to access a text file  
 The following example creates a linked server for directly accessing text files, without linking the files as tables in an Access .mdb file. The provider is `Microsoft.Jet.OLEDB.4.0` and the provider string is `Text`.  
  
 The data source is the full path of the directory that contains the text files. A schema.ini file, which describes the structure of the text files, must exist in the same directory as the text files. For more information about how to create a Schema.ini file, see the Jet Database Engine documentation.  
  
```  
--Create a linked server.  
EXEC sp_addlinkedserver txtsrv, N'Jet 4.0',   
   N'Microsoft.Jet.OLEDB.4.0',  
   N'c:\data\distqry',  
   NULL,  
   N'Text';  
GO  
  
--Set up login mappings.  
EXEC sp_addlinkedsrvlogin txtsrv, FALSE, Admin, NULL;  
GO  
  
--List the tables in the linked server.  
EXEC sp_tables_ex txtsrv;  
GO  
  
--Query one of the tables: file1#txt  
--using a four-part name.   
SELECT *   
FROM txtsrv...[file1#txt];  
```  
  
### F. Using the Microsoft OLE DB Provider for DB2  
 The following example creates a linked server named `DB2` that uses the `Microsoft OLE DB Provider for DB2`.  
  
```  
EXEC sp_addlinkedserver  
   @server=N'DB2',  
   @srvproduct=N'Microsoft OLE DB Provider for DB2',  
   @catalog=N'DB2',  
   @provider=N'DB2OLEDB',  
   @provstr=N'Initial Catalog=PUBS;  
       Data Source=DB2;  
       HostCCSID=1252;  
       Network Address=XYZ;  
       Network Port=50000;  
       Package Collection=admin;  
       Default Schema=admin;';  
```  
  
### G. Add a [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] as a Linked Server For Use With Distributed Queries on Cloud and On-Premise Databases  
 You can add a [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] as a linked server and then use it with distributed queries that span the on-premises and cloud databases. This is a component for database hybrid solutions spanning on-premises corporate networks and the Windows Azure cloud.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] box product contains the distributed query feature, which allows you to write queries to combine data from local data sources and data from remote sources (including data from non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data sources) defined as linked servers. Every [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] (except the virtual master) can be added as an individual linked server and then used directly in your database applications as any other database.  
  
 The benefits of using [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] include manageability, high availability, scalability, working with a familiar development model, and a relational data model. The requirements of your database application determine how it would use [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] in the cloud. You can move all of your data at once to [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], or progressively move some of your data while keeping the remaining data on-premises. For such a hybrid database application, [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] can now be added as linked servers and the database application can issue distributed queries to combine data from [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and on-premise data sources.  
  
 Here's a simple example explaining how to connect to a [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] using distributed queries:  
  
```  
------ Configure the linked server  
-- Add one Windows Azure SQL DB as Linked Server  
EXEC sp_addlinkedserver  
@server='myLinkedServer', -- here you can specify the name of the linked server  
@srvproduct='',       
@provider='sqlncli', -- using SQL Server Native Client  
@datasrc='myServer.database.windows.net',   -- add here your server name  
@location='',  
@provstr='',  
@catalog='myDatabase'  -- add here your database name as initial catalog (you cannot connect to the master database)  
-- Add credentials and options to this linked server  
EXEC sp_addlinkedsrvlogin  
@rmtsrvname = 'myLinkedServer',  
@useself = 'false',  
@rmtuser = 'myLogin',             -- add here your login on Azure DB  
@rmtpassword = 'myPassword' -- add here your password on Azure DB  
EXEC sp_serveroption 'myLinkedServer', 'rpc out', true;  
------ Now you can use the linked server to execute 4-part queries  
-- You can create a new table in the Azure DB  
exec ('CREATE TABLE t1tutut2(col1 int not null CONSTRAINT PK_col1 PRIMARY KEY CLUSTERED (col1) )') at myLinkedServer  
-- Insert data from your local SQL Server  
exec ('INSERT INTO t1tutut2 VALUES(1),(2),(3)') at myLinkedServer  
  
-- Query the data using 4-part names  
select * from myLinkedServer.myDatabase.dbo.myTable  
```  
  
## See Also  
 [Distributed Queries Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/distributed-queries-stored-procedures-transact-sql.md)   
 [sp_addlinkedsrvlogin &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlinkedsrvlogin-transact-sql.md)   
 [sp_addserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addserver-transact-sql.md)   
 [sp_dropserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropserver-transact-sql.md)   
 [sp_serveroption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-serveroption-transact-sql.md)   
 [sp_setnetname &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-setnetname-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md)  
  
  

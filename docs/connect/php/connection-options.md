---
title: "Connection Options | Microsoft Docs"
ms.custom: ""
ms.date: "07/10/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 6d1ea295-8e34-438e-8468-4bbc0f76192c
caps.latest.revision: 37
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Connection Options
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This topic lists the options that are permitted in the associative array (when using [sqlsrv_connect](../../connect/php/sqlsrv-connect.md) in the SQLSRV driver) or the keywords that are permitted in the data source name (dsn) (when using [PDO::__construct](../../connect/php/pdo-construct.md) in the PDO_SQLSRV driver).  
  
|Key|Value|Description|Default|  
|-------|---------|---------------|-----------|  
|APP|String|Specifies the application name used in tracing.|No value set.|  
|ApplicationIntent|String|Declares the application workload type when connecting to a server. Possible values are ReadOnly and ReadWrite.<br /><br />For more information about [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] support for [!INCLUDE[ssHADR](../../includes/sshadr_md.md)], see [PHP Driver for SQL Server Support for High Availability, Disaster Recovery](../../connect/php/php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md).|ReadWrite|  
|AttachDBFileName|String|Specifies which database file the server should attach.|No value set.|  
|CharacterSet<br /><br />(not supported in the PDO_SQLSRV driver)|String|Specifies the character set used to send data to the server.<br /><br />Possible values are SQLSRV_ENC_CHAR and UTF-8. For more information, see [How to: Send and Retrieve UTF-8 Data Using Built-In UTF-8 Support](../../connect/php/how-to-send-and-retrieve-utf-8-data-using-built-in-utf-8-support.md).|SQLSRV_ENC_CHAR|  
|ConnectionPooling|1 or **true** for connection pooling on.<br /><br />0 or **false** for connection pooling off.|Specifies whether the connection is assigned from a connection pool (1 or **true**) or not (0 or **false**).|**true** (1)|  
|Database|String|Specifies the name of the database in use for the connection being established<sup>1</sup>.|The default database for the login being used.|  
|Encrypt|1 or **true** for encryption on.<br /><br />0 or **false** for encryption off.|Specifies whether the communication with SQL Server is encrypted (1 or **true**) or unencrypted (0 or **false**)<sup>2</sup>.|**false** (0)|  
|Failover_Partner|String|Specifies the server and instance of the database's mirror (if enabled and configured) to use when the primary server is unavailable.<br /><br />There are restrictions to using Failover_Partner with MultiSubnetFailover. For more information, see [PHP Driver for SQL Server Support for High Availability, Disaster Recovery](../../connect/php/php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md).|No value set.|  
|LoginTimeout|Integer (SQLSRV driver<br /><br />String (PDO_SQLSRV driver|Specifies the number of seconds to wait before failing the connection attempt.|No timeout.|  
|MultipleActiveResultSets|1 or **true** to use multiple active result sets.<br /><br />0 or **false** to disable multiple active result sets.|Disables or explicitly enables support for multiple active Result sets (MARS).<br /><br />For more information, see [How to: Disable Multiple Active Resultsets &#40;MARS&#41;](../../connect/php/how-to-disable-multiple-active-resultsets-mars.md).|true (1)|  
|MultiSubnetFailover|String|Always specify **multiSubnetFailover=yes** when connecting to the availability group listener of a [!INCLUDE[ssSQL11](../../includes/sssql11_md.md)] availability group or a [!INCLUDE[ssSQL11](../../includes/sssql11_md.md)] Failover Cluster Instance. **multiSubnetFailover=yes** configures [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] to provide faster detection of and connection to the (currently) active server. Possible values are Yes and No.<br /><br />For more information about [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] support for [!INCLUDE[ssHADR](../../includes/sshadr_md.md)], see [PHP Driver for SQL Server Support for High Availability, Disaster Recovery](../../connect/php/php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md).|No|  
|PWD<br /><br />(not supported in the PDO_SQLSRV driver)|String|Specifies the password associated with the User ID to be used when connecting with SQL Server Authentication<sup>3</sup>.|No value set.|  
|QuotedId|1 or **true** to use SQL-92 rules.<br /><br />0 or **false** to use legacy rules.|Specifies whether to use SQL-92 rules for quoted identifiers (1 or **true**) or to use legacy Transact-SQL rules (0 or **false**).|**true** (1)|  
|ReturnDatesAsStrings<br /><br />(not supported in the PDO_SQLSRV driver)|1 or **true** to return date and time types as strings.<br /><br />0 or **false** to return date and time types as PHP **DateTime** types.|Retrieves date and time types (datetime, date, time, datetime2, and datetimeoffset) as strings or as PHP types. When using the PDO_SQLSRV driver, dates are returned as strings. The PDO_SQLSRV driver has no **datetime** type.<br /><br />For more information, see [How to: Retrieve Date and Time Type as Strings Using the SQLSRV Driver](../../connect/php/how-to-retrieve-date-and-time-type-as-strings-using-the-sqlsrv-driver.md).|**false**|  
|Scrollable|String|"buffered" indicates that you want a client-side (buffered) cursor, which allows you to cache an entire result set in memory. See [Cursor Types &#40;SQLSRV Driver&#41;](../../connect/php/cursor-types-sqlsrv-driver.md) for more information.|Forward-only cursor|  
|Server<br /><br />(not supported in the SQLSRV driver)|String|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] instance to connect to.<br /><br />You can also specify a virtual network name, to connect to an AlwaysOn availability group. For more information about [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] support for [!INCLUDE[ssHADR](../../includes/sshadr_md.md)], see [PHP Driver for SQL Server Support for High Availability, Disaster Recovery](../../connect/php/php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md).|Server is a required keyword (although it does not have to be the first keyword in the connection string). If a server name is not passed to the keyword, an attempt will be made to connect to the local instance.<br /><br />The value passed to Server can be the name of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] instance, or the IP address of the instance. You can optionally specify a port number (for example, `sqlsrv:server=(local),1033`).<br /><br />Beginning in version 3.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] you can also specify a LocalDB instance with `server=(localdb)\instancename`. For more information, see [PHP Driver for SQL Server Support for LocalDB](../../connect/php/php-driver-for-sql-server-support-for-localdb.md).|  
|TraceFile|String|Specifies the path for the file used for trace data.|No value set.|  
|TraceOn|1 or **true** to enable tracing.<br /><br />0 or **false** to disable tracing.|Specifies whether ODBC tracing is enabled (1 or **true**) or disabled (0 or **false**) for the connection being established.|**false** (0)|  
|TransactionIsolation|The SQLSRV driver uses the following values:<br /><br />SQLSRV_TXN_READ_UNCOMMITTED<br /><br />SQLSRV_TXN_READ_COMMITTED<br /><br />SQLSRV_TXN_REPEATABLE_READ<br /><br />SQLSRV_TXN_SNAPSHOT<br /><br />SQLSRV_TXN_SERIALIZABLE<br /><br />The PDO_SQLSRV driver uses the following values:<br /><br />PDO::SQLSRV_TXN_READ_UNCOMMITTED<br /><br />PDO::SQLSRV_TXN_READ_COMMITTED<br /><br />PDO::SQLSRV_TXN_REPEATABLE_READ<br /><br />PDO::SQLSRV_TXN_SNAPSHOT<br /><br />PDO::SQLSRV_TXN_SERIALIZABLE|Specifies the transaction isolation level.<br /><br />For more information about transaction isolation, see [SET TRANSACTION ISOLATION LEVEL](http://go.microsoft.com/fwlink/?LinkID=191497) in the SQL Server documentation.|SQLSRV_TXN_READ_COMMITTED<br /><br />or<br /><br />PDO::SQLSRV_TXN_READ_COMMITTED|  
|TransparentNetworkIPResolution|**Enabled** or **Disabled**|Affects the connection sequence when the first resolved IP of the hostname does not respond and there are multiple IPs associated with the hostname.<br /><br />It interacts with MultiSubnetFailover to provide different connection sequences. For more information, see [Using Transparent Network IP Resolution](https://docs.microsoft.com/en-us/sql/connect/odbc/using-transparent-network-ip-resolution).|Enabled|
|TrustServerCertificate|1 or **true** to trust certificate.<br /><br />0 or **false** to not trust certificate.|Specifies whether the client should trust (1 or **true**) or reject (0 or **false**) a self-signed server certificate.|**false** (0)|  
|UID<br /><br />(not supported in the PDO_SQLSRV driver)|String|Specifies the User ID to be used when connecting with SQL Server Authentication<sup>3</sup>.|No value set.|  
|WSID|String|Specifies the name of the computer for tracing.|No value set.|  
  
1. All queries executed on the established connection will be made to the database that is specified by the *Database* attribute. However, data in other databases can be accessed by using a fully-qualified name if the user has the appropriate permissions. For example, if the *master* database is set with the *Database* connection attribute, it is still possible to execute a Transact-SQL query that accesses the *AdventureWorks.HumanResources.Employee* table by using the fully-qualified name.  
  
2. Enabling *Encryption* can impact the performance of some applications due to the computational overhead required to encrypt data.  
  
3. The *UID* and *PWD* attributes must both be set when connecting with [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Authentication.  
  
Many of the supported keys are ODBC connection string attributes. For information about ODBC connection strings, see [Using Connection String Keywords with SQL Native Client](http://go.microsoft.com/fwlink/?LinkId=105504).  
  
## See Also  
[Connecting to the Server](../../connect/php/connecting-to-the-server.md)  
  

---
title: "Tracing Driver Operation | Microsoft Docs"
ms.custom: ""
ms.date: "07/11/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 723aeae7-6504-4585-ba8b-3525115bea8b
author: MightyPen
ms.author: genemi
manager: craigg
---
# Tracing Driver Operation
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] supports the use of tracing (or logging) to help resolve issues and problems with the JDBC driver when it's used in your application. To enable the use of tracing, the JDBC driver uses the logging APIs in java.util.logging, which provides a set of classes for creating Logger and LogRecord objects.  
  
> [!NOTE]  
>  For the native component (sqljdbc_xa.dll) that is included with the JDBC driver, tracing is enabled by the Built-In Diagnostics (BID) framework. For information about BID, see [Data Access Tracing in SQL Server](https://go.microsoft.com/fwlink/?LinkId=70042).  
  
 When you develop your application, you can make calls to Logger objects, which in turn create LogRecord objects, which are then passed to Handler objects for processing. Logger and Handler objects both use logging levels, and optionally logging filters, to regulate which LogRecords are processed. When the logging operations are complete, the Handler objects can optionally use Formatter objects to publish the log information.  
  
 By default, the java.util.logging framework writes its output to a file. This output log file must have write permissions for the context under which the JDBC driver is running.  
  
> [!NOTE]  
>  For more information about using the various logging objects for program tracing, see the Java Logging APIs documentation on the Sun Microsystems Web site.  
  
 The following sections describe the logging levels and the categories that can be logged, and provide information about how to enable tracing in your application.  
  
## Logging Levels  
 Every log message that is created has an associated logging level. The logging level determines the importance of the log message, which is defined by the **Level** class in java.util.logging. Enabling logging at one level also enables logging at all higher levels. This section describes the logging levels for both public logging categories and internal logging categories. For more information about the logging categories, see the Logging Categories section in this article.  
  
 The following table describes each of the available logging levels for public logging categories.  
  
|Name|Description|  
|----------|-----------------|  
|SEVERE|Indicates a serious failure and is the highest level of logging. In the JDBC driver, this level is used for reporting errors and exceptions.|  
|WARNING|Indicates a potential problem.|  
|INFO|Provides informational messages.|  
|CONFIG|Provides configuration messages. Note that the JDBC driver doesn't currently provide any configuration messages.|  
|FINE|Provides basic tracing information including all exceptions thrown by the public methods.|  
|FINER|Provides detailed tracing information including all public method entry and exit points with the associated parameter data types, and all public properties for public classes. In addition, input parameters, output parameters, and method return values except CLOB, BLOB, NCLOB, Reader, \<stream> return value types.|  
|FINEST|Provides highly detailed tracing information. This is the lowest level of logging.|  
|OFF|Turns off logging.|  
|ALL|Enables logging of all messages.|  
  
 The following table describes each of the available logging levels for the internal logging categories.  
  
|Name|Description|  
|----------|-----------------|  
|SEVERE|Indicates a serious failure and is the highest level of logging. In the JDBC driver, this level is used for reporting errors and exceptions.|  
|WARNING|Indicates a potential problem.|  
|INFO|Provides informational messages.|  
|FINE|Provides tracing information including basic object creation and destruction. In addition, all exceptions thrown by the public methods.|  
|FINER|Provides detailed tracing information including all public method entry and exit points with the associated parameter data types, and all public properties for public classes. In addition, input parameters, output parameters, and method return values except CLOB, BLOB, NCLOB, Reader, \<stream> return value types.<br /><br /> The following logging categories existed in version 1.2 of the JDBC driver and had the FINE logging level: [SQLServerConnection](../../connect/jdbc/reference/sqlserverconnection-class.md), [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md), XA, and [SQLServerDataSource](../../connect/jdbc/reference/sqlserverdatasource-class.md). Beginning in the version 2.0 release, these are upgraded to the FINER level.|  
|FINEST|Provides highly detailed tracing information. This is the lowest level of logging.<br /><br /> The following logging categories existed in version 1.2 of the JDBC driver and had the FINEST logging level: TDS.DATA and TDS.TOKEN. Beginning in the version 2.0 release, they retain the FINEST logging level.|  
|OFF|Turns off logging.|  
|ALL|Enables logging of all messages.|  
  
## Logging Categories  
 When you create a Logger object, you must tell the object which named entity or category that you're interested in getting log information from. The JDBC driver supports the following public logging categories, which are all defined in the com.microsoft.sqlserver.jdbc driver package.  
  
|Name|Description|  
|----------|-----------------|  
|Connection|Logs messages in the [SQLServerConnection](../../connect/jdbc/reference/sqlserverconnection-class.md) class. The applications can set the logging level as FINER.|  
|Statement|Logs messages in the [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md) class. The applications can set the logging level as FINER.|  
|DataSource|Logs messages in the [SQLServerDataSource](../../connect/jdbc/reference/sqlserverdatasource-class.md) class. The applications can set the logging level as FINE.|  
|ResultSet|Logs messages in the [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) class. The applications can set the logging level as FINER.|  
|Driver|Logs messages in the [SQLServerDriver](../../connect/jdbc/reference/sqlserverdriver-class.md) class. The applications can set the logging level as FINER.|  
  
 Starting with the Microsoft JDBC Driver version 2.0, the driver also provides the com.microsoft.sqlserver.jdbc.internals package, which includes the logging support for the following internal logging categories.  
  
|Name|Description|  
|----------|-----------------|  
|AuthenticationJNI|Logs messages regarding the Windows integrated authentication issues (when the **authenticationScheme** connection property is implicitly or explicitly set to **NativeAuthentication**).<br /><br /> The applications can set the logging level as FINEST and FINE.|  
|SQLServerConnection|Logs messages in the [SQLServerConnection](../../connect/jdbc/reference/sqlserverconnection-class.md) class. The applications can set the logging level as FINE and FINER.|  
|SQLServerDataSource|Logs messages in the [SQLServerDataSource](../../connect/jdbc/reference/sqlserverdatasource-class.md), [SQLServerConnectionPoolDataSource](../../connect/jdbc/reference/sqlserverconnectionpooldatasource-class.md), and [SQLServerPooledConnection](../../connect/jdbc/reference/sqlserverpooledconnection-class.md) classes.<br /><br /> The applications can set the logging level as FINER.|  
|InputStream|Logs messages regarding the following data types: java.io.InputStream, java.io.Reader and the data types, which have a max specifier such as varchar, nvarchar, and varbinary data types.<br /><br /> The applications can set the logging level as FINER.|  
|SQLServerException|Logs messages in the [SQLServerException](../../connect/jdbc/reference/sqlserverexception-class.md) class. The applications can set the logging level as FINE.|  
|SQLServerResultSet|Logs messages in the [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md) class. The applications can set the logging level as FINE, FINER, and FINEST.|  
|SQLServerStatement|Logs messages in the [SQLServerStatement](../../connect/jdbc/reference/sqlserverstatement-class.md) class. The applications can set the logging level as FINE, FINER, and FINEST.|  
|XA|Logs messages for all XA transactions in the [SQLServerXADataSource](../../connect/jdbc/reference/sqlserverxadatasource-class.md) class. The applications can set the logging level as FINE and FINER.|  
|KerbAuthentication|Logs messages regarding type 4 Kerberos authentication (when the **authenticationScheme** connection property is set to **JavaKerberos**). The application can set the logging level as FINE or FINER.|  
|TDS.DATA|Logs messages containing the TDS protocol-level conversation between the driver and SQL Server. The detailed contents of each TDS packet sent and received are logged in ASCII and hexadecimal. The login credentials (user names and passwords) aren't logged. All other data is logged.<br /><br /> This category creates very verbose and detailed messages, and can only be enabled by setting the logging level to FINEST.|  
|TDS.Channel|This category traces actions of the TCP communications channel with SQL Server. The logged messages include socket opening and closing as well as reads and writes. It also traces messages related to establishing a Secure Sockets Layer (SSL) connection with SQL Server.<br /><br /> This category can only be enabled by setting the logging level to FINE, FINER, or FINEST.|  
|TDS.Writer|This category traces writes to the TDS channel. Note that only the length of the writes is traced, not the contents. This category also traces issues when an attention signal is sent to the server to cancel a statement's execution.<br /><br /> This category can only be enabled by setting the logging level to FINEST.|  
|TDS.Reader|This category traces certain read operations from the TDS channel at the FINEST level. At the FINEST level, tracing can be verbose. At WARNING and SEVERE levels, this category traces when the driver receives an invalid TDS protocol from SQL Server before the driver closes the connection.<br /><br /> This category can only be enabled by setting the logging level to FINER and FINEST.|  
|TDS.Command|This category traces low-level state transitions and other information associated with executing TDS commands, such as [!INCLUDE[tsql](../../includes/tsql-md.md)] statement executions, ResultSet cursor fetches, commits, and so on.<br /><br /> This category can only be enabled by setting the logging level to FINEST.|  
|TDS.TOKEN|This category logs only the tokens within the TDS packets, and is less verbose than the TDS.DATA category. It can only be enabled by setting the logging level to FINEST.<br /><br /> At the FINEST level, this category traces TDS tokens as they're processed in the response. At the SEVERE level, this category traces when an invalid TDS token is encountered.|  
|SQLServerDatabaseMetaData|Logs messages in the [SQLServerDatabaseMetaData](../../connect/jdbc/reference/sqlserverdatabasemetadata-class.md) class. The applications can set the logging level as FINE.|  
|SQLServerResultSetMetaData|Logs messages in the [SQLServerResultSetMetaData](../../connect/jdbc/reference/sqlserverresultsetmetadata-class.md) class. The applications can set the logging level as FINE.|  
|SQLServerParameterMetaData|Logs messages in the [SQLServerParameterMetaData](../../connect/jdbc/reference/sqlserverparametermetadata-class.md) class. The applications can set the logging level as FINE.|  
|SQLServerBlob|Logs messages in the [SQLServerBlob](../../connect/jdbc/reference/sqlserverblob-class.md) class. The applications can set the logging level as FINE.|  
|SQLServerClob|Logs messages in the [SQLServerClob](../../connect/jdbc/reference/sqlserverclob-class.md) class. The applications can set the logging level as FINE.|  
|SQLServerSQLXML|Logs messages in the internal SQLServerSQLXML class. The applications can set the logging level as FINE.|  
|SQLServerDriver|Logs messages in the [SQLServerDriver](../../connect/jdbc/reference/sqlserverdriver-class.md) class. The applications can set the logging level as FINE.|  
|SQLServerNClob|Logs messages in the [SQLServerNClob](../../connect/jdbc/reference/sqlservernclob-class.md) class. The applications can set the logging level as FINE.|  
  
## Enabling Tracing Programmatically  
 Tracing can be enabled programmatically by creating a Logger object and indicating the category to be logged. For example, the following code shows how to enable logging for SQL statements:  
  
```java
Logger logger = Logger.getLogger("com.microsoft.sqlserver.jdbc.Statement");  
logger.setLevel(Level.FINER);  
```  
  
 To turn off logging in your code, use the following:  
  
```java
logger.setLevel(Level.OFF);  
```  
  
 To log all available categories, use the following:  
  
```java
Logger logger = Logger.getLogger("com.microsoft.sqlserver.jdbc");  
logger.setLevel(Level.FINE);  
```  
  
 To disable a specific category from being logged, use the following:  
  
```java
Logger logger = Logger.getLogger("com.microsoft.sqlserver.jdbc.Statement");  
logger.setLevel(Level.OFF);  
```  
  
## Enabling Tracing by Using the Logging.Properties File  
 You can also enable tracing by using the `logging.properties` file, which can be found in the `lib` directory of your Java Runtime Environment (JRE) installation. This file can be used to set the default values for the loggers and handlers that will be used when tracing is enabled.  
  
 The following is an example of the settings that you can make in the `logging.properties` files:  
  
```  
# Specify the handler, the handlers will be installed during VM startup.  
handlers= java.util.logging.FileHandler  
  
# Default global logging level.  
.level= OFF  
  
# default file output is in user's home directory.  
java.util.logging.FileHandler.pattern = %h/java%u.log  
java.util.logging.FileHandler.limit = 5000000  
java.util.logging.FileHandler.count = 20  
java.util.logging.FileHandler.formatter = java.util.logging.SimpleFormatter  
java.util.logging.FileHandler.level = FINEST  
  
# Facility specific properties.  
com.microsoft.sqlserver.jdbc.level=FINEST  
  
```  
  
> [!NOTE]  
>  You can set the properties in the `logging.properties` file by using the LogManager object that is part of java.util.logging.  
  
## See Also  
 [Diagnosing Problems with the JDBC Driver](../../connect/jdbc/diagnosing-problems-with-the-jdbc-driver.md)  
  
  

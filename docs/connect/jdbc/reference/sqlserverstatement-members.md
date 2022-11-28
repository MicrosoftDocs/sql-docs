---
title: "SQLServerStatement Members"
description: "SQLServerStatement Members"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# SQLServerStatement Members
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  The following tables list the members that are exposed by the [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) class.  
  
## Constructors  
 None.  
  
## Fields  
 None.  
  
## Inherited Fields  
  
|Name|Description|  
|----------|-----------------|  
|java.sql.Statement|CLOSE_ALL_RESULTS, CLOSE_CURRENT_RESULT, EXECUTE_FAILED, KEEP_CURRENT_RESULT, NO_GENERATED_KEYS, RETURN_GENERATED_KEYS, SUCCESS_NO_INFO|  
  
## Methods  
  
|Name|Description|  
|----------|-----------------|  
|[addBatch](../../../connect/jdbc/reference/addbatch-method-sqlserverstatement.md)|Adds the given SQL command to the current list of commands for this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.|  
|[cancel](../../../connect/jdbc/reference/cancel-method-sqlserverstatement.md)|Cancels the SQL statement that is currently being run by this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.|  
|[clearBatch](../../../connect/jdbc/reference/clearbatch-method-sqlserverstatement.md)|Empties the current list of SQL commands for this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.|  
|[clearWarnings](../../../connect/jdbc/reference/clearwarnings-method-sqlserverstatement.md)|Clears all the warnings that are reported on this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.|  
|[close](../../../connect/jdbc/reference/close-method-sqlserverstatement.md)|Releases this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object's database and JDBC resources immediately instead of waiting for them to be automatically released.|  
|[execute](../../../connect/jdbc/reference/execute-method-sqlserverstatement.md)|Runs the given SQL statement, which can return multiple results.|  
|[executeBatch](../../../connect/jdbc/reference/executebatch-method-sqlserverstatement.md)|Submits a batch of commands to the database to be run. If all commands run successfully, returns an array of update counts.|  
|[executeQuery](../../../connect/jdbc/reference/executequery-method-sqlserverstatement.md)|Runs the given SQL statement and returns a single [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[executeUpdate](../../../connect/jdbc/reference/executeupdate-method-sqlserverstatement.md)|Runs the given SQL statement, which can be an INSERT, UPDATE, MERGE, or DELETE statement; or an SQL statement that returns nothing, such as an SQL DDL statement.|  
|[getConnection](../../../connect/jdbc/reference/getconnection-method-sqlserverstatement.md)|Retrieves the [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object that produced this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.|  
|[getFetchDirection](../../../connect/jdbc/reference/getfetchdirection-method-sqlserverstatement.md)|Retrieves the direction for fetching rows from database tables that is the default for result sets generated from this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.|  
|[getFetchSize](../../../connect/jdbc/reference/getfetchsize-method-sqlserverstatement.md)|Retrieves the number of result set rows that is the default fetch size for result set objects generated from this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.|  
|[getGeneratedKeys](../../../connect/jdbc/reference/getgeneratedkeys-method-sqlserverstatement.md)|Retrieves any auto-generated keys that are created as a result of running this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.|  
|[getMaxFieldSize](../../../connect/jdbc/reference/getmaxfieldsize-method-sqlserverstatement.md)|Retrieves the maximum number of bytes that can be returned for character and binary column values in a [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object that is produced by this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.|  
|[getMaxRows](../../../connect/jdbc/reference/getmaxrows-method-sqlserverstatement.md)|Retrieves the maximum number of rows that a [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object that is produced by this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object can contain.|  
|[getMoreResults](../../../connect/jdbc/reference/getmoreresults-method-sqlserverstatement.md)|Moves to the next result of this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.|  
|[getQueryTimeout](../../../connect/jdbc/reference/getquerytimeout-method-sqlserverstatement.md)|Retrieves the number of seconds the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] will wait for this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object to run.|  
|[getResponseBuffering](../../../connect/jdbc/reference/getresponsebuffering-method-sqlserverstatement.md)|Retrieves the response buffering mode for this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.|  
|[getResultSet](../../../connect/jdbc/reference/getresultset-method-sqlserverstatement.md)|Retrieves the current result as a [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.|  
|[getResultSetConcurrency](../../../connect/jdbc/reference/getresultsetconcurrency-method-sqlserverstatement.md)|Retrieves the result set concurrency for [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) objects that are generated by this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.|  
|[getResultSetHoldability](../../../connect/jdbc/reference/getresultsetholdability-method-sqlserverstatement.md)|Retrieves the result set holdability for [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) objects that are generated by this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.|  
|[getResultSetType](../../../connect/jdbc/reference/getresultsettype-method-sqlserverstatement.md)|Retrieves the result set type for [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) objects that are generated by this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.|  
|[getUpdateCount](../../../connect/jdbc/reference/getupdatecount-method-sqlserverstatement.md)|Retrieves the current result as an update count.|  
|[getWarnings](../../../connect/jdbc/reference/getwarnings-method-sqlserverstatement.md)|Retrieves the first warning that is reported by calls on this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.|  
|[isClosed](../../../connect/jdbc/reference/isclosed-method-sqlserverstatement.md)|Indicates whether this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object has been closed.|  
|[isPoolable](../../../connect/jdbc/reference/ispoolable-method-sqlserverstatement.md)|Returns a value indicating if a statement can be added to the user-provided statement pool.|  
|[isWrapperFor](../../../connect/jdbc/reference/iswrapperfor-method-sqlserverstatement.md)|Indicates whether this statement object is a wrapper for the specified interface.|  
|[setCursorName](../../../connect/jdbc/reference/setcursorname-method-sqlserverstatement.md)|Sets the SQL cursor name to the given String, which will be used by subsequent execute methods.|  
|[setEscapeProcessing](../../../connect/jdbc/reference/setescapeprocessing-method-sqlserverstatement.md)|Sets the escape processing mode.|  
|[setFetchDirection](../../../connect/jdbc/reference/setfetchdirection-method-sqlserverstatement.md)|Gives the JDBC driver a hint as to the direction in which result set rows should be processed.|  
|[setFetchSize](../../../connect/jdbc/reference/setfetchsize-method-sqlserverstatement.md)|Gives the JDBC driver a hint as to the number of rows that should be fetched from the database when more rows are needed.|  
|[setMaxFieldSize](../../../connect/jdbc/reference/setmaxfieldsize-method-sqlserverstatement.md)|Sets the limit for the maximum number of bytes in a [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) column storing character or binary values to the given number of bytes.|  
|[setMaxRows](../../../connect/jdbc/reference/setmaxrows-method-sqlserverstatement.md)|Sets the limit for the maximum number of rows that any [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object can contain to the given number.|  
|[setPoolable](../../../connect/jdbc/reference/setpoolable-method-sqlserverstatement.md)|Requests that a statement be pooled or not pooled.|  
|[setQueryTimeout](../../../connect/jdbc/reference/setquerytimeout-method-sqlserverstatement.md)|Sets the number of seconds the driver will wait for a [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object to run to the given number of seconds.|  
|[setResponseBuffering](../../../connect/jdbc/reference/setresponsebuffering-method-sqlserverstatement.md)|Sets the response buffering mode for this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object to case-insensitive **String full** or **adaptive**.|  
|[unwrap](../../../connect/jdbc/reference/unwrap-method-sqlserverstatement.md)|Returns an object that implements the specified interface to allow access to the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)]-specific methods.|  
  
## Inherited Methods  
  
|Class inherited from:|Methods|  
|---------------------------|-------------|  
|java.lang.Object|clone, equals, getClass, hashCode, notify, notifyAll, toString, wait|  
|java.sql.Wrapper|isWrapperFor, unwrap|  
  
## See Also  
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  

---
title: "SQLServerConnectionPoolDataSource Members"
description: "SQLServerConnectionPoolDataSource Members"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# SQLServerConnectionPoolDataSource Members
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  The following tables list the members that are exposed by the [SQLServerConnectionPoolDataSource](../../../connect/jdbc/reference/sqlserverconnectionpooldatasource-class.md) class.  
  
## Constructors  
  
|Name|Description|  
|----------|-----------------|  
|[SQLServerConnectionPoolDataSource ()](../../../connect/jdbc/reference/sqlserverconnectionpooldatasource-constructor.md)|Initializes a new instance of the [SQLServerConnectionPoolDataSource](../../../connect/jdbc/reference/sqlserverconnectionpooldatasource-class.md) class.|  
  
## Fields  
 None.  
  
## Inherited Fields  
 None.  
  
## Methods  
  
|Name|Description|  
|----------|-----------------|  
|[getApplicationIntent](../../../connect/jdbc/reference/getapplicationintent-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Returns the value of the **applicationIntent** connection property.|  
|[getApplicationName](../../../connect/jdbc/reference/getapplicationname-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Returns the application name.|  
|[getConnection](../../../connect/jdbc/reference/getconnection-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Tries to establish a connection with the data source that this DataSource object represents.|  
|[getDatabaseName](../../../connect/jdbc/reference/getdatabasename-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Returns the database name.|  
|[getDescription](../../../connect/jdbc/reference/getdescription-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Returns a description of the data source.|  
|[getFailoverPartner](../../../connect/jdbc/reference/getfailoverpartner-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Returns the name of the failover server that is used in a database mirroring configuration.|  
|[getInstanceName](../../../connect/jdbc/reference/getinstancename-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Returns the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance name.|  
|[getLastUpdateCount](../../../connect/jdbc/reference/getlastupdatecount-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Returns a **boolean** value that indicates if the lastUpdateCount property is enabled.|  
|[getLockTimeout](../../../connect/jdbc/reference/getlocktimeout-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Returns an **int** value that indicates the number of milliseconds that the database will wait before reporting a lock time out.|  
|[getLoginTimeout](../../../connect/jdbc/reference/getlogintimeout-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Returns the number of seconds that this DataSource object will wait while trying to make a connection.|  
|[getLogWriter](../../../connect/jdbc/reference/getlogwriter-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Returns a character output stream to be used for all logging and tracing messages.|  
|[getMultiSubnetFailover](../../../connect/jdbc/reference/getmultisubnetfailover-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Returns the value of the **multiSubnetFailover** connection property.|  
|[getPooledConnection](../../../connect/jdbc/reference/getpooledconnection-method-sqlserverconnectionpooldatasource.md)|Tries to establish a physical database connection that can be used as a pooled connection.|  
|[getPortNumber](../../../connect/jdbc/reference/getportnumber-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Returns the current port number that is used to communicate with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].|  
|[getReference](../../../connect/jdbc/reference/getreference-method-sqlserverconnectionpooldatasource.md)|Returns a reference to this DataSource object.|  
|[getSelectMethod](../../../connect/jdbc/reference/getselectmethod-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Returns the default cursor type that is used for all result sets that are created by using this DataSource object.|  
|[getSendStringParametersAsUnicode](../../../connect/jdbc/reference/getsendstringparametersasunicode-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Returns a **Boolean** value that indicates if sending string parameters to the server in UNICODE format is enabled.|  
|[getServerName](../../../connect/jdbc/reference/getservername-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Returns the name of the computer that is running [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].|  
|[getURL](../../../connect/jdbc/reference/geturl-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Returns the URL that is used to connect to the data source.|  
|[getUser](../../../connect/jdbc/reference/getuser-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Returns the user name that is used to connect the data source.|  
|[getWorkstationID](../../../connect/jdbc/reference/getworkstationid-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Returns the name of the client computer name that is used to connect to the data source.|  
|[getXopenStates](../../../connect/jdbc/reference/getxopenstates-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Returns a **Boolean** value that indicates if converting SQL states to XOPEN compliant states is enabled.|  
|[isWrapperFor](../../../connect/jdbc/reference/iswrapperfor-method-sqlserverconnectionpooldatasource.md)|Indicates whether this object is a wrapper for the specified interface.|  
|[setApplicationIntent](../../../connect/jdbc/reference/setapplicationintent-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Sets the value of the **applicationIntent** connection property.|  
|[setApplicationName](../../../connect/jdbc/reference/setapplicationname-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Sets the application name.|  
|[setAuthenticationSceme](../../../connect/jdbc/reference/setauthenticationscheme-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Indicates the kind of integrated security you want your application to use.|  
|[setDatabaseName](../../../connect/jdbc/reference/setdatabasename-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Sets the database name to connect to.|  
|[setDescription](../../../connect/jdbc/reference/setdescription-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Sets the description of the data source.|  
|[setFailoverPartner](../../../connect/jdbc/reference/setfailoverpartner-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Sets the name of the failover server that is used in a database mirroring configuration.|  
|[setInstanceName](../../../connect/jdbc/reference/setinstancename-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Sets the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance name.|  
|[setIntegratedSecurity](../../../connect/jdbc/reference/setintegratedsecurity-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Sets a **Boolean** value that indicates if the integratedSecurity property is enabled.|  
|[setLastUpdateCount](../../../connect/jdbc/reference/setlastupdatecount-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Sets a **Boolean** value that indicates if the lastUpdateCount property is enabled.|  
|[setLockTimeout](../../../connect/jdbc/reference/setlocktimeout-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Sets an **int** value that indicates the number of milliseconds to wait before the database reports a lock time out.|  
|[setLoginTimeout](../../../connect/jdbc/reference/setlogintimeout-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Sets the number of seconds that this DataSource object will wait while trying to make a connection.|  
|[setLogWriter](../../../connect/jdbc/reference/setlogwriter-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Sets a character output stream to be used for all logging and tracing messages.|  
|[setMultiSubnetFailover](../../../connect/jdbc/reference/setmultisubnetfailover-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Sets the value of the **multiSubnetFailover** connection property.|  
|[setPassword](../../../connect/jdbc/reference/setpassword-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Sets the password that will be used to connect to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].|  
|[setPortNumber](../../../connect/jdbc/reference/setportnumber-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Sets the port number to be used to communicate with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].|  
|[setSelectMethod](../../../connect/jdbc/reference/setselectmethod-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Sets the default cursor type that is used for all result sets that are created by using this DataSource object.|  
|[setSendStringParametersAsUnicode](../../../connect/jdbc/reference/setsendstringparametersasunicode-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Sets a **Boolean** value that indicates if sending string parameters to the server in UNICODE format is enabled.|  
|[setServerName](../../../connect/jdbc/reference/setservername-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Sets the name of the computer that is running [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].|  
|[setURL](../../../connect/jdbc/reference/seturl-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Sets the URL that is used to connect to the data source.|  
|[setUser](../../../connect/jdbc/reference/setuser-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Sets the user name that is used to connect the data source.|  
|[setWorkstationID](../../../connect/jdbc/reference/setworkstationid-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Sets the client computer name that is used to connect to the data source.|  
|[setXopenStates](../../../connect/jdbc/reference/setxopenstates-method-sqlserverdatasource.md)|(Inherited from [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md)) Sets a **boolean** value that indicates if converting SQL states to XOPEN compliant states is enabled.|  
|[unwrap](../../../connect/jdbc/reference/unwrap-method-sqlserverconnectionpooldatasource.md)|Returns an object that implements the specified interface to allow access to the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)]-specific methods.|  
  
## Inherited Methods  
  
|Class inherited from:|Methods|  
|---------------------------|-------------|  
|com.microsoft.sqlserver.jdbc.SQLServerDataSource|getApplicationName, getConnection, getDatabaseName, getDescription, getFailoverPartner, getInstanceName, getLastUpdateCount, getLockTimeout, getLoginTimeout, getLogWriter, getPortNumber, getSelectMethod, getSendStringParametersAsUnicode, getServerName, getURL, getUser, getWorkstationID, getXopenStates, setApplicationName, setDatabaseName, setDescription, setFailoverPartner, setInstanceName, setIntegratedSecurity, setLastUpdateCount, setLockTimeout, setLoginTimeout, setLogWriter, setPassword, setPortNumber, setSelectMethod, setSendStringParametersAsUnicode, setServerName, setURL, setUser, setWorkstationID, setXopenStates|  
|java.lang.Object|clone, equals, finalize, getClass, hashCode, notify, notifyAll, toString, wait|  
|java.sql.Wrapper|isWrapperFor, unwrap|  
|javax.sql.ConnectionPoolDataSource|getLoginTimeout, getLogWriter, setLoginTimeout, setLogWriter, getPooledConnection|  
  
## See Also  
 [SQLServerConnectionPoolDataSource Class](../../../connect/jdbc/reference/sqlserverconnectionpooldatasource-class.md)  
  
  

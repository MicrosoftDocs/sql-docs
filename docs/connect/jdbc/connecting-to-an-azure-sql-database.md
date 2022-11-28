---
title: Connect to an Azure SQL database
description: This article discusses issues when you use the Microsoft JDBC Driver for SQL Server to connect to an Azure SQL Database.
author: David-Engel
ms.author: v-davidengel
ms.date: 07/26/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Connect to an Azure SQL database

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This article discusses issues when you use the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] to connect to an [!INCLUDE[ssAzure](../../includes/ssazure_md.md)]. For more information to connect to an [!INCLUDE[ssAzure](../../includes/ssazure_md.md)], see:  
  
- [Azure SQL Database](/azure/sql-database/sql-database-technical-overview)  
  
- [How to: Connect to Azure SQL Using JDBC](/azure/sql-database/sql-database-connect-query-java)  

- [Connect using Azure Active Directory Authentication](connecting-using-azure-active-directory-authentication.md)  
  
## Details

To connect to an [!INCLUDE[ssAzure](../../includes/ssazure_md.md)], you should connect to the master database to call **SQLServerDatabaseMetaData.getCatalogs**.  
[!INCLUDE[ssAzure](../../includes/ssazure_md.md)] doesn't support returning the entire set of catalogs from a user database. **SQLServerDatabaseMetaData.getCatalogs** use the sys.databases view to get the catalogs. Refer to the discussion of permissions in [sys.databases (Transact-SQL)](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) to understand **SQLServerDatabaseMetaData.getCatalogs** behavior on an [!INCLUDE[ssAzure](../../includes/ssazure_md.md)].  
  
## Login timeout

When connecting to Azure SQL databases, the recommended default `loginTimeout` is 30 seconds. If you're connecting to a serverless instance, it's recommended to use an even longer `loginTimeout` of 60 seconds or more. If the serverless instance has been idle, it can take some time to wake up on an initial connection. For more information on how to set the `loginTimeout`, see [Setting the connection properties](setting-the-connection-properties.md).

## Connections dropped

When you connect to an [!INCLUDE[ssAzure](../../includes/ssazure_md.md)], idle connections may be terminated by a network component (such as a firewall) after a period of inactivity. There are two types of idle connections, in this context:  

- Idle at the TCP layer, where connections can be dropped by any number of network devices.  

- Idle by the Azure SQL Gateway, where TCP **keepalive** messages might be occurring (which makes the connection not idle from a TCP perspective), but not had an active query in 30 minutes. In this scenario, the Gateway will determine that the TDS connection is idle at 30 minutes and terminates the connection.  
  
To address the second point and avoid the Gateway terminating idle connections, you can:

- Use the **Redirect** [connection policy](/azure/azure-sql/database/connectivity-architecture#connection-policy) to configure your Azure SQL data source.

- Keep connections active via lightweight activity. This method isn't recommended and should only be used if there are no other possible options.

To address the first point and avoid dropping idle connections by a network component, set the following registry settings or their non-Windows equivalents on the operating system where the driver is loaded:  
  
|Registry Setting|Recommended Value|  
|----------------------|-----------------------|  
|HKEY_LOCAL_MACHINE \ SYSTEM \ CurrentControlSet \ Services \ Tcpip \ Parameters \ KeepAliveTime|30000|  
|HKEY_LOCAL_MACHINE \ SYSTEM \ CurrentControlSet \ Services \ Tcpip \ Parameters \ KeepAliveInterval|1000|  
|HKEY_LOCAL_MACHINE \ SYSTEM \ CurrentControlSet \ Services \ Tcpip \ Parameters \ TcpMaxDataRetransmissions|10|  
  
Restart the computer for the registry settings to take effect.  

The KeepAliveTime and KeepAliveInterval values are in milliseconds. These settings cause an unresponsive connection to disconnect within 10 to 40 seconds. If no response is received after a keep alive packet is sent, it will be retried every second up to 10 times. If no response is received during that time, the client-side socket is disconnected. Depending on your environment, you might want to increase the KeepAliveInterval to accommodate known disruptions (for example, virtual machine migrations), that might cause a server to be unresponsive for longer than 10 seconds.

> [!NOTE]
> TcpMaxDataRetransmissions isn't controllable on Windows Vista or Windows 2008 and higher.

To configure this in an Azure VM, create a startup task to add the registry keys.  For example, add the following Startup task to the service definition file:  

```xml
<Startup>  
    <Task commandLine="AddKeepAlive.cmd" executionContext="elevated" taskType="simple">  
    </Task>  
</Startup>  
```

Then add a AddKeepAlive.cmd file to your project. Set the "Copy to Output Directory" setting to Copy always. The following script is a sample AddKeepAlive.cmd file:  

```bat
if exist keepalive.txt goto done  
time /t > keepalive.txt  
REM Workaround for JDBC keep alive on Azure SQL  
REG ADD HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters /v KeepAliveTime /t REG_DWORD /d 30000 >> keepalive.txt  
REG ADD HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters /v KeepAliveInterval /t REG_DWORD /d 1000 >> keepalive.txt  
REG ADD HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters /v TcpMaxDataRetransmissions /t REG_DWORD /d 10 >> keepalive.txt  
shutdown /r /t 1  
:done  
```

## Append the server name to the userId in the connection string  

Prior to the 4.0 version of the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], to connect to an [!INCLUDE[ssAzure](../../includes/ssazure_md.md)], you were required to append the server name to the UserId in the connection string. For example, user@servername. Beginning in version 4.0 of the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], it's no longer necessary to append @servername to the UserId in the connection string.  

## Using encryption requires setting hostNameInCertificate

Prior to the 7.2 version of the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], to connect to an [!INCLUDE[ssAzure](../../includes/ssazure_md.md)], you should specify **hostNameInCertificate** if you specify **encrypt=true** (If the server name in the connection string is *shortName*.*domainName*, set the **hostNameInCertificate** property to \*.*domainName*.). This property is optional as of version 7.2 of the driver.

For example:

```java
jdbc:sqlserver://abcd.int.mscds.com;databaseName=myDatabase;user=myName;password=myPassword;encrypt=true;hostNameInCertificate=*.int.mscds.com;
```

## See also

[Connecting to SQL Server with the JDBC driver](connecting-to-sql-server-with-the-jdbc-driver.md)

---
title: "Connecting to an Azure SQL database | Microsoft Docs"
ms.custom: ""
ms.date: "01/21/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 49645b1f-39b1-4757-bda1-c51ebc375c34
author: MightyPen
ms.author: genemi
manager: craigg
---
# Connecting to an Azure SQL database

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

This article discusses issues when using the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] to connect to a [!INCLUDE[ssAzure](../../includes/ssazure_md.md)]. For more information about connecting to a [!INCLUDE[ssAzure](../../includes/ssazure_md.md)], see:  
  
- [SQL Azure Database](https://docs.microsoft.com/azure/sql-database/sql-database-technical-overview)  
  
- [How to: Connect to SQL Azure Using JDBC](https://docs.microsoft.com/azure/sql-database/sql-database-connect-query-java)  

- [Connecting using Azure Active Directory Authentication](../../connect/jdbc/connecting-using-azure-active-directory-authentication.md)  
  
## Details

When connecting to a [!INCLUDE[ssAzure](../../includes/ssazure_md.md)], you should connect to the master database to call **SQLServerDatabaseMetaData.getCatalogs**.  
[!INCLUDE[ssAzure](../../includes/ssazure_md.md)] doesn't support returning the entire set of catalogs from a user database. **SQLServerDatabaseMetaData.getCatalogs** use the sys.databases view to get the catalogs. Please refer to the discussion of permissions in [sys.databases (SQL Azure Database)](https://go.microsoft.com/fwlink/?LinkId=217396) to understand **SQLServerDatabaseMetaData.getCatalogs** behavior on a [!INCLUDE[ssAzure](../../includes/ssazure_md.md)].  
  
## Connections Dropped

When connecting to a [!INCLUDE[ssAzure](../../includes/ssazure_md.md)], idle connections may be terminated by a network component (such as a firewall) after a period of inactivity. There are two types of idle connections, in this context:  

- Idle at the TCP layer, where connections can be dropped by any number of network devices.  

- Idle by the SQL Azure Gateway, where TCP **keepalive** messages might be occurring (making the connection not idle from a TCP perspective), but not had an active query in 30 minutes. In this scenario, the Gateway will determine that the TDS connection is idle at 30 minutes and terminate the connection.  
  
To avoid dropping idle connections by a network component, the following registry settings (or their non-Windows equivalents) should be set on the operating system where the driver is loaded:  
  
|Registry Setting|Recommended Value|  
|----------------------|-----------------------|  
|HKEY_LOCAL_MACHINE \ SYSTEM \ CurrentControlSet \ Services \ Tcpip \ Parameters \ KeepAliveTime|30000|  
|HKEY_LOCAL_MACHINE \ SYSTEM \ CurrentControlSet \ Services \ Tcpip \ Parameters \ KeepAliveInterval|1000|  
|HKEY_LOCAL_MACHINE \ SYSTEM \ CurrentControlSet \ Services \ Tcpip \ Parameters \ TcpMaxDataRetransmissions|10|  
  
Restart the computer for the registry settings to take effect.  

To accomplish this when running in Windows Azure create a startup task to add the registry keys.  For example, add the following Startup task to the service definition file:  

```xml
<Startup>  
    <Task commandLine="AddKeepAlive.cmd" executionContext="elevated" taskType="simple">  
    </Task>  
</Startup>  
```

Then add a AddKeepAlive.cmd file to your project. Set the "Copy to Output Directory" setting to Copy always. The following is a sample AddKeepAlive.cmd file:  

```bat
if exist keepalive.txt goto done  
time /t > keepalive.txt  
REM Workaround for JDBC keep alive on SQL Azure  
REG ADD HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters /v KeepAliveTime /t REG_DWORD /d 30000 >> keepalive.txt  
REG ADD HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters /v KeepAliveInterval /t REG_DWORD /d 1000 >> keepalive.txt  
REG ADD HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters /v TcpMaxDataRetransmissions /t REG_DWORD /d 10 >> keepalive.txt  
shutdown /r /t 1  
:done  
```

## Appending the Server Name to the UserId in the Connection String  

Prior to the 4.0 version of the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], when connecting to an [!INCLUDE[ssAzure](../../includes/ssazure_md.md)], you were required to append the server name to the UserId in the connection string. For example, user@servername. Beginning in version 4.0 of the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], it's no longer necessary to append @servername to the UserId in the connection string.  

## Using Encryption Requires Setting hostNameInCertificate

Prior to the 7.2 version of the [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)], when connecting to an [!INCLUDE[ssAzure](../../includes/ssazure_md.md)], you should specify **hostNameInCertificate** if you specify **encrypt=true** (If the server name in the connection string is *shortName*.*domainName*, set the **hostNameInCertificate** property to \*.*domainName*.). This property is optional as of 7.2 version of the driver.

For example:

```java
jdbc:sqlserver://abcd.int.mscds.com;databaseName= myDatabase;user=myName;password=myPassword;encrypt=true;hostNameInCertificate= *.int.mscds.com;
```

## See Also

[Connecting to SQL Server with the JDBC Driver](../../connect/jdbc/connecting-to-sql-server-with-the-jdbc-driver.md)  

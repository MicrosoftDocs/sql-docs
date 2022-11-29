---
title: Connecting from Linux or macOS
description: Learn how to create a connection to a database from Linux or macOS using the Microsoft ODBC Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/08/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "connect to linux"
  - "configure odbc.ini"
---

# Connecting from Linux or macOS

[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

This article discusses how you can create a connection to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database.  
  
## Connection Properties  

See [DSN and Connection String Keywords and Attributes](../dsn-connection-string-attribute.md) for all the connection string keywords and attributes supported on Linux and macOS.

> [!IMPORTANT]  
> When connecting to a database that uses database mirroring (has a failover partner), do not specify the database name in the connection string. Instead, send a **use** _database_name_ command to connect to the database before executing your queries.  
  
The value passed to the **Driver** keyword can be one of the following:  
  
- The name you used when you installed the driver.

- The path to the driver library, which was specified in the template .ini file used to install the driver.  

DSNs are optional. You can use a DSN to define connection string keywords under a `DSN` name that you can then reference in the connection string. To create a DSN, create (if necessary) and edit the file **~/.odbc.ini** (`.odbc.ini` in your home directory) for a User DSN only accessible to the current user, or `/etc/odbc.ini` for a System DSN (administrative privileges required.) The following odbc.ini is a sample that shows the minimal required entries for a DSN:

```ini
# [DSN name]
[MSSQLTest]  
Driver = ODBC Driver 18 for SQL Server  
# Server = [protocol:]server[,port]  
Server = tcp:localhost,1433
Encrypt = yes
#
# Note:  
# Port isn't a valid keyword in the odbc.ini file  
# for the Microsoft ODBC driver on Linux or macOS
#  
```  

To connect using the above DSN in a connection string, you would specify the `DSN` keyword like: `DSN=MSSQLTest;UID=my_username;PWD=my_password`  
The above connection string would be the equivalent of specifying a connection string without the `DSN` keyword like: `Driver=ODBC Driver 18 for SQL Server;Server=tcp:localhost,1433;Encrypt=yes;UID=my_username;PWD=my_password`

You can optionally specify the protocol and port to connect to the server. For example, **Server=tcp:**_servername_**,12345**. The only protocol supported by the Linux and macOS drivers is `tcp`.

To connect to a named instance on a static port, use <b>Server=</b>_servername_,**port_number**. Connecting to a dynamic port isn't supported before version 17.4.

Alternatively, you can add the DSN information to a template file, and execute the following command to add it to `~/.odbc.ini` :

```bash
odbcinst -i -s -f <template_file>
```

For complete documentation on ini files and `odbcinst`, see the [unixODBC documentation](http://www.unixodbc.org/odbcinst.html). For entries in the `odbc.ini` file specific to the ODBC Driver for SQL Server, see [DSN and Connection String Keywords and Attributes](../dsn-connection-string-attribute.md) for ones supported on Linux and macOS.

You can verify that your driver is working by using `isql` to test the connection, or you can use this command:

```bash
bcp master.INFORMATION_SCHEMA.TABLES out OutFile.dat -S <server> -U <name> -P <password>
```

## Using TLS/SSL  

You can use Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL), to encrypt connections to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. TLS protects [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] user names and passwords over the network. TLS also verifies the identity of the server to protect against man-in-the-middle (MITM) attacks.  

Enabling encryption increases security at the expense of performance.

For more information, see [Encrypting Connections to SQL Server](/previous-versions/sql/sql-server-2008-r2/ms189067(v=sql.105)) and [Using Encryption Without Validation](../../../relational-databases/native-client/features/using-encryption-without-validation.md).

Regardless of the settings for **Encrypt** and **TrustServerCertificate**, the server login credentials (user name and password) are always encrypted. The following tables show the effect of the **Encrypt** and **TrustServerCertificate** settings.  

### ODBC Driver 18 and newer

| **Encrypt Setting** | **Trust Server Certificate** | **Server Force Encryption** | **Result** |
|---------------------|------------------------------|--------------------------|------------|
| No  | No  | No  | Server certificate isn't checked.<br/>Data sent between client and server isn't encrypted. |
| No  | Yes | No  | Server certificate isn't checked.<br/>Data sent between client and server isn't encrypted. |
| Yes | No  | No  | Server certificate is checked.<br/>Data sent between client and server is encrypted. |
| Yes | Yes | No  | Server certificate isn't checked.<br/>Data sent between client and server is encrypted. |
| No  | No  | Yes | Server certificate is checked.<br/>Data sent between client and server is encrypted. |
| No  | Yes | Yes | Server certificate isn't checked.<br/>Data sent between client and server is encrypted. |
| Yes | No  | Yes | Server certificate is checked.<br/>Data sent between client and server is encrypted. |
| Yes | Yes | Yes | Server certificate isn't checked.<br/>Data sent between client and server is encrypted. |
| Strict | - | - | TrustServerCertificate is ignored. Server certificate is checked.<br/>Data sent between client and server is encrypted. |

> [!NOTE]
> Strict is only available against servers that support TDS 8.0 connections.

### ODBC Driver 17 and older

| **Encrypt Setting** | **Trust Server Certificate** | **Server Force Encryption** | **Result** |
|---------------------|------------------------------|--------------------------|------------|
| No  | No  | No  | Server certificate isn't checked.<br/>Data sent between client and server isn't encrypted. |
| No  | Yes | No  | Server certificate isn't checked.<br/>Data sent between client and server isn't encrypted. |
| Yes | No  | No  | Server certificate is checked.<br/>Data sent between client and server is encrypted. |
| Yes | Yes | No  | Server certificate isn't checked.<br/>Data sent between client and server is encrypted. |
| No  | No  | Yes | Server certificate isn't checked.<br/>Data sent between client and server is encrypted. |
| No  | Yes | Yes | Server certificate isn't checked.<br/>Data sent between client and server is encrypted. |
| Yes | No  | Yes | Server certificate is checked.<br/>Data sent between client and server is encrypted. |
| Yes | Yes | Yes | Server certificate isn't checked.<br/>Data sent between client and server is encrypted. |

When using connection encryption, the name (or IP address) in a Subject Common Name (CN) or Subject Alternative Name (SAN) in a SQL Server TLS/SSL certificate should exactly match the server name (or IP address) specified in the connection string. The `HostnameInCertificate` keyword (v18.0+) can be used to specify an alternate name used to match against the names in the TLS/SSL certificate. When the keyword is specified, the SQL Server TLS/SSL certificate must match either one of the server name, or the `HostnameInCertificate`.

By default, encrypted connections always verify the server's certificate. However, if you connect to a server that has a self-signed certificate, and aren't using strict encryption mode, you can add the `TrustServerCertificate` option to bypass checking the certificate against the list of trusted certificate authorities:  

```ini
Driver={ODBC Driver 18 for SQL Server};Server=ServerNameHere;Encrypt=YES;TrustServerCertificate=YES  
```

In strict encryption mode, the certificate is always verified. As an option to standard certificate validation, the `ServerCertificate` keyword (v18.1+) can be used to specify the path to a certificate file to match against the SQL Server certificate. This option is only available when using strict encryption. The accepted certificate formats are PEM, DER, and CER. If specified, the SQL Server certificate is checked by seeing if the `ServerCertificate` provided is an exact match.<br/><br/>
TLS on Linux and macOS uses the OpenSSL library. The following table shows the minimum supported versions of OpenSSL and the default Certificate Trust Store locations for each platform:

|Platform|Minimum OpenSSL Version|Default Certificate Trust Store Location|  
|------------|---------------------------|--------------------------------------------|
|Debian 10, 11|1.1.1|/etc/ssl/certs|
|Debian 9|1.1.0|/etc/ssl/certs|
|Debian 8.71|1.0.1|/etc/ssl/certs|
|OS X 10.11, macOS|1.0.2|/usr/local/etc/openssl/certs|
|Red Hat Enterprise Linux 9|3.0.1|/etc/pki/tls/cert.pem|
|Red Hat Enterprise Linux 8|1.1.1|/etc/pki/tls/cert.pem|
|Red Hat Enterprise Linux 7|1.0.1|/etc/pki/tls/cert.pem|
|Red Hat Enterprise Linux 6|1.0.0-10|/etc/pki/tls/cert.pem|
|SUSE Linux Enterprise 15|1.1.0|/etc/ssl/certs|
|SUSE Linux Enterprise 11, 12|1.0.1|/etc/ssl/certs|
|Ubuntu 22.04|3.0.2|/etc/ssl/certs|
|Ubuntu 20.04, 21.04, 21.10 |1.1.1|/etc/ssl/certs|
|Ubuntu 18.04|1.1.0|/etc/ssl/certs|
|Ubuntu 16.04, 16.10, 17.10|1.0.2|/etc/ssl/certs|
|Ubuntu 14.04|1.0.1|/etc/ssl/certs|

You can also specify encryption in the connection string using the `Encrypt` option when using **SQLDriverConnect** to connect.

## Adjusting the TCP Keep-Alive Settings

Starting with ODBC Driver 17.4, how often the driver sends keep-alive packets and retransmits them when a response isn't received is configurable.
To configure, add the following settings to either the driver's section in `odbcinst.ini`, or the DSN's section in `odbc.ini`. When connecting
with a DSN, the driver will use the settings in the DSN's section if present; otherwise, or if connecting with a connection string only, it will use the
settings in the driver's section in `odbcinst.ini`. If the setting isn't present in either location, the driver uses the default value.
Beginning with ODBC Driver 17.8, `KeepAlive` and `KeepAliveInterval` keywords can be specified in the connection string.

- `KeepAlive=<integer>` controls how often TCP attempts to verify that an idle connection is still intact by sending a keep-alive packet. The default is **30** seconds.

- `KeepAliveInterval=<integer>` determines the interval separating keep-alive retransmissions until a response is received.  The default is **1** second.

## See Also

- [Installing the Microsoft ODBC Driver for SQL Server on Linux](installing-the-microsoft-odbc-driver-for-sql-server.md)
- [Installing the Microsoft ODBC Driver for SQL Server on macOS](install-microsoft-odbc-driver-sql-server-macos.md)
- [Programming Guidelines](programming-guidelines.md)

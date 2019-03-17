---
title: "Connecting to SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data source names"
  - "connection string keywords"
  - "DSNs"
ms.assetid: f95cdbce-e7c2-4e56-a9f7-8fa3a920a125
author: MightyPen
ms.author: genemi
manager: craigg
---
# Connecting to SQL Server
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

This topic discusses how you can create a connection to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database.  
  
## Connection Properties  

See [DSN and Connection String Keywords and Attributes](../../../connect/odbc/dsn-connection-string-attribute.md) for all the connection string keywords and attributes supported on Linux and Mac

> [!IMPORTANT]  
> When connecting to a database that uses database mirroring (has a failover partner), do not specify the database name in the connection string. Instead, send a **use** _database_name_ command to connect to the database before executing your queries.  
  
The value passed to the **Driver** keyword can be one of the following:  
  
-   The name you used when you installed the driver.

-   The path to the driver library, which was specified in the template .ini file used to install the driver.  

To create a DSN, create (if necessary) and edit the file **~/.odbc.ini** (`.odbc.ini` in your home directory) for a User DSN only accessible to the current user, or `/etc/odbc.ini` for a System DSN (administrative privileges required.) The following is a sample file that shows the minimal required entries for a DSN:  

```  
[MSSQLTest]  
Driver = ODBC Driver 13 for SQL Server  
Server = [protocol:]server[,port]  
#   
# Note:  
# Port is not a valid keyword in the odbc.ini file  
# for the Microsoft ODBC driver on Linux or macOS
#  
```  

You can optionally specify the protocol and port to connect to the server. For example, **Server=tcp:**_servername_**,12345**. Note that the only protocol supported by the Linux and macOS drivers is `tcp`.

To connect to a named instance on a static port, use <b>Server=</b>*servername*,**port_number**. Connecting to a dynamic port is not supported.  

Alternatively, you can add the DSN information to a template file, and execute the following command to add it to `~/.odbc.ini` :
 - **odbcinst -i -s -f** _template_file_  
 
You can verify that your driver is working by using `isql` to test the connection, or you can use this command:
 - **bcp master.INFORMATION_SCHEMA.TABLES out OutFile.dat -S <server> -U <name> -P <password>**  

## Using Secure Sockets Layer (SSL)  
You can use Secure Sockets Layer (SSL) to encrypt connections to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. SSL protects [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] user names and passwords over the network. SSL also verifies the identity of the server to protect against man-in-the-middle (MITM) attacks.  

Enabling encryption increases security at the expense of performance.

For more information, see [Encrypting Connections to SQL Server](https://go.microsoft.com/fwlink/?LinkId=220900) and [Using Encryption Without Validation](https://docs.microsoft.com/sql/relational-databases/native-client/features/using-encryption-without-validation).

Regardless of the settings for **Encrypt** and **TrustServerCertificate**, the server login credentials (user name and password) are always encrypted. The following table shows the effect of the **Encrypt** and **TrustServerCertificate** settings.  

||**TrustServerCertificate=no**|**TrustServerCertificate=yes**|  
|-|-------------------------------------|------------------------------------|  
|**Encrypt=no**|Server certificate is not checked.<br /><br />Data sent between client and server is not encrypted.|Server certificate is not checked.<br /><br />Data sent between client and server is not encrypted.|  
|**Encrypt=yes**|Server certificate is checked.<br /><br />Data sent between client and server is encrypted.<br /><br />The name (or IP address) in a Subject Common Name (CN) or Subject Alternative Name (SAN) in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] SSL certificate should exactly match the server name (or IP address) specified in the connection string.|Server certificate is not checked.<br /><br />Data sent between client and server is encrypted.|  

By default, encrypted connections always verify the server's certificate. However, if you connect to a server that has a self-signed certificate, also add the `TrustServerCertificate` option to bypass checking the certificate against the list of trusted certificate authorities:  

```  
Driver={ODBC Driver 13 for SQL Server};Server=ServerNameHere;Encrypt=YES;TrustServerCertificate=YES  
```  
  
SSL uses the OpenSSL library. The following table shows the minimum supported versions of OpenSSL and the default Certificate Trust Store locations for each platform:

|Platform|Minimum OpenSSL Version|Default Certificate Trust Store Location|  
|------------|---------------------------|--------------------------------------------|
|Debian 9|1.1.0|/etc/ssl/certs|
|Debian 8.71 |1.0.1|/etc/ssl/certs|
|macOS 10.13|1.0.2|/usr/local/etc/openssl/certs|
|macOS 10.12|1.0.2|/usr/local/etc/openssl/certs|
|OS X 10.11|1.0.2|/usr/local/etc/openssl/certs|
|Red Hat Enterprise Linux 7|1.0.1|/etc/pki/tls/cert.pem|
|Red Hat Enterprise Linux 6|1.0.0-10|/etc/pki/tls/cert.pem|
|SuSE Linux Enterprise 12 |1.0.1|/etc/ssl/certs|
|SuSE Linux Enterprise 11 |0.9.8|/etc/ssl/certs|
|Ubuntu 17.10 |1.0.2|/etc/ssl/certs|
|Ubuntu 16.10 |1.0.2|/etc/ssl/certs|
|Ubuntu 16.04 |1.0.2|/etc/ssl/certs|
  
You can also specify encryption in the connection string using the `Encrypt` option when using **SQLDriverConnect** to connect.

## See Also  
[Installing the Microsoft ODBC Driver for SQL Server on Linux and macOS](../../../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md)  
[Programming Guidelines](../../../connect/odbc/linux-mac/programming-guidelines.md)

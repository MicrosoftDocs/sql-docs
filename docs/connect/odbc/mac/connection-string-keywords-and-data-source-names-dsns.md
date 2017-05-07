---
title: "Connection String Keywords and Data Source Names (DSNs) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology:
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords:
  - "data source names"
  - "connection string keywords"
  - "DSNs"
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Connection String Keywords and Data Source Names (DSNs)
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

This topic discusses how you can create a connection to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] database.  

## Connection Properties  
For this release of the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] on macOS, you can use the following connection keywords:  

||||||  
|-|-|-|-|-|  
|`Addr`|`Address`|`ApplicationIntent`|`AutoTranslate`|`Database`|
|`Driver`|`DSN`|`Encrypt`|`FileDSN`|`MARS_Connection`|  
|`MultiSubnetFailover`|`PWD`|`Server`|`Trusted_Connection`|`TrustServerCertificate`|  
|`UID`|`WSID`|`ColumnEncryption`|`TransparentNetworkIPResolution`||  

> [!IMPORTANT]  
> When connecting to a database that uses database mirroring (has a failover partner), do not specify the database name in the connection string. Instead, send a **use** *database_name* command to connect to the database before executing your queries.  

For more information about these keywords, see the ODBC section of [Using Connection String Keywords with SQL Server Native Client](http://go.microsoft.com/fwlink/?LinkID=126696).

The value passed to the **Driver** keyword can be one of the following:  

-   The name you used when you installed the driver.

-   The path to the driver library, which was specified in the template .ini file used to install the driver.  

To create a DSN, create (if necessary) and edit the file **~/.odbc.ini** (`odbc.ini` in your home directory) for a User DSN only accessible to the current user, or `/etc/odbc.ini` for a System DSN (administrative privileges required.) The following is a sample file that shows the minimal required entries for a DSN:  

```  
[MSSQLTest]  
Driver = ODBC Driver 11 for SQL Server  
Server = [protocol:]server[,port]  
#   
# Note:  
# Port is not a valid keyword in the ~/.odbc.ini file  
# for the Microsoft ODBC driver on macOS  
#  
```  

You can optionally specify the protocol and port to connect to the server. For example, **Server = tcp:***servername***,12345**. Note that the only protocol supported by the macOS driver is `tcp`.

To connect to a named instance on a static port, use **Server =** *servername***,***port_number*. Connecting to a dynamic port is not supported.  

Alternatively, you can add the DSN information to a template file, and execute the following command to add it to `~/.odbc.ini` :
 * **odbcinst -i -s -f** *template_file*  
 
You can verify that your driver is working by using `isql` to test the connection, or you can use this command:
 * **bcp master.INFORMATION_SCHEMA.TABLES out OutFile.dat -S <server> -U <name> -P <password>**  

## Using Secure Sockets Layer (SSL)  
You can use Secure Sockets Layer (SSL) to encrypt connections to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)]. SSL protects [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] user names and passwords over the network. SSL also verifies the identity of the server to protect against man-in-the-middle (MITM) attacks.  

Enabling encryption increases security at the expense of performance.  

For more information, see [Encrypting Connections to SQL Server](http://go.microsoft.com/fwlink/?LinkId=220900) and [Using Encryption Without Validation](https://docs.microsoft.com/en-us/sql/relational-databases/native-client/features/using-encryption-without-validation).

Regardless of the settings for **Encrypt** and **TrustServerCertificate**, the server login credentials (user name and password) are always encrypted. The following table shows the effect of the **Encrypt** and **TrustServerCertificate** settings.  

||**TrustServerCertificate=no**|**TrustServerCertificate=yes**|  
|-|-------------------------------------|------------------------------------|  
|**Encrypt=no**|Server certificate is not checked.<br /><br />Data sent between client and server is not encrypted.|Server certificate is not checked.<br /><br />Data sent between client and server is not encrypted.|  
|**Encrypt=yes**|Server certificate is checked.<br /><br />Data sent between client and server is encrypted.<br /><br />The name (or IP address) in a Subject Common Name (CN) or Subject Alternative Name (SAN) in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] SSL certificate should exactly match the server name (or IP address) specified in the connection string.|Server certificate is not checked.<br /><br />Data sent between client and server is encrypted.|  

By default, encrypted connections always verify the server’s certificate. However, if you connect to a server that has a self-signed certificate, also add the `TrustServerCertificate` option to bypass checking the certificate against the list of trusted certificate authorities:  

```  
Driver='ODBC Driver 13 for SQL Server';Server=ServerNameHere;Encrypt=YES;TrustServerCertificate=YES  
```  


You can use **SQLDriverConnect** to specify encryption in the connection string.  

## See Also  
[Microsoft ODBC Driver for SQL Server on macOS](../../../connect/odbc/mac/microsoft-odbc-driver-for-sql-server-on-mac.md)  
[Installing the Microsoft ODBC Driver for SQL Server on macOS](../../../connect/odbc/mac/installing-the-microsoft-odbc-driver-for-sql-server-on-macos.md)  

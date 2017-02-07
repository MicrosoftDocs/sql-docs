---
title: "Using Integrated Authentication | Microsoft Docs"
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
  - "integrated authentication"
ms.assetid: 9499ffdf-e0ee-4d3c-8bca-605371eb52d9
caps.latest.revision: 23
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Using Integrated Authentication
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

The [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] on Linux supports connections that use Kerberos integrated authentication. The ODBC driver on Linux supports MIT Kerberos Key Distribution Center (KDC), and works with Generic Security Services Application Program Interface (GSSAPI) and Kerberos libraries.  
  
## Using Integrated Authentication to Connect to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] from an ODBC Application  
You can enable Kerberos Windows integrated authentication by specifying **Trusted_Connection=yes** in the connection string of **SQLDriverConnect** or **SQLConnect**. For example:  
  
```  
Driver='ODBC Driver 11 for SQL Server';Server=your_server;Trusted_Connection=yes  
```  
  
You can also add **Trusted_Connection=yes** in the DSN entry of the ODBC.ini.  
  
You can also use the **-E** option in **sqlcmd** and the **-T** option in **bcp**; see [Connecting with sqlcmd](../../../connect/odbc/linux/connecting-with-sqlcmd.md) for more information.  
  
Ensure that the Linux principal server that is going to connect to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] is already authenticated with the Kerberos KDC.  
  
**ServerSPN** and **FailoverPartnerSPN** are not supported.  
  
## Deploying an ODBC Driver on Linux Application Designed to Run as a Service  
A system administrator can deploy an application to run as a service that uses Kerberos Authentication to connect to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)].  
  
You first need to configure Kerberos on the Linux computer and then ensure that the application can use the Kerberos credential of the default principal.  
  
Ensure that you use kinit or PAM (Pluggable Authentication Module) to obtain and cache the TGT for the principal that the connection uses:  
  
-   Run kinit, passing in a principal name and password.  
  
-   Run kinit, passing in a principal name and a location of a keytab file that contains the principal’s key, that ktutil created.  
  
-   Ensure that the login to the system was done using PAM (Pluggable Authentication Module).  
  
Because an application runs as a service and Kerberos credentials, by design, expire, renew the credentials to ensure continued service availability. The ODBC driver on Linux does not provide the renewal of the credentials. Ensure that there is a cron job or script that periodically runs to renew the credentials before the expiration. To avoid requiring the password for each renewal in this case you can use the keytab file, created earlier.  
  
[Kerberos Configuration and Use](http://commons.oreilly.com/wiki/index.php/Linux_in_a_Windows_World/Centralized_Authentication_Tools/Kerberos_Configuration_and_Use) provides details on ways to Kerberize services on Linux.  
  
## Tracking Access to a Database  
A database administrator can create an audit trail of access to a database when using system accounts to access [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] using Integrated Authentication.  
  
Logging in to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] uses the system account and there is no functionality on Linux to impersonate security context. Therefore, more is required to determine the user.  
  
To audit activities in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] on behalf of users other than system account, the application must use [!INCLUDE[tsql](../../../includes/tsql_md.md)]**EXECUTE AS**.  
  
To improve application performance, an application can use connection pooling with Integrated Authentication and auditing. However, combining connection pooling, Integrated Authentication, and auditing creates a security risk because the Unix ODBC Driver manager permits different users to reuse pooled connections. For more information, see [ODBC Connection Pooling](http://www.unixodbc.org/doc/conn_pool.html).  
  
Before reuse, an application must reset pooled connections by executing sp_reset_connection.  
  
## Using Active Directory to Manage User Identities  
An application system administrator does not have to manage separate sets of login credentials for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)]. It is possible to configure Active Directory as a key distribution center (KDC) for Integrated Authentication.  
  
## Using Linked Server and Distributed Queries  
Developers can deploy an application that uses a linked server or distributed queries without a database administrator who maintains separate sets of SQL credentials. In that situation, a developer must configure an application to use integrated authentication:  
  
-   User logs in to a client machine and authenticates to the application server.  
  
-   The application server authenticates as a different database and connects to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)].  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] authenticates as a database user to another database ([!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)].  
  
After integrated authentication is configured, credentials will be passed to the linked server.  
  
## Integrated Authentication and sqlcmd  
To access [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] using integrated authentication, use the -E sqlcmd option. Ensure that the account that runs sqlcmd is associated with the default Kerberos principal server.  
  
## Integrated Authentication and bcp  
To access [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] using integrated authentication, use the -T bcp option. Ensure that the account that runs bcp is associated with the default Kerberos principal server.  
  
It is an error to use -T with the -U or -P option.  
  
## Supported Syntax for an SPN Registered by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)]  
The syntax that SPNs use in connection string or connection attributes is as follows:  
  
|Syntax|Description|  
|----------|---------------|  
|MSSQLSvc/*fqdn*:*port*|The provider-generated, default SPN when TCP is used. *port* is a TCP port number. *fqdn* is a fully qualified domain name.|  
  
## Authenticating a Linux Computer with Active Directory  
(For more information about authenticating your Linux computer with Active Directory, see [Authenticate Linux Clients with Active Directory](http://technet.microsoft.com/magazine/2008.12.linux.aspx#id0060048).)  
  
Enter data into the krb5.conf file. krb5.conf is in /etc/ but you can refer to another file (**export KRB5_CONFIG=/home/dbapp/etc/krb5.conf**). The following is an example krb5.conf file:  
  
```  
[libdefaults]  
default_realm = YYYY.CORP.CONTOSO.COM  
dns_lookup_realm = false  
dns_lookup_kdc = true  
ticket_lifetime = 24h  
forwardable = yes  
  
[domain_realm]  
.yyyy.corp.contoso.com = YYYY.CORP.CONTOSO.COM  
.zzzz.corp.contoso.com = ZZZZ.CORP.CONTOSO.COM  
```  
  
If your Linux computer uses Dynamic Host Configuration Protocol (DHCP) and the Windows DHCP server provides the DNS servers to use, you can use **dns_lookup_kdc=true**. Now, you can use Kerberos to sign in to your domain, as follows: `kinit  alias@YYYY.CORP.CONTOSO.COM`. Parameters passed to kinit are case-sensitive and the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] computer configured to be in the domain must have that user alias@YYYY.CORP.CONTOSO.COM added for login. The ODBC driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] on Linux does not support generating Kerberos credentials via a keytab file. Now, you can use trusted connections (**Trusted_Connection=YES** in a connection string or **sqlcmd -E**).  
  
The time on the Linux computer and the time on the Kerberos Domain Controller (KDC) must be close. Ensure that your system time is set correctly and equivalent to the Network Time Protocol (NTP).  
  
If Kerberos authentication fails, the ODBC driver on Linux does not use NTLM authentication.  
  
## See Also  
[Microsoft ODBC Driver for SQL Server on Linux](../../../connect/odbc/linux/microsoft-odbc-driver-for-sql-server-on-linux.md)  
  

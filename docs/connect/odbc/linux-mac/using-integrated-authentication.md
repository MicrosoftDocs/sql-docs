---
title: "Using Integrated Authentication | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "integrated authentication"
ms.assetid: 9499ffdf-e0ee-4d3c-8bca-605371eb52d9
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using Integrated Authentication
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

The [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux and macOS supports connections that use Kerberos integrated authentication. It supports the MIT Kerberos Key Distribution Center (KDC), and works with Generic Security Services Application Program Interface (GSSAPI) and Kerberos v5 libraries.
  
## Using Integrated Authentication to Connect to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] from an ODBC Application  

You can enable Kerberos integrated authentication by specifying **Trusted_Connection=yes** in the connection string of **SQLDriverConnect** or **SQLConnect**. For example:  

```
Driver='ODBC Driver 13 for SQL Server';Server=your_server;Trusted_Connection=yes  
```
  
When connecting with a DSN, you can also add **Trusted_Connection=yes** to the DSN entry in `odbc.ini`.
  
The `-E` option of `sqlcmd` and the `-T` option of `bcp` can also be used to specify integrated authentication; see [Connecting with **sqlcmd**](../../../connect/odbc/linux-mac/connecting-with-sqlcmd.md) and [Connecting with **bcp**](../../../connect/odbc/linux-mac/connecting-with-bcp.md) for more information.

Ensure that the client principal which is going to connect to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is already authenticated with the Kerberos KDC.
  
**ServerSPN** and **FailoverPartnerSPN** are not supported.  
  
## Deploying a Linux or macOS ODBC Driver Application Designed to Run as a Service

A system administrator can deploy an application to run as a service that uses Kerberos Authentication to connect to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
You first need to configure Kerberos on the client and then ensure that the application can use the Kerberos credential of the default principal.

Ensure that you use `kinit` or PAM (Pluggable Authentication Module) to obtain and cache the TGT for the principal that the connection uses, via one of the following methods:  
  
-   Run `kinit`, passing in a principal name and password.  
  
-   Run `kinit`, passing in a principal name and a location of a keytab file that contains the principal's key created by `ktutil`.  
  
-   Ensure that the login to the system was done using the Kerberos PAM (Pluggable Authentication Module).

When an application runs as a service, because Kerberos credentials expire by design, renew the credentials to ensure continued service availability. The ODBC driver does not renew credentials itself; ensure that there is a `cron` job or script that periodically runs to renew the credentials before their expiration. To avoid requiring the password for each renewal, you can use a keytab file.  
  
[Kerberos Configuration and Use](https://commons.oreilly.com/wiki/index.php/Linux_in_a_Windows_World/Centralized_Authentication_Tools/Kerberos_Configuration_and_Use) provides details on ways to Kerberize services on Linux.
  
## Tracking Access to a Database

A database administrator can create an audit trail of access to a database when using system accounts to access [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] using Integrated Authentication.  
  
Logging in to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] uses the system account and there is no functionality on Linux to impersonate security context. Therefore, more is required to determine the user.
  
To audit activities in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on behalf of users other than the system account, the application must use [!INCLUDE[tsql](../../../includes/tsql-md.md)] **EXECUTE AS**.  
  
To improve application performance, an application can use connection pooling with Integrated Authentication and auditing. However, combining connection pooling, Integrated Authentication, and auditing creates a security risk because the unixODBC driver manager permits different users to reuse pooled connections. For more information, see [ODBC Connection Pooling](https://www.unixodbc.org/doc/conn_pool.html).  

Before reuse, an application must reset pooled connections by executing `sp_reset_connection`.  

## Using Active Directory to Manage User Identities

An application system administrator does not have to manage separate sets of login credentials for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. It is possible to configure Active Directory as a key distribution center (KDC) for Integrated Authentication. See [Microsoft Kerberos](/windows/desktop/SecAuthN/microsoft-kerberos) for more information.

## Using Linked Server and Distributed Queries

Developers can deploy an application that uses a linked server or distributed queries without a database administrator who maintains separate sets of SQL credentials. In this situation, a developer must configure an application to use integrated authentication:  
  
-   User logs in to a client machine and authenticates to the application server.  
  
-   The application server authenticates as a different database and connects to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
-   [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] authenticates as a database user to another database ([!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
After integrated authentication is configured, credentials will be passed to the linked server.  
  
## Integrated Authentication and sqlcmd
To access [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] using integrated authentication, use the `-E` option of `sqlcmd`. Ensure that the account which runs `sqlcmd` is associated with the default Kerberos client principal.

## Integrated Authentication and bcp
To access [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] using integrated authentication, use the `-T` option of `bcp`. Ensure that the account which runs `bcp` is associated with the default Kerberos client principal. 
  
It is an error to use `-T` with the `-U` or `-P` option.
  
## Supported Syntax for an SPN Registered by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]

The syntax that SPNs use in the connection string or connection attributes is as follows:  

|Syntax|Description|  
|----------|---------------|  
|MSSQLSvc/*fqdn*:*port*|The provider-generated, default SPN when TCP is used. *port* is a TCP port number. *fqdn* is a fully qualified domain name.|  
  
## Authenticating a Linux or macOS Computer with Active Directory

To configure Kerberos, enter data into the `krb5.conf` file. `krb5.conf` is in `/etc/` but you can refer to another file using the syntax e.g. `export KRB5_CONFIG=/home/dbapp/etc/krb5.conf`. The following is an example `krb5.conf` file:  
  
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
  
If your Linux or macOS computer is configured to use the Dynamic Host Configuration Protocol (DHCP) with a Windows DHCP server providing the DNS servers to use, you can use **dns_lookup_kdc=true**. Now, you can use Kerberos to sign in to your domain by issuing the command `kinit alias@YYYY.CORP.CONTOSO.COM`. Parameters passed to `kinit` are case-sensitive and the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] computer configured to be in the domain must have that user `alias@YYYY.CORP.CONTOSO.COM` added for login. Now, you can use trusted connections (**Trusted_Connection=YES** in a connection string, **bcp -T**, or **sqlcmd -E**).  
  
The time on the Linux or macOS computer and the time on the Kerberos Key Distribution Center (KDC) must be close. Ensure that your system time is set correctly, e.g. by using the Network Time Protocol (NTP).  

If Kerberos authentication fails, the ODBC driver on Linux or macOS does not use NTLM authentication.  

For more information about authenticating Linux or macOS computers with Active Directory, see [Authenticate Linux Clients with Active Directory](https://technet.microsoft.com/magazine/2008.12.linux.aspx#id0060048) and [Best Practices for Integrating OS X with Active Directory](https://training.apple.com/pdf/Best_Practices_for_Integrating_OS_X_with_Active_Directory.pdf). For more information about configuring Kerberos, see the [MIT Kerberos Documentation](https://web.mit.edu/kerberos/krb5-1.12/doc/index.html).

## See Also  
[Programming Guidelines](../../../connect/odbc/linux-mac/programming-guidelines.md)

[Release Notes](../../../connect/odbc/linux-mac/release-notes.md)

[Using Azure Active Directory](../../../connect/odbc/using-azure-active-directory.md)

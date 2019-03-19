---
title: "Register a Service Principal Name for Kerberos Connections | Microsoft Docs"
ms.custom: ""
ms.date: "11/20/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "connections [SQL Server], SPNs"
  - "network connections [SQL Server], SPNs"
  - "registering SPNs"
  - "Server Principal Names"
  - "SPNs [SQL Server]"
ms.assetid: e38d5ce4-e538-4ab9-be67-7046e0d9504e
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Register a Service Principal Name for Kerberos Connections
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  To use Kerberos authentication with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires both the following conditions to be true:  
  
-   The client and server computers must be part of the same Windows domain, or in trusted domains.  
  
-   A Service Principal Name (SPN) must be registered with Active Directory, which assumes the role of the Key Distribution Center in a Windows domain. The SPN, after it is registered, maps to the Windows account that started the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance service. If the SPN registration has not been performed or fails, the Windows security layer cannot determine the account associated with the SPN, and Kerberos authentication will not be used.  
  
    > [!NOTE]  
    >  If the server cannot automatically register the SPN, the SPN must be registered manually. See [Manual SPN Registration](#Manual).  
  
You can verify that a connection is using Kerberos by querying the sys.dm_exec_connections dynamic management view. Run the following query and check the value of the auth_scheme column, which will be "KERBEROS" if Kerberos is enabled.  
  
```sql  
SELECT auth_scheme FROM sys.dm_exec_connections WHERE session_id = @@spid ;  
```  
  
> [!TIP]  
>  **[!INCLUDE[msCoName](../../includes/msconame-md.md)] Kerberos Configuration Manager for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]** is a diagnostic tool that helps troubleshoot Kerberos related connectivity issues with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Microsoft Kerberos Configuration Manager for SQL Server](https://www.microsoft.com/download/details.aspx?id=39046).  
  
##  <a name="Role"></a> The Role of the SPN in Authentication  
 When an application opens a connection and uses Windows Authentication, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client passes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer name, instance name and, optionally, an SPN. If the connection passes an SPN it is used without any changes.  
  
 If the connection does not pass an SPN, a default SPN is constructed based on the protocol used, server name, and the instance name.  
  
 In both of the preceding scenarios, the SPN is sent to the Key Distribution Center to obtain a security token for authenticating the connection. If a security token cannot be obtained, authentication uses NTLM.  
  
 A service principal name (SPN) is the name by which a client uniquely identifies an instance of a service. The Kerberos authentication service can use an SPN to authenticate a service. When a client wants to connect to a service, it locates an instance of the service, composes an SPN for that instance, connects to the service, and presents the SPN for the service to authenticate.  
  
> [!NOTE]  
>  The information that is provided in this topic also applies to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] configurations that use clustering.  
  
 Windows Authentication is the preferred method for users to authenticate to SQL Server. Clients that use Windows Authentication are authenticated by either using NTLM or Kerberos. In an Active Directory environment, Kerberos authentication is always attempted first. Kerberos authentication is not available for [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] clients using named pipes.  
  
##  <a name="Permissions"></a> Permissions  
 When the [!INCLUDE[ssDE](../../includes/ssde-md.md)] service starts, it attempts to register the Service Principal Name (SPN). If the account starting SQL Server doesn't have permission to register a SPN in Active Directory Domain Services, this call will fail and a warning message will be logged in the Application event log as well as the SQL Server error log. To register the SPN, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] must be running under a built-in account, such as Local System (not recommended), or NETWORK SERVICE, or an account that has permission to register an SPN, such as a domain administrator account. When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running on the  [!INCLUDE[win7](../../includes/win7-md.md)] or  [!INCLUDE[winserver2008r2](../../includes/winserver2008r2-md.md)] operating system, you can run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using a virtual account or a managed service account (MSA). Both virtual accounts and MSA's can register an SPN. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not running under one of these accounts, the SPN is not registered at startup and the domain administrator must register the SPN manually.  
  
> [!NOTE]  
>  When the Windows domain is configured to run at less than the [!INCLUDE[winserver2008r2](../../includes/winserver2008r2-md.md)] Windows Server 2008 R2 functional level, then the Managed Service Account will not have the necessary permissions to register the SPNs for the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] service. If Kerberos authentication is required, the Domain Administrator should manually register the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] SPNs on the Managed Service Account.  
  
 The KB article, [How to use Kerberos authentication in SQL Server](https://support.microsoft.com/kb/319723), contains information about how to grant read or write permission to an SPN for an account that is not a Domain Administrator.  
  
 Additional information is available at [How to Implement Kerberos Constrained Delegation with SQL Server 2008](https://technet.microsoft.com/library/ee191523.aspx)  
  
##  <a name="Formats"></a> SPN Formats  
 Beginning with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], the SPN format is changed in order to support Kerberos authentication on TCP/IP, named pipes, and shared memory. The supported SPN formats for named and default instances are as follows.  
  
**Named instance**  
  
-   **MSSQLSvc/\<FQDN>:[\<port> | \<instancename>]**, where:  
  
    -   **MSSQLSvc** is the service that is being registered.  
  
    -   **\<FQDN>** is the fully qualified domain name of the server.  
  
    -   **\<port>** is the TCP port number.  
  
    -   **\<instancename>** is the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
**Default instance**  
  
-   **MSSQLSvc/\<FQDN>:\<port>** | **MSSQLSvc/\<FQDN>**, where:  
  
    -   **MSSQLSvc** is the service that is being registered.  
  
    -   **\<FQDN>** is the fully qualified domain name of the server.  
  
    -   **\<port>** is the TCP port number.  
  
    > [!NOTE]
    > The new SPN format does not require a port number. This means that a multiple-port server or a protocol that does not use port numbers can use Kerberos authentication.  
   
|||  
|-|-|  
|MSSQLSvc/\<FQDN>:<port>|The provider-generated, default SPN when TCP is used. \<port> is a TCP port number.|  
|MSSQLSvc/\<FQDN>|The provider-generated, default SPN for a default instance when a protocol other than TCP is used. \<FQDN> is a fully-qualified domain name.|  
|MSSQLSvc/\<FQDN>:\<instancename>|The provider-generated, default SPN for a named instance when a protocol other than TCP is used. \<instancename> is the name of an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  

> [!NOTE]  
> In the case of a TCP/IP connection, where the TCP port is included in the SPN, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must enable the TCP protocol for a user to connect by using Kerberos authentication. 

##  <a name="Auto"></a> Automatic SPN Registration  
 When an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] starts, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tries to register the SPN for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service. When the instance is stopped, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tries to unregister the SPN. For a TCP/IP connection the SPN is registered in the format *MSSQLSvc/\<FQDN>*:*\<tcpport>*.Both named instances and the default instance are registered as *MSSQLSvc*, relying on the *\<tcpport>* value to differentiate the instances.  
  
 For other connections that support Kerberos the SPN is registered in the format *MSSQLSvc/\<FQDN>*/*\<instancename>* for a named instance. The format for registering the default instance is *MSSQLSvc/\<FQDN>*.  
  
 Manual intervention might be required to register or unregister the SPN if the service account lacks the permissions that are required for these actions.  
  
##  <a name="Manual"></a> Manual SPN Registration  
To register the SPN manually, the administrator must use the Setspn.exe tool that is provided with the Microsoft [!INCLUDE[winxpsvr](../../includes/winxpsvr-md.md)] Support Tools. For more information, see the [Windows Server 2003 Service Pack 1 Support Tools](https://support.microsoft.com/kb/892777) KB article.  
  
Setspn.exe is a command line tool that enables you to read, modify, and delete the Service Principal Names (SPN) directory property. This tool also enables you to view the current SPNs, reset the account's default SPNs, and add or delete supplemental SPNs.  
  
The following example illustrates the syntax used to register manually register an SPN for a TCP/IP connection using a domain user account:  
  
```  
setspn -A MSSQLSvc/myhost.redmond.microsoft.com:1433 redmond\accountname  
```  
  
> [!NOTE]
> If an SPN already exists, it must be deleted before it can be reregistered. You do this by using the `setspn` command together with the `-D` switch. The following examples illustrate how to manually register a new instance-based SPN. For a default instance using a domain user account, use:  
  
```  
setspn -A MSSQLSvc/myhost.redmond.microsoft.com redmond\accountname  
```  
  
For a named instance, use:  
  
```  
setspn -A MSSQLSvc/myhost.redmond.microsoft.com/instancename redmond\accountname  
```  
  
##  <a name="Client"></a> Client Connections  
 User-specified SPNs are supported in client drivers. However, if an SPN is not provided, it will be generated automatically based on the type of a client connection. For a TCP connection, an SPN in the format *MSSQLSvc*/*FQDN*:[*port*] is used for both the named and default instances.  
  
For named pipes and shared memory connections, an SPN in the format *MSSQLSvc/\<FQDN>:\<instancename>* is used for a named instance and *MSSQLSvc/\<FQDN>* is used for the default instance.  
  
 **Using a service account as an SPN**  
  
Service accounts can be used as an SPN. They are specified through the connection attribute for the Kerberos authentication and take the following formats:  
  
-   **username@domain** or **domain\username** for a domain user account  
  
-   **machine$@domain** or **host\FQDN** for a computer domain account such as Local System or NETWORK SERVICES.  
  
To determine the authentication method of a connection, execute the following query.  
  
```sql  
SELECT net_transport, auth_scheme   
FROM sys.dm_exec_connections   
WHERE session_id = @@SPID;  
```  
  
##  <a name="Defaults"></a> Authentication Defaults  
 The following table describes the authentication defaults that are used based on SPN registration scenarios.  
  
|Scenario|Authentication method|  
|--------------|---------------------------|  
|The SPN maps to the correct domain account, virtual account, MSA, or built-in account. For example, Local System or NETWORK SERVICE.|Local connections use NTLM, remote connections use Kerberos.|  
|The SPN is the correct domain account, virtual account, MSA, or built-in account.|Local connections use NTLM, remote connections use Kerberos.|  
|The SPN maps to an incorrect domain account, virtual account, MSA, or built-in account|Authentication fails.|  
|The SPN lookup fails or does not map to a correct domain account, virtual account, MSA, or built-in account, or is not a correct domain account, virtual account, MSA, or built-in account.|Local and remote connections use NTLM.|  
  
> [!NOTE]  
> 'Correct' means that the account mapped by the registered SPN is the account that the SQL Server service is running under.  
  
##  <a name="Comments"></a> Comments  
 The Dedicated Administrator Connection (DAC) uses an instance name based SPN. Kerberos authentication can be used with a DAC if that SPN is registered successfully. As an alternative a user can specify the account name as an SPN.  
  
 If SPN registration fails during startup, this failure is recorded in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log, and startup continues.  
  
 If SPN de-registration fails during shutdown, this failure is recorded in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log, and shutdown continues.  
  
## See Also  
 [Service Principal Name &#40;SPN&#41; Support in Client Connections](../../relational-databases/native-client/features/service-principal-name-spn-support-in-client-connections.md)   
 [Service Principal Names &#40;SPNs&#41; in Client Connections &#40;OLE DB&#41;](../../relational-databases/native-client/ole-db/service-principal-names-spns-in-client-connections-ole-db.md)   
 [Service Principal Names &#40;SPNs&#41; in Client Connections &#40;ODBC&#41;](../../relational-databases/native-client/odbc/service-principal-names-spns-in-client-connections-odbc.md)   
 [SQL Server Native Client Features](../../relational-databases/native-client/features/sql-server-native-client-features.md)   
 [Manage Kerberos Authentication Issues in a Reporting Services Environment](https://technet.microsoft.com/library/ff679930.aspx)  
  
  

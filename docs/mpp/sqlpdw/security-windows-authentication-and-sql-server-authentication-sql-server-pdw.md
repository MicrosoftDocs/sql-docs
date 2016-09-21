---
title: "Security - Windows Authentication and SQL Server Authentication (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 414622f0-11a8-4b9c-85e1-35ccde851e7b
caps.latest.revision: 11
author: BarbKess
---
# Security - Windows Authentication and SQL Server Authentication (SQL Server PDW)
SQL Server PDW supports two methods of login authentication; SQL Server Authentication and Windows Authentication. The configuration where both SQL Server Authentication logins and Windows Authentication logins are accepted is known as *Mixed Mode Authentication*. SQL Server PDW only supports Mixed Mode Authentication, which means that neither SQL Server Authentication nor Windows Authentication can be disabled or turned off. However, you do not have to permit any SQL Server logins except the administrative **sa** login and the logins used internally by the SQL Server PDW components. And you do not have to create any Windows logins.  
  
This topic briefly defines SQL Server authentication, but primarily describes the configuration necessary to support Windows Authentication.  
  
## SQL Server Authentication  
SQL Server Authentication is an authentication method which does not rely upon Windows credentials. Logins are created in SQL Server PDW, and SQL Server PDW stores the login name, and a hash of the login password. When connecting using a SQL Server Authentication login, the client submits the login name and password to SQL Server PDW in an encrypted string, which PDW uses to authenticate the identity of the user. Separate SQL Server logins should be created for each person connecting to SQL Server PDW. Windows Authentication is preferred over SQL Server Authentication for many reasons. because SQL Server Authentication requires users to remember a second identity and password for PDW, and enter credentials at the time of connection, and because SQL Server Authentication does not support all of the security features, such as Kerberos.  
  
-   SQL Server Authentication requires users to remember a second identity and password for PDW, and enter credentials at the time of connection.  
  
-   SQL Server Authentication does not support all of the security features, such as Kerberos.  
  
-   SQL Server Authentication requires transmitting the login password over the network when connecting. The password is encrypted, but is an point of potential attack.  
  
-   By using Windows Authentication, Windows groups can be created at the domain level, and a login can be created on SQL Server for the entire group. Managing access from at the domain level can simplify account administration.  
  
For additional information about authentication modes, see [Choose an Authentication Mode](http://technet.microsoft.com/en-us/library/ms144284.aspx).  
  
The following tools do not yet support Windows Authentication and must be used with SQL Server Authentication logins.  
  
-   Informatica  
  
-   BOBJ connectors  
  
-   The internal PDW Appliance Verification (PAV) tool  
  
## Windows Authentication  
Windows Authentication (sometimes known as Integrated Authentication) lets you create logins that represent Windows users, or Windows groups. The authentication of the connecting Windows user is performed by the client operating system and domain when the client logs into the client computer. The connecting Windows users pass Windows tokens to SQL Server PDW which include both the authenticated Windows user identity and the security identifier (SID) for the Windows groups that the Windows user belongs to. SQL Server PDW does not have to verify the identity of the connecting user but does connect to the authenticating Active Directory to validate the tokens. SQL Server PDW accepts the identity from any Windows domain that is trusted by the SQL Server PDW appliance domain. SQL Server PDW must still confirm that the connecting user has a login, either for the Windows user account, or for a Windows group. If fully configured, Windows Authentication supports Kerberos authentication. If Kerberos authentication fails, the connection automatically attempts NTLM authentication. Kerberos authentication is preferred.  
  
Analytics Platform System contains a single appliance domain and this domain participates in user authentication. The appliance domain contains organizational units (OUs) for each region within the appliance. The PDW OU contains the virtual machines that are the logical level nodes of the PDW appliance: control (CTL) and compute nodes (CMP). For more information on the PDW appliance architecture, see [Understanding SQL Server PDW &#40;SQL Server PDW&#41;](../sqlpdw/understanding-sql-server-pdw-sql-server-pdw.md). The appliance active directory must be configured, as well as the corporate domain in order for customers to be able to use Windows Authentication with PDW.  
  
To connect to PDW with their domain accounts using Windows Authentication, the PDW appliance domain must trust the corporate domain where Windows users are being authenticated. A one-way forest trust is the minimal trust required for Kerberos to work with Windows Authentication. Users from other domains that are in a transitive trust with that corporate domain will also be able to connect to the PDW appliance. There are other configurations that can enable Integrated Authentication, such as 2-way forest or external trust for Kerberos, 1-way external trust, or account mirroring for NTLM that are not discussed in this topic.  
  
For Kerberos to work, an appropriate Service Principal Name (SPN) must be registered on the APS appliance domain controller. In PDW we provide this automatically. During setup PDW registers a default SPN in the appliance Active Directory Domain Services based on the internal name of the CTL node and port 17001. Customers can use this SPN by creating a forward lookup zone (FLZ) in their corporate DNS that uses internal PDW names, and also adding the appropriate **_ldap** and **_kerberos** SRV records. To use some other, more meaningful names for accessing PDW nodes, aliases can be created. For Kerberos to work, PDW must be accessed by the DNS name that corresponds to a registered SPN, or by an alias that points to such a name. If an IP address is used, fallback to NTLM authentication may occur.  
  
When a Windows authentication connection is initiated, the system will first try to use Kerberos as a more secure protocol. If Kerberos cannot be used, authentication will fall back to NTLM. If NTLM cannot be used either (e.g. because there is no trust established between the domains), then Windows authentication will not be possible. The principal reasons Kerberos could fail, are:  
  
-   Because a 1-way external domain trust was established between domains rather than a forest trust.  
  
-   Because the required SRV records were not properly added to the corporate DNS.  
  
-   Because an IP address was used to connect rather than the DNS name that corresponds to registered SPN.  
  
For step-by-step instructions to configure domains for Windows Authentication, see [Security - Configure Domain Trusts &#40;SQL Server PDW&#41;](../sqlpdw/security-configure-domain-trusts-sql-server-pdw.md).  
  

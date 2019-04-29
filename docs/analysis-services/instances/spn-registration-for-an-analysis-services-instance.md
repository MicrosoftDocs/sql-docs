---
title: "SPN registration for an Analysis Services instance | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom:
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# SPN registration for an Analysis Services instance
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  A Service Principle Name (SPN) uniquely identifies a service instance in an Active Directory domain when Kerberos is used to mutually authenticate client and service identities. An SPN is associated with the logon account under which the service instance runs.  
  
 For client applications connecting to Analysis Services via Kerberos authentication, the Analysis Services client libraries construct an SPN using the host name from the connection string and other well-known variables, such as the service class, that are fixed in any given release of Analysis Services.  
  
 For mutual authentication to occur, the SPNs constructed by the client must match a corresponding SPN object on an Active Directory Domain Controller (DC). This means that you might need to register multiple SPNs for a single Analysis Services instance to cover all of the ways in which a user might specify the host name on a connection string. For example, you probably need two SPNs to handle both the fully-qualified domain name (FQDN) of a server, as well as the short computer name. Correctly registering the Analysis Services SPN is essential for a successful connection. If the SPN is non-existent, malformed, or duplicated, the connection will fail.  
  
 SPN registration is a manual task performed by the Analysis Services administrator. Unlike the SQL Server database engine, Analysis Services never auto-registers its SPN at service startup. Manual registration is required when Analysis Services runs under the default virtual account, a domain user account, or a built-in account, including a per-service SID.  
  
 SPN registration is not required if the service runs under a predefined managed service account created by a domain administrator. Note that depending on the functional level of your domain, registering an SPN can require domain administrator permissions.  
  
> [!TIP]  
>  **[!INCLUDE[msCoName](../../includes/msconame-md.md)] Kerberos Configuration Manager for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]** is a diagnostic tool that helps troubleshoot Kerberos related connectivity issues with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Microsoft Kerberos Configuration Manager for SQL Server](http://www.microsoft.com/download/details.aspx?id=39046).  
  
 This topic contains the following sections:  
  
 [When SPN registration is required](#bkmk_scnearios)  
  
 [SPN format for Analysis Services](#bkmk_SPNSyntax)  
  
 [SPN registration for a virtual account](#bkmk_virtual)  
  
 [SPN registration for a domain account](#bkmk_domain)  
  
 [SPN registration for a built-in account](#bkmk_builtin)  
  
 [SPN registration for a named instance](#bkmk_spnNamed)  
  
 [SPN registration for an SSAS cluster](#bkmk_spnCluster)  
  
 [SPN registration for SSAS instances configured for HTTP access](#bkmk_spnHTTP)  
  
 [SPN registration for SSAS instances listening on fixed ports](#bkmk_spnFixedPorts)  
  
##  <a name="bkmk_scnearios"></a> When SPN registration is required  
 Any client connection that specifies "SSPI=Kerberos" on the connection string will introduce SPN registration requirements for an Analysis Services instance.  
  
 SPN registration is required under the following circumstances. For more detailed information, see [Configure Analysis Services for Kerberos constrained delegation](../../analysis-services/instances/configure-analysis-services-for-kerberos-constrained-delegation.md).  
  
-   Identity delegation is necessary to flow the user identity from the client application or middle-tier service to Analysis Services. Identity delegation is typically used when per-user permissions or filters are defined on specific objects.  
  
     A common scenario involving identity delegation is configuring middle-tier services, such as Excel Services or Reporting Services, for constrained delegation for the purpose of impersonating a user identity when retrieving data in Analysis Services. To support this behavior, you must provide an Analysis Services SPN as the destination service when configuring Excel Services or Reporting Services for constrained delegation.  
  
-   Analysis Services delegates a user identity when retrieving data from a SQL Server relational database for tabular databases using DirectQuery mode. This is the only scenario in which Analysis Services will delegate the user identity to another service.  
  
##  <a name="bkmk_SPNSyntax"></a> SPN format for Analysis Services  
 Use **setspn** to register an SPN. On newer operating systems, **setspn** is installed as a system utility. For more information, see [SetSPN](http://technet.microsoft.com/library/cc731241\(WS.10\).aspx).  
  
 The following table describes each part of an Analysis Services SPN.  
  
|Element|Description|  
|-------------|-----------------|  
|Service class|MSOLAPSvc.3 identifies the service as an Analysis Services instance. The .3 is a reference to the version of the XMLA-over-TCP/IP protocol used in Analysis Services transmissions. It is unrelated to product release. As such, MSOLAPSvc.3 is the correct service class for SQL Server 2005, 2008, 2008 R2, 2012 and any future release of Analysis Services until the protocol itself is revised.|  
|Host-name|Identifies the computer on which the service is running. This can be a fully-qualified domain name or a NetBIOS name. You should register an SPN for both.<br /><br /> When registering an SPN for the NetBIOS name of a server, be sure to use `SetupSPN -S` to check for duplicate registration. NetBIOS names are not guaranteed to be unique in a forest, and having a duplicate SPN registration will cause the connection to fail.<br /><br /> For Analysis Services load balanced clusters, the host name should be the virtual name assigned to the cluster.<br /><br /> Never create an SPN using the IP address. Kerberos uses the DNS resolution capabilities of the domain. Specifying an IP address bypasses that capability.|  
|Port-number|Although the port number is part of SPN syntax, you never specify a port number when registering an Analysis Services SPN. The colon ( : ) character, typically used to provide a port number in standard SPN syntax, is used by Analysis Services to specify the instance name. For an Analysis Services instance, the port is assumed to be the default port (TCP 2383) or a port assigned by the SQL Server Browser service (TCP 2382).|  
|Instance-name|Analysis Services is a replicable service that can be installed multiple times on the same computer. Each instance is identified through its instance name.<br /><br /> The instance name is prefixed with a colon ( : ) character. For example, given a host computer named SRV01 and a named instance of SSAS-Tabular, the SPN should be SRV01:SSAS-Tabular.<br /><br /> Note that the syntax for specifying a named Analysis Services instance differs from that used by other SQL Server instances. Other services uses a backslash ( \ ) to append the instance name in an SPN.|  
|Service-account|This is the startup account of the **MSSQLServerOLAPService** Windows service. It can be a Windows domain user account, virtual account, managed service account (MSA) or a built-in account such as a per-service SID, NetworkService, or LocalSystem. A Windows domain user account can be formatted as domain\user or user@domain.|  
  
##  <a name="bkmk_virtual"></a> SPN registration for a virtual account  
 Virtual accounts are the default account type for SQL Server services. The virtual account is **NT Service\MSOLAPService** for a default instance and **NT Service\MSOLAP$**\<instance-name> for a named instance.  
  
 As the name implies, these accounts do not exist in Active Directory. A virtual account exists only on the local computer. When connecting to external services, applications, or devices, the connection is made using the local machine account. For this reason, an SPN registration for Analysis Services running under a virtual account is actually an SPN registration for the machine account.  
  
 **Example syntax for a default instance running as NT Service\MSOLAPService**  
  
 This example shows **setspn** syntax for Analysis Services default instance running under the default virtual account. In this example, the computer host name is **AW-SRV01**. As noted, SPN registration must specify the *machine account* instead of the virtual account, **NT Service\MSOLAPService**.  
  
```  
Setspn -s MSOLAPSvc.3/AW-SRV01.AdventureWorks.com AW-SRV01  
```  
  
> [!NOTE]  
>  Remember to create two SPN registrations, one for the NetBIOS host name and a second for a fully-qualified domain name of the host. Different client applications use different host name conventions when connecting to Analysis Services. Having two SPN registrations ensures that both versions of the host name are accounted for.  
  
 **Example syntax for a named instance running as NT Service\MSOLAP$\<instance-name>**  
  
 This example shows **setspn** syntax for a named instance running under the default virtual account. In this example, the computer host name is **AW-SRV02**, and the instance name is **AW-FINANCE**. Again, it is the machine account that is specified for the SPN, rather than the virtual account **NT Service\MSOLAP$**\<instance-name>.  
  
```  
Setspn -s MSOLAPSvc.3/AW-SRV02.AdventureWorks.com:AW-FINANCE AW-SRV02  
```  
  
##  <a name="bkmk_domain"></a> SPN registration for a domain account  
 Using a domain account to run an Analysis Services instance is a common practice.  
  
 For Analysis Services instances that run in a network or hardware load balanced cluster, a domain account is required, with each instance in the cluster running under the same domain account.  
  
 **Example syntax for a default instance running as a domain user**  
  
 This example shows **setspn** syntax for Analysis Services default instance running under a domain user account, **SSAS-Service**, in the AdventureWorks domain.  
  
```  
Setspn -s msolapsvc.3/AW-SRV01.Adventureworks.com AdventureWorks\SSAS-Service  
```  
  
> [!TIP]  
>  Verify whether the SPN was created for the Analysis Services server by running `Setspn -L <domain account>` or `Setspn -L <machinename>`, depending on how the SPN was registered. You should see MSOLAPSVC.3/\<hostname> in the list.  
  
##  <a name="bkmk_builtin"></a> SPN registration for a built-in account  
 Although this practice is not recommended, older Analysis Services installations are sometimes configured to run under built-in accounts like Network Service, Local Service, or Local System.  
  
 **Example syntax for a default instance running under a built-in account**  
  
 SPN registration for a service running under a built-in account or per-service SID is equivalent to the SPN syntax used for the virtual account. Instead of the account name, use the machine account:  
  
```  
Setspn -s MSOLAPSvc.3/AW-SRV01.AdventureWorks.com AW-SRV01  
```  
  
##  <a name="bkmk_spnNamed"></a> SPN registration for a named instance  
 Named instances of Analysis Services use dynamic port assignments that are detected by the SQL Server Browser service. When using a named instance, register an SPN for both the SQL Server Browser Service and the Analysis Services named instance. For more information, see [An SPN for the SQL Server Browser service is required when you establish a connection to a named instance of SQL Server Analysis Services or of SQL Server](http://support.microsoft.com/kb/950599).  
  
 **Example of SPN syntax for the SQL Browser Service running as LocalService**  
  
 The service class is **MSOLAPDisco.3**. By default, this service runs as NT AUTHORITY\LocalService, which means SPN registration is set for the machine account. In this example, the machine account is **AW-SRV01**, corresponding to the computer name.  
  
```  
Setspn -S MSOLAPDisco.3/AW-SRV01.AdventureWorks.com AW-SRV01  
```  
  
##  <a name="bkmk_spnCluster"></a> SPN registration for an SSAS cluster  
 For Analysis Services failover clusters, the host name should be the virtual name assigned to the cluster. This is the SQL Server network name, specified during SQL Server Setup when you installed Analysis Services on top of an existing WSFC. You can find this name in Active Directory. You can also find it in **Failover Cluster Manager** | **Role** | **Resources** tab. The server name on the Resources tab is what should be used as the 'virtual name' in the SPN command.  
  
 **SPN syntax for an Analysis Services cluster**  
  
```  
Setspn -s msolapsvc.3/<virtualname.FQDN > <domain user account>  
```  
  
 Recall that nodes in an Analysis Services cluster are required to use the default port (TCP 2383) and run under the same domain user account so that each node has the same SID. See [How to Cluster SQL Server Analysis Services](http://msdn.microsoft.com/library/dn736073.aspx) for more information.  
  
##  <a name="bkmk_spnHTTP"></a> SPN registration for SSAS instances configured for HTTP access  
 Depending on solution requirements, you might have configured Analysis Services for HTTP access. If your solution includes IIS as a middle tier component, and Kerberos authentication is a solution requirement, you might need to manually register an SPN for IIS. For more information, see "Configure the settings on the computer running IIS" in [How to configure SQL Server 2008 Analysis Services and SQL Server 2005 Analysis Services to use Kerberos authentication](http://support.microsoft.com/kb/917409).  
  
 In terms of SPN registration for the Analysis Services instance, there is no difference between an instance configured for TCP or HTTP. The connection to Analysis Services from IIS, using the MSMDPUMP ISAPI extension, is always TCP.  
  
 This means that you can use the instructions from previous sections for default or named instance to register the SPN. When specifying the host name, be sure to use the host name you specified in the msmdpump.ini file when you configured the service for HTTP access.  
  
 For more information about HTTP access, see [Configure HTTP Access to Analysis Services on Internet Information Services &#40;IIS&#41; 8.0](../../analysis-services/instances/configure-http-access-to-analysis-services-on-iis-8-0.md).  
  
##  <a name="bkmk_spnFixedPorts"></a> SPN registration for SSAS instances listening on fixed ports  
 You cannot specify a port number on an Analysis Services SPN registration. If you installed Analysis Services as the default instance and configured it to listen on a fixed port, you must now configure it to listen on the default port (TCP 2383). For named instances, you need to use SQL Server Browser service and dynamic port assignments.  
  
 An Analysis Services instance can only listen on a single port. Using multiple ports is not supported. For more information about port configuration, see [Configure the Windows Firewall to Allow Analysis Services Access](../../analysis-services/instances/configure-the-windows-firewall-to-allow-analysis-services-access.md).  
  
## See Also  
 [Microsoft BI Authentication and Identity Delegation](http://go.microsoft.com/fwlink/?LinkID=286576)   
 [Mutual Authentication Using Kerberos](http://go.microsoft.com/fwlink/?LinkId=299283)   
 [How to configure SQL Server 2008 Analysis Services and SQL Server 2005 Analysis Services to use Kerberos authentication](http://support.microsoft.com/kb/917409)   
 [Service Principal Names (SPNs) SetSPN Syntax (Setspn.exe)](http://social.technet.microsoft.com/wiki/contents/articles/717.service-principal-names-spns-setspn-syntax-setspn-exe.aspx)   
 [What SPN do I use and how does it get there?](http://social.technet.microsoft.com/wiki/contents/articles/717.service-principal-names-spns-setspn-syntax-setspn-exe.aspx)   
 [SetSPN](http://technet.microsoft.com/library/cc731241\(WS.10\).aspx)   
 [Service Accounts Step-by-Step Guide](http://technet.microsoft.com/library/dd548356\(WS.10\).aspx)   
 [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)   
 [How to use SPNs when you configure Web applications that are hosted on Internet Information Services](http://support.microsoft.com/kb/929650)   
 [what's new in service accounts](http://technet.microsoft.com/library/dd367859\(WS.10\).aspx)   
 [Configure Kerberos authentication for SharePoint 2010 Products (white paper)](http://technet.microsoft.com/library/ff829837.aspx)  
  
  

---
title: "Configure the Windows Firewall to Allow Analysis Services Access | Microsoft Docs"
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
# Configure the Windows Firewall to Allow Analysis Services Access
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  An essential first step in making [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] or [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] available on the network is to determine whether you need to unblock ports in a firewall. Most installations will require that you create at least one in-bound firewall rule that allows connections to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
 Firewall configuration requirements vary depending on how you installed [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]:  
  
-   Open TCP port 2383 when installing a default instance or creating an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] failover cluster.  
  
-   Open TCP port 2382 when installing a named instance. Named instances use dynamic port assignments. As the discovery service for Analysis Services, SQL Server Browser service listens on TCP port 2382 and redirects the connection request to the port currently used by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
-   Open TCP port 2382 when installing [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] in SharePoint mode to support [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] 2013. In [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] 2013, the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance is external to SharePoint. Inbound requests to the named '[!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]' instance originate from SharePoint web applications over a network connection, requiring an open port. As with other [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] named instances, create an inbound rule for SQL Server Browser service on TCP 2382 to allow access to [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)].  
  
-   For [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] 2010, do not open ports in Windows Firewall. As an add-in to SharePoint, the service uses ports configured for SharePoint and makes only local connections to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance that loads and queries [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data models.  
  
-   For [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instances running on Windows Azure Virtual Machines, use alternate instructions for configuring server access. See [SQL Server Business Intelligence in Windows Azure Virtual Machines](http://msdn.microsoft.com/library/windowsazure/jj992719.aspx).  
  
 Although the default instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] listens on TCP port 2383, you can configure the server to listen on a different fixed port, connecting to the server in this format: \<servername>:\<portnumber>.  
  
 Only one TCP port can be used by an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance. On computers having multiple network cards or multiple IP addresses, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] listens on one TCP port for all IP addresses assigned or aliased to the computer. If you have specific multi-port requirements, consider configuring [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] for HTTP access. You can then set up multiple HTTP endpoints on whatever ports you choose. See [Configure HTTP Access to Analysis Services on Internet Information Services &#40;IIS&#41; 8.0](../../analysis-services/instances/configure-http-access-to-analysis-services-on-iis-8-0.md).  
  
 This topic contains the following sections:  
  
-   [Check port and firewall settings for Analysis Services](#bkmk_checkport)  
  
-   [Configure Windows Firewall for a default instance of Analysis Services](#bkmk_default)  
  
-   [Configure Windows Firewall access for a named instance of Analysis Services](#bkmk_named)  
  
-   [Port configuration for an Analysis Services cluster](#bkmk_cluster)  
  
-   [Port configuration for Power Pivot for SharePoint](#bkmk_powerpivot)  
  
-   [Use a fixed port for a default or named instance of Analysis Services](#bkmk_fixed)  
  
 For more information about the default Windows firewall settings, and a description of the TCP ports that affect the Database Engine, Analysis Services, Reporting Services, and Integration Services, see [Configure the Windows Firewall to Allow SQL Server Access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md).  
  
##  <a name="bkmk_checkport"></a> Check port and firewall settings for Analysis Services  
 On the Microsoft Windows operating systems that are supported by [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], Windows Firewall is on by default and is blocking remote connections. You must manually open a port in the firewall to allow inbound requests to Analysis Services. SQL Server Setup does not perform this step for you.  
  
 Port settings are specified in the msmdsrv.ini file and in the General properties page of an Analysis Services instance in SQL Server Management Studio. If **Port** is set to a positive integer, the service is listening on a fixed port. If **Port** is set to 0, the service is listening on port 2383 if it is the default instance or on a dynamically assigned port if it is a named instance.  
  
 Dynamic port assignments are only used by named instances. The **MSOLAP$InstanceName** service determines which port to use when it starts up. You can determine the actual port number in use by a named instance by doing the following:  
  
-   Start Task Manager and then click **Services** to get the PID of the **MSOLAP$InstanceName**.  
  
-   Run **netstat -ao -p TCP** from the command line to view the TCP port information for that PID.  
  
-   Verify the port by using SQL Server Management Studio and connect to an Analysis Services server in this format: \<IPAddress>:\<portnumber>.  
  
 Although an application might be listening on a specific port, connections will not succeed if a firewall is blocking access. In order for connections to reach a named Analysis Services instance, you must unblock access to either msmdsrv.exe or the fixed port on which it is listening in the firewall. The remaining sections in this topic provide instructions for doing so.  
  
 To check whether firewall settings are already defined for Analysis Services, use Windows Firewall with Advanced Security in Control Panel. The Firewall page in the Monitoring folder shows a complete list of the rules defined for the local server.  
  
 Note that for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], all firewall rules must be manually defined. Although [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] and SQL Server Browser reserve ports 2382 and 2383, neither the SQL Server setup program nor any of the configuration tools define firewall rules that allow access to either the ports or the program executable files.  
  
##  <a name="bkmk_default"></a> Configure Windows Firewall for a default instance of Analysis Services  
 The default instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] listens on TCP port 2383. If you installed the default instance and want to use this port, you only need to unblock inbound access to TCP port 2383 in Windows Firewall to enable remote access to the default instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. If you installed the default instance but want to configure the service to listen on a fixed port, see [Use a fixed port for a default or named instance of Analysis Services](#bkmk_fixed) in this topic.  
  
 To verify whether the service is running as the default instance (MSSQLServerOLAPService), check the service name in SQL Server Configuration Manager. A default instance of Analysis Services is always listed as **SQL Server Analysis Services (MSSQLSERVER)**.  
  
> [!NOTE]  
>  Different Windows operating systems provide alternative tools for configuring Windows Firewall. Most of these tools let you choose between opening a specific port or program executable. Unless you have a reason for specifying the program executable, we recommend that you specify the port.  
  
 When specifying an inbound rule, be sure to adopt a naming convention that allows you to easily find the rules later (for example, **SQL Server Analysis Services (TCP-in) 2383**).  
  
#### Windows Firewall with Advanced Security  
  
1.  On Windows 7 or Windows Vista, in Control Panel, click **System and Security**, select **Windows Firewall**, and then click **Advanced settings**. On Windows Server 2008 or 2008 R2, open Administrator Tools and click **Windows Firewall with Advanced Security**. On Windows Server 2012, open the Applications page and type **Windows Firewall**.  
  
2.  Right-click **Inbound Rules** and select **New Rule**.  
  
3.  In Rule Type, click **Port** and then click **Next**.  
  
4.  In Protocol and Ports, select **TCP** and then type **2383** in **Specific local ports**.  
  
5.  In Action, click **Allow the connection** and then click **Next**.  
  
6.  In Profile, clear any network locations that do not apply and then click **Next**.  
  
7.  In Name, type a descriptive name for this rule (for example, **SQL Server Analysis Services (tcp-in) 2383**), and then click **Finish**.  
  
8.  To verify that remote connections are enabled, open SQL Server Management Studio or Excel on a different computer and connect to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] by specifying the network name of the server in **Server name**.  
  
    > [!NOTE]  
    >  Other users will not have access to this server until you grant permissions. For more information, see [Authorizing access to objects and operations &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/authorizing-access-to-objects-and-operations-analysis-services.md).  
  
#### Netsh AdvFirewall Syntax  
  
-   The following command creates an inbound rule that allows incoming requests on TCP port 2383.  
  
    ```  
    netsh advfirewall firewall add rule name="SQL Server Analysis Services inbound on TCP 2383" dir=in action=allow protocol=TCP localport=2383 profile=domain  
    ```  
  
##  <a name="bkmk_named"></a> Configure Windows Firewall access for a named instance of Analysis Services  
 Named instances of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] can either listen on a fixed port or on a dynamically assigned port, where SQL Server Browser service provides the connection information that is current for the service at the time of the connection.  
  
 SQL Server Browser service listens on TCP port 2382. UDP is not used. TCP is the only transmission protocol used by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
 Choose one of the following approaches to enable remote access to a named instance of Analysis Services:  
  
-   Use dynamic port assignments and SQL Server Browser service. Unblock the port used by SQL Server Browser service in Windows Firewall. Connect to the server in this format: \<servername>\\<instancename\>.  
  
-   Use a fixed port and SQL Server Browser service together. This approach lets you connect using this format: \<servername>\\<instancename\>, identical to the dynamic port assignment approach, except that in this case the server listens on a fixed port. In this scenario, SQL Server Browser Service provides name resolution to the Analysis Services instance listening on the fixed port. To use this approach, configure the server to listen on a fixed port, unblock access to that port, and unblock access to the port used by SQL Server Browser service.  
  
 SQL Server Browser service is only used with named instances, never with the default instance. The service is automatically installed and enabled whenever you install any SQL Server feature as a named instance. If you choose an approach that requires SQL Server Browser service, be sure it remains enabled and started on your server.  
  
 If you cannot use SQL Server Browser service, you must assign a fixed port in the connection string, bypassing domain name resolution. Without SQL Server Browser service, all client connections must include the port number on the connection string (for example, AW-SRV01:54321).  
  
 **Option 1: Use dynamic port assignments and unblock access to SQL Server Browser service**  
  
 Dynamic port assignments for named instances of Analysis Services are established by the **MSOLAP$InstanceName** when the service starts. By default, the service claims the first available port number that it finds, using a different port number each time the service is restarted.  
  
 Instance name resolution is handled by the SQL Server browser service. Unblocking TCP port 2382 for SQL Server Browser service is always required if you are using dynamic port assignments with a named instance.  
  
> [!NOTE]  
>  SQL Server Browser service listens on both UDP port 1434 and TCP port 2382 for the Database Engine and Analysis Services, respectively. Even if you already unblocked UDP port 1434 for the SQL Server Browser service, you must still unblock TCP port 2382 for Analysis Services.  
  
#### Windows Firewall with Advanced Security  
  
1.  On Windows 7 or Windows Vista, in Control Panel, click **System and Security**, select **Windows Firewall**, and then click **Advanced settings**. On Windows Server 2008 or 2008 R2, open Administrator Tools and click **Windows Firewall with Advanced Security**. On Windows Server 2012, open the Applications page and type **Windows Firewall**.  
  
2.  To unblock access to SQL Server Browser service, right-click **Inbound Rules** and select **New Rule**.  
  
3.  In Rule Type, click **Port** and then click **Next**.  
  
4.  In Protocol and Ports, select **TCP** and then type **2382** in **Specific local ports**.  
  
5.  In Action, click **Allow the connection** and then click **Next**.  
  
6.  In Profile, clear any network locations that do not apply and then click **Next**.  
  
7.  In Name, type a descriptive name for this rule (for example, **SQL Server Browser Service (tcp-in) 2382**), and then click **Finish**.  
  
8.  To verify that remote connections are enabled, open SQL Server Management Studio or Excel on a different computer and connect to the Analysis Services by specifying the network name of the server and the instance name in this format: \<servername>\\<instancename\>. For example, on a server named **AW-SRV01** with a named instance of **Finance**, the server name is **AW-SRV01\Finance**.  
  
 **Option 2: Use a fixed port for a named instance**  
  
 Alternatively, you can assign a fixed port, and then unblock access to that port. This approach offers better auditing capability than if you allowed access to the program executable. For this reason, using a fixed port is the recommended approach for accessing any Analysis Services instance.  
  
 To assign a fixed port, follow the instructions in [Use a fixed port for a default or named instance of Analysis Services](#bkmk_fixed) in this topic, then return to this section to unblock the port.  
  
#### Windows Firewall with Advanced Security  
  
1.  On Windows 7 or Windows Vista, in Control Panel, click **System and Security**, select **Windows Firewall**, and then click **Advanced settings**. On Windows Server 2008 or 2008 R2, open Administrator Tools and click **Windows Firewall with Advanced Security**. On Windows Server 2012, open the Applications page and type **Windows Firewall**.  
  
2.  To unblock access to Analysis Services, right-click **Inbound Rules** and select **New Rule**.  
  
3.  In Rule Type, click **Port** and then click **Next**.  
  
4.  In Protocol and Ports, select **TCP** and then type the fixed port in **Specific local ports**.  
  
5.  In Action, click **Allow the connection** and then click **Next**.  
  
6.  In Profile, clear any network locations that do not apply and then click **Next**.  
  
7.  In Name, type a descriptive name for this rule (for example, **SQL Server Analysis Services on port 54321**), and then click **Finish**.  
  
8.  To verify that remote connections are enabled, open SQL Server Management Studio or Excel on a different computer and connect to the Analysis Services by specifying the network name of the server and the port number in this format: \<servername>:\<portnumber>.  
  
#### Netsh AdvFirewall Syntax  
  
-   The following commands create inbound rules that unblock TCP 2382 for SQL Server Browser service and unblock the fixed port that you specified for the Analysis Services instance. You can run either one to allow access to a named Analysis Services instance.  
  
     In this sample command, port 54321 is the fixed port. Be sure to replace it with the actual port in use on your system.  
  
    ```  
    netsh advfirewall firewall add rule name="SQL Server Analysis Services (tcp-in) on 54321" dir=in action=allow protocol=TCP localport=54321 profile=domain  
    ```  
  
    ```  
    netsh advfirewall firewall add rule name="SQL Server Browser Services inbound on TCP 2382" dir=in action=allow protocol=TCP localport=2382 profile=domain  
    ```  
  
##  <a name="bkmk_fixed"></a> Use a fixed port for a default or named instance of Analysis Services  
 This section explains how to configure Analysis Services to listen on a fixed port. Using a fixed port is common if you installed Analysis Services as a named instance, but you can also use this approach if business or security requirements specify that you use non-default port assignments.  
  
 Note that using a fixed port will alter the connection syntax for the default instance by requiring you to append the port number to the server name. For example, connecting to a local, default Analysis Services instance listening on port 54321 in SQL Server Management Studio would require that you type localhost:54321 as the server name in the Connect to Server dialog box in Management Studio.  
  
 If you are using a named instance, you can assign a fixed port with no changes to how you specify the server name (specifically, you can use \<servername\instancename> to connect to a named instance listening on a fixed port). This works only if SQL Server Browser service is running and you unblocked the port on which it is listening. SQL Server Browser service will provide redirection to the fixed port based on \<servername\instancename>. As long as you open ports for both SQL Server Browser service and the named instance of Analysis Services listening on the fixed port, SQL Server Browser service will resolve the connection to a named instance.  
  
1.  Determine an available TCP/IP port to use.  
  
     To view a list of reserved and registered ports that you should avoid using, see [Port Numbers (IANA)](http://go.microsoft.com/fwlink/?LinkID=198469). To view a list of ports that are already in use on your system, open a command prompt window and type **netstat -a -p TCP** to display a list of the TCP ports that are open on the system.  
  
2.  After you determine which port to use, specify the port by either editing the **Port** configuration setting in the msmdsrv.ini file or in the General properties page of an Analysis Services instance in SQL Server Management Studio.  
  
3.  Restart the service.  
  
4.  Configure Windows Firewall to unblock the TCP port you specified. Or, if you are using a fixed port for a named instance, unblock both the TCP port you specified for that instance and TCP port 2382 for SQL Server Browser service.  
  
5.  Verify by connecting locally (in Management Studio) and then remotely from a client application on another computer. To use Management Studio, connect to an Analysis Services default instance by specifying a server name in this format: \<servername>:\<portnumber>. For a named instance, specify the server name as \<servername>\\<instancename\>.  
  
##  <a name="bkmk_cluster"></a> Port configuration for an Analysis Services cluster  
 An [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] failover cluster always listens on TCP port 2383, regardless of whether you installed it as a default instance or named instance. Dynamic port assignments are not used by [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] when it is installed on a Windows failover cluster. Be sure to open TCP 2383 on every node running [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] in the cluster. For more information about clustering [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], see [How to Cluster SQL Server Analysis Services](http://go.microsoft.com/fwlink/p/?LinkId=396548).  
  
##  <a name="bkmk_powerpivot"></a> Port configuration for Power Pivot for SharePoint  
 Server architecture for [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] is fundamentally different depending on which version of SharePoint you are using.  
  
 **SharePoint 2013**  
  
 In SharePoint 2013, Excel Services redirects requests for Power Pivot data models, which are subsequently loaded on an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance outside of the SharePoint environment. Connections follow the typical pattern, where an Analysis Services client library on a local computer sends a connection request to a remote [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance in the same network.  
  
 Because [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] always installs [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] as a named instance, you should assume SQL Server Browser service and dynamic port assignments. As noted earlier, SQL Server Browser service listens on TCP port 2382 for connection requests sent to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] named instances, redirecting the request to the current port.  
  
 Note that Excel Services in SharePoint 2013 does not support the fixed port connection syntax, so make sure SQL Server Browser service is accessible.  
  
 **SharePoint 2010**  
  
 If you are using SharePoint 2010, you do not need to open ports in Windows Firewall. SharePoint opens the ports that it requires, and add-ins such as [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint operate within the SharePoint environment. In a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint 2010 installation, the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service has exclusive use of the local SQL Server Analysis Services ( [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]) service instance that is installed with it on the same computer. It uses local connections, not network connections, to access the local Analysis Services engine service that loads, queries, and processes [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data on the SharePoint server. To request [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data from client applications, requests are routed through ports that are opened by SharePoint Setup (specifically, inbound rules are defined to allow access to SharePoint - 80, SharePoint Central Administration v4, SharePoint Web Services, and SPUserCodeV4). Because [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] web services run within a SharePoint farm, the SharePoint firewall rules are sufficient for remote access to [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data in a SharePoint farm.  
  
## See Also  
 [SQL Server Browser Service &#40;Database Engine and SSAS&#41;](../../database-engine/configure-windows/sql-server-browser-service-database-engine-and-ssas.md)   
 [Start, Stop, Pause, Resume, Restart the Database Engine, SQL Server Agent, or SQL Server Browser Service](../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md)   
 [Configure a Windows Firewall for Database Engine Access](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md)  
  
  

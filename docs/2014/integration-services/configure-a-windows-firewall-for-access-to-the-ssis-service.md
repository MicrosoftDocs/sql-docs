---
title: "Configure a Windows Firewall for Access to the SSIS Service | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "security [Integration Services], firewalls"
  - "Windows Firewall [Integration Services]"
  - "unauthorized access [Integration Services]"
  - "Integration Services service, firewalls"
  - "firewall systems [Integration Services]"
  - "SQL Server Integration Services, firewalls"
  - "SSIS, firewalls"
ms.assetid: 39975cf2-c351-4205-8c39-27a0fadfb010
author: janinezhang
ms.author: janinez
manager: craigg
---
# Configure a Windows Firewall for Access to the SSIS Service
    
> [!IMPORTANT]  
>  This topic discusses the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service, a Windows service for managing [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages. [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] supports the service for backward compatibility with earlier releases of [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]. Starting in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], you can manage objects such as packages on the Integration Services server.  
  
 The windowsfirewall system helps prevent unauthorized access to computer resources over a network connection. To access [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] through this firewall, you have to configure the firewall to enable access.  
  
> [!IMPORTANT]  
>  To manage packages that are stored on a remote server, you do not have to connect to the instance of the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service on that remote server. Instead, edit the configuration file for the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service so that [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] displays the packages that are stored on the remote server. For more information, see [Configuring the Integration Services Service &#40;SSIS Service&#41;](configuring-the-integration-services-service-ssis-service.md).  
  
 The [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service uses the DCOM protocol. For more information about how the DCOM protocol works through firewalls, see the article, "[Using Distributed COM with Firewalls](https://go.microsoft.com/fwlink/?LinkId=12490)," in the MSDN Library.  
  
 There are many firewall systems available. If you are running a firewall other than windowsfirewall, see your firewall documentation for information that is specific to the system you are using.  
  
 If the firewall supports application-level filtering, you can use the user interface that Windows provides to specify the exceptions that are allowed through the firewall, such as programs and services. Otherwise, you have to configure DCOM to use a limited set of TCP ports. The Microsoft website link previously provided includes information about how to specify the TCP ports to use.  
  
 The Integration Services service uses port 135, and the port cannot be changed. You have to open TCP port 135 for access to the service control manager (SCM). SCM performs tasks such as starting and stopping [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] services and transmitting control requests to the running service.  
  
 The information in the following section is specific to windowsfirewall. You can configure the windowsfirewall system by running a command at the command prompt, or by setting properties in the windowsfirewall dialog box.  
  
 For more information about the default windowsfirewall settings, and a description of the TCP ports that affect the Database Engine, Analysis Services, Reporting Services, and Integration Services, see [Configure the Windows Firewall to Allow SQL Server Access](../../2014/sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md).  
  
## Configuring a windowsfirewall  
 You can use the following commands to open TCP port 135, add MsDtsSrvr.exe to the exception list, and specify the scope of unblocking for the firewall.  
  
#### To configure a windowsfirewall using the Command Prompt window  
  
1.  Run the command: `netsh firewall add portopening protocol=TCP port=135 name="RPC (TCP/135)" mode=ENABLE scope=SUBNET`  
  
2.  Run the command: `netsh firewall add allowedprogram program="%ProgramFiles%\Microsoft SQL Server\100\DTS\Binn\MsDtsSrvr.exe" name="SSIS Service" scope=SUBNET`  
  
    > [!NOTE]  
    >  To open the firewall for all computers, and also for computers on the Internet, replace scope=SUBNET with scope=ALL.  
  
 The following procedure describes how to use the Windows user interface to open TCP port 135, add MsDtsSrvr.exe to the exception list, and specify the scope of unblocking for the firewall.  
  
#### To configure a firewall using the windowsfirewall dialog box  
  
1.  In the Control Panel, double-click **Windows Firewall**.  
  
2.  In the **Windows Firewall** dialog box, click the **Exceptions** tab and then click **Add Program.**  
  
3.  In the **Add a Program** dialog box, **click Browse**, navigate to the Program Files\Microsoft SQL Server\100\DTS\Binn folder, click MsDtsSrvr.exe, and then click **Open**. Click **OK** to close the **Add a Program** dialog box.  
  
4.  On the **Exceptions** tab, click **Add Port.**  
  
5.  In the **Add a Port** dialog box, type **RPC(TCP/135)** or another descriptive name in the **Nam**e box, type **135** in the **Port Number** box, and then select **TCP**.  
  
    > [!IMPORTANT]  
    >  [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service always uses port 135. You cannot specify a different port.  
  
6.  In the **Add a Port** dialog box, you can optionally click **Change Scope** to modify the default scope.  
  
7.  In the **Change Scope** dialog box, select **My network (subnet only)** or type a custom list, and then click **OK**.  
  
8.  To close the **Add a Port** dialog box, click **OK**.  
  
9. To close the **Windows Firewall** dialog box, click **OK**.  
  
    > [!NOTE]  
    >  To configure the windowsfirewall, this procedure uses the **Windows Firewall** item in Control Panel. The **Windows Firewall** item only configures the firewall for the current network location profile. However, you can also configure the windowsfirewall by using the **netsh** command line tool or the [!INCLUDE[msCoName](../includes/msconame-md.md)] Management Console (MMC) snap-in named windowsfirewall with Advanced Security. For more information about these tools, see [Configure the Windows Firewall to Allow SQL Server Access](../../2014/sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md).  
  
## See Also  
 [Configuring the Integration Services Service &#40;SSIS Service&#41;](service/integration-services-service-ssis-service.md)   
 [Integration Services Service &#40;SSIS Service&#41;](service/integration-services-service-ssis-service.md)  
  
  

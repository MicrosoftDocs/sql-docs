---
title: "Connect to a Remote Integration Services Server (SSIS Service) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "service [Integration Services], remote instance"
  - "Integration Services service, connecting"
  - "Integration Services service, remote instance"
  - "service [Integration Services], connecting"
ms.assetid: 9487aff1-44d8-42c1-8176-bb9891d4632d
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Connect to a Remote Integration Services Server (SSIS Service)
    
> [!IMPORTANT] 
> This topic discusses the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service, a Windows service for managing [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages. [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] supports the service for backward compatibility with earlier releases of [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]. Starting in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], you can manage objects such as packages on the Integration Services server.  
  
 Connecting to an instance of [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] on a remote server, from [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] or another management application, requires a specific set of rights on the server for the users of the application.  
  
> [!IMPORTANT] 
> To manage packages that are stored on a remote server, you do not have to connect to the instance of the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service on that remote server. Instead, edit the configuration file for the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service so that [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] displays the packages that are stored on the remote server. For more information, see [Configuring the Integration Services Service &#40;SSIS Service&#41;](service/integration-services-service-ssis-service.md).  
  
## Connecting to Integration Services on a Remote Server  
  
#### To connect to Integration Services on a Remote Server  
  
1.  Open [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
2.  Select **File**, **Connect Object Explorer** to display the **Connect to Server** dialog box.  
  
3.  Select **Integration Services** in the **Server type** list.  
  
4.  Type the name of a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] server in the **Server name** text box.  
  
    > [!NOTE]  
    >  The [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service is not instance-specific. You connect to the service by using the name of the computer on which the Integration Services service is running.  
  
5.  Click **Connect**.  
  
> [!NOTE]  
>  The **Browse for Servers** dialog box does not display remote instances of [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]. In addition, the options available on the **Connection Options** tab of the **Connect to Server** dialog box, which is displayed by clicking the **Options** button, are not applicable to [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] connections.  
  
## Eliminating the "Access Is Denied" Error  
 When a user without sufficient rights attempts to connect to an instance of [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] on a remote server, the server responds with an "Access is denied" error message. You can avoid this error message by ensuring that users have the required DCOM permissions.  
  
#### To configure rights for remote users on Windows Server 2003 or Windows XP  
  
1.  If the user is not a member of the local Administrators group, add the user to the Distributed COM Users group. You can do this in the Computer Management MMC snap-in accessed from the **Administrative Tools** menu.  
  
2.  Open Control Panel, double-click **Administrative Tools,** and then double-click **Component Services** to start the Component Services MMC snap-in.  
  
3.  Expand the **Component Services** node in the left pane of the console. Expand the **Computers** node, expand **My Computer**, and then click the **DCOM Config** node.  
  
4.  Select the **DCOM Config** node, and then select SQL Server Integration Services 11.0 in the list of applications that can be configured.  
  
5.  Right-click on SQL Server Integration Services 11.0 and select **Properties**.  
  
6.  In the **SQL Server Integration Services 11.0 Properties** dialog box, select the **Security** tab.  
  
7.  Under **Launch and Activation Permissions**, select **Customize**, then click **Edit** to open the **Launch Permission** dialog box.  
  
8.  In the **Launch Permission** dialog box, add or delete users, and assign the appropriate permissions to the appropriate users and groups. The available permissions are Local Launch, Remote Launch, Local Activation, and Remote Activation. The Launch rights grant or deny permission to start and stop the service; the Activation rights grant or deny permission to connect to the service.  
  
9. Click OK to close the dialog box.  
  
10. Under **Access Permissions**, repeat steps 7 and 8 to assign the appropriate permissions to the appropriate users and groups.  
  
11. Close the MMC snap-in.  
  
12. Restart the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service.  
  
#### To configure rights for remote users on Windows 2000 with the latest service packs  
  
1.  Run **dcomcnfg.exe** at the command prompt.  
  
2.  On the **Applications** page of the **Distributed COM Configuration Properties** dialog box, select SQL Server Integration Services 11.0 and then click **Properties**.  
  
3.  Select the **Security** page.  
  
4.  Use the two separate dialog boxes to configure **Access Permissions** and **Launch Permissions**. You cannot distinguish between remote and local access - Access permissions include local and remote access, and Launch permissions include local and remote launch.  
  
5.  Close the dialog boxes and **dcomcnfg.exe**.  
  
6.  Restart the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service.  
  
## Connecting by using a Local Account  
 If you are working in a local Windows account on a client computer, you can connect to the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service on a remote computer only if a local account that has the same name and password and the appropriate rights exists on the remote computer.  
  
## By default the SSIS service does not support delegation  
By default the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service does not support the delegation of credentials, or what is sometimes referred to as a double hop. In this scenario, you are working on a client computer, the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service is running on a second computer, and [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is running on a third computer. First, [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] successfully passes your credentials from the client computer to the second computer on which the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service is running. Then, however, the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service cannot delegate your credentials from the second computer to the third computer on which [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is running.

You can enable delegation of credentials by granting the **Trust this user for delegation to any service (Kerberos Only)** right to the SQL Server service account, which launches the Integration Services service (ISServerExec.exe) as a child process. Before you grant this right, consider whether it meets the security requirements of your organization.

For more info, see [Getting Cross Domain Kerberos and Delegation working with SSIS Package](https://blogs.msdn.microsoft.com/psssql/2014/06/26/getting-cross-domain-kerberos-and-delegation-working-with-ssis-package/).
  
## See Also  
 [Configure a Windows Firewall for Access to the SSIS Service](../../2014/integration-services/configure-a-windows-firewall-for-access-to-the-ssis-service.md)  
  
  

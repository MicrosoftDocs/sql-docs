---
title: "Configure a Report Server for Remote Administration | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "Reporting Services Configuration tool"
  - "WMI provider [Reporting Services], remote configuration"
  - "configuration management [WMI]"
  - "report servers [Reporting Services], configuring"
  - "remote server administration [Reporting Services]"
ms.assetid: 8c7f145f-3ac2-4203-8cd6-2a4694395d09
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Configure a Report Server for Remote Administration
  In [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], you can configure report server instances locally or remotely. To configure a remote report server instance, you can use the Reporting Services Configuration tool or write custom code that uses the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Windows Management Instrumentation (WMI) provider. The Reporting Services Configuration tool provides a graphical interface to the WMI provider so that you can configure a report server without having to write code. When you start the tool, you can specify a remote server to connect to.  
  
 Before you can use the tool to configure a remote report server, you must follow the instructions in this topic to enable ports in Windows Firewall, enable remote connections, and enable remote WMI requests.  
  
 Proper configuration helps you avoid the following error:  
  
 `The machine could not be found.`  
  
 `"The RPC server is unavailable. (Exception from HRESULT: 0x800706BA)".`  
  
## Prerequisites  
 To modify firewall settings, you must be logged on locally and you must be a member of the local Administrators group. You cannot modify the Windows firewall settings of a remote computer over a remote connection.  
  
 If you want to enable remote administration for a non-administrator user, you must grant the account Distributed Component Object Model (DCOM) Remote Activation permissions. Instructions for configuring the server for non-administrator access are provided in this topic.  
  
 Some organizations have group policies that prevent remote server administration for certain operating systems or users. Before you begin modifying firewall settings, check with your network administrator to verify whether there are restrictions on remote administration.  
  
 For more information, see [Connecting Through Windows Firewall](https://go.microsoft.com/fwlink/?LinkId=63615) in the Platform SDK documentation on MSDN.  
  
## Tasks  
 Tasks that enable remote report server configuration include the following:  
  
-   Enable ports in Windows Firewall to allow requests on ports used by the report server and by the SQL Server Database Engine instance.  
  
-   Enable remote connections to the instance of the Database Engine instance that hosts the report server database. A remote connection is necessary for configuring the report server database connection and managing the encryption keys.  
  
-   Enable remote WMI requests to pass through the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows firewall.  
  
-   If you are configuring a remote report server for administration by a non-administrative user, you must set DCOM permissions to enable remote WMI access to a standard Windows user account. Because WMI uses DCOM as transport for remote calls, you must set the DCOM permissions so that users who are not logged on as the local administrator can configure the server.  
  
-   If you are configuring a remote report server for administration by a non-administrative user, you must also set WMI permissions on the report server WMI namespace. By default, all members of the local Administrator group have access to the report server WMI namespace. If you want to grant access to non-administrators, you must set permissions.  
  
 Instructions on how to perform these tasks are provided in this topic.  
  
### To open ports in Windows Firewall  
  
1.  [Configure a Windows Firewall for Database Engine Access](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md).  
  
2.  [Configure a Firewall for Report Server Access](configure-a-firewall-for-report-server-access.md).  
  
### To configure remote connections to the report server database  
  
1.  Click **Start**, point to **Programs**, point to [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], point to **Configuration Tools**, and click **SQL Server Configuration Manager**.  
  
2.  In the left pane, expand **SQL Server Network Configuration**, and then click **Protocols** for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
3.  In the details pane, enable the TCP/IP and Named Pipes protocols, and then restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.  
  
### To enable remote administration in Windows Firewall  
  
1.  Log on as a local administrator to the computer for which you want to enable remote administration.  
  
2.  If the report server is running on Windows Vista, right-click **Command Prompt** and select **Run as administrator**. For other operating systems, open a command prompt window.  
  
3.  Run the following command:  
  
    ```  
    netsh.exe firewall set service type=REMOTEADMIN mode=ENABLE scope=ALL  
    ```  
  
     You can specify different options for Scope. For more information, see the Windows Firewall product documentation.  
  
4.  Verify that remote administration is enabled. You can run the following command to show the status:  
  
    ```  
    netsh.exe firewall show state  
    ```  
  
5.  Reboot the computer.  
  
### To set DCOM permissions to enable remote WMI access for non-administrators  
  
1.  On the Start menu, point to **Administrative Tools**, click **Component Services**.  
  
     For Windows Vista, on the Start menu, click **All Programs**, click **Run**, and then enter `mmc comexp.msc`.  
  
2.  Open the Component Services folder.  
  
3.  Open the Computers folder.  
  
4.  Select My Computer.  
  
5.  On the **Action** menu, and select **Properties**.  
  
6.  Click **COM Security**.  
  
7.  In **Launch and Activation Permissions**, click **Edit Limits**.  
  
8.  If you do not see your name in **Launch Permission**, click **Add**.  
  
9. Type the name of your user account, and then click **OK**.  
  
10. In **Permissions for \<User or Group>**, in the **Allow** column, select **Remote Launch** and **Remote Activation**, and then click **OK**.  
  
### To set permissions on the report server WMI namespace for non-administrators  
  
1.  On the Start menu, point to **Administrative Tools**, click **Computer Management**.  
  
2.  Open the Services and Applications folder.  
  
3.  Right-click **WMI Control**, and select **Properties**.  
  
4.  Click **Security**.  
  
5.  Open the Root folder.  
  
6.  Open the Microsoft folder.  
  
7.  Open the SQLServer folder.  
  
8.  Open the ReportServer folder.  
  
9. Open instance folder. If you installed the default instance, the folder is MSSQLSERVER.  
  
10. Open the v10 folder.  
  
11. Select the Admin folder, and then click **Security**.  
  
12. Click **Add**, and then type the user account you will use to manage the server.  
  
13. In the **Allow** column, select **Enable Account**, **Remote Enable**, and **Read Security**, and then click **OK**.  
  
## See Also  
 [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../sql-server/install/reporting-services-configuration-manager-native-mode.md)  
  
  

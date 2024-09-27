---
title: "Configure a report server for remote administration"
description: Learn how to configure Reporting Services Report Server instances for local or remote configuration by using the configuration tool or writing custom code.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "Reporting Services Configuration tool"
  - "WMI provider [Reporting Services], remote configuration"
  - "configuration management [WMI]"
  - "report servers [Reporting Services], configuring"
  - "remote server administration [Reporting Services]"
---
# Configure a report server for remote administration
  In [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], you can configure report server instances locally or remotely. To configure a remote report server instance, you can use the Reporting Services Configuration tool or write custom code that uses the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Windows Management Instrumentation (WMI) provider. The Reporting Services Configuration tool provides a graphical interface to the WMI provider so that you can configure a report server without having to write code. When you start the tool, you can specify a remote server to connect to.  
  
 Before you can use the tool to configure a remote report server, you must follow the instructions in this article to enable ports in Windows Firewall, enable remote connections, and enable remote WMI requests.  
  
 Proper configuration helps you avoid the following error:  
  
 ```
The machine could not be found.  
  
 The RPC server is unavailable. (Exception from HRESULT: 0x800706BA).
```  
  
## Prerequisites  
 To modify firewall settings, you must be logged on locally and you must be a member of the local Administrators group. You can't modify the Windows Firewall settings of a remote computer over a remote connection.  
  
 If you want to enable remote administration for a nonadministrator user, you must grant the account Distributed Component Object Model (DCOM) Remote Activation permissions. Instructions for configuring the server for nonadministrator access are provided in this article.  
  
 Some organizations have group policies that prevent remote server administration for certain operating systems or users. Before you begin modifying firewall settings, check with your network administrator to verify whether there are restrictions on remote administration.  
  
 For more information, see [Connecting through Windows Firewall](/windows/win32/wmisdk/connecting-to-wmi-remotely-with-vbscript) in the Platform SDK documentation on MSDN.  
  
## Tasks  
 Tasks that enable remote report server configuration include the following settings:  
  
-   Enable ports in Windows Firewall to allow requests on ports used by the report server and by the SQL Server Database Engine instance.  See [Configure a Firewall for Report Server Access](../../reporting-services/report-server/configure-a-firewall-for-report-server-access.md) and [Configure a Windows Firewall for Database Engine Access](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md).  
  
-   Enable remote connections to the instance of the Database Engine instance that hosts the report server database. A remote connection is necessary for configuring the report server database connection and managing the encryption keys.  
  
-   Enable remote WMI requests to pass through the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Firewall.  
  
-   If you're configuring a remote report server for administration by a nonadministrative user, you must set DCOM permissions to enable remote WMI access to a standard Windows user account. Because WMI uses DCOM as transport for remote calls, you must set the DCOM permissions so that users who aren't logged on as the local administrator can configure the server.  
  
-   If you're configuring a remote report server for administration by a nonadministrative user, you must also set WMI permissions on the report server WMI namespace. By default, all members of the local Administrator group have access to the report server WMI namespace. If you want to grant access to nonadministrators, you must set permissions.  
  
 Instructions on how to perform these tasks are provided in this article.  
  
### Configure remote connections to the report server database  
  
1. Select **Start**, point to **Programs**, point to [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], point to **Configuration Tools**, and choose **SQL Server Configuration Manager**.  
  
1. In the left pane, expand **SQL Server Network Configuration**, and then select **Protocols** for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
1. In the details pane, enable the **TCP/IP** and **Named Pipes** protocols, and then restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.  
  
### Enable remote administration in Windows Firewall  
  
1. Sign in as a local administrator to the computer for which you want to enable remote administration.  
  
1. Open a command prompt with administrative privileges.  
  
1. Run the following command:  
  
    ```  
    netsh.exe firewall set service type=REMOTEADMIN mode=ENABLE scope=ALL  
    ```  
  
     You can specify different options for Scope. For more information, see the Windows Firewall product documentation.  
  
1. Verify that remote administration is enabled. You can run the following command to show the status:  
  
    ```  
    netsh.exe firewall show state  
    ```  
  
1. Restart the computer.  
  
### Set DCOM permissions to enable remote WMI access for nonadministrators  
  
1. On the **Start** menu, point to **Administrative Tools**, select **Component Services**.  
  
     For Windows Vista, on the **Start** menu, point to **All Programs**, select **Run**, and then enter **mmc comexp.msc**.  
  
1. Open the **Component Services** folder.  
  
1. Open the **Computers** folder.  
  
1. Select **My Computer**.  
  
1. On the **Action** menu, and select **Properties**.  
  
1. Select **COM Security**.  
  
1. In **Launch and Activation Permissions**, select **Edit Limits**.  
  
1. If you don't see your name in **Launch Permission**, select **Add**.  
  
1. Enter the name of your user account, and then select **OK**.  
  
1. In **Permissions for \<User or Group>**, under the **Allow** column, choose **Remote Launch** and **Remote Activation**, and then select **OK**.  
  
### Set permissions on the report server WMI namespace for nonadministrators  
  
1.  On the **Start** menu, point to **Administrative Tools**, select **Computer Management**.  
  
2.  Open the **Services and Applications** folder.  
  
3.  Right-click **WMI Control**, and select **Properties**.  
  
4.  Select **Security**.  
  
5.  Open the **Root** folder.  
  
6.  Open the **Microsoft** folder.  
  
7.  Open the **SQLServer** folder.  
  
8.  Open the **ReportServer** folder.  
  
9. Open **Instance** folder. If you installed the default instance, the folder is **MSSQLSERVER**.  
  
10. Open the **v10** folder.  
  
11. Select the **Admin** folder, and then select **Security**.  
  
12. Select **Add**, and then enter the user account you use to manage the server.  
  
13. In the **Allow** column, choose **Enable Account**, **Remote Enable**, and **Read Security**, and then select **OK**.  
  
## Related content

- [Report Server Configuration Manager &#40;native mode&#41;](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md)

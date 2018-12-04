---
title: "View Offline Log Files | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "Log File Viewer, viewing offline logs"
  - "offline log files"
ms.assetid: 9223e474-f224-4907-a4f2-081e11db58f5
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# View Offline Log Files
  Beginning in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], you can view [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] log files from a local or remote instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] when the target instance is offline or cannot start.  
  
 You can access the offline log files from Registered Servers, or programmatically through WMI and WQL (WMI Query Language) queries.  
  
> [!NOTE]  
>  You can also use these methods to connect to an instance that is online, but for some reason, you cannot connect through a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connection.  
  
## Before you Begin  
 To connect to offline log files, an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be installed on the computer that you are using to view the offline log files, and on the computer where the log files that you want to view are located. If an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed on both computers, you can view offline files for instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and for instances that are running earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on either computer.  
  
 If you are using Registered Servers, the instance that you want to connect to must be registered under **Local Server Groups** or under **Central Management Servers**. (The instance can be registered on its own or be a member of a server group.) For more information about how to add an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to Registered Servers, see the following topics:  
  
-   [Create or Edit a Server Group &#40;SQL Server Management Studio&#41;](../../ssms/register-servers/create-or-edit-a-server-group-sql-server-management-studio.md)  
  
-   [Register a Connected Server &#40;SQL Server Management Studio&#41;](../../ssms/register-servers/register-a-connected-server-sql-server-management-studio.md)  
  
-   [Create a Central Management Server and Server Group &#40;SQL Server Management Studio&#41;](../../ssms/register-servers/create-a-central-management-server-and-server-group.md)  
  
 For more information about how to view offline log files programmatically through WMI and WQL queries, see the following topics:  
  
-   [SqlErrorLogEvent Class](../wmi-provider-configuration-classes/sqlerrorlogevent-class.md) (This topic shows how to retrieve values for logged events in a specified log file.)  
  
-   [SqlErrorLogFile Class](../wmi-provider-configuration-classes/sqlerrorlogfile-class.md) (This topic shows how to retrieve information about all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] log files on a specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].)  
  
##  <a name="BeforeYouBegin"></a> Permissions  
 To connect to an offline log file, you must have the following permissions on both the local and remote computers:  
  
-   Read access to the **Root\Microsoft\SqlServer\ComputerManagement12** WMI namespace. By default, everyone has read access through the Enable Account permission. For more information, see the "To verify WMI permissions" procedure later in this section.  
  
-   Read permission to the folder that contains the error log files. By default the error log files are located in the following path (where \<*Drive>* represents the drive where you installed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and \<*InstanceName*> is the name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]):  
  
     **\<Drive>:\Program Files\Microsoft SQL Server\MSSQL12.\<InstanceName>\MSSQL\Log**  
  
 To verify WMI namespace security settings, you can use the WMI Control snap-in.  
  
#### To verify WMI permissions  
  
1.  Open the WMI Control snap-in. To do this, do either of the following, depending on the operating system:  
  
    -   Click **Start**, type `wmimgmt.msc` in the **Start Search** box, and then press ENTER.  
  
    -   Click **Start**, click **Run**, type `wmimgmt.msc`, and then press ENTER.  
  
2.  By default, the WMI Control snap-in manages the local computer.  
  
     If you want to connect to a remote computer, follow these steps:  
  
    1.  Right-click **WMI Control (Local)**, and then click **Connect to another computer**.  
  
    2.  In the **Change managed computer** dialog box, click **Another computer**.  
  
    3.  Enter the remote computer name, and then click **OK**.  
  
3.  Right-click **WMI Control (Local)** or **WMI Control (***RemoteComputerName***)**, and then click **Properties**.  
  
4.  In the **WMI Control Properties** dialog box, click the **Security** tab.  
  
5.  In the namespace tree, locate and then click the following namespace:  
  
     **Root\Microsoft\SqlServer\ComputerManagement10**  
  
6.  Click **Security**.  
  
7.  Make sure that the account that will be used has the **Enable Account** permission. This permission allows Read access to WMI objects.  
  
### View Log Files  
 The following procedure shows how to view offline log files through Registered Servers. The procedure assumes the following:  
  
 The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that you want to connect to is already registered in Registered Servers.  
  
##### To view log files for instances that are offline  
  
1.  If you want to view offline log files on a local instance, make sure that you start [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] with elevated permissions. To do this, when you start [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], right-click **SQL Server Management Studio**, and then click **Run as administrator**.  
  
2.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], on the **View** menu, click **Registered Servers**.  
  
3.  In the console tree, locate the instance on which you want to view the offline files.  
  
4.  Do one of the following:  
  
    -   If the instance is under **Local Server Groups**, expand **Local Server Groups**, expand the server group (if the instance is a member of a group), right-click the instance, and then click **View SQL Server Log**.  
  
    -   If the instance is the Central Management Server itself, expand **Central Management Servers**, right-click the instance, point to **Central Management Server Actions**, and then click **View SQL Server Log**.  
  
    -   If the instance is under **Central Management Servers**, expand **Central Management Servers**, expand the Central Management Server, right-click the instance (or expand a server group and right-click the instance), and then click **View SQL Server Log**.  
  
5.  If you are connecting to a local instance, the connection is made using the current user credentials.  
  
     If you are connecting to a remote instance, in the **Log File Viewer - Connect As** dialog box, do either of the following:  
  
    -   To connect as the current user, make sure that the **Connect as another user** check box is cleared, and then click **OK**.  
  
    -   To connect as another user, select the **Connect as another user** check box, and then click **Set User**. When you are prompted, enter the user credentials (with the user name in the format *domain_name*\\*user_name*), click **OK**, and then click **OK** again to connect.  
  
    > [!NOTE]  
    >  If the log files take too long to load, you can click **Stop** on the Log File Viewer toolbar.  
  
## See Also  
 [Log File Viewer](log-file-viewer.md)  
  
  

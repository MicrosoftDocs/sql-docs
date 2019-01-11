---
title: "Configure a Windows Firewall for Database Engine Access | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "connections [SQL Server], firewall systems"
  - "firewall systems, [Database Engine]"
  - "security [SQL Server], firewalls"
ms.assetid: 0093b43c-c6b5-4574-9b30-3a0e91e1a1f9
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Configure a Windows Firewall for Database Engine Access
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  > [!div class="nextstepaction"]
  > [Please share your feedback about the SQL Docs Table of Contents!](https://aka.ms/sqldocsurvey)

  This topic describes how to configure a Windows firewall for Database Engine access in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using SQL Server Configuration Manager. Firewall systems help prevent unauthorized access to computer resources. To access an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] through a firewall, you must configure the firewall on the computer running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to allow access.  
  
 For more information about the default Windows firewall settings, and a description of the TCP ports that affect the [!INCLUDE[ssDE](../../includes/ssde-md.md)], Analysis Services, Reporting Services, and Integration Services, see [Configure the Windows Firewall to Allow SQL Server Access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md). There are many firewall systems available. For information specific to your system, see the firewall documentation.  
  
 The principal steps to allow access are:  
  
1.  Configure the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to use a specific TCP/IP port. The default instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] uses port 1433, but that can be changed. The port used by the [!INCLUDE[ssDE](../../includes/ssde-md.md)] is listed in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log. Instances of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)], [!INCLUDE[ssEW](../../includes/ssew-md.md)], and named instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] use dynamic ports. To configure these instances to use a specific port, see [Configure a Server to Listen on a Specific TCP Port &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/configure-a-server-to-listen-on-a-specific-tcp-port.md).  
  
2.  Configure the firewall to allow access to that port for authorized users or computers.  
  
> [!NOTE]  
>  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service lets users connect to instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] that are not listening on port 1433, without knowing the port number. To use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser, you must open UDP port 1434. To promote the most secure environment, leave the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service stopped, and configure clients to connect using the port number.  
  
> [!NOTE]  
>  By default, [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows enables the Windows Firewall, which closes port 1433 to prevent Internet computers from connecting to a default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on your computer. Connections to the default instance using TCP/IP are not possible unless you reopen port 1433. The basic steps to configure the Windows firewall are provided in the following procedures. For more information, see the Windows documentation.  
  
 As an alternative to configuring [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to listen on a fixed port and opening the port, you can list the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] executable (Sqlservr.exe) as an exception to the blocked programs. Use this method when you want to continue to use dynamic ports. Only one instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can be accessed in this way.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To configure a Windows Firewall for Database Engine access, using:**  
  
     [SQL Server Configuration Manager](#SSMSProcedure)  
  
## Before You Begin  
  
###  <a name="Security"></a> Security  
 Opening ports in your firewall can leave your server exposed to malicious attacks. Make sure that you understand firewall systems before you open ports. For more information, see [Security Considerations for a SQL Server Installation](../../sql-server/install/security-considerations-for-a-sql-server-installation.md)  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Configuration Manager  
 Applies to Windows Vista, Windows 7, and Windows Server 2008  
  
 The following procedures configure the Windows Firewall by using the Windows Firewall with Advanced Security Microsoft Management Console (MMC) snap-in. The Windows Firewall with Advanced Security only configures the current profile. For more information about the Windows Firewall with Advanced Security, see [Configure the Windows Firewall to Allow SQL Server Access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md)  
  
#### To open a port in the Windows firewall for TCP access  
  
1.  On the **Start** menu, click **Run**, type **WF.msc**, and then click **OK**.  
  
2.  In the **Windows Firewall with Advanced Security**, in the left pane, right-click **Inbound Rules**, and then click **New Rule** in the action pane.  
  
3.  In the **Rule Type** dialog box, select **Port**, and then click **Next**.  
  
4.  In the **Protocol and Ports** dialog box, select **TCP**. Select **Specific local ports**, and then type the port number of the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], such as **1433** for the default instance. Click **Next**.  
  
5.  In the **Action** dialog box, select **Allow the connection**, and then click **Next**.  
  
6.  In the **Profile** dialog box, select any profiles that describe the computer connection environment when you want to connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)], and then click **Next**.  
  
7.  In the **Name** dialog box, type a name and description for this rule, and then click **Finish**.  
  
#### To open access to SQL Server when using dynamic ports  
  
1.  On the **Start** menu, click **Run**, type **WF.msc**, and then click **OK**.  
  
2.  In the **Windows Firewall with Advanced Security**, in the left pane, right-click **Inbound Rules**, and then click **New Rule** in the action pane.  
  
3.  In the **Rule Type** dialog box, select **Program**, and then click **Next**.  
  
4.  In the **Program** dialog box, select **This program path**. Click **Browse**, and navigate to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that you want to access through the firewall, and then click **Open**. By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is at **C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Binn\Sqlservr.exe**. Click **Next**.  
  
5.  In the **Action** dialog box, select **Allow the connection**, and then click **Next**.  
  
6.  In the **Profile** dialog box, select any profiles that describe the computer connection environment when you want to connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)], and then click **Next**.  
  
7.  In the **Name** dialog box, type a name and description for this rule, and then click **Finish**.  
  
## See Also  
 [How to: Configure Firewall Settings (Azure SQL Database)](https://azure.microsoft.com/documentation/articles/sql-database-configure-firewall-settings/)  
  
  

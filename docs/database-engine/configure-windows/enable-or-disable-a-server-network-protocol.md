---
title: "Enable or Disable a Server Network Protocol"
description: Find out how to enable or disable a server network protocol. See how to use SQL Server Configuration Manager or PowerShell for this task.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "network protocols [SQL Server], disabling"
  - "remote connections [SQL Server], enabling using Configuration Manager"
  - "protocols [SQL Server], enabling using Configuration Manager"
  - "protocols [SQL Server], disabling using Configuration Manager"
  - "disabling network protocols, Configuration Manager"
  - "network protocols [SQL Server], enabling"
  - "enabling network protocols, Configuration Manager"
  - "surface area configuration [SQL Server], connection protocols"
  - "connections [SQL Server], enabling remote using Configuration Manager"
---
# Enable or Disable a Server Network Protocol
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  All network protocols are installed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup, but may or may not be enabled. This topic describes how to enable or disable a server network protocol in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager or PowerShell. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] must be stopped and restarted for the change to take effect.  
  
> [!IMPORTANT]  
>  During setup of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] a login is added for the BUILTIN\Users group. This allows all authenticated users of the computer to access the instance of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] as a member of the public role. The BUILTIN\Users login can be safely removed to restrict [!INCLUDE[ssDE](../../includes/ssde-md.md)] access to computer users who have individual logins or are members of other Windows groups with logins.  
  
> [!WARNING]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[msCoName](../../includes/msconame-md.md)] data providers for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] up to [!INCLUDE[sssql14](../../includes/sssql14-md.md)] only support TLS 1.0 and SSL 3.0 by default. If you enforce a different protocol (such as TLS 1.1 or TLS 1.2) by making changes in the operating system SChannel layer, your connections to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might fail unless you have installed the appropriate update to add support for TLS 1.1 and 1.2 to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] which is listed <a href="https://support.microsoft.com/help/3135244/tls-1-2-support-for-microsoft-sql-server">here</a>. Starting from [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], all release versions of SQL Server include TLS 1.2 support without further updates required.
  
 **In This Topic**  
  
-   **To enable or disable a server network protocol using:**  
  
     [SQL Server Configuration Manager](#SSMSProcedure)  
  
     [PowerShell](#PowerShellProcedure)  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Configuration Manager  
  
#### To enable a server network protocol  
  
1.  In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, in the console pane, expand **SQL Server  Network Configuration**.  
  
2.  In the console pane, click **Protocols for** _\<instance name>_.  
  
3.  In the details pane, right-click the protocol you want to change, and then click **Enable** or **Disable**.  
  
4.  In the console pane, click **SQL Server Services**.  
  
5.  In the details pane, right-click **SQL Server (**_\<instance name>_**)**, and then click **Restart**, to stop and restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.  
  
##  <a name="PowerShellProcedure"></a> Using SQL Server PowerShell  
  
#### To Enable a Server Network Protocol Using PowerShell  
  
1.  Using administrator permissions open a command prompt.  
  
2.  Start Windows PowerShell from the taskbar, or click Start, then All Programs, then Accessories, then Windows PowerShell, then Windows PowerShell.  
  
3.  Import the **sqlps** module by entering **Import-Module "sqlps"**  
  
4.  Execute the following statements to enable both the TCP and named pipes protocols. Replace `<computer_name>` with the name of the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you are configuring a named instance, replace `MSSQLSERVER` with the instance name.  
  
     To disable protocols, set the `IsEnabled` properties to `$false`.  
  
    ```  
    $smo = 'Microsoft.SqlServer.Management.Smo.'  
    $wmi = new-object ($smo + 'Wmi.ManagedComputer').  
  
    # List the object properties, including the instance names.  
    $Wmi  
  
    # Enable the TCP protocol on the default instance.  
    $uri = "ManagedComputer[@Name='<computer_name>']/ ServerInstance[@Name='MSSQLSERVER']/ServerProtocol[@Name='Tcp']"  
    $Tcp = $wmi.GetSmoObject($uri)  
    $Tcp.IsEnabled = $true  
    $Tcp.Alter()  
    $Tcp  
  
    # Enable the named pipes protocol for the default instance.  
    $uri = "ManagedComputer[@Name='<computer_name>']/ ServerInstance[@Name='MSSQLSERVER']/ServerProtocol[@Name='Np']"  
    $Np = $wmi.GetSmoObject($uri)  
    $Np.IsEnabled = $true  
    $Np.Alter()  
    $Np  
    ```  
  
#### To configure the protocols for the local computer  
  
-   When the script is run locally and configures the local computer, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell can make the script more flexible by dynamically determining the local computer name. To retrieve the local computer name, replace the line setting the `$uri` variable with the following line.  
  
    ```  
    $uri = "ManagedComputer[@Name='" + (get-item env:\computername).Value + "']/ServerInstance[@Name='MSSQLSERVER']/ServerProtocol[@Name='Tcp']"  
    ```  
  
#### To restart the Database Engine by using SQL Server PowerShell  
  
-   After you enable or disable protocols, you must stop and restart the [!INCLUDE[ssDE](../../includes/ssde-md.md)] for the change to take effect. Execute the following statements to stop and start the default instance by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell. To stop and start a named instance replace `'MSSQLSERVER'` with `'MSSQL$<instance_name>'`.  
  
    ```  
    # Get a reference to the ManagedComputer class.  
    CD SQLSERVER:\SQL\<computer_name>  
    $Wmi = (get-item .).ManagedComputer  
    # Get a reference to the default instance of the Database Engine.  
    $DfltInstance = $Wmi.Services['MSSQLSERVER']  
    # Display the state of the service.  
    $DfltInstance  
    # Stop the service.  
    $DfltInstance.Stop();  
    # Wait until the service has time to stop.  
    # Refresh the cache.  
    $DfltInstance.Refresh();   
    # Display the state of the service.  
    $DfltInstance  
    # Start the service again.  
    $DfltInstance.Start();  
    # Wait until the service has time to start.  
    # Refresh the cache and display the state of the service.  
    $DfltInstance.Refresh(); $DfltInstance  
    ```  
  
  

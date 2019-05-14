---
title: "Troubleshoot Connecting to the SQL Server Database Engine | Microsoft Docs"
ms.custom: sqlfreshmay19
ms.date: "05/15/2019"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "troubleshooting, connecting to Database Engine"
  - "connecting to Database Engine, troubleshooting"
ms.assetid: 474c365b-c451-4b07-b636-1653439f4b1f
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Troubleshoot Connecting to the SQL Server Database Engine
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This article lists troubleshooting techniques to use when you cannot connect to an instance of the SQL Server Database Engine on a single server.

>[!NOTE]
>For other scenarios, see:
>- [Availability group listener](../availability-groups/windows/listeners-client-connectivity-application-failover.md)
>- [Failover cluster instances](../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md)

These steps are not in the order of the most likely problems which you probably already tried. These steps are in order of the most basic problems to more complex problems. These steps assume that you are connecting to SQL Server instance from another computer by using the TCP/IP protocol, which is the most common situation. These steps are written for SQL Server 2016 with both the SQL Server and the client applications running Windows 10, however the steps generally apply to other versions of SQL Server and other operating systems with only slight modifications.

These instructions are useful when troubleshooting the "**Connect to Server**" error, which can be `Error Number: 11001 (or 53), Severity: 20, State: 0`, and error messages such as:

> `A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections.`

> `(provider: Named Pipes Provider, error: 40 - Could not open a connection to SQL Server) (Microsoft SQL Server, Error: 53)`

> `(provider: TCP Provider, error: 0 - No such host is known.) (Microsoft SQL Server, Error: 11001)`

This error usually means that the SQL Server computer can't be found or that the TCP port number is either not known, or is not the correct port number, or is blocked by a firewall.

> [!TIP]
> An interactive troubleshooting page is available from [!INCLUDE[msCoName_md](../../includes/msconame-md.md)] Customer Support Services at [Solving Connectivity errors to SQL Server](http://support.microsoft.com/help/4009936/solving-connectivity-errors-to-sql-server).

### Not included

* This topic does not include information about SSPI errors. For SSPI errors, see [How to troubleshoot the "Cannot generate SSPI context" error message](http://support.microsoft.com/kb/811889). 
* This topic does not include information about Kerberos errors. For help, see [Microsoft Kerberos Configuration Manager for SQL Server](http://www.microsoft.com/download/details.aspx?id=39046).
* This topic does not include information about SQL Azure Connectivity. For help, see [Troubleshooting connectivity issues with Microsoft Azure SQL Database](http://support.microsoft.com/help/10085/troubleshooting-connectivity-issues-with-microsoft-azure-sql-database).

## Get instance name from Configuration Manger

Verify the server name from [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).

Configuration Manager is automatically installed on the computer when SQL Server is installed. Instructions on starting Configuration Manager vary slightly by version of SQL Server and Windows. For version specific details, see [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).)

1. Sign in to the computer hosting the instance of SQL Server.
1. Start SQL Server Configuration Manager.
1. In the left pane, select **SQL Server Services**.
1. In the right pane, verify the name of the instance of the database engine.

    * `SQL SERVER (MSSQLSERVER)` denotes a default instance of SQL Server. The name of the default instance is `<computer name>`.
    * `SQL SERVER (<instance name>)` denotes a named instance of SQL Server. The name of the name instance is `<computer name>\<instance name>`

## Verify - the instance is running

To verify that the instance is running, in Configuration Manager look at the symbol by the SQL Server instance.

* A green arrow indicates that an instance is running.
* A red square indicates that an instance is stopped.

If the instance is stopped, right-click the instance and then click **Start**. The server instance starts, and the indicator becomes a green arrow.

<a name = "startbrowser"></a>To connect to a named instance, SQL Server Browser service must be running. In Configuration Manager, locate **SQL Server Browser** service and verify that it is running. If it is not running, start it. SQL Server Browser service is not required for default instances.

## Get the IP address of the server

Get the IP Address of the computer hosting the instance of SQL Server.

1. On the Start menu, click **Run**. In the **Run** window type **cmd**, and then click **OK**.
1. In the command prompt window, type **ipconfig** and then press enter. Make a note of the **IPv4** Address and the **IPv6** Address. 

  >SQL Server can connect using the either IP version 4 protocol or IP version 6 protocol. Your network could allow either or both. Most people start by troubleshooting the **IPv4** address. It's shorter and easier to type.)

## <a name = "getTCP"></a>Get the SQL Server instance TCP port

In most cases, you connect to the Database Engine from another computer using the TCP protocol.

1. Using SQL Server Management Studio on the computer running SQL Server, connect to the instance of SQL Server. In Object Explorer, expand **Management**, expand **SQL Server Logs**, and then double-click the current log.
2. In the Log Viewer, click the **Filter** button on the toolbar. In the **Message contains text** box, type `server is listening on`, click **Apply filter**, and then click **OK**.
3. A message similar to `Server is listening on [ 'any' <ipv4> 1433]` should be listed. 

  This message indicates that this instance of SQL Server is listening on all the IP addresses on this computer (for IP version 4) and is listening to TCP port 1433. (TCP port 1433 is usually the port used by the Database Engine. Only one instance of SQL Server can use a port, so if there is more than one instance of SQL Server installed, some instances must use other port numbers.) Make a note of the port number used by the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] instance that you are trying to connect to.

  > [!NOTE]
  > `IP address 127.0.0.1` is probably listed. It is called the loopback adapter address. Only processes on the same computer can use it to connect. It can be useful for troubleshooting, but you can't use it to connect from another computer.

## <a name = "enableprotocols"></a>Enable Protocols

In some installations of SQL Server, connecting to the Database Engine from another computer is not enabled unless an administrator uses Configuration Manager to enable it. To enable connections from another computer:

1. Open SQL Server Configuration Manager, as described earlier.
2. Using Configuration Manager, in the left pane expand **SQL Server Network Configuration**, and then select the instance of SQL Server that you want to connect to. The right-pane lists the connection protocols available. Shared Memory is normally enabled. It can only be used from the same computer, so most installations leave Shared Memory enabled. To connect to SQL Server from another computer, you normally use TCP/IP. If TCP/IP is not enabled, right-click **TCP/IP**, and then click **Enable**.
3. If you changed the enabled setting for any protocol, restart the Database Engine. In the left pane select **SQL Server Services**. In the right-pane, right-click the instance of the Database Engine, and then click **Restart**.

## <a name="testTCPIP"></a>Testing TCP/IP Connectivity

Connecting to SQL Server by using TCP/IP requires that Windows can establish the connection. Use the `ping` tool to test TCP.
1. On the Start menu, click **Run**. In the **Run** window type **cmd**, and then click **OK**. 
2. In the command prompt window, type `ping <ip address>` and then the IP address of the computer that is running SQL Server. For example:

   * IPv4: `ping 192.168.1.101`
   * IPv6: `ping fe80::d51d:5ab5:6f09:8f48%11`

1. If your network is properly configured, `ping` returns `Reply from <IP address>` followed by some additional information. If `ping` returns `Destination host unreachable` or `Request timed out`, then TCP/IP is not correctly configured. Errors at this point could indicate a problem with the client computer, the server computer, or something about the network such as a router. To troubleshoot network problems, see[Advanced troubleshooting for TCP/IP issues](/windows/client-management/troubleshoot-tcpip).
1. Next, if the ping test succeeded using the IP address, test that the computer name can be resolved to the TCP/IP address. On the client computer, in the command prompt window, type `ping` and then the computer name of the computer that is running SQL Server. For example, `ping newofficepc` 
1. If `ping` to the IP address succeeds, but `ping` to the computer returns `Destination host unreachable` or `Request timed out` you might have old (stale) name resolution information cached on the client computer. Type `ipconfig /flushdns` to clear the DNS (Dynamic Name Resolution) cache. Then ping the computer by name again. With the DNS cache empty, the client computer will check for the newest information about the IP address for the server computer. 
1. If your network is properly configured, `ping` returns `Reply from <IP address>` followed by some additional information. If you can successfully ping the server computer by IP address but receive an error such as `Destination host unreachable.` or `Request timed out.` when pinging by computer name, then name resolution is not correctly configured. (For more information, see the 2006 article previously referenced, [How to Troubleshoot Basic TCP/IP Problems](http://support.microsoft.com/kb/169790).) Successful name resolution is not required to connect to SQL Server, but if the computer name cannot be resolved to an IP address, then connections must be made specifying the IP address. Name resolution can be fixed later.

## Testing a Local Connection

Before troubleshooting a connection problem from another computer, first test your ability to connect from a client application installed locally on the computer that is running SQL Server. Connecting locally avoids issues with networks and firewalls. 
This procedure uses SQL Server Management Studio. If you do not have Management Studio installed, see [Download SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md). If you are not able to install Management Studio, you can test the connection using the `sqlcmd.exe` utility. `sqlcmd.exe` is installed with the Database Engine. For information about `sqlcmd.exe`, see [sqlcmd Utility](../../tools/sqlcmd-utility.md).)

1. Sign in to the computer where SQL Server is installed, using a login that has permission to access SQL Server. (During installation, SQL Server requires at least one login to be specified as a SQL Server Administrator. If you do not know an administrator, see [Connect to SQL Server When System Administrators Are Locked Out](connect-to-sql-server-when-system-administrators-are-locked-out.md).)
2. On the Start page, type **SQL Server Management Studio**, or on older versions of Windows on the Start menu, point to **All Programs**, point to **Microsoft SQL Server**, and then click **SQL Server Management Studio**.
3. In the **Connect to Server** dialog box, in the **Server** type box, select **Database Engine**. In the **Authentication** box, select **Windows Authentication**. In the **Server name** box, type one of the following connection types:

   |Connecting to|Type|Example|
   |:-----------------|:---------------|:-----------------|
   |Default instance|`<computer name>`|`ACCNT27`|
   |Named Instance|`<computer name\instance name>`|`ACCNT27\PAYROLL`|

   > [!NOTE]
   > When connecting to a SQL Server from a client application on the same computer, the shared memory protocol is used. Shared memory is a type of local named pipe, so sometimes errors regarding pipes are encountered.

   If you receive an error at this point, you will have to resolve it before proceeding. There are many possible things that could be a problem. Your login might not be authorized to connect. Your default database might be missing.

   > [!NOTE]
   > Some error messages passed to the client intentionally do not give enough information to troubleshoot the problem. This is a security feature to avoid providing an attacker with information about SQL Server. To view the complete information about the error, look in the SQL Server error log. The details are provided there. 

1. If you receive error `18456 Login failed for user`, Books Online topic [MSSQLSERVER_18456](../../relational-databases/errors-events/mssqlserver-18456-database-engine-error.md) contains additional information about error codes. And Aaron Bertrand's blog has a very extensive list of error codes at [Troubleshooting Error 18456](http://sqlblog.org/2011/01/14/troubleshooting-error-18456). You can view the error log with SSMS (if you can connect), in the Management section of the Object Explorer. Otherwise, you can view the error log with the Windows Notepad program. The default location varies with your version and can be changed during setup. The default location for [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] is `C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Log\ERRORLOG`. 

1. If you can connect using shared memory, test connecting using TCP. You can force a TCP connection by specifying `tcp:` before the name. For example:

   |Connecting to:|Type:|Example:|
   |-----------------|---------------|-----------------|
   |Default instance|`tcp:<computer name>`|`tcp:ACCNT27`|
   |Named Instance|`tcp:<computer name/instance name>`|`tcp:ACCNT27\PAYROLL`|

1. If you can connect with shared memory but not TCP, then you must fix the TCP problem. The most likely issue is that TCP is not enabled. To enable TCP, See the [Enable Protocols](#enableprotocols) steps above.

1. If your goal is to connect with an account other than an administrator account, once you can connect as an administrator, try the connection again using the Windows Authentication login or the SQL Server Authentication login that the client application will be using.

## <a name = "openport"></a>Opening a Port in the Firewall

By default, the Windows firewall is turned on and will block connections from another computer. To connect using TCP/IP from another computer, on the SQL Server computer you must configure the firewall to allow connections to the TCP port used by the Database Engine. The default instance is listening on TCP port 1433, by default. If you have named instances or if you changed the default instance port, the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] TCP port may be listening on another port. See [Get the SQL Server instance TCP port](#getTCP).

If you are connecting to a named instance or a port other than TCP port 1433, you must also open the UDP port 1434 for the SQL Server Browser service. For step by step instruction on opening a port in the Windows firewall, see [Configure a Windows Firewall for Database Engine Access](configure-a-windows-firewall-for-database-engine-access.md).

## Testing the Connection

Once you can connect using TCP on the same computer, it's time to try connecting from the client computer. You could theoretically use any client application, but to avoid additional complexity, install the SQL Server Management tools on the client and make the attempt using SQL Server Management Studio.

1. On the client computer, using SQL Server Management Studio, attempt to connect using the IP Address and the TCP port number in the format IP address comma port number. For example, `192.168.1.101,1433`. If this connection fails, then you probably have one of the following problems:

    * `ping` of the IP address doesn't work, indicating a general TCP configuration problem. Go back to the section [Testing TCP/IP Connectivity](#testTCPIP).
    * SQL Server is not listening on the TCP protocol. Go back to the section [Enable Protocols](#enableprotocols).
    * SQL Server is listening on a port other than the port you specified. Go back to the section [Get the TCP port number](#getTCP).
    * The SQL Server TCP port is being blocked by the firewall. Go back to the section [Opening a Port in the Firewall](#opening-a-port-in-the-firewall).

2. Once you can connect using the IP address and port number, attempt to connect using the IP address without a port number. For a default instance, just use the IP address. For a named instance, use the IP address and the instance name in the format IP address backslash instance name, for example `192.168.1.101\<instance name` If this doesn't work, then you probably have one of the following problems:

    * If you are connecting to the default instance, it might be listening on a port other than TCP port 1433, and the client isn't attempting to connect to the correct port number.
    * If you are connecting to a named instance, the port number is not being returned to the client.

Both of these problems are related to the SQL Server Browser service, which provides the port number to the client. The solutions are:

  * Start the SQL Server Browser service. See the instructions to [start browser in SQL Server Configuration Manager](#startbrowser).
  * The SQL Server Browser service is being blocked by the firewall. Open UDP port 1434 in the firewall. Go back to the section [Opening a Port in the Firewall](#opening-a-port-in-the-firewall). Make sure you are opening a UDP port, not a TCP port.
  * The UDP port 1434 information is being blocked by a router. UDP communication (user datagram protocol) is not designed to pass through routers. This keeps the network from getting filled with low-priority traffic. You might be able to configure your router to forward UDP traffic, or you can decide to always provide the port number when you connect.
  * If the client computer is using Windows 7 or Windows Server 2008, (or a more recent operating system,) the UDP traffic might be dropped by the client operating system because the response from the server is returned from a different IP address than was queried. This is a security feature blocking "loose source mapping." For more information, see the **Multiple Server IP Addresses** section of the Books Online topic [Troubleshooting: Timeout Expired](http://msdn.microsoft.com/library/ms190181.aspx). This is an article from SQL Server 2008 R2, but the principals still apply. You might be able to configure the client to use the correct IP address, or you can decide to always provide the port number when you connect.

3. Once you can connect using the IP address (or IP address and instance name for a named instance), attempt to connect using the computer name (or computer name and instance name for a named instance). Put `tcp:` in front of the computer name to force a TCP/IP connection. For example, for the default instance on a computer named `ACCNT27`, use `tcp:ACCNT27` For a named instance called `PAYROLL`, on that computer use `tcp:ACCNT27\PAYROLL` If you can connect using the IP address but not using the computer name, then you have a name resolution problem. Go back to the section **Testing TCP/IP Connectivity**, section 4.

4. Once you can connect using the computer name forcing TCP, attempt connecting using the computer name but not forcing TCP. For example, for a default instance use just the computer name such as `CCNT27` For a named instance use the computer name and instance name like `ACCNT27\PAYROLL` If you could connect while forcing TCP, but not without forcing TCP, then the client is probably using another protocol (such as named pipes).

    1. On the client computer, using SQL Server Configuration Manager, in the left-pane expand **SQL Native Client** *version* **Configuration**, and then select **Client Protocols**.
    2. On the right-pane, Make sure TCP/IP is enabled. If TCP/IP is disabled, right-click **TCP/IP** and then click **Enable**.
    3. Make sure that the protocol order for TCP/IP is a smaller number that the named pipes (or VIA on older versions) protocols. Generally you should leave Shared Memory as order 1 and TCP/IP as order 2. Shared memory is only used when the client and SQL Server are running on the same computer. All enabled protocols are tried in order until one succeeds, except that shared memory is skipped when the connection is not to the same computer.


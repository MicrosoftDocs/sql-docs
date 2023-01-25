---
title: "Troubleshooting connectivity"
description: "Learn about JDBC connectivity and how to troubleshoot potential connection issues when using the Microsoft JDBC Driver for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/12/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Troubleshooting connectivity
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  The [!INCLUDE[jdbcNoVersion](../../includes/jdbcnoversion_md.md)] requires that TCP/IP be installed and running to communicate with your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. You can use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager to verify which network library protocols are installed.  
  
 A database connection attempt might fail for many reasons. These can include the following:  
  
-   TCP/IP is not enabled for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or the server or port number specified is incorrect. Verify that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is listening with TCP/IP on the specified server and port. This might be reported with an exception similar to: "The login has failed. The TCP/IP connection to the host has failed." This indicates one of the following:  
  
    -   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed but TCP/IP has not been installed as a network protocol for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Network Utility for [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)], or the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager for [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later.  
  
    -   TCP/IP is installed as a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] protocol, but it is not listening on the port specified in the JDBC connection URL. The default port is 1433, but [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can be configured at product installation to listen on any port. Make sure that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is listening on port 1433. Or, if the port has been changed, make sure that the port specified in the JDBC connection URL matches the changed port. For more information about JDBC connection URLs, see [Building the connection URL](../../connect/jdbc/building-the-connection-url.md).  
  
    -   The address of the computer that is specified in the JDBC connection URL does not refer to a server where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed and started.  
  
    -   The networking operation of TCP/IP between the client and server running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not operable. You can check TCP/IP connectivity to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using telnet. For example, at the command prompt, type `telnet 192.168.0.0 1433` where 192.168.0.0 is the address of the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and 1433 is the port it is listening on. If you receive a message that states "Telnet cannot connect," TCP/IP is not listening on that port for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connections. Use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Network Utility for [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)], or the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager for [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later to make sure that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is configured to use TCP/IP on port 1433.  
  
    -   The port that is used by the server has not been opened in the firewall. This includes the port that is used by the server or optionally, the port associated with a named instance of the server.  
  
-   The specified database name is incorrect. Make sure that you are logging on to an existing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
  
-   The user name or password is incorrect. Make sure that you have the correct values.  
  
-   When you use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, the JDBC driver requires that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, which is not the default. Make sure that this option is included when you install or configure your instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## See also  
 [Diagnosing problems with the JDBC driver](diagnosing-problems-with-the-jdbc-driver.md)   
 [Connecting to SQL Server with the JDBC driver](connecting-to-sql-server-with-the-jdbc-driver.md)  
  
  

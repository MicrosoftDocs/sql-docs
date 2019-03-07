---
title: Install R language and Python features on an Azure virtual machine - SQL Server Machine Learning Services
description: Run R and Python data science and machine learning solutions on a SQL Server virtual machine in the Azure cloud.
ms.prod: sql
ms.technology: machine-learning

ms.date: 11/09/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Install SQL Server Machine Learning Services with R and Python on an Azure virtual machine
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

You can install R and Python integration with Machine Learning Services on a SQL Server virtual machine in Azure, eliminating installation and configuration tasks. Once the virtual machine is deployed, the features are ready to use.
 
For step by step instructions, see [How to provision a Windows SQL Server virtual machine in the Azure portal](https://docs.microsoft.com/azure/virtual-machines/windows/sql/virtual-machines-windows-portal-sql-server-provision).

The [Configure SQL server settings](https://docs.microsoft.com/azure/virtual-machines/windows/sql/virtual-machines-windows-portal-sql-server-provision#4-configure-sql-server-settings) step is where you add machine learning to your instance.

<a name="firewall"></a>

## Unblock the firewall

By default, the firewall on the Azure virtual machine includes a rule that blocks network access for local user accounts.

You must disable this rule to ensure that you can access the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance from a remote data science client.  Otherwise, your machine learning code cannot execute in compute contexts that use the virtual machine's workspace.

To enable access from remote data science clients:

1. On the virtual machine, open Windows Firewall with Advanced Security.
2. Select **Outbound Rules**
3. Disable the following rule:
  
     `Block network access for R local user accounts in SQL Server instance MSSQLSERVER`
  
## Enable ODBC callbacks for remote clients

If you expect that clients calling the server will need to issue ODBC queries as part of their machine learning solutions, you must ensure that the Launchpad can make ODBC calls on behalf of the remote client. 

To do this, you must allow the SQL worker accounts that are used by Launchpad to log into the instance. For more information, see [Add SQLRUserGroup as a database user](../security/add-sqlrusergroup-to-database.md).

<a name="network"></a>

## Add network protocols

+ Enable Named Pipes
  
  [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] uses the Named Pipes protocol for connections between the client and server computers, and for some internal connections. If Named Pipes is not enabled, you must install and enable it on both the Azure virtual machine, and on any data science clients that connect to the server.
  
+ Enable TCP/IP

  TCP/IP is required for loopback connections. If you get the error "DBNETLIB; SQL Server does not exist or access denied", enable TCP/IP on the virtual machine that supports the instance.
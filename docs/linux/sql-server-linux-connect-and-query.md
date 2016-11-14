---
# required metadata

title: Connect to SQL Server on Linux - SQL Server vNext CTP1 | Microsoft Docs
description: This topic provides an overview of connection requirements for SQL Server on Linux. The sqlcmd tool is used for an example.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 11/14/2016
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 2b5aa551-3ad7-4f0d-b69b-4fe692dbbcee

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""
---
# Connect to SQL Server on Linux

This topic provides connection requirements and guidance for SQL Server vNext CTP1 running on Linux. In most cases, the connection requirements and processes do not differ across platforms. But this topic approaches the subject in the context of Linux and then points to other resources.

## Connection requirements
To connect to SQL Server on Linux, you must use SQL Authentication (username and password). To connect remotely, you must ensure that the port SQL Server listens on is open. The default instance of SQL Server listens on TCP port 1433. Depending on your Linux distribution and configuration, you might have to open this port in the firewall. 

If you are running Linux on an Azure virtual machine, you must also [create a network security group rule](#azure).

## Tools
You can connect to SQL Server using a client tool or through code. For example, the following command uses the **sqlcmd** tool to connect to the local SQL Server instance and return a list of database names. 

    sqlcmd -H localhost -U SA -P password -Q "SELECT Name from sys.Databases"

For a complete example of connecting and querying, see [Connect and query SQL Server on Linux with sqlcmd](sql-server-linux-connect-and-query-sqlcmd.md).

For examples of how to connect with other tools, see the [Develop](sql-server-linux-develop-overview.md) and [Manage](sql-server-linux-management-overview.md) areas. 

## <a id="troubleshoot"></a> Troubleshoot connection failures
If you are having difficulty connecting to your Linux SQL Server instance, there are a few things to check. 

- Verify that the server name or IP address is reachable from your client machine.
- Verify that the user name and password do not contain any typos or extra spaces or incorrect casing.
- Try to explicitly set the protocol and port number with the server name like the following: **tcp:servername,1433**.
- Network connectivity issues can also cause connection errors and timeouts. After verifying your connection information and network connectivity, try the connection again.

### <a id="azure"></a> Requirements for Linux VMs in Azure
If you are running Linux in an Azure virtual machine (VM), you must also create a Network Security Group rule for port 1433 to connect to SQL Server remotely.

1. In the Azure portal, select your Linux VM, and then select the **Network interfaces** setting. 
2. In the next blade, select your network interface to view its properties.
3. In the Network interface blade, click the **Network security group** link to manage the Network Security Group associated with your VM.
4. Create a Network Security Group rule. For step-by-step instructions, use the steps in [Create rules in an existing NSG](https://azure.microsoft.com/documentation/articles/virtual-networks-create-nsg-arm-pportal/#create-rules-in-an-existing-nsg). These provide the steps for creating an NSG rule, but you must customize your rule for incoming TCP traffic on port 1433. This is shown in the following screenshot.

    ![SQL Server network security group rule](./media/sql-server-linux-connect-and-query/network-security-rule.png)

## Next Steps
For more information on using SQL Server vNext on Linux, see [SQL Server on Linux](/).

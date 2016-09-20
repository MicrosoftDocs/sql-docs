---
title: "Connect With sqlcmd Command-Line SQL Client (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 32c7ea5e-f8b8-4f78-8143-6c90a0c44e4c
caps.latest.revision: 8
author: BarbKess
---
# Connect With sqlcmd Command-Line SQL Client (SQL Server PDW)
Describes how to use the SQL Server**sqlcmd** Command-Line Tool to connect to SQL Server PDW. **sqlcmd** is a command-line tool that ships with SQL Server; you can also download it directly from MSDN if you don’t have SQL Server installed. Use this tool to send queries to SQL Server PDW.  
  
-   To install **sqlcmd**, see [Install sqlcmd Command-Line Client &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/install-sqlcmd-command-line-client-sql-server-pdw.md).  
  
-   For the complete SQL Server documentation, see [sqlcmd Utility](http://msdn.microsoft.com/en-US/library/ms162773(v=sql.110).aspx) on MSDN.  
  
## sqlcmd Examples  
The following example, establishes a connection to the **AdventureWorksPDW2012** database. It uses 10.192.63.134 for the Control node Cluster IP address, and port 17001.  
  
```  
sqlcmd -S "10.192.63.134,17001" -U <login> -P **** -I -d AdventureWorksPDW2012  
```  
  
## General Remarks  
The –I option sets the SET QUOTED_IDENTIFIER connection option to ON. It is required for SQL Server 2012**sqlcmd** connections to SQL Server PDW. We recommend always using –I when connecting from any of the supported versions of SQL Server.  
  
## Limitations and Restrictions  
**Unsupported Options**  
  
The following list shows sqlcmd options that do not apply or are not supported for connections to SQL Server PDW.  
  
-A  
Specifies to login to SQL Server PDW with a Dedicated Administrator Connection (DAC).  
  
-K application_intent  
Specifies to declare the application workload type when connecting to a server.  
  
-C  
Specifies to configures the client to implicitly trust the server certificate without validation. This option is equivalent to the ADO.NET option TRUSTSERVERCERTIFICATE = true.  
  
-f codepage | i:codepage[,o:codepage] | o:codepage[,i:codepage]  
Specifies the input and output code pages. The codepage number is a numeric value that specifies an installed Windows code page.  
  
-M multisubnet_failover  
Specifies to provides faster detection of and connection to the active server of an availability group or failover cluster.  
  
-N  
Specifies to use an encrypted connection.  
  

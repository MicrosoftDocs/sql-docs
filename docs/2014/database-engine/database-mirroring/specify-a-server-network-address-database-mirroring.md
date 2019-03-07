---
title: "Specify a Server Network Address (Database Mirroring) | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "database mirroring [SQL Server], deployment"
  - "database mirroring [SQL Server], endpoint"
  - "endpoints [SQL Server], database mirroring"
  - "server network addresses [SQL Server]"
ms.assetid: a64d4b6b-9016-4f1e-a310-b1df181dd0c6
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Specify a Server Network Address (Database Mirroring)
  Setting up a database mirroring session requires a server network address for each of the server instances. The server network address of a server instance must unambiguously identify the instance by providing a system address and the number of the port on which the instance is listening.  
  
 Before you can specify a port in a server network address, the database mirroring endpoint must exist on the server instance. For more information, see [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md).  
  
  
  
##  <a name="Syntax"></a> Syntax for a Server Network Address  
 The syntax for a server network address is of the form:  
  
 TCP<strong>://</strong>*\<system-address>*<strong>:<strong>*\<port>* 
  
 where  
  
-   *\<system-address>* is a string that unambiguously identifies the destination computer system. Typically, the server address is a system name (if the systems are in the same domain), a fully qualified domain name, or an IP address:  
  
    -   If the systems are the same domain, you can use the name of the computer system; for example, `SYSTEM46`.  
  
    -   To use an IP address, it must be unique in your environment. We recommend that you use an IP address only if it is static. The IP address can be IP Version 4 (IPv4) or IP Version 6 (IPv6). An IPv6 address must be enclosed within square brackets, for example: **[**_<IPv6_address>_**]**.  
  
         To learn the IP address of a system, at the Windows command prompt, enter the **ipconfig** command.  
  
    -   The fully qualified domain name is guaranteed to work. This is a locally defined address string that different forms in different places. Often, but not always, a fully qualified domain name is a compound name that includes the computer name and a series of period-separated domain segments of the form:  
  
         _computer_name_ **.** _domain_segment_[...**.**_domain_segment_]  
  
         where *computer_name i*s the network name of the computer running the server instance, and *domain_segment*[...**.**_domain_segment_] is the remaining domain information of the server; for example: `localinfo.corp.Adventure-Works.com`.  
  
         The content and number of domain segments are determined within the company or organization. If you do not know the fully qualified domain name for your server, see your system administrator.  
  
        > [!NOTE]  
        >  For information about how to find a fully qualified domain name, see "Finding the Fully Qualified Domain Name," later in this topic.  
  
-   *\<port>* is the port number used by the mirroring endpoint of the partner server instance. For information about specifying an endpoint, see [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md).  
  
     A database mirroring endpoint can use any available port on the computer system. Each port number on a computer system must be associated with only one endpoint, and each endpoint is associated with a single server instance; thus, different server instances on the same server listen on different endpoints with different ports. Therefore, the port you specify in the server network address when you set up a database mirroring session will always direct the session to the server instance whose endpoint is associated with that port.  
  
     In the server network address of a server instance, only the number of the port associated with its mirroring endpoint distinguishes that instance from any other instances on the computer. The following figure illustrates the server network addresses of two server instances on a single computer. The default instance uses port `7022` and the named instance uses port `7033`. The server network address for these two server instances are, respectively: `TCP://MYSYSTEM.Adventure-works.MyDomain.com:7022` and `TCP://MYSYSTEM.Adventure-works.MyDomain.com:7033`. Note that the address does not contain the name of the server instance.  
  
     ![Server network addresses of a default instance](../media/dbm-2-instances-ports-1-system.gif "Server network addresses of a default instance")  
  
     To identify the port currently associated with database mirroring endpoint of a server instance, use the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement:  
  
    ```  
    SELECT type_desc, port FROM sys.tcp_endpoints  
    ```  
  
     Find the row whose **type_desc** value is "DATABASE_MIRRORING," and use the corresponding port number.  
  
### Examples  
  
#### A. Using a system name  
 The following server network address specifies a system name, `SYSTEM46`, and port `7022`.  
  
```  
ALTER DATABASE AdventureWorks SET PARTNER ='tcp://SYSTEM46:7022';  
```  
  
#### B. Using a fully qualified domain name  
 The following server network address specifies a fully qualified domain name, `DBSERVER8.manufacturing.Adventure-Works.com`, and port `7024`.  
  
```  
ALTER DATABASE AdventureWorks SET PARTNER ='tcp://DBSERVER8.manufacturing.Adventure-Works.com:7024';  
```  
  
#### C. Using IPv4  
 The following server network address specifies an IPv4 address, `10.193.9.134`, and port `7023`.  
  
```  
ALTER DATABASE AdventureWorks SET PARTNER ='tcp://10.193.9.134:7023';  
```  
  
#### D. Using IPv6  
 The following server network address contains an IPv6 address, `2001:4898:23:1002:20f:1fff:feff:b3a3`, and port `7022`.  
  
```  
ALTER DATABASE AdventureWorks SET PARTNER ='tcp://[2001:4898:23:1002:20f:1fff:feff:b3a3]:7022';  
```  
  
## Finding the Fully Qualified Domain Name  
 To find the fully qualified domain name of a system, at the Windows command prompt on that system, enter:  
  
 **IPCONFIG /ALL**  
  
 To form the fully qualified domain name, concatenate the values of*<host_name>* and *<Primary_Dns_Suffix>* as follows:  
  
 _<host_name>_ **.** _<Primary_Dns_Suffix>_  
  
 For example, the IP configuration  
  
 `Host Name  .  .  .  .  .  .  : MYSERVER`  
  
 `Primary Dns Suffix  .  .  .  : mydomain.Adventure-Works.com`  
  
 equates to the following fully qualified domain name:  
  
 `MYSERVER.mydomain.Adventure-Works.com`  
  
##  <a name="Examples"></a> Examples  
 The following example shows the server network address for a server instance on a computer system named `REMOTESYSTEM3` in another domain. The domain information is `NORTHWEST.ADVENTURE-WORKS.COM`, and the port of the database mirroring endpoint is `7025`. Given these example components, the server network address is.  
  
 `TCP://REMOTESYSTEM3.NORTHWEST.ADVENTURE-WORKS.COM:7025`  
  
 The following example shows the server network address for a server instance on a computer system named `DBSERVER1`. This system is in the local domain and is unambiguously identified by its system name. The port of the database mirroring endpoint is `7022`.  
  
 `TCP://DBSERVER1:7022`  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md)  
  
## See Also  
 [Database Mirroring &#40;SQL Server&#41;](database-mirroring-sql-server.md)   
 [The Database Mirroring Endpoint &#40;SQL Server&#41;](the-database-mirroring-endpoint-sql-server.md)  
  
  

---
title: "ALTER ROUTE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/30/2018"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_ROUTE_TSQL"
  - "ALTER ROUTE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ALTER ROUTE statement"
  - "dropping routes"
  - "modifying routes"
  - "removing routes"
  - "routes [Service Broker], modifying"
ms.assetid: 8dfb7b16-3dac-4e1e-8c97-adf2aad07830
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017"
---
# ALTER ROUTE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md.md)]

  Modifies route information for an existing route in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. 

  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
ALTER ROUTE route_name  
WITH    
  [ SERVICE_NAME = 'service_name' [ , ] ]  
  [ BROKER_INSTANCE = 'broker_instance' [ , ] ]  
  [ LIFETIME = route_lifetime [ , ] ]  
  [ ADDRESS =  'next_hop_address' [ , ] ]  
  [ MIRROR_ADDRESS = 'next_hop_mirror_address' ]  
[ ; ]  
  
```  
  
## Arguments  
 *route_name*  
 Is the name of the route to change. Server, database, and schema names cannot be specified.  
  
 WITH  
 Introduces the clauses that define the route being altered.  
  
 SERVICE_NAME **='**_service\_name_**'**  
 Specifies the name of the remote service that this route points to. The *service_name* must exactly match the name the remote service uses. [!INCLUDE[ssSB](../../includes/sssb-md.md)] uses a byte-by-byte comparison to match the *service_name*. In other words, the comparison is case sensitive and does not consider the current collation. A route with a service name of **'SQL/ServiceBroker/BrokerConfiguration'** is a route to a Broker Configuration Notice service. A route to this service might not specify a broker instance.  
  
 If the SERVICE_NAME clause is omitted, the service name for the route is unchanged.  
  
 BROKER_INSTANCE **='**_broker\_instance_**'**  
 Specifies the database that hosts the target service. The *broker_instance* parameter must be the broker instance identifier for the remote database, which can be obtained by running the following query in the selected database:  
  
```  
SELECT service_broker_guid  
FROM sys.databases  
WHERE database_id = DB_ID();  
```  
  
 When the BROKER_INSTANCE clause is omitted, the broker instance for the route is unchanged.  
  
> [!NOTE]  
>  This option is not available in a contained database.  
  
 LIFETIME **=**_route\_lifetime_  
 Specifies the time, in seconds, that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] retains the route in the routing table. At the end of the lifetime, the route expires, and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] no longer considers the route when choosing a route for a new conversation. If this clause is omitted, the lifetime of the route is unchanged.  
  
 ADDRESS **='**_next\_hop\_address_'  

 For Azure SQL Database Managed Instance, `ADDRESS` must be local.

 Specifies the network address for this route. The *next_hop_address* specifies a TCP/IP address in the following format:  
  
 **TCP://** { *dns_name* | *netbios_name* |*ip_address* } **:** *port_number*  
  
 The specified *port_number* must match the port number for the [!INCLUDE[ssSB](../../includes/sssb-md.md)] endpoint of an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] at the specified computer. This can be obtained by running the following query in the selected database:  
  
```  
SELECT tcpe.port  
FROM sys.tcp_endpoints AS tcpe  
INNER JOIN sys.service_broker_endpoints AS ssbe  
   ON ssbe.endpoint_id = tcpe.endpoint_id  
WHERE ssbe.name = N'MyServiceBrokerEndpoint';  
```  
  
 When a route specifies **'LOCAL'** for the *next_hop_address*, the message is delivered to a service within the current instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 When a route specifies **'TRANSPORT'** for the *next_hop_address*, the network address is determined based on the network address in the name of the service. A route that specifies **'TRANSPORT'** can specify a service name or broker instance.  
  
 When the *next_hop_address* is the principal server for a database mirror, you must also specify the MIRROR_ADDRESS for the mirror server. Otherwise, this route does not automatically failover to the mirror server.  
  
> [!NOTE]  
>  This option is not available in a contained database.  
  
 MIRROR_ADDRESS **='**_next\_hop\_mirror\_address_**'**  
 Specifies the network address for the mirror server of a mirrored pair whose principal server is at the *next_hop_address*. The *next_hop_mirror_address* specifies a TCP/IP address in the following format:  
  
 **TCP://**{ *dns_name* | *netbios_name* | *ip_address* } **:** *port_number*  
  
 The specified *port_number* must match the port number for the [!INCLUDE[ssSB](../../includes/sssb-md.md)] endpoint of an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] at the specified computer. This can be obtained by running the following query in the selected database:  
  
```  
SELECT tcpe.port  
FROM sys.tcp_endpoints AS tcpe  
INNER JOIN sys.service_broker_endpoints AS ssbe  
   ON ssbe.endpoint_id = tcpe.endpoint_id  
WHERE ssbe.name = N'MyServiceBrokerEndpoint';  
```  
  
 When the MIRROR_ADDRESS is specified, the route must specify the SERVICE_NAME clause and the BROKER_INSTANCE clause. A route that specifies **'LOCAL'** or **'TRANSPORT'** for the *next_hop_address* might not specify a mirror address.  
  
> [!NOTE]  
>  This option is not available in a contained database.  
  
## Remarks  
 The routing table that stores the routes is a meta-data table that can be read through the **sys.routes** catalog view. The routing table can only be updated through the CREATE ROUTE, ALTER ROUTE, and DROP ROUTE statements.  
  
 Clauses that are not specified in the ALTER ROUTE command remain unchanged. Therefore, you cannot ALTER a route to specify that the route does not time out, that the route matches any service name, or that the route matches any broker instance. To change these characteristics of a route, you must drop the existing route and create a new route with the new information.  
  
 When a route specifies **'TRANSPORT'** for the *next_hop_address*, the network address is determined based on the name of the service. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can successfully process service names that begin with a network address in a format that is valid for a *next_hop_address*. Services with names that contain valid network addresses will route to the network address in the service name.  
  
 The routing table can contain any number of routes that specify the same service, network address, and/or broker instance identifier. In this case, [!INCLUDE[ssSB](../../includes/sssb-md.md)] chooses a route using a procedure designed to find the most exact match between the information specified in the conversation and the information in the routing table.  
  
 To alter the AUTHORIZATION for a service, use the ALTER AUTHORIZATION statement.  
  
## Permissions  
 Permission for altering a route defaults to the owner of the route, members of the **db_ddladmin** or **db_owner** fixed database roles, and members of the **sysadmin** fixed server role.  
  
## Examples  
  
### A. Changing the service for a route  
 The following example modifies the `ExpenseRoute` route to point to the remote service `//Adventure-Works.com/Expenses`.  
  
```  
ALTER ROUTE ExpenseRoute  
   WITH   
     SERVICE_NAME = '//Adventure-Works.com/Expenses';  
```  
  
### B. Changing the target database for a route  
 The following example changes the target database for the `ExpenseRoute` route to the database identified by the unique identifier `D8D4D268-00A3-4C62-8F91-634B89B1E317.`  
  
```  
ALTER ROUTE ExpenseRoute  
   WITH   
     BROKER_INSTANCE = 'D8D4D268-00A3-4C62-8F91-634B89B1E317';  
```  
  
### C. Changing the address for a route  
 The following example changes the network address for the `ExpenseRoute` route to TCP port `1234` on the host with the IP address `10.2.19.72`.  
  
```  
ALTER ROUTE ExpenseRoute   
   WITH   
     ADDRESS = 'TCP://10.2.19.72:1234';  
```  
  
### D. Changing the database and address for a route  
 The following example changes the network address for the `ExpenseRoute` route to TCP port `1234` on the host with the DNS name `www.Adventure-Works.com`. It also changes the target database to the database identified by the unique identifier `D8D4D268-00A3-4C62-8F91-634B89B1E317`.  
  
```  
ALTER ROUTE ExpenseRoute  
   WITH   
     BROKER_INSTANCE = 'D8D4D268-00A3-4C62-8F91-634B89B1E317',  
     ADDRESS = 'TCP://www.Adventure-Works.com:1234';  
```  
  
## See Also  
 [CREATE ROUTE &#40;Transact-SQL&#41;](../../t-sql/statements/create-route-transact-sql.md)   
 [DROP ROUTE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-route-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  

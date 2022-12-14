---
title: "CREATE ROUTE (Transact-SQL)"
description: CREATE ROUTE (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "03/30/2018"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CREATE_ROUTE_TSQL"
  - "ROUTE"
  - "CREATE ROUTE"
  - "ROUTE_TSQL"
helpviewer_keywords:
  - "lifetimes [Service Broker]"
  - "routes [Service Broker], creating"
  - "forwarding brokers [SQL Server]"
  - "service names [Service Broker]"
  - "database mirroring [SQL Server], routing"
  - "expired routes [SQL Server]"
  - "activating routes"
  - "CREATE ROUTE statement"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016||>=sql-server-linux-2017"
---
# CREATE ROUTE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdbmi-xxxx-xxx-md.md)]

  Adds a new route to the routing table for the current database. For outgoing messages, [!INCLUDE[ssSB](../../includes/sssb-md.md)] determines routing by checking the routing table in the local database. For messages on conversations that originate in another instance, including messages to be forwarded, [!INCLUDE[ssSB](../../includes/sssb-md.md)] checks the routes in **msdb**.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
CREATE ROUTE route_name  
[ AUTHORIZATION owner_name ]  
WITH    
   [ SERVICE_NAME = 'service_name', ]  
   [ BROKER_INSTANCE = 'broker_instance_identifier' , ]  
   [ LIFETIME = route_lifetime , ]  
   ADDRESS =  'next_hop_address'  
   [ , MIRROR_ADDRESS = 'next_hop_mirror_address' ]  
[ ; ]  
```  
  
## Arguments  
 *route_name*  
 Is the name of the route to create. A new route is created in the current database and owned by the principal specified in the AUTHORIZATION clause. Server, database, and schema names cannot be specified. The *route_name* must be a valid **sysname**.  
  
 AUTHORIZATION *owner_name*  
 Sets the owner of the route to the specified database user or role. The *owner_name* can be the name of any valid user or role when the current user is a member of either the **db_owner** fixed database role or the **sysadmin** fixed server role. Otherwise, *owner_name* must be the name of the current user, the name of a user that the current user has IMPERSONATE permission for, or the name of a role to which the current user belongs. When this clause is omitted, the route belongs to the current user.  
  
 WITH  
 Introduces the clauses that define the route being created.  
  
 SERVICE_NAME = **'**_service\_name_**'**  
 Specifies the name of the remote service that this route points to. The *service_name* must exactly match the name the remote service uses. [!INCLUDE[ssSB](../../includes/sssb-md.md)] uses a byte-by-byte comparison to match the *service_name*. In other words, the comparison is case sensitive and does not consider the current collation. If the SERVICE_NAME is omitted, this route matches any service name, but has lower priority for matching than a route that specifies a SERVICE_NAME. A route with a service name of **'SQL/ServiceBroker/BrokerConfiguration'** is a route to a Broker Configuration Notice service. A route to this service might not specify a broker instance.  
  
 BROKER_INSTANCE = **'**_broker\_instance\_identifier_**'**  
 Specifies the database that hosts the target service. The *broker_instance_identifier* parameter must be the broker instance identifier for the remote database, which can be obtained by running the following query in the selected database:  
  
```sql  
SELECT service_broker_guid  
FROM sys.databases  
WHERE database_id = DB_ID()  
```  
  
 When the BROKER_INSTANCE clause is omitted, this route matches any broker instance. A route that matches any broker instance has higher priority for matching than routes with an explicit broker instance when the conversation does not specify a broker instance. For conversations that specify a broker instance, a route with a broker instance has higher priority than a route that matches any broker instance.  
  
 LIFETIME **=**_route\_lifetime_  
 Specifies the time, in seconds, that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] retains the route in the routing table. At the end of the lifetime, the route expires, and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] no longer considers the route when choosing a route for a new conversation. If this clause is omitted, the *route_lifetime* is NULL and the route never expires.  
  
 ADDRESS **='**_next\_hop\_address_**'**  
For SQL Managed Instance, `ADDRESS` must be local. 

Specifies the network address for this route. The *next_hop_address* specifies a TCP/IP address in the following format:  
  
 **TCP://**{ *dns_name* | *netbios_name* | *ip_address* } **:**_port\_number_  
  
 The specified *port_number* must match the port number for the [!INCLUDE[ssSB](../../includes/sssb-md.md)] endpoint of an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] at the specified computer. This can be obtained by running the following query in the selected database:  
  
```sql  
SELECT tcpe.port  
FROM sys.tcp_endpoints AS tcpe  
INNER JOIN sys.service_broker_endpoints AS ssbe  
   ON ssbe.endpoint_id = tcpe.endpoint_id  
WHERE ssbe.name = N'MyServiceBrokerEndpoint';  
```  
  
 When the service is hosted in a mirrored database, you must also specify the MIRROR_ADDRESS for the other instance that hosts a mirrored database. Otherwise, this route does not fail over to the mirror.  
  
 When a route specifies **'LOCAL'** for the *next_hop_address*, the message is delivered to a service within the current instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 When a route specifies **'TRANSPORT'** for the *next_hop_address*, the network address is determined based on the network address in the name of the service. A route that specifies **'TRANSPORT'** might not specify a service name or broker instance.  
  
 MIRROR_ADDRESS **='**_next\_hop\_mirror\_address_**'**  
 Specifies the network address for a mirrored database with one mirrored database hosted at the *next_hop_address*. The *next_hop_mirror_address* specifies a TCP/IP address in the following format:  
  
 **TCP://**{ *dns_name* | *netbios_name* | *ip_address* } **:** *port_number*  
  
 The specified *port_number* must match the port number for the [!INCLUDE[ssSB](../../includes/sssb-md.md)] endpoint of an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] at the specified computer. This can be obtained by running the following query in the selected database:  
  
```sql  
SELECT tcpe.port  
FROM sys.tcp_endpoints AS tcpe  
INNER JOIN sys.service_broker_endpoints AS ssbe  
   ON ssbe.endpoint_id = tcpe.endpoint_id  
WHERE ssbe.name = N'MyServiceBrokerEndpoint';  
```  
  
 When the MIRROR_ADDRESS is specified, the route must specify the SERVICE_NAME clause and the BROKER_INSTANCE clause. A route that specifies **'LOCAL'** or **'TRANSPORT'** for the *next_hop_address* might not specify a mirror address.  
  
## Remarks  
 The routing table that stores the routes is a metadata table that can be read through the **sys.routes** catalog view. This catalog view can only be updated through the CREATE ROUTE, ALTER ROUTE, and DROP ROUTE statements.  
  
 By default, the routing table in each user database contains one route. This route is named **AutoCreatedLocal**. The route specifies **'LOCAL'** for the *next_hop_address* and matches any service name and broker instance identifier.  
  
 When a route specifies **'TRANSPORT'** for the *next_hop_address*, the network address is determined based on the name of the service. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can successfully process service names that begin with a network address in a format that is valid for a *next_hop_address*.  
  
 The routing table can contain any number of routes that specify the same service, network address, and broker instance identifier. In this case, [!INCLUDE[ssSB](../../includes/sssb-md.md)] chooses a route using a procedure designed to find the most exact match between the information specified in the conversation and the information in the routing table.  
  
 [!INCLUDE[ssSB](../../includes/sssb-md.md)] does not remove expired routes from the routing table. An expired route can be made active using the ALTER ROUTE statement.  
  
 A route cannot be a temporary object. Route names that start with **#** are allowed, but are permanent objects.  
  
## Permissions  
 Permission for creating a route defaults to members of the **db_ddladmin** or **db_owner** fixed database roles and the **sysadmin** fixed server role.  
  
## Examples  
  
### A. Creating a TCP/IP route by using a DNS name  
 The following example creates a route to the service `//Adventure-Works.com/Expenses`. The route specifies that messages to this service travel over TCP to port `1234` on the host identified by the DNS name `www.Adventure-Works.com`. The target server delivers the messages upon arrival to the broker instance identified by the unique identifier `D8D4D268-00A3-4C62-8F91-634B89C1E315`.  
  
```sql 
CREATE ROUTE ExpenseRoute  
    WITH  
    SERVICE_NAME = '//Adventure-Works.com/Expenses',  
    BROKER_INSTANCE = 'D8D4D268-00A3-4C62-8F91-634B89C1E315',  
    ADDRESS = 'TCP://www.Adventure-Works.com:1234' ;  
```  
  
### B. Creating a TCP/IP route by using a NetBIOS name  
 The following example creates a route to the service `//Adventure-Works.com/Expenses`. The route specifies that messages to this service travel over TCP to port `1234` on the host identified by the NetBIOS name `SERVER02`. Upon arrival, the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] delivers the message to the database instance identified by the unique identifier `D8D4D268-00A3-4C62-8F91-634B89C1E315`.  
  
```sql  
CREATE ROUTE ExpenseRoute  
    WITH   
    SERVICE_NAME = '//Adventure-Works.com/Expenses',  
    BROKER_INSTANCE = 'D8D4D268-00A3-4C62-8F91-634B89C1E315',  
    ADDRESS = 'TCP://SERVER02:1234' ;  
```  
  
### C. Creating a TCP/IP route by using an IP address  
 The following example creates a route to the service `//Adventure-Works.com/Expenses`. The route specifies that messages to this service travel over TCP to port `1234` on the host at the IP address `192.168.10.2`. Upon arrival, the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] delivers the message to the broker instance identified by the unique identifier `D8D4D268-00A3-4C62-8F91-634B89C1E315`.  
  
```sql  
CREATE ROUTE ExpenseRoute  
    WITH  
    SERVICE_NAME = '//Adventure-Works.com/Expenses',  
    BROKER_INSTANCE = 'D8D4D268-00A3-4C62-8F91-634B89C1E315',  
    ADDRESS = 'TCP://192.168.10.2:1234' ;  
```  
  
### D. Creating a route to a forwarding broker  
 The following example creates a route to the forwarding broker on the server `dispatch.Adventure-Works.com`. Because both the service name and the broker instance identifier are not specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses this route for services that have no other route defined.  
  
```sql  
CREATE ROUTE ExpenseRoute  
    WITH  
    ADDRESS = 'TCP://dispatch.Adventure-Works.com' ;   
```  
  
### E. Creating a route to a local service  
 The following example creates a route to the service `//Adventure-Works.com/LogRequests` in the same instance as the route.  
  
```sql  
CREATE ROUTE LogRequests  
    WITH  
    SERVICE_NAME = '//Adventure-Works.com/LogRequests',  
    ADDRESS = 'LOCAL' ;  
```  
  
### F. Creating a route with a specified lifetime  
 The following example creates a route to the service `//Adventure-Works.com/Expenses`. The lifetime for the route is `259200` seconds, which equates to 72 hours.  
  
```sql  
CREATE ROUTE ExpenseRoute  
    WITH  
    SERVICE_NAME = '//Adventure-Works.com/Expenses',  
    LIFETIME = 259200,  
    ADDRESS = 'TCP://services.Adventure-Works.com:1234' ;  
```  
  
### G. Creating a route to a mirrored database  
 The following example creates a route to the service `//Adventure-Works.com/Expenses`. The service is hosted in a database that is mirrored. One of the mirrored databases is located at the address `services.Adventure-Works.com:1234`, and the other database is located at the address `services-mirror.Adventure-Works.com:1234`.  
  
```sql  
CREATE ROUTE ExpenseRoute  
    WITH  
    SERVICE_NAME = '//Adventure-Works.com/Expenses',  
    BROKER_INSTANCE = '69fcc80c-2239-4700-8437-1001ecddf933',  
    ADDRESS = 'TCP://services.Adventure-Works.com:1234',   
    MIRROR_ADDRESS = 'TCP://services-mirror.Adventure-Works.com:1234' ;  
```  
  
### H. Creating a route that uses the service name for routing  
 The following example creates a route that uses the service name to determine the network address to send the message to. Notice that a route that specifies `'TRANSPORT'` as the network address has lower priority for matching than other routes.  
  
```sql  
CREATE ROUTE TransportRoute  
    WITH ADDRESS = 'TRANSPORT' ;  
```  
  
## See Also  
 [ALTER ROUTE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-route-transact-sql.md)   
 [DROP ROUTE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-route-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  

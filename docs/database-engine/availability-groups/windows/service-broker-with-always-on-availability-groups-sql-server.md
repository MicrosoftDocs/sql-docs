---
title: "Service Broker with availability groups"
description: Contains information about configuring Service Broker with SQL Server Always On availability groups.
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "Service Broker, AlwaysOn Availability Groups"
  - "Service Broker, Always On Availability Groups"
  - "Availability Groups [SQL Server], interoperability"
---
# Service Broker with Always On Availability Groups (SQL Server)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

  This topic contains information about configuring Service Broker to work with [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)].  
  
  
##  <a name="ReceiveRemoteMessages"></a> Requirements for a Service in an Availability Group to Receive Remote Messages  
  
1.  **Ensure that the availability group possesses a listener.**  
  
     For more information, see [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md).  
  
2.  **Ensure that the Service Broker endpoint exists and is correctly configured.**  
  
     On every instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that hosts an availability replica for the availability group, configure the Service Broker endpoint, as follows:  
  
    -   Set LISTENER_IP to 'ALL'. This setting enables connections on any valid IP address that is bound to the availability group listener.  
  
    -   Set the Service Broker PORT to the same port number on all the host server instances.  
  
        > [!TIP]  
        >  To view the port number of the Service Broker endpoint on a given server instance, query the **port** column of the [sys.tcp_endpoints](../../../relational-databases/system-catalog-views/sys-tcp-endpoints-transact-sql.md) catalog view, where **type_desc** = 'SERVICE_BROKER'.  
  
     The following example creates a Windows authenticated Service Broker endpoint that uses the default Service Broker port (4022) and listens to all valid IP addresses.  
  
    ```  
    CREATE ENDPOINT [SSBEndpoint]  
        STATE = STARTED  
        AS TCP  (LISTENER_PORT = 4022, LISTENER_IP = ALL )  
        FOR SERVICE_BROKER (AUTHENTICATION = WINDOWS)  
    ```  
  
     For more information, see [CREATE ENDPOINT &#40;Transact-SQL&#41;](../../../t-sql/statements/create-endpoint-transact-sql.md).  

    > [!NOTE]  
    > SQL Server Service Broker is not MultiSubnet aware. Set `RegisterAllProvidersIP` to 0 and verify that the cluster has required permission in DNS to use static IP addresses. See [configure availability group listener](create-or-configure-an-availability-group-listener-sql-server.md) to learn more. Service Broker may delay message with status "CONVERSING" trying to use a disabled IP address.

3.  **Grant CONNECT permission on the endpoint.**  
  
     Grant CONNECT permission on the Service Broker endpoint either to PUBLIC or to a login.  
  
     The following example grants the connection on a Service Broker endpoint named `broker_endpoint` to PUBLIC.  
  
    ```  
    GRANT CONNECT ON ENDPOINT::[broker_endpoint] TO [PUBLIC]  
    ```  
  
     For more information, see [GRANT &#40;Transact-SQL&#41;](../../../t-sql/statements/grant-transact-sql.md).  
  
4.  **Ensure that msdb contains either an AutoCreatedLocal route or a route to the specific service.**  
  
    > [!NOTE]  
    >  By default, each user database, including **msdb**, contains the route **AutoCreatedLocal**. This route matches any service name and broker instance and specifies that the message should be delivered within the current instance. **AutoCreatedLocal** has lower priority than routes that explicitly specify a specific service that communicates with a remote instance.  
  
     For information about creating routes, see [Service Broker Routing Examples](https://msdn.microsoft.com/library/ms166090\(SQL.105\).aspx) (in the [!INCLUDE[ssKilimanjaro](../../../includes/sskilimanjaro-md.md)] version of Books Online) and [CREATE ROUTE &#40;Transact-SQL&#41;](../../../t-sql/statements/create-route-transact-sql.md).  
  
##  <a name="SendRemoteMessages"></a> Requirements for Sending Messages to a Remote Service in an Availability Group  
  
1.  **Create a route to the target service.**  
  
     Configure the route as follows:  
  
    -   Set ADDRESS to the listener IP address of availability group that hosts the service database.  
  
    -   Set PORT to the port that you specified in the Service Broker endpoint of each of the remote SQL Server instances.  
  
     The following example creates a route named `RouteToTargetService` for the `ISBNLookupRequestService` service. The route targets the availability group listener, `MyAgListener`, which uses port 4022.  
  
    ```  
    CREATE ROUTE [RouteToTargetService] WITH   
    SERVICE_NAME = 'ISBNLookupRequestService',   
    ADDRESS = 'TCP://MyAgListener:4022';  
  
    ```  
  
     For more information, see [CREATE ROUTE &#40;Transact-SQL&#41;](../../../t-sql/statements/create-route-transact-sql.md).  
  
2.  **Ensure that msdb contains either an AutoCreatedLocal route or a route to the specific service.** (For more information, see [Requirements for a Service in an Availability Group to Receive Remote Messages](#ReceiveRemoteMessages), earlier in this topic.)  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [CREATE ENDPOINT &#40;Transact-SQL&#41;](../../../t-sql/statements/create-endpoint-transact-sql.md)  
  
-   [CREATE ROUTE &#40;Transact-SQL&#41;](../../../t-sql/statements/create-route-transact-sql.md)  
  
-   [GRANT &#40;Transact-SQL&#41;](../../../t-sql/statements/grant-transact-sql.md)  
  
-   [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md).  
  
-   [Creation and Configuration of Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/creation-and-configuration-of-availability-groups-sql-server.md)  
  
-   [Set Up Login Accounts for Database Mirroring or Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/database-mirroring/set-up-login-accounts-database-mirroring-always-on-availability.md)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md)   
 [SQL Server Service Broker](../../../database-engine/configure-windows/sql-server-service-broker.md)  
  
  

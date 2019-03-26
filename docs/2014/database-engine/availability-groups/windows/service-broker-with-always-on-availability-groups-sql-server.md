---
title: "Service Broker with AlwaysOn Availability Groups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Service Broker, AlwaysOn Availability Groups"
  - "Availability Groups [SQL Server], interoperability"
ms.assetid: 881c20e5-1c99-44eb-b393-09fc5ea0f122
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Service Broker with AlwaysOn Availability Groups (SQL Server)
  This topic contains information about configuring Service Broker to work with [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  
 **In This Topic:**  
  
-   [Requirements for a Service in an Availability Group to Receive Remote Messages](#ReceiveRemoteMessages)  
  
-   [Requirements for Sending Messages to a Remote Service in an Availability Group](#SendRemoteMessages)  
  
##  <a name="ReceiveRemoteMessages"></a> Requirements for a Service in an Availability Group to Receive Remote Messages  
  
1.  **Ensure that the availability group possesses a listener.**  
  
     For more information, see [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](create-or-configure-an-availability-group-listener-sql-server.md).  
  
2.  **Ensure that the Service Broker endpoint exists and is correctly configured.**  
  
     On every instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that hosts an availability replica for the availability group, configure the Service Broker endpoint, as follows:  
  
    -   Set LISTENER_IP to 'ALL'. This setting enables connections on any valid IP address that is bound to the availability group listener.  
  
    -   Set the Service Broker PORT to the same port number on all the host server instances.  
  
        > [!TIP]  
        >  To view the port number of the Service Broker endpoint on a given server instance, query the **port** column of the [sys.tcp_endpoints](/sql/relational-databases/system-catalog-views/sys-tcp-endpoints-transact-sql) catalog view, where **type_desc** = 'SERVICE_BROKER'.  
  
     The following example creates a Windows authenticated Service Broker endpoint that uses the default Service Broker port (4022) and listens to all valid IP addresses.  
  
    ```  
    CREATE ENDPOINT [SSBEndpoint]  
        STATE = STARTED  
        AS TCP  (LISTENER_PORT = 4022, LISTENER_IP = ALL )  
        FOR SERVICE_BROKER (AUTHENTICATION = WINDOWS)  
    ```  
  
     For more information, see [CREATE ENDPOINT &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-endpoint-transact-sql).  
  
3.  **Grant CONNECT permission on the endpoint.**  
  
     Grant CONNECT permission on the Service Broker endpoint either to PUBLIC or to a login.  
  
     The following example grants the connection on a Service Broker endpoint named `broker_endpoint` to PUBLIC.  
  
    ```  
    GRANT CONNECT ON ENDPOINT::[broker_endpoint] TO [PUBLIC]  
    ```  
  
     For more information, see [GRANT &#40;Transact-SQL&#41;](/sql/t-sql/statements/grant-transact-sql).  
  
4.  **Ensure that msdb contains either an AutoCreatedLocal route or a route to the specific service.**  
  
    > [!NOTE]  
    >  By default, each user database, including **msdb**, contains the route **AutoCreatedLocal**. This route matches any service name and broker instance and specifies that the message should be delivered within the current instance. **AutoCreatedLocal** has lower priority than routes that explicitly specify a specific service that communicates with a remote instance.  
  
     For information about creating routes, see [Service Broker Routing Examples](https://msdn.microsoft.com/library/ms166090\(SQL.105\).aspx) (in the [!INCLUDE[ssKilimanjaro](../../../includes/sskilimanjaro-md.md)] version of Books Online) and [CREATE ROUTE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-route-transact-sql).  
  
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
  
     For more information, see [CREATE ROUTE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-route-transact-sql).  
  
2.  **Ensure that msdb contains either an AutoCreatedLocal route or a route to the specific service.** (For more information, see [Requirements for a Service in an Availability Group to Receive Remote Messages](#ReceiveRemoteMessages), earlier in this topic.)  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [CREATE ENDPOINT &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-endpoint-transact-sql)  
  
-   [CREATE ROUTE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-route-transact-sql)  
  
-   [GRANT &#40;Transact-SQL&#41;](/sql/t-sql/statements/grant-transact-sql)  
  
-   [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](create-or-configure-an-availability-group-listener-sql-server.md).  
  
-   [Creation and Configuration of Availability Groups &#40;SQL Server&#41;](creation-and-configuration-of-availability-groups-sql-server.md)  
  
-   [Set Up Login Accounts for Database Mirroring or AlwaysOn Availability Groups &#40;SQL Server&#41;](../../database-mirroring/set-up-login-accounts-database-mirroring-always-on-availability.md)  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../listeners-client-connectivity-application-failover.md)   
 [SQL Server Service Broker](../../configure-windows/sql-server-service-broker.md)  
  
  

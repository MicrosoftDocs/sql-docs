---
title: "WMI Provider for Configuration Management Classes"
description: Discover and implement the functionality that Windows Management Instrumentation provides. Choose from these classes for Configuration Manager.
author: markingmyname
ms.author: maghan
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "WMI Provider for Configuration Management, classes"
  - "classes [WMI]"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "WMI Classes"
---

# WMI Provider for Configuration Management Classes

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The SQL Server WMI provider for Configuration Management exposes a set of classes that administrators, automation systems, and management tools can use to query and modify SQL Server instance settings, network libraries, protocol properties, error logs, and service configuration.  
This article organizes the WMI classes into functional groups so that you can quickly find the class that applies to your configuration scenario.

## Client-side Networking & Configuration Classes

These classes define how SQL Server clients resolve servers, use network libraries, and determine client-level communication settings.

| Class | Description |
|-------|-------------|
| [ClientNetLibInfo](../../relational-databases/wmi-provider-configuration-classes/clientnetlibinfo-class/clientnetlibinfo-class.md) | Returns information about installed client network libraries and protocols |
| [ClientNetworkProtocol](../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocol-class/clientnetworkprotocol-class.md) | Represents a specific client network protocol configuration (such as TCP/IP or Named Pipes) |
| [ClientNetworkProtocolProperty](../../relational-databases/wmi-provider-configuration-classes/clientnetworkprotocolproperty-class/clientnetworkprotocolproperty-class.md) | Exposes protocol-level settings for client network protocols |
| [ClientSettings](../../relational-databases/wmi-provider-configuration-classes/clientsettings-class.md) | Provides high-level client configuration, including resolution behavior and defaults |
| [ClientSettingsGeneralFlag](../../relational-databases/wmi-provider-configuration-classes/clientsettingsgeneralflag-class/clientsettingsgeneralflag-class.md) | Represents general client flags used to toggle or control client features |

## Server Networking & Protocol Configuration

These classes control server-side networking options such as protocol enablement, IP bindings, and advanced communication properties.

| Class | Description |
|-------|-------------|
| [ServerNetworkProtocol](../../relational-databases/wmi-provider-configuration-classes/servernetworkprotocol-class/servernetworkprotocol-class.md) | Represents a server-side network protocol (TCP/IP, Named Pipes, Shared Memory) |
| [ServerNetworkProtocolIPAddress](../../relational-databases/wmi-provider-configuration-classes/servernetworkprotocolipaddress-class/servernetworkprotocolipaddress-class.md) | Represents IP address-specific bindings and configuration for SQL Server network protocols |
| [ServerNetworkProtocolProperty](../../relational-databases/wmi-provider-configuration-classes/servernetworkprotocolproperty-class/servernetworkprotocolproperty-class.md) | Exposes protocol-level configurable settings such as ports and packet behavior |

## Server settings, flags, and instance metadata

These classes provide information you can use to query or control SQL Server instance-level settings, flags, and server environment details.

| Class | Description |
|-------|-------------|
| [ServerSettings](../../relational-databases/wmi-provider-configuration-classes/serversettings-class/serversettings-class.md) | Defines general SQL Server instance configuration settings |
| [ServerSettingsGeneralFlag](../../relational-databases/wmi-provider-configuration-classes/serversettingsgeneralflag-class/serversettingsgeneralflag-class.md) | Represents configurable flags that enable or disable server-level features |
| [CInstance](../../relational-databases/wmi-provider-configuration-classes/cinstance-class.md) | Provides details for clustered SQL Server instances in failover cluster environments |
| [SInstance](../../relational-databases/wmi-provider-configuration-classes/sinstance-class/sinstance-class.md) | Represents a standalone SQL Server instance, including identity and configuration metadata |

## SQL Server services and alias management

Use these classes to manage SQL Server services, such as starting, stopping, and configuring them. You can also manage client and server alias definitions.

| Class | Description |
|-------|-------------|
| [SqlService](../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/sqlservice-class.md) | Represents a SQL-related Windows service, such as SQL Server, SQL Agent, or Browser, and allows you to manage its state |
| [SqlServiceAdvancedProperty](../../relational-databases/wmi-provider-configuration-classes/sqlserviceadvancedproperty-class/sqlserviceadvancedproperty-class.md) | Exposes advanced service configuration properties |
| [SqlServerAlias](../../relational-databases/wmi-provider-configuration-classes/sqlserveralias-class/sqlserveralias-class.md) | Represents client-side alias definitions for redirecting SQL Server connections |

## SQL Server error log and diagnostic classes

These classes expose SQL Server error log metadata and error events. By using them, you can monitor or automate log parsing.

| Class | Description |
|-------|-------------|
| [SqlErrorLogEvent](../../relational-databases/wmi-provider-configuration-classes/sqlerrorlogevent-class.md) | Represents individual events recorded in the SQL Server error log |
| [SqlErrorLogFile](../../relational-databases/wmi-provider-configuration-classes/sqlerrorlogfile-class.md) | Provides metadata about SQL Server error log files, including paths, sizes, and rollover behavior |

## Security and certificate configuration

These classes support client and server certificate management for encrypted connections.

| Class | Description |
|-------|-------------|
| [SecurityCertificate](../../relational-databases/wmi-provider-configuration-classes/securitycertificate-class/securitycertificate-class.md) | Represents certificates that SQL Server uses for encrypted network communication |

## Related content

- [WMI Provider for Configuration Management](../wmi-provider-configuration/wmi-provider-for-configuration-management.md)

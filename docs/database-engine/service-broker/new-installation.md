---
title: "New Installation (Service Broker)"
description: "To install a Service Broker service, the developer gives the administrator a set of installation scripts."
author: adbadram
ms.author: adbadram
ms.reviewer: vanto
ms.date: 09/10/2025
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
---

# New installation (Service Broker)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

To install a Service Broker service, the developer gives the administrator a set of installation scripts. These scripts typically include the Transact-SQL statements that are required to create the message types, contracts, queues, services, and stored procedures for the service. Depending on the service, the developer might provide one set of scripts for the target service and a different set of scripts for the initiating service.

## Installation process

First, the administrator reviews and executes the scripts. Then, the administrator configures the security principals, certificates, remote service bindings, and routes required for the application to work in a production environment.

The development or test environment might contain the same user names as the production environment, but have different certificates associated with those users. This difference in certificates provides an extra degree of isolation between the test environment and the production environment, without requiring the Transact-SQL code to change for deployment. Developers can test the exact code to be used in production without requiring that the administrator provide the certificates used in the production environment.

## Plan to uninstall Service Broker applications

As part of the installation process, the developer and the administrator should plan and document the procedure to uninstall the application. Applications that use Service Broker typically rely on Service Broker's guarantee of reliable messaging. Therefore, the developer and the administrator must work together to outline a strategy for making sure that the application processes all of the messages it has received before the administrator uninstalls the application.

## Related content

- [Uninstall Service Broker applications](uninstalling-service-broker-applications.md)

---
title: Application Development Overview
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: Learn about available connectivity libraries and best practices for applications connecting to Azure SQL Database and Azure SQL Managed Instance.
author: cwebb-msft
ms.author: chweb
ms.reviewer: wiassaf
ms.date: 05/26/2026
ms.service: azure-sql
ms.subservice: development
ms.topic: concept-article
ms.custom:
  - sqldbrb=2
  - ignite-2024
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi || =fabricsql"
---
# Application development overview - Azure SQL Database & Azure SQL Managed Instance

[!INCLUDE [appliesto-sqldb-sqlmi-fabricsqldb](../includes/appliesto-sqldb-sqlmi-fabricsqldb.md)]

This article walks through the basic considerations that a developer should be aware of when writing code to connect to your database in Azure. This article applies to Azure SQL Database, and Azure SQL Managed Instance.

## Language and platform

You can use various [programming languages and platforms](connect-query-content-reference-guide.md) to connect and query Azure SQL Database. You can find [sample applications](https://azure.microsoft.com/resources/samples/?service=sql-database&sort=0) that you can use to connect to the database.

You can use open-source tools like [cheetah](https://github.com/wunderlist/cheetah), [sql-cli](https://www.npmjs.com/package/sql-cli), [VS Code](https://code.visualstudio.com/). Additionally, Azure SQL Database works with Microsoft tools like [Visual Studio](https://visualstudio.microsoft.com/downloads) and  [SQL Server Management Studio](/sql/ssms/sql-server-management-studio-ssms). You can also use the Azure portal, PowerShell, and REST APIs to help you gain more productivity.

## Authentication

Access to Azure SQL Database is protected with logins and firewalls. Azure SQL Database and SQL Managed Instance support users and logins for both SQL authentication and [authentication](authentication-aad-overview.md) with Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)).

Learn more about [managing database access and logins](logins-create-manage.md).

## Client connections

In your client connection logic, override the default timeout to be 30 seconds. The default of 15 seconds is too short for connections that depend on the internet.

If you're using a [connection pool](/dotnet/framework/data/adonet/sql-server-connection-pooling), be sure to close the connection the instant your program isn't actively using it, and isn't preparing to reuse it.

Avoid long-running transactions because any infrastructure or connection failure might roll back the transaction. If possible, split the transaction in the multiple smaller transactions and use [batching to improve performance](../performance-improve-use-batching.md).

It's possible to connect your application to your Azure SQL resource by using the following languages: 

- [.NET with Visual Studio](connect-query-dotnet-visual-studio.md)
- [.NET with Windows, Linux, and macOS](connect-query-dotnet-core.md)
- [Go](connect-query-go.md)
- [Node.js](connect-query-nodejs.md)
- [PHP](connect-query-php.md)
- [Python](connect-query-python.md)
- [Ruby](connect-query-ruby.md)

It's possible to configure Microsoft Entra authentication to your Azure SQL resource. Review the following articles for more information: 

- [Connect to Azure SQL with Microsoft Entra authentication and SqlClient](/sql/connect/ado-net/sql/azure-active-directory-authentication)
- [Managed identities in Microsoft Entra for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md)
- [Connect to SQL Database from .NET App Service without secrets using a managed identity](/azure/app-service/tutorial-connect-msi-sql-database)

## Resiliency

Azure SQL Database is a cloud service where you might expect transient errors that happen in the underlying infrastructure or in the communication between cloud entities. Although Azure SQL Database is resilient on the transitive infrastructure failures, any network infrastructure failures could briefly affect your connectivity. When a transient error occurs while connecting to SQL Database, your code should [retry the call](troubleshoot-common-connectivity-issues.md#retry-logic-for-transient-errors). 

We recommend that retry logic always retry after delay, using backoff logic, so that it doesn't overwhelm the service with multiple clients retrying simultaneously. Retry logic depends on the [error messages for SQL Database client programs](troubleshoot-common-errors-issues.md#list-of-transient-fault-error-codes).

For more information on retry logic after delay:

- [Azure Architecture Center: Retry pattern](/azure/architecture/patterns/retry)
- [Troubleshoot transient connection errors](troubleshoot-common-connectivity-issues.md?view=azuresql-db&preserve-view=true#retry-logic-for-transient-errors)
- [Configurable retry logic in Microsoft.Data.SqlClient](/sql/connect/ado-net/configurable-retry-logic-sqlclient-introduction?view=azuresqldb-current&preserve-view=true)
- [DevBlog: Introducing Configurable Retry Logic in Microsoft.Data.SqlClient](https://devblogs.microsoft.com/azure-sql/configurable-retry-logic-for-microsoft-data-sqlclient/)

For more information about how to prepare for planned maintenance events on your Azure SQL Database, see [planning for Azure maintenance events in Azure SQL Database](planned-maintenance.md).

## Network considerations

- On the computer that hosts your client program, ensure the firewall allows outgoing TCP communication on port 1433. More information: [Azure SQL Database IP firewall rules](firewall-configure.md).
- If your client program connects to SQL Database while your client runs on an Azure virtual machine (VM), you must open certain port ranges on the VM. More information: [Ports beyond 1433 for ADO.NET 4.5](adonet-v12-develop-direct-route-ports.md).
- Client connections to Azure SQL Database sometimes bypass the proxy and interact directly with the database. Ports other than 1433 become important. For more information, [Connectivity architecture](connectivity-architecture.md) and [Ports beyond 1433 for ADO.NET 4.5](adonet-v12-develop-direct-route-ports.md).
- For networking configuration for an instance of SQL Managed Instance, see [network configuration for SQL Managed Instance](../managed-instance/how-to-content-reference-guide.md#network-configuration).

## Related content

Explore all the capabilities of [SQL Database](sql-database-paas-overview.md) and [SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md).

To get started, see the guides for [Azure SQL Database](quickstart-content-reference-guide.md) and [Azure SQL Managed Instances](../managed-instance/quickstart-content-reference-guide.md).

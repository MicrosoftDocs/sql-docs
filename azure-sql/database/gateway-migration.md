---
title: Gateway traffic migration notice
description: Article provides notice to users about the migration of Azure SQL Database gateway IP addresses
author: rohitnayakmsft
ms.author: rohitna
ms.reviewer: wiassaf, vanto, mathoma
ms.date: 02/14/2024
ms.service: azure-sql-database
ms.subservice: service-overview
ms.topic: conceptual
ms.custom:
  - sqldbrb=1
---
# Azure SQL Database traffic migration to newer gateways

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

Microsoft periodically refreshes hardware to optimize the customer experience. During refreshes, Azure adds gateways built on newer hardware, migrates traffic to them, and eventually decommissions gateways built on older hardware in some regions.

To avoid service disruptions during refreshes, allow network traffic to reach **both** the individual Gateway IP addresses and Gateway IP address subnets in a region. Review [SQL Gateway IP subnet ranges](connectivity-architecture.md#gateway-ip-addresses) and include the ranges for your region.

Customers can [use the Azure portal to set up activity log alerts](/azure/service-health/alerts-activity-log-service-notifications-portal).

## Impact of this change

Traffic migration might change the public IP address that DNS resolves for your database in Azure SQL Database.

You might be impacted if you:

- Hard coded the IP address for any particular gateway in your on-premises firewall
- Have any subnets using Microsoft.SQL as a Service Endpoint but can't communicate with the gateway IP addresses
- Use the [zone redundant configuration for General Purpose tier](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability)
- Use the [zone redundant configuration for Premium & Business Critical tiers](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability)

You won't be impacted if you have:

- Redirection as the connection policy
- Connections to SQL Database from inside Azure and using Service Tags
- Connections made using supported versions of JDBC Driver for Azure SQL will see no impact. For supported JDBC versions, see [Download Microsoft JDBC Driver for SQL Server](/sql/connect/jdbc/download-microsoft-jdbc-driver-for-sql-server).

## What to do you do if you're affected

We recommend that you allow outbound traffic to IP addresses for all the [gateway IP addresses](connectivity-architecture.md#gateway-ip-addresses) in the region on TCP port 1433. Also, allow port range 11000 thru 11999 when connecting from a client located within Azure (for example, an Azure VM) or when your Connection Policy is set to Redirection. This recommendation is applicable to clients connecting from on-premises and also those connecting via Service Endpoints. For more information on port ranges, see [Connection policy](connectivity-architecture.md#connection-policy).

Connections made from applications using Microsoft JDBC Driver below version 4.0 might fail certificate validation. Lower versions of Microsoft JDBC rely on Common Name (CN) in the Subject field of the certificate. The mitigation is to ensure that the `hostNameInCertificate` property is set to `*.database.windows.net`. For more information on how to set the hostNameInCertificate property, see [Connecting with Encryption](/sql/connect/jdbc/connecting-with-ssl-encryption).

If the above mitigation doesn't work, file a support request for SQL Database or SQL Managed Instance using the following URL: https://aka.ms/getazuresupport

## Next step

> [!div class="nextstepaction"]
> [Azure SQL Database and Azure Synapse Analytics connectivity architecture](connectivity-architecture.md)

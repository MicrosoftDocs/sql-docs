---
title: Azure Resource Manager templates
description: Use Azure Resource Manager templates to create and configure Azure SQL Database.
author: urosmil
ms.author: urmilano
ms.reviewer: wiassaf, mathoma
ms.date: 04/30/2024
ms.service: sql-database
ms.subservice: deployment-configuration
ms.topic: conceptual
ms.custom: overview-samples, azure-sql-split, devx-track-arm-template
monikerRange: "= azuresql || = azuresql-db"
---

# Azure Resource Manager templates for Azure SQL Database
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](arm-templates-content-guide.md?view=azuresql&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/arm-templates-content-guide.md?view=azuresql&preserve-view=true)

Azure Resource Manager templates enable you to define your infrastructure as code and deploy your solutions to the Azure cloud for Azure SQL Database.

## Templates

The following table includes links to Azure Resource Manager templates for Azure SQL Database.

|Link |Description|
|---|---|
| [SQL Database](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.sql/sql-database-transparent-encryption-create) | This Azure Resource Manager template creates a single database in Azure SQL Database and configures server-level IP firewall rules. |
| [Server](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.sql/sql-logical-server) | This Azure Resource Manager template creates a server for Azure SQL Database. |
| [Elastic pool](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.sql/sql-elastic-pool-create) | This template allows you to deploy an elastic pool and to assign databases to it. |
| [Failover groups](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.sql/sql-with-failover-group) | This template creates two servers, a single database, and a failover group in Azure SQL Database.|
| [Auditing to Azure Blob storage](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.sql/sql-auditing-server-policy-to-blob-storage) | This template allows you to deploy a server with auditing enabled to write audit logs to a Blob storage. Auditing for Azure SQL Database tracks database events and writes them to an audit log that can be placed in your Azure storage account, OMS workspace, or Event Hubs.|
| [Auditing to Azure Event Hubs](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.sql/sql-auditing-server-policy-to-eventhub) | This template allows you to deploy a server with auditing enabled to write audit logs to an existing event hub. In order to send audit events to Event Hubs, set auditing settings with `Enabled` `State`, and set `IsAzureMonitorTargetEnabled` as `true`. Also, configure Diagnostic Settings with the `SQLSecurityAuditEvents` log category on the `master` database (for server-level auditing). Auditing tracks database events and writes them to an audit log that can be placed in your Azure storage account, OMS workspace, or Azure Event Hubs.|
| [Azure Web App with SQL Database](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.web/web-app-sql-database) | This sample creates a free Azure web app and a database in Azure SQL Database at the "Basic" service level.|
| [Azure Web App and Redis Cache with SQL Database](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.web/web-app-redis-cache-sql-database) | This template creates a web app, Redis Cache, and database in the same resource group and creates two connection strings in the web app for the database and Redis Cache.|
| [Import data from Blob storage using ADF V2](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.datafactory/data-factory-v2-blob-to-sql-copy) | This Azure Resource Manager template creates an instance of Azure Data Factory V2 that copies data from Azure Blob storage to SQL Database.|
| [HDInsight cluster with a database](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.hdinsight/hdinsight-linux-with-sql-database) | This template allows you to create an HDInsight cluster, a logical SQL server, a database, and two tables. This template is used by the [Use Sqoop with Hadoop in HDInsight article](/azure/hdinsight/hadoop/hdinsight-use-sqoop). |
| [Azure Logic App that runs a SQL Stored Procedure on a schedule](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.logic/logic-app-sql-proc) | This template allows you to create a logic app that will run a SQL stored procedure on schedule. Any arguments for the procedure can be put into the body section of the template.|
| [Provision server with Microsoft Entra-only authentication enabled](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.sql/sql-logical-server-aad-only-auth) | This template creates a [logical server in Azure](logical-servers.md) with a Microsoft Entra admin configured for the server and Microsoft Entra-only authentication enabled. |



## Related content

- [ARM templates](/azure/azure-resource-manager/templates/overview)

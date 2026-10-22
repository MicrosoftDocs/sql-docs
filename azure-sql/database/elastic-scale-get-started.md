---
title: Get Started with Elastic Database Tools
description: Basic explanation of the Elastic Database Tools feature of Azure SQL Database, including an easy-to-run sample app.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: bgavrilovic
ms.date: 06/13/2025
ms.service: azure-sql-database
ms.subservice: scale-out
ms.topic: how-to
ms.custom:
  - sqldbrb=1
monikerRange: "=azuresql || =azuresql-db "
---
# Get started with Elastic Database Tools

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

[!INCLUDE [elastic-query-shard-map-manager-mode-end-of-support](includes/elastic-query-shard-map-manager-mode-end-of-support.md)]

This article introduces you to the developer experience for the [elastic database client library](elastic-database-client-library.md) by helping you run a sample app. The sample app creates a simple sharded application and explores key capabilities of the Elastic Database Tools feature of Azure SQL Database. Learn more about the use cases for [shard map management](elastic-scale-shard-map-management.md), [data-dependent routing](elastic-scale-data-dependent-routing.md), and [multi-shard querying](elastic-scale-multishard-querying.md). The client library is available for .NET as well as Java.

## Elastic Database Tools for Java

### Prerequisites

- A Java Developer Kit (JDK), version 1.8 or later
- [Maven](https://maven.apache.org/download.cgi)
- SQL Database or a local SQL Server instance

### Download and run the sample app

To build the JAR files and get started with the sample project, do the following:

1. Clone the [GitHub repository](https://github.com/Microsoft/elastic-db-tools-for-java) containing the client library, along with the sample app.

1. Edit the `./sample/src/main/resources/resource.properties` file to set the following:
    * TEST_CONN_USER
    * TEST_CONN_PASSWORD
    * TEST_CONN_SERVER_NAME

1. To build the sample project, in the `./sample` directory, run the following command:

    ```cmd
    mvn install
    ```

1. To start the sample project, in the `./sample` directory, run the following command:

    ```cmd
    mvn -q exec:java "-Dexec.mainClass=com.microsoft.azure.elasticdb.samples.elasticscalestarterkit.Program"
    ```

1. To learn more about the client library capabilities, experiment with the various options. Feel free to explore the code to learn about the sample app implementation.

    :::image type="content" source="media/elastic-scale-get-started/java-client-library.png" alt-text="Screenshot of the Java Client Library Elastic Database tools starter kit.":::

Congratulations! You have successfully built and run your first sharded application by using Elastic Database Tools on Azure SQL Database. Use Visual Studio or SQL Server Management Studio to connect to your database and take a quick look at the shards that the sample created. You will notice new sample shard databases and a shard map manager database that the sample has created.

To add the client library to your own Maven project, add the following dependency in your POM file:

```xml
<dependency>
    <groupId>com.microsoft.azure</groupId>
    <artifactId>elastic-db-tools</artifactId>
    <version>1.0.0</version>
</dependency>
```

## Elastic Database Tools for .NET

### Prerequisites

- Visual Studio 2012 or later with C#. Download a free version at [Visual Studio Downloads](https://visualstudio.microsoft.com/downloads).
- NuGet 2.7 or later. To get the latest version, see [Installing NuGet](/nuget/install-nuget-client-tools).

### Download and run the sample app

To install the library, go to [Microsoft.Azure.SqlDatabase.ElasticScale.Client](https://www.nuget.org/packages/Microsoft.Azure.SqlDatabase.ElasticScale.Client/). The library is installed with the sample app that's described in the following section.

To download and run the sample, follow these steps:

1. Download the [Elastic DB Tools for Azure SQL - Getting Started sample](https://github.com/Azure/elastic-db-tools). Unzip the sample to a location that you choose.

1. To create a project, open the `ElasticDatabaseTools.sln` solution from the `elastic-db-tools-master` directory. 

1. Set the `ElasticScaleStarterKit` project as the Startup Project.

1. In the `ElasticScaleStarterKit` project, open the `App.config` file. Then follow the instructions in the file to add your server name and your sign in information (username and password).

1. Build and run the application. When you are prompted, enable Visual Studio to restore the NuGet packages of the solution. This action downloads the latest version of the elastic database client library from NuGet.

1. To learn more about the client library capabilities, experiment with the various options. Note the steps that the application takes in the console output, and feel free to explore the code behind the scenes.

   :::image type="content" source="media/elastic-scale-get-started/elastic-scale-starter-kit-terminal.png" alt-text="Screenshot of the Elastic Scale starter kit terminal.":::

Congratulations! You have successfully built and run your first sharded application by using Elastic Database Tools on SQL Database. Use Visual Studio or SQL Server Management Studio to connect to your database and take a quick look at the shards that the sample created. You will notice new sample shard databases and a shard map manager database that the sample has created.

> [!IMPORTANT]
> We recommend that you always use the latest version of Management Studio so that you stay synchronized with updates to Azure and SQL Database. [Update SQL Server Management Studio](/ssms/install/install).

## Key pieces of the code sample

- **Managing shards and shard maps**: The code illustrates how to work with shards, ranges, and mappings in the `ShardManagementUtils.cs` file. For more information, see [Scale out databases with the shard map manager](elastic-scale-shard-map-management.md).  

- **Data-dependent routing**: Routing of transactions to the right shard is shown in the `DataDependentRoutingSample.cs` file. For more information, see [Use data-dependent routing to route a query to an appropriate database](elastic-scale-data-dependent-routing.md).

- **Querying over multiple shards**: Querying across shards is illustrated in the `MultiShardQuerySample.cs` file. For more information, see [Multi-shard querying using elastic database tools](elastic-scale-multishard-querying.md).

- **Adding empty shards**: The iterative adding of new empty shards is performed by the code in the `CreateShardSample.cs` file. For more information, see [Scale out databases with the shard map manager](elastic-scale-shard-map-management.md).

## Other elastic scale operations

- **Splitting an existing shard**: The capability to split shards is provided by the split-merge tool. For more information, see [Moving data between scaled-out cloud databases](elastic-scale-overview-split-and-merge.md).

- **Merging existing shards**: Shard merges are also performed by using the split-merge tool. For more information, see [Moving data between scaled-out cloud databases](elastic-scale-overview-split-and-merge.md).

## Cost

The Elastic Database Tools library is free. When you use Elastic Database Tools, you incur no additional charges beyond the cost of your Azure usage.

For example, the sample application creates new databases. The cost of this capability depends on the SQL Database edition you choose and the Azure usage of your application.

For pricing information, see [SQL Database pricing details](https://azure.microsoft.com/pricing/details/sql-database/).

## Related content

- [.NET](https://github.com/Azure/elastic-db-tools)
- [Java](https://search.maven.org/#search%7Cga%7C1%7Ca%3A%22azure-elasticdb-tools%22)
- [Blog: Elastic Scale announcement](https://azure.microsoft.com/blog/introducing-elastic-scale-preview-for-azure-sql-database/)
- [Building scalable cloud databases](elastic-database-client-library.md)

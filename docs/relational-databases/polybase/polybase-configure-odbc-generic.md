---
title: "Access external data: ODBC generic types - PolyBase"
description: PolyBase in SQL Server allows you to connect to compatible data sources through the ODBC connector. Install the ODBC driver and create external tables.
ms.date: 02/19/2020
ms.custom: seo-lt-2019
ms.prod: sql
ms.technology: polybase
ms.topic: conceptual
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mikeray
monikerRange: ">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"
---
# Configure PolyBase to access external data with ODBC generic types

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

PolyBase in SQL Server 2019 allows you to connect to ODBC -compatible data sources through the ODBC connector.

This article provides some examples using an ODBC driver. Check with your ODBC provider for specific examples. Reference the ODBC driver documentation for the data source to determine the appropriate connection string options. The examples in this article may not apply to any specific ODBC driver.

## Prerequisites

>[!NOTE]
>This feature requires SQL Server on Windows.

* [PolyBase installation](polybase-installation.md).

* Before creating a database scoped credential, a [Master Key](../../t-sql/statements/create-master-key-transact-sql.md) must be created.

## Install the ODBC driver

First, download and install the ODBC driver of the data source you want to connect to on each of the PolyBase nodes. Once the driver is properly installed, you can view and test the driver from the **ODBC Data Source Administrator**.

![PolyBase scale-out groups](../../relational-databases/polybase/media/polybase-odbc-admin.png) 

In the example above, the name of the driver is circled in red. Use this name when you create the external data source.

> [!IMPORTANT]
> In order to improve query performance, enable connection pooling. This can be accomplished from the **ODBC Data Source Administrator**.

## Create an external table

To query the data from an ODBC data source, you must create external tables to reference the external data. This section provides sample code to create external tables.

The following Transact-SQL commands are used in this section:

* [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)
* [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md) 
* [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md)

1. Create a database scoped credential for accessing the ODBC source.

    ```sql
    CREATE DATABASE SCOPED CREDENTIAL <credential_name> WITH IDENTITY = '<username>', Secret = '<password>';
    ```

    For example, the following example creates a credential named `credential_name`, with an identity of `username` and a complex password.

    ```sql
    CREATE DATABASE SCOPED CREDENTIAL credential_name WITH IDENTITY = 'username', Secret = 'BycA4ZjrE#*2W%!';
    ```

1. Create an external data source with [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md).

    ```sql
    CREATE EXTERNAL DATA SOURCE <external_data_source_name>
    WITH ( LOCATION = 'odbc://<ODBC server address>[:<port>]',
    CONNECTION_OPTIONS = 'Driver={<Name of Installed Driver>};
    ServerNode = <name of server  address>:<Port>',
    -- PUSHDOWN = [ON] | OFF,
    CREDENTIAL = <credential_name> );
    ```

    The following example creates an external data source:
    * Named `external_data_source_name`
    * Located at the ODBC `SERVERNAME` and port `4444`
    * Connecting with `CData ODBC Driver For SAP 2015` - This is the driver created under [Install the ODBC driver](#install-the-odbc-driver)
    * On `ServerNode` `sap_server_node` port `5555`
    * Configured for processing pushed down to the server (`PUSHDOWN = ON`)
    * Using the `credential_name` credential

    ```sql
    CREATE EXTERNAL DATA SOURCE external_data_source_name
    WITH ( LOCATION = 'odbc://SERVERNAME:4444',
    CONNECTION_OPTIONS = 'Driver={CData ODBC Driver For SAP 2015};
    ServerNode = sap_server_node:5555',
    PUSHDOWN = ON,
    CREDENTIAL = credential_name );
    ```

1. **Optional:** Create statistics on an external table.

    For optimal query performance, we recommend creating statistics on external table columns especially the ones used for joins, filters, and aggregates.

    ```sql
    CREATE STATISTICS statistics_name ON customer (C_CUSTKEY) WITH FULLSCAN; 
    ```

>[!IMPORTANT]
>Once you have created an external data source, use the [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md) command to create a query-able table over that source.

## Next steps

To learn more about PolyBase, see [Overview of SQL Server PolyBase](polybase-guide.md).

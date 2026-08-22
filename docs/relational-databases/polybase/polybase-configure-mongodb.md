---
title: "Access External Data: MongoDB - PolyBase"
description: The article explains how to use PolyBase on a SQL Server instance to query external data in MongoDB. Create external tables to reference the external data.
author: markingmyname
ms.author: maghan
ms.reviewer: hudequei, randolphwest
ms.date: 01/28/2026
ms.service: sql
ms.subservice: polybase
ms.topic: how-to
monikerRange: ">=sql-server-linux-ver15 || >=sql-server-ver15"
---
# Configure PolyBase to access external data in MongoDB

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article explains how to use PolyBase on a SQL Server instance to query external data in MongoDB.

## Prerequisites

Install [Install PolyBase on Windows](polybase-installation.md).

Before you create a database scoped credential, create a database master key (DMK) to protect the credential. For more information, see [CREATE MASTER KEY](../../t-sql/statements/create-master-key-transact-sql.md).

## Configure a MongoDB external data source

To query data from a MongoDB data source, you must create external tables to reference the external data. This section provides sample code to create these external tables.

The following Transact-SQL commands are used in this section:

- [CREATE DATABASE SCOPED CREDENTIAL](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)
- [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md)
- [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md)
- [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md)

1. Create a database scoped credential for accessing the MongoDB source.

   The following script creates a database scoped credential. Before you run the script, update it for your environment:

   - Replace `<credential_name>` with a name for the credential.
   - Replace `<username>` with the user name for the external source.
   - Replace `<password>` with the appropriate password.

   ```sql
   CREATE DATABASE SCOPED CREDENTIAL [<credential_name>]
   WITH IDENTITY = '<username>',
        SECRET = '<password>';
   ```

   > [!IMPORTANT]  
   > The MongoDB ODBC Connector for PolyBase supports only basic authentication, not Kerberos authentication.

1. Create an external data source.

   The following script creates the external data source. For reference, see [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md). Before you run the script, update it for your environment:

   - Update the location. Set the `<server>` and `<port>` for your environment.
   - Replace `<credential_name>` with the name of the credential you created in the previous step.
   - Optionally, specify `PUSHDOWN = ON` or `PUSHDOWN = OFF` if you want to specify pushdown computation to the external source.

   ```sql
   CREATE EXTERNAL DATA SOURCE external_data_source_name
   WITH (LOCATION = '<mongodb://<server>[:<port>]>'
   [ [ , ] CREDENTIAL = <credential_name> ]
   [ [ , ] CONNECTION_OPTIONS = '<key_value_pairs>'[,...]]
   [ [ , ] PUSHDOWN = { ON | OFF } ])
   [ ; ]
   ```

1. Query the external schema in MongoDB.

   Use [sp_data_source_objects](../system-stored-procedures/sp-data-source-objects.md) to detect the collection schema (columns) for MongoDB collections that contain arrays, and manually create the external table. The `sp_data_source_table_columns` stored procedure also automatically performs the flattening via the PolyBase ODBC Driver for MongoDB driver.

1. Create an external table.

   To provide the schema manually, consider the following sample script to create an external table. For reference, see [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md).

   Before you run the script, update it for your environment:

   - Update the fields with their name, collation, and if they are collections, specify the collection name and the field name. In the example, `friends` is a custom data type.

   - Update the location. Set the database name and the table name. Three-part names aren't allowed, so you can't create it for the `system.profile` table. Also, you can't specify a view because it can't obtain the metadata from it.

   - Update the data source with the name of the one you created in the previous step.

   ```sql
   CREATE EXTERNAL TABLE [MongoDbRandomData]
   (
       [_id] NVARCHAR (24) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
       [RandomData_friends_id] INT,
       [RandomData_tags] NVARCHAR (MAX) COLLATE SQL_Latin1_General_CP1_CI_AS
   )
   WITH (
       DATA_SOURCE = [MongoDb],
       LOCATION = 'MyDb.RandomData'
   );
   ```

1. **Optional**: Create statistics on an external table.

   Create statistics on external table columns, especially the ones used for joins, filters, and aggregates, for optimal query performance.

   ```sql
   CREATE STATISTICS statistics_name
       ON customer(C_CUSTKEY)
       WITH FULLSCAN;
   ```

## MongoDB connection options

For information about MongoDB connection options, see [MongoDB documentation: Connection String URI Format](https://docs.mongodb.com/manual/reference/connection-string/#connection-string-options).

## Flattening

Flattening is enabled on nested and repeated data from MongoDB document collections. You need to enable `create an external table` and explicitly specify a relational schema over MongoDB document collections that might have nested or repeated data.

JSON nested and repeated data types are flattened as follows:

- Object: unordered key/value collection enclosed in curly braces (nested)

  - SQL Server creates a table column for each object key

    - Column name: `<objectname>_<keyname>`

- Array: ordered values, separated by commas, enclosed in square brackets (repeated)

  - SQL Server adds a new table row for each array item

  - SQL Server creates a column per array to store the array item index

    - Column name: `<arrayname>_index`

    - Data Type: **bigint**

This technique can cause several issues, including:

- An empty repeated field masks the data in the flat fields of the same record.

- Multiple repeated fields increase the number of produced rows.

For example, SQL Server evaluates the MongoDB sample dataset restaurant collection stored in non-relational JSON format. Each restaurant has a nested address field and an array of grades it was assigned on different days. The following image shows a typical restaurant with nested address and nested-repeated grades.

:::image type="content" source="media/mongo-flattening.png" alt-text="Screenshot of MongoDB flattening.":::

Object addresses are flattened as follows:

- Nested field `restaurant.address.building` becomes `restaurant.address_building`
- Nested field `restaurant.address.coord` becomes `restaurant.address_coord`
- Nested field `restaurant.address.street` becomes `restaurant.address_street`
- Nested field `restaurant.address.zipcode` becomes `restaurant.address_zipcode`

Array grades are flattened as follows:

| grades_date | grades_grade | games_score |
| --- | --- | --- |
| `1393804800000` | A | 2 |
| `1378857600000` | A | 6 |
| `135898560000` | A | 10 |
| `1322006400000` | A | 9 |
| `1299715200000` | B | 14 |

## Cosmos DB connection

You can use the Cosmos DB Mongo API and the MongoDB PolyBase connector to create an external table for a **Cosmos DB instance**. Follow the same steps described earlier. Make sure the database scoped credential, server address, port, and location string match the Cosmos DB server.

## Examples

The following example creates an external data source with the following parameters:

| Parameter | Value |
| --- | --- |
| **Name** | `external_data_source_name` |
| **Service** | `mongodb0.example.com` |
| **Instance** | `27017` |
| **Replica set** | `myRepl` |
| **TLS** | `true` |
| **Pushdown computation** | `ON` |

```sql
CREATE EXTERNAL DATA SOURCE external_data_source_name
WITH (
    LOCATION = 'mongodb://mongodb0.example.com:27017',
    PUSHDOWN = ON,
    CONNECTION_OPTIONS = 'replicaSet = myRepl; tls = true',
    CREDENTIAL = credential_name
);
```

## Related content

- [PolyBase Transact-SQL reference](polybase-t-sql-objects.md)
- [PolyBase overview](overview.md)

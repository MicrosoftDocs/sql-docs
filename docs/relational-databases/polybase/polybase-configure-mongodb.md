---
title: "Access external data: MongoDB - PolyBase"
description: The article explains how to use PolyBase on a SQL Server instance to query external data in MongoDB. Create external tables to reference the external data.
ms.date: 06/06/2022
ms.metadata: seo-lt-2019
ms.service: sql
ms.subservice: polybase
ms.topic: conceptual
ms.custom:
- event-tier1-build-2022
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
monikerRange: ">= sql-server-linux-ver15 || >= sql-server-ver15"
---
# Configure PolyBase to access external data in MongoDB

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The article explains how to use PolyBase on a SQL Server instance to query external data in MongoDB.

## Prerequisites

If you haven't installed PolyBase, see [PolyBase installation](polybase-installation.md).

Before you create a database scoped credential, the database must have a master key to protect the credential. For more information, see [CREATE MASTER KEY](../../t-sql/statements/create-master-key-transact-sql.md).

## Configure a MongoDB external data source

To query the data from a MongoDB data source, you must create external tables to reference the external data. This section provides sample code to create these external tables.

The following Transact-SQL commands are used in this section:

- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)
- [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md)
- [CREATE EXTERNAL TABLE (Transact-SQL)](../../t-sql/statements/create-external-table-transact-sql.md)
- [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md)

1. Create a database scoped credential for accessing the MongoDB source.

   The following script creates a database scoped credential. Before you run the script update it for your environment:

    - Replace `<credential_name>` with a name for the credential.
    - Replace `<username>` with the user name for the external source.
    - Replace `<password>` with the appropriate password. 

    ```sql
    CREATE DATABASE SCOPED CREDENTIAL [<credential_name>] WITH IDENTITY = '<username>', Secret = '<password>';
    ```

   > [!IMPORTANT]
   > The MongoDB ODBC Connector for PolyBase supports only basic authentication, not Kerberos authentication.

1. Create an external data source.

    The following script creates the external data source. For reference, see [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md). Before you run the script update it for your environment:

    - Update the location. Set the `<server>` and `<port>` for your environment.
    - Replace `<credential_name>` with the name of the credential you created in the previous step.
    - Optionally you can specify `PUSHDOWN = ON` or `PUSHDOWN = OFF` if you want to specify pushdown computation to the external source.

    ```sql
    CREATE EXTERNAL DATA SOURCE external_data_source_name
    WITH (LOCATION = '<mongodb://<server>[:<port>]>'
    [ [ , ] CREDENTIAL = <credential_name> ]
    [ [ , ] CONNECTION_OPTIONS = '<key_value_pairs>'[,...]]
    [ [ , ] PUSHDOWN = { ON | OFF } ])
    [ ; ]
    ```

1. Query the external schema in MongoDB.

    You can use the [Data Virtualization extension for Azure Data Studio](../../azure-data-studio/extensions/data-virtualization-extension.md) to connect to and generate a CREATE EXTERNAL TABLE statement based on the schema detected by the PolyBase ODBC Driver for MongoDB driver. You can also manually customize a script based on the output of the system stored procedure [sp_data_source_objects (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-data-source-objects.md). The Data Virtualization extension for Azure Data Studio and `sp_data_source_table_columns` use the same internal stored procedures to query the external schema schema.

    To create external tables to MongoDB collections that contain arrays, the recommendation is to use [Data Virtualization extension for Azure Data Studio](../../azure-data-studio/extensions/data-virtualization-extension.md). The flattening actions are performed automatically by the driver. The `sp_data_source_table_columns` stored procedure also automatically performs the flattening via the PolyBase ODBC Driver for MongoDB driver.

1. Create an external table.

    If you use the [Data Virtualization extension for Azure Data Studio](../../azure-data-studio/extensions/data-virtualization-extension.md), you can skip this step, as the CREATE EXTERNAL TABLE statement is generated for you. To provide the schema manually, consider the following sample script to create an external table. For reference, see [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md). 

    Before you run the script, update it for your environment:

    - Update the fields with their name, collation, and if they are collections then specify the collection name and the field name. In the example, `friends` is a custom data type.
    - Update the location. Set the database name and the table name. Note three-part names are not allowed, so you can't create it for the `system.profile` table. Also you can't specify a view because it can't obtain the metadata from it.
    - Update the data source with the name of the one you created in the previous step.

    ```sql
    CREATE EXTERNAL TABLE [MongoDbRandomData](
      [_id] NVARCHAR(24) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
      [RandomData_friends_id] INT,
      [RandomData_tags] NVARCHAR(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS)
    WITH (
      LOCATION='MyDb.RandomData',
      DATA_SOURCE=[MongoDb])
    ```

1. **Optional:** Create statistics on an external table.

    We recommend creating statistics on external table columns, especially the ones used for joins, filters and aggregates, for optimal query performance.

    ```sql
    CREATE STATISTICS statistics_name ON customer (C_CUSTKEY) WITH FULLSCAN; 
    ```

>[!IMPORTANT]
>Once you have created an external data source, you can use the [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md) command to create a query-able table over that source.
>
>For an example, see [Create an external table for MongoDB](../../t-sql/statements/create-external-table-transact-sql.md#k-create-an-external-table-for-mongodb).

## MongoDB connection options

For information about MongoDB connection options, see [MongoDB documentation: Connection String URI Format](https://docs.mongodb.com/manual/reference/connection-string/#connection-string-options).

## Flattening

Flattening  is enabled for nested and repeated data from MongoDB document collections. User is required to enable `create an external table` and explicitly specify a relational schema over MongoDB document collections that may have nested and/or repeated data. 
JSON nested/repeated data types will be flattened as follows

* Object: unordered key/value collection enclosed in curly braces (nested)

   - SQL Server creates a table column for each object key

     * Column Name: objectname_keyname

* Array: ordered values, separated by commas, enclosed in square brackets (repeated)

   - SQL Server adds a new table row for each array item

   - SQL Server creates a column per array to store the array item index

     * Column Name: arrayname_index

     * Data Type: bigint

There are several potential issues with this technique, two of them being:

* An empty repeated field will effectively mask the data contained in the flat fields of the same record

* The presence of multiple repeated fields can result in an explosion of the number of produced rows

As an example, SQL Server evaluates the MongoDB sample dataset restaurant collection stored in non-relational JSON format. Each restaurant has a nested address field and an array of grades it was assigned on different days. The figure below illustrates a typical restaurant with nested address and nested-repeated grades.

![MongoDB flattening](../../relational-databases/polybase/media/mongo-flattening.png "MongoDB restaurant flattening")

Object address will be flattened as below:

- Nested field `restaurant.address.building` becomes `restaurant.address_building`
- Nested field `restaurant.address.coord` becomes `restaurant.address_coord`
- Nested field `restaurant.address.street` becomes `restaurant.address_street`
- Nested field `restaurant.address.zipcode` becomes `restaurant.address_zipcode`

Array grades will be flattened as below:

| grades_date | grades_grade  | games_score | 
| ------------- | ------------------------- | -------------- |
|1393804800000 |A |2|
|1378857600000|A |6|
|135898560000 |A |10|
|1322006400000|A |9|
|1299715200000 |B |14|

## Cosmos DB Connection

Using the Cosmos DB Mongo API and the Mongo DB PolyBase connector you can create an external table of a **Cosmos DB instance**. This accomplished by following the same steps listed above. Make sure the Database scoped credential, Server address, port, and location string reflect that of the Cosmos DB server.

## Examples

The following example creates an external data source with the following parameters:

| Parameter | Value|
|---|---|
| Name | `external_data_source_name`|
| Service | `mongodb0.example.com`|
| Instance | `27017`|
| Replica set | `myRepl`|
| TLS | `true`|
| Pushdown computation | `On`|

```sql
CREATE EXTERNAL DATA SOURCE external_data_source_name
    WITH (LOCATION = 'mongodb://mongodb0.example.com:27017',
    CONNECTION_OPTIONS = 'replicaSet=myRepl; tls=true',
    PUSHDOWN = ON ,
    CREDENTIAL = credential_name);
```

## Next steps

For more tutorials on creating external data sources and external tables to a variety of data sources, see [PolyBase Transact-SQL reference](polybase-t-sql-objects.md).

To learn more about PolyBase, see [Overview of SQL Server PolyBase](polybase-guide.md).

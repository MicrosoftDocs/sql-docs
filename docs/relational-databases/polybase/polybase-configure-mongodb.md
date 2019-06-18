---
title: "Configure PolyBase to access external data in MongoDB | Microsoft Docs"
ms.custom: ""
ms.date: 04/23/2019
ms.prod: sql
ms.technology: polybase
ms.topic: conceptual
author: Abiola
ms.author: aboke
ms.reviewer: jroth
manager: craigg
monikerRange: ">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"
---
# Configure PolyBase to access external data in MongoDB

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

The article explains how to use PolyBase on a SQL Server instance to query external data in MongoDB.

## Prerequisites

If you haven't installed PolyBase, see [PolyBase installation](polybase-installation.md).

Before creating a database scoped credential a [Master Key](../../t-sql/statements/create-master-key-transact-sql.md) must be created. 
    

## Configure a MongoDB external data source

To query the data from a MongoDB data source, you must create external tables to reference the external data. This section provides sample code to create these external tables.

The following Transact-SQL commands are used in this section:

- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)
- [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md) 
- [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md)

1. Create a database scoped credential for accessing the MongoDB source.

    ```sql
    /*  specify credentials to external data source
    *  IDENTITY: user name for external source. 
    *  SECRET: password for external source.
    */
    CREATE DATABASE SCOPED CREDENTIAL credential_name WITH IDENTITY = 'username', Secret = 'password';
    ```
1. Create an external data source with [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md).

    ```sql
    /*  LOCATION: Location string should be of format '<type>://<server>[:<port>]'.
    *  PUSHDOWN: specify whether computation should be pushed down to the source. ON by default.
    *CONNECTION_OPTIONS: Specify driver location
    *  CREDENTIAL: the database scoped credential, created above.
    */
    CREATE EXTERNAL DATA SOURCE external_data_source_name
    WITH (LOCATION = 'mongodb://<server>[:<port>]',
    -- PUSHDOWN = ON | OFF,
    CREDENTIAL = credential_name);
    ```

1. **Optional:** Create statistics on an external table.

    We recommend creating statistics on external table columns, especially the ones used for joins, filters and aggregates, for optimal query performance.

    ```sql
    CREATE STATISTICS statistics_name ON customer (C_CUSTKEY) WITH FULLSCAN; 
    ```

>[!IMPORTANT] 
>Once you have created an external data source, you can use the [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md) command to create a queryable table over that source. 

## Flattening
 flattening  is enabled for nested and repeated data from MongoDB document collections. User is required to enable `create an external table` and explicitly specify a relational schema over MongoDB document collections that may have nested and/or repeated data. We will enable auto-schema detection over mongo document collections in future milestones.
JSON nested/repeated data types will be flattened as follows

* Object: unordered key/value collection enclosed in curly braces (nested)

   - We will create a table column for each object key

     * Column Name: objectname_keyname

* Array: ordered values, separated by commas, enclosed in square brackets (repeated)

   - We will add a new table row for each array item

   - We will create a column per array to store the array item index

     * Column Name: arrayname_index

     * Data Type: bigint

There are several potential issues with this technique, two of them being:

* an empty repeated field will effectively mask the data contained in the flat fields of the same record

* the presence of multiple repeated fields can result in an explosion of the number of produced rows

As an example, we evaluate the MongoDB sample dataset restaurant collection stored in non-relational JSON format. Each restaurant has a nested address field and an array of grades it was assigned on different days. The figure below illustrates a typical restaurant with nested address and nested-repeated grades.

![MongoDB flattening](../../relational-databases/polybase/media/mongo-flattening.png "MongoDB restaurant flattening")

Object address will be flattened as below:

* Nested field restaurant.address.building becomes restaurant.address_building
* Nested field restaurant.address.coord becomes restaurant.address_coord
* Nested field restaurant.address.street becomes restaurant.address_street
* Nested field restaurant.address.zipcode becomes restaurant.address_zipcode

Array grades will be flattened as below:

| grades_date | grades_grade  | games_score | 
| ------------- | ------------------------- | -------------- |
|1393804800000 |A |2|
|1378857600000|A |6|
|135898560000 |A |10|
|1322006400000|A |9|
|1299715200000 |B |14|

## Cosmos DB Connection

Using the Cosmos DB mongo api and the Mongo DB PolyBase connector you can create an external table of a **Cosmos DB instance**. This accomplished by following the same steps listed above. Make sure the Database scoped credential, Server address, port, and location string reflect that of the Cosmos DB server. 

## Next steps

To learn more about PolyBase, see [Overview of SQL Server PolyBase](polybase-guide.md).

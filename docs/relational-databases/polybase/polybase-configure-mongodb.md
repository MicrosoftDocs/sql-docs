---
title: "Configure PolyBase to access external data in MongoDB | Microsoft Docs"
ms.custom: ""
ms.date: 09/24/2018
ms.prod: sql
ms.reviewer: ""
ms.technology: polybase
ms.topic: conceptual
author: Abiola
ms.author: aboke
manager: craigg
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---
# Configure PolyBase to access external data in MongoDB

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

The article explains how to use PolyBase on a SQL Server instance to query external data in MongoDB.

## Prerequisites

If you haven't installed PolyBase, see [PolyBase installation](polybase-installation.md). The installation article explains the prerequisites.

## Configure an External Table

To query the data from an MongoDB data source, you must create external tables to reference the external data. This section provides sample code to create these external tables.

We recommend creating statistics on external table columns, especially the ones used for joins, filters and aggregates, for optimal query performance.

These objects will create in this section:

- CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)
- CREATE EXTERNAL DATA SOURCE (Transact-SQL)
- CREATE EXTERNAL TABLE (Transact-SQL)
- CREATE STATISTICS (Transact-SQL)

1.    Create a master key on the database. This is required to encrypt the credential secret.

      ```sql
      CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'S0me!nfo';  
      ```

1.   Create a database scoped credential.

     ```sql
     /*  specify credentials to external data source
     *  IDENTITY: user name for external source.  
     *  SECRET: password for external source.
     */
     CREATE DATABASE SCOPED CREDENTIAL MongoDBCredentials 
     WITH IDENTITY = 'username', Secret = 'password';
     ```

1.  Create an external data source with [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md). Specify external data source location and credentials for the MongoDB data source.

     ```sql
     /*  LOCATION: Location string should be of format '<vendor>://<server>[:<port>]'.
    *  PUSHDOWN: specify whether computation should be pushed down to the source. ON by default.
    *  CREDENTIAL: the database scoped credential, created above.
    */  
    CREATE EXTERNAL DATA SOURCE MongoInstance
    WITH (
    LOCATION = mongodb://MongoServer,
    -- PUSHDOWN = ON | OFF,
      CREDENTIAL = MongoDBCredentials
    );
     ```

1. Create schemas for external data

     ```sql
     CREATE SCHEMA MongoDB;
     GO
     ```

1.  Create external tables that represents data stored in external MongoDB system [CREATE EXTERNAL TABLE](../../t-sql/statements/create-external-table-transact-sql.md).

     ```sql
     /*  LOCATION: MongoDB table/view in '<database_name>.<schema_name>.<object_name>' format
     *  DATA_SOURCE: the external data source, created above.
     */
     CREATE EXTERNAL TABLE MongoDB.orders(
     [O_ORDERKEY] DECIMAL(38) NOT NULL,
     [O_CUSTKEY] DECIMAL(38) NOT NULL,
     [O_ORDERSTATUS] CHAR COLLATE Latin1_General_BIN NOT NULL,
     [O_TOTALPRICE] DECIMAL(15,2) NOT NULL,
     [O_ORDERDATE] DATETIME2(0) NOT NULL,
     [O_COMMENT] VARCHAR(79) COLLATE Latin1_General_BIN NOT NULL
     )
     WITH (
     LOCATION='TPCH..ORDERS',
     DATA_SOURCE= MongoDBInstance
     );
     ```

1. Create statistics on an external table for optimized performance.

     ```sql
      CREATE STATISTICS OrdersOrderKeyStatistics ON MongoDB.orders(O_ORDERKEY) WITH FULLSCAN;
     ```

## Flattening
 flattening  is enable for nested and repeated data from MongoDB document collections. User is required to enable create an external table and explicitly specify a relational schema over MongoDB document collections that may have nested and/or repeated data. We will enable auto-schema detection over mongo document collections in future milestones.
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

## Next steps

To learn more about PolyBase, see [Overview of SQL Server PolyBase](polybase-guide.md).

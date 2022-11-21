---
title: "Bulk copy API for batch insert in JDBC"
description: "Microsoft JDBC Driver for SQL Server supports using Bulk Copy for batch inserts for faster loading of data into the database."
author: David-Engel
ms.author: v-davidengel
ms.date: "01/29/2021"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---

# Using bulk copy API for batch insert operation

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Microsoft JDBC Driver for SQL Server version 9.2 and above supports using the Bulk Copy API for batch insert operations. This feature allows users to enable the driver to do Bulk Copy operations underneath when executing batch insert operations. The driver aims to achieve improvement in performance while inserting the same data as the driver would have with regular batch insert operation. The driver parses the user's SQL Query, using the Bulk Copy API instead of the usual batch insert operation. Below are various ways to enable the Bulk Copy API for batch insert feature and lists its limitations. This page also contains a small sample code that demonstrates a usage and the performance increase as well.

This feature is only applicable to PreparedStatement and CallableStatement's `executeBatch()` & `executeLargeBatch()` APIs.

## Prerequisites

Prerequisite to enable Bulk Copy API for batch insert.

* The query must be an insert query (the query may contain comments, but the query must start with the INSERT keyword for this feature to come into effect).

## Enabling bulk copy API for batch insert

There are three ways to enable Bulk Copy API for batch insert.

### 1. Enabling with connection property

Adding **useBulkCopyForBatchInsert=true;** to the connection string enables this feature.

```java
Connection connection = DriverManager.getConnection("jdbc:sqlserver://<server>:<port>;userName=<user>;password=<password>;database=<database>;encrypt=true;useBulkCopyForBatchInsert=true;");
```

### 2. Enabling with setUseBulkCopyForBatchInsert() method from SQLServerConnection object

Calling **SQLServerConnection.setUseBulkCopyForBatchInsert(true)** enables this feature.

**SQLServerConnection.getUseBulkCopyForBatchInsert()** retrieves the current value for **useBulkCopyForBatchInsert** connection property.

The value for **useBulkCopyForBatchInsert** stays constant for each PreparedStatement at the time of its initialization. Any subsequent calls to **SQLServerConnection.setUseBulkCopyForBatchInsert()** won't affect the already created PreparedStatement's value.

### 3. Enabling with setUseBulkCopyForBatchInsert() method from SQLServerDataSource object

Similar to above, but using SQLServerDataSource to create a SQLServerConnection object. Both methods achieve the same result.

## Known limitations

There are currently these limitations that apply to this feature.

* Insert queries that contain non-parameterized values (for example, `INSERT INTO TABLE VALUES (?, 2`)), isn't supported. Wildcards (?) are the only supported parameters for this function.
* Insert queries that contain INSERT-SELECT expressions (for example, `INSERT INTO TABLE SELECT * FROM TABLE2`), isn't supported.
* Insert queries that contain multiple VALUE expressions (for example, `INSERT INTO TABLE VALUES (1, 2) (3, 4)`), isn't supported.
* Insert queries that are followed by the OPTION clause, joined with multiple tables, or followed by another query, isn't supported.
* Because of the limitations of Bulk Copy API, `MONEY`, `SMALLMONEY`, `DATE`, `DATETIME`, `DATETIMEOFFSET`, `SMALLDATETIME`, `TIME`, `GEOMETRY`, and `GEOGRAPHY` data types, are currently not supported for this feature.

If the query fails because of non "SQL server" related errors, the driver will log the error message and fallback to the original logic for batch insert.

## Example

This is an example that demonstrates the use case for a batch insert operation of a thousand rows, for both regular vs Bulk Copy API scenarios.

```java
    public static void main(String[] args) throws Exception
    {
        String tableName = "batchTest";
        String tableNameBulkCopyAPI = "batchTestBulk";

        String connectionUrl = "jdbc:sqlserver://<server>:<port>;encrypt=true;databaseName=<database>;user=<user>;password=<password>";

        try (Connection con = DriverManager.getConnection(connectionUrl);
                Statement stmt = con.createStatement();
                PreparedStatement pstmt = con.prepareStatement("insert into " + tableName + " values (?, ?)");) {

            String dropSql = "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[" + tableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) DROP TABLE [" + tableName + "]";
            stmt.execute(dropSql);

            String createSql = "create table " + tableName + " (c1 int, c2 varchar(20))";
            stmt.execute(createSql);

            System.out.println("Starting batch operation using regular batch insert operation.");
            long start = System.currentTimeMillis();
            for (int i = 0; i < 1000; i++) {
                pstmt.setInt(1, i);
                pstmt.setString(2, "test" + i);
                pstmt.addBatch();
            }
            pstmt.executeBatch();

            long end = System.currentTimeMillis();

            System.out.println("Finished. Time taken : " + (end - start) + " milliseconds.");
        }

        try (Connection con = DriverManager.getConnection(connectionUrl + ";useBulkCopyForBatchInsert=true");
                Statement stmt = con.createStatement();
                PreparedStatement pstmt = con.prepareStatement("insert into " + tableNameBulkCopyAPI + " values (?, ?)");) {

            String dropSql = "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[" + tableNameBulkCopyAPI + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) DROP TABLE [" + tableNameBulkCopyAPI + "]";
            stmt.execute(dropSql);

            String createSql = "create table " + tableNameBulkCopyAPI + " (c1 int, c2 varchar(20))";
            stmt.execute(createSql);

            System.out.println("Starting batch operation using Bulk Copy API.");
            long start = System.currentTimeMillis();
            for (int i = 0; i < 1000; i++) {
                pstmt.setInt(1, i);
                pstmt.setString(2, "test" + i);
                pstmt.addBatch();
            }
            pstmt.executeBatch();

            long end = System.currentTimeMillis();

            System.out.println("Finished. Time taken : " + (end - start) + " milliseconds.");
        }
    }
```

Result:

```bash
Starting batch operation using regular batch insert operation.
Finished. Time taken : 104132 milliseconds.
Starting batch operation using Bulk Copy API.
Finished. Time taken : 1058 milliseconds.
```

## See also

[Improving performance and reliability with the JDBC driver](improving-performance-and-reliability-with-the-jdbc-driver.md)

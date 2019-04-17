---
title: "Using Bulk Copy API for Batch Insert Operation for MSSQL JDBC Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/21/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 
author: MightyPen
ms.author: genemi
manager: craigg
---

# Using Bulk Copy API for Batch Insert Operation

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Microsoft JDBC Driver 7.0 for SQL Server supports using Bulk Copy API for batch insert operations for Azure Data Warehouse. This feature allows users to enable driver to perform Bulk Copy operation underneath when executing batch insert operations. The driver aims to achieve improvement in performance while inserting the same data as the driver would have with regular batch insert operation. The driver parses the user's SQL Query, leveraging the Bulk Copy API in lieu of the usual batch insert operation. Below are various ways to enable the Bulk Copy API for batch insert feature, as well as the list of its limitations. This page also contains a small sample code that demonstrates a usage and the performance increase as well.

This feature is only applicable to PreparedStatement and CallableStatement's `executeBatch()` & `executeLargeBatch()` APIs.

## Pre-Requisites

There are two prerequisites to enable Bulk Copy API for batch insert.

* The server must be Azure Data Warehouse.
* The query must be an insert query (the query may contain comments, but the query must start with the INSERT keyword for this feature to come into effect).

## Enabling Bulk Copy API for batch insert

There are three ways to enable Bulk Copy API for batch insert.

### 1. Enabling with connection property

Adding **useBulkCopyForBatchInsert=true;** to the connection string enables this feature.

```java
Connection connection = DriverManager.getConnection("jdbc:sqlserver://<server>:<port>;userName=<user>;password=<password>;database=<database>;useBulkCopyForBatchInsert=true;");
```

### 2. Enabling with setUseBulkCopyForBatchInsert() method from SQLServerConnection object

Calling **SQLServerConnection.setUseBulkCopyForBatchInsert(true)** enables this feature.

**SQLServerConnection.getUseBulkCopyForBatchInsert()** retrieves the current value for **useBulkCopyForBatchInsert** connection property.

The value for **useBulkCopyForBatchInsert** stays constant for each PreparedStatement at the time of its initialization. Any subsequent calls to **SQLServerConnection.setUseBulkCopyForBatchInsert()** will not affect the already created PreparedStatement with regard to its value.

### 3. Enabling with setUseBulkCopyForBatchInsert() method from SQLServerDataSource object

Similar to above, but using SQLServerDataSource to create a SQLServerConnection object. Both methods achieve the same result.

## Known limitations

There are currently these limitations that apply to this feature.

* Insert queries that contain non-parameterized values (for example, `INSERT INTO TABLE VALUES (?, 2`)), are not supported. Wildcards (?) are the only supported parameters for this function.
* Insert queries that contain INSERT-SELECT expressions (for example, `INSERT INTO TABLE SELECT * FROM TABLE2`), are not supported.
* Insert queries that contain multiple VALUE expressions (for example, `INSERT INTO TABLE VALUES (1, 2) (3, 4)`), are not supported.
* Insert queries that are followed by the OPTION clause, joined with multiple tables, or followed by another query, are not supported.
* Due to the limitations of Bulk Copy API, `MONEY`, `SMALLMONEY`, `DATE`, `DATETIME`, `DATETIMEOFFSET`, `SMALLDATETIME`, `TIME`, `GEOMETRY`, and `GEOGRAPHY` data types, are currently not supported for this feature.

If the query fails because of non "SQL server" related errors, the driver will log the error message and fallback to the original logic for batch insert.

## Example

Below is an example code that demonstrates the use case for a batch insert operation against Azure DW of a thousand rows, for both (regular vs Bulk Copy API) scenarios.

```java
    public static void main(String[] args) throws Exception
    {
        String tableName = "batchTest";
        String tableNameBulkCopyAPI = "batchTestBulk";

        String azureDWconnectionUrl = "jdbc:sqlserver://<server>:<port>;databaseName=<database>;user=<user>;password=<password>";

        try (Connection con = DriverManager.getConnection(azureDWconnectionUrl); // connects to an Azure Data Warehouse.
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

        try (Connection con = DriverManager.getConnection(azureDWconnectionUrl + ";useBulkCopyForBatchInsert=true"); // connects to an Azure Data Warehouse, with useBulkCopyForBatchInsert connection property set to true.
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

## See Also

[Improving Performance and Reliability with the JDBC Driver](../../connect/jdbc/improving-performance-and-reliability-with-the-jdbc-driver.md)

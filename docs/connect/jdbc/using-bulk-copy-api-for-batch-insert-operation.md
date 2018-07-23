---
title: "Using Bulk Copy API for Batch Insert Operation | Microsoft Docs"
ms.custom: ""
ms.date: "07/23/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.suite: "sql"
ms.technology: connectivity
ms.tgt_pltfrm: ""
ms.topic: conceptual
ms.assetid: 
caps.latest.revision: 1
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using Bulk Copy API for Batch Insert Operation
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Using Bulk Copy API for batch insert operation is supported starting from JDBC driver preview release 6.5.4. This feature allows the users to utilize Bulk Copy API underneath when executing batch insert operations against Azure Data Warehouses, which improves the performance significantly. This feature aims to achieve the performance improvement while inserting the same data as the driver would have with regular batch insert operation, by parsing the user's SQL Query and leveraging the Bulk Copy API in lieu of the usual batch insert operation. This page shows various ways to enable the Bulk Copy API for batch insert feature, as well as its limitations. This page contains a small sample code at the end that demonstrates a usage and the performance increase as well.

This feature is only applicable to PreparedStatement and CallableStatement's executeBatch & executeLargeBatch APIs.

## Prerequisites
There are two prerequisites to enable Bulk Copy API for batch insert.

1. The server must be Azure Data Warehouse.
2. The query must be an insert query (the query may contain comments, but the query must start with the INSERT keyword for this feature to come into effect).

## Enabling Bulk Copy API for batch insert

There are three ways to enable Bulk Copy API for batch insert.

### 1. Enabling with connection property:

Adding **useBulkCopyForBatchInsert=true;** to the connection string enables this feature.

```
connection = DriverManager.getConnection("jdbc:sqlserver://localhost;userName=user;password=password;database=test;useBulkCopyForBatchInsert=true;");
```

### 2. Enabling with setUseBulkCopyForBatchInsert() method from SQLServerConnection object:

Calling **SQLServerConnection.setUseBulkCopyForBatchInsert(true)** enables this feature.

**SQLServerConnection.getUseBulkCopyForBatchInsert()** retrieves the current value for **useBulkCopyForBatchInsert** connection property.

Note that the value for **useBulkCopyForBatchInsert** stays constant for each PreparedStatement at the time of its initialization, and subsequent calls to **SQLServerConnection.setUseBulkCopyForBatchInsert()** will not affect the already created PreparedStatement with regard to the value for **useBulkCopyForBatchInsert**.

### 3. Enabling with setUseBulkCopyForBatchInsert() method from SQLServerDataSource object:

Similar to above, but using SQLServerDataSource to create a SQLServerConnection object. Both methods achieve the same result.

## Limitations

There are currently these limitations that apply to this feature.

1. Insert queries that contain non-parameterized values (e.g. INSERT INTO TABLE VALUES (?, **2**)) are not supported. Wildcards (?) are the only supported parameters for this function.
2. Insert queries that contain INSERT-SELECT expressions (e.g. INSERT INTO TABLE **SELECT * FROM TABLE2**) are not supported.
3. Insert queries that contain multiple VALUE expressions (e.g. INSERT INTO TABLE VALUES **(1, 2) (3, 4)**) are not supported.
4. Insert queries that are followed by the OPTION clause or joined with other queries that are not part of the INSERT query are not supported.

If the query fails due to non-SQL server related errors, the driver will log the error message and fallback to the original logic for batch insert.

## Example

Below is an example code that demonstrates the use case for a batch insert operation against Azure DW of a thousand rows, for both (regular vs Bulk Copy API) scenarios.

```java
      public static void main(String[] args) throws Exception
      {
          Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
          String tableName = "batchTest";
          String tableNameBulkCopyAPI = "batchTestBulk";
          
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
```
Starting batch operation using regular batch insert operation.
Finished. Time taken : 104132 milliseconds.
Starting batch operation using Bulk Copy API.
Finished. Time taken : 1058 milliseconds.
```

## See Also  
 [Improving Performance and Reliability with the JDBC Driver](../../connect/jdbc/improving-performance-and-reliability-with-the-jdbc-driver.md)  
  
  

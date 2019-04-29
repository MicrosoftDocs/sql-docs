---
title: "Retrieving ParameterMetaData via useFmtOnly | Microsoft Docs"
ms.custom: ""
ms.date: "04/26/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.suite: "sql"
ms.technology: connectivity
ms.tgt_pltfrm: ""
ms.topic: conceptual
ms.assetid: ""
caps.latest.revision: ""
author: rene-ye
ms.author: ""
manager: kenvh
---
# Retrieving ParameterMetaData via useFmtOnly
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  The Microsoft SQL Server JDBC Driver includes an alternative way to query ParameterMetaData from the server, **useFmtOnly**. This feature was first introduced in version 7.3 of the driver, and is required as a work around for known issues in `sp_describe_undeclared_parameters`.
  
  The driver primarily uses `sp_describe_undeclared_parameters` to query ParameterMetaData, and this is the recommended method for ParameterMetaData retrieval under most circumstances. However, executing the stored procedure currently fails under the following use cases:
  
-   Against Always Encrypted columns
  
-   Against temporary tables and table variables
  
-   Against views 
  
  The proposed solution for these use cases is to parse the user's sql query for parameters and table targets, then execute a SELECT query with FMTONLY enabled. The following snippet will help visualize the feature.
  
```sql
--create a normal table 'Foo' and a temporary table 'Bar'
CREATE TABLE Foo(c1 int);
CREATE TABLE #Bar(c1 int);

EXEC sp_describe_undeclared_parameters N'SELECT * FROM Foo WHERE c1 = @p0' --works fine
EXEC sp_describe_undeclared_parameters N'SELECT * FROM #Bar WHERE c1 = @p0' --fails with "Invalid object name '#Bar'"

SET FMTONLY ON;
SELECT c1 FROM Foo; --works
SET FMTONLY OFF;
SET FMTONLY ON;
SELECT c1 FROM #Bar; --works
SET FMTONLY OFF;
```
 
## Turning the feature on/off 
 The feature **useFmtOnly** is off by default. Users can enable this feature through the connections string by specifying `useFmtOnly=true`. E.g. `-Dconnectionstring=jdbc:sqlserver://localhost;databaseName=tempdb;username=<user>;password=<password>;useFmtOnly=true;`.
 
 Alternatively, the feature is available through `SQLServerDataSource`.
 ```java
SQLServerDataSource ds = new SQLServerDataSource();
ds.setURL("jdbc:sqlserver://localhost");
ds.setDatabaseName("tempdb");
ds.setUser("<user>");
ds.setPassword("<password>");
ds.setUseFmtOnly(true);
try (Connection c = ds.getConnection()) {
	// do work with connection
}
 ```
 
 The feature is also available on the Statement level. Users can turn the feature on/off through `PreparedStatement.setUseFmtOnly(boolean)`.
> [!NOTE]  
>  The driver will prioritize the Statement level property over the Connection level property.

## Using the feature
  Once enabled, the driver will internally start using the new feature instead of `sp_describe_undeclared_parameters` when querying ParameterMetaData. There is no further action necessary from the end user.
```java
final String sql = "INSERT INTO #Bar VALUES (?)";
try (Connection c = DriverManager.getConnection(URL, USERNAME, PASSWORD)) {
	try (Statement s = c.createStatement()) {
		s.execute("CREATE TABLE #Bar(c1 int)");
	}
	try (PreparedStatement p1 = c.prepareStatement(sql);PreparedStatement p2 = c.prepareStatement(sql)) {
		((SQLServerPreparedStatement)p1).setUseFmtOnly(true);
		ParameterMetaData pmd1 = p1.getParameterMetaData();
		System.out.println(pmd1.getParameterTypeName(1)); // prints int
		ParameterMetaData pmd2 = p2.getParameterMetaData(); // throws exception, Invalid object name '#Bar'
	}
}
```
> [!NOTE]  
>  The feature only supports SELECT/INSERT/UPDATE/DELETE queries. Queries should start with one of the 4 supported key words or a Common Table Expression. Parameters within Common Table Expressions are not supported.

## Known issues
  There are currently some issues with the feature which are caused by deficiencies in SQL parsing logic. These issues may be addressed in a future update to the feature, and are documented below along with workaround suggestions.
  
A. Using a 'foward declared' alias
```sql
CREATE TABLE Foo(c1 int)

DELETE fooAlias FROM Foo fooAlias WHERE c1 > ?; --Invalid object name 'fooAlias'

--Workaround #1: Specify AS keyword
DELETE fooAlias FROM Foo AS fooAlias WHERE c1 > ?;
--Workaround #2: Use the table name
DELETE Foo FROM Foo fooAlias WHERE c1 > ?;
```

B. Ambiguous Column Name when tables have shared column names
```sql
CREATE TABLE Foo(c1 int, c2 int, c3 int)
Create TABLE Bar(c1 int, c2 int, c3 int)

SELECT c1,c2 FROM Foo WHERE c3 IN (SELECT c3 FROM BAR WHERE c1 > ? and c2 < ? and c3 = ?); --Ambiguous Column Name

--Workaround: Use aliases
SELECT c1,c2 FROM Foo WHERE c3 IN (SELECT c3 FROM BAR b WHERE b.c1 = ? and b.c2 = ? and b.c3 = ?);
```

C. SELECT from a subquery with parameters
```sql

CREATE TABLE Foo(c1 int)

SELECT * FROM (SELECT * FROM Foo WHERE c1 = ?) WHERE c1 = ?; --Incorrect syntax near '?'

--Workaround: N/A
```

D. Subqueries in a SET clause
```sql
CREATE TABLE Foo(c1 int)

UPDATE Foo SET c1 = (SELECT c1 FROM Foo) WHERE c1 = ?; --Incorrect syntax near ')'

--Workaround: Add a 'delimiting' condition
UPDATE Foo SET c1 = (SELECT c1 FROM Foo HAVING (HASH JOIN)) WHERE c1 = ?;
```

## See Also  
 [Setting the Connection Properties](../../connect/jdbc/setting-the-connection-properties.md)  
  
  

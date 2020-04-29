---
title: "SQL Server 2019 Compliance | Microsoft Docs"
ms.custom: ""
ms.date: "29/04/2020"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
monikerRange: "= sql-server-2019 || = sqlallproducts-allversions"
---
# SQL Server 2019 SQL Standards Compliance

| Function | Description | Microsoft SQL
|----------|-------------|--------------
| E011 | Numeric data types | Unknown
| E011-01 | INTEGER and SMALLINT data types including all spellings | Unknown
| E011-02 | REAL, DOUBLE PRECISION, and FLOAT data types | Unknown
| E011-03 | DECIMAL and NUMERIC data types | Unknown
| E011-04 | Arithmetic operators | Unknown
| E011-05 | Numeric comparison | Unknown
| E011-06 | Implicit casting among the numeric data types | Unknown
| E021 | Character string types | Partial 
| E021-01 | CHARACTER data type including all its spellings | Unknown
| E021-02 | CHARACTER VARYING data type including all its spellings | Unknown
| E021-03 | Character literals | Unknown
| E021-04 | CHARACTER_LENGTH function | Unknown
| E021-05 | OCTET_LENGTH function | Unknown
| E021-06 | SUBSTRING function | Yes
| E021-07 | Character concatenation | Unknown
| E021-08 | UPPER and LOWER functions | Yes
| E021-09 | TRIM function | No
| E021-10 | Implicit casting among the fixed-length and variable-length character string types | Unknown
| E021-11 | POSITION function | No
| E021-12 | Character comparison | Unknown
| E031 | Identifiers | Unknown
| E031-01 | Delimited identifiers | Unknown
| E031-02 | Lower case identifiers | Unknown
| E031-03 | Trailing underscore | Unknown
| E051 | Basic query specification | Unknown
| E051-01 | SELECT DISTINCT | Yes
| E051-02 | GROUP BY clause | Yes
| E051-04 | GROUP BY can contain columns not in <select-list> | Unknown
| E051-05 | Select list items can be renamed | Yes
| E051-06 | HAVING clause | Yes
| E051-07 | Qualified * in select list | Unknown
| E051-08 | Correlation names in the FROM clause | Unknown
| E051-09 | Rename columns in the FROM clause | Unknown
| E061 | Basic predicates and search conditions | Unknown
| E061-01 | Comparison predicate | Unknown
| E061-02 | BETWEEN predicate | Yes
| E061-03 | IN predicate with list of values | Yes
| E061-04 | LIKE predicate | Yes
| E061-05 | LIKE predicate: ESCAPE clause | Unknown
| E061-06 | NULL predicate | Unknown
| E061-07 | Quantified comparison predicate | Unknown
| E061-08 | EXISTS predicate | Yes
| E061-09 | Subqueries in comparison predicate | Unknown
| E061-11 | Subqueries in IN predicate | Yes
| E061-12 | Subqueries in quantified comparison predicate | Unknown
| E061-13 | Correlated subqueries | Yes
| E061-14 | Search condition | Unknown
| E071 | Basic query expressions | Unknown
| E071-01 | UNION DISTINCT table operator | Unknown
| E071-02 | UNION ALL table operator | Yes
| E071-03 | EXCEPT DISTINCT table operator | Unknown
| E071-05 | Columns combined via table operators need not have exactly the same data type | Yes
| E071-06 | Table operators in subqueries | Unknown
| E081 | Basic Privileges | Unknown
| E081-01 | SELECT privilege at the table level | Unknown
| E081-02 | DELETE privilege | Unknown
| E081-03 | INSERT privilege at the table level | Unknown
| E081-04 | UPDATE privilege at the table level | Unknown
| E081-05 | UPDATE privilege at the column level | Unknown
| E081-06 | REFERENCES privilege at the table level | Unknown
| E081-07 | REFERENCES privilege at the column level | Unknown
| E081-08 | WITH GRANT OPTION | Unknown
| E081-09 | USAGE privilege | Unknown
| E081-10 | EXECUTE privilege | Unknown
| E091 | Set functions | Unknown
| E091-01 | AVG | Yes
| E091-02 | COUNT | Yes
| E091-03 | MAX | Yes
| E091-04 | MIN | Yes
| E091-05 | SUM | Yes
| E091-06 | ALL quantifier | Unknown
| E091-07 | DISTINCT quantifier | Yes
| E101 | Basic data manipulation | Unknown
| E101-01 | INSERT statement | Yes
| E101-03 | Searched UPDATE statement | Unknown
| E101-04 | Searched DELETE statement | Unknown
| E111 | Single row SELECT statement | Unknown
| E121 | Basic cursor support | Unknown
| E121-01 | DECLARE CURSOR | Unknown
| E121-02 | ORDER BY columns need not be in select list | Yes
| E121-03 | Value expressions in ORDER BY clause | Yes
| E121-04 | OPEN statement | Unknown
| E121-06 | Positioned UPDATE statement | Unknown
| E121-07 | Positioned DELETE statement | Unknown
| E121-08 | CLOSE statement | Unknown
| E121-10 | FETCH statement: implicit NEXT | Unknown
| E121-17 | WITH HOLD cursors | Unknown
| E131 | Null value support (nulls in lieu of values) | Yes
| E141 | Basic integrity constraints | Unknown
| E141-01 | NOT NULL constraints | Yes
| E141-02 | UNIQUE constraints of NOT NULL columns | Unknown
| E141-03 | PRIMARY KEY constraints | Yes
| E141-04 | Basic FOREIGN KEY constraint with the NO ACTION default for both referential delete action and referential update action | Unknown
| E141-06 | CHECK constraints | Unknown
| E141-07 | Column defaults | Yes
| E141-08 | NOT NULL inferred on PRIMARY KEY | Unknown
| E141-10 | Names in a foreign key can be specified in any order | Unknown
| E151 | Transaction support | Yes
| E151-01 | COMMIT statement | Yes
| E151-02 | ROLLBACK statement | Yes
| E152 | Basic SET TRANSACTION statement | Unknown
| E152-01 | SET TRANSACTION statement: ISOLATION LEVEL SERIALIZABLE clause | Unknown
| E152-02 | SET TRANSACTION statement: READ ONLY and READ WRITE clauses | Unknown
| E* | Other | Unknown
| E153 | Updatable queries with subqueries | Unknown
| E161 | SQL comments using leading double minus | Yes
| E171 | SQLSTATE support | Unknown
| E182 | Host language Binding (previously "Module Language") | Unknown
| F021 | Basic information schema | Yes
| F021-01 | COLUMNS view | Yes
| F021-02 | TABLES view | Yes
| F021-03 | VIEWS view | Yes
| F021-04 | TABLE_CONSTRAINTS view | Yes
| F021-05 | REFERENTIAL_CONSTRAINTS view | Yes
| F021-06 | CHECK_CONSTRAINTS view | Yes
| F031 | Basic schema manipulation | Unknown
| F031-01 | CREATE TABLE statement to create persistent base tables | Yes
| F031-02 | CREATE VIEW statement | Yes
| F031-03 | GRANT statement | Unknown
| F031-04 | ALTER TABLE statement: ADD COLUMN clause | Unknown
| F031-13 | DROP TABLE statement: RESTRICT clause | Unknown
| F031-16 | DROP VIEW statement: RESTRICT clause | Unknown
| F031-19 | REVOKE statement: RESTRICT clause | Unknown
| F041 | Basic joined table | Unknown
| F041-01 | Inner join (but not necessarily the INNER keyword) | Yes
| F041-02 | INNER keyword | Yes
| F041-03 | LEFT OUTER JOIN | Yes
| F041-04 | RIGHT OUTER JOIN | Yes
| F041-05 | Outer joins can be nested | Unknown
| F041-07 | The inner table in a left or right outer join can also be used in an inner join | Unknown
| F041-08 | All comparison operators are supported (rather than just =) | Unknown
| F051 | Basic date and time | Unknown
| F051-01 | DATE data type including support of DATE literal | Unknown
| F051-02 | TIME data type including support of TIME literal with fractional seconds precision of at least 0 | Unknown
| F051-03 | TIMESTAMP data type including support of TIMESTAMP literal with fractional seconds precision of at least 0 and 6 | Unknown
| F051-04 | Comparison predicate on DATE, TIME, and TIMESTAMP data types | Unknown
| F051-05 | Explicit CAST between datetime types and character string types | Unknown
| F051-06 | CURRENT_DATE | Unknown
| F051-07 | LOCALTIME | Unknown
| F051-08 | LOCALTIMESTAMP | Unknown
| F081 | UNION and EXCEPT in views | Yes
| F131 | Grouped operations | Unknown
| F131-01 | WHERE, GROUP BY, and HAVING clauses supported in queries with grouped views | Unknown
| F131-02 | Multiple tables supported in queries with grouped views | Unknown
| F131-03 | Set functions supported in queries with grouped views | Unknown
| F131-04 | Subqueries with GROUP BY and HAVING clauses and grouped views | Unknown
| F131-05 | Single row SELECT with GROUP BY and HAVING clauses and grouped views | Unknown
| F* | Other | Unknown
| F181 | Multiple module support | Unknown
| F201 | Mono|CAST function | Unknown
| F221 | Explicit defaults | Unknown
| F261 | Mono|CASE expression | Yes
| F261-01 | Simple CASE | Unknown
| F261-02 | Searched CASE | Unknown
| F261-03 | NULLIF | Unknown
| F261-04 | COALESCE | Yes
| F311 | Schema definition statement | Unknown
| F311-01 | CREATE SCHEMA | Unknown
| F311-02 | CREATE TABLE for persistent base tables | Yes
| F311-03 | CREATE VIEW | Yes
| F311-04 | CREATE VIEW: WITH CHECK OPTION | Unknown
| F311-05 | GRANT statement | Unknown
| F471 | Scalar subquery values | Unknown
| F481 | Expanded NULL predicate | Unknown
| F501 | Features and conformance views | Unknown
| F501-01 | SQL_FEATURES view | Unknown
| F501-02 | SQL_SIZING view | Unknown
| F501-03 | SQL_LANGUAGES view | Unknown
| F812 | Basic flagging | Unknown
| S011 | Distinct data types | Unknown
| S011-01 | USER_DEFINED_TYPES view | Unknown
| T321 | Basic SQL-invoked routines | Unknown
| T321-01 | User-defined functions with no overloading | Yes
| T321-02 | User-defined stored procedures with no overloading | Yes
| T321-03 | Function invocation | Yes
| T321-04 | CALL statement | Unknown
| T321-05 | RETURN statement | Unknown
| T321-06 | ROUTINES view | Unknown
| T321-07 | PARAMETERS view | Unknown
| T631 | Mono|IN predicate with one list element | Unknown

[!INCLUDE[contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]

![MS_Logo_X-Small](../sql-server/media/ms-logo-x-small.png)

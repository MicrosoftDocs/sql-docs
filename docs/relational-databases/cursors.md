---
title: "Cursors (SQL Server)"
description: Cursors extend result processing by manipulating result sets in small blocks at a time.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/07/2023
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "results [SQL Server], cursors"
  - "Transact-SQL cursors, about cursors"
  - "cursors [SQL Server]"
  - "data access [SQL Server], cursors"
  - "result sets [SQL Server], cursors"
  - "requesting cursors"
  - "cursors [SQL Server], about cursors"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Cursors (SQL Server)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../includes/applies-to-version/sql-asdb-asdbmi.md)]

Operations in a relational database act on a complete set of rows. For example, the set of rows returned by a `SELECT` statement consists of all the rows that satisfy the conditions in the `WHERE` clause of the statement. This complete set of rows returned by the statement is known as the *result set*. Applications, especially interactive online applications, can't always work effectively with the entire result set as a unit. These applications need a mechanism to work with one row or a small block of rows at a time. Cursors are an extension to result sets that provide that mechanism.

Cursors extend result processing by:

- Allowing positioning at specific rows of the result set.

- Retrieving one row or block of rows from the current position in the result set.

- Supporting data modifications to the rows at the current position in the result set.

- Supporting different levels of visibility to changes made by other users to the database data that is presented in the result set.

- Providing [!INCLUDE [tsql](../includes/tsql-md.md)] statements in scripts, stored procedures, and triggers access to the data in a result set.

## Remarks

In some scenarios, if there's a primary key on a table, a `WHILE` loop can be used instead of a cursor, without incurring in the overhead of a cursor.

However, there are scenarios where cursors aren't only unavoidable, they're actually needed. When that is the case, if there's no requirement to update tables based on the cursor, then use *firehose* cursors, meaning [fast-forward and read-only](#forward-only) cursors.

## Cursor implementations

[!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] supports three cursor implementations.

| Cursor implementation | Description |
| --- | --- |
| **Transact-SQL cursors** | [!INCLUDE [tsql](../includes/tsql-md.md)] cursors are based on the `DECLARE CURSOR` syntax and used mainly in [!INCLUDE [tsql](../includes/tsql-md.md)] scripts, stored procedures, and triggers. [!INCLUDE [tsql](../includes/tsql-md.md)] cursors are implemented on the server and are managed by [!INCLUDE [tsql](../includes/tsql-md.md)] statements sent from the client to the server. They might also be contained in batches, stored procedures, or triggers. |
| **Application programming interface (API) server cursors** | API cursors support the API cursor functions in OLE DB and ODBC. API server cursors are implemented on the server. Each time a client application calls an API cursor function, the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Native Client OLE DB provider or ODBC driver transmits the request to the server for action against the API server cursor. |
| **Client cursors** | Client cursors are implemented internally by the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Native Client ODBC driver and by the DLL that implements the ADO API. Client cursors are implemented by caching all the result set rows on the client. Each time a client application calls an API cursor function, the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Native Client ODBC driver or the ADO DLL performs the cursor operation on the result set rows cached on the client. |

## Type of cursors

[!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] supports four cursor types.

Cursors can use `tempdb` worktables. Just like aggregation or sort operations that spill, these incur I/O costs, and are a potential performance bottleneck. `STATIC` cursors use worktables from its inception. For more information, see the [worktables section in the Query processing architecture guide](../relational-databases/query-processing-architecture-guide.md#worktables).

### Forward-only

A forward-only cursor is specified as `FORWARD_ONLY` and `READ_ONLY` and doesn't support scrolling. These are also called *firehose* cursors and support only fetching the rows serially from the start to the end of the cursor. The rows aren't retrieved from the database until they're fetched. The effects of all `INSERT`, `UPDATE`, and `DELETE` statements made by the current user or committed by other users that affect rows in the result set are visible as the rows are fetched from the cursor.

Because the cursor can't be scrolled backward, most changes made to rows in the database after the row was fetched aren't visible through the cursor. In cases where a value used to determine the location of the row within the result set is modified, such as updating a column covered by a clustered index, the modified value is visible through the cursor.

Although the database API cursor models consider a forward-only cursor to be a distinct type of cursor, [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] doesn't. [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] considers both forward-only and scroll as options that can be applied to static, keyset-driven, and dynamic cursors. [!INCLUDE [tsql](../includes/tsql-md.md)] cursors support forward-only static, keyset-driven, and dynamic cursors. The database API cursor models assume that static, keyset-driven, and dynamic cursors are always scrollable. When a database API cursor attribute or property is set to forward-only, [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] implements this as a forward-only dynamic cursor.

### Static

The complete result set of a static cursor is built in `tempdb` when the cursor is opened. A static cursor always displays the result set as it was when the cursor was opened. Static cursors detect few or no changes, but consume relatively few resources while scrolling.

The cursor doesn't reflect any changes made in the database that affect either the membership of the result set or changes to the values in the columns of the rows that make up the result set. A static cursor doesn't display new rows inserted in the database after the cursor was opened, even if they match the search conditions of the cursor `SELECT` statement. If rows making up the result set are updated by other users, the new data values aren't displayed in the static cursor. The static cursor displays rows deleted from the database after the cursor was opened. No `UPDATE`, `INSERT`, or `DELETE` operations are reflected in a static cursor (unless the cursor is closed and reopened), not even modifications made using the same connection that opened the cursor.

> [!NOTE]  
> [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] static cursors are always read-only.

Because the result set of a static cursor is stored in a worktable in `tempdb`, the size of the rows in the result set can't exceed the maximum row size for a [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] table.

For more information, see the [worktables section in the Query processing architecture guide](../relational-databases/query-processing-architecture-guide.md#worktables). For more information on max row size, see [Maximum capacity specifications for SQL Server](../sql-server/maximum-capacity-specifications-for-sql-server.md).

[!INCLUDE [tsql](../includes/tsql-md.md)] uses the term *insensitive* for static cursors. Some database APIs identify them as *snapshot* cursors.

### Keyset

The membership and order of rows in a keyset-driven cursor are fixed when the cursor is opened. Keyset-driven cursors are controlled by a set of unique identifiers, or keys, known as the *keyset*. The keys are built from a set of columns that uniquely identify the rows in the result set. The keyset is the set of the key values from all the rows that qualified for the `SELECT` statement at the time the cursor was opened. The keyset for a keyset-driven cursor is built in `tempdb` when the cursor is opened.

### Dynamic

Dynamic cursors are the opposite of static cursors. Dynamic cursors reflect all changes made to the rows in their result set when scrolling through the cursor. The data values, order, and membership of the rows in the result set can change on each fetch. All `UPDATE`, `INSERT`, and `DELETE` statements made by all users are visible through the cursor. Updates are visible immediately if they're made through the cursor using either an API function such as `SQLSetPos` or the [!INCLUDE [tsql](../includes/tsql-md.md)] `WHERE CURRENT OF` clause. Updates made outside the cursor aren't visible until they're committed, unless the cursor transaction isolation level is set to read uncommitted. For more information on isolation levels, see [SET TRANSACTION ISOLATION LEVEL (Transact-SQL)](../t-sql/statements/set-transaction-isolation-level-transact-sql.md).

> [!NOTE]  
> Dynamic cursor plans never use spatial indexes.

## Request a cursor

[!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] supports two methods for requesting a cursor:

- [!INCLUDE [tsql](../includes/tsql-md.md)]

  The [!INCLUDE [tsql](../includes/tsql-md.md)] language supports a syntax for using cursors modeled after the ISO cursor syntax.

- Database application programming interface (API) cursor functions

  [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] supports the cursor functionality of these database APIs:

  - ADO ([!INCLUDE [msCoName](../includes/msconame-md.md)] ActiveX Data Object)

  - OLE DB

  - ODBC (Open Database Connectivity)

An application should never mix these two methods of requesting a cursor. An application that uses the API to specify cursor behaviors shouldn't then execute a [!INCLUDE [tsql](../includes/tsql-md.md)] `DECLARE CURSOR` statement to also request a [!INCLUDE [tsql](../includes/tsql-md.md)] cursor. An application should only execute `DECLARE CURSOR` if it sets all the API cursor attributes back to their defaults.

If neither a [!INCLUDE [tsql](../includes/tsql-md.md)] nor API cursor is requested, [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] defaults to returning a complete result set, known as a default result set, to the application.

## Cursor process

[!INCLUDE [tsql](../includes/tsql-md.md)] cursors and API cursors have different syntax, but the following general process is used with all [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] cursors:

1. Associate a cursor with the result set of a [!INCLUDE [tsql](../includes/tsql-md.md)] statement, and define characteristics of the cursor, such as whether the rows in the cursor can be updated.

1. Execute the [!INCLUDE [tsql](../includes/tsql-md.md)] statement to populate the cursor.

1. Retrieve the rows in the cursor you want to see. The operation to retrieve one row or one block of rows from a cursor is called a fetch. Performing a series of fetches to retrieve rows in either a forward or backward direction is called scrolling.

1. Optionally, perform modification operations (update or delete) on the row at the current position in the cursor.

1. Close the cursor.

## Related content

- [Cursor Behaviors](native-client-odbc-cursors/cursor-behaviors.md)
- [How Cursors Are Implemented](native-client-odbc-cursors/implementation/how-cursors-are-implemented.md)
- [DECLARE CURSOR (Transact-SQL)](../t-sql/language-elements/declare-cursor-transact-sql.md)
- [Cursors (Transact-SQL)](../t-sql/language-elements/cursors-transact-sql.md)
- [Cursor Functions (Transact-SQL)](../t-sql/functions/cursor-functions-transact-sql.md)
- [Cursor stored procedures (Transact-SQL)](system-stored-procedures/cursor-stored-procedures-transact-sql.md)
- [SET TRANSACTION ISOLATION LEVEL (Transact-SQL)](../t-sql/statements/set-transaction-isolation-level-transact-sql.md)

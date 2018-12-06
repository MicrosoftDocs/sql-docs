---
title: "Data Source Wizard Screen 3 (ODBC Driver for SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "09/27/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 76326eeb-1144-4b9f-85db-50524c655d30
author: MightyPen
ms.author: genemi
manager: craigg
---
# Data Source Wizard Screen 3

Specify the default database, various ANSI options to be used by the driver, and the name of a mirror server.

## Options

### Change the default database to

Specifies the name of the default database for any connection made using this data source. When this box is cleared, connections use the default database defined for the login ID on the server. When this box is selected, the database named in the box overrides the default database defined for the login ID. If the **Attach database filename** box has the name of a primary file, the database described by the primary file is attached as a database using the database name specified in the **Change the default database to** box.

Using the default database for the login ID is more efficient than specifying a default database in the ODBC data source.

### Mirror server

Specifies the name of the failover partner of the database to be mirrored. If a database name is not shown in the **Change the default database to** box, or the name shown is the default database, **Mirror Server** is grayed out.

Optionally, you can specify a server principal name (SPN) for the mirror server. The SPN for the mirror server is used for mutual authentication between client and server.

### Attach database filename

Specifies the name of the primary file for an attachable database. This database is attached and used as the default database for the data source. Specify the full path and file name for the primary file. The database name specified in the **Change the default database to** box is used as the name for the attached database.

### Use ANSI quoted identifiers

Specifies that QUOTED_IDENTIFIERS be set on when the ODBC driver for SQL Server connects. When this check box is selected, SQL Server enforces ANSI rules regarding quote marks. Double quotes can only be used for identifiers, such as column and table names. Character strings must be enclosed in single quotes:

```
SELECT "LastName"
FROM "Person.Contact"
WHERE "LastName" = 'O''Brien'
```

When this check box is cleared, applications that use quoted identifiers, such as the Microsoft Query utility that comes with Microsoft Excel, encounter errors when they generate SQL statements with quoted identifiers.

### Use ANSI nulls, paddings, and warnings

Specifies that the ANSI_NULLS, ANSI_WARNINGS, and ANSI_PADDINGS options be set on when the ODBC Driver for SQL Server connects.

With ANSI_NULLS set on, the server enforces ANSI rules regarding comparing columns for NULL. The ANSI syntax "IS NULL" or "IS NOT NULL" must be used for all NULL comparisons. The Transact-SQL syntax "= NULL" is not supported.

With ANSI_WARNINGS set on, SQL Server issues warning messages for conditions that violate ANSI rules but do not violate the rules of Transact-SQL. Examples of such errors are data truncation on execution of an INSERT or UPDATE statement, or encountering a null value during an aggregate function. 

With ANSI_PADDING set on, trailing blanks on **varchar** values and trailing zeroes on **varbinary** values are not automatically trimmed.

### Application intent

Declares the application workload type when connecting to a server. Possible values are **ReadOnly** and **ReadWrite**.

### Multi-subnet failover.

If your application is connecting to a high-availability, disaster recovery (AlwaysOn Availability Groups) availability group (AG) on different subnets, enabling **Multi-subnet failover.** configures ODBC Driver for SQL Server to provide faster detection of and connection to the (currently) active server.

### Transparent Network IP Resolution.

Alters the behavior of **Multi-subnet failover** to allow for faster reconnection during failover. See [Using Transparent Network IP Resolution](../../../connect/odbc/using-transparent-network-ip-resolution.md) for more information.

### Column Encryption.

Enables automatic decryption and encryption of data transfers to and from columns encrypted with the [Always Encrypted](../../../connect/odbc/using-always-encrypted-with-the-odbc-driver.md) feature available in SQL Server 2016 and later.

### Use FMTONLY metadata discovery:

Use the legacy SET FMTONLY metadata discovery method when connecting to SQL Server 2012 or newer. Enable this only when using queries not supported by [sp_describe_first_result_set](../../../relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql.md), such as those containing temporary tables. 

### Next

Proceeds to the next screen of the wizard.

### Back

Returns to the previous screen of the wizard.

## Next steps

[Data Source Wizard Screen 2](../../../connect/odbc/windows/dsn-wizard-2.md)

[Data Source Wizard Screen 4](../../../connect/odbc/windows/dsn-wizard-4.md)

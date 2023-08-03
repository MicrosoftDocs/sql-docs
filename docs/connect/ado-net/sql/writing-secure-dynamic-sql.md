---
title: "Writing secure dynamic SQL in SQL Server"
description: "Describes techniques for writing secure dynamic SQL using stored procedures."
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-kaywon
ms.date: "09/26/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Writing secure dynamic SQL in SQL Server

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

SQL Injection is the process by which a malicious user enters Transact-SQL statements instead of valid input. If the input is passed directly to the server without being validated and if the application inadvertently executes the injected code, the attack has the potential to damage or destroy data.  
  
Any procedure that constructs SQL statements should be reviewed for injection vulnerabilities because SQL Server will execute all syntactically valid queries that it receives. Even parameterized data can be manipulated by a skilled and determined attacker. If you use dynamic SQL, be sure to parameterize your commands, and never include parameter values directly into the query string.  
  
## Anatomy of a SQL injection attack  
The injection process works by prematurely terminating a text string and appending a new command. Because the inserted command may have additional strings appended to it before it is executed, the malefactor terminates the injected string with a comment mark "--". Subsequent text is ignored at execution time. Multiple commands can be inserted using a semicolon (;) delimiter.  
  
As long as injected SQL code is syntactically correct, tampering cannot be detected programmatically. Therefore, you must validate all user input and carefully review code that executes constructed SQL commands in the server that you are using. Never concatenate user input that is not validated. String concatenation is the primary point of entry for script injection.  
  
Here are some helpful guidelines:  
  
- Never build Transact-SQL statements directly from user input; use stored procedures to validate user input.  
  
- Validate user input by testing type, length, format, and range. Use the Transact-SQL QUOTENAME() function to escape system names or the REPLACE() function to escape any character in a string.  
  
- Implement multiple layers of validation in each tier of your application.  
  
- Test the size and data type of input and enforce appropriate limits. This can help prevent deliberate buffer overruns.  
  
- Test the content of string variables and accept only expected values. Reject entries that contain binary data, escape sequences, and comment characters.  
  
- When you are working with XML documents, validate all data against its schema as it is entered.  
  
- In multi-tiered environments, all data should be validated before admission to the trusted zone.  
  
- Do not accept the following strings in fields from which file names can be constructed: AUX, CLOCK$, COM1 through COM8, CON, CONFIG$, LPT1 through LPT8, NUL, and PRN.  
  
- Use <xref:Microsoft.Data.SqlClient.SqlParameter> objects with stored procedures and commands to provide type checking and length validation.  
  
- Use <xref:System.Text.RegularExpressions.Regex> expressions in client code to filter invalid characters.  
  
## Dynamic SQL strategies  
Executing dynamically created SQL statements in your procedural code breaks the ownership chain, causing SQL Server to check the permissions of the caller against the objects being accessed by the dynamic SQL.  
  
SQL Server has methods for granting users access to data using stored procedures and user-defined functions that execute dynamic SQL.  
  
- Using impersonation with the Transact-SQL EXECUTE AS clause.  
  
- Signing stored procedures with certificates.  
  
### EXECUTE AS  
The EXECUTE AS clause replaces the permissions of the caller with that of the user specified in the EXECUTE AS clause. Nested stored procedures or triggers execute under the security context of the proxy user. This can break applications that rely on row-level security or require auditing. Some functions that return the identity of the user return the user specified in the EXECUTE AS clause, not the original caller. Execution context is reverted to the original caller only after execution of the procedure or when a REVERT statement is issued.  
  
### Certificate signing  
When a stored procedure that has been signed with a certificate executes, the permissions granted to the certificate user are merged with those of the caller. The execution context remains the same; the certificate user does not impersonate the caller. Signing stored procedures requires several steps to implement. Each time the procedure is modified, it must be re-signed.  
  
### Cross database access  
Cross-database ownership chaining does not work in cases where dynamically created SQL statements are executed. You can work around this in SQL Server by creating a stored procedure that accesses data in another database and signing the procedure with a certificate that exists in both databases. This gives users access to the database resources used by the procedure without granting them database access or permissions.  
  
## External resources  
For more information, see the following resources.  
  
|Resource|Description|  
|--------------|-----------------|  
|[Stored Procedures](../../../relational-databases/stored-procedures/stored-procedures-database-engine.md) and [SQL Injection](../../../relational-databases/security/sql-injection.md) in SQL Server Books Online|Topics describe how to create stored procedures and how SQL Injection works.|  
  
## Next steps
- [Application security scenarios in SQL Server](application-security-scenarios-sql-server.md)

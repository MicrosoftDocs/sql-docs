---
title: List of Bugs Fixed
description: This page contains a listing of bugs fixed in each release of the Microsoft ODBC Driver for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: randolphwest, davidengel, sunilbs, mcimfl, vanto
ms.date: 09/07/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: release-notes
helpviewer_keywords:
  - "driver"
---
# List of bugs fixed

This page contains a listing of bugs fixed in each release of the [!INCLUDE [msCoName](../../includes/msconame-md.md)] ODBC Driver for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], starting with the most recent versions.

### Bug fixes in the Microsoft ODBC Driver 18.7.1.1 for SQL Server

- Fix reliability of XA distributed transactions during SQL Server connectivity failures.
- Fix vector connection attribute handling.
- Fix heap corruption in the SQL Server Network Interface (SNI) packet pool.
- Fix an out-of-bounds read when parsing malformed `LOGINACK` tokens.
- Fix length validation when parsing malformed `ENVCHANGE` tokens.
- Fix warning generation when connection timeout values are outside the supported range.
- Fix memory management when Multiple Active Result Sets (MARS) connections break abruptly.
- Fix redundant logging during Azure Key Vault token acquisition for Always Encrypted.
- Fix handling of OpenSSL errors left on the calling thread's error queue.
- Fix asynchronous timeout handling for partially length-prefixed data with zero-length chunks.
- Fix byte count reporting for overlapped named pipe writes in Tabular Data Stream (TDS) packet tracing.
- Fix asynchronous timeout handling for `DATACLASSIFICATION` tokens.
- Fix heap corruption on Linux ARM64 and macOS ARM64.
- Fix the contact email address in Linux packages.

### Bug fixes in the Microsoft ODBC Driver 17.11.1.1 for SQL Server

- Fix connection recovery to obtain the active primary node when a server moves.
- Fix DATACLASSIFICATION async timeout.
- Fix XA Recovery XIDs Calculation.
- Fix SQL_ATTR_PARAMS_PROCESSED_PTR updates so the number of processed parameter sets is reported correctly when executing parameter arrays.
- Fix SQL_ATTR_PARAMS_PROCESSED_PTR and row counting when SQL_PARAM_IGNORE is used in parameter arrays.

### Bug fixes in the Microsoft ODBC Driver 18.6.2.1 for SQL Server

- Fix SQL_ATTR_PARAMS_PROCESSED_PTR updates so the number of processed parameter sets is reported correctly when executing parameter arrays.
- Fix SQL_ATTR_PARAMS_PROCESSED_PTR and row counting when SQL_PARAM_IGNORE is used in parameter arrays.
- Fix segmentation fault when calling SQLNumResultCol in describe-only scenarios where no parameter bindings are present.
- Fix bcp_bind to correctly handle consecutive field terminators without misinterpreting them as empty fields.
- Fix RPM packaging rules to allow installing multiple driver versions side by side.
- Fix XA (X/Open distributed transaction) recovery to compute transaction identifiers correctly and avoid missing recoverable transactions.
- Fix segmentation fault on handling of NULL values in table-valued parameter (TVP) arguments.


### Bug fixes in the Microsoft ODBC Driver 18.6.1.1 for SQL Server

- Fix Tabular Data Stream (TDS) packet size handling in Built-In Diagnostics (BID) traces.

### Bug fixes in the Microsoft ODBC Driver 18.5.1.1 for SQL Server

- Fix connection recovery to obtain the active primary node when a server moves
- Fix crashes under low-memory conditions
- Fix some error messages

### Bug fixes in the Microsoft ODBC Driver 18.4.1.1 for SQL Server

- Fix crashes when receiving invalid data from server
- Fix sending of null data for fixed-length types using DAE
- Fix 10-minute delay upon disconnection after timeout
- Fix memory leak upon disconnection when an error occurs
- Fix memory leak upon reconnection with Strict encryption
- Fix intermittent crash when connecting with Strict encryption enabled and Transport Layer Security (TLS) 1.3.
- Fix crashes under low-memory conditions

### Bug fixes in the Microsoft ODBC Driver 18.3.3.1 for SQL Server

- Fix crashes when receiving invalid data from server
- Fix infinite loop when receiving invalid data from server
- Fix App Service Containers managed identity (formerly Managed Service Identity, MSI) authentication.

### Bug fixes in the Microsoft ODBC Driver 17.10.6.1 for SQL Server

- Fix crashes when receiving invalid data from server
- Fix infinite loop when receiving invalid data from server
- Fix a crash when attempting to connect under low-memory conditions
- Fix memory leak upon reconnect
- Fix App Service Containers managed identity (formerly Managed Service Identity, MSI) authentication.

### Bug fixes in the Microsoft ODBC Driver 18.3.2 for SQL Server

- Fix crashes when receiving invalid data from server
- Fix infinite loop when receiving invalid data from server
- Fix a crash when attempting to connect under low-memory conditions

### Bug fixes in the Microsoft ODBC Driver 17.10.5 for SQL Server

- Fix crashes when receiving invalid data from server

### Bug fixes in the Microsoft ODBC Driver 18.3.1 for SQL Server

- Fix a bug in setting of data classification version attribute

### Bug fixes in the Microsoft ODBC Driver 18.2.2 for SQL Server

- Fix a crash when retrieving data with Auto-Translate option off
- Fix partial writes on Linux in presence of signals
- Fix crashes when receiving invalid data from server
- Fix memory leak when processing encrypted columns
- Fix errors with long enclave queries

### Bug fixes in the Microsoft ODBC Driver 17.10.4.1 for SQL Server

- Fix a crash when retrieving data with `AutoTranslate` option off
- Fix partial writes on Linux in presence of signals
- Fix crashes when receiving invalid data from server
- Fix memory leak when processing encrypted columns

### Bug fixes in the Microsoft ODBC Driver 18.2 for SQL Server

- Fix VBS-NONE enclave attestation protocol
- Fix an error when retrieving numeric column with Regional=Yes
- Fix intermittent lack of error when server is stopped during bcp out operation
- Fix error when stored procedure call contains unquoted string parameter
- Fix a memory leak upon reconnect
- Fix a crash when receiving invalid data from server
- Correct error message when Dedicated Administrative Connections (DAC) fail

### Bug fixes in the Microsoft ODBC Driver 17.10.3 for SQL Server

- Correct error message when Dedicated Administrative Connections (DAC) fail

### Bug fixes in the Microsoft ODBC Driver 18.1.2 for SQL Server

- Fix VBS-NONE enclave attestation protocol.
- Fix error when retrieving numeric column with Regional=Yes.
- Fix intermittent lack of error when server is stopped during bcp out operation.
- Fix error when stored procedure call contains unquoted string parameter.

### Bug fixes in the Microsoft ODBC Driver 18.1 for SQL Server

- Fix intermittent issue with polling for first successful connection when multiple IP addresses are resolved.
- Fix intermittent issue where the driver stops responding when using SQLBulkOperations in async mode.
- Fix connecting with Strict and non-Strict encryption modes simultaneously.
- Fix missing dependency in Debian package.
- Fix issue with idle connection resiliency when Kerberos authentication is used.

### Bug fixes in the Microsoft ODBC Driver 18.0 for SQL Server

- Fix UI issues where text was cut off and position of items was off.
- Fix issue with Active Directory Interactive login where attempting to sign in after closing the window of the first failure would automatically succeed if cached credentials were available.
- Fix use of XADTC with Azure SQL Managed Instance.
- Fix loss of Microsoft Entra authentication mode when reconnecting an idle connection.
- Fix an issue with federated authentication when using PingFed.

### Bug fixes in the Microsoft ODBC Driver 17.10.2 for SQL Server

- Fix error when retrieving numeric column with Regional=Yes
- Fix intermittent lack of error when server is stopped during bcp out operation
- Fix error when stored procedure call contains unquoted string parameter
- Fix a crash when receiving invalid data from server

### Bug fixes in the Microsoft ODBC Driver 17.10 for SQL Server

- Fix intermittent issue with polling for first successful connection when multiple IP addresses are resolved.
- Fix missing dependency in Debian package.
- Fix for only using Microsoft Authentication Library when required.
- Fix issue with idle connection resiliency when Kerberos auth was used.

### Bug fixes in the Microsoft ODBC Driver 17.9 for SQL Server

- Fix UI issues where text was cut off and position of items was off.
- Fix issue with Active Directory Interactive login where attempting to sign in after closing the window of the first failure would automatically succeed if cached credentials were available.
- Fix use of XADTC with Azure SQL Managed Instance.
- Fix loss of Microsoft Entra authentication mode when reconnecting an idle connection.
- Fix an issue with federated authentication when using PingFed.

### Bug fixes in the Microsoft ODBC Driver 17.8 for SQL Server

- Fix for restrictions on connection string regarding usage of `UID` and `PWD` keywords
- Fix for inconsistent fonts in non-English dialogs
- Fix issue with having multiple connections using different Azure Key Vault (AKV) credentials.
- Fix issue with NonVisual Desktop Access (NVDA) not reading connection test results in the Data Source Name (DSN) configuration UI.

### Bug fixes in the Microsoft ODBC Driver 17.7.2 for SQL Server

- Fix issue with 404 Not Found errors when using Managed Service Identity authentication
- Fix for intermittent Encryption Not Supported errors under high multithreaded loads
- Fix for intermittent crash under high multithreaded loads

### Bug fixes in the Microsoft ODBC Driver 17.7 for SQL Server

- Fix character encoding of VARIANT columns in BCP NATIVE mode
- Fix setting of SQL_ATTR_PARAMS_PROCESSED_PTR under specific conditions
- Fix SQLDescribeParam in FMTONLY mode for statements containing comments
- Fix an issue with federated authentication when using Okta
- Fix excessive memory usage on multi-processor systems
- Fix Microsoft Entra authentication for some variants of Azure SQL

### Bug fixes in the Microsoft ODBC Driver 17.6 for SQL Server

- Fix Microsoft Authentication Library error when authenticating with a federated account (Windows)
- Fix an issue where the driver becomes unresponsive when a timeout occurs during an asynchronous notification operation
- Fix driver reference count upon upgrade in Alpine Linux
- Fix libc6 dependency version for Ubuntu
- Add missing defines to Linux/Mac msodbcsql.h

### Bug fixes in the Microsoft ODBC Driver 17.5.2.2 for SQL Server (Alpine Linux only)

- Fix a crash when using Always Encrypted with secure enclaves on Alpine Linux

### Bug fixes in the Microsoft ODBC Driver 17.5.2 for SQL Server

- Added msodbcsql.h to Alpine Linux package

### Bug fixes in the Microsoft ODBC Driver 17.5 for SQL Server

- Fix AKV CMK metadata hash computation on Linux/macOS
- Fix error when loading OpenSSL 1.0.0
- Fix conversion issues when using ISO-8859-1 and ISO-8859-2 codepages
- Fix internal library name on macOS to include version number
- Fix setting of null indicator when separate length and indicator bindings are used

### Bug fixes in the Microsoft ODBC Driver 17.4.2 for SQL Server

- Fix for an issue where process ID and application name wouldn't be sent correctly to SQL Server (for `sys.dm_exec_sessions` analysis) (Linux)
- Removed redundant dependency on libuuid (Linux)
- Fix for a bug with sending UTF8 data to SQL Server 2019
- Fix for a bug where locales that end in "@euro" weren't being correctly detected (Linux)
- Fix for XML data being returned incorrectly when fetched as an output parameter while using Always Encrypted

### Bug fixes in the Microsoft ODBC Driver 17.4 for SQL Server

- Fix for intermittent issue when Multiple Active Results Sets (MARS) is enabled where the driver stops responding
- Fix connection resiliency issue when async notification is enabled where the driver stops responding
- Fix crash when retrieving diagnostic records for multithreaded connection attempts
- Fix 'Encryption not supported' upon reconnect after calling SQLGetInfo() with SQL_USER_NAME and SQL_DATA_SOURCE_READ_ONLY
- Fix COM initialization error during Microsoft Entra interactive authentication
- Fix SQLGetData() for multi-byte UTF8 data
- Fix retrieving length of sql_variant columns using SQLGetData()
- Fix importing of sql_variant columns containing more than 7,992 bytes using bcp
- Fix sending of correct encoding to server for narrow character data

### Bug fixes in the Microsoft ODBC Driver 17.3 for SQL Server

- Fixed TCP send notification event handle memory leak
- Fixed redefinition issue of enum _SQL_FILESTREAM_DESIRED_ACCESS in msodbcsql.h header file
- Fixed missing ACCESS_TOKEN and AUTHENTICATION related definition in msodbcsql.h header file for Linux

### Bug fixes in the Microsoft ODBC Driver 17.2 for SQL Server

- Fixed an error message about Microsoft Entra authentication
- Fixed encoding detection when locale environment variables are set differently
- Fixed a crash upon disconnect with connection recovery in progress
- Fixed detection of connection liveness
- Fixed incorrect detection of closed sockets
- Fixed an infinite wait when attempting to release a statement handle during failed recovery
- Fixed incorrect uninstallation behavior when both versions 13 and 17 are installed on Windows
- Fixed decryption behavior on older Windows platform (Windows 7, 8 and Server 2012)
- Fixed a cache issue when using ADAL Authentication on Windows
- Fixed an issue that was locking and overwriting trace logs on Windows

### Bug fixes in the Microsoft ODBC Driver 17.1 for SQL Server

- Fixed 1-second delay when calling SQLFreeHandle with MARS enabled and connection attribute "Encrypt=yes"
- Fixed an error 22003 crash in SQLGetData when the size of the buffer passed in is smaller than the data being retrieved (Windows)
- Fixed truncated ADAL error messages
- Fixed a rare bug on 32-bit Windows when converting a floating point number to an integer
- Fixed an issue where inserting double into decimal field with Always Encrypted on would return data truncation error
- Fixed a warning on macOS installer
- Fixed sending incorrect state to SQL Server during Session Recovery attempt when Connection Resiliency and Connection Pooling both are enabled, causing the server to drop the session

### Bug fixes in the Microsoft ODBC Driver 17 for SQL Server

- Fixed a bug where when using Kerberos authentication, bulk insert could fail with "access denied" error
- Removed workaround for a unixODBC bug present in versions older than 2.3.1 (driver doubled the sizes of certain buffers passed to unixODBC)
- Fixed Connection Resiliency (reconnect) stopping to respond when using ColumnEncryption=enabled
- Fixed DSN creation bug, where when using "Active Directory Interactive authentication" option Azure Authentication window could become unresponsive (Windows)
- Fixed a rare crash during ODBC shutdown when asynchronous execution is enabled (happened when clearing connection handle)
- Fixed an issue where SQL Driver caused high CPU consumption while executing long stored procedures
- Fixed inability to retrieve data in an encrypted varbinary(max) column without conversion
- Fixed a problem where after a null varchar(max) encrypted column is fetched using SQLGetData() on a static cursor, the following column is also nulled even if it has data
- Fixed an issue with fetching varbinary(max) field with Always Encrypted on
- Fixed a problem of setlocale() not working with Always Encrypted
- Fixed an issue with SQLDescribeParam() returning error when called on XML-type stored procedure parameter with Always Encrypted on
- Fixed escaped underscores not working in SQLTables
- Fixed a bug where Hebrew data (varchar) is truncated when returned as wide chars on Linux
- Fixed an issue with querying Shift-JIS encoded char/varchar from UTF-8 application
- Fixed the bug where calling SQLGetInfo with SQL_DRIVER_NAME parameter returned Linux-style filename on macOS
- Fixed an issue where loading Windows-1252 character data, using input files larger than 32k bytes into VARCHAR columns using the BCP utility would result in failures

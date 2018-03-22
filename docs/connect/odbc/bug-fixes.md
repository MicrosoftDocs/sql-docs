### Bug fixes in the [!INCLUDE[msCoName](../../includes/msconame_md.md)] ODBC Driver 17.1 for [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)]

- Fixed 1-second delay upon SQLFreeHandle with Encrypt and MARS
- Fixed an error 22003 crash in SQLGetData on Windows
- Fixed truncated ADAL error messages
- Fixed a rare floating point conversion bug on 32-bit Windows
- Fixed an issue where inserting double into decimal field with AE on would no return data truncation error
- Fixed a warning on MacOS installer

### Bug fixes in the [!INCLUDE[msCoName](../../includes/msconame_md.md)] ODBC Driver 17 for [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)]

- Fixed unexpected Session Recovery parsing errors when using Connection Resiliency and Connection Pooling at the same time
- Fixed a bulk insert problem where "access denied" was issue, due to not handling SPNs correctly
- Removed workaround for a unixODBC bug present in version below 2.3.1
- Fixed Connection Resiliency (reconnect) hanging when using ColumnEncryption=enabled
- Fixed DSN creation bug, where window could become unresponsive (Windows)
- Fixed a rare crash during ODBC shutdown
- Fixed an issue where SQL Driver caused high CPU consumption while executing long stored procedures
- Fixed inability to retrieve data in an encrypted varbinary(max) column without conversion
- Fixed a problem where after a null varchar(max) encrypted column is fetched using SQLGetData() on a static cursor, the following column is also nulled even if it has data
- Fixed an issue with fetching varbinary(max) field with AE on
- Fixed a problem of setlocale not working with AE
- Fixed an issue with SQLDescribeParam() returning error when callen on XML-type stored procedure parameter with AE on
- Fixed escaped underscores not working in SQLTables
- Fixed a bug where Hebrew data (varchar) is chopped when returned as wide chars on Linux
- Fixed an issue with querying Shift-JIS encoded char/varchar from UTF-8 application
- Fixed the name property issue of the SQL Login dialog
- Fixed multiple large file import failing with BCP API

### Bug fixes in the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver 17.1 for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)]

- Fixed 1-second delay upon SQLFreeHandle with Encrypt and MARS
- Fixed the type of cekMDversion, it is now an 8-byte integer, not an array of 8 bytes.
- Fixed possible attempts to convert negatively sized strings in stringtonumeric function.
- Fixed in issue with invalid option being passed to send() on MacOS
- Fixed an error 22003 crash in SQLGetData on Windows
- Fixed truncated ADAL error messages
- Fixed a rare floating point conversion bug on 32-bit Windows
- Fixed an issue where inserting double into decimal field with AE on would no return data truncation error
- Fixed a warning on MacOS installer
- Fixed the issue of connecting to HostPipe with SQLNCLI17E

---
author: dzsquared
ms.author: drskwier
ms.prod: sql
ms.topic: include
---

|**/TargetConnectionString:**|**/tcs**|{string}|Specifies a valid SQL Server/Azure connection string to the target database. If this parameter is specified, it shall be used exclusively of all other target parameters. |
|**/TargetDatabaseName:**|**/tdn**|{string}|Specifies an override for the name of the database that is the target of SqlPackage.exe Action. |
|**/TargetEncryptConnection:**|**/tec**|{Optional&#124;Mandatory&#124;Strict&#124;True&#124;False}|Specifies if SQL encryption should be used for the target database connection. |
|**/TargetHostNameInCertificate:**|**/thnic**|{string}|Specifies value that is used to validate the target SQL Server TLS/SSL certificate when the communication layer is encrypted by using TLS.|
|**/TargetPassword:**|**/tp**|{string}|For SQL Server Auth scenarios, defines the password to use to access the target database. |
|**/TargetServerName:**|**/tsn**|{string}|Defines the name of the server hosting the target database. |
|**/TargetTimeout:**|**/tt**|{int}|Specifies the timeout for establishing a connection to the target database in seconds. For Azure AD, it is recommended that this value be greater than or equal to 30 seconds.|
|**/TargetTrustServerCertificate:**|**/ttsc**|{True&#124;False}|Specifies whether to use TLS to encrypt the target database connection and bypass walking the certificate chain to validate trust. |
|**/TargetUser:**|**/tu**|{string}|For SQL Server Auth scenarios, defines the SQL Server user to use to access the target database. |
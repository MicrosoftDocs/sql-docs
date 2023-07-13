---
title: "Connection Encryption Troubleshooting"
description: "Learn how to troubleshoot common connection issues related to connection encryption."
author: David-Engel
ms.author: v-davidengel
ms.date: "07/31/2023"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Connection Encryption Troubleshooting

### SSL Provider: The certificate chain was issued by an authority that is not trusted.
Connection encryption is enabled by default in version 18 and newer. Users may see this error if the SQL Server isn't configured to use certificates. To configure connection encryption for the server, see [Configure SQL Server Database Engine for encrypting connections](../../database-engine/configure-windows/configure-sql-server-encryption.md).

Users can also choose to set the `Encrypt` connection string keyword to `no`/`optional` to disable connection encryption to match the default behavior prior to version 18. In the DSN Configuration UI, this option is set using the `Connection Encryption` dropdown. If connection encryption is desired, `TrustServerCertificate` can also be set to `yes` to skip server certificate validation.

### SSL Provider: The target principal name is incorrect.
Users may see this error if the host name in the certificate returned by the server doesn't match what is expected. By default, the server name is used to check against the certificate. The `HostNameInCertificate` keyword can be used to specify the name expected from the server certificate. Alternatively, a certificate can also be specified to match and verify the returned server certificate against by using the `ServerCertificate` keyword (v18.1+). For more information, see [DSN and Connection String Keywords and Attributes](dsn-connection-string-attribute.md).

You may also use `TrustServerCertificate` to skip server certificate validation.

--------------------------------------------------
## See Also  
* [DSN and Connection String Keywords and Attributes](dsn-connection-string-attribute.md)
* [Data Source Wizard Screen 4](windows/dsn-wizard-4.md)

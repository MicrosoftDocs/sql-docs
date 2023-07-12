---
title: "Connection Encryption Troubleshooting"
description: "Learn how to troubleshoot common connection issues related to connection encryption."
author: David-Engel
ms.author: v-davidengel
ms.date: "03/14/2022"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Connection Encryption Troubleshooting

### SSL Provider: The certificate chain was issued by an authority that is not trusted.
Connection encryption is now enabled by default in version 18 and newer. Users may see this if the SQL Server is not configured to use certificates. To configure connection encryption for the server, see [Configure SQL Server Database Engine for encrypting connections](../../database-engine/configure-windows/configure-sql-server-encryption.md).

Users can also choose to set the `Encrypt` connection string keyword to `no`/`optional` to disable connection encryption, which will match the default behavior prior to version 18. In the DSN Configuration UI, this is set using the `Connection Encryption` dropdown. If connection encryption is desired, `TrustServerCertificate` can also be set to `yes` to trust the server's certificate.

### SSL Provider: The target principal name is incorrect.
Users may see this if the host name in the certificate returned by the server does not match what is expected. By default, the server name is used to check against the certificate. The `HostNameInCertificate` keyword can be used to specify the name expected from the server certificate. Alternatively, a server certificate can also be specified to verify against the returned server certificate by using the `ServerCertificate` keyword (v18.1+). For more information, see [DSN and Connection String Keywords and Attributes](dsn-connection-string-attribute.md).

You may also use `TrustServerCertificate` to skip server certificate validation.

--------------------------------------------------
## See Also  
* [DSN and Connection String Keywords and Attributes](dsn-connection-string-attribute.md)
* [Data Source Wizard Screen 4](windows/dsn-wizard-4.md)

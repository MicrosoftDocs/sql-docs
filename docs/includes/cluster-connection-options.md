---
author: uc-msft
ms.author: umajay
ms.reviewer: vanto
ms.date: 04/22/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: include
---
| Key | Supported Values | Description |
| --- | --- | --- |
| `Encrypt` | `Mandatory`, `Strict`, `Optional` | Specifies how encryption to the availability group is enforced. If the server doesn't support encryption, the connection fails. If you set encryption to `Mandatory`, then `TrustServerCertificate` must be set to yes. If you set encryption to `Strict`, then `TrustServerCertificate` is ignored.<br /><br />**Note**: This key value pair is required. |
| `HostNameInCertificate` | Replica name or AG listener name | Specifies the replica name or availability group listener name in the certificate that's used for encryption. This value must match the value in the **Subject Alternative Name** of the certificate. If the server name is listed in the certificate, then you can omit the `HostNameInCertificate` key-value pair. If the server name isn't listed in the certificate, then you must specify the `HostNameInCertificate` key-value pair with the server name.<br /><br />**Note**: This key value pair is optional. |
| `TrustServerCertificate` | `Yes`, `No` | Set to `yes` to specify that the driver doesn't validate the server TLS/SSL certificate. If `no`, the driver validates the certificate. For more information, review [TDS 8.0](../relational-databases/security/networking/tds-8.md#additional-changes-to-connection-string-encryption-properties).<br /><br />**Note**: This key value pair is optional. |
| `ServerCertificate` | Path to your certificate | If you don't want to use `HostNameInCertificate`, you can pass the path to your certificate. The cluster service account must have permission to read the certificate from the given location.<br /><br />**Note**: This key value pair is optional. |
| `CLUSTER_CONNECTION_OPTIONS` | Empty string (`''`) | Clears the existing configuration and reverts to default encryption settings of `Encrypt=Mandatory` and `TrustServerCertificate=Yes`. |

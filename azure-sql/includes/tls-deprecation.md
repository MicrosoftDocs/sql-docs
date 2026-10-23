---
author: VanMSFT
ms.author: vanto
ms.reviewer: vanto
ms.date: 09/19/2025
ms.service: azure-sql
ms.topic: include
---
Azure has announced that support for older TLS versions (TLS 1.0, and 1.1) ends August 31, 2025. For more information, see [TLS 1.0 and 1.1 deprecation](https://azure.microsoft.com/updates/azure-support-tls-will-end-by-31-october-2024-2/).

Starting November 2024, you'll no longer be able to set the minimal TLS version for Azure SQL Database and Azure SQL Managed Instance client connections below TLS 1.2.

#### Why is TLS 1.0 and 1.1 being retired?

TLS versions 1.0 and 1.1 are outdated and no longer meet modern security standards. They're being retired to:

- Reduce exposure to known vulnerabilities.
- Align with industry best practices and compliance requirements.
- Ensure clients are using stronger encryption protocols like TLS 1.2 or TLS 1.3.

#### What happens if TLS 1.0 and 1.1 are used after August 31, 2025?

After August 31, 2025, TLS 1.0 and 1.1 will no longer be supported, and connections using TLS 1.0 and 1.1 will likely fail. It's critical to transition to a minimum of TLS 1.2 or higher before the deadline.

#### How can I check if my SQL Database, SQL Managed Instance, Cosmos DB, or MySQL instances are using TLS 1.0/1.1?

- To identify clients that are connecting to your Azure SQL Database using TLS 1.0 and 1.1, [SQL audit logs](/azure/azure-sql/database/auditing-setup) must be enabled. With auditing enabled, you can view client connections.

- To identify clients that are connecting to your Azure SQL Managed Instance using TLS 1.0 and 1.1, [auditing](/azure/azure-sql/managed-instance/auditing-configure) must be enabled. With auditing enabled, you can [consume audit logs](/azure/azure-sql/managed-instance/auditing-configure#consume-audit-logs) with Azure Storage, Event Hubs, or Azure Monitor Logs to view client connections.

- To verify the minimum TLS version of your Azure Cosmos DB, [get the current value of the `minimalTlsVersion` property](/azure/cosmos-db/self-serve-minimum-tls-enforcement#how-to-verify-minimum-tls-version-enforcement) using Azure CLI or Azure PowerShell.

- To verify the minimum TLS version configured for your Azure Database for MySQL Server, check the value of the `tls_version` server parameter using the MySQL command-line interface to understand what protocols are configured.

#### Why was my service flagged if I've already configured TLS 1.2?

Services might be incorrectly flagged due to:

- Intermittent fallback to older TLS versions by legacy clients.
- Misconfigured client libraries or connection strings that don't enforce TLS 1.2.
- Telemetry lag or false positives in detection logic.

#### What should I do if I received a retirement notice in error?

If your server or database is already configured with minimum TLS 1.2, or configured with no minimum TLS (the default setting in SQL Database and SQL Managed Instance `minimalTLSVersion` that maps to `0`) and connecting with 1.2, no action is required.

#### What happens if my application or client library doesn't support TLS 1.2?

Connections will fail once TLS 1.0/1.1 are disabled. You must upgrade your client libraries, drivers, or frameworks to versions that support TLS 1.2.

#### What if my server is configured with no minimum TLS version?

Servers configured with no minimum TLS version and connecting with TLS 1.0/1.1 should be upgraded to minimum TLS version 1.2. For servers configured with no minimum TLS version and connecting with 1.2, no action is required. For servers configured with no minimum TLS version and using encrypted connections, no action is required.

#### How will I be notified about TLS retirement for my resources?

Email reminders will continue leading up to the retirement of TLS 1.0 and 1.1 in August.

#### Who can I contact if I need help with validating or with updating my TLS settings?

If you need help with validating or with updating your TLS settings, contact [Microsoft Q&A](/answers/questions/) or open a support ticket using the Azure portal if you have a support plan.

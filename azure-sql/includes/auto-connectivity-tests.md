---
author: bluefooted
ms.author: pamela
ms.date: 05/13/2026
ms.service: azure-sql-managed-instance
ms.topic: include
---
Azure SQL Managed Instance runs automatic internal connectivity tests to monitor service reliability and accelerate issue detection. These tests run every 10 seconds from internal IP addresses within the SQL managed instance's subnet and have a negligible performance impact on network throughput and service performance. One test validates end-to-end connectivity by attempting a login with a known-to-fail credential (`AzureSQLConnectivityChecker`), which generates expected failed login entries in audit logs, Extended Events, and SQL error logs. These entries are normal and don't indicate a security problem. For more information, including how to identify test signatures in your logs, see [Automatic internal connectivity tests](../managed-instance/connectivity-testing-overview.md).
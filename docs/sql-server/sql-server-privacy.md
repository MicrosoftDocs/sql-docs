---
title: SQL Server privacy supplement
description: SQL Server privacy supplement
ms.date: 05/31/2022
ms.service: sql
ms.subservice: release-landing
ms.reviewer: "wopeter"
ms.custom: ""
ms.topic: conceptual
f1_keywords: 
helpviewer_keywords: 
author: MikeRayMSFT
ms.author: mikeray
---

# SQL Server privacy supplement

[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]

This article summarizes Internet-enabled features that can collect and send anonymous feature usage and diagnostic data to Microsoft. SQL Server may collect standard computer information and data about usage and performance may be transmitted to Microsoft and analyzed for purposes of improving the quality, security, and reliability of the product. If you install SQL Server in a virtual machine, a container, or a physical server using an applicable Azure service, environment information may be sent to Microsoft so that Microsoft can install the necessary SQL Server extension and register the installed SQL Server instances in your Azure account.

The applicable Azure services include:

- Azure Virtual Machines
- Azure VMware Solution
- Azure Kubernetes Service
- Azure Arc-enabled Servers
- Azure Arc-enabled VMware vSphere
- Azure Arc-enabled Kubernetes
- Azure Stack HCI  

Refer to the [Microsoft Privacy Statement](https://go.microsoft.com/fwlink/?LinkId=521839) for more information about privacy. 

The data classification in this article only applies to versions of the SQL Server on-premises product.

It doesn't apply to the items listed below:
- [Azure Data Studio](../azure-data-studio/usage-data-collection.md)
- [SQL Server Management Studio (SSMS)](../ssms/sql-server-management-studio-telemetry-ssms.md)
- [SQL Server Data Tools (SSDT)](../ssdt/anonymous-usage-data.md)
- [Database Migration Assistant (DMA)](../dma/dma-diagnostic-data-collection.md)
- [SQL Server Migration Assistant (SSMA)](../ssma/ssma-diagnostic-data-collection.md)

Definition of *Permitted usage Scenarios*. For the context of this article, Microsoft defines "Permitted Usages Scenarios" as actions or activities that are initiated by Microsoft.

## Access control

Credential-related information used to secure logins, users, or accounts within a SQL Server installation.

### Examples of access control

- Passwords
- Certificates

### Permitted usage scenarios

|Scenario |Access restrictions |Retention requirements |
|---------|---------|---------|
|These credentials never leave the user machine via Usage and Diagnostics Data. |- |- |
|Crash Dumps may contain Access Control Data. |- |Crash Dumps: Maximum 30 days. |
|These credentials never leave the user machine via User Feedback unless customer injects it manually |Limit to Microsoft internal use with no third-party access. |User Feedback: Max 1 year|

## Customer data

Customer data is defined as data stored within user tables, directly or indirectly. The data includes statistics or user literals within query texts that might be stored within user tables.

### Examples of customer data

- Data values stored within the rows of any user table.
- Statistics objects containing copies of values within the rows of any user table.
- Query texts containing literal values.

### Permitted usage scenarios

|Scenario  |Access restrictions  |Retention requirements |
|---------|---------|---------|
|This data doesn't leave the user machine via Usage and Diagnostics Data. |- |- |
|Crash Dumps may contain customer data and be emitted to Microsoft. |- |Crash Dumps: Max 30 days. |
|Customers with their consent can send User Feedback that contains customer data to Microsoft. |Limit to Microsoft internal with no third-party access. Microsoft can expose the data to the original customer. |User Feedback: Max 1 year |

## Personal data

Data received from a user, or generated from their use of the product.
- Linkable to an individual user.
- Doesn't contain customer data.

### Examples of personal data

- Interface Identification. The Full IP address
- Machine Name
- Login/User names
- Local-part of e-mail address (joe@contoso.com)
- Location Information
- Customer Identification

### Permitted usage scenarios

|Scenario  |Access restrictions  |Retention requirements|
|---------|---------|---------|
|This data doesn't leave the user machine via Usage and Diagnostics Data. |- |- |
|Crash dumps may contain personal data and be emitted to Microsoft. |- |Crash dumps: Max 30 days |
|Customer identification ID may be emitted to Microsoft to deliver new hybrid and cloud features that the users have subscribed to. |- |Currently no such hybrid or cloud features exist.|
|Customers with their consent can send User Feedback that contains customer data to Microsoft.|Limit to Microsoft internal use with no third-party access. Microsoft can expose the data to the original customer. |User feedback: Max 1 year |

## Internet-based services data

Data needed to provide Internet-based services,  per the SQL Server EULA.

### Examples of Internet-based services data

- Computer specification information
- Browser name/version
- SQL Server version
- Language code
- An IP address with certain octets removed
- Map data

### Permitted usage scenarios

|Scenario  |Access restrictions  |Retention requirements|
|---------|---------|---------| 
|May be used by Microsoft to improve features and/or fix bugs in current features. |Limit to Microsoft internal use with no third-party access. Microsoft can expose the data to the original customer.  For example, dashboards |Min 90 days - Max 3 years |
|Customers with their consent can send User Feedback that contains customer data to Microsoft. |Limit to Microsoft internal use with no third-party access. |Customers with their consent can send User Feedback that contains customer data to Microsoft. |
|Power View and SQL Reporting Services Map Item(s) may send data for use of Bing Maps. |Limit to session data |- |

> [!NOTE]
> Power View support is no longer available after SQL Server 2017.

## Non-personal data

1. Data received from an organization, or generated from their use of the product. It's linkable to an organization and doesn't contain customer data.

   - Example
     - Organization name (example: Microsoft Corp.)

   - Permitted usage scenarios

     |Scenario  |Access restrictions  |Retention requirements|
     |---------|---------|---------|
     | Microsoft may collect generic usage data of SQL Server instances running SQL Server with an applicable Azure Service for the express purpose of giving customers optional benefits within Azure. | Microsoft can expose data to the customer, such as through the Azure portal, to help customers running SQL Server with an applicable Azure service to access the specific benefits in Azure. </br></br>Microsoft doesn't use this data for licensing audits without customer's advance consent. | Min 90 days - Max 3 years |

2. Data that describes or is used to configure servers, databases, tables, and other resources created or provided by customers. It includes database table and column names but not the contents of database rows or other customer data. Customers shouldn't place any personal data in those fields or create applications designed to store personal data in these fields. For the permitted usage scenarios below, only hash form is used to determine usage patterns to improve the product.

   - Example
      - SQL Server database names
      - Table names and column names
      - Statistics names

    - Permitted usage scenarios

      > [!NOTE]
      > All metadata values are hashed before collection.
      >

      |Scenario  |Access restrictions  |Retention requirements|
      |---------|---------|---------|
      |May be used by Microsoft to improve features and or fix bugs in current features. |Limited to Microsoft internal use with no third-party access. |Min 90 days - Max 3 years|

3. Data that is generated in the course of running the server. It doesn't contain customer data, non-personal data as listed in 1. or 2. (above), customer access control data, or personal data.

   - Example
     - Database GUID
     - Hash of machine name
     - Hash of instance name
     - Application name
     - Behavioral/usage data
     - SQL Customer Experience improvement program data (SQLCEIP)
     - Server configuration data, for example settings of sp_configure
     - Feature configuration data
     - Event names and error codes
     - Hardware settings and identification such as OEM Manufacturer

   Microsoft does examine application name values set by other programs that use SQL Server (example: SharePoint or third-party packaged programs and includes this information in metadata fields sent to Microsoft when Usage Data is enabled). Customers shouldn't place personal data in those metadata fields or create applications designed to store personal data in these fields.

   - Permitted usage scenarios

     |Scenario  |Access restrictions  |Retention requirements|
     |---------|---------|---------|
     |May be used by Microsoft to improve features and or fix bugs in current features.|Limit to Microsoft internal use with no third-party access. |Min 90 days - Max 3 years |
     |May be used to make suggestions to the customer.  For example, "Based on your usage of the product, consider using feature *X* since it would perform better." |Microsoft can expose the data to the original customer, for example through dashboards. |Customer Data Security Logs: Min 3 years - Max 6 years |
     |May be used by Microsoft for future product planning. |Microsoft may share this information with other hardware and software vendors to improve how their products run with Microsoft software. |Min 90 days - Max 3 years|
     |May be used by Microsoft to provide cloud-based services based on emitted Usage and Diagnostics Data. For example, a customer dashboard showing feature usage across all SQL Server installations in an organization. |Microsoft can expose the data to the original customer, for example, through dashboards. |Min 90 days - Max 3 years |
     |Customers with their consent can send User Feedback that contains customer data to Microsoft. |Limit to Microsoft internal with no third-party access. Microsoft can expose the data to the original customer. |User Feedback: Max 1 year |
     |May use database name and application name to categorize databases and applications into known categories, for example, those that may be running software provided by Microsoft or other companies.|Limit to Microsoft internal with no third-party access.|Min 90 days - Max 3 years |

## System-generated logs controls

Instructions on how system-generated logs can be turned on/off in product can be referenced here - [Configure usage and diagnostic data collection for SQL Server (CEIP)](usage-and-diagnostic-data-configuration-for-sql-server.md).

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]

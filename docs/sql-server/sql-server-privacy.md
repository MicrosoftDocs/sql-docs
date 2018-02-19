---
title: "SQL Server Privacy Supplement| Microsoft Docs"
ms.date: "2/19/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "sql-non-specified"
ms.service: ""
ms.component: "sql-non-specified"
ms.reviewer: ""
ms.suite: "sql"
ms.custom: ""
ms.technology: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
helpviewer_keywords: 
author: "craigg-msft"
ms.author: "craigg"
manager: "jhubbard"
ms.workload: "Active"
---
# SQL Server Documentation
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This article summarizes the behavior of different data objects used within SQL Server and how the objects are used to pass information of a personal or confidential manner. The data classification in this article only applies to versions of the SQL Server on-premises product. It does not apply to the following:

- Azure SQL Database
- SQL Server Management Studio (SSMS)
- SQL Server Data Tools (SSDT)
- SQL Operations Studio

>## Access Control

Credential-related information used to secure logins, users, or accounts within a SQL Server installation.

### Examples of access control

- Passwords
- Certificates

### Permitted Usage Scenarios

|Scenario  |Access Restrictions  |Retention Requirements |
|---------|---------|---------|
|These credentials never leave the user machine via Usage Feedback.     |-         |-         |
|Crash Dumps may contain Access Control Data.     |-         |Crash Dumps: Maximum 30 days.         |
|These credentials never leave the user machine via User Feedback unless Customer injects it manually    |Limit to Microsoft internal use with no third-party access.         |User Feedback: Max 1 year         |
 |
>## Customer Content

Customer content is defined as data stored within user tables, directly or indirectly. The data includes statistics or user literals within query texts that might be stored within user tables.

### Examples of customer content

- Data values stored within the rows of any user table.
- Statistics objects containing copies of values within the rows of any user table.
- Query texts containing literal values.

### Permitted Usage Scenarios
|Scenario  |Access Restrictions  |Retention Requirements |
|---------|---------|---------|
|This data does not leave the user machine via Usage Feedback. |- |- |
|Crash Dumps may contain Customer Content and be emitted to Microsoft. |- |Crash Dumps: Max 30 days. |
|Customers with their consent can send User Feedback that contains Customer Content to Microsoft. |Limit to Microsoft internal with no third party access. Microsoft can expose the data to the original customer. |User Feedback: Max 1 year |

>## End-User Identifiable Information (EUII)

Data received from a user, or generated from their use of the product.
- Linkable to an individual user.
- Does not contain content.

### Examples end-User identifiable information

- Interface ID (Full IP address)
- Machine Name
- Login/User names
- Local-part of e-mail address (joe@contoso.com)
- Location Information
- Customer Identification ID

### Permitted Usage Scenarios

|Scenario  |Access Restrictions  |Retention Requirements|
|---------|---------|---------|
|This data does not leave the user machine via Usage Feedback. |- |- |
|Crash Dumps may contain EUII and be emitted to Microsoft. |- |Crash Dumps: Max 30 days |
|Customer Identification ID may be emitted to Microsoft to deliver new hybrid and cloud features that the users have subscribed to. |- |Currently no such hybrid or cloud features exist.|
|Customers with their consent can send User Feedback that contain Customer Content to Microsoft.|Limit to Microsoft internal use with no third party access. Microsoft can expose the data to the original customer. |User Feedback: Max 1 year |

>## Internet-Based Services Data

Data needed to provide Internet-based services,  per the SQL Server EULA.

### Examples of Internet-based services data

- Computer Specification Information
- Browser name/version
- SQL Server version
- Language Code
- An IP Address with specif octets removed
- Map Data

### Permitted Usage Scenarios

|Scenario  |Access Restrictions  |Retention Requirements|
|---------|---------|---------| 
|May be used by Microsoft to improve features and/or fix bugs in current features. |Limit to Microsoft internal use with no third-party access. Microsoft can expose the data to the original customer.  For example, dashboards |Min 90 days - Max 3 years |
|Customers with their consent can send User Feedback that contain Customer Content to Microsoft. |Limit to Microsoft internal use with no third-party access. |Customers with their consent can send User Feedback that contain Customer Content to Microsoft. |
|Power View and SQL Reporting Services Map Item(s) may send data for use of Bing Maps. |Limit to session data |- |

>## System Metadata

Data generated in the course of running the server.  It does not contain Customer content.

### Examples of system metadata

The following are considered system metadata when they do not inlcude customer content, customer access control, or EUII:

- Database GUID
- Hash of Machine Name
- Hash of Instance Name
- Hash of Application Name
- Behavioral/Usage Data
- SQL Customer Experience improvement program data  (SQLCEIP)
- Server configuration data, for example settings of sp_configure
- Feature configuration data
- Event Names and Error Codes

### Permitted Usage Scenarios

|Scenario  |Access Restrictions  |Retention Requirements|
|---------|---------|---------|
|May be used by Microsoft to improve features and or fix bugs in current features.|Limit to Microsoft internal use with no third-party access. |Min 90 days - Max 3 years |
|May be used to make suggestions to the customer.  For example, “Based on your usage of the product, consider using feature X since it would perform better.”. |Microsoft can expose the data to the original customer, for example through dashboards. |Customer Data Security Logs: Min 3 years - Max 6 years |
May be used by Microsoft for future product planning. |Microsoft may share this information with other hardware and software vendors to improve how their products run with Microsoft software. |Min 90 days - Max 3 years|
|May be used by Microsoft to provide cloud-based services based on emitted Usage Feedback. For example, a customer dashboard showing feature usage across all SQL Server installations in an organization. |Microsoft can expose the data to the original customer, for example, through dashboards. |Min 90 days - Max 3 years |
|Customers with their consent can send User Feedback that contain Customer Content to Microsoft. |Limit to Microsoft internal with no third-party access. Microsoft can expose the data to the original customer. |User Feedback: Max 1 year |

>## Object Metadata

Data that describes or is used to configure servers, databases, tables, and other resources.  Object metadata includes database table and column names but not the contents of database rows or other Customer Content. Customers should not place personal data, such as end-user identifiable information in Object Metadata fields or create applications designed to store personal data in these fields.

### Examples of object metadata

- SQL Server database names
- Table names and Column names
- Statistics Names

### Permitted Usage Scenarios

None

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
---
title: "SQL Server privacy supplement | Microsoft Docs"
ms.date: 4/25/2018
ms.prod: sql
ms.reviewer: ""
ms.custom: ""
ms.topic: conceptual
f1_keywords: 
helpviewer_keywords: 
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# SQL Server privacy supplement
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This article summarizes the behavior of different data objects used within SQL Server and how the objects are used to pass information of a personal or confidential manner. This article serves as an addendum to the overall [Microsoft Privacy Statement](https://go.microsoft.com/fwlink/?LinkId=521839). The data classification in this article only applies to versions of the SQL Server on-premises product. It does not apply to the items:

- Azure SQL Database
- SQL Server Management Studio (SSMS)
- SQL Server Data Tools (SSDT)
- Azure Data Studio
- Database Migration Assistant
- SQL Server Migration Assistant
- MS-SQL Extension

Definition of *Permitted usage Scenarios*. For the context of this article, Microsoft defines "Permitted Usages Scenarios" as actions or activities that are initiated by Microsoft.

## Access control

Credential-related information used to secure logins, users, or accounts within a SQL Server installation.

### Examples of access control

- Passwords
- Certificates

### Permitted usage scenarios

|Scenario  |Access restrictions  |Retention requirements |
|---------|---------|---------|
|These credentials never leave the user machine via Usage Feedback.     |-         |-         |
|Crash Dumps may contain Access Control Data.     |-         |Crash Dumps: Maximum 30 days.         |
|These credentials never leave the user machine via User Feedback unless customer injects it manually    |Limit to Microsoft internal use with no third-party access.         |User Feedback: Max 1 year         |
 |
## Customer content

Customer content is defined as data stored within user tables, directly or indirectly. The data includes statistics or user literals within query texts that might be stored within user tables.

### Examples of customer content

- Data values stored within the rows of any user table.
- Statistics objects containing copies of values within the rows of any user table.
- Query texts containing literal values.

### Permitted usage scenarios
|Scenario  |Access restrictions  |Retention requirements |
|---------|---------|---------|
|This data does not leave the user machine via Usage Feedback. |- |- |
|Crash Dumps may contain Customer Content and be emitted to Microsoft. |- |Crash Dumps: Max 30 days. |
|Customers with their consent can send User Feedback that contains Customer Content to Microsoft. |Limit to Microsoft internal with no third-party access. Microsoft can expose the data to the original customer. |User Feedback: Max 1 year |

## End-user identifiable information (EUII)

Data received from a user, or generated from their use of the product.
- Linkable to an individual user.
- Does not contain content.

### Examples of end-user identifiable information

- Interface Identification. The Full IP address
- Machine Name
- Login/User names
- Local-part of e-mail address (joe@contoso.com)
- Location Information
- Customer Identification

### Permitted usage scenarios

|Scenario  |Access restrictions  |Retention requirements|
|---------|---------|---------|
|This data does not leave the user machine via Usage Feedback. |- |- |
|Crash dumps may contain EUII and be emitted to Microsoft. |- |Crash dumps: Max 30 days |
|Customer identification ID may be emitted to Microsoft to deliver new hybrid and cloud features that the users have subscribed to. |- |Currently no such hybrid or cloud features exist.|
|Customers with their consent can send User Feedback that contains customer content to Microsoft.|Limit to Microsoft internal use with no third-party access. Microsoft can expose the data to the original customer. |User feedback: Max 1 year |

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
|Customers with their consent can send User Feedback that contains Customer Content to Microsoft. |Limit to Microsoft internal use with no third-party access. |Customers with their consent can send User Feedback that contains Customer Content to Microsoft. |
|Power View and SQL Reporting Services Map Item(s) may send data for use of Bing Maps. |Limit to session data |- |

## System metadata

Data generated in the course of running the server.  The data does not contain customer content.

### Examples of system metadata

The following are considered system metadata when they do not inlcude customer content, customer access control, or EUII:

- Database GUID
- Hash of machine name
- Hash of instance name
- Application name
- Behavioral/usage data
- SQL Customer Experience improvement program data  (SQLCEIP)
- Server configuration data, for example settings of sp_configure
- Feature configuration data
- Event names and error codes
- Hardware settings and identification such as OEM Manufacturer

Microsoft does examine application name values set by other programs that use SQL Server (example: Sharepoint or 3rd party packaged programs and includes this information in System Metadata sent to Microsoft when Usage Data is enabled). Customers should not place personal data, such as end-user identifiable information, in System Metadata fields or create applications designed to store personal data in these fields. 

### Permitted usage scenarios

|Scenario  |Access Restrictions  |Retention Requirements|
|---------|---------|---------|
|May be used by Microsoft to improve features and or fix bugs in current features.|Limit to Microsoft internal use with no third-party access. |Min 90 days - Max 3 years |
|May be used to make suggestions to the customer.  For example, "Based on your usage of the product, consider using feature *X* since it would perform better." |Microsoft can expose the data to the original customer, for example through dashboards. |Customer Data Security Logs: Min 3 years - Max 6 years |
May be used by Microsoft for future product planning. |Microsoft may share this information with other hardware and software vendors to improve how their products run with Microsoft software. |Min 90 days - Max 3 years|
|May be used by Microsoft to provide cloud-based services based on emitted Usage Feedback. For example, a customer dashboard showing feature usage across all SQL Server installations in an organization. |Microsoft can expose the data to the original customer, for example, through dashboards. |Min 90 days - Max 3 years |
|Customers with their consent can send User Feedback that contains Customer Content to Microsoft. |Limit to Microsoft internal with no third-party access. Microsoft can expose the data to the original customer. |User Feedback: Max 1 year |
|May use database name and application name to categorize databases and applications into known categories, for example, those that may be running software provided by Microsoft or other companies.|Limit to Microsoft internal with no third-party access.|Min 90 days - Max 3 years |

## Object metadata

Data that describes or is used to configure servers, databases, tables, and other resources.  Object metadata includes database table and column names but not the contents of database rows or other Customer Content. Customers should not place personal data, such as end-user identifiable information in Object Metadata fields or create applications designed to store personal data in these fields. For the permitted usage scenario's below, only hash form is used to determine usage patterns to improve the product. 

### Examples of object metadata

- SQL Server database names
- Table names and column names
- Statistics names

### Permitted usage scenarios

|Scenario  |Access restrictions  |Retention requirements|
|---------|---------|---------|
|May be used by Microsoft to improve features and or fix bugs in current features. |Limited to Microsoft internal use with no third-party access. |Min 90 days - Max 3 years|

## Telemetry controls

Instructions on how telemetry can be turned on/off in product can be referenced here - https://support.microsoft.com/help/3153756/how-to-configure-sql-server-2016-to-send-feedback-to-microsoft.

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]

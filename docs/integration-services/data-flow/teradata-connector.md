---
title: "Microsoft connector for Teradata"
description: "Microsoft connector for Teradata"
author: chugugrace
ms.author: chugu
ms.reviewer: maghan
ms.date: 10/10/2022
ms.service: sql
ms.subservice: integration-services
ms.topic: how-to
---

# Microsoft Connector for Teradata (SSIS)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

Microsoft Connector for Teradata enables exporting and loading data into Teradata databases in an SSIS package.

This new connector supports databases with 1MB-enabled tables.

## Version support

Microsoft Connector supports the following Microsoft SQL Server products for Teradata:

- Microsoft SQL Server 2019
- Microsoft SQL Server 2022
- SQL Server Integration Services Projects for Visual Studio 2019
- SQL Server Integration Services Projects for Visual Studio 2022

Microsoft Connector for Teradata uses ODBC Driver for Teradata, and Teradata Parallel Transporter shipped with Teradata Tools and Utilities (TTU). Supported TTU versions are 16.20 and 17.10.

## Installation

Install TTU from [Teradata site](https://downloads.teradata.com/download/database/teradata-tools-and-utilities-13-10). Make sure that ODBC Driver for Teradata and Teradata Parallel Transporter features is selected during the installation.

To install the connector for the Teradata database, download and run the installer from [the latest version of Microsoft connector for Teradata](https://www.microsoft.com/download/details.aspx?id=100599). Then follow the directions in the installation wizard.

> [!NOTE]
> To design packages with SQL Server Integration Services Projects, you will need to install the connector for both the target and the latest SQL Server version.

After you install the connector, you must restart the SQL Server Integration Service to be sure that the Teradata source and destination work correctly.

## Design and execute SSIS packages

Microsoft Connector for Teradata provides a similar user experience to Attunity Teradata Connector. Users can design new packages based on previous experience, using SSDT for VS 2017 or VS 2019, with *targeting SQL Server 2019*.

Teradata source and destination are under Common category.

:::image type="content" source="media/teradata-connector/teradata-component.png" alt-text="Screenshot the Teradata Component.":::

Teradata connection manager is displayed as "TERADATA."

:::image type="content" source="media/teradata-connector/teradata-connection-manager-type.png" alt-text="Screenshot of the Teradata connection manager type":::

Existing SSIS packages designed with Attunity Teradata Connector are automatically upgraded to use Microsoft Connector for Teradata. The icons are changed as well.

To execute the SSIS package *targeting SQL Server 2017 and below*, you need to install **Microsoft Connector for Teradata by Attunity** with corresponding version from the below link:

- [SQL Server 2017: Microsoft Connector Version 5.0 for Teradata by Attunity](https://www.microsoft.com/download/details.aspx?id=55179)
- [SQL Server 2016: Microsoft Connector Version 4.0 for Teradata by Attunity](https://www.microsoft.com/download/details.aspx?id=52950)
- [SQL Server 2014: Microsoft Connector Version 3.0 for Teradata by Attunity](https://www.microsoft.com/download/details.aspx?id=44582)
- [SQL Server 2012: Microsoft Connector Version 2.0 for Teradata by Attunity](https://www.microsoft.com/download/details.aspx?id=29283)

To design the SSIS package in SSDT *targeting SQL Server 2017 and below*, you need to have **Microsoft Connector for Teradata** and install **Microsoft Connector for Teradata by Attunity** with the corresponding version.

## Limitations and known issues

- Teradata Source/Destination Editor: **Default database** property doesn't take effect. As a workaround, type the database name in dropdown box to filter table or view.

- Teradata Source/Destination Editor: Mapping step doesn't work when type `\<database>.<table/view>`. As work-around, type `\<database>.<table/view>`, then select the drop-down button.

- Teradata Source Editor: view can't be displayed when Data access mode is "Table Name – TPT Export." As work-around, use Advanced Editor of Teradata Source.

- Teradata Destination: attribute "PackMaximum" can't be set to "True." Otherwise, error occurs.

- Teradata Source always reads columns in the order as they're defined in the table. ODBC, on the other hand, requires columns be read in a specific order in certain circumstances [Getting Long Data](../../odbc/reference/develop-app/getting-long-data.md) and [SQLGetData](../../relational-databases/native-client-odbc-api/sqlgetdata.md). When the two orders don't match, read fails with error message "Invalid descriptor index, descriptor record doesn't exist, or descriptor record wasn't properly initialized."

- Installing a new version over an old installation doesn't remove the old version entry from the installed program list. While this practice doesn't impact functioning of the new version, user can manually uninstall the old version in the control panel "Programs, and Features" to get rid of the old version entry.

## Uninstallation

You can uninstall wizard to remove **Microsoft connector for Teradata**.

## Release Notes

### Rev. 282

**Bug fixes**

- Teradata Source can't handle newline character in SQL command (**SQL command - TPT Export** data access mode).
- Teradata Destination crashes under certain circumstances.
- Specified authentication **Mechanism** on Teradata Connection Manager Editor isn't persisted and doesn't take effect.

### Rev. 275

**Bug fixes**

- Teradata Destination crashes under certain circumstances.
- Teradata Destination reports success despite errors occurred under certain circumstances.
- Teradata Destination reports a larger number of rows written than reality under certain circumstances.

**Improvements**

- When error occurred, Teradata Destination retains and directs user to TPT error tables for investigation.

### Rev. 257

**Bug fixes**

- Memory leak in Teradata Destination.
- Teradata Destination fails with error message "an error occurred when converting string from source codepage" when consuming empty strings.
- Under certain circumstances, Data Flow Task reports success despite errors occurred in Teradata Destination.
- Teradata Destination fails when writing `VARCHAR` values with a large length.

**Improvements**

- Improved performance of Teradata Destination.
- Teradata Connection Manager logs error detail for connection failure.

### Rev. 240

**Bug fixes**

- When processing tabular data with 24 or more columns, an error occurs with message `[Teradata][ODBC] (10670) Invalid descriptor index, descriptor record doesn't exist, or descriptor record was not properly initialized.`
- A newly created Teradata Connection Manager defaults to latest version Teradata ODBC driver installed even when it isn't supported.

### Rev. 233

**Bug fixes**

- In a newly created Teradata Connection Manager, some required connection string properties like `DRIVER` aren't populated by default.

### Rev. 225

**New Features**

- Add support for TTU 17.10.

**Bug fixes**

- Teradata Source may load incorrect data under certain circumstances.
- Default error table in Teradata Destination is incorrectly named and may not be created in the database of the destination table.
- Options specified in the connection string property of Teradata Connection Manager may not be picked up and therefore don't take effect.

### Rev. 197

**Bug fixes**

- When reading empty string data, Teradata Source would fail with the error message "An error occurred when converting string to target codepage."

## Next steps

- Configure [Teradata connection manager](teradata-connection-manager.md)
- Configure [Teradata source](teradata-source.md)
- Configure [Teradata destination](teradata-destination.md)
- If you have questions, visit [Tech Community](https://aka.ms/AA6iwdw)
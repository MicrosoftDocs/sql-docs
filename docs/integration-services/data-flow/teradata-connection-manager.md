---
title: "Teradata connection manager | Microsoft Docs"
ms.custom: ""
ms.date: "11/22/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
author: chugugrace
ms.author: chugu
---
# Teradata connection manager

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]

A Teradata Connection Manager is used to enable a package to extract data from Teradata databases and load data into Teradaata databases.

The **ConnectionManagerType** property for the Teradata connection manager is set to **MSTERA**.

## Configuring the Teradata connection manager

Connection Manager configuration changes will be resolved  by Integration Services at runtime. Use the **Teradata Connection Manager Editor** dialog box to add a connection to a Teradata data source.
![connection manager editor](media/teradata-connection-manager.png)

### Options

#### Connection manager information

Enter information about the connection manager.

**Name**

Enter a name for the connection. The default name is **Teradata Connection Manager**.

**Description**

Enter a description of the connection. This is optional.

**Server info**

Enter the information of the Teradata server to connect.

**Server name**

Enter the name of the Teradata server to connect.

**Authentication**

- **Use Windows Authentication**: Select this to use Windows authentication.
- **Use Teradata Authentication**: Select this to use Teradata database authentication. Enter the credentials as below for this type of authentication:
    - **Mechanism**: Select the desired security checking mechanism, valid Mechanism values include TD1, TD2, LDAP, KRB5, KRB5C, NTLM, NTLMC.
    - **Parameter**: Type parameters required for the desired security checking mechanism.
    - **User name**: Type user name used to connect to the Teradata database.  
    - **Password**: Type Teradata database password of the user.

**Default database**

This is optional.
Select the Teradata database to connect if access permission is correct. Drop-down will return error if access permission is not correct, then manually enter the database name.

**Account**

This is optional.
Type the name of the account that identifies the user name.
If this value is empty, the account for the immediate owner of the database is used.

### Custom property

**UseUTF8CharSet**:

Specifies whether the UTF8 character set is used. The default value is true.

To set the property:

1. From the Connection Manager area in SSDT, right click the Teradata Connection Manager and select Properties.
2. In the Properties pane, select True or False for UseUTF8CharSet property.

## Teradata connection manager troubleshooting

Enable Windows ODBC tracing in the Windows ODBC Data Source Administrator, to log Teradata connection manager calls to Teradata ODBC driver.

## Next steps

- Configure [Teradata source](teradata-source.md)
- Configure [Teradata destination](teradata-destination.md).
- If you have questions, visit [Tech Community](https://aka.ms/AA5u35j).

---
title: "Oracle Connection Manager"
description: "Oracle Connection Manager"
author: chugugrace
ms.author: chugu
ms.date: "08/14/2019"
ms.service: sql
ms.subservice: integration-services
ms.topic: how-to
---
# Oracle Connection Manager

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

An Oracle Connection Manager is used to enable a package to extract data from Oracle Databases and load data into Oracle Databases.

The **ConnectionManagerType** property for the Oracle Connection Manager is set to **ORACLE**.

In SSIS execution logs, this connector is referred to as "Oracle Connection Manager."

## Configuring the Oracle Connection Manager

Oracle Connection Manager configuration changes will be resolved  by Integration Services at runtime. Use the **Oracle Connection Manager Editor** dialog box to add a connection to an Oracle data source.

![Connection Manager](media/oracle-connection-manager.png)

### Options

#### Connection manager information

Enter information about the Oracle connection.

**Name**

Input a name for the Oracle connection. The default name is Oracle Connection Manager. 

**Description** 

Input a description of the connection. This input is optional.

**TNS service name**

Input the name of the Oracle database you work with. The TNS service name could be:

- The connection name defined in the tnsnames.ora file

- EzConnect format: [//]host[:port][/service_name]

To use a tnsnames.ora file, you may need to add a system environment variable to the machine running the SSIS package. The TNS_Admin environment variable specifies the location of the folder that contains the tnsnames.ora file. This will be required if you have not installed an Oracle client. To add the environment variable in Windows 10, Windows 11 or Windows Server 2022: 

1. Right-click the Start icon and select **System**. 
2. In the Settings window, select **Advanced System Settings**. 
3. On the Advanced tab of the System Properties window, select **Environment Variables**.
4. In the Environment Variables window under System, select **New**. 
5. In the New System Variable window, enter "TNS_Admin" for the Variable name and the correct path to the folder that contains your tnsnames.ora file for the Variable value. 
6. Select **OK** in the New System Variable, Environment Variables, and System Properties windows. 

For more information, see the Oracle documentation.

#### Connection Manager logging

Select one of the below options:

- **Use Windows Authentication**: Select this to use Windows authentication.

- **Use Oracle Authentication**: Select this to use Oracle database authentication. If you use this authentication, enter your Oracle credentials as follows:  
	**User name**: Type the user name used to connect to the Oracle database.  
	**Password**: Type the Oracle database password for the user entered in the user name field.

> [!NOTE]
>
>Windows Authentication is not supported for Oracle Server 18c.

**Test Connection**

Click **Test Connection** to verify if the information provided is correct. You will receive the message **Test connection succeeded**, if the information entered is able to connect to the Oracle database.

> [!NOTE]
>
> To specify **ConnectionString** directly, here is a sample with Oracle Authentication:
>
> `SERVER=\<YourOracleServerName or EzConnect format>;USERNAME=\<YourUserName>;PWD=\<YourPassword>;WINAUTH=0`

### Custom properties

There are following custom connection manager properties in the Oracle connection manager:

- **EnableDetailedTracing**: Not Used.

- **OracleHome**: Specify 32-bit Oracle Home name or folder to be used by the connector. (Optional)

- **OracleHome64**: Specify 64-bit Oracle Home name or folder to be used by the connector when running in 64-bit mode. (Optional)

Custom properties are not listed in Oracle Connection Manager Editor. To set the **OracleHome** and **OracleHome64** properties:

1. From the Connection Manager area, right-click the Oracle connection manager you are working with and select **Properties**.

2. In the **Properties** pane, set the **OracleHome** or **OracleHome64** property with the full path to the Oracle home directory.


## Next steps

- Configure [Oracle Source](oracle-source.md).
- Configure [Oracle Destination](oracle-destination.md).
- If you have questions, visit [TechCommunity](https://aka.ms/AA5u35j).
